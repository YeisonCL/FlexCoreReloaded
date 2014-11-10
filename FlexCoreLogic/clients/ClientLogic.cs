using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.exceptions;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;

namespace FlexCoreLogic.clients
{
    class ClientLogic
    {

        private static ClientLogic _instance = null;
        private static object _syncLock = new object();

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

        public void newClient(PersonDTO pPerson, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            try
            {
                PersonLogic personLogic = PersonLogic.getInstance();
                if (!personLogic.exists(pPerson))
                {
                    if (pPerson.getPersonType() == PersonDTO.PHYSICAL_PERSON)
                    {
                        PhysicalPersonLogic.getInstance().newPerson((PhysicalPersonDTO)pPerson, pAddresses, pPhones, pDocuments, pPhoto);
                    }
                    else
                    {
                        JuridicPersonLogic.getInstance().newPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
                    }
                    pPerson = PersonLogic.getInstance().search(pPerson)[0];
                }
                this.insert(pPerson);
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
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                insert(pPerson, command);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
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
                if (!PersonLogic.getInstance().exists(pPerson, pCommand))
                {
                    if (pPerson.getPersonType() == PersonDTO.JURIDIC_PERSON)
                    {
                        int id = JuridicPersonLogic.getInstance().insert(pPerson, pCommand);
                        pPerson.setPersonID(id);
                    }
                    else
                    {
                        int id = PhysicalPersonLogic.getInstance().insert((PhysicalPersonDTO)pPerson, pCommand);
                        pPerson.setPersonID(id);

                    }
                }
                ClientDAO clientDAO = ClientDAO.getInstance();
                ClientDTO client = new ClientDTO();
                client.setClientID(pPerson.getPersonID());
                client.setCIF(generarCIF());
                client.setActive(true);
                clientDAO.insert(client, pCommand);
            }
            catch (SqlException e)
            {
                throw new InsertException();
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

        public List<ClientVDTO> search(ClientVDTO pClient, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                return search(pClient, command, pPageNumber, pShowCount, pOrderBy);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public List<ClientVDTO> search(ClientVDTO pClient, SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
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

        public List<ClientVDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                ClientVDAO dao = ClientVDAO.getInstance();
                return dao.getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        }

        private static string generarCIFAux()
        {
            string _CIF = "";
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            for (int i = 0; i < 10; i++)
            {
                int _numero = _random.Next(0, 10);
                _CIF = _CIF + Convert.ToString(_numero);
                System.Threading.Thread.Sleep(1);
            }
            string _CIFAux = new string(_CIF.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
            return _CIFAux;
        }

        public static string generarCIF()
        {
            string CIF = "";
            bool generate = true;
            ClientDTO dummy = new ClientDTO();
            while (generate)
            {
                CIF = generarCIFAux();
                dummy.setCIF(CIF);
                ClientDTO result = ClientDAO.getInstance().search(dummy)[0];
                if (result == null) { generate = false; }
            }
            return CIF;
        }
    }
}
