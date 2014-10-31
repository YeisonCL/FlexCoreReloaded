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
        private static readonly string NEW_PHYSICAL_PERSON = "/persona/fisica";
        private static readonly string NEW_JURIDICAL_PERSON = "/persona/juridica";
        private static readonly string NEW_DOCUMENT = "/persona/documento";
        private static readonly string NEW_PHONE = "/persona/telefono";
        private static readonly string NEW_ADDRESS = "/persona/direccion";
        private static readonly string NEW_PHOTO = "/persona/foto";

        public static int newPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string hex = Utils.ObjectToHexString(pPerson);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_PHYSICAL_PERSON, HttpVerb.POST, hex);
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
            string hex = Utils.ObjectToHexString(pPerson);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_JURIDICAL_PERSON, HttpVerb.POST, hex);
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
            string hex = Utils.ObjectToHexString(pDocument);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_DOCUMENT, HttpVerb.POST, hex);
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
            string hex = Utils.ObjectToHexString(pPhone);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_PHONE, HttpVerb.POST, hex);
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
            string hex = Utils.ObjectToHexString(pAddress);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_ADDRESS, HttpVerb.POST, hex);
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
            string hex = Utils.ObjectToHexString(pPhoto);
            RestClient client = new RestClient(IP + ":" + PORT + NEW_DOCUMENT, HttpVerb.POST, hex);
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
