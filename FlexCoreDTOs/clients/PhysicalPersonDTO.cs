using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    [DataContract(Namespace = "")]
    public class PhysicalPersonDTO:PersonDTO
    {
        public string _firstLastName;
        public string _secondLastName;

        public PhysicalPersonDTO()
        {
            _firstLastName = DTOConstants.DEFAULT_STRING;
            _secondLastName = DTOConstants.DEFAULT_STRING;
            setPersonType(PHYSICAL_PERSON);
        }

        public PhysicalPersonDTO(int pPersonID)
            : base (pPersonID)
        {
            _firstLastName = DTOConstants.DEFAULT_STRING;
            _secondLastName = DTOConstants.DEFAULT_STRING;
        }

        public PhysicalPersonDTO(int pPersonID, string pName, string pFirstLastName, string pSecondLastName, string pIDCard)
            : base (pPersonID, pName, pIDCard, PersonDTO.PHYSICAL_PERSON)
        {
             _firstLastName = pFirstLastName;
            _secondLastName = pSecondLastName;
        }

        public PhysicalPersonDTO(string pName, string pFirstLastName, string pSecondLastName, string pIDCard)
            : base(DTOConstants.DEFAULT_INT_ID, pName, pIDCard, PersonDTO.PHYSICAL_PERSON)
        {
            _firstLastName = pFirstLastName;
            _secondLastName = pSecondLastName;
        }

        //setters
        public void setFirstLastName(string pLastName) { _firstLastName = pLastName; }
        public void setSecondLastName(string pLastName) { _secondLastName = pLastName; }

        //getters
        public string getFirstLastName() { return _firstLastName; }
        public string getSecondLastName() { return _secondLastName; }

    }
}
