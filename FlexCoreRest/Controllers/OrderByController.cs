using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class OrderByController : ApiController
    {
        //GET /orderby?PersonaFisica=valor&PersonaJuridica=valor&TodasPersonas=valor&Cliente=valor
        //Obtener los orderby definidos
        public HttpResponseMessage GetObtenerPersonaFisica(string PersonaFisica = "", string PersonaJuridica = "", string TodasPersonas = "", string Cliente = "")
        {
            try
            {
                List<String> _orderBy = new List<string>();
                if(PersonaFisica == "True")
                {

                }
                else if(PersonaJuridica == "True")
                {

                }
                else if(TodasPersonas == "True")
                {

                }
                else if(Cliente == "True")
                {

                }
                string _orderBySerializada = TransformingObjects.serializeObejct<List<String>>(_orderBy);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_orderBySerializada, Encoding.UTF8, "text/plain");
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
