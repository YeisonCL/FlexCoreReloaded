using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionSQLServer.PrincipalSQLServerConnection;

namespace ConexionSQLServer.SQLServerConnectionManager
{
    public static class SQLServerManager
    {
        public static SqlConnection nuevaConexion()
        {
            SQLServerConnectionDAO _SQLServerConnection = new SQLServerConnectionDAO();
            return _SQLServerConnection.startConnection();
        }

        public static void cerrarConexion(SqlConnection pConnection)
        {
            SQLServerConnectionDAO.closeConnection(pConnection);
        }
    }
}
