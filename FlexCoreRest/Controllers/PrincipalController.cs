using FlexCoreLogic.cuentas.Facade;
using FlexCoreLogic.principalogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("Iniciada lógica principal....", Encoding.UTF8, "text/plain");
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
