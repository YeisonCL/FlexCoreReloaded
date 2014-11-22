using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreLogic.principalogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class AltoVolumenDeDatosController : ApiController
    {
        //GET /sistema/altovolumendedatos?Dia=valor&Mes=valor&Anio=valor&Hora=valor&Minuto=valor&Segundo=valor
        //Inicia la insercion del alto volumen de datos
        public HttpResponseMessage GetIniciarInsercionDeAltoVolumenDeDatos(string Dia, string Mes, string Anio, string Hora, string Minuto, string Segundo)
        {
            try
            {
                DateTime _horaInicio = new DateTime(Convert.ToInt32(Anio), Convert.ToInt32(Mes), Convert.ToInt32(Dia), Convert.ToInt32(Hora), Convert.ToInt32(Minuto),
                    Convert.ToInt32(Segundo));
                AltoVolumenDeDatos.iniciarAltoVolumenDeDatos(_horaInicio);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("Iniciada La Insercion De Datos", Encoding.UTF8, "text/plain");
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

        //GET /sistema/altovolumendedatos?Estado=True
        //Obtiene el estado de la insercion de datos
        public HttpResponseMessage GetObtenerEstadoDeInsercion(string Estado)
        {
            try
            {
                int estadoInsercion = AltoVolumenDeDatos.consultarEstadoDeInsercion();
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(estadoInsercion.ToString(), Encoding.UTF8, "text/plain");
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

        //GET /sistema/altovolumendedatos?CantidadDeDatos=True
        //Obtiene la cantidad de datos total a insertar
        public HttpResponseMessage GetObtenerCantidadDeDatosAInsertar(string CantidadDeDatos)
        {
            try
            {
                int cantidadDeDatos = AltoVolumenDeDatos.consultarCantidadDeDatosAInsertar();
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(cantidadDeDatos.ToString(), Encoding.UTF8, "text/plain");
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

        ////////
        //GET /sistema/altovolumendedatos?CantidadDeDatos=True
        //Obtiene la cantidad de datos total a insertar
        public HttpResponseMessage GetMagico(string Conexiones)
        {
            try
            {
                int conexiones = SQLServerManager.numeroConexiones - SQLServerManager._listaDeConexiones.Count;
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(conexiones.ToString(), Encoding.UTF8, "text/plain");
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
        ////////

        //POST /sistema/altovolumendedatos
        //Inicia la insercion de datos o la detiene
        public HttpResponseMessage PostCrearAltoVolumenDeDatos()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                if (_datosPost == "cancelarInsercion")
                {
                    AltoVolumenDeDatos.cancelarInsercion();
                }
                else
                {
                    //Se inicia la insercion de datos
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
