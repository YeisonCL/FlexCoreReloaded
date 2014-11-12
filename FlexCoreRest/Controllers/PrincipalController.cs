using FlexCoreLogic.cuentas.Facade;
using FlexCoreLogic.principalogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class PrincipalController : ApiController
    {
        //GET 
        //Iniciar logica principal
        public HttpResponseMessage GetIniciarLogica()
        {
            try
            {
                TiempoManager.iniciarReloj();
                ControladorDeCierre.iniciarControladorDeCierre();
                FacadeCuentas.iniciarSincronizacionCuentasAhorroAutomatico();
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "Iniciada logica principal...");
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
