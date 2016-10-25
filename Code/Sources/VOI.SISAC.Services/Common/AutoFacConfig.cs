// -----------------------------------------------------------------------
// <copyright file="AutoFacConfig.cs" company="Volaris">
// Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
// -----------------------------------------------------------------------

namespace VOI.SISAC.Services.Common
{
    using Autofac;
    using System.Reflection;
    using System.ServiceModel;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Itineraries;

    /// <summary>
    /// AutoFacConfig class
    /// </summary>
    public class AutoFacConfig
    {
        /*
         * TODO: Delete this class if the class AutofacConfiguration works.
         */

        /// <summary>
        /// Configures and builds Autofac IOC container.
        /// </summary>
        /// <returns></returns>
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
    
            builder.RegisterType<VOI.SISAC.Services.ServiceOpenManifestServices>()
                .As<VOI.SISAC.Services.IServiceOpenManifestServices>();

            // Register the project Business
            builder.RegisterAssemblyTypes(Assembly.Load("VOI.SISAC.Business"))
                .Where(t => t.Name.EndsWith("Business", System.StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register the project Dal
            builder.RegisterAssemblyTypes(Assembly.Load("VOI.SISAC.Dal"))
              .Where(t => t.Name.EndsWith("Repository", System.StringComparison.Ordinal))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();

            builder.RegisterType<DbFactory>()
            .As<IDbFactory>();

            // build container
            return builder.Build();

           
        }
    }
}