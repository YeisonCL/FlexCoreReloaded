using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FlexCoreRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Pagos",
                routeTemplate: "pagar",
                defaults: new { controller = "pagos" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaFisica",
                routeTemplate: "personafisica",
                defaults: new { controller = "personafisica" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaJuridica",
                routeTemplate: "personajuridica",
                defaults: new { controller = "personajuridica" }
            );

            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "persona/creardocumento",
                defaults: new { controller = "documento" }
            );

            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "persona/creardireccion",
                defaults: new { controller = "direccion" }
            );

            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "persona/crearfoto",
                defaults: new { controller = "foto" }
            );

            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "persona/creartelefono",
                defaults: new { controller = "telefono" }
            );
        }
    }
}
