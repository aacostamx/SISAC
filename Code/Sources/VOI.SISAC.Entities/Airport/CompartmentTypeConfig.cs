//------------------------------------------------------------------------
// <copyright file="CompartmentTypeConfig.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------


namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// CompartmentTypeConfig Entity
    /// </summary>
    public partial class CompartmentTypeConfig
    {
        /// <summary>
        /// Gets or sets the compartment type identifier.
        /// </summary>
        /// <value>
        /// The compartment type identifier.
        /// </value>
        public int CompartmentTypeID { get; set; }

        /// <summary>
        /// Gets or sets the compartment type code.
        /// </summary>
        /// <value>
        /// The compartment type code.
        /// </value>
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the compartment type.
        /// </summary>
        /// <value>
        /// The name of the compartment type.
        /// </value>
        public string CompartmentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the maximum weight.
        /// </summary>
        /// <value>
        /// The maximum weight.
        /// </value>
        public decimal MaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the compartment type level.
        /// </summary>
        /// <value>
        /// The compartment type level.
        /// </value>
        public int CompartmentTypeLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompartmentTypeConfig"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the compartment.
        /// Relationship with CompartmentType
        /// </summary>
        /// <value>
        /// The type of the compartment.
        /// </value>
        public virtual CompartmentType CompartmentType { get; set; }
    }
}
