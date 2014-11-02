using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Windows.Forms;

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
                MessageBox.Show("Entre al post XD");
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                MessageBox.Show("Lei el post XD, HEX: " + _datosPost);
                byte[] _physicalPersonByte = TransformingObjects.ConvertHexToBytes(_datosPost);
                MessageBox.Show("Converti de bytes a hex");
                PhysicalPersonDTO _physicalPerson = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonByte);
                MessageBox.Show(_physicalPerson.getName());
                ClientsFacade.getInstance().insertPhysicalPerson(_physicalPerson);
                MessageBox.Show("Agregue el objeto a la base");
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

        //PUT /persona/fisica?Anterior=valor&Nueva=valor
        //Modifica una persona fisica
        public HttpResponseMessage PutModificarPersonaFisica(string Anterior, string Nueva)
        {
            try
            {
                byte[] _physicalPersonDTOByteAnterior = TransformingObjects.ConvertHexToBytes(Anterior);
                byte[] _physicalPersonDTOByteNueva = TransformingObjects.ConvertHexToBytes(Nueva);
                PhysicalPersonDTO _physicalPersonDTOAnterior = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonDTOByteAnterior);
                PhysicalPersonDTO _physicalPersonDTONueva = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonDTOByteNueva);
                ClientsFacade.getInstance().updatePhysicalPerson(_physicalPersonDTONueva, _physicalPersonDTOAnterior);
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

        //DELETE /persona/fisica?Borrado=valor
        //Borra una persona fisica de la base de datos
        public HttpResponseMessage DeleteBorrarPersonaFisica(string Borrado)
        {
            try
            {
                byte[] _physicalPersonDTOByte = TransformingObjects.ConvertHexToBytes(Borrado);
                PhysicalPersonDTO _physicalPersonDTO = (PhysicalPersonDTO)TransformingObjects.ByteArrayToObject(_physicalPersonDTOByte);
                ClientsFacade.getInstance().deletePhysicalPerson(_physicalPersonDTO);
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
    }
}
