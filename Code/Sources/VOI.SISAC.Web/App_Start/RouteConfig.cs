//------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------
namespace VOI.SISAC.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Clase que configura el ruteo del sitio web
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registra que se utilizará en el sitio web
        /// </summary>
        /// <param name="routes">Colección de rutas de ASP.NET</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Account",
                    action = "Login",
                    id = UrlParameter.Optional
                });

            /* Attribute-based Routing, where routes are defined using attributes 
             * on controller actions. Attribute-based routing is not enabled by 
             * default. You enable it by adding the highlighted line of code to 
             * the RegisterRoutes method */
            routes.MapMvcAttributeRoutes();
        }
    }
}
