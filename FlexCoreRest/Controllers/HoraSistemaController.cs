using FlexCoreDTOs.administration;
using FlexCoreLogic.administracion;
using FlexCoreLogic.principalogic;
using FlexCoreRest.Conversiones;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
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
                string _horaSistemaSerializada = TransformingObjects.serializeObejct<DateTime>(TiempoManager.obtenerHoraActual());
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_horaSistemaSerializada, Encoding.UTF8, "text/plain");
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
                    case "dia":
                        TiempoManager.agregarDia(1);
                        break;
                    case "mes":
                        TiempoManager.agregarMeses(1);
                        break;
                    case "año":
                        TiempoManager.agregarAnos(1);
                        break;
                    case "pausarReloj":
                        TiempoManager.pausarReloj();
                        break;
                    case "reanudarReloj":
                        TiempoManager.reanudarReloj();
                        break;
                }
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
