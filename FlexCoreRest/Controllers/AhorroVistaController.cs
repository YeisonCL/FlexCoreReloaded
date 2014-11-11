using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class AhorroVistaController : ApiController
    {
        //POST cuentas/ahorrovista
        //Crea una nueva cuenta ahorro vista
        public HttpResponseMessage PostCrearCuentaAhorroVista()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroVistaDTO _cuentaAhorroVista = TransformingObjects.deserializeObject<CuentaAhorroVistaDTO>(_datosPost);
                FacadeCuentas.agregarCuentaAhorroVista(_cuentaAhorroVista);
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

        //GET cuentas/ahorrovista?NumeroCuenta=valor
        //Obtener una cuenta ahorro vista
        public HttpResponseMessage GetObtenerCuentaAhorroVistaNumeroCuenta(string NumeroCuenta)
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
                _cuentaAhorroVistaDTO.setNumeroCuenta(NumeroCuenta);
                CuentaAhorroVistaDTO _cuentaAhorroVistaList = FacadeCuentas.obtenerCuentaAhorroVistaNumeroCuenta(_cuentaAhorroVistaDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _cuentaAhorroVistaList);
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

        //PUT cuentas/ahorrovista
        //Modifica una nueva cuenta ahorro vista
        public HttpResponseMessage PutModificarCuentaAhorroVista()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = TransformingObjects.deserializeObject<CuentaAhorroVistaDTO>(_datosPut);
                FacadeCuentas.modificarCuentaAhorroVista(_cuentaAhorroVistaDTO);
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
