﻿using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDAOs.cuentas
{
    public class CuentaBeneficiariosDAO
    {
        public static void agregarBeneficiarios(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            foreach (PhysicalPersonDTO beneficiario in pCuentaAhorroVista.getListaBeneficiarios())
            {
                String _query = "INSERT INTO CUENTA_BENEFICIARIOS(IDCUENTA, IDBENEFICIARIO) VALUES(@idCuenta, @idBeneficiario);";
                pComando.Parameters.Clear();
                pComando.CommandText = _query;
                pComando.Parameters.AddWithValue("@idCuenta", _id);
                pComando.Parameters.AddWithValue("@idBeneficiario", beneficiario.getPersonID());
                pComando.ExecuteNonQuery();
            }
        }

        public static void eliminarBeneficiario(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            String _query = "DELETE FROM CUENTA_BENEFICIARIOS WHERE idCuenta = @idCuenta";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static List<PhysicalPersonDTO> obtenerListaBeneficiarios(CuentaAhorroVistaDTO pCuentaAhorroVista, SqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            List<PhysicalPersonDTO> _listaBeneficiarios = new List<PhysicalPersonDTO>();
            String _query = "SELECT * FROM CUENTA_BENEFICIARIOS WHERE IDCUENTA = @idCuenta;";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            SqlDataReader _reader = pComando.ExecuteReader();
            if (_reader.Read())
            {
                PhysicalPersonDTO _beneficiario = new PhysicalPersonDTO(Convert.ToInt32(_reader["idBeneficiario"]), "", "", "", "");
                _listaBeneficiarios.Add(_beneficiario);
            }
            _reader.Close();
            return _listaBeneficiarios;
        }
    }
}