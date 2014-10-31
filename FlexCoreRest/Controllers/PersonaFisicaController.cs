using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                byte[] _physicalPersonByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                PhysicalPersonDTO _physicalPerson = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonByte);
                ClientsFacade.getInstance().newPhysicalPerson(_physicalPerson);
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

        //GET /persona/fisica?Busqueda=valor
        //Obtener una persona fisica
        public HttpResponseMessage GetObtenerPersonaFisica(string Busqueda)
        {
            try
            {
                byte[] _physicalPersonDTOByte = TransformingObjects.ConvertHexToBytes(Busqueda);
                PhysicalPersonDTO _physicalPersonDTO = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonDTOByte);
                List<PhysicalPersonDTO> _physicalPersonList = ClientsFacade.getInstance().searchPhysicalPerson(_physicalPersonDTO);
                byte[] _physicalPersonListByte = TransformingObjects.ObjectToByteArray(_physicalPersonList);
                string _physicalPersonListHex = BitConverter.ToString(_physicalPersonListByte);
                _physicalPersonListHex = _physicalPersonListHex.Replace("-", "");
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _physicalPersonListHex);
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
    }
}
