//using FlexCoreDTOs.cuentas;
//using FlexCoreLogic.cuentas.Facade;
//using FlexCoreRest.Conversiones;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace FlexCoreRest.Controllers
//{
//    public class AhorroVistaController : ApiController
//    {
//        //POST cuentas/ahorrovista
//        //Crea una nueva cuenta ahorro vista
//        public HttpResponseMessage PostCrearCuentaAhorroVista()
//        {
//            try
//            {
//                string _datosPost = Request.Content.ReadAsStringAsync().Result;
//                byte[] _cuentaAhorroVistaByte = TransformingObjects.ConvertHexToBytes(_datosPost);
//                CuentaAhorroVistaDTO _cuentaAhorroVista = (CuentaAhorroVistaDTO)TransformingObjects.ByteArrayToObject(_cuentaAhorroVistaByte);
//                FacadeCuentas.agregarCuentaAhorroVista(_cuentaAhorroVista);
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
