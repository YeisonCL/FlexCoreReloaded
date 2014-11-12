using FlexCoreLogic.pagos.VerificacionPreviaPagos;
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
    public class PagosController : ApiController
    {
        //POST pagos/pagar
        //Reliza un nuevo pago
        public HttpResponseMessage PostPagar()
        {
            try
            {
                PrePagos _iniciarPago = new PrePagos();
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                List<string> _valores = new List<string>();
                _valores = crearListaValores(_datosPost);
                _iniciarPago.iniciarPago(_valores[1], _valores[3], _valores[5]);
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

        private List<string> crearListaValores(string pPost)
        {
            List<string> _valores = new List<string>();
            string[] _arregloValores = pPost.Split(new string[] { "=", "&" }, StringSplitOptions.None);
            foreach (string valor in _arregloValores)
            {
                _valores.Add(valor);
            }
            return _valores;
        }
    }
}
