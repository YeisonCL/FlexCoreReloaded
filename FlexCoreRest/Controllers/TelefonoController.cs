using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //GET /persona/telefono?Id=valor
        //Obtiene un nuevo documento
        public HttpResponseMessage GetObtenerTelefono(string Id = "")
        {
            try
            {
                PersonDTO _personPhoneDTO = new PersonDTO(Convert.ToInt32(Id));
                List<PersonPhoneDTO> _phonePersonList = ClientsFacade.getInstance().getPhones(_personPhoneDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _phonePersonList);
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //FALTA HACER EL PUT

        //DELETE /persona/telefono?Id=valor
        //Borra el telefono asociado a una persona
        public HttpResponseMessage DeleteBorrarTelefono(string Id = "")
        {
            try
            {
                PersonPhoneDTO _phonePersonDTO = new PersonPhoneDTO(Convert.ToInt32(Id));
                ClientsFacade.getInstance().deletePhone(_phonePersonDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }
    }
}
