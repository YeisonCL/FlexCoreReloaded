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
    public class HistoricoTransaccionalQueriesDAO
    {
        public void insertHistoricoTransaccional(String pDescripcion, DateTime pFechaHoraEntrada, DateTime pFechaHoraSalida, string pEstado, int pVersionAplicacion,
            int idCuenta, int tipoTransaccion)
        {
            String query = "INSERT INTO TRANSACCIONES_VUELO (descripcion, fechaHoraEntrada, fechaHoraSalida, estado, versionAplicacion, idCuenta, tipoTransaccion)" +
                " VALUES (@descripcion, @fechaHoraEntrada, @fechaHoraSalida, @estado, @versionAplicacion, @idCuenta, @tipoTransaccion);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", pDescripcion);
            command.Parameters.AddWithValue("@fechaHoraEntrada", pFechaHoraEntrada);
            command.Parameters.AddWithValue("@fechaHoraSalida", pFechaHoraSalida);
            command.Parameters.AddWithValue("@estado", pEstado);
            command.Parameters.AddWithValue("@versionAplicacion", pVersionAplicacion);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@tipoTransaccion", tipoTransaccion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<HistoricoTransaccionalDTO> getHistoricoTransaccional()
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"], reader["descripcion"].ToString(),
                    DateTime.Parse(reader["fechaHoraEntrada"].ToString()), DateTime.Parse(reader["fechaHoraSalida"].ToString()),
                    reader["estado"].ToString(), (int)reader["versionAplicacion"], (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public List<HistoricoTransaccionalDTO> getHistoricoTransaccional(int idTransaccion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE idTransaccion = @idTransaccion;";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"], reader["descripcion"].ToString(),
                    DateTime.Parse(reader["fechaHoraEntrada"].ToString()), DateTime.Parse(reader["fechaHoraSalida"].ToString()),
                    reader["estado"].ToString(), (int)reader["versionAplicacion"], (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public List<HistoricoTransaccionalDTO> getHistoricoTransaccional(String descripcion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"], reader["descripcion"].ToString(),
                    DateTime.Parse(reader["fechaHoraEntrada"].ToString()), DateTime.Parse(reader["fechaHoraSalida"].ToString()),
                    reader["estado"].ToString(), (int)reader["versionAplicacion"], (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return transacciones_vuelo;
        }

        public int getIdHistoricoTransaccional(String descripcion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
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

        public void updateDescripcionHistoricoTransaccional(int idTransaccion, String descripcion)
        {
            String query = "UPDATE HISTORICO_TRANSACCIONAL SET descripcion = @descripcion WHERE idTransaccion = @idTransaccion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteHistoricoTransaccional(int idTransaccion)
        {
            String query = "DELETE FROM HISTORICO_TRANSACCIONAL WHERE idTransaccion = @idTransaccion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteHistoricoTransaccional(String descripcion)
        {
            String query = "DELETE FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

    }
}