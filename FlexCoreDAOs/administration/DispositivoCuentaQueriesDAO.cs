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
    public class DispositivoCuentaQueriesDAO
    {

        public void insertDispositivoCuenta(String idDispositivo, bool activo, int idCuenta)
        {
            String query = "INSERT INTO DISPOSITIVO_CUENTA (idDispositivo, activo, idCuenta)" +
                " VALUES (@idDispositivo, @activo, @idCuenta);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivo", idDispositivo);
            command.Parameters.AddWithValue("@activo", activo);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<DispositivoCuentaDTO> getDispositivoCuenta()
        {
            String query = "SELECT * FROM DISPOSITIVO_CUENTA";
            List<DispositivoCuentaDTO> dispositivoCuenta = new List<DispositivoCuentaDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DispositivoCuentaDTO tmp = new DispositivoCuentaDTO((int)reader["idDispositivoCuenta"],
                    reader["idDispositivo"].ToString(), (bool)reader["activo"], (int)reader["idCuenta"]);
                dispositivoCuenta.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return dispositivoCuenta;
        }

        public List<DispositivoCuentaDTO> getDispositivoCuenta(int idDispositivoCuenta)
        {
            String query = "SELECT * FROM DISPOSITIVO_CUENTA WHERE idDispositivoCuenta = @idDispositivoCuenta";
            List<DispositivoCuentaDTO> dispositivoCuenta = new List<DispositivoCuentaDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DispositivoCuentaDTO tmp = new DispositivoCuentaDTO((int)reader["idDispositivoCuenta"],
                    reader["idDispositivo"].ToString(), (bool)reader["activo"], (int)reader["idCuenta"]);
                dispositivoCuenta.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return dispositivoCuenta;
        }

        public List<int> checkDispositivoCuenta(String idDispositivo, int idCuenta)
        {
            List<int> respuesta = new List<int>();
            if (existDispositivo(idDispositivo) == ConstantesDAO.DISPOSITIVOEXISTE)
            {
                respuesta.Add(ConstantesDAO.DISPOSITIVOEXISTE);
                String query = "SELECT activo FROM DISPOSITIVO_CUENTA WHERE idDispositivo = @idDispositivo" +
                    " AND idCuenta = @idCuenta";
                SqlConnection connD = SQLServerManager.newConnection();
                SqlCommand command = connD.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@idDispositivo", idDispositivo);
                command.Parameters.AddWithValue("@idCuenta", idCuenta);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    respuesta.Add(ConstantesDAO.DISPOSITIVOCUENTAENLAZADOS);
                    if ((bool)reader["activo"]) { respuesta.Add(ConstantesDAO.DISPOSITIVOACTIVO); }
                    else { respuesta.Add(ConstantesDAO.DISPOSITIVOINACTIVO); }
                }
                else { respuesta.Add(ConstantesDAO.DISPOSITIVOCUENTANOENLAZADOS); }
                SQLServerManager.closeConnection(connD);
            }
            else { respuesta.Add(ConstantesDAO.DISPOSITIVONOEXISTE); }
            return respuesta;
        }

        private int existDispositivo(String idDispositivo)
        {
            int resp = ConstantesDAO.DISPOSITIVONOEXISTE;
            String query = "SELECT estado FROM DISPOSITIVO_CUENTA WHERE idDispositivo = @idDispositivo";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivo", idDispositivo);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                resp = ConstantesDAO.DISPOSITIVOEXISTE;
            }
            SQLServerManager.closeConnection(connD);
            return resp;
        }

        public void updateDispositivoCuenta(int idDispositivoCuenta, String idDispositivo, bool activo, 
            int idCuenta)
        {
            String query = "UPDATE DISPOSITIVO_CUENTA SET idDispositivo = @idDispositivo, activo = @activo," +
                " idCuenta = @idCuenta WHERE idDispositivoCuenta = @idDispositivoCuenta;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            command.Parameters.AddWithValue("@idDispositivov", idDispositivo);
            command.Parameters.AddWithValue("@activo", activo);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteDispositivoCuenta(int idDispositivoCuenta)
        {
            String query = "DELETE FROM DISPOSITIVO_CUENTA WHERE idDispositivoCuenta = @idDispositivoCuenta;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
