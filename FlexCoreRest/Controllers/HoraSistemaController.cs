using FlexCoreDTOs.administration;
using FlexCoreLogic.administracion;
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
    public class HoraSistemaController : ApiController
    {
        //GET /sistema/hora
        //Obtiene la hora del sistema
        public HttpResponseMessage GetObtenerHoraSistema()
        {
            try
            {
                ConfiguracionesDTO _horaSistema = FacadeAdministracion.obtenerHoraSistema();
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _horaSistema);
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

        //POST /sistema/hora
        //Realiza algunos comandos en la hora del sistema
        //segundo -> Aumenta un segundo al tiempo de la base
        //minuto -> Aumenta un minuto al tiempo de la base
        //hora -> Aumenta una hora al tiempo de la base
        //mes -> Aumenta un mes al tiempo de la base
        //año -> Aumenta un año al tiempo de la base
        //pausarReloj -> Pausa el reloj de la base
        //reanudarReloj -> Reanuda el reloj de la base si esta pausado
        public HttpResponseMessage PostCrearPersonaJuridica()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                switch (_datosPost)
                {
                    case "segundo":
                        TiempoManager.agregarSegundos(1);
                        break;
                    case "minuto":
                        TiempoManager.agregarMinutos(1);
                        break;
                    case "hora":
                        TiempoManager.agregarHoras(1);
                        break;
                    case "mes":
                        TiempoManager.agregarMeses(1);
                        break;
                    case "ano":
                        TiempoManager.agregarAnos(1);
                        break;
                    case "pausarReloj":
                        TiempoManager.pausarReloj();
                        break;
                    case "reanudarReloj":
                        TiempoManager.reanudarReloj();
                        break;
                }
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, "True");
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
