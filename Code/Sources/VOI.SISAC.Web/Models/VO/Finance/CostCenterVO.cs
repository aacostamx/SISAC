//------------------------------------------------------------------------
// <copyright file="CostCenterVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VOI.SISAC.Web.Models.VO.Airport;
namespace VOI.SISAC.Web.Models.VO.Finance
{
    /// <summary>
    /// Class Cost Center VO
    /// </summary>
    public class CostCenterVO
    {
        /// <summary>
        /// Gets or sets the cc number.
        /// </summary>
        /// <value>
        /// The cc number.
        /// </value>
        [Display(Name = "CCNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(14, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax14")]
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the cc.
        /// </summary>
        /// <value>
        /// The name of the cc.
        /// </value>
        [Display(Name = "CCName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string CCName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CostCenterVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public AirlineVO Airline { get; set; }

        /// <summary>
        /// InternationalFuelContractVO
        /// </summary>
        public ICollection<InternationalFuelContractVO> InternationalFuelContract { get; set; }
    }
}