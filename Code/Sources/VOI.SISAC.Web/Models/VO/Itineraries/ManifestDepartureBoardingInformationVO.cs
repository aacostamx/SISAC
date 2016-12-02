//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformationVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ManifestDepartureBoardingInformationVO
    /// </summary>
    public class ManifestDepartureBoardingInformationVO
    {
        /// <summary>
        /// Gets or sets the boarding information identifier.
        /// </summary>
        /// <value>
        /// The boarding information identifier.
        /// </value>
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
        //public ManifestDepartureBoardingVO ManifestDepartureBoarding { get; set; }
    }
}