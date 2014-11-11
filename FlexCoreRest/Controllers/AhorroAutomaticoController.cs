using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class AhorroAutomaticoController : ApiController
    {
        //POST cuentas/ahorroautomatico
        //Crea una nueva cuenta ahorro automatico
        public HttpResponseMessage PostCrearCuentaAhorroAutomatico()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = TransformingObjects.deserializeObject<CuentaAhorroAutomaticoDTO>(_datosPost);
                FacadeCuentas.agregarCuentaAhorroAutomatico(_cuentaAhorroAutomatico);
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
