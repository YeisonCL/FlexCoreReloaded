using FlexCoreDTOs.clients;
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

        //GET /persona/documento?IdPersona=valor&Nombre=valor
        //Obtiene un nuevo documento
        public HttpResponseMessage GetObtenerDocumentoCompleto(string IdPersona, string Nombre)
        {
            try
            {
                PersonDocumentDTO _personDocumentDTO = new PersonDocumentDTO();
                _personDocumentDTO.setPersonID(Convert.ToInt32(IdPersona));
                _personDocumentDTO.setName(Nombre);
                PersonDocumentDTO _documentPersonComplete = ClientsFacade.getInstance().getCompleteDoc(_personDocumentDTO);
                string _documentPersonCompleteSerializado = TransformingObjects.serializeObejct<PersonDocumentDTO>(_documentPersonComplete);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_documentPersonCompleteSerializado, Encoding.UTF8, "text/plain");
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

        //GET /persona/documento?IdPersona=valor
        //Obtiene un documento parcialmente
        public HttpResponseMessage GetObtenerDocumentoParcial(string IdPersona)
        {
            try
            {
                PersonDocumentDTO _personDocumentDTO = new PersonDocumentDTO();
                _personDocumentDTO.setPersonID(Convert.ToInt32(IdPersona));
                List<PersonDocumentDTO> _documentPersonList = ClientsFacade.getInstance().getPartialDoc(_personDocumentDTO);
                string _documentPersonListSerializado = TransformingObjects.serializeObejct<List<PersonDocumentDTO>>(_documentPersonList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_documentPersonListSerializado, Encoding.UTF8, "text/plain");
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

        //PUT /persona/documento
        //Modifica el documento asociado a una persona
        public HttpResponseMessage PutModificarDocumento()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonDocumentDTO _personDocument = TransformingObjects.deserializeObject<PersonDocumentDTO>(_datosPost);
                ClientsFacade.getInstance().addDoc(_personDocument);
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

        //DELETE /persona/documento?IdPersona=valor&Nombre=valor
        //Borra una direccion
        public HttpResponseMessage DeleteBorrarDocumento(string IdPersona, string Nombre)
        {
            try
            {
                PersonDocumentDTO _documentPersonDTO = new PersonDocumentDTO();
                _documentPersonDTO.setPersonID(Convert.ToInt32(IdPersona));
                _documentPersonDTO.setName(Nombre);
                ClientsFacade.getInstance().deleteDoc(_documentPersonDTO);
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
