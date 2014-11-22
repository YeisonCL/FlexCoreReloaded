using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreLogic.cuentas.Generales;

namespace AltoVolumenDeDatos
{
    public static class InsertarCuentaAhorroVista
    {
        public static string insertarCuentaAhorroVistaBase(int pIdCliente)
        {
            ClientVDTO cliente = new ClientVDTO();
            cliente.setClientID(pIdCliente);
            CuentaAhorroVistaDTO cuenta = new CuentaAhorroVistaDTO();
            cuenta.setListaBeneficiarios(null);
            cuenta.setCliente(cliente);
            cuenta.setDescripcion("Cuenta De Prueba V");
            cuenta.setTipoMoneda(Constantes.COLONES);
            FacadeCuentas.agregarCuentaAhorroVista(cuenta);
            FacadeCuentas.agregarDineroCuentaAhorroVista(cuenta, 900000000);
            return cuenta.getNumeroCuenta();
        }
    }
}
