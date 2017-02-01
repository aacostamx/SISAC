//------------------------------------------------------------------------
// <copyright file="ProviderVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Provider View Objet
    /// </summary>
    public class ProviderVO
    {
        /// <summary>
        /// Gets or Set the Provider Number
        /// </summary>
        [Display(Name = "ProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required (ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax10")]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or Set the Provider Name
        /// </summary>
        [Display(Name = "ProviderName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "ProviderNameMaxLong")]        
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or Set the Provider Short Name
        /// </summary>
        [Display(Name = "ShortNameProvider", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(15, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "ShortNameProviderMaxLong")]
        public string ProviderShortName { get; set; }

        /// <summary>
        /// Gets or Set the status of the provider
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// AirportServiceContracts
        /// </summary>
        public ICollection<AirportServiceContractVO> AirportServiceContracts { get; set; }

        /// <summary>
        /// InternationalFuelContracts
        /// </summary>
        public ICollection<InternationalFuelContractVO> InternationalFuelContracts { get; set; }

        /// <summary>
        /// InternationalFuelContractConcepts
        /// </summary>
        public ICollection<InternationalFuelContractConceptVO> InternationalFuelContractConcepts { get; set; }

    }
}
