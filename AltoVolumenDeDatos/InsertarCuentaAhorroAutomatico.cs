using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreLogic.cuentas.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoVolumenDeDatos
{
    public class InsertarCuentaAhorroAutomatico
    {
        public static void insertarCuentaAhorroAutomaticoBase(int pIdCliente, string pNumeroCuentaDeduccion)
        {
            ClientVDTO cliente = new ClientVDTO();
            cliente.setClientID(pIdCliente);
            CuentaAhorroAutomaticoDTO cuenta = new CuentaAhorroAutomaticoDTO();
            cuenta.setCliente(cliente);
            cuenta.setDescripcion("Cuenta De Prueba");
            cuenta.setFechaInicio(21, 11, 2014, 1, 56, 0);
            cuenta.setMagnitudPeriodoAhorro(10);
            cuenta.setMontoDeduccion(1000);
            //cuenta.setTiempoAhorro(1);
            cuenta.setNumeroCuentaDeduccion(pNumeroCuentaDeduccion);
            cuenta.setTipoMoneda(Constantes.COLONES);
            cuenta.setProposito(Constantes.SALUD);
            cuenta.setTipoPeriodo(Constantes.SEGUNDOS);
            FacadeCuentas.agregarCuentaAhorroAutomatico(cuenta);
        }
    }
}
