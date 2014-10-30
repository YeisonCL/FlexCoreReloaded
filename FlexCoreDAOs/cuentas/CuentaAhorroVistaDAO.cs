using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDAOs.cuentas
{
    public static class CuentaAhorroVistaDAO
    {
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroVista, pComando);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            String _query = "INSERT INTO CUENTA_AHORRO_VISTA(SALDOFLOTANTE, IDCUENTA) VALUES(@saldoFlotante, @idCuenta);";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@saldoFlotante", pCuentaAhorroVista.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            CuentaBeneficiariosDAO.agregarBeneficiarios(pCuentaAhorroVista, pComando);
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroVista, pComando);
        }

        public static void eliminarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            CuentaBeneficiariosDAO.eliminarBeneficiario(pCuentaAhorroVista, pComando);
            String _query = "DELETE FROM CUENTA_AHORRO_VISTA WHERE idCuenta = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            CuentaAhorroDAO.eliminarCuentaAhorro(pCuentaAhorroVista, pComando);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaSalida = null;
            List<PhysicalPersonDTO> _listaBeneficiarios = CuentaBeneficiariosDAO.obtenerListaBeneficiarios(pCuentaAhorroVista, pComando);
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorroVista.getNumeroCuenta());
            SqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                string _numeroCuenta = _reader["numCuenta"].ToString();
                string _descripcion = _reader["descripcion"].ToString();
                decimal _saldo = Convert.ToDecimal(_reader["saldo"]);
                bool _estado = Transformaciones.intToBool(Convert.ToInt32(_reader["activa"]));
                int _tipoMoneda = Convert.ToInt32(_reader["idMoneda"]);
                decimal _saldoFlotante = Convert.ToDecimal(_reader["saldoFlotante"]);
                int _idCliente = Convert.ToInt32(_reader["idCliente"]);
                ClientVDTO _cliente = new ClientVDTO();
                _cliente.setClientID(_idCliente);
                _cuentaSalida = new CuentaAhorroVistaDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente, _saldoFlotante, _listaBeneficiarios);
            }
            _reader.Close();
            return _cuentaSalida;
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaCedulaOCIF(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando, int pIDCliente)
        {
            List<CuentaAhorroVistaDTO> _cuentasSalida = new List<CuentaAhorroVistaDTO>();
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE IDCLIENTE = @idCliente";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCliente", pIDCliente);
            SqlDataReader _reader = pComando.ExecuteReader();
            if (_reader.Read())
            {
                string _numeroCuenta = _reader["numCuenta"].ToString();
                string _descripcion = _reader["descripcion"].ToString();
                decimal _saldo = Convert.ToDecimal(_reader["saldo"]);
                bool _estado = Transformaciones.intToBool(Convert.ToInt32(_reader["activa"]));
                int _tipoMoneda = Convert.ToInt32(_reader["idMoneda"]);
                decimal _saldoFlotante = Convert.ToDecimal(_reader["saldoFlotante"]);
                int _idCliente = pIDCliente;
                ClientVDTO _cliente = new ClientVDTO();
                _cliente.setClientID(_idCliente);
                CuentaAhorroVistaDTO _cuentaSalidaAux = new CuentaAhorroVistaDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente, _saldoFlotante, null);
                _cuentasSalida.Add(_cuentaSalidaAux);
            }
            _reader.Close();
            _cuentasSalida = setearBeneficiarios(_cuentasSalida, pComando);
            return _cuentasSalida;
        }

        private static List<CuentaAhorroVistaDTO> setearBeneficiarios(List<CuentaAhorroVistaDTO> pListaCuentas, SqlCommand pComando)
        {
            List<PhysicalPersonDTO> _listaBeneficiarios = new List<PhysicalPersonDTO>();
            foreach(CuentaAhorroVistaDTO cuenta in pListaCuentas)
            {
                _listaBeneficiarios = CuentaBeneficiariosDAO.obtenerListaBeneficiarios(cuenta, pComando);
                cuenta.setListaBeneficiarios(_listaBeneficiarios);
            }
            return pListaCuentas;
        }

        public static void agregarDinero(CuentaAhorroDTO pCuentaAhorro, decimal pMonto, int pTipoCuenta, SqlCommand pComando)
        {
            if(pTipoCuenta == ConstantesDAO.AHORROVISTA)
            {
                CuentaAhorroVistaDTO _cuentaAhorroVista = new CuentaAhorroVistaDTO();
                _cuentaAhorroVista.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                agregarDineroAux(_cuentaAhorroVista, pMonto, pComando);
            }
            else if(pTipoCuenta == ConstantesDAO.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomatico.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                CuentaAhorroAutomaticoDAO.agregarDinero(_cuentaAhorroAutomatico, pMonto, ConstantesDAO.AHORROAUTOMATICO, pComando);
            }
        }

        private static void agregarDineroAux(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto, SqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaAhorroVista = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista, pComando);
            _cuentaAhorroVista.setSaldoFlotante(_cuentaAhorroVista.getSaldoFlotante() + pMonto);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroVista, pComando);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroVista.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static void quitarDinero(CuentaAhorroDTO pCuentaOrigen, decimal pMonto, CuentaAhorroDTO pCuentaDestino, int pTipoCuenta, SqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaOrigenEntrada = new CuentaAhorroVistaDTO();
            _cuentaOrigenEntrada.setNumeroCuenta(pCuentaOrigen.getNumeroCuenta());
            CuentaAhorroVistaDTO _cuentaAhorroOrigen = obtenerCuentaAhorroVistaNumeroCuenta(_cuentaOrigenEntrada, pComando);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroOrigen, pComando);
            decimal _montoDeduccion = Transformaciones.convertirDinero(pMonto, _cuentaAhorroOrigen.getTipoMoneda(), CuentaAhorroDAO.obtenerCuentaAhorroMoneda(pCuentaDestino, pComando));
            _cuentaAhorroOrigen.setSaldoFlotante(_cuentaAhorroOrigen.getSaldoFlotante() - _montoDeduccion);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroOrigen.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            agregarDinero(pCuentaDestino, pMonto, pTipoCuenta, pComando);
        }

        public static void iniciarCierre(SqlCommand pComando)
        {
            CuentaAhorroDTO _cuentaAhorro = new CuentaAhorroDTO();
            List<Tuple<string, decimal>> _cuentas = new List<Tuple<string, decimal>>();
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            SqlDataReader _reader = pComando.ExecuteReader();
            while(_reader.Read())
            {
                var _cuentaBase = new Tuple<string, decimal>(_reader["numCuenta"].ToString(), Convert.ToDecimal(_reader["saldoFlotante"]));
                _cuentas.Add(_cuentaBase);
            }
            _reader.Close();
            foreach(Tuple<string, decimal> Cuenta in _cuentas)
            {
                _cuentaAhorro.setNumeroCuenta(Cuenta.Item1);
                CuentaAhorroDAO.modificarSaldo(_cuentaAhorro, Cuenta.Item2, pComando);
            }
        }
    }
}
