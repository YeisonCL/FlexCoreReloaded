using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using System.Collections.Generic;
using FlexCoreLogic.general;

namespace FlexCoreLogic.clients
{
    public class ClientsFacade
    {

        private static ClientsFacade _instance = null;
        private static object _syncLock = new object();

        public static ClientsFacade getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientsFacade();
                    }
                }
            }
            return _instance;
        }

        private ClientsFacade() { }

        public ClientDTO getClientByID(ClientDTO pClient)
        {
            return ClientLogic.getInstance().getClientByID(pClient);
        }

        public int newClient(PersonDTO pPerson, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            return ClientLogic.getInstance().newClient(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
        }

        public void insertClient(PersonDTO pPerson)
        {
            ClientLogic.getInstance().insert(pPerson);
        }

        public void deleteClient(ClientDTO pClient)
        {
            ClientLogic.getInstance().delete(pClient);
        }

        public SearchResultDTO<ClientVDTO> searchClient(ClientVDTO pClient, int pPageNumber=0, int pShowCount=0, string pOrderBy = "")
        {
            return ClientLogic.getInstance().search(pClient, pPageNumber, pShowCount, pOrderBy);
        }

        public SearchResultDTO<ClientVDTO> getAllClient(int pPageNumber=0, int pShowCount=0, string pOrderBy = "")
        {
            return ClientLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }

        public bool isClientActive(ClientDTO pClient)
        {
            return ClientLogic.getInstance().isActive(pClient);
        }

        public void setClientActiveStatus(ClientDTO pClient)
        {
            ClientLogic.getInstance().setActive(pClient);
        }

        public List<string> getClientOrderBy()
        {
            return ClientLogic.getInstance().getOrderByList();
        }

        //address
        public void addAddress(List<PersonAddressDTO> pAddresses)
        {
            PersonLogic.getInstance().addAddress(pAddresses);
        }

        public void updateAddress(PersonAddressDTO pOldAddress, PersonAddressDTO pNewAddress)
        {
            PersonLogic.getInstance().updateAddress(pOldAddress, pNewAddress);
        }

        public void addAddress(PersonAddressDTO pAddress)
        {
            PersonLogic.getInstance().addAddress(pAddress);
        }

        public void deleteAddress(PersonAddressDTO pAddress)
        {
            PersonLogic.getInstance().deleteAddress(pAddress);
        }

        public List<PersonAddressDTO> getAddress(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getAddress(pPerson);
        }

        //Photo
        public void updatePhoto(PersonPhotoDTO pPhoto)
        {
            PersonLogic.getInstance().updatePhoto(pPhoto);
        }

        public PersonPhotoDTO getPhoto(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getPhoto(pPerson);
        }

        //Phone

        public void addPhone(List<PersonPhoneDTO> pPhones)
        {
            PersonLogic.getInstance().addPhone(pPhones);
        }

        public void addPhone(PersonPhoneDTO pPhone)
        {
            PersonLogic.getInstance().addPhone(pPhone);
        }

        public void deletePhone(PersonPhoneDTO pPhones)
        {
            PersonLogic.getInstance().deletePhone(pPhones);
        }

        public int newClientAndPerson(PersonDTO pPerson, List<PersonAddressDTO> pAddresses = null, List<PersonPhoneDTO> pPhones = null, List<PersonDocumentDTO> pDocuments = null, PersonPhotoDTO pPhoto = null)
        {
            return ClientLogic.getInstance().newClientAndPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
        }

        public List<PersonPhoneDTO> getPhones(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getPhones(pPerson);
        }

        public void updatePhone(PersonPhoneDTO pOldPhone, PersonPhoneDTO pNewPhone)
        {
            PersonLogic.getInstance().updatePhone(pOldPhone, pNewPhone);
        }

        //Document

        public void addDoc(List<PersonDocumentDTO> pDocuments)
        {
            PersonLogic.getInstance().addDoc(pDocuments);
        }

        public void addDoc(PersonDocumentDTO pDocuments)
        {
            PersonLogic.getInstance().addDoc(pDocuments);
        }

        public void deleteDoc(PersonDocumentDTO pDocuments)
        {
            PersonLogic.getInstance().deleteDoc(pDocuments);
        }

        public PersonDocumentDTO getCompleteDoc(PersonDocumentDTO pDocumment)
        {
            return PersonLogic.getInstance().getCompleteDoc(pDocumment);
        }

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocumment)
        {
            return PersonLogic.getInstance().getPartialDoc(pDocumment);
        }


        //juridical person
        public int newJuridicalPerson(PersonDTO pPerson, List<PersonAddressDTO> pAddresses = null, List<PersonPhoneDTO> pPhones = null, List<PersonDocumentDTO> pDocuments = null, PersonPhotoDTO pPhoto = null){
            return JuridicPersonLogic.getInstance().newPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
        }

        public int newJuridicalPerson(PersonDTO pPerson)
        {
            return JuridicPersonLogic.getInstance().insert(pPerson);
        }

        public void deleteJuridicalPerson(PersonDTO pPerson)
        {
            JuridicPersonLogic.getInstance().delete(pPerson);
        }

        public void updateJuridicalPerson(PersonDTO pNewPerson, PersonDTO pPastPerson)
        {
            JuridicPersonLogic.getInstance().update(pNewPerson, pPastPerson);
        }


        public SearchResultDTO<PersonDTO> searchJuridicalPerson(PersonDTO pPerson, int pPageNumber = 0, int pShowCount = 0, string pOrderBy = "")
        {
            return JuridicPersonLogic.getInstance().search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PersonDTO> getAllJuridicalPerson(int pPageNumber = 0, int pShowCount = 0, string pOrderBy = "")
        {
            return JuridicPersonLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }

        public List<string> getJuridicalOrderBy()
        {
            return JuridicPersonLogic.getInstance().getOrderByList();
        }

        //physical person
        public int newPhysicalPerson(PhysicalPersonDTO pPerson, List<PersonAddressDTO> pAddresses = null, List<PersonPhoneDTO> pPhones = null, List<PersonDocumentDTO> pDocuments = null, PersonPhotoDTO pPhoto = null)
        {
            return PhysicalPersonLogic.getInstance().newPerson(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
        }

        public int insertPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            return PhysicalPersonLogic.getInstance().insert(pPerson);
        }

        public void deletePhysicalPerson(PhysicalPersonDTO pPerson)
        {
            PhysicalPersonLogic.getInstance().delete(pPerson);
        }

        public void updatePhysicalPerson(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson)
        {
            PhysicalPersonLogic.getInstance().update(pNewPerson, pPastPerson);
        }

        public SearchResultDTO<PhysicalPersonDTO> searchPhysicalPerson(PhysicalPersonDTO pPerson, int pPageNumber = 0, int pShowCount = 0, string pOrderBy = "")
        {
            return PhysicalPersonLogic.getInstance().search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PhysicalPersonDTO> getAllPhysicalPerson(int pPageNumber=0, int pShowCount=0, string pOrderBy = "")
        {
            return PhysicalPersonLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }

        public List<string> getPhysicalPersonOrderBy()
        {
            return PhysicalPersonLogic.getInstance().getOrderByList();
        }

        //----------

        public SearchResultDTO<GenericPersonDTO> getAllPersons(int pPageNumber, int pShowCount, string pOrderBy)
        {
            return PersonLogic.getInstance().getAllPersons(pPageNumber, pShowCount, pOrderBy);
        }

        public SearchResultDTO<GenericPersonDTO> searchAllPersons(GenericPersonDTO pPerson, int pPageNumber, int pShowCount, string pOrderBy)
        {
            return PersonLogic.getInstance().searchAllPersons(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<string> getAllPersonsOrderBy()
        {
            return PersonLogic.getInstance().getAllOrderByList();
        }

    }
}
