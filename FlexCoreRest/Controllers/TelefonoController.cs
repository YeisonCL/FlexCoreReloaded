using FlexCoreDTOs.clients;
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
                byte[] _telefonoByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                List<PersonPhoneDTO> _personDocument = (List<PersonPhoneDTO>)TransformingObjects.ByteArrayToObject(_telefonoByte);
                //ClientsFacade.getInstance().newDocument SE AGREGA EL TELEFONO A LA PERSONA
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
