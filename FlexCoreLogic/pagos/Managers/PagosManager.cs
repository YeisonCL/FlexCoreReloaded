using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Managers;
using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDAOs.cuentas;


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
                TransaccionesVuelo.insertTransaccionVuelo("Transferencia de dinero", Tiempo.getHoraActual(), CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen,_comandoSQL),
                    Constantes.TRANSFERENCIA);
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoSQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoSQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "**TFondos insuficientes**";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "**MCuenta origen desactivada**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "**MCuenta destino desactivada**";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
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
                TransaccionesVuelo.insertTransaccionVuelo("Transferencia de dinero", Tiempo.getHoraActual(), CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVistaOrigen, _comandoSQL),
                    Constantes.TRANSFERENCIA);
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoSQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoSQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "**TFondos insuficientes**";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "**MCuenta origen desactivada**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "**MCuenta destino en ahorro**";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
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
                TransaccionesVuelo.insertTransaccionVuelo("Transferencia de dinero", Tiempo.getHoraActual(), CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL),
                    Constantes.TRANSFERENCIA);
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoSQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoSQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    return "**MCuenta origen en ahorro**";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "**TFondos insuficientes**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "**MCuenta destino en ahorro**";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
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
                TransaccionesVuelo.insertTransaccionVuelo("Transferencia de dinero", Tiempo.getHoraActual(), CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomaticoOrigen, _comandoSQL),
                    Constantes.TRANSFERENCIA);
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoSQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoSQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    return "**MCuenta origen en ahorro**";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "**TFondos insuficientes**";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "**MCliente origen inactivo**";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "**MCliente destino inactivo**";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "**MCuenta destino desactivada**";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
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
