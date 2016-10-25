//------------------------------------------------------------------------
// <copyright file="TimelineController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using Business.Itineraries;
    using Resources;
    using System;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// TimelineController class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class TimelineController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(TimelineController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.Timeline;

        /// <summary>
        /// The timeline business
        /// </summary>
        private readonly ITimelineBusiness timelineBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineController"/> class.
        /// </summary>
        /// <param name="timelineBusiness">The timeline business.</param>
        public TimelineController(ITimelineBusiness timelineBusiness)
        {
            this.userInfo = string.Format(LogMessages.UserInfo, Environment.UserDomainName, Environment.UserName, Environment.MachineName);
            this.timelineBusiness = timelineBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "TIMELINE-IDX")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Views the timeline.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "TIMELINE-VIE")]
        public ActionResult ViewTimeline()
        {
            return View();
        }
    }
}