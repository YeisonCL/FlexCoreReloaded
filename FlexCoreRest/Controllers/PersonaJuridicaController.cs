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
    public class PersonaJuridicaController : ApiController
    {
        //POST /personajuridica
        //Crea una nueva persona juridica
        public HttpResponseMessage PostCrearPersonaJuridica()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                byte[] _juridicalPersonByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                PersonDTO _juridicalPerson = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicalPersonByte);
                ClientsFacade.getInstance().newJuridicalPerson(_juridicalPerson);
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
