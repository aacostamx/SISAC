//------------------------------------------------------------------------
// <copyright file="WebApiApplication.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services
{
    using App_Start;
    using Autofac;
    using AutoMapper;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;


    /// <summary>
    /// WebApiApplication class
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register(GlobalConfiguration.Configuration);
            MapperConfig.RegisterMappings();
            Log4netConfig.Run();
        }
    }
}
