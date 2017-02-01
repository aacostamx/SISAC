//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformation.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Manifest Departure Boarding Information
    /// </summary>
    [Table("Itinerary.ManifestDepartureBoardingInformation")]
    public class ManifestDepartureBoardingInformation
    {
        /// <summary>
        /// Gets or sets the boarding information identifier.
        /// </summary>
        /// <value>
        /// The boarding information identifier.
        /// </value>
        //[Key]
        public long BoardingInformationID { get; set; }

        /// <summary>
        /// Gets or sets the boarding identifier.
        /// </summary>
        /// <value>
        /// The boarding identifier.
        /// </value>
        public long BoardingID { get; set; }

        /// <summary>
        /// Gets or sets the compartment type identifier.
        /// </summary>
        /// <value>
        /// The compartment type identifier.
        /// </value>
        public int CompartmentTypeID { get; set; }

        /// <summary>
        /// Gets or sets the compartment type information identifier.
        /// </summary>
        /// <value>
        /// The compartment type information identifier.
        /// </value>
        public int CompartmentTypeInformationID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ManifestDepartureBoardingInformation"/> is checked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checked; otherwise, <c>false</c>.
        /// </value>
        public bool Checked { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding.
        /// </summary>
        /// <value>
        /// The manifest departure boarding.
        /// </value>
        public virtual ManifestDepartureBoarding ManifestDepartureBoarding { get; set; }

        /// <summary>
        /// Gets or sets the compartment type information.
        /// </summary>
        /// <value>
        /// The compartment type information.
        /// </value>
        public virtual CompartmentTypeInformation CompartmentTypeInformation { get; set; }

        /// <summary>
        /// Gets or sets the compartment type configuration.
        /// </summary>
        /// <value>
        /// The compartment type configuration.
        /// </value>
        public virtual CompartmentTypeConfig CompartmentTypeConfig { get; set; }        
    }
}
