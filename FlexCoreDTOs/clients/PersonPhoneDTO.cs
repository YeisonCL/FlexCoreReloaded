using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class PersonPhoneDTO
    {
        public string _phone;
        public int _personID;

        public PersonPhoneDTO()
        {
            _phone = DTOConstants.DEFAULT_STRING;
            _personID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonPhoneDTO(int pPersonID)
        {
            _personID = pPersonID;
            _phone = DTOConstants.DEFAULT_STRING;
        }

        public PersonPhoneDTO(int pPersonID, string pPhone)
        {
            _phone = pPhone;
            _personID = pPersonID;
        }

        public PersonPhoneDTO(string pPhone)
            :this (DTOConstants.DEFAULT_INT_ID, pPhone)
        {

        }

        //setters
        public void setPhone(string pPhone) { _phone = pPhone; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getPhone() { return _phone; }
        public int getPersonID() { return _personID; }
    }
}
