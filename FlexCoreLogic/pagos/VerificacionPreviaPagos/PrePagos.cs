using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.cuentas;
using FlexCoreDAOs.administration;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Managers;
using FlexCoreLogic.pagos.Facade;
using FlexCoreLogic.cuentas.Facade;

namespace FlexCoreLogic.pagos.VerificacionPreviaPagos
{
    public class PrePagos
    {
        string _respuesta = "";

        public string iniciarPago(string pCuentaDestino, string pNumeroTarjeta, string pMonto)
        {
            CuentaAhorroVistaDTO _cuentaAhorroVistaOrigen = new CuentaAhorroVistaDTO();
            CuentaAhorroVistaDTO _cuentaAhorroVistaDestino = new CuentaAhorroVistaDTO();
            CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoOrigen = new CuentaAhorroAutomaticoDTO();
            CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDestino = new CuentaAhorroAutomaticoDTO();
            DispositivoCuentaQueriesDAO _dipositivo = new DispositivoCuentaQueriesDAO();
            if(_dipositivo.existeDispositivo(pNumeroTarjeta) == false)
            {
                return "El dispositivo utilizado no se encuentra asociado a ninguna cuenta";
            }
            else if(_dipositivo.dispositivoActivo(pNumeroTarjeta) == false)
            {
                return "El dispositivo asociado a la cuenta se encuentra desactivado";
            }
            else
            {
                int cuentaDestinoTipo = identificarCuentas(pCuentaDestino);
                string numeroCuentaOrigen = obtenerNumeroCuentaOrigen(pNumeroTarjeta, _dipositivo);
                int cuentaOrigenTipo = identificarCuentas(numeroCuentaOrigen);
                if (cuentaOrigenTipo == Constantes.AHORROAUTOMATICO && cuentaDestinoTipo == Constantes.AHORROAUTOMATICO)
                {
                    _cuentaAhorroAutomaticoOrigen.setNumeroCuenta(numeroCuentaOrigen);
                    _cuentaAhorroAutomaticoDestino.setNumeroCuenta(pCuentaDestino);
                    _respuesta = FacadePagos.realizarPagoODebitoCuentoAhorroAutomatico(_cuentaAhorroAutomaticoOrigen, Convert.ToDecimal(pMonto), _cuentaAhorroAutomaticoDestino);
                }
                else if (cuentaOrigenTipo == Constantes.AHORROAUTOMATICO && cuentaDestinoTipo == Constantes.AHORROVISTA)
                {
                    _cuentaAhorroAutomaticoOrigen.setNumeroCuenta(numeroCuentaOrigen);
                    _cuentaAhorroVistaDestino.setNumeroCuenta(pCuentaDestino);
                    _respuesta = FacadePagos.realizarPagoODebitoCuentoAhorroAutomatico(_cuentaAhorroAutomaticoOrigen, Convert.ToDecimal(pMonto), _cuentaAhorroVistaDestino);
                }
                else if (cuentaOrigenTipo == Constantes.AHORROVISTA && cuentaDestinoTipo == Constantes.AHORROAUTOMATICO)
                {
                    _cuentaAhorroVistaOrigen.setNumeroCuenta(numeroCuentaOrigen);
                    _cuentaAhorroAutomaticoDestino.setNumeroCuenta(pCuentaDestino);
                    _respuesta = FacadePagos.realizarPagoODebitoCuentaAhorroVista(_cuentaAhorroVistaOrigen, Convert.ToDecimal(pMonto), _cuentaAhorroAutomaticoDestino);
                }
                else if (cuentaOrigenTipo == Constantes.AHORROVISTA && cuentaDestinoTipo == Constantes.AHORROVISTA)
                {
                    _cuentaAhorroVistaOrigen.setNumeroCuenta(numeroCuentaOrigen);
                    _cuentaAhorroVistaDestino.setNumeroCuenta(pCuentaDestino);
                    _respuesta = FacadePagos.realizarPagoODebitoCuentaAhorroVista(_cuentaAhorroVistaOrigen, Convert.ToDecimal(pMonto), _cuentaAhorroVistaDestino);
                }
                return _respuesta;
            }
        }

        private string obtenerNumeroCuentaOrigen(string pNumeroTarjeta, DispositivoCuentaQueriesDAO pDispositivo)
        {
            int idCuenta = pDispositivo.obtenerIdCuenta(pNumeroTarjeta);
            return FacadeCuentas.obtenerNumeroCuenta(idCuenta);
        }

        private int identificarCuentas(string pNumeroCuenta)
        {
            int resultado = -1;
            switch (pNumeroCuenta.ElementAt(0))
            {
                case '6':
                    resultado = Constantes.AHORROAUTOMATICO;
                    break;
                case '7':
                    resultado = Constantes.AHORROAUTOMATICO;
                    break;
                case '8':
                    resultado = Constantes.AHORROVISTA;
                    break;
                case '9':
                    resultado = Constantes.AHORROVISTA;
                    break;
            }
            return resultado;
        }
    }
}
