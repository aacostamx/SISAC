//------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Controllers
{
    using MvcSiteMapProvider;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        /// <summary>
        /// Home page index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Unauthorized page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {
            return this.View();
        }
    }
}