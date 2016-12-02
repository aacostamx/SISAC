//------------------------------------------------------------------------
// <copyright file="AjaxRequestAttribute.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers.CustomAttributes
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Ajax Request Attribute Class
    /// </summary>
    /// <seealso cref="System.Web.Mvc.ActionMethodSelectorAttribute" />
    public class AjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Determines whether the action method selection is valid for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="methodInfo">Information about the action method.</param>
        /// <returns>
        /// true if the action method selection is valid for the specified controller context; otherwise, false.
        /// </returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}