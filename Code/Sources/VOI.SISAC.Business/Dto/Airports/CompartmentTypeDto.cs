//------------------------------------------------------------------------
// <copyright file="CompartmentTypeDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// CompartmentType Data Transfer Object
    /// </summary>
    public partial class CompartmentTypeDto
    {

        /// <summary>
        /// Gets or sets the compartment type code.
        /// </summary>
        /// <value>
        /// The compartment type code.
        /// </value>
        [Required] 
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompartmentTypeDto"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplane types.
        /// Relationship with AirplaneType.
        /// </summary>
        /// <value>
        /// The airplane types.
        /// </value>
        public IList<AirplaneTypeDto> AirplaneTypes { get; set; }

        /////// <summary>
        /////// Gets or sets the compartment type configs.
        /////// Relationship with CompartmentTypeConfig.
        /////// </summary>
        /////// <value>
        /////// The compartment type configs.
        /////// </value>
        ////public virtual ICollection<CompartmentTypeConfig> CompartmentTypeConfigs { get; set; }

        /////// <summary>
        /////// Gets or sets the compartment type informations.
        /////// Relationship with CompartmentTypeInformation
        /////// </summary>
        /////// <value>
        /////// The compartment type informations.
        /////// </value>
        ////public virtual ICollection<CompartmentTypeInformation> CompartmentTypeInformations { get; set; }
    }
}