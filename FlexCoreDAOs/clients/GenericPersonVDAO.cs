using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using System.Windows.Forms;

namespace FlexCoreDAOs.clients
{
    public class GenericPersonVDAO:GeneralDAO<GenericPersonDTO>
    {

        public static readonly string PERSON_ID = "idPersona";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";
        public static readonly string FIRST_LSTNM = "primerApellido";
        public static readonly string SECOND_LSTNM = "segundoApellido";
        public static readonly string PHOTO = "fotografia";

        private static object _syncLock = new object();
        private static GenericPersonVDAO _instance;

        public static GenericPersonVDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new GenericPersonVDAO();
                    }
                }
            }
            return _instance;
        }

        private GenericPersonVDAO() { }

        public override void insert(GenericPersonDTO pPerson)
        {
            SqlCommand command = getCommand();
            insert(pPerson, command);
            SQLServerManager.closeConnection(command.Connection);
        }

        public override void delete(GenericPersonDTO pPerson)
        {
            SqlCommand command = getCommand();
            delete(pPerson, command);
            SQLServerManager.closeConnection(command.Connection);
        }

        public override void update(GenericPersonDTO pNewDTO, GenericPersonDTO pPastDTO)
        {
            SqlCommand command = getCommand();
            update(pNewDTO, pPastDTO, command);
            SQLServerManager.closeConnection(command.Connection);
        }

        public override List<GenericPersonDTO> search(GenericPersonDTO pPerson, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            SqlCommand command = getCommand();
            List<GenericPersonDTO> result = search(pPerson, command, pPageNumber, pShowCount, pOrderBy);
            SQLServerManager.closeConnection(command.Connection);
            return result;
        }

        public override List<GenericPersonDTO> search(GenericPersonDTO pPerson)
        {
            SqlCommand command = getCommand();
            List<GenericPersonDTO> result = search(pPerson, command);
            SQLServerManager.closeConnection(command.Connection);
            return result;
        }

        public override List<GenericPersonDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            SqlCommand command = getCommand();
            List<GenericPersonDTO> result = getAll(command, pPageNumber, pShowCount, pOrderBy);
            SQLServerManager.closeConnection(command.Connection);
            return result;
        }

        protected override string getFindCondition(GenericPersonDTO pPerson)
        {
            string condition = "";
            if (pPerson.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", PERSON_ID));
            }
            if (pPerson.getName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", NAME));
            }
            if (pPerson.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", ID_CARD));
            }
            if (pPerson.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", FIRST_LSTNM));
            }
            if (pPerson.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", SECOND_LSTNM));
            }
            return condition;
        }

        protected override void setFindParameters(SqlCommand pCommand, GenericPersonDTO pPerson)
        {
            if (pPerson.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPerson.getPersonID());
            }

            if (pPerson.getName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + NAME, pPerson.getName());
            }
            if (pPerson.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + ID_CARD, pPerson.getIDCard());
            }
            if (pPerson.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + FIRST_LSTNM, pPerson.getFirstLastName());
            }
            if (pPerson.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + SECOND_LSTNM, pPerson.getSecondLastName());
            }
        }

        public override List<GenericPersonDTO> search(GenericPersonDTO pPerson, SqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "TODAS_LAS_PERSONAS_V";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<GenericPersonDTO> list = new List<GenericPersonDTO>();

            while (reader.Read())
            {
                GenericPersonDTO person = new GenericPersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());               
                person.setFirstLastName(reader[FIRST_LSTNM].ToString());
                person.setSecondLastName(reader[SECOND_LSTNM].ToString());
                person.setPersonType(reader[TYPE].ToString());
                byte[] photo = reader[PHOTO].GetType() != typeof(System.DBNull) ? (byte[])reader[PHOTO] : null;
                person.setPhotoBytes(photo);
                list.Add(person);
            }
            reader.Close();
            return list;
        }

        public override List<GenericPersonDTO> getAll(SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "TODAS_LAS_PERSONAS_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            List<GenericPersonDTO> list = new List<GenericPersonDTO>();
            while (reader.Read())
            {
                GenericPersonDTO person = new GenericPersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setFirstLastName(reader[FIRST_LSTNM].ToString());
                person.setSecondLastName(reader[SECOND_LSTNM].ToString());
                person.setPersonType(reader[TYPE].ToString());
                byte[] photo = reader[PHOTO].GetType() != typeof(System.DBNull)?(byte[])reader[PHOTO]:null;
                person.setPhotoBytes(photo);
                list.Add(person);
            }
            reader.Close();
            return list;
        }

        public override int getAllCount(SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("COUNT(*) as " + COUNT, "TODAS_LAS_PERSONAS_V");
            pCommand.CommandText = query;
            SqlDataReader reader = pCommand.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt32(reader[COUNT].ToString());
            reader.Close();
            return count;
        }

        public override int getSearchCount(SqlCommand pCommand, GenericPersonDTO pPerson)
        {
            string selection = "COUNT(*) as " + COUNT;
            string from = "TODAS_LAS_PERSONAS_V";
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

        public override List<GenericPersonDTO> searchSelectParam(SqlCommand pCommand, string pParam, GenericPersonDTO pPerson)
        {
            pCommand.Parameters.Clear();
            string selection = pParam;
            string from = "TODAS_LAS_PERSONAS_V";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            SqlDataReader reader = pCommand.ExecuteReader();
            List<GenericPersonDTO> list = new List<GenericPersonDTO>();

            while (reader.Read())
            {
                GenericPersonDTO person = new GenericPersonDTO();
                if (pParam == PERSON_ID)
                {
                    person.setPersonID((int)reader[PERSON_ID]);
                }
                else if (pParam == NAME)
                {
                    person.setName(reader[NAME].ToString());
                }
                else if (pParam == ID_CARD)
                {
                    person.setIDCard(reader[ID_CARD].ToString());
                }
                else if (pParam == FIRST_LSTNM)
                {
                    person.setFirstLastName(reader[FIRST_LSTNM].ToString());
                }
                else if (pParam == SECOND_LSTNM)
                {
                    person.setSecondLastName(reader[SECOND_LSTNM].ToString());
                }
                else if (pParam == TYPE)
                {
                    person.setPersonType(reader[TYPE].ToString());
                }
                else if (pParam == PHOTO)
                {
                    byte[] photo = reader[PHOTO].GetType() != typeof(System.DBNull) ? (byte[])reader[PHOTO] : null;
                    person.setPhotoBytes(photo);
                }
                list.Add(person);
            }
            reader.Close();
            return list;
        }
    }
}
