using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using FlexCoreRest.Conversiones;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class FotoController : ApiController
    {
        //POST /persona/foto
        //Crea una nueva foto a una persona
        public HttpResponseMessage PostCrearFoto()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonPhotoDTO _personPhoto = TransformingObjects.deserializeObject<PersonPhotoDTO>(_datosPost);
                ClientsFacade.getInstance().updatePhoto(_personPhoto);
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

        //GET /persona/foto?Id=valor
        //Obtiene la foto de una persona
        public HttpResponseMessage GetObtenerFoto(string Id = "")
        {
            try
            {
                PersonDTO _personPhotoDTO = new PersonDTO(Convert.ToInt32(Id));
                PersonPhotoDTO _photoPerson = ClientsFacade.getInstance().getPhoto(_personPhotoDTO);
                string _photoPersonSerializada = TransformingObjects.serializeObejct<PersonPhotoDTO>(_photoPerson);
                HttpResponseMessage _request = new HttpResponseMessage(HttpStatusCode.OK);
                _request.Content = new StringContent(_photoPersonSerializada, Encoding.UTF8, "text/plain");
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

        //PUT /persona/foto
        //Modifica una foto asociada a una persona
        public HttpResponseMessage PutModificarFoto()
        {
            try
            {
                string _datosPost = Request.Content.ReadAsStringAsync().Result;
                PersonPhotoDTO _personPhoto = TransformingObjects.deserializeObject<PersonPhotoDTO>(_datosPost);
                ClientsFacade.getInstance().updatePhoto(_personPhoto);
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
