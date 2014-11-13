using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FlexCoreDTOs.clients
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class GenericPersonDTO:PhysicalPersonDTO
    {
        public GenericPersonDTO() { }

        public byte[] _photoBytes;

        //setters
        public void setPhotoBytes(byte[] pFile) { _photoBytes = pFile; }

        //getters
        public byte[] getPhotoBytes() { return _photoBytes; }


        public GenericPersonDTO(int pPersonID, string pName, string pIDCard, byte[] pPhoto = null)
            : base(pPersonID, pName, DTOConstants.DEFAULT_STRING, DTOConstants.DEFAULT_STRING, pIDCard)
        {
            setPersonType(PersonDTO.JURIDIC_PERSON);
            _photoBytes = pPhoto;
        }

        public GenericPersonDTO(int pPersonID, string pName, string pFirstLastName, string pSecondLastName, string pIDCard, byte[] pPhoto = null)
            : base (pPersonID, pName, pFirstLastName, pSecondLastName, pIDCard)
        {
            setPersonType(PersonDTO.PHYSICAL_PERSON);
            _photoBytes = pPhoto;
        }

        public string getPersonType() { return _type; }

        public void setPersonType(string pType) { _type = pType; }

    }
}
