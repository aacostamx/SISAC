//------------------------------------------------------------------------
// <copyright file="PeriodVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Process code view object
    /// </summary>
    public class PeriodVO
    {
        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        [Display(Name = "Period", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the start date period.
        /// </summary>
        /// <value>
        /// The start date period.
        /// </value>
        public DateTime StartDatePeriod { get; set; }

        /// <summary>
        /// Gets or sets the end date period.
        /// </summary>
        /// <value>
        /// The end date period.
        /// </value>
        public DateTime EndDatePeriod { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status code.
        /// </summary>
        /// <value>
        /// The confirmation status code.
        /// </value>
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation date.
        /// </summary>
        /// <value>
        /// The confirmation date.
        /// </value>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets the start date to string.
        /// </summary>
        /// <value>
        /// The start date to string.
        /// </value>
        public string StartDateToString
        {
            get
            {
                return this.StartDatePeriod.ToShortDateString().ToString(CultureInfo.CurrentUICulture);
            }
        }

        /// <summary>
        /// Gets the end date to string.
        /// </summary>
        /// <value>
        /// The end date to string.
        /// </value>
        public string EndDateToString
        {
            get
            {
                return this.EndDatePeriod.ToShortDateString().ToString(CultureInfo.CurrentUICulture);
            }
        }

        /// <summary>
        /// Gets the confirmation date to string.
        /// </summary>
        /// <value>
        /// The confirmation date to string.
        /// </value>
        public string ConfirmationDateToString
        {
            get
            {
                if (this.ConfirmationDate == null) 
                { 
                    return string.Empty; 
                }
                else 
                { 
                    return this.ConfirmationDate.Value.ToShortDateString().ToString(CultureInfo.CurrentUICulture); 
                }
            }
        }
    }
}