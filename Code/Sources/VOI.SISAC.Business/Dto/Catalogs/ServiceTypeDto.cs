//------------------------------------------------------------------------
// <copyright file="ServiceTypeDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// Class ServiceTypeDto
    /// </summary>
    public class ServiceTypeDto
    {
        /// <summary>
        /// Gets or sets the service type identifier.
        /// </summary>
        /// <value>
        /// The service type identifier.
        /// </value>
        public string ServiceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the service type.
        /// </summary>
        /// <value>
        /// The name of the service type.
        /// </value>
        public string ServiceTypeName { get; set; }
    }
}
