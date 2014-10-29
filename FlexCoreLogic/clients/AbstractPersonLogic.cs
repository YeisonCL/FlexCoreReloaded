using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using FlexCoreLogic.exceptions;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;

namespace FlexCoreLogic.clients
{

    abstract class AbstractPersonLogic<DTO>
    {

        public virtual void insert(DTO pPerson)
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

        public virtual void delete(DTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                delete(pPerson, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public virtual void update(DTO pNewPerson, DTO pPastPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                update(pNewPerson, pPastPerson, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public virtual List<DTO> search(DTO pPerson, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                return search(pPerson, command, pPageNumber, pShowCount, pOrderBy);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public virtual void newPerson(DTO pPerson, List<PersonAddressDTO> pAddresses, List<PersonPhoneDTO> pPhones, List<PersonDocumentDTO> pDocuments, PersonPhotoDTO pPhoto)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                newPerson(pPerson, command, pAddresses, pPhones, pDocuments, pPhoto);
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

        public virtual void newPerson(DTO pPerson, SqlCommand pCommand, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            insert(pPerson, pCommand);
            if (pAddresses != null)
            {
                addAddress(pAddresses, pCommand);
            }
            if (pPhoto != null)
            {
                updatePhoto(pPhoto, pCommand);
            }
            if (pPhones != null)
            {
                addPhone(pPhones, pCommand);
            }
            if (pDocuments != null)
            {
                addDoc(pDocuments, pCommand);
            }
        }

        public abstract void insert(DTO pPerson, SqlCommand pCommand);

        public abstract void delete(DTO pPerson, SqlCommand pCommand);

        public abstract void update(DTO pNewPerson, DTO pPastPerson, SqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, SqlCommand pCommand, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public bool exists(PersonDTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                return exists(pPerson, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public bool exists(PersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                return PersonDAO.getInstance().search(pPerson, pCommand)[0] != null;
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        }

        //Address

        public void addAddress(List<PersonAddressDTO> pAddresses)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addAddress(pAddresses, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addAddress(List<PersonAddressDTO> pAddresses, SqlCommand pCommand){
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.insert(address, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new InsertException();
            }
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deleteAddress(pAddresses, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses, SqlCommand pCommand)
        {
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.delete(address, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new DeleteException();
            }
        }

        public List<PersonAddressDTO> getAddress(PersonDTO pPerson)
        {
            PersonAddressDTO dummy = new PersonAddressDTO(pPerson.getPersonID());
            try
            {                
                return PersonAddressDAO.getInstance().search(dummy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
            
        }

        //Photo

        public void updatePhoto(PersonPhotoDTO pPhoto)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                updatePhoto(pPhoto, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void updatePhoto(PersonPhotoDTO pPhoto, SqlCommand pCommand)
        {
            try
            {
                PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
                PersonPhotoDTO result = dao.search(pPhoto, pCommand)[0];
                if (result != null)
                {
                    dao.update(pPhoto, pPhoto, pCommand);
                }
                else
                {
                    dao.insert(pPhoto, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public PersonPhotoDTO getPhoto(PersonDTO pPerson)
        {
            PersonPhotoDTO dummy = new PersonPhotoDTO(pPerson.getPersonID());
            try
            {
                return PersonPhotoDAO.getInstance().search(dummy)[0];
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
            
        }

        //Phone

        public void addPhone(List<PersonPhoneDTO> pPhones)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addPhone(pPhones, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addPhone(List<PersonPhoneDTO> pPhones, SqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.insert(phone, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deletePhone(pPhones, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones, SqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.delete(phone, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public List<PersonPhoneDTO> getPhones(PersonDTO pPerson)
        {
            PersonPhoneDTO dummy = new PersonPhoneDTO(pPerson.getPersonID());
            try
            {
                return PersonPhoneDAO.getInstance().search(dummy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        }

        //Document

        public void addDoc(List<PersonDocumentDTO> pDocuments)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addDoc(pDocuments, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addDoc(List<PersonDocumentDTO> pDocuments, SqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                PersonDocumentDTO result;
                foreach (PersonDocumentDTO document in pDocuments)
                {
                    result = dao.search(document)[0];
                    if (result != null)
                    {
                        dao.update(document, document, pCommand);
                    }
                    else
                    {
                        dao.insert(document, pCommand);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }        

        public void deleteDoc(List<PersonDocumentDTO> pDocuments)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deleteDoc(pDocuments, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deleteDoc(List<PersonDocumentDTO> pDocuments, SqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                foreach (PersonDocumentDTO document in pDocuments)
                {
                    dao.delete(document, pCommand);
                }
                
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public PersonDocumentDTO getCompleteDoc(PersonDocumentDTO pDocumment)
        {
            try
            {
                return PersonDocumentDAO.getInstance().search(pDocumment)[0];
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        }

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocument, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return PersonDocumentDAO.getInstance().searchPartial(pDocument, pPageNumber, pShowCount, pOrderBy);
        }

    }
}
