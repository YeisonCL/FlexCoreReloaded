using ConexionSQLServer.SQLServerConnectionManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.cuentas.Generales
{
    internal static class Conexiones
    {
        public static SqlCommand obtenerConexionSQL()
        {
            SqlConnection _conexionSQLBase = SQLServerManager.newConnection();
            SqlCommand _comandoSQL = _conexionSQLBase.CreateCommand();
            SqlTransaction _transaccion = _conexionSQLBase.BeginTransaction();
            _comandoSQL.Connection = _conexionSQLBase;
            _comandoSQL.Transaction = _transaccion;
            return _comandoSQL;
        }
    }
}
