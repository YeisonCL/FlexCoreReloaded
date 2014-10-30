using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDAOs.cuentas
{
    public static class Transformaciones
    {
        public static int boolToInt(bool pEstado)
        {
            if (pEstado == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool intToBool(int pEstado)
        {
            if (pEstado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static decimal convertirDinero(decimal pMonto, int pMonedaOrigen, int pMonedaDestino)
        {
            if (pMonedaOrigen == ConstantesDAO.COLONES && pMonedaDestino == ConstantesDAO.DOLARES)
            {
                //SE REGISTRA CAMBIO DE DOLARES A COLONES.
                return dolaresAColores(pMonto);
            }
            else if (pMonedaOrigen == ConstantesDAO.DOLARES && pMonedaDestino == ConstantesDAO.COLONES)
            {
                //SE REGISTRA CAMBIO DE COLONES A DOLARES
                return colonesADolares(pMonto);
            }
            else
            {
                return pMonto;
            }
        }

        private static decimal dolaresAColores(decimal pMonto)
        {
            return pMonto * ConstantesDAO.UNDOLARENCOLONES;
        }

        private static decimal colonesADolares(decimal pMonto)
        {
            return pMonto / ConstantesDAO.UNDOLARENCOLONES;
        }
    }
}
