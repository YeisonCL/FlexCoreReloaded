﻿using FlexCoreDTOs.clients;
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

        //GET /persona/foto?Id=valor
        //Obtiene la foto de una persona
        public HttpResponseMessage GetObtenerFoto(string Id = "")
        {
            try
            {
                PersonDTO _personPhotoDTO = new PersonDTO(Convert.ToInt32(Id));
                PersonPhotoDTO _photoPerson = ClientsFacade.getInstance().getPhoto(_personPhotoDTO);
                HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _photoPerson);
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

        //FALTA HACER EL PUT DE FOTO
    }
}
