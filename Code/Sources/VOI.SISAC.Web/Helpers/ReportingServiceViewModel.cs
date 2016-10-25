//------------------------------------------------------------------------
// <copyright file="ReportingServiceViewModel.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using Microsoft.Reporting.WebForms;
    using System.Security.Policy;

    /// <summary>
    /// Reporting Service View Model
    /// </summary>
    public class ReportingServiceViewModel
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingServiceViewModel"/> class.
        /// </summary>
        /// <param name="reportPath">The report path.</param>
        /// <param name="Parameters">The parameters.</param>
        public ReportingServiceViewModel(String reportPath,List<ReportParameter> Parameters)
        {
            ReportPath = reportPath;
            parameters = Parameters.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingServiceViewModel"/> class.
        /// </summary>
        public ReportingServiceViewModel()
        {           
        }
        #endregion Constructor
 
        #region Public Properties
        /// <summary>
        /// Gets the server credentials.
        /// </summary>
        /// <value>
        /// The server credentials.
        /// </value>
        public ReportServerCredentials ServerCredentials { get { return new ReportServerCredentials(); } }

        /// <summary>
        /// Gets or sets the report path.
        /// </summary>
        /// <value>
        /// The report path.
        /// </value>
        public String ReportPath { get; set; }

        /// <summary>
        /// Gets the report server URL.
        /// </summary>
        /// <value>
        /// The report server URL.
        /// </value>
        public Uri ReportServerURL { get { return new Uri(WebConfigurationManager.AppSettings["ReportServerUrl"]); } }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ReportParameter[] parameters { get; set; }

        /// <summary>
        /// Gets or sets the page where the reporte has called.
        /// </summary>
        /// <value>
        /// The page source URL.
        /// </value>
        public string PageSourceUrl { get; set; }

        /// <summary>
        /// The upload directory
        /// </summary>
        private string UploadDirectory = HttpContext.Current.Server.MapPath("~/App_Data/UploadTemp/");

        /// <summary>
        /// The temporary directory
        /// </summary>
        private string TempDirectory = HttpContext.Current.Server.MapPath("~/tempFiles/");
        #endregion
    }
}