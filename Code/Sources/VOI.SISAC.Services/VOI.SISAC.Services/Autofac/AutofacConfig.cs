//------------------------------------------------------------------------
// <copyright file="AutofacConfig.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.Autofac
{
    using global::Autofac;
    using global::Autofac.Integration.WebApi;
    using System.Reflection;
    using System.Web.Http;


    /// <summary>
    /// AutofacConfig class
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Base set-up
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Register dependencies
            builder.RegisterModule(new VOI.SISAC.Business.Configuration.AutofacModuleConfig());

            // Build registration.
            var container = builder.Build();

            // Set the dependency resolver to be Autofac.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}