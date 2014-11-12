﻿using FlexCoreDTOs.clients;
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
        //POST cuenta/ahorrovista
        //Crea una nueva cuenta ahorro vista
        public HttpResponseMessage PostCrearCuentaAhorroVista()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                CuentaAhorroVistaDTO _cuentaAhorroVista = TransformingObjects.deserializeObject<CuentaAhorroVistaDTO>(_datosPost);
                FacadeCuentas.agregarCuentaAhorroVista(_cuentaAhorroVista);
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

        //GET cuenta/ahorrovista?NumeroCuenta=valor
        //Obtener una cuenta ahorro vista dado un numero de cuenta
        public HttpResponseMessage GetObtenerCuentaAhorroVistaNumeroCuenta(string NumeroCuenta = "")
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
                _cuentaAhorroVistaDTO.setNumeroCuenta(NumeroCuenta);
                CuentaAhorroVistaDTO _cuentaAhorroVista = FacadeCuentas.obtenerCuentaAhorroVistaNumeroCuenta(_cuentaAhorroVistaDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _cuentaAhorroVista);
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

        //GET cuenta/ahorrovista?Cedula=valor
        //Obtener una cuenta ahorro vista dada una cedula
        public HttpResponseMessage GetObtenerCuentaAhorroVistaCedula(string Cedula = "")
        {
            try
            {
                ClientVDTO _clienteVDTO = new ClientVDTO();
                _clienteVDTO.setIDCard(Cedula);
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
                _cuentaAhorroVistaDTO.setCliente(_clienteVDTO);
                List<CuentaAhorroVistaDTO> _cuentaAhorroVistaList = FacadeCuentas.obtenerCuentaAhorroVistaCedula(_cuentaAhorroVistaDTO);
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

        //GET cuenta/ahorrovista?CIF=valor
        //Obtener una cuenta ahorro vista dado un CIF
        public HttpResponseMessage GetObtenerCuentaAhorroVistaCIF(string CIF = "")
        {
            try
            {
                ClientVDTO _clienteVDTO = new ClientVDTO();
                _clienteVDTO.setCIF(CIF);
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
                _cuentaAhorroVistaDTO.setCliente(_clienteVDTO);
                List<CuentaAhorroVistaDTO> _cuentaAhorroVistaList = FacadeCuentas.obtenerCuentaAhorroVistaCIF(_cuentaAhorroVistaDTO);
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

        //PUT cuenta/ahorrovista
        //Modifica una cuenta ahorro vista
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

        //DELETE cuenta/ahorrovista?NumeroCuenta=valor
        //Borra una cuenta ahorro vista dado el numero de cuenta
        public HttpResponseMessage PutModificarCuentaAhorroVista(string NumeroCuenta = "")
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
                _cuentaAhorroVistaDTO.setNumeroCuenta(NumeroCuenta);
                FacadeCuentas.eliminarCuentaAhorroVista(_cuentaAhorroVistaDTO);
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