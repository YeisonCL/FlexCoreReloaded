using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using FlexCoreLogic.clients;
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

        //GET /persona/direccion?Id=valor
        //Obtiene una nueva direccion
        public HttpResponseMessage GetObtenerDireccion(string Id = "")
        {
            try
            {
                PersonDTO _personDTO = new PhysicalPersonDTO();
                _personDTO.setPersonID(Convert.ToInt32(Id));
                List<PersonAddressDTO> _addressPersonList = ClientsFacade.getInstance().getAddress(_personDTO);
                string _addressPersonListSerializada = TransformingObjects.serializeObejct<List<PersonAddressDTO>>(_addressPersonList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_addressPersonListSerializada, Encoding.UTF8, "text/plain");
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

        //PUT /persona/direccion
        //Modifica una direccion asociada a una persona
        public HttpResponseMessage PutModificarDireccion()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                UpdateDTO<PersonAddressDTO> _directionList = TransformingObjects.deserializeObject<UpdateDTO<PersonAddressDTO>>(_datosPut);
                PersonAddressDTO _direccionAnterior = _directionList._previous;
                PersonAddressDTO _direccionNueva = _directionList._new;
                ClientsFacade.getInstance().updateAddress(_direccionAnterior, _direccionNueva);
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

        //DELETE /persona/direccion?Id=valor&Direccion=valor
        //Borra una direccion
        public HttpResponseMessage DeleteBorrarDireccion(string Id = "", string Direccion = "")
        {
            try
            {
                PersonAddressDTO _addressPersonDTO = new PersonAddressDTO(Convert.ToInt32(Id), Direccion);
                ClientsFacade.getInstance().deleteAddress(_addressPersonDTO);
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
