//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformationDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    /// <summary>
    /// ManifestDepartureBoardingInformationDto
    /// </summary>
    public class ManifestDepartureBoardingInformationDto
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
        /// Gets or sets the name of the compartment type information.
        /// </summary>
        /// <value>
        /// The name of the compartment type information.
        /// </value>
        public string CompartmentTypeInformationName { get; set; }

        /// <summary>
        /// Gets or sets the name of the compartment type.
        /// </summary>
        /// <value>
        /// The name of the compartment type.
        /// </value>
        public string CompartmentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding.
        /// </summary>
        /// <value>
        /// The manifest departure boarding.
        /// </value>
        //public ManifestDepartureBoardingDto ManifestDepartureBoarding { get; set; }
    }
}
