using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class GenericPersonDTO:PhysicalPersonDTO
    {
        public GenericPersonDTO() { }

        public GenericPersonDTO(int pPersonID, string pName, string pIDCard)
            : base(pPersonID, pName, DTOConstants.DEFAULT_STRING, DTOConstants.DEFAULT_STRING, pIDCard)
        {
            setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public GenericPersonDTO(int pPersonID, string pName, string pFirstLastName, string pSecondLastName, string pIDCard)
            : base (pPersonID, pName, pFirstLastName, pSecondLastName, pIDCard)
        {
            setPersonType(PersonDTO.PHYSICAL_PERSON);
        }

        public string getPersonType() { return _type; }

        public void setPersonType(string pType) { _type = pType; }

    }
}
