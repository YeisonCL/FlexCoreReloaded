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
    public class PersonaFisicaController : ApiController
    {
        //POST /persona/fisica
        //Crea una nueva persona fisica
        public HttpResponseMessage PostCrearPersonaFisica()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PhysicalPersonDTO _physicalPerson = TransformingObjects.deserializeObject<PhysicalPersonDTO>(_datosPost);
                int _idInsertado = ClientsFacade.getInstance().insertPhysicalPerson(_physicalPerson);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_idInsertado.ToString(), Encoding.UTF8, "text/plain");
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

        //GET /persona/fisica?IdPersona=valor&Nombre=valor&PrimerApellido=valor&SegundoApellido=valor&Cedula=valor&NumeroPagina=valor&CantidadMostrar=valor&Ordenamiento=valor
        //Obtener una persona fisica
        public HttpResponseMessage GetObtenerPersonaFisica(string IdPersona, string Nombre = "", string PrimerApellido = "", string SegundoApellido = "", 
            string Cedula = "", string NumeroPagina = "0", string CantidadMostrar = "0", string Ordenamiento = "")
        {
            try
            {
                PhysicalPersonDTO _physicalPersonDTO = new PhysicalPersonDTO();
                _physicalPersonDTO.setName(Nombre);
                _physicalPersonDTO.setFirstLastName(PrimerApellido);
                _physicalPersonDTO.setSecondLastName(SegundoApellido);
                _physicalPersonDTO.setIDCard(Cedula);
                _physicalPersonDTO.setPersonID(Convert.ToInt32(IdPersona));
                List<PhysicalPersonDTO> _physicalPersonList = ClientsFacade.getInstance().searchPhysicalPerson(_physicalPersonDTO, Convert.ToInt32(NumeroPagina), 
                    Convert.ToInt32(CantidadMostrar), Ordenamiento);
                string _physicalPersonListSerializada = TransformingObjects.serializeObejct<List<PhysicalPersonDTO>>(_physicalPersonList);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_physicalPersonListSerializada, Encoding.UTF8, "text/plain");
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

        //PUT /persona/fisica
        //Modifica una persona fisica
        public HttpResponseMessage PutModificarPersonaFisica()
        {
            try
            {
                string _datosPut = Request.Content.ReadAsStringAsync().Result;
                UpdateDTO<PhysicalPersonDTO> _physicalPersonList = TransformingObjects.deserializeObject<UpdateDTO<PhysicalPersonDTO>>(_datosPut);
                PhysicalPersonDTO _physicalPersonDTOAnterior = _physicalPersonList._previous;
                PhysicalPersonDTO _physicalPersonDTONueva = _physicalPersonList._new;
                ClientsFacade.getInstance().updatePhysicalPerson(_physicalPersonDTONueva, _physicalPersonDTOAnterior);
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

        //DELETE /persona/fisica?IdPersona=valor
        //Borra una persona fisica de la base de datos
        public HttpResponseMessage DeleteBorrarPersonaFisica(string IdPersona)
        {
            try
            {
                PhysicalPersonDTO _physicalPersonDTO = new PhysicalPersonDTO(Convert.ToInt32(IdPersona));
                ClientsFacade.getInstance().deletePhysicalPerson(_physicalPersonDTO);
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
