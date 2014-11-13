using System;
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

        public static int newPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string msg = Utils.serializeObejct<PhysicalPersonDTO>(pPerson);
            MessageBox.Show(msg);
            RestClient client = new RestClient(IP + ":" + PORT + PHYSICAL_PERSON, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                MessageBox.Show("ans:" + ans + "f.");
                return Convert.ToInt32(ans);
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
                    MessageBox.Show("eee");
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

        public static void setPhoto(PersonPhotoDTO pPhoto)
        {
            string msg = Utils.serializeObejct<PersonPhotoDTO>(pPhoto);

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

    }
}
