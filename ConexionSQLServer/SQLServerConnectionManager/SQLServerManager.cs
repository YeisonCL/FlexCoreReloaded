using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionSQLServer.PrincipalSQLServerConnection;
using System.Threading;
using System.Windows.Forms;

namespace ConexionSQLServer.SQLServerConnectionManager
{
    public static class SQLServerManager
    {
        public static List<SqlConnection> _listaDeConexiones = new List<SqlConnection>();
        public static int numeroConexiones = 150;
        private static Object bloqueo = new Object();

        public static void iniciarOReiniciarListaDeConexiones()
        {
            _listaDeConexiones.Clear();
            for(int i = 0; i < numeroConexiones; i++)
            {
                SQLServerConnectionDAO _SQLServerConnection = new SQLServerConnectionDAO();
                SqlConnection _nuevaConexion = _SQLServerConnection.startConnection();
                _listaDeConexiones.Add(_nuevaConexion);
            }

        }

        public static SqlConnection newConnection()
        {
            while (_listaDeConexiones.Count == 0)
            {
                Thread.Sleep(15);
            }
            lock (bloqueo)
            {
                SqlConnection _conexionTemporal;
                if (_listaDeConexiones.Count > 0)
                {
                    _conexionTemporal = _listaDeConexiones[0];
                    _listaDeConexiones.Remove(_conexionTemporal);
                    return _conexionTemporal;
                }
            }
            return newConnection();
        }

        public static SqlConnection newConnectionHour()
        {
            SQLServerConnectionDAO _SQLServerConnection = new SQLServerConnectionDAO();
            SqlConnection _nuevaConexion = _SQLServerConnection.startConnection();
            return _nuevaConexion;
        }

        public static void closeConnectionHour(SqlConnection pConnection)
        {
            SQLServerConnectionDAO.closeConnection(pConnection);
        }

        public static void closeConnection(SqlConnection pConnection)
        {
            lock (bloqueo)
            {
                SQLServerConnectionDAO.closeConnection(pConnection);
                SQLServerConnectionDAO _SQLServerConnection = new SQLServerConnectionDAO();
                SqlConnection _nuevaConexion = _SQLServerConnection.startConnection();
                _listaDeConexiones.Add(_nuevaConexion);
            }
        }
    }
}
