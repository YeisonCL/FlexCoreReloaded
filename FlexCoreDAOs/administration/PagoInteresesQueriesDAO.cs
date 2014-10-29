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
    public class PagoInteresesQueriesDAO
    {
        public void insertPagoIntereses(Decimal monto, int idCuenta, int idCierre)
        {
            String query = "INSERT INTO PAGO_INTERESES (monto, idCuenta, idCierre)" +
                " VALUES (@monto, @idCuenta, @idCierre);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@monto", monto);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<PagoInteresesDTO> getPagoIntereses()
        {
            String query = "SELECT * FROM PAGO_INTERESES";
            List<PagoInteresesDTO> pagoIntereses = new List<PagoInteresesDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PagoInteresesDTO tmp = new PagoInteresesDTO((int)reader["idPago"],
                    (Decimal)reader["monto"], (int)reader["idCuenta"], (int)reader["idCierre"]);
                pagoIntereses.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return pagoIntereses;
        }

        public List<PagoInteresesDTO> getPagoIntereses(int idPago)
        {
            String query = "SELECT * FROM PAGO_INTERESES WHERE idPago = @idPago";
            List<PagoInteresesDTO> pagoIntereses = new List<PagoInteresesDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PagoInteresesDTO tmp = new PagoInteresesDTO((int)reader["idPago"],
                    (Decimal)reader["monto"], (int)reader["idCuenta"], (int)reader["idCierre"]);
                pagoIntereses.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return pagoIntereses;
        }

        public void updatePagoIntereses(int idPago, Decimal monto, int idCuenta, int idCierre)
        {
            String query = "UPDATE PAGO_INTERESES SET monto = @monto, idCuenta = @idCuenta, idCierre = @idCierre"+
                " WHERE idPago = @idPago;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            command.Parameters.AddWithValue("@monto", monto);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deletePagoIntereses(int idPago)
        {
            String query = "DELETE FROM PAGO_INTERESES WHERE idPago = @idPago;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
