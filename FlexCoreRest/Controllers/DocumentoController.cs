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
    public class DocumentoController : ApiController
    {
        //POST /persona/creardocumento
        //Crea un nuevo documento a una persona
        public HttpResponseMessage PostCrearDocumento()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonDocumentDTO _personDocument = TransformingObjects.deserializeObject<PersonDocumentDTO>(_datosPost);
                ClientsFacade.getInstance().addDoc(_personDocument);
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

        //GET /persona/documento?Id=valor
        //Obtiene un nuevo documento
        public HttpResponseMessage GetObtenerDocumento(string Id = "")
        {
            try
            {
                PersonDocumentDTO _personDocumentDTO = new PersonDocumentDTO();
                _personDocumentDTO.setPersonID(Convert.ToInt32(Id));
                List<PersonDocumentDTO> _documentPersonList = ClientsFacade.getInstance().getPartialDoc(_personDocumentDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _documentPersonList);
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

        //FALTA PUT DOCUMENTO

        //DELETE /persona/documento?Id=valor&Nombre=valor
        //Borra una direccion
        public HttpResponseMessage DeleteBorrarDocumento(string Id = "", string Nombre = "")
        {
            try
            {
                PersonDocumentDTO _documentPersonDTO = new PersonDocumentDTO(Convert.ToInt32(Id), Nombre);
                ClientsFacade.getInstance().deleteDoc(_documentPersonDTO);
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
