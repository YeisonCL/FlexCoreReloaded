using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class PersonaJuridicaController : ApiController
    {
        //POST /persona/juridica
        //Crea una nueva persona juridica
        public HttpResponseMessage PostCrearPersonaJuridica()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonDTO _juridicalPerson = TransformingObjects.deserializeObject<PersonDTO>(_datosPost);
                int _idInsertado = ClientsFacade.getInstance().newJuridicalPerson(_juridicalPerson);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_idInsertado.ToString(), Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent("False", Encoding.UTF8, "text/plain"); ;
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //GET /persona/juridica?IdPersona=valor&Nombre=valor&Cedula=valor&NumeroPagina=valor&CantidadMostrar=valor&Ordenamiento=valor
        //Obtener una persona juridica
        public HttpResponseMessage GetObtenerPersonaJuridica(string IdPersona, string Nombre = "", string Cedula = "", string NumeroPagina = "0", string CantidadMostrar = "0", 
            string Ordenamiento = "")
        {
            try
            {
                PersonDTO _juridicalPersonDTO = new PersonDTO();
                _juridicalPersonDTO.setPersonID(Convert.ToInt32(IdPersona));
                _juridicalPersonDTO.setName(Nombre);
                _juridicalPersonDTO.setIDCard(Cedula);
                List<PersonDTO> _juridicalPersonList = ClientsFacade.getInstance().searchJuridicalPerson(_juridicalPersonDTO, Convert.ToInt32(NumeroPagina),
                    Convert.ToInt32(CantidadMostrar), Ordenamiento);
                string _juridicalPersonListSerializada = TransformingObjects.serializeObejct<List<PersonDTO>>(_juridicalPersonList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_juridicalPersonListSerializada, Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
            catch(Exception e)
            {
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(e.Message, Encoding.UTF8, "text/plain");
                _request.Headers.Add("Access-Control-Allow-Origin", "*");
                return _request;
            }
        }

        //PUT /persona/juridica
        //Modifica una persona juridica
        public HttpResponseMessage PutModificarPersonaJuridica()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                UpdateDTO<PersonDTO> _juridicalPersonList = TransformingObjects.deserializeObject<UpdateDTO<PersonDTO>>(_datosPut);
                PersonDTO _juridicalPersonDTOAnterior = _juridicalPersonList._previous;
                PersonDTO _juridicalPersonDTONueva = _juridicalPersonList._new;
                ClientsFacade.getInstance().updateJuridicalPerson(_juridicalPersonDTONueva, _juridicalPersonDTOAnterior);
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

        //DELETE /persona/juridica?IdPersona=valor
        //Borra una persona juridica de la base de datos
        public HttpResponseMessage DeleteBorrarPersonaJuridica(string IdPersona)
        {
            try
            {
                PersonDTO _juridicalPersonDTO = new PersonDTO(Convert.ToInt32(IdPersona));
                ClientsFacade.getInstance().deleteJuridicalPerson(_juridicalPersonDTO);
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
