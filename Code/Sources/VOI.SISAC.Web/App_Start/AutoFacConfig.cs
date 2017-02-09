//------------------------------------------------------------------------
// <copyright file="AutoFacConfig.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.App_Start
{    
    using System.Web.Mvc;
    using System.Web.Security;
    using Autofac;
    using Autofac.Integration.Mvc;
    using IoC = VOI.SISAC.Business.Configuration;
    using VOI.SISAC.Web.Services;

    /// <summary>
    /// Class that configure the dependency injector container
    /// </summary>
    public class AutoFacConfig
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            SetAutofacContainer();
        }

        /// <summary>
        /// Sets the container.
        /// </summary>
        private static void SetAutofacContainer()
        {
            // Create an instance of ContainerBuilder class
            ContainerBuilder builder = new ContainerBuilder();

            // Register all Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            // Register all Module in this case ModuleBusiness
            builder.RegisterModule(new IoC.AutofacModuleConfig());

            // Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register the services for policies
            builder.RegisterType<PolicyService>().As<IPolicyService>();

            // Build the Container
            IContainer container = builder.Build();            

            // Resolve all dependency
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
