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
using System.Windows.Forms;

namespace FlexCoreLogic.clients
{

    abstract class AbstractPersonLogic<DTO> where DTO : PersonDTO
    {

        protected static readonly string ID_CARD = "Cédula";
        protected static readonly string NAME = "Nombre";
        protected static readonly string TYPE = "Tipo";

        public virtual int insert(DTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                return insert(pPerson, command);
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

        public virtual int newPerson(DTO pPerson, List<PersonAddressDTO> pAddresses, List<PersonPhoneDTO> pPhones, List<PersonDocumentDTO> pDocuments, PersonPhotoDTO pPhoto)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                int pid = newPerson(pPerson, command, pAddresses, pPhones, pDocuments, pPhoto);
                tran.Commit();
                return pid;
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

        public virtual int newPerson(DTO pPerson, SqlCommand pCommand, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            int pid = insert(pPerson, pCommand);
            if (pAddresses != null)
            {
                foreach (var address in pAddresses)
                {
                    address.setPersonID(pid);
                }
                addAddress(pAddresses, pCommand);
            }
            if (pPhoto != null)
            {
                pPhoto.setPersonID(pid);
                updatePhoto(pPhoto, pCommand);
            }
            if (pPhones != null)
            {
                foreach (var phone in pPhones)
                {
                    phone.setPersonID(pid);
                }
                addPhone(pPhones, pCommand);
            }
            if (pDocuments != null)
            {
                foreach (var doc in pDocuments)
                {
                    doc.setPersonID(pid);
                }
                addDoc(pDocuments, pCommand);
            }
            return pid;
        }

        public List<GenericPersonDTO> getAllPersons(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                return GenericPersonVDAO.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (Exception e)
            {
                throw new SearchException("", e);
            }
            
        }

        public abstract int insert(DTO pPerson, SqlCommand pCommand);

        public abstract void delete(DTO pPerson, SqlCommand pCommand);

        public abstract void update(DTO pNewPerson, DTO pPastPerson, SqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, SqlCommand pCommand, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public List<string> getSortCategories()
        {
            List<string> list = new List<string>();
            list.Add(ID_CARD);
            list.Add(NAME);
            list.Add(TYPE);
            return list;
        }

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
                return PersonDAO.getInstance().search(pPerson, pCommand).Count != 0;
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

        public void addAddress(PersonAddressDTO pAddress)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addAddress(pAddress, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addAddress(PersonAddressDTO pAddress, SqlCommand pCommand)
        {
            try
            {
                PersonAddressDAO.getInstance().insert(pAddress, pCommand);
            }
            catch (SqlException e)
            {
                throw new InsertException();
            }
        }

        public void deleteAddress(PersonAddressDTO pAddress)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deleteAddress(pAddress, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deleteAddress(PersonAddressDTO pAddress, SqlCommand pCommand)
        {
            try
            {
                PersonAddressDAO.getInstance().delete(pAddress, pCommand);
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

        public void updateAddress(PersonAddressDTO pOldAddress, PersonAddressDTO pNewAddress)
        {
            try
            {
                PersonAddressDAO.getInstance().update(pNewAddress, pOldAddress);
            }
            catch (SqlException e)
            {
                throw new UpdateException("", e);
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
            catch (Exception e)
            {
                throw new UpdateException("", e);
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
                List<PersonPhotoDTO> result = dao.search(pPhoto, pCommand);
                if (result.Count != 0)
                {
                    MessageBox.Show("lalalala");
                    dao.update(pPhoto, pPhoto, pCommand);
                }
                else
                {
                    MessageBox.Show("adsdasdasd");
                    dao.insert(pPhoto, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException("", e);
            }
        }

        public PersonPhotoDTO getPhoto(PersonDTO pPerson)
        {
            PersonPhotoDTO dummy = new PersonPhotoDTO(pPerson.getPersonID());
            try
            {
                List<PersonPhotoDTO> result = PersonPhotoDAO.getInstance().search(dummy);
                if (result.Count != 0){
                    return result[0];
                } 
                else 
                {
                    return null;
                }
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

        public void addPhone(PersonPhoneDTO pPhone)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addPhone(pPhone, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addPhone(PersonPhoneDTO pPhone, SqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO.getInstance().insert(pPhone, pCommand);
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }

        public void deletePhone(PersonPhoneDTO pPhone)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deletePhone(pPhone, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deletePhone(PersonPhoneDTO pPhone, SqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO.getInstance().delete(pPhone, pCommand);
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

        public void updatePhone(PersonPhoneDTO pOldPhone, PersonPhoneDTO pNewPhone)
        {
            try
            {
                PersonPhoneDAO.getInstance().update(pOldPhone, pNewPhone);
            } 
            catch (SqlException e){
                throw new UpdateException("", e);
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
                List<PersonDocumentDTO> result;
                foreach (PersonDocumentDTO document in pDocuments)
                {
                    result = dao.search(document);
                    if (result.Count != 0)
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

        public void addDoc(PersonDocumentDTO pDocument)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                addDoc(pDocument, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void addDoc(PersonDocumentDTO pDocument, SqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                List<PersonDocumentDTO> result;
                result = dao.search(pDocument);
                if (result.Count != 0)
                {
                    dao.update(pDocument, pDocument, pCommand);
                }
                else
                {
                    dao.insert(pDocument, pCommand);
                }
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
        }        

        public void deleteDoc(PersonDocumentDTO pDocument)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            try
            {
                deleteDoc(pDocument, command);
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public void deleteDoc(PersonDocumentDTO pDocument, SqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO.getInstance().delete(pDocument, pCommand);                
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

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocument)
        {
            return PersonDocumentDAO.getInstance().searchPartial(pDocument);
        }

        protected virtual string getOrderBy(string pSort)
        {
            if (pSort == ID_CARD)
            {
                return PersonDAO.ID_CARD;
            }
            else if (pSort == NAME)
            {
                return PersonDAO.NAME;
            }
            else if (pSort == TYPE)
            {
                return PersonDAO.TYPE;
            }
            else
            {
                return null;
            }
        }

    }
}
