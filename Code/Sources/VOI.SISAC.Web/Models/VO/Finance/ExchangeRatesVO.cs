//------------------------------------------------------------------------
// <copyright file="ExchangeRatesVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ExchangeRatesVO Class
    /// </summary>
    public class ExchangeRatesVO
    {
        /// <summary>
        /// Year
        /// </summary>
        [Display(Name = "Year", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        [Display(Name = "Month", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int Month { get; set; }

        /// <summary>
        /// Exchange Rate Date
        /// </summary>
        [Display(Name = "ExchangeDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public DateTime ExchangeDate { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Average Exchange Rate
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        public decimal Rate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public CurrencyVO Currency { get; set; }
    }
}