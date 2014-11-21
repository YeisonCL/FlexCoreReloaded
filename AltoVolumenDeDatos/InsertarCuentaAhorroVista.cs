using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;

namespace AltoVolumenDeDatos
{
    public static class InsertarCuentaAhorroVista
    {
        public static void insertarCuentaAhorroVistaBase(int pIdCliente)
        {
            ClientVDTO pcliente = new ClientVDTO();
            pcliente.setClientID(pIdCliente);
            CuentaAhorroVistaDTO cuenta = new CuentaAhorroVistaDTO();
            cuenta.setListaBeneficiarios(null);
            cuenta.setCliente(pcliente);
            cuenta.setDescripcion("Cuenta De Prueba");
            cuenta.setEstado(true);
            cuenta.setTipoMoneda(1);
            FacadeCuentas.agregarCuentaAhorroVista(cuenta);
        }
    }
}
