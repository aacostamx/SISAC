// -----------------------------------------------------------------------
// <copyright file="Global.cs" company="Volaris">
// Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
// -----------------------------------------------------------------------

namespace VOI.SISAC.Services
{
    using Autofac;
    using Autofac.Integration.Wcf;
    using System;
    using VOI.SISAC.Services.Common;
    using Service = VOI.SISAC.Services.App_Start;
   

    /// <summary>
    /// Global class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// Application_Start method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // build and set container in application start
            // IContainer container = AutoFacConfig.BuildContainer();
            // AutofacHostFactory.Container = container;
            AutofacConfiguration.Run();

            // Automapper Configuration for web layer
            Service.MapperConfig.RegisterMappings();
        }

        /// <summary>
        /// Session_Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application_BeginRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application_AuthenticateRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application_Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Session_End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application_End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}