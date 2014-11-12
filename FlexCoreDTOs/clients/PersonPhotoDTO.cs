﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonPhotoDTO
    {
        public byte[] _photoBytes;
        public int _personID;

        public PersonPhotoDTO()
        {
            _personID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonPhotoDTO(int pPersonID)
        {
            _personID = pPersonID;
            _photoBytes = null;
        }

        public PersonPhotoDTO(int pPersonID, byte[] pFile)
        {
            _photoBytes = pFile;
            _personID = pPersonID;
        }

        public PersonPhotoDTO(byte[] pFile)
            :this (DTOConstants.DEFAULT_INT_ID, pFile)
        {

        }

        //setters
        public void setHexBytes(byte[] pFile) { _photoBytes = pFile; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public byte[] getHexBytes() { return _photoBytes; }
        public int getPersonID() { return _personID; }
    }
}
