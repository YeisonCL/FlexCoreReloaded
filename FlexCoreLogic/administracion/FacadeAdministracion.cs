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
    }
}
