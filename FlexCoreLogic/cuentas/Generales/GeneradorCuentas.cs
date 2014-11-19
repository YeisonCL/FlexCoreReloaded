using FlexCoreDAOs.cuentas;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace FlexCoreLogic.cuentas.Generales
{
    internal static class GeneradorCuentas
    {
        private static string generarCuentaAux(SqlCommand pComando)
        {
            string _numeroCuenta = "";
            string _numeroCuentaAux = "";
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            do
            {
                for(int i = 0; i < 8; i++)
                {
                    int _numero = _random.Next(0, 10);
                    _numeroCuenta = _numeroCuenta + Convert.ToString(_numero);
                    System.Threading.Thread.Sleep(1);
                }
                _numeroCuentaAux = new string(_numeroCuenta.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
            } while (CuentaAhorroDAO.existeCuenta(_numeroCuenta, pComando));
            return _numeroCuentaAux;
        }

        public static string generarCuenta(int pTipoCuenta, int pTipoMoneda, SqlCommand pComando)
        {
            string _numeroCuenta = generarCuentaAux(pComando);
            if (pTipoCuenta == Constantes.AHORROVISTA)
            {
                if (pTipoMoneda == Constantes.COLONES)
                {
                    _numeroCuenta = "9" + _numeroCuenta;
                }
                else if (pTipoMoneda == Constantes.DOLARES)
                {
                    _numeroCuenta = "8" + _numeroCuenta;
                }

            }
            else if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                if (pTipoMoneda == Constantes.COLONES)
                {
                    _numeroCuenta = "7" + _numeroCuenta;
                }
                else if (pTipoMoneda == Constantes.DOLARES)
                {
                    _numeroCuenta = "6" + _numeroCuenta;
                }
            }
            return _numeroCuenta;
        }
    }
}
