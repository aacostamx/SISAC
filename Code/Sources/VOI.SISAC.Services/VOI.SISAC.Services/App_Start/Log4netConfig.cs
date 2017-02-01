//------------------------------------------------------------------------
// <copyright file="Log4netConfig.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace VOI.SISAC.Services.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Log4netConfig class
    /// </summary>
    public partial class Log4netConfig
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            SetConfiguration();
        }

        /// <summary>
        /// Sets configuration for log4net.
        /// </summary>
        private static void SetConfiguration()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}