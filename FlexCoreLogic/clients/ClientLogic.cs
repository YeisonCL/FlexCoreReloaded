using System;
using System.Collections.Generic;
using System.Linq;
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

        public int newClientAndPerson(PersonDTO pPerson, List<PersonAddressDTO> pAddresses = null, List<PersonPhoneDTO> pPhones = null, List<PersonDocumentDTO> pDocuments = null, PersonPhotoDTO pPhoto = null)
        {
            PersonLogic personLogic = PersonLogic.getInstance();
            int pid;
            if (pPerson.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                Console.WriteLine("INSERTANDO NUEVA PERSONA:");
                Console.WriteLine(String.Format("Nombre:{0} Apellido1:{1} Apellido2:{2} Cedula:{3}", pPerson.getName(), ((PhysicalPersonDTO)pPerson).getFirstLastName(), ((PhysicalPersonDTO)pPerson).getSecondLastName(), pPerson.getIDCard()));
                pid = PhysicalPersonLogic.getInstance().newPerson((PhysicalPersonDTO)pPerson, pAddresses, pPhones, pDocuments, pPhoto);
                Console.WriteLine("ID de persona:" + pid);
            }
            else
            {
                pid = JuridicPersonLogic.getInstance().newPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
            }
            Console.WriteLine("Se ha inseretado la persona (Lease con acento español)");
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
                Console.WriteLine("Generando CIF");
                client.setCIF(generarCIF());
                Console.WriteLine("CIF listo!");
                client.setActive(true);
                clientDAO.insert(client, pCommand);
                Console.WriteLine("Cliente insertado con exito :D");
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

        //private static string generarCIFAux()
        //{
        //    string _CIF = "";
        //    int _semilla = (int)DateTime.Now.Millisecond;
        //    Random _random = new Random(_semilla);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int _numero = _random.Next(0, 10);
        //        _CIF = _CIF + Convert.ToString(_numero);
        //        System.Threading.Thread.Sleep(1);
        //    }
        //    string _CIFAux = new string(_CIF.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
        //    return _CIFAux;
        //}

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
    }
}
