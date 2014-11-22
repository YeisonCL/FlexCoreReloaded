using System;
using System.Collections.Generic;
using System.Threading;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreDTOs.cuentas;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.administracion;
using FlexCoreLogic.principalogic;
using System.Windows.Forms;
using FlexCoreLogic.general;

namespace FlexCoreLogic.cuentas.Managers
{
    internal static class CuentaAhorroAutomaticoManager
    {
        public static int SLEEP = 5000;
        public static bool _sincronizacion = false;

        public static string agregarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROAUTOMATICO, pCuentaAhorroAutomatico.getTipoMoneda(), _comandoSQL);
                DateTime _fechaFinalizacion = pCuentaAhorroAutomatico.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                pCuentaAhorroAutomatico.setNumeroCuenta(_numeroCuenta);
                pCuentaAhorroAutomatico.setSaldo(0);
                pCuentaAhorroAutomatico.setEstado(false);
                pCuentaAhorroAutomatico.setFechaFinalizacion(_fechaFinalizacion.Day, _fechaFinalizacion.Month, _fechaFinalizacion.Year, _fechaFinalizacion.Hour, _fechaFinalizacion.Minute, _fechaFinalizacion.Second);
                pCuentaAhorroAutomatico.setMontoAhorro(_montoAhorro);
                pCuentaAhorroAutomatico.setUltimaFechaCobro(pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Month, pCuentaAhorroAutomatico.getFechaInicio().Year, pCuentaAhorroAutomatico.getFechaInicio().Hour, pCuentaAhorroAutomatico.getFechaInicio().Minute, pCuentaAhorroAutomatico.getFechaInicio().Second);
                CuentaAhorroAutomaticoDAO.agregarCuentaAhorroAutomaticoBase(pCuentaAhorroAutomatico, _comandoSQL);
                _comandoSQL.Transaction.Commit();
                iniciarAhorro(pCuentaAhorroAutomatico);
                return "Transaccion completada con exito";
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transaccion";
                }
                catch
                {
                    return "Ha ocurrido un error en la transaccion";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        private static string iniciarAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(() => esperarTiempoInicioAhorro(pCuentaAhorroAutomatico));
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transaccion completada con exito";
            }
            catch
            {
                return "Ha ocurrido un error en la transaccion";
            }
            
        }

        private static void esperarTiempoInicioAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while(TiempoManager.obtenerHoraActual() < pCuentaAhorroAutomatico.getFechaInicio())
            {
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, true);
            iniciarAhorroAux(pCuentaAhorroAutomatico);
        }

        private static void modificarEstadoCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, bool pEstado)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                pCuentaAhorroAutomatico.setEstado(pEstado);
                CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico, _comandoSQL);
                _comandoSQL.Transaction.Commit();
            }
            catch
            {
                try
                {
                    pCuentaAhorroAutomatico.setEstado(!pEstado);
                    _comandoSQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        private static void iniciarAhorroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.SEGUNDOS)
            {
                cobrarEnSegundos(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.MINUTOS)
            {
                cobrarEnMinutos(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.HORAS)
            {
                cobrarEnHoras(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.DIAS)
            {
                cobrarEnDias(pCuentaAhorroAutomatico);
            }
        }

        private static void cobrarEnSegundos(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            bool _hizoAhorro = false;
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddSeconds(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < TiempoManager.obtenerHoraActual())
                {
                    _hizoAhorro = true;
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalSeconds / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                if(_hizoAhorro == true)
                {
                    pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                    _hizoAhorro = false;
                }
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnDias(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            bool _hizoAhorro = false;
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddDays(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < TiempoManager.obtenerHoraActual())
                {
                    _hizoAhorro = true;
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalDays / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                if (_hizoAhorro == true)
                {
                    pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                    _hizoAhorro = false;
                }
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnMinutos(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            bool _hizoAhorro = false;
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddMinutes(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < TiempoManager.obtenerHoraActual())
                {
                    _hizoAhorro = true;
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalMinutes / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                if (_hizoAhorro == true)
                {
                    pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                    _hizoAhorro = false;
                }
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnHoras(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            bool _hizoAhorro = false;
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddHours(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < TiempoManager.obtenerHoraActual())
                {
                    _hizoAhorro = true;
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalHours / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                if (_hizoAhorro == true)
                {
                    pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                    _hizoAhorro = false;
                }
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void modificarUltimaFechaCobro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, DateTime pHoraActual, int pProporcionalidadDeCobro)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            DateTime _ultimaFechaCobro = new DateTime();
            if(pHoraActual == pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getFechaFinalizacion();
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.SEGUNDOS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddSeconds(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.MINUTOS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddMinutes(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.HORAS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddHours(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.DIAS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddDays(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            pCuentaAhorroAutomatico.setUltimaFechaCobro(_ultimaFechaCobro.Day, _ultimaFechaCobro.Month, _ultimaFechaCobro.Year, _ultimaFechaCobro.Hour, _ultimaFechaCobro.Minute, _ultimaFechaCobro.Second);
            try
            {
                CuentaAhorroAutomaticoDAO.modificarUltimaFechaCobro(pCuentaAhorroAutomatico, pCuentaAhorroAutomatico.getUltimaFechaCobro(), _comandoSQL);
                _comandoSQL.Transaction.Commit();
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        private static DateTime getHoraActualLimitada(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            if(pCuentaAhorroAutomatico.getFechaFinalizacion() < TiempoManager.obtenerHoraActual())
            {
                return pCuentaAhorroAutomatico.getFechaFinalizacion();
            }
            else
            {
                return TiempoManager.obtenerHoraActual();
            }
        }

        private static void realizarAhorro(CuentaAhorroVistaDTO pCuentaOrigen, decimal pMontoAhorro, CuentaAhorroAutomaticoDTO pCuentaDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaOrigen, _comandoSQL);
                if (_cuentaOrigen.getEstado() == false)
                {
                    Console.WriteLine("La cuenta desde donde se hace la deduccion se encuentra desactivada");
                    FacadeAdministracion.insertTransaccionVuelo("Ahorro automatico", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1, 
                        CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaOrigen, _comandoSQL), Constantes.AHORRO);
                }
                else if (_cuentaOrigen.getSaldoFlotante() < pMontoAhorro)
                {
                    Console.WriteLine("La cuenta desde donde se hace la deduccion se ha quedado sin fondos");
                    FacadeAdministracion.insertTransaccionVuelo("Ahorro automatico", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                        CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaOrigen, _comandoSQL), Constantes.AHORRO);
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(pCuentaOrigen, pMontoAhorro, pCuentaDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    FacadeAdministracion.insertTransaccionVuelo("Ahorro automatico", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completada", 1,
                        CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaOrigen, _comandoSQL), Constantes.AHORRO);
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string eliminarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroAutomaticoDAO.eliminarCuentaAhorroAutomaticoBase(pCuentaAhorroAutomatico, _comandoSQL);
                int _idCliente = CuentaAhorroDAO.obtenerCuentaAhorroIdCliente(pCuentaAhorroAutomatico, _comandoSQL);
                if (!CuentaAhorroDAO.comprobarCuentasEnCero(_idCliente, _comandoSQL))
                {
                    ClientDTO _clienteDTO = new ClientDTO();
                    _clienteDTO.setClientID(_idCliente);
                    _clienteDTO.setActive(false);
                    ClientsFacade.getInstance().setClientActiveStatus(_clienteDTO);
                }
                _comandoSQL.Transaction.Commit();
                return "Transaccion completada con exito";
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transaccion";
                }
                catch
                {
                    return "Ha ocurrido un error en la transaccion";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string modificarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoInterna = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                DateTime _fechaFinalizacion = _cuentaAhorroAutomaticoInterna.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                _cuentaAhorroAutomaticoInterna.setMontoAhorro(_montoAhorro);
                _cuentaAhorroAutomaticoInterna.setFechaFinalizacion(_fechaFinalizacion.Day, _fechaFinalizacion.Month, _fechaFinalizacion.Year, _fechaFinalizacion.Hour, _fechaFinalizacion.Minute, _fechaFinalizacion.Second);
                _cuentaAhorroAutomaticoInterna.setDescripcion(pCuentaAhorroAutomatico.getDescripcion());
                _cuentaAhorroAutomaticoInterna.setTipoMoneda(pCuentaAhorroAutomatico.getTipoMoneda());
                _cuentaAhorroAutomaticoInterna.setTiempoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro());
                _cuentaAhorroAutomaticoInterna.setMontoDeduccion(pCuentaAhorroAutomatico.getMontoDeduccion());
                _cuentaAhorroAutomaticoInterna.setProposito(pCuentaAhorroAutomatico.getProposito());
                _cuentaAhorroAutomaticoInterna.setMagnitudPeriodoAhorro(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
                _cuentaAhorroAutomaticoInterna.setTipoPeriodo(pCuentaAhorroAutomatico.getTipoPeriodo());
                _cuentaAhorroAutomaticoInterna.setNumeroCuentaDeduccion(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                CuentaAhorroAutomaticoDAO.modificarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomaticoInterna, _comandoSQL);
                _comandoSQL.Transaction.Commit();
                return "Transaccion completada con exito";
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transaccion";
                }
                catch
                {
                    return "Ha ocurrido un error en la transaccion";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaSalida = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico, _comandoSQL);
                _comandoSQL.Transaction.Commit();
                return _cuentaSalida;
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                ClientsFacade _facade = ClientsFacade.getInstance();
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroAutomatico.getCliente()).getResult();
                int idCliente = _listaClientes[0].getClientID();
                List<CuentaAhorroAutomaticoDTO> _cuentasSalida = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCedulaOCIF(_comandoSQL, idCliente);
                _comandoSQL.Transaction.Commit();
                return _cuentasSalida;
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                ClientsFacade _facade = ClientsFacade.getInstance();
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroAutomatico.getCliente()).getResult(); ;
                int idCliente = _listaClientes[0].getClientID();
                List<CuentaAhorroAutomaticoDTO> _cuentasSalida = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCedulaOCIF(_comandoSQL, idCliente);
                _comandoSQL.Transaction.Commit();
                return _cuentasSalida;
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        private static decimal calcularMontoAhorro(int pTiempoAhorro, int pMagnitudPeriodoAhorro, int pTipoPeriodo, decimal pMontoDeduccion)
        {
            decimal _montoAhorro = 0;
            if (pTipoPeriodo == Constantes.SEGUNDOS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.segundosAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.MINUTOS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.minutosAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.HORAS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.horasAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.DIAS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.diasAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            return _montoAhorro;
        }

        public static void iniciarSincronizacion()
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                if(_sincronizacion == false)
                {
                    _sincronizacion = true;
                    List<CuentaAhorroAutomaticoDTO> _cuentasAhorroAutomatico = CuentaAhorroAutomaticoDAO.obtenerTodasCuentaAhorroAutomatico(_comandoSQL);
                    foreach (CuentaAhorroAutomaticoDTO cuenta in _cuentasAhorroAutomatico)
                    {
                        iniciarAhorro(cuenta);
                    }

                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static void calcularInteres(CuentaAhorroAutomaticoDTO pCuenta, decimal pInteresTotal)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroAutomaticoDAO.agregarDinero(pCuenta, pInteresTotal, Constantes.AHORROAUTOMATICO, _comandoSQL);
                FacadeAdministracion.insertTransaccionVuelo("Interes agregado", _horaEntrada, TiempoManager.obtenerHoraActual(), "Comppletada", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuenta, _comandoSQL), Constantes.INTERES);
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }
    }
}
