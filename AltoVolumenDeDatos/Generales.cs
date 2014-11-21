using ConexionSQLServer.SQLServerConnectionManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace AltoVolumenDeDatos
{
    public static class Generales
    {
        /*
         * Metodo implementado para vaciar la base de datos
         */
        public static void vaciarBaseDeDatos()
        {
            SqlConnection conn = SQLServerManager.newConnection();
            SqlCommand query = new SqlCommand("LimpiarBase", conn);
            query.ExecuteNonQuery();
            conn.Close();
        }

        public static void prepararCuentaAhorroVista()
        {
            SqlConnection conn = SQLServerManager.newConnection();
            SqlCommand query = new SqlCommand("PrepararCuentaAhorroVista", conn);
            query.ExecuteNonQuery();
            conn.Close();
        }

        public static string obtenerPrimerNumeroCuenta()
        {
            SqlConnection conexion = SQLServerManager.newConnection();
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE IDCUENTA = 1";
            SqlCommand comando = new SqlCommand(_query, conexion);
            SqlDataReader _reader = comando.ExecuteReader();
            if (_reader.Read())
            {
                return _reader["numCuenta"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static List<string> extraerDatos(string pDireccion)
        {
            var _datos = new List<string>();
            XmlDocument _documentoXML = new XmlDocument();
            _documentoXML.Load(pDireccion);
            XmlNodeList _configuracion = _documentoXML.SelectNodes("/Configuracion")[0].ChildNodes;
            for (var index = 0; index < _configuracion.Count; index++)
            {
                _datos.Add(_configuracion[index].InnerXml);
            }
            return _datos;
        }
    }
}
