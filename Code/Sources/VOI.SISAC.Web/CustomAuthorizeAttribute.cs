//------------------------------------------------------------------------
// <copyright file="CustomAuthorizeAttribute.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Custom authorize attribute
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Put here extra logic if is need it for authorize actions
            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">
        ///     Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. 
        ///     The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.
        /// </param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // If the user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
            }
            else if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // If the user is not allowed to e entar to an Ajax call
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = urlHelper.Action(
                            "Unauthorized", 
                            "Home", 
                            new { area = string.Empty })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else 
            {
                // If the user is not allowed to enter to a common action
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new 
                        { 
                            controller = "Home",
                            action = "Unauthorized",
                            area = string.Empty
                        }));
            }
        }
    }
}