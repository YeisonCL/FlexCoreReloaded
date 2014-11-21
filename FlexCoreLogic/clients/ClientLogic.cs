using System;
using System.Collections.Generic;
using System.Linq;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.exceptions;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreDTOs.general;
using FlexCoreLogic.general;

namespace FlexCoreLogic.clients
{
    class ClientLogic
    {

        private static ClientLogic _instance = null;
        private static object _syncLock = new object();

        public static string ACTIVE = "Estado";
        public static string CIF = "CIF";
        public static string FIRST_LSTNM = "Primer apellido";
        public static string ID_CARD = "Cédula";
        public static string NAME = "Nombre";
        public static string SECOND_LSTNM = "Segundo apellido";
        public static string TYPE = "Tipo de persona";

        public static ClientLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientLogic();
                    }
                }
            }
            return _instance;
        }

        private ClientLogic() { }

        public int newClientAndPerson(PersonDTO pPerson, List<PersonAddressDTO> pAddresses = null, List<PersonPhoneDTO> pPhones = null, List<PersonDocumentDTO> pDocuments = null, PersonPhotoDTO pPhoto = null)
        {
            PersonLogic personLogic = PersonLogic.getInstance();
            int pid;
            if (pPerson.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                pid = PhysicalPersonLogic.getInstance().newPerson((PhysicalPersonDTO)pPerson, pAddresses, pPhones, pDocuments, pPhoto);
            }
            else
            {
                pid = JuridicPersonLogic.getInstance().newPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
            }
            pPerson.setPersonID(pid);
            this.insert(pPerson);
            return pid;
        }

        public int newClient(PersonDTO pPerson, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            try
            {
                PersonLogic personLogic = PersonLogic.getInstance();
                if (!personLogic.exists(pPerson))
                {
                    return newClientAndPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
                }
                else
                {
                    this.insert(pPerson);
                    return DTOConstants.DEFAULT_INT_ID;
                }
            }
            catch (Exception e)
            {
                throw new InsertException("",e);
            }
        }

        public bool isActive(ClientDTO pClient)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                return isActive(pClient, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public ClientDTO getClientByID(ClientDTO pClient)
        {
            try
            {
                List<ClientDTO> result = ClientDAO.getInstance().search(pClient);
                if (result.Count == 0)
                {
                    throw new SearchException("No se ha podido encontrar el cliente mediante el id " + pClient.getClientID());
                }
                else
                {
                    return result[0];
                }
            }
            catch (Exception e)
            {
                throw new SearchException("", e);
            }
        }

        public bool isActive(ClientDTO pClient, SqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                ClientDTO result = dao.search(pClient, pCommand)[0];
                return result.isActive();
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
            
        }

        public void setActive(ClientDTO pClient)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                setActive(pClient, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void setActive(ClientDTO pClient, SqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                dao.setActive(pClient, pCommand);
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public void insert(PersonDTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                insert(pPerson, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void insert(PersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                ClientDAO clientDAO = ClientDAO.getInstance();
                ClientDTO client = new ClientDTO();
                client.setClientID(pPerson.getPersonID());
                client.setCIF(generarCIF());
                client.setActive(true);
                clientDAO.insert(client, pCommand);
            }
            catch (SqlException e)
            {
                throw new InsertException("", e);
            }
        }

        public void delete(ClientDTO pClient)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                delete(pClient, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void delete(ClientDTO pClient, SqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                dao.delete(pClient, pCommand);
            }
            catch (SqlException e)
            {
                throw new DeleteException();
            }
            
        }

        public int searchCount(ClientVDTO pClient)
        {
            try
            {
                return ClientVDAO.getInstance().getSearchCount(pClient);
            }
            catch (Exception e)
            {
                throw new SearchException("", e);
            }
        }

        public SearchResultDTO<ClientVDTO> search(ClientVDTO pClient, int pPageNumber, int pShowCount, string pOrderBy)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                string orderBy = getOrderBy(pOrderBy);
                List<ClientVDTO> result = search(pClient, command, pPageNumber, pShowCount, orderBy);
                SearchResultDTO<ClientVDTO> searchResult = new SearchResultDTO<ClientVDTO>(result);
                if (pShowCount != 0)
                {
                    int count = searchCount(pClient);
                    int maxPage = Utils.getMaxPage(count, pShowCount);
                    searchResult.setMaxPage(maxPage);
                }
                return searchResult;
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public List<ClientVDTO> search(ClientVDTO pClient, SqlCommand pCommand, int pPageNumber, int pShowCount, string pOrderBy)
        {
            try
            {
                ClientVDAO dao = ClientVDAO.getInstance();
                return dao.search(pClient, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
            
        }

        public int getAllCount()
        {
            try
            {
                return ClientVDAO.getInstance().getAllCount();
            }
            catch (Exception e)
            {
                throw new SearchException("", e);
            }
        }

        public SearchResultDTO<ClientVDTO> getAll(int pPageNumber, int pShowCount, string pOrderBy)
        {
            try
            {
                string orderBy = getOrderBy(pOrderBy);
                ClientVDAO dao = ClientVDAO.getInstance();
                List<ClientVDTO> result = dao.getAll(pPageNumber, pShowCount, orderBy);
                SearchResultDTO<ClientVDTO> searchResult = new SearchResultDTO<ClientVDTO>(result);
                if (pShowCount != 0)
                {
                    int count = getAllCount();
                    int maxPage = Utils.getMaxPage(count, pShowCount);
                    searchResult.setMaxPage(maxPage);
                }
                return searchResult;
            }
            catch (SqlException e)
            {
                throw new SearchException("", e);
            }
        }

        public static string generarCIF()
        {
            string CIF = "";
            bool generate = true;
            ClientDTO dummy = new ClientDTO();
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            while (generate)
            {
                CIF = "";
                for (int i = 0; i < 10; i++)
                {
                    int _numero = _random.Next(0, 10);
                    CIF = CIF + Convert.ToString(_numero);
                    System.Threading.Thread.Sleep(1);
                }
                CIF = new string(CIF.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
                dummy.setCIF(CIF);
                List<ClientDTO> result = ClientDAO.getInstance().search(dummy);
                if (result.Count == 0) { generate = false; }
            }
            return CIF;
        }

        public List<string> getOrderByList()
        {
            List<string> list = new List<string>();
            list.Add(ACTIVE);
            list.Add(CIF);
            list.Add(FIRST_LSTNM);
            list.Add(ID_CARD);
            list.Add(NAME);
            list.Add(SECOND_LSTNM);
            list.Add(TYPE);
            return list;
        }

        protected string getOrderBy(string pSort)
        {
            if (pSort == ACTIVE)
            {
                return ClientVDAO.ACTIVE;
            }
            else if (pSort == CIF)
            {
                return ClientVDAO.CIF;
            }
            else if (pSort == FIRST_LSTNM)
            {
                return ClientVDAO.FIRST_LSTNM;
            }
            else if (pSort == ID_CARD)
            {
                return ClientVDAO.ID_CARD;
            }
            else if (pSort == NAME)
            {
                return ClientVDAO.NAME;
            }
            else if (pSort == SECOND_LSTNM)
            {
                return ClientVDAO.SECOND_LSTNM;
            }
            else if (pSort == TYPE)
            {
                return ClientVDAO.TYPE;
            }
            else
            {
                return "";
            }
        }
    }
}
