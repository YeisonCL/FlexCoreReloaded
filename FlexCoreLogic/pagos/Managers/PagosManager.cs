using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.administracion;
using System;
using FlexCoreLogic.principalogic;


namespace FlexCoreLogic.pagos.Managers
{
    internal class PagosManager
    {    
        private static bool verificarCliente(int pIdCliente)
        {
            ClientsFacade _facadeCliente = ClientsFacade.getInstance();
            ClientDTO _cliente = new ClientDTO();
            _cliente.setClientID(pIdCliente);
            return _facadeCliente.isClientActive(_cliente);
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoSQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoSQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**TFondos insuficientes**";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta origen desactivada**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta destino desactivada**";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completa", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**BTransaccion exitosa**";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "**MError en la transaccion**";
                }
                catch
                {
                    return "**MError en la transaccion**";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoSQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoSQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**TFondos insuficientes**";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta origen desactivada**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta destino en ahorro**";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completa", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**BTransaccion exitosa**";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "**MError en la transaccion**";
                }
                catch
                {
                    return "**MError en la transaccion**";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoSQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoSQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta origen en ahorro**";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**TFondos insuficientes**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta destino en ahorro**";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completa", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**BTransaccion exitosa**";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "**BError en la transaccion**";
                }
                catch
                {
                    return "**BError en la transaccion**";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                DateTime _horaEntrada = TiempoManager.obtenerHoraActual();
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoSQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoSQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta origen en ahorro**";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**TFondos insuficientes**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Erronea", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**MCuenta destino desactivada**";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    FacadeAdministracion.insertTransaccionVuelo("Transferencia de dinero", _horaEntrada, TiempoManager.obtenerHoraActual(), "Completa", 1,
                    CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL), Constantes.TRANSFERENCIA);
                    return "**BTransaccion exitosa**";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "**MError en la transaccion**";
                }
                catch
                {
                    return "**MError en la transaccion**";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }
    }
}
