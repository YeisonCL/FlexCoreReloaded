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
    public class ErrorQueriesDAO
    {
        public void insertError(String metodo, int linea, DateTime fechaHora, 
            String descripcion)
        {
            String query = "INSERT INTO ERROR (metodo, linea, fechaHora, descripcion)" +
                " VALUES (@metodo, @linea, @fechaHora, @descripcion);";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@metodo", metodo);
            command.Parameters.AddWithValue("@linea", linea);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public List<ErrorDTO> getError()
        {
            String query = "SELECT * FROM ERROR";
            List<ErrorDTO> error = new List<ErrorDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ErrorDTO tmp = new ErrorDTO((int)reader["idError"],
                    reader["metodo"].ToString(),  (int)reader["linea"], (DateTime)reader["fechaHora"], 
                    reader["idCierre"].ToString());
                error.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return error;
        }

        public List<ErrorDTO> getError(int idError)
        {
            String query = "SELECT * FROM ERROR WHERE idError = @idError";
            List<ErrorDTO> error = new List<ErrorDTO>();
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ErrorDTO tmp = new ErrorDTO((int)reader["idError"],
                    reader["metodo"].ToString(), (int)reader["linea"], (DateTime)reader["fechaHora"],
                    reader["idCierre"].ToString());
                error.Add(tmp);
            }
            SQLServerManager.closeConnection(connD);
            return error;
        }

        public void updateError(int idError, String metodo, int linea, DateTime fechaHora,
            String descripcion)
        {
            String query = "UPDATE ERROR SET metodo = @metodo, linea = @linea, fechaHora = @fechaHora," +
                "descripcion = @descripcion WHERE idError = @idError;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            command.Parameters.AddWithValue("@metodo", metodo);
            command.Parameters.AddWithValue("@linea", linea);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }

        public void deleteError(int idError)
        {
            String query = "DELETE FROM ERROR WHERE idError = @idError;";
            SqlConnection connD = SQLServerManager.newConnection();
            SqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            command.ExecuteNonQuery();
            SQLServerManager.closeConnection(connD);
        }
    }
}
