using FlexCoreDAOs.cuentas;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace FlexCoreLogic.cuentas.Generales
{
    internal static class GeneradorCuentas
    {
        public static string generarCuenta(int pTipoCuenta, int pTipoMoneda, SqlCommand pComando)
        {
            string _numeroCuenta = "";
            string _numeroCuentaAux = "";
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            do
            {
                _numeroCuentaAux = "";
                _numeroCuenta = "";
                for (int i = 0; i < 8; i++)
                {
                    int _numero = _random.Next(0, 10);
                    _numeroCuenta = _numeroCuenta + Convert.ToString(_numero);
                }
                _numeroCuentaAux = new string(_numeroCuenta.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
                if (pTipoCuenta == Constantes.AHORROVISTA)
                {
                    if (pTipoMoneda == Constantes.COLONES)
                    {
                        _numeroCuentaAux = "9" + _numeroCuenta;
                    }
                    else if (pTipoMoneda == Constantes.DOLARES)
                    {
                        _numeroCuentaAux = "8" + _numeroCuenta;
                    }
                }
                else if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
                {
                    if (pTipoMoneda == Constantes.COLONES)
                    {
                        _numeroCuentaAux = "7" + _numeroCuenta;
                    }
                    else if (pTipoMoneda == Constantes.DOLARES)
                    {
                        _numeroCuentaAux = "6" + _numeroCuenta;
                    }
                }
            } while (CuentaAhorroDAO.existeCuenta(_numeroCuentaAux, pComando));
            return _numeroCuentaAux;
        }
    }
}
