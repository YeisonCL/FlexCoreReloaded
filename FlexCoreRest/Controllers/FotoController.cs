//using FlexCoreDTOs.clients;
//using FlexCoreRest.Conversiones;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace FlexCoreRest.Controllers
//{
//    public class FotoController : ApiController
//    {
//        //POST /persona/crearfoto
//        //Crea una nueva foto a una persona
//        public HttpResponseMessage PostCrearFoto()
//        {
//            try
//            {
//                string _datosPost = Request.Content.ReadAsStringAsync().Result;
//                byte[] _fotoByte = TransformingObjects.ConvertHexToBytes(_datosPost);
//                PersonPhotoDTO _personDocument = (PersonPhotoDTO)TransformingObjects.ByteArrayToObject(_fotoByte);
//                //ClientsFacade.getInstance().newDocument SE AGREGA LA FOTO
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//            catch
//            {
//                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "False");
//                _request.Headers.Add("Access-Control-Allow-Origin", "*");
//                return _request;
//            }
//        }
//    }
//}
