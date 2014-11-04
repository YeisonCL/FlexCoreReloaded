//using FlexCoreDTOs.clients;
//using FlexCoreLogic.clients;
//using FlexCoreRest.Conversiones;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Web.Http;

//namespace FlexCoreRest.Controllers
//{
//    public class PersonaJuridicaController : ApiController
//    {
//        //POST /persona/juridica
//        //Crea una nueva persona juridica
//        public HttpResponseMessage PostCrearPersonaJuridica()
//        {
//            try
//            {
//                string _datosPost = Request.Content.ReadAsStringAsync().Result;
//                byte[] _juridicalPersonByte = TransformingObjects.ConvertHexToBytes(_datosPost);
//                PersonDTO _juridicalPerson = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicalPersonByte);
//                ClientsFacade.getInstance().newJuridicalPerson(_juridicalPerson);
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//            catch
//            {
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//        }

//        //GET /persona/juridica?Busqueda=valor
//        //Obtener una persona juridica
//        public HttpResponseMessage GetObtenerPersonaJuridica(string Busqueda)
//        {
//            try
//            {
//                byte[] _juridicPersonDTOByte = TransformingObjects.ConvertHexToBytes(Busqueda);
//                PersonDTO _juridicPersonDTO = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicPersonDTOByte);
//                List<PersonDTO> _juridicPersonList = ClientsFacade.getInstance().searchJuridicalPerson(_juridicPersonDTO);
//                byte[] _juridicPersonListByte = TransformingObjects.ObjectToByteArray(_juridicPersonList);
//                string _juridicPersonListHex = BitConverter.ToString(_juridicPersonListByte);
//                _juridicPersonListHex = _juridicPersonListHex.Replace("-", "");
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _juridicPersonListHex);
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//            catch
//            {
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//        }

//        //PUT /persona/juridica?Anterior=valor&Nueva=valor
//        //Modifica una persona juridica
//        public HttpResponseMessage PutModificarPersonaJuridica(string Anterior, string Nueva)
//        {
//            try
//            {
//                byte[] _juridicPersonDTOByteAnterior = TransformingObjects.ConvertHexToBytes(Anterior);
//                byte[] _juridicPersonDTOByteNueva = TransformingObjects.ConvertHexToBytes(Nueva);
//                PersonDTO _juridicPersonDTOAnterior = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicPersonDTOByteAnterior);
//                PersonDTO _juridicPersonDTONueva = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicPersonDTOByteNueva);
//                ClientsFacade.getInstance().updateJuridicalPerson(_juridicPersonDTONueva, _juridicPersonDTOAnterior);
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//            catch
//            {
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//        }

//        //DELETE /persona/juridica?Borrado=valor
//        //Borra una persona juridica de la base de datos
//        public HttpResponseMessage DeleteBorrarPersonaJuridica(string Borrado)
//        {
//            try
//            {
//                byte[] _juridicPersonDTOByte = TransformingObjects.ConvertHexToBytes(Borrado);
//                PersonDTO _juridicPersonDTO = (PersonDTO)TransformingObjects.ByteArrayToObject(_juridicPersonDTOByte);
//                ClientsFacade.getInstance().deleteJuridicalPerson(_juridicPersonDTO);
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//            catch
//            {
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
//                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//        }
//    }
//}
