//------------------------------------------------------------------------
// <copyright file="InternationalFuelRateVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;

    /// <summary>
    /// International Fuel Rate View Object
    /// </summary>
    public class InternationalFuelRateVO
    {
        /// <summary>
        /// Gets or sets International Fuel Rate Identifier
        /// </summary>
        public long InternationalFuelRateID { get; set; }

        /// <summary>
        /// Gets or sets International Fuel Contract Concept Identifier
        /// </summary>
        public long InternationalFuelContractConceptID { get; set; }

        /// <summary>
        /// Gets or sets the Rate Start Date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Rate Start Date.
        /// </value>
        [Display(Name = "RateStartDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime RateStartDate { get; set; }

        /// <summary>
        /// Gets or sets the Rate End Date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Rate End Date.
        /// </value>
        [Display(Name = "RateEndDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime RateEndDate { get; set; }

        /// <summary>
        /// Gets or sets the Rate.
        /// </summary>
        /// <value>
        /// The Rate.
        /// </value>        
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,##0.0000#}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,5})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets a list of International Fuel Contract Concepts
        /// </summary>
        public InternationalFuelContractConceptVO InternationalFuelContractConcept { get; set; }
    }
}