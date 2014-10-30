﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FlexCoreLogic.cuentas.Generales;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDTOs.cuentas;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.clients;
using FlexCoreDTOs.clients;

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

        public static string eliminarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.eliminarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoSQL);
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

        public static string modificarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.modificarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoSQL);
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
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista, pMonto, Constantes.AHORROVISTA, _comandoSQL);
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

        public static string realizarCierreCuentas()
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(realizarCierreCuentasAux);
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
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
