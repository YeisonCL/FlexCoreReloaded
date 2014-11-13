using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    [DataContract(Namespace = "")]
    public class JuridicalClientVDTO
    {
        public ClientDTO _client;
        public PersonDTO _person;

        public JuridicalClientVDTO()
        {
            _client = new ClientDTO();
            _person = new PersonDTO();
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(int pClientID)
        {
            _client = new ClientDTO(pClientID);
            _person = new PersonDTO(pClientID);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(int pIDClient, string pName, string pIDCard, string pType, string pCIF, bool pActive = false)
        {
            _client = new ClientDTO(pIDClient, pCIF, pActive);
            _person = new PersonDTO(pIDClient, pName, pIDCard, pType);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(string pName, string pIDCard, string pType, string pCIF, bool pActive = false)
        {
            _client = new ClientDTO(DTOConstants.DEFAULT_INT_ID, pCIF, pActive);
            _person = new PersonDTO(DTOConstants.DEFAULT_INT_ID, pName, pIDCard, pType);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        //Setters
        public void setClientID(int pClientID)
        {
            _client.setClientID(pClientID);
            _person.setPersonID(pClientID);
        }

        public void setCIF(string pCIF) { _client.setCIF(pCIF); }

        public void setActive(bool pActive) { _client.setActive(pActive); }

        public void setName(string pName) { _person.setName(pName); }

        public void setIDCard(string pIDCard) { _person.setIDCard(pIDCard); }

        public void setPersonType(string pType) { _person.setPersonType(pType); }

        //Getters
        public int getClientID() { return _client.getClientID(); }

        public string getCIF() { return _client.getCIF(); }

        public bool isActive() { return _client.isActive(); }

        public int getPersonID() { return _person.getPersonID(); }

        public string getName() { return _person.getName(); }

        public string getIDCard() { return _person.getIDCard(); }

        public string getPersonType() { return _person.getPersonType(); }

        public ClientDTO getClientDTO() { return _client; }

        public PersonDTO getPersonDTO() { return _person; }


    }
}
