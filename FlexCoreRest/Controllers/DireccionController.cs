using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
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
    public class DireccionController : ApiController
    {
        //POST /persona/direccion
        //Crea una nueva direccion a una persona
        public HttpResponseMessage PostCrearDireccion()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonAddressDTO _personAdress = TransformingObjects.deserializeObject<PersonAddressDTO>(_datosPost);
                ClientsFacade.getInstance().addAddress(_personAdress);
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

        //GET /persona/direccion?Id=valor
        //Obtiene una nueva direccion
        public HttpResponseMessage GetObtenerDireccion(string Id = "")
        {
            try
            {
                PersonDTO _personDTO = new PhysicalPersonDTO();
                _personDTO.setPersonID(Convert.ToInt32(Id));
                List<PersonAddressDTO> _addressPersonList = ClientsFacade.getInstance().getAddress(_personDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _addressPersonList);
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

        //FALTA POR HACER EL PUT

        //DELETE /persona/direccion?Id=valor&Direccion=valor
        //Borra una direccion
        public HttpResponseMessage DeleteBorrarDireccion(string Id = "", string Direccion = "")
        {
            try
            {
                PersonAddressDTO _addressPersonDTO = new PersonAddressDTO(Convert.ToInt32(Id), Direccion);
                ClientsFacade.getInstance().deleteAddress(_addressPersonDTO);
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
