//------------------------------------------------------------------------
// <copyright file="CompartmentTypeInformation.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// CompartmentTypeInformation Entity
    /// </summary>
    public partial class CompartmentTypeInformation
    {
        /// <summary>
        /// Gets or sets the compartment type information identifier.
        /// </summary>
        /// <value>
        /// The compartment type information identifier.
        /// </value>
        public int CompartmentTypeInformationID { get; set; }

        /// <summary>
        /// Gets or sets the compartment type code.
        /// </summary>
        /// <value>
        /// The compartment type code.
        /// </value>
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the compartment type information.
        /// </summary>
        /// <value>
        /// The name of the compartment type information.
        /// </value>
        public string CompartmentTypeInformationName { get; set; }

        /// <summary>
        /// Gets or sets the compartment type information level.
        /// </summary>
        /// <value>
        /// The compartment type information level.
        /// </value>
        public int CompartmentTypeInformationLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompartmentTypeInformation"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the compartment.
        /// Relationship with  CompartmentType.
        /// </summary>
        /// <value>
        /// The type of the compartment.
        /// </value>
        public virtual CompartmentType CompartmentType { get; set; }
    }
}
