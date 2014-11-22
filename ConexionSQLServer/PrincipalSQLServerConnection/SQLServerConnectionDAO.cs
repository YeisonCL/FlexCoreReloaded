using ConexionSQLServer.Xml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConexionSQLServer.PrincipalSQLServerConnection
{
    internal class SQLServerConnectionDAO
    {
        private string _documentDirection = "C:/Configuracion.xml";
        private String _connectionString;
        private String _server;
        private String _port;
        private String _database;
        private String _user;
        private String _password;

        private SqlConnection _SQLServerConnection;

        public SQLServerConnectionDAO()
        {
            List<string> _valores = XmlAcess.extraerDatos(_documentDirection);
            setServer(_valores[0]);
            setPort(_valores[1]);
            setDatabase(_valores[2]);
            setUser(_valores[3]);
            setPassword(_valores[4]);
            createConnectionString();
        }

        private void createConnectionString()
        {
            _connectionString = "Data Source=" + _server + "," + _port + ";Network Library=DBMSSOCN" + ";Initial Catalog=" + _database + ";User ID=" + _user + ";Password=" + _password + ";Max Pool Size=3072;";
        }

        public SqlConnection startConnection()
        {
            try
            {
                _SQLServerConnection = new SqlConnection(_connectionString);
                _SQLServerConnection.Open();
                return _SQLServerConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void closeConnection(SqlConnection pConnection)
        {
            try
            {
                pConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void setServer(String pServer)
        {
            _server = pServer;
        }
        public void setPort(String pPort)
        {
            _port = pPort;
        }
        public void setDatabase(String pDatabase)
        {
            _database = pDatabase;
        }
        public void setUser(String pUser)
        {
            _user = pUser;
        }
        public void setPassword(String pPassword)
        {
            _password = pPassword;
        }
        public void setConnectionString(String pConnectionString)
        {
            _connectionString = pConnectionString;
        }
        public String getServer()
        {
            return _server;
        }
        public String getPort()
        {
            return _port;
        }
        public String getUser()
        {
            return _user;
        }
        public String getPassword()
        {
            return _password;
        }
        public String getConnectionString()
        {
            return _connectionString;
        }
        public SqlConnection getSQLServerConnection()
        {
            return _SQLServerConnection;
        }
    }
}
