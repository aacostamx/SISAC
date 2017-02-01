//------------------------------------------------------------------------
// <copyright file="AutofacModuleConfig.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Configuration
{
    using System.Reflection;
    using Autofac;
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// Class than register
    /// </summary>
    public class AutofacModuleConfig : Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
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

            // Register the interface and the class 
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();
        }
    }
}
