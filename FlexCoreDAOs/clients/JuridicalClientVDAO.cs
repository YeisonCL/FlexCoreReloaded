using System;
using System.Collections.Generic;
using FlexCoreDTOs.clients;
using System.Data.SqlClient;

namespace FlexCoreDAOs.clients
{
    public class JuridicalClientVDAO:GeneralDAO<JuridicalClientVDTO>
    {

        public static readonly string CLIENT_ID = "idCliente";
        public static readonly string CIF = "CIF";
        public static readonly string ACTIVE = "activo";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";

        private static object _syncLock = new object();
        private static JuridicalClientVDAO _instance;

        public static JuridicalClientVDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new JuridicalClientVDAO();
                    }
                }
            }
            return _instance;
        }

        private JuridicalClientVDAO() { }

        protected override string getFindCondition(JuridicalClientVDTO pClient)
        {
            string condition = "";
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CLIENT_ID));
            }
            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CIF));
            }
            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CIF));
            }
            return condition;
        }

        protected override void setFindParameters(SqlCommand pCommand, JuridicalClientVDTO pClient)
        {
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + CLIENT_ID, pClient.getClientID());
            }

            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            }
            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + ID_CARD, pClient.getIDCard());
            }
        }

        public override List<JuridicalClientVDTO> search(JuridicalClientVDTO pClient, SqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "CLIENTE_JURIDICO_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<JuridicalClientVDTO> list = new List<JuridicalClientVDTO>();

            while (reader.Read())
            {
                JuridicalClientVDTO client = new JuridicalClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(PersonDTO.JURIDIC_PERSON);
                list.Add(client);
            }
            reader.Close();
            return list;
        }

        public override List<JuridicalClientVDTO> getAll(SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "CLIENTE_JURIDICO_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            List<JuridicalClientVDTO> list = new List<JuridicalClientVDTO>();
            while (reader.Read())
            {
                JuridicalClientVDTO client = new JuridicalClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(PersonDTO.JURIDIC_PERSON);
                list.Add(client);
            }
            reader.Close();
            return list;
        }

        public override int getAllCount(SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("COUNT(*) as " + COUNT, "CLIENTE_JURIDICO_V");
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt32(reader[COUNT].ToString());
            reader.Close();
            return count;
        }

        public override int getSearchCount(SqlCommand pCommand, JuridicalClientVDTO pPerson)
        {
            pCommand.Parameters.Clear();
            string selection = "COUNT(*) as " + COUNT;
            string from = "CLIENTE_JURIDICO_V";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            SqlDataReader reader = pCommand.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt32(reader[COUNT].ToString());
            reader.Close();
            return count;
        }

        public override List<JuridicalClientVDTO> searchSelectParam(SqlCommand pCommand, string pParam, JuridicalClientVDTO pClient)
        {
            pCommand.Parameters.Clear();
            string selection = pParam;
            string from = "CLIENTE_JURIDICO_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<JuridicalClientVDTO> list = new List<JuridicalClientVDTO>();

            while (reader.Read())
            {
                JuridicalClientVDTO client = new JuridicalClientVDTO();
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
                else if (pParam == ID_CARD)
                {
                    client.setIDCard(reader[ID_CARD].ToString());
                }
                client.setPersonType(PersonDTO.JURIDIC_PERSON);
                list.Add(client);
            }
            reader.Close();
            return list;
        }
    }
}
