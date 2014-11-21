using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using FlexCoreLogic.clients;
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
    public class ClienteController : ApiController
    {
        //GET /cliente?NumeroPagina=valor&CantidadMostrar=valor&Ordenamiento=valor&Todos=TRUE
        //Obtiene todos los clientes
        public HttpResponseMessage GetObtenerTodosClientes(string Todos, string NumeroPagina = "0", string CantidadMostrar = "0", string Ordenamiento = "")
        {
            try
            {
                SearchResultDTO<ClientVDTO> _clientList = ClientsFacade.getInstance().getAllClient(Convert.ToInt32(NumeroPagina), Convert.ToInt32(CantidadMostrar),
                    Ordenamiento);
                string _clientesSerializados = TransformingObjects.serializeObejct<SearchResultDTO<ClientVDTO>>(_clientList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_clientesSerializados, Encoding.UTF8, "text/plain");
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

        //GET /cliente?CIF=Valor&Cedula=Valor&Nombre=Valor&PrimerApellido=Valor&SegundoApellido=Valor&Tipo
        //Obtiene un ClientVDTO
        public HttpResponseMessage GetObtenerClienteVDTO(string CIF = "", string Cedula = "", string Nombre = "", string PrimerApellido = "", 
            string SegundoApellido = "", string Tipo = "")
        {
            try
            {
                ClientVDTO _clienteVDTO = new ClientVDTO(Nombre, PrimerApellido, SegundoApellido, Cedula, CIF, Tipo);
                SearchResultDTO<ClientVDTO> _clientVDTOBuscado = ClientsFacade.getInstance().searchClient(_clienteVDTO);
                string _clienteSerializado = TransformingObjects.serializeObejct<SearchResultDTO<ClientVDTO>>(_clientVDTOBuscado);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_clienteSerializado, Encoding.UTF8, "text/plain");
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

        //GET /cliente?IdCliente=Valor
        //Obtiene un ClientDTO
        public HttpResponseMessage GetObtenerCliente(string IdCliente)
        {
            try
            {
                ClientDTO _clienteDTO = new ClientDTO(Convert.ToInt32(IdCliente));
                ClientDTO _clientDTOBuscado = ClientsFacade.getInstance().getClientByID(_clienteDTO);
                string _clienteSerializado = TransformingObjects.serializeObejct<ClientDTO>(_clientDTOBuscado);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_clienteSerializado, Encoding.UTF8, "text/plain");
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

        //POST /cliente
        //Crea un nuevo cliente
        public HttpResponseMessage PostCrearCliente()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonDTO _clienteDTO = TransformingObjects.deserializeObject<PersonDTO>(_datosPost);
                ClientsFacade.getInstance().insertClient(_clienteDTO);
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

        //PUT /cliente
        //Modifica el estado de un cliente
        public HttpResponseMessage PutModificarEstadoCliente()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                ClientDTO _clientDTO = TransformingObjects.deserializeObject<ClientDTO>(_datosPut);
                ClientsFacade.getInstance().setClientActiveStatus(_clientDTO);
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

        //DELETE /cliente?IdCliente=Valor
        //Borra un cliente
        public HttpResponseMessage DeleteCliente(string IdCliente)
        {
            try
            {
                ClientDTO _clienteDTO = new ClientDTO(IdCliente);
                ClientsFacade.getInstance().deleteClient(_clienteDTO);
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
