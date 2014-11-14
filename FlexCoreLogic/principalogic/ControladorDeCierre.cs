using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDAOs.administration;
using FlexCoreDAOs.cuentas;
using FlexCoreDTOs.administration;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.administracion;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace FlexCoreLogic.principalogic
{
    public static class ControladorDeCierre
    {
        static int _tiempoEspera = 1000;
        static bool _logicaIniciada = false;

        public static void iniciarControladorDeCierre()
        {
            if(_logicaIniciada == false)
            {
                _logicaIniciada = true;
                ThreadStart _delegado = new ThreadStart(iniciarControladorDeCierreAux);
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
            }
        }

        private static void iniciarControladorDeCierreAux()
        {
            while(true)
            {
                if(TiempoManager.obtenerCambioDeDia())
                {
                    iniciarCierre();
                    TiempoManager.apagarCambioDeDia();
                    Thread.Sleep(_tiempoEspera);
                }
                else
                {
                    Thread.Sleep(_tiempoEspera);
                }
            }
        }

        public static void iniciarCierre()
        {
            SqlCommand _comandoSQL = Conexiones.obtenerConexionSQL();
            try
            {
                CierreQueriesDAO _cierre = new CierreQueriesDAO();
                ConfiguracionesQueriesDAO _configuraciones = new ConfiguracionesQueriesDAO();
                List<ConfiguracionesDTO> _listaConfiguraciones = new List<ConfiguracionesDTO>();
                List<CierreDTO> _listaCierres = new List<CierreDTO>();
                _listaConfiguraciones = _configuraciones.getConfiguracion();
                _listaCierres = _cierre.getCierre();
                List<CuentaAhorroAutomaticoDTO> _cuentas = CuentaAhorroAutomaticoDAO.obtenerTodasCuentaAhorroAutomatico(_comandoSQL);
                _cierre.insertCierre(TiempoManager.obtenerHoraActual(), true);
                FacadeCuentas.realizarCierreCuentas();
                FacadeAdministracion.moverTransaccionesEnVueloAHistorial();
                decimal _dias = 30;
                foreach(CuentaAhorroAutomaticoDTO cuenta in _cuentas)
                {
                    decimal plazo = _dias / (_dias * (Convert.ToDecimal((cuenta.getFechaFinalizacion() - cuenta.getFechaInicio()).TotalDays)/_dias));
                    decimal interes = _listaConfiguraciones[0].getTasaInteresAhorro();
                    decimal valorActual = cuenta.getMontoAhorro() * Convert.ToDecimal((TiempoManager.obtenerHoraActual() - _listaCierres[0].getFechaHora()).TotalHours);
                    decimal interesTotal = plazo * interes * valorActual;
                    CuentaAhorroAutomaticoManager.calcularInteres(cuenta, interesTotal);
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
