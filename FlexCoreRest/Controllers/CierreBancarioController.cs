using FlexCoreDTOs.administration;
using FlexCoreLogic.administracion;
using FlexCoreLogic.principalogic;
using FlexCoreRest.Conversiones;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class CierreBancarioController : ApiController
    {
        //GET /sistema/cierre
        //Obtiene los cierres hasta la fecha del sistema bancario
        public HttpResponseMessage GetObtenerCierres()
        {
            try
            {
                List<CierreDTO> _cierresBancarios = new List<CierreDTO>();
                _cierresBancarios = FacadeAdministracion.obtenerCierresBancarios();
                string _cierresBancariosSerializado = TransformingObjects.serializeObejct<List<CierreDTO>>(_cierresBancarios);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_cierresBancariosSerializado, Encoding.UTF8, "text/plain");
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

        //POST /sistema/cierre
        //Fuerza un cierre bancario
        public HttpResponseMessage PostIniciarCierre()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                ControladorDeCierre.iniciarCierre();
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
