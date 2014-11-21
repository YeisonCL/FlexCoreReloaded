using System.Collections.Generic;
using System.Threading;
using FlexCoreLogic.cuentas.Generales;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDTOs.cuentas;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.administracion;
using System;
using FlexCoreLogic.principalogic;

namespace FlexCoreLogic.cuentas.Managers
{
    internal static class CuentaAhorroVistaManager
    {
        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROVISTA, pCuentaAhorroVista.getTipoMoneda(), _comandoSQL);
                pCuentaAhorroVista.setNumeroCuenta(_numeroCuenta);
                pCuentaAhorroVista.setSaldo(0);
                pCuentaAhorroVista.setSaldoFlotante(0);
                CuentaAhorroVistaDAO.agregarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoSQL);
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

        public static string eliminarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.eliminarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoSQL);
                int _idCliente = CuentaAhorroDAO.obtenerCuentaAhorroIdCliente(pCuentaAhorroVista, _comandoSQL);
                if(!CuentaAhorroDAO.comprobarCuentasEnCero(_idCliente, _comandoSQL))
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

        public static string modificarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.modificarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoSQL);
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

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDTO _cuentaSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista, _comandoSQL);
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

        public static string obtenerNumeroCuenta(int pIdCuenta)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                return CuentaAhorroDAO.obtenerNumeroCuenta(pIdCuenta, _comandoSQL);
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

        public static List<CuentaAhorroVistaDTO> obtenerTodasCuentaAhorroVista()
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                List<CuentaAhorroVistaDTO> _cuentaSalida = CuentaAhorroVistaDAO.obtenerTodasCuentaAhorroVista(_comandoSQL);
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

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                ClientsFacade _facade = ClientsFacade.getInstance();
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroVista.getCliente());
                int idCliente = _listaClientes[0].getClientID();
                List<CuentaAhorroVistaDTO> _cuentaSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedulaOCIF(pCuentaAhorroVista, _comandoSQL, idCliente);
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

        public static string agregarNuevosBeneficiarios(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaBeneficiariosDAO.agregarBeneficiarios(pCuentaAhorroVista, _comandoSQL);
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

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                ClientsFacade _facade = ClientsFacade.getInstance();
                List<ClientVDTO> _listaClientes = _facade.searchClient(pCuentaAhorroVista.getCliente());
                int idCliente = _listaClientes[0].getClientID();
                List<CuentaAhorroVistaDTO> _cuentasSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedulaOCIF(pCuentaAhorroVista, _comandoSQL, idCliente);
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

        public static int obtenerCuentaAhorroVistaID(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, _comandoSQL);
                _comandoSQL.Transaction.Commit();
                return _id;
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista, pMonto, Constantes.AHORROVISTA, _comandoSQL);
                _comandoSQL.Transaction.Commit();
                FacadeAdministracion.insertTransaccionVuelo("Deposito de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completa", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, _comandoSQL), Constantes.DEPOSITO);
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

        public static string realizarCierreCuentas()
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(realizarCierreCuentasAux);
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transaccion completada con exito";
            }
            catch
            {
                return "Ha ocurrido un error en la transaccion";
            }
        }

        private static void realizarCierreCuentasAux()
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.iniciarCierre(_comandoSQL);
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
    }
}
