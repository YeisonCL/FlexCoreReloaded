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
    public class DireccionController : ApiController
    {
        //POST /persona/creardireccion
        //Crea una nueva direccion a una persona
        public HttpResponseMessage PostCrearDireccion()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                byte[] _direccionByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                PersonAddressDTO _personDocument = (PersonAddressDTO)TransformingObjects.ByteArrayToObject(_direccionByte);
                //ClientsFacade.getInstance().newDocument SE AGREGA LA DIRECCION
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
