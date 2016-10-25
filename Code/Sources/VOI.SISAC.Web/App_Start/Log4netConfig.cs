using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]   
namespace VOI.SISAC.Web.App_Start
{
    /// <summary>
    /// Configure log4net
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
            //log4net.Config.XmlConfigurator.Configure(
            //    new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}