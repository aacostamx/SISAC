//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Finance;


    /// <summary>
    /// ReconciliationToleranceVO class 
    /// </summary>
    public class ReconciliationToleranceVO
    {
        /// <summary>
        /// Currency Code
        /// </summary>
        [Display(Name = "ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [Display(Name = "ToleranceTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string ToleranceTypeCode { get; set; }

        /// <summary>
        /// Average Exchange Rate
        /// </summary>
        [Display(Name = "ToleranceValue", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public decimal ToleranceValue { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReconciliationToleranceVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }


        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ServiceVO Service { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public CurrencyVO Currency { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public ToleranceTypeVO ToleranceType { get; set; }
    }
}