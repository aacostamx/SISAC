//------------------------------------------------------------------------
// <copyright file="AirportServiceContractController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Contract Parameters View Object
    /// </summary>
    public class ContractParametersVO
    {
        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        [Display(Name = "EffectiveDate", ResourceType = typeof(Resources.Resource))]
        public DateTime EffectiveDateParameter { get; set; }

        /// <summary>
        /// Gets or sets the airline description.
        /// </summary>
        /// <value>
        /// The airline description.
        /// </value>
        [Display(Name = "Airline", ResourceType = typeof(Resources.Resource))]
        public string AirlineDescription { get; set; }

        /// <summary>
        /// Gets or sets the airport description.
        /// </summary>
        /// <value>
        /// The airport description.
        /// </value>
        [Display(Name = "Airport", ResourceType = typeof(Resources.Resource))]
        public string AirportDescription { get; set; }

        /// <summary>
        /// Gets or sets the service description.
        /// </summary>
        /// <value>
        /// The service description.
        /// </value>
        [Display(Name = "Service", ResourceType = typeof(Resources.Resource))]
        public string ServiceDescription { get; set; }

        /// <summary>
        /// Gets or sets the provider description.
        /// </summary>
        /// <value>
        /// The provider description.
        /// </value>
        [Display(Name = "Provider", ResourceType = typeof(Resources.Resource))]
        public string ProviderDescription { get; set; }

        /// <summary>
        /// Gets or sets the cost center description.
        /// </summary>
        /// <value>
        /// The cost center description.
        /// </value>
        [Display(Name = "CostCenter", ResourceType = typeof(Resources.Resource))]
        public string CostCenterNumber { get; set; }
    }
}