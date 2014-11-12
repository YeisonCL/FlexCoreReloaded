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
                    return "*Fondos insuficientes*";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "*La cuenta con la cual se desea pagar se encuentra actualmente desactivada*";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "*El cliente que desea hacer el pago se encuentra inactivo*";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "*El cliente al cual se desea hacer el pago se encuentra inactivo*";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "*La cuenta a la cual se desea pagar se encuentra actualmente desactivada*";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    return "*Transaccion completada con exito*";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "*Ha ocurrido un error en la transaccion*";
                }
                catch
                {
                    return "*Ha ocurrido un error en la transaccion*";
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
                    return "*Fondos insuficientes*";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "*La cuenta con la cual se desea pagar se encuentra actualmente desactivada*";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "*El cliente que desea hacer el pago se encuentra inactivo*";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "*El cliente al cual se desea hacer el pago se encuentra inactivo*";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "*La cuenta a la cual se desea pagar se encuentra actualmente en ahorro*";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    return "*Transaccion completada con exito*";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "*Ha ocurrido un error en la transaccion*";
                }
                catch
                {
                    return "*Ha ocurrido un error en la transaccion*";
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
                    return "*La cuenta con la cual se desea pagar se encuentra actualmente en ahorro*";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "*Fondos insuficientes*";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "*El cliente que desea hacer el pago se encuentra inactivo*";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "*El cliente al cual se desea hacer el pago se encuentra inactivo*";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "*La cuenta a la cual se desea pagar se encuentra actualmente en ahorro*";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    return "*Transaccion completada con exito*";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "*Ha ocurrido un error en la transaccion*";
                }
                catch
                {
                    return "*Ha ocurrido un error en la transaccion*";
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
                    return "*La cuenta con la cual se desea pagar se encuentra actualmente en ahorro*";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "*Fondos insuficientes*";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "*El cliente que desea hacer el pago se encuentra inactivo*";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "*El cliente al cual se desea hacer el pago se encuentra inactivo*";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "*La cuenta a la cual se desea pagar se encuentra actualmente desactivada*";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoSQL);
                    _comandoSQL.Transaction.Commit();
                    return "*Transaccion completada con exito*";
                }
            }
            catch
            {
                try
                {
                    _comandoSQL.Transaction.Rollback();
                    return "*Ha ocurrido un error en la transaccion*";
                }
                catch
                {
                    return "*Ha ocurrido un error en la transaccion*";
                }
            }
            finally
            {
                SQLServerManager.closeConnection(_comandoSQL.Connection);
            }
        }
    }
}
