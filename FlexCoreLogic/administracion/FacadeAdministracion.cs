using FlexCoreDAOs.administration;
using FlexCoreDTOs.administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.administracion
{
    public static class FacadeAdministracion
    {
        public static ConfiguracionesDTO obtenerHoraSistema()
        {
            ConfiguracionesQueriesDAO _configuraciones = new ConfiguracionesQueriesDAO();
            List<ConfiguracionesDTO> _listaConfiguraciones = new List<ConfiguracionesDTO>();
            _listaConfiguraciones = _configuraciones.getConfiguracion();
            ConfiguracionesDTO horaSalida = new ConfiguracionesDTO(_listaConfiguraciones[0].getCompraDolar(), _listaConfiguraciones[0].getVentaDolar(),
                _listaConfiguraciones[0].getFechaHoraActual(), _listaConfiguraciones[0].getTasaInteresAhorro());
            return horaSalida;
        }

        public static List<CierreDTO> obtenerCierresBancarios()
        {

            CierreQueriesDAO _cierre = new CierreQueriesDAO();;
            List<CierreDTO> _listaCierres = new List<CierreDTO>();
            _listaCierres = _cierre.getCierre();
            return _listaCierres;
        }

        public static void insertTransaccionVuelo(String pDescripcion, DateTime pFechaHoraEntrada, DateTime pFechaHoraSalida, string pEstado, int pVersionAplicacion,
            int idCuenta, int tipoTransaccion)
        {
            TransaccionesVueloQueriesDAO _transaccionVuelo = new TransaccionesVueloQueriesDAO();
            _transaccionVuelo.insertTransaccionVuelo(pDescripcion, pFechaHoraEntrada, pFechaHoraSalida, pEstado, pVersionAplicacion, idCuenta, tipoTransaccion);
        }

        public static void moverTransaccionesEnVueloAHistorial()
        {
            TransaccionesVueloQueriesDAO _transaccionesVueloDAO = new TransaccionesVueloQueriesDAO();
            HistoricoTransaccionalQueriesDAO _historicoTransaccional = new HistoricoTransaccionalQueriesDAO();
            List<TransaccionesVueloDTO> _transaccionesVuelo = _transaccionesVueloDAO.getTransaccionesEnVuelo();
            foreach (TransaccionesVueloDTO transaccion in _transaccionesVuelo)
            {
                _historicoTransaccional.insertHistoricoTransaccional(transaccion.getDescripcion(), transaccion.getFechaHoraEntrada(), transaccion.getFechaHoraSalida(),
                    transaccion.getEstado(), transaccion.getVersionAplicacion(), transaccion.getIdCuenta(), transaccion.getTipoTransaccion());
                _transaccionesVueloDAO.deleteTransaccionVuelo(transaccion.getIdTransaccion());
            }
        }
    }
}
