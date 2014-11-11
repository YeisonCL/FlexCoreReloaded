using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreDTOs.cuentas;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.clients;
using FlexCoreDTOs.clients;

namespace FlexCoreLogic.cuentas.Managers
{
    internal static class CuentaAhorroAutomaticoManager
    {
        public static int SLEEP = 1000;
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
                Console.WriteLine(iniciarAhorro(pCuentaAhorroAutomatico));
                return "Transacción completada con éxito";
            }
            catch(Exception ex)
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    Console.WriteLine(ex.Message + ex.TargetSite);
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string iniciarAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(() => esperarTiempoInicioAhorro(pCuentaAhorroAutomatico));
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
            
        }

        private static void esperarTiempoInicioAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while(Tiempo.getHoraActual() < pCuentaAhorroAutomatico.getFechaInicio())
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
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddSeconds(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalSeconds / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnDias(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddDays(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalDays / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnMinutos(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddMinutes(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalMinutes / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            modificarEstadoCuentaAhorroAutomatico(pCuentaAhorroAutomatico, false);
        }

        private static void cobrarEnHoras(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddHours(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalHours / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
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
            if(pCuentaAhorroAutomatico.getFechaFinalizacion() < Tiempo.getHoraActual())
            {
                return pCuentaAhorroAutomatico.getFechaFinalizacion();
            }
            else
            {
                return Tiempo.getHoraActual();
            }
        }

        private static void realizarAhorro(CuentaAhorroVistaDTO pCuentaOrigen, decimal pMontoAhorro, CuentaAhorroAutomaticoDTO pCuentaDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaOrigen, _comandoSQL);
                if (_cuentaOrigen.getEstado() == false)
                {
                    Console.WriteLine("La cuenta desde donde se hace la deduccion se encuentra desactivada");
                    //GENERO EL ERROR A LA TABLA DE ERRORES.
                }
                else if (_cuentaOrigen.getSaldoFlotante() < pMontoAhorro)
                {
                    Console.WriteLine("La cuenta desde donde se hace la deduccion se ha quedado sin fondos");
                    //SE GENERA EL ERROR A LA TABLA DE ERRORES
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(pCuentaOrigen, pMontoAhorro, pCuentaDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
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
                _comandoSQL.Transaction.Commit();
                return "Transacción completada con éxito";
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
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
                return "Transacción completada con éxito";
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
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
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroAutomatico.getCliente());
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
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroAutomatico.getCliente());
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
    }
}
