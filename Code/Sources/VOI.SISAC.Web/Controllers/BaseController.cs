//------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Controllers
{
    using System;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Begins to invoke the action in the current controller context.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// Returns an IAsyncController instance.
        /// </returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            this.Response.Cache.SetNoStore();

            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                // obtain it from HTTP header AcceptLanguages
                cultureName = Request.UserLanguages != null &&
                    Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }
    }
}