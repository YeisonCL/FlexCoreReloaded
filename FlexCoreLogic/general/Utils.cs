using ConexionSQLServer.SQLServerConnectionManager;
using System.Data.SqlClient;

namespace FlexCoreLogic.general
{
    public static class Utils
    {
        public static int getMaxPage(float pCount, int pShowCount)
        {
            float div = pCount / pShowCount;
            if (div % 1 > 0)
            {
                div++;
            }
            return (int)div;
        }

        public static void iniciarBaseDeDatos()
        {
            SqlConnection conn = SQLServerManager.newConnection();
            SqlCommand query = new SqlCommand("IniciarBaseDeDatos", conn);
            query.ExecuteNonQuery();
            conn.Close();
        }
    }
}
