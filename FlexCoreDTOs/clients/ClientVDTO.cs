﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class ClientVDTO
    {
        public PersonDTO _person;
        public ClientDTO _client;

        public static readonly string PHYSICAL_PERSON = "Fisica";
        public static readonly string JURIDIC_PERSON = "Juridica";

        public ClientVDTO()
        {
            _client = new ClientDTO();
            _person = new PersonDTO();
            _person.setPersonType(PersonDTO.PHYSICAL_PERSON);
        }

        public ClientVDTO(int pClientID)
        {
            _client = new ClientDTO(pClientID);
            _person = new PersonDTO(pClientID);
        }

        public ClientVDTO(int pIDClient, string pName, string pIDCard, string pCIF, string pType, bool pActive = false)
        {
            _client = new ClientDTO(pIDClient, pCIF, pActive);
            _person = new PersonDTO(pIDClient, pName, pIDCard, pType);
        }

        public ClientVDTO(string pName, string pFirstLastName, string pSecondLastName, string pIDCard, string pCIF, string pType, bool pActive = false)
        {
            _client = new ClientDTO(DTOConstants.DEFAULT_INT_ID, pCIF, pActive);
            _person = new PersonDTO(DTOConstants.DEFAULT_INT_ID, pName, pIDCard, pType);
        }

        //Setters
        public void setClientID(int pClientID) { 
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
