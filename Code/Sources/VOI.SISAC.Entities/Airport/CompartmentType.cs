//------------------------------------------------------------------------
// <copyright file="CompartmentType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// CompartmentType Entity
    /// </summary>
    public partial class CompartmentType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentType"/> class.
        /// </summary>
        public CompartmentType()
        {
            this.AirplaneTypes = new HashSet<AirplaneType>();
            this.CompartmentTypeConfigs = new HashSet<CompartmentTypeConfig>();
            this.CompartmentTypeInformations = new HashSet<CompartmentTypeInformation>();
        }

        /// <summary>
        /// Gets or sets the compartment type code.
        /// </summary>
        /// <value>
        /// The compartment type code.
        /// </value>
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompartmentType"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplane types.
        /// Relationship with AirplaneType.
        /// </summary>
        /// <value>
        /// The airplane types.
        /// </value>
        public virtual ICollection<AirplaneType> AirplaneTypes { get; set; }

        /// <summary>
        /// Gets or sets the compartment type configs.
        /// Relationship with CompartmentTypeConfig.
        /// </summary>
        /// <value>
        /// The compartment type configs.
        /// </value>
        public virtual ICollection<CompartmentTypeConfig> CompartmentTypeConfigs { get; set; }

        /// <summary>
        /// Gets or sets the compartment type informations.
        /// Relationship with CompartmentTypeInformation
        /// </summary>
        /// <value>
        /// The compartment type informations.
        /// </value>
        public virtual ICollection<CompartmentTypeInformation> CompartmentTypeInformations { get; set; }
    }
}