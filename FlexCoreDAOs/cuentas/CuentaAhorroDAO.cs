using FlexCoreDTOs.cuentas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDAOs.cuentas
{
    public static class CuentaAhorroDAO
    {
        public static void agregarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            String _query = "INSERT INTO CUENTA_AHORRO(NUMCUENTA, DESCRIPCION, SALDO, ACTIVA, IDCLIENTE, TIPOMONEDA) VALUES(@numCuenta, @descripcion, @saldo, @activa, @idCliente, @tipoMoneda);";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            pComando.Parameters.AddWithValue("@descripcion", pCuentaAhorro.getDescripcion());
            pComando.Parameters.AddWithValue("@saldo", pCuentaAhorro.getSaldo());
            pComando.Parameters.AddWithValue("@activa", TransformacionesDAO.boolToInt(pCuentaAhorro.getEstado()));
            pComando.Parameters.AddWithValue("@idCliente", pCuentaAhorro.getCliente().getClientID());
            pComando.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorro.getTipoMoneda());
            pComando.ExecuteNonQuery();
        }
        

        public static void eliminarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            int _id = obtenerCuentaAhorroID(pCuentaAhorro, pComando);
            String _query = "DELETE FROM CUENTA_AHORRO WHERE idCuenta = @idCuenta";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static void modificarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            int _id = obtenerCuentaAhorroID(pCuentaAhorro, pComando);
            String _query = "UPDATE CUENTA_AHORRO SET DESCRIPCION = @descripcion, TIPOMONEDA = @tipoMoneda, ACTIVA = @estado WHERE IDCUENTA = @idCuenta;";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@descripcion", pCuentaAhorro.getDescripcion());
            pComando.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorro.getTipoMoneda());
            pComando.Parameters.AddWithValue("@estado", pCuentaAhorro.getEstado());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static void modificarSaldo(CuentaAhorroDTO pCuentaAhorro, decimal pSaldo, SqlCommand pComando)
        {
            int _id = obtenerCuentaAhorroID(pCuentaAhorro, pComando);
            String _query = "UPDATE CUENTA_AHORRO SET SALDO = @saldo WHERE IDCUENTA = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@saldo", pSaldo);
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static bool existeCuenta(string pNumeroCuenta, SqlCommand pComando)
        {
            String _query = "SELECT numCuenta FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            SqlDataReader _reader = pComando.ExecuteReader();
            if (_reader.Read())
            {
                _reader.Close();
                return true;
            }
            else
            {
                _reader.Close();
                return false;
            }
        }

        public static int obtenerCuentaAhorroID(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            int _idCuenta = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            SqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                _idCuenta = Convert.ToInt32(_reader["idCuenta"]);
            }
            _reader.Close();
            return _idCuenta;
        }

        public static int obtenerCuentaAhorroIdCliente(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            int _idCuenta = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            SqlDataReader _reader = pComando.ExecuteReader();
            if (_reader.Read())
            {
                _idCuenta = Convert.ToInt32(_reader["idCliente"]);
            }
            _reader.Close();
            return _idCuenta;
        }

        public static bool comprobarCuentasEnCero(int pIdCliente, SqlCommand pComando)
        {
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE IDCLIENTE = @idCliente";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCliente", pIdCliente);
            SqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int obtenerCuentaAhorroMoneda(CuentaAhorroDTO pCuentaAhorro, SqlCommand pComando)
        {
            int _moneda = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            SqlDataReader _reader = pComando.ExecuteReader();
            if (_reader.Read())
            {
                _moneda = Convert.ToInt32(_reader["tipoMoneda"]);
            }
            _reader.Close();
            return _moneda;
        }

        public static string obtenerNumeroCuenta(int pIdCuenta, SqlCommand pComando)
        {
            string _numeroCuenta = "";
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE IDCUENTA = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            SqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                _numeroCuenta = _reader["numCuenta"].ToString();
            }
            _reader.Close();
            return _numeroCuenta;
        }
    }
}
