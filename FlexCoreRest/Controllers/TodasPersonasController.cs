using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class TodasPersonasController : ApiController
    {
        //GET /persona/todas?Pagina=valor
        //Obtiene el numero de paginas totales correspondiente a todas las personas
        public HttpResponseMessage GetObtenerNumeroPaginasTotales(string Pagina)
        {
            try
            {
                int _paginas = 0;
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_paginas.ToString(), Encoding.UTF8, "text/plain");
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

        //GET /persona/todas?NumeroPagina=valor&CantidadMostrar=valor&Ordenamiento=valor
        //Obtiene toda las personas
        public HttpResponseMessage GetObtenerTodasPersonas(string NumeroPagina = "0", string CantidadMostrar = "0", string Ordenamiento = "")
        {
            try
            {
                List<GenericPersonDTO> _genericPersonList = ClientsFacade.getInstance().getAllPersons(Convert.ToInt32(NumeroPagina), Convert.ToInt32(CantidadMostrar), 
                    Ordenamiento);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _genericPersonList);
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch(Exception e)
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(e.Message, Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }
    }
}
