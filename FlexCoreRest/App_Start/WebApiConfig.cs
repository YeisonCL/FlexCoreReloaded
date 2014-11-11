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
                routeTemplate: "pagos/pagar",
                defaults: new { controller = "pagos" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaFisica",
                routeTemplate: "persona/fisica",
                defaults: new { controller = "personafisica" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaJuridica",
                routeTemplate: "persona/juridica",
                defaults: new { controller = "personajuridica" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaDocumento",
                routeTemplate: "persona/documento",
                defaults: new { controller = "documento" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaDireccion",
                routeTemplate: "persona/direccion",
                defaults: new { controller = "direccion" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaFoto",
                routeTemplate: "persona/foto",
                defaults: new { controller = "foto" }
            );

            config.Routes.MapHttpRoute(
                name: "PersonaTelefono",
                routeTemplate: "persona/telefono",
                defaults: new { controller = "telefono" }
            );

            config.Routes.MapHttpRoute(
                name: "CuentaAhorroVista",
                routeTemplate: "cuenta/ahorrovista",
                defaults: new { controller = "ahorrovista" }
            );

            config.Routes.MapHttpRoute(
                name: "CuentaAhorroAutomatico",
                routeTemplate: "cuenta/ahorroautomatico",
                defaults: new { controller = "ahorroautomatico" }
            );
        }
    }
}
