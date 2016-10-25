//------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models;
    using Web = VOI.SISAC.Web.App_Start;

    /// <summary>
    /// Initialize the web project
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// executes custom initialization code after all event handler modules have been added.
        /// </summary>
        public override void Init()
        {
            this.PostAuthenticateRequest += new EventHandler(this.ApplicationPostAuthenticateRequest);
        }

        /// <summary>
        /// Stars the application, register the areas, routes and bundles.
        /// Also loads the Dependency Injection container configuration
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac configurations
            Web.AutoFacConfig.Run();

            // log4net configuration
            Web.Log4netConfig.Run();

            // Automapper Configuration for web layer
            Web.MapperConfig.RegisterMappings();
        }

        /// <summary>
        /// Occurs when a security module has established the identity of the user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArguments">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationPostAuthenticateRequest(object sender, EventArgs eventArguments)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!string.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
                    UserIdentity id = new UserIdentity(ticket);
                    UserData userPermissions = GetUserPermissions();
                    string[] permission = userPermissions.Roles != null ? userPermissions.Roles : new string[0];
                    GenericPrincipal principal = new GenericPrincipal(id, permission);
                    HttpContext.Current.User = principal;
                }
            }
        }

        /// <summary>
        /// Gets the user permissions.
        /// </summary>
        /// <returns></returns>
        private static UserData GetUserPermissions()
        {
            UserData userPermissions = new UserData();

            if (HttpContext.Current.Request.Cookies["UserPermissions"] != null)
            {
                HttpCookie userCookie = HttpContext.Current.Request.Cookies["UserPermissions"];
                string decrypt = EncryptHelper.DecryptString(userCookie.Value);
                string decompress = CompressHelper.Decompress(decrypt);
                userPermissions = JsonConvert.DeserializeObject<UserData>(decompress);
            }

            return userPermissions;
        }
    }
}
