//------------------------------------------------------------------------
// <copyright file="CountryDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// Class Dto Country 
    /// </summary>
    public class CountryDto
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country code short.
        /// </summary>
        /// <value>
        /// The country code short.
        /// </value>
        public string CountryCodeShort { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CountryDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
