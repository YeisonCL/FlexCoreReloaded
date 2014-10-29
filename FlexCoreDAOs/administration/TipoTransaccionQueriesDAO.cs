using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlexCoreDTOs.administration;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;

namespace FlexCoreDAOs.administration
{
    public class TipoTransaccionQueriesDAO
    {

        public void insertDescripcion(String desc)
        {
            String query = "INSERT INTO TIPO_TRANSACCION (descripcion) VALUES (@descripcion);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", desc);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<TipoTransaccionDTO> getDescripcion()
        {
            String query = "SELECT * FROM TIPO_TRANSACCION";
            List<TipoTransaccionDTO> tipo_transaccion = new List<TipoTransaccionDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TipoTransaccionDTO tmp = new TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return tipo_transaccion;
        }

        public List<TipoTransaccionDTO> getDescripcion(int idTipo)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE idTipo = @idTipo";
            List<TipoTransaccionDTO> tipo_transaccion = new List<TipoTransaccionDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TipoTransaccionDTO tmp = new TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return tipo_transaccion;
        }

        public List<TipoTransaccionDTO> getDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE descripcion = @descripcion";
            List<TipoTransaccionDTO> tipo_transaccion = new List<TipoTransaccionDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TipoTransaccionDTO tmp = new TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return tipo_transaccion;
        }

        public int getIdDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE descripcion = @descripcion";
            int idTipo = 0;
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                idTipo = (int)reader["idTipo"];
            }
            SQLServerManager.closeConnection(connD);
            return idTipo;
        }

        public void updateDescripcion(int idTipo, String descripcion)
        {
            String query = "UPDATE TIPO_TRANSACCION SET descripcion = @descripcion WHERE idTipo = @idTipo;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteDescription(int idTipo)
        {
            String query = "DELETE FROM TIPO_TRANSACCION WHERE idTipo = @idTipo;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteDescription(String descripcion)
        {
            String query = "DELETE FROM TIPO_TRANSACCION WHERE descripcion = @descripcion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
