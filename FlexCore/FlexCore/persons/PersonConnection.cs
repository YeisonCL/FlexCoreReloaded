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
        private static readonly string IP = "http://25.100.205.44";
        private static string PORT = "6358";
        private static readonly string NEW_PHYSICAL_PERSON = "/persona/fisica";
        private static readonly string NEW_JURIDICAL_PERSON = "/persona/juridica";
        private static readonly string NEW_DOCUMENT = "/persona/documento";
        private static readonly string NEW_PHONE = "/persona/telefono";
        private static readonly string NEW_ADDRESS = "/persona/direccion";
        private static readonly string NEW_PHOTO = "/persona/foto";

        public static int newPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string msg = Utils.serializeObejct<PhysicalPersonDTO>(pPerson);
            MessageBox.Show(msg);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_PHYSICAL_PERSON, HttpVerb.POST, msg);
            try
            {
                string ans = client.MakeRequest();
                MessageBox.Show(ans);
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

            RestClient client = new RestClient(IP + ":" + PORT + NEW_JURIDICAL_PERSON, HttpVerb.POST, msg);
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

        public static void newDocument(PersonDocumentDTO pDocument)
        {
            string msg = Utils.serializeObejct<PersonDocumentDTO>(pDocument);

            RestClient client = new RestClient(IP + ":" + PORT + NEW_DOCUMENT, HttpVerb.POST, msg);
            try
            {
                client.MakeRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void newPhone(PersonPhoneDTO pPhone)
        {
            string msg = Utils.serializeObejct<PersonPhoneDTO>(pPhone);

            RestClient client = new RestClient(IP + ":" + PORT + NEW_PHONE, HttpVerb.POST, msg);
            try
            {
                client.MakeRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void newAddress(PersonAddressDTO pAddress)
        {
            string msg = Utils.serializeObejct<PersonAddressDTO>(pAddress);

            RestClient client = new RestClient(IP + ":" + PORT + NEW_ADDRESS, HttpVerb.POST, msg);
            try
            {
                client.MakeRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void setPhoto(PersonPhotoDTO pPhoto)
        {
            string msg = Utils.serializeObejct<PersonPhotoDTO>(pPhoto);

            RestClient client = new RestClient(IP + ":" + PORT + NEW_DOCUMENT, HttpVerb.POST, msg);
            try
            {
                client.MakeRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
