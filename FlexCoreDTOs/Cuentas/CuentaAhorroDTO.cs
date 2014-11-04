using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.cuentas
{
    public class CuentaAhorroDTO
    {
        public string _numeroCuenta;
        public string _descripcion;
        public int _tipoMoneda;
        public decimal _saldo;
        public bool _estado;
        public ClientVDTO _cliente;

        public CuentaAhorroDTO() { }

        public CuentaAhorroDTO(string pNumeroCuenta, string pDecripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, ClientVDTO pCliente)
        {
            _numeroCuenta = pNumeroCuenta;
            _descripcion = pDecripcion;
            _saldo = pSaldo;
            _estado = pEstado;
            _tipoMoneda = pTipoMoneda;
            _cliente = pCliente;
        }

        public string getNumeroCuenta()
        {
            return _numeroCuenta;
        }

        public string getDescripcion()
        {
            return _descripcion;
        }

        public decimal getSaldo()
        {
            return _saldo;
        }

        public void setSaldo(decimal pSaldo)
        {
            _saldo = pSaldo;
        }

        public bool getEstado()
        {
            return _estado;
        }

        public int getTipoMoneda()
        {
            return _tipoMoneda;
        }

        public void setNumeroCuenta(string pNumeroCuenta)
        {
            _numeroCuenta = pNumeroCuenta;
        }

        public void setDescripcion(string pDescripcion)
        {
            _descripcion = pDescripcion;
        }

        public void setEstado(bool pEstado)
        {
            _estado = pEstado;
        }

        public void setTipoMoneda(int pTipoMoneda)
        {
            _tipoMoneda = pTipoMoneda;
        }

        public void setCliente(ClientVDTO pCliente)
        {
            _cliente = pCliente;
        }

        public ClientVDTO getCliente()
        {
            return _cliente;
        }
    }
}
