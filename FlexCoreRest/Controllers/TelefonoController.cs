using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class TelefonoController : ApiController
    {
        //POST /persona/telefono
        //Crea un nuevo telefono a una persona
        public HttpResponseMessage PostCrearTelefono()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonPhoneDTO _personPhone = TransformingObjects.deserializeObject<PersonPhoneDTO>(_datosPost);
                ClientsFacade.getInstance().addPhone(_personPhone);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("True", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("False", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //GET /persona/telefono?IdPersona=valor
        //Obtiene un nuevo documento
        public HttpResponseMessage GetObtenerTelefono(string IdPersona = "")
        {
            try
            {
                PersonDTO _personPhoneDTO = new PersonDTO(Convert.ToInt32(IdPersona));
                List<PersonPhoneDTO> _phonePersonList = ClientsFacade.getInstance().getPhones(_personPhoneDTO);
                string _phonePersonListSerializado = TransformingObjects.serializeObejct<List<PersonPhoneDTO>>(_phonePersonList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_phonePersonListSerializado, Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("False", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //PUT /persona/telefono
        //Modifica un telefono asociado a una persona
        public HttpResponseMessage PutModificarTelefono()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                UpdateDTO<PersonPhoneDTO> _telefonoList = TransformingObjects.deserializeObject<UpdateDTO<PersonPhoneDTO>>(_datosPut);
                PersonPhoneDTO _telefonoAnterior = _telefonoList._previous;
                PersonPhoneDTO _telefonoNuevo = _telefonoList._new;
                ClientsFacade.getInstance().updatePhone(_telefonoAnterior, _telefonoNuevo);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("True", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("False", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //DELETE /persona/telefono?IdPersona=valor
        //Borra el telefono asociado a una persona
        public HttpResponseMessage DeleteBorrarTelefono(string IdPersona = "")
        {
            try
            {
                PersonPhoneDTO _phonePersonDTO = new PersonPhoneDTO(Convert.ToInt32(IdPersona));
                ClientsFacade.getInstance().deletePhone(_phonePersonDTO);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("True", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("False", Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }
    }
}
