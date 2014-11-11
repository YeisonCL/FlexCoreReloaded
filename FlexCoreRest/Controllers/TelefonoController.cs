using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class TelefonoController : ApiController
    {
        //POST /persona/creartelefono
        //Crea un nuevo telefono a una persona
        public HttpResponseMessage PostCrearTelefono()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonPhoneDTO _personPhone = TransformingObjects.deserializeObject<PersonPhoneDTO>(_datosPost);
                ClientsFacade.getInstance().addPhone(_personPhone);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }
    }
}
