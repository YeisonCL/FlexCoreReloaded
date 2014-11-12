using FlexCoreDAOs.administration;
using FlexCoreDTOs.administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.cuentas.Generales
{
    public static class TransaccionesVuelo
    {
        public static void insertTransaccionVuelo(string pDescripcion, DateTime pFecha, int pIdCuenta, int pTipoTransaccion)
        {
            TransaccionesVueloQueriesDAO _transaccionVuelo = new TransaccionesVueloQueriesDAO();
            _transaccionVuelo.insertTransaccionVuelo(pDescripcion, pFecha, pIdCuenta, pTipoTransaccion);
        }

        public static void moverTransaccionesEnVueloAHistorial()
        {
            TransaccionesVueloQueriesDAO _transaccionesVueloDAO = new TransaccionesVueloQueriesDAO();
            HistoricoTransaccionalQueriesDAO _historicoTransaccional = new HistoricoTransaccionalQueriesDAO();
            List<TransaccionesVueloDTO> _transaccionesVuelo = _transaccionesVueloDAO.getTransaccionesEnVuelo();
            foreach(TransaccionesVueloDTO transaccion in _transaccionesVuelo)
            {
                _historicoTransaccional.insertHistoricoTransaccional(transaccion.getDescripcion(), transaccion.getFechaHora(), transaccion.getIdCuenta(),
                    transaccion.getTipoTransaccion());
                _transaccionesVueloDAO.deleteTransaccionVuelo(transaccion.getIdTransaccion());
            }
        }
    }
}
