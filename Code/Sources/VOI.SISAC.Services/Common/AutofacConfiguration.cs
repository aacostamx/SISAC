//------------------------------------------------------------------------
// <copyright file="AutofacConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Services.Common
{
    using Autofac;
    using Autofac.Integration.Wcf;
    using VOI.SISAC.Business.Configuration;

    /// <summary>
    /// Dependency injection configuration class
    /// </summary>
    public class AutofacConfiguration
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

            builder.RegisterType<VOI.SISAC.Services.ServiceOpenManifestServices>()
                .As<VOI.SISAC.Services.IServiceOpenManifestServices>();

            // Register all Module in this case ModuleBusiness
            builder.RegisterModule(new AutofacModuleConfig());

            // Build the Container
            IContainer container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}