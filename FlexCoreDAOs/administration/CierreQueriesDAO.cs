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
    public class CierreQueriesDAO
    {
        public void insertCierre(DateTime fechaHora, bool estado)
        {
            String query = "INSERT INTO CIERRE (fechaHora, estado)" +
                " VALUES (@fechaHora, @estado);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@estado", estado);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<CierreDTO> getCierre()
        {
            String query = "SELECT * FROM CIERRE";
            List<CierreDTO> cierre = new List<CierreDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CierreDTO tmp = new CierreDTO((int)reader["idCierre"],
                    DateTime.Parse(reader["fechaHora"].ToString()), (bool)reader["estado"]);
                cierre.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return cierre;
        }

        public List<CierreDTO> getCierre(int idCierre)
        {
            String query = "SELECT * FROM CIERRE WHERE idCierre = @idCierre";
            List<CierreDTO> cierre = new List<CierreDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idCierre", idCierre);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CierreDTO tmp = new CierreDTO((int)reader["idCierre"],
                    DateTime.Parse(reader["fechaHora"].ToString()), (bool)reader["estado"]);
                cierre.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return cierre;
        }

        public void updateCierre(int idCierre, DateTime fechaHora, bool estado)
        {
            String query = "UPDATE CIERRE SET fechaHora = @fechaHora, estado = @estado WHERE idCierre = @idCierre;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteCierre(int idCierre)
        {
            String query = "DELETE FROM CIERRE WHERE idCierre = @idCierre;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
