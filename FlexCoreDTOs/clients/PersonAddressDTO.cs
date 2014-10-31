﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    [Serializable()]
    public class PersonAddressDTO
    {
        private string _address;
        private int _personID;

        public PersonAddressDTO(int pPersonID)
        {
            _personID = pPersonID;
            _address = DTOConstants.DEFAULT_STRING;
        }

        public PersonAddressDTO(int pPersonID, string pAddress)
        {
            _address = pAddress;
            _personID = pPersonID;
        }

        public PersonAddressDTO(string pAddress)
            :this (DTOConstants.DEFAULT_INT_ID, pAddress)
        {

        }

        //setters
        public void setAddress(string pAddress) { _address = pAddress; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getAddress() { return _address; }
        public int getPersonID() { return _personID; }

    }
}
