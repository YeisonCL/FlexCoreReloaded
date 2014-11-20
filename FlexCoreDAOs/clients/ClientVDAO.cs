using System;
using System.Collections.Generic;
using FlexCoreDTOs.clients;
using System.Data.SqlClient;

namespace FlexCoreDAOs.clients
{
    public class ClientVDAO:GeneralDAO<ClientVDTO>
    {
        public static readonly string JURIDICAL_PERSON = "Juridica";
        public static readonly string PHYSICAL_PERSON = "Fisica";

        public static readonly string CLIENT_ID = "idCliente";
        public static readonly string CIF = "CIF";
        public static readonly string ACTIVE = "activo";
        public static readonly string NAME = "nombre";
        public static readonly string FIRST_LSTNM = "primerApellido";
        public static readonly string SECOND_LSTNM = "segundoApellido";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";

        private static object _syncLock = new object();
        private static ClientVDAO _instance;

        public static ClientVDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientVDAO();
                    }
                }
            }
            return _instance;
        }

        private ClientVDAO() { }

        protected override string getFindCondition(ClientVDTO pClient)
        {
            string condition = "";
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", CLIENT_ID));
            }
            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} =@{0}", CIF));
            }
            if (pClient.getName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", NAME));
            }

            if (pClient.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", FIRST_LSTNM));
            }
            if (pClient.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", SECOND_LSTNM));
            }
            
            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", ID_CARD));
            }
            if (pClient.getPersonType() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", TYPE));
            }
            return condition;
        }

        protected override void setFindParameters(SqlCommand pCommand, ClientVDTO pClient)
        {
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + CLIENT_ID, pClient.getClientID());
            }
            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            }
            if (pClient.getName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + NAME, pClient.getName());
            }

            if (pClient.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + FIRST_LSTNM, pClient.getName());
            }
            if (pClient.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + SECOND_LSTNM, pClient.getName());
            }

            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + ID_CARD, pClient.getIDCard());
            }
            if (pClient.getPersonType() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + TYPE, pClient.getPersonType());
            }
        }

        public override List<ClientVDTO> search(ClientVDTO pClient, SqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "CLIENTE_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<ClientVDTO> list = new List<ClientVDTO>();

            while (reader.Read())
            {
                ClientVDTO client = new ClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setFirstLastName(reader[FIRST_LSTNM].ToString());
                client.setSecondLastName(reader[SECOND_LSTNM].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(reader[TYPE].ToString());
                list.Add(client);
            }
            reader.Close();
            return list;
        }

        public override List<ClientVDTO> getAll(SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "CLIENTE_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            List<ClientVDTO> list = new List<ClientVDTO>();
            while (reader.Read())
            {
                ClientVDTO client = new ClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setFirstLastName(reader[FIRST_LSTNM].ToString());
                client.setSecondLastName(reader[SECOND_LSTNM].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(reader[TYPE].ToString());
                list.Add(client);
            }
            reader.Close();
            return list;
        }

        public override int getAllCount(SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("COUNT(*) as " + COUNT, "CLIENTE_V");
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt32(reader[COUNT].ToString());
            reader.Close();
            return count;
        }

        public override int getSearchCount(SqlCommand pCommand, ClientVDTO pClient)
        {
            pCommand.Parameters.Clear();
            string selection = "COUNT(*) as " + COUNT;
            string from = "CLIENTE_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            SqlDataReader reader = pCommand.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt32(reader[COUNT].ToString());
            reader.Close();
            return count;
        }

        public override List<ClientVDTO> searchSelectParam(SqlCommand pCommand, string pParam, ClientVDTO pClient)
        {
            pCommand.Parameters.Clear();
            string selection = pParam;
            string from = "CLIENTE_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<ClientVDTO> list = new List<ClientVDTO>();

            while (reader.Read())
            {
                ClientVDTO client = new ClientVDTO();
                if (pParam == CLIENT_ID)
                {
                    client.setClientID((int)reader[CLIENT_ID]);
                }
                else if (pParam == CIF)
                {
                    client.setCIF(reader[CIF].ToString());
                }
                else if (pParam == ACTIVE)
                {
                    client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                }
                else if (pParam == NAME)
                {
                    client.setName(reader[NAME].ToString());
                }
                else if (pParam == FIRST_LSTNM)
                {
                    client.setFirstLastName(reader[FIRST_LSTNM].ToString());
                }
                else if (pParam == SECOND_LSTNM)
                {
                    client.setSecondLastName(reader[SECOND_LSTNM].ToString());
                }
                else if (pParam == ID_CARD)
                {
                    client.setIDCard(reader[ID_CARD].ToString());
                }
                else if (pParam == TYPE)
                {
                    client.setPersonType(reader[TYPE].ToString());
                }
                list.Add(client);
            }
            reader.Close();
            return list;
        }
    }
}
