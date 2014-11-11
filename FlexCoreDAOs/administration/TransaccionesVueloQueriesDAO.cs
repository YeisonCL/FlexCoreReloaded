﻿using System;
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
    public class TransaccionesVueloQueriesDAO
    {

        public void insertTransaccionVuelo(String descripcion, DateTime fechaHora, int idCuenta, int tipoTransaccion)
        {
            String query = "INSERT INTO TRANSACCIONES_VUELO (descripcion, fechaHora, idCuenta, tipoTransaccion)" +
                " VALUES (@descripcion, @fechaHora, @idCuenta, @tipoTransaccion);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@tipoTransaccion", tipoTransaccion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<TransaccionesVueloDTO> getTransaccionesEnVuelo()
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"], 
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()), 
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public List<TransaccionesVueloDTO> getTransaccionesEnVueloTransaccion(int idTransaccion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE idTransaccion = @idTransaccion;";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public List<TransaccionesVueloDTO> getTransaccionesEnVueloDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public int getIdTransaccionVuelo(String descripcion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
            int transaccion_vuelo = -1;
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                transaccion_vuelo = (int)reader["idTransaccion"];
            }
            SQLServerManager.closeConnection(connD);
            return transaccion_vuelo;
        }

        public void updateTransaccionVuelo(int idTransaccion, String descripcion)
        {
            String query = "UPDATE TRANSACCIONES_VUELO SET descripcion = @descripcion WHERE idTransaccion = @idTransaccion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteTransaccionVuelo(int idTransaccion)
        {
            String query = "DELETE FROM TRANSACCIONES_VUELO WHERE idTransaccion = @idTransaccion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteTransaccionVuelo(String descripcion)
        {
            String query = "DELETE FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
