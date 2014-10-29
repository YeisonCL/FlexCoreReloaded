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
    public class DocumentoController : ApiController
    {
        //POST /persona/creardocumento
        //Crea un nuevo documento a una persona
        public HttpResponseMessage PostCrearDocumento()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                byte[] _documentByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                List<PersonDocumentDTO> _personDocument = (List<PersonDocumentDTO>)TransformingObjects.ByteArrayToObject(_documentByte);
                //ClientsFacade.getInstance().newDocument SE AGREGA EL DOCUMENTO
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
