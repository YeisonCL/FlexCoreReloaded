using ConexionSQLServer.SQLServerConnectionManager;
using System.Data.SqlClient;

namespace FlexCoreLogic.cuentas.Generales
{
    internal static class Conexiones
    {
        public static SqlCommand obtenerConexionSQL()
        {
            try
            {
                SqlConnection _conexionSQLBase = SQLServerManager.newConnection();
                SqlCommand _comandoSQL = _conexionSQLBase.CreateCommand();
                SqlTransaction _transaccion = _conexionSQLBase.BeginTransaction();
                _comandoSQL.Connection = _conexionSQLBase;
                _comandoSQL.Transaction = _transaccion;
                return _comandoSQL;
            }
            catch
            {
                return obtenerConexionSQL();
            }
        }
    }
}
