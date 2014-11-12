using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Facade;
using FlexCoreRest.Conversiones;
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
    public class AhorroAutomaticoController : ApiController
    {
        //POST cuenta/ahorroautomatico
        //Crea una nueva cuenta ahorro automatico
        public HttpResponseMessage PostCrearCuentaAhorroAutomatico()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = TransformingObjects.deserializeObject<CuentaAhorroAutomaticoDTO>(_datosPost);
                FacadeCuentas.agregarCuentaAhorroAutomatico(_cuentaAhorroAutomatico);
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

        //GET cuenta/ahorroautomatico?NumeroCuenta=valor
        //Obtener una cuenta ahorro automatico dado un numero de cuenta
        public HttpResponseMessage GetObtenerCuentaAhorroAutomaticoNumeroCuenta(string NumeroCuenta = "")
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomaticoDTO.setNumeroCuenta(NumeroCuenta);
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = FacadeCuentas.obtenerCuentaAhorroAutomaticoNumeroCuenta(_cuentaAhorroAutomaticoDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _cuentaAhorroAutomatico);
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

        //GET cuenta/ahorroautomatico?Cedula=valor
        //Obtener una cuenta ahorro automatico dada una cedula
        public HttpResponseMessage GetObtenerCuentaAhorroAutomaticoCedula(string Cedula = "")
        {
            try
            {
                ClientVDTO _clienteVDTO = new ClientVDTO();
                _clienteVDTO.setIDCard(Cedula);
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomaticoDTO.setCliente(_clienteVDTO);
                List<CuentaAhorroAutomaticoDTO> _cuentaAhorroAutomaticoList = FacadeCuentas.obtenerCuentaAhorroAutomaticoCedula(_cuentaAhorroAutomaticoDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _cuentaAhorroAutomaticoList);
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

        //GET cuenta/ahorroautomatico?CIF=valor
        //Obtener una cuenta ahorro automatico dado un CIF
        public HttpResponseMessage GetObtenerCuentaAhorroAutomaticoCIF(string CIF = "")
        {
            try
            {
                ClientVDTO _clienteVDTO = new ClientVDTO();
                _clienteVDTO.setCIF(CIF);
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomaticoDTO.setCliente(_clienteVDTO);
                List<CuentaAhorroAutomaticoDTO> _cuentaAhorroAutomaticoList = FacadeCuentas.obtenerCuentaAhorroAutomaticoCIF(_cuentaAhorroAutomaticoDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _cuentaAhorroAutomaticoList);
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

        //PUT cuenta/ahorroautomatico
        //Modifica una cuenta ahorro Automatico
        public HttpResponseMessage PutModificarCuentaAhorroAutomatico()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = TransformingObjects.deserializeObject<CuentaAhorroAutomaticoDTO>(_datosPut);
                FacadeCuentas.modificarCuentaAhorroAutomatico(_cuentaAhorroAutomaticoDTO);
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

        //DELETE cuenta/ahorroautomatico?NumeroCuenta=valor
        //Borra una cuenta ahorro automatico dado el numero de cuenta
        public HttpResponseMessage PutModificarCuentaAhorroAutomatico(string NumeroCuenta = "")
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomaticoDTO.setNumeroCuenta(NumeroCuenta);
                FacadeCuentas.eliminarCuentaAhorroAutomatico(_cuentaAhorroAutomaticoDTO);
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
