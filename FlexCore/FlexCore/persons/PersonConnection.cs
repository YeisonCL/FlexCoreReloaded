﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCore.general;
using System.Windows.Forms;

namespace FlexCore.persons
{
    static class PersonConnection
    {
        private static readonly string IP = "http://192.168.0.115";
        private static string PORT = "6358";
        private static readonly string PHYSICAL_PERSON = "/persona/fisica";
        private static readonly string JURIDICAL_PERSON = "/persona/juridica";
        private static readonly string GENERIC_PERSON= "/persona/todas";
        private static readonly string DOCUMENT = "/persona/documento";
        private static readonly string PHONE = "/persona/telefono";
        private static readonly string ADDRESS = "/persona/direccion";
        private static readonly string PHOTO = "/persona/foto";
        private static readonly string ERROR_MSG = "False";

        private static readonly string NAME = "Nombre=";
        private static readonly string F_LASTNAME = "PrimerApellido=";
        private static readonly string S_LASTNAME = "SegundoApellido=";
        private static readonly string ID_CARD = "Cedula=";
        private static readonly string PERSON_ID = "IdPersona=";

        private static readonly string PAGE_NUMBER = "NumeroPagina=";
        private static readonly string COUNT = "CantidadMostrar=";
        private static readonly string ORDER = "Ordenamiento=";

        public static List<PhysicalPersonDTO> getPhysicalPerson(PhysicalPersonDTO pPerson, int pPageNumber = 0, int pItemCount = 0, string pOrderBy = "")
        {
            string name = pPerson.getName() == ""?"":NAME + pPerson.getName();
            string fLast = pPerson.getFirstLastName() == "" ? "" : F_LASTNAME + pPerson.getFirstLastName();
            string sLast = pPerson.getSecondLastName() == "" ? "" : S_LASTNAME + pPerson.getSecondLastName();
            string idCard = pPerson.getIDCard() == "" ? "" : ID_CARD + pPerson.getIDCard();
            string personID = pPerson.getPersonID() == DTOConstants.DEFAULT_INT_ID ? "" : PERSON_ID + pPerson.getPersonID();
            string pageNumber = pPageNumber == 0 ? "" : PAGE_NUMBER + pPageNumber;
            string count = pItemCount == 0 ? "" : COUNT + pItemCount;
            string order = pOrderBy == "" ? "" : ORDER + pOrderBy;

            string attribs = Utils.getGetAttributes(name, fLast, sLast, idCard, personID, pageNumber, count, order);

            RestClient client = new RestClient(IP + ":" + PORT + PHYSICAL_PERSON, HttpVerb.GET);
            try
            {
                MessageBox.Show(attribs);
                string ans = client.MakeRequest("?"+attribs);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<PhysicalPersonDTO> list = Utils.deserializeObject<List<PhysicalPersonDTO>>(ans);
                    return list;
                }
            } 
            catch (Exception e) 
            {
                throw e;
            }
        }

        public static int newPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string msg = Utils.serializeObejct<PhysicalPersonDTO>(pPerson);
            RestClient client = new RestClient(IP + ":" + PORT + PHYSICAL_PERSON, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                return Convert.ToInt32(ans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PersonDTO> getJuridicalPerson(PersonDTO pPerson, int pPageNumber = 0, int pItemCount = 0, string pOrderBy = "")
        {
            string name = pPerson.getName() == "" ? "" : NAME + pPerson.getName();
            string idCard = pPerson.getIDCard() == "" ? "" : ID_CARD + pPerson.getIDCard();
            string personID = pPerson.getPersonID() == DTOConstants.DEFAULT_INT_ID ? "" : PERSON_ID + pPerson.getPersonID();
            string pageNumber = pPageNumber == 0 ? "" : PAGE_NUMBER + pPageNumber;
            string count = pItemCount == 0 ? "" : COUNT + pItemCount;
            string order = pOrderBy == "" ? "" : ORDER + pOrderBy;

            string attribs = Utils.getGetAttributes(name, idCard, personID, pageNumber, count, order);
            MessageBox.Show("jur" + attribs);
            RestClient client = new RestClient(IP + ":" + PORT + JURIDICAL_PERSON, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?"+attribs);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<PersonDTO> list = Utils.deserializeObject<List<PersonDTO>>(ans);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int newJuridicalPerson(PersonDTO pPerson)
        {
            string msg = Utils.serializeObejct<PersonDTO>(pPerson);

            RestClient client = new RestClient(IP + ":" + PORT + JURIDICAL_PERSON, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                return Convert.ToInt32(ans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PersonDocumentDTO> getPersonDocuments(int pPersonID)
        {
            string personID = PERSON_ID + pPersonID;

            RestClient client = new RestClient(IP + ":" + PORT + DOCUMENT, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?" + personID);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<PersonDocumentDTO> list = Utils.deserializeObject<List<PersonDocumentDTO>>(ans);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void newDocument(PersonDocumentDTO pDocument)
        {
            string msg = Utils.serializeObejct<PersonDocumentDTO>(pDocument);

            RestClient client = new RestClient(IP + ":" + PORT + DOCUMENT, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PersonPhoneDTO> getPersonPhones(int pPersonID)
        {
            string personID = PERSON_ID + pPersonID;

            RestClient client = new RestClient(IP + ":" + PORT + PHONE, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?"+personID);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    MessageBox.Show(ans);
                    List<PersonPhoneDTO> list = Utils.deserializeObject<List<PersonPhoneDTO>>(ans);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void newPhone(PersonPhoneDTO pPhone)
        {
            string msg = Utils.serializeObejct<PersonPhoneDTO>(pPhone);

            RestClient client = new RestClient(IP + ":" + PORT + PHONE, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int getAllMaxPage(int pCount)
        {
            string count = "CantidadMostrar=" + pCount;
            string page = "Pagina=true";
            RestClient client = new RestClient(IP + ":" + PORT + GENERIC_PERSON, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest(String.Format("?{0}&{1}", page, count));
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    return Convert.ToInt32(ans);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GenericPersonDTO> getAllPersons(int pPage, int pCount, string pOrderBy)
        {
            string page = "NumeroPagina="+pPage;
            string count = "CantidadMostrar="+pCount;
            string order = "Ordenamiento="+pOrderBy;
            RestClient client = new RestClient(IP + ":" + PORT + GENERIC_PERSON, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest(String.Format("?{0}&{1}&{2}", page, count, order));
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<GenericPersonDTO> list = Utils.deserializeObject<List<GenericPersonDTO>>(ans);
                    return list;
                }
            } 
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PersonAddressDTO> getPersonAddress(int pPersonID)
        {
            string personID = PERSON_ID + pPersonID;

            RestClient client = new RestClient(IP + ":" + PORT + ADDRESS, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?" + personID);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<PersonAddressDTO> list = Utils.deserializeObject<List<PersonAddressDTO>>(ans);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void newAddress(PersonAddressDTO pAddress)
        {
            string msg = Utils.serializeObejct<PersonAddressDTO>(pAddress);

            RestClient client = new RestClient(IP + ":" + PORT + ADDRESS, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static PersonPhotoDTO getPersonPhoto(int pPersonID)
        {
            string personID = PERSON_ID + pPersonID;

            RestClient client = new RestClient(IP + ":" + PORT + PHOTO, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?" + personID);
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    PersonPhotoDTO list = Utils.deserializeObject<PersonPhotoDTO>(ans);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void setPhoto(PersonPhotoDTO pPhoto)
        {
            string msg = Utils.serializeObejct<PersonPhotoDTO>(pPhoto);

            RestClient client = new RestClient(IP + ":" + PORT + PHOTO, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
