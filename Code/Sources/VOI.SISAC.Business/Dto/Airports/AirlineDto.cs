//------------------------------------------------------------------------
// <copyright file="AirlineDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Class Dto Airline
    /// </summary>
    public class AirlineDto
    {
        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the airline.
        /// </summary>
        /// <value>
        /// The name of the airline.
        /// </value>
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the airline.
        /// </summary>
        /// <value>
        /// The short name of the airline.
        /// </value>
        public string AirlineShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirlineDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
        
        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>
        /// The company code.
        /// </value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the division.
        /// </summary>
        /// <value>
        /// The division.
        /// </value>
        public string Division { get; set; }

        /// </summary>
        /// <value>
        /// The division.
        /// </value>
        public string BusinessName { get; set; }

        /// <summary>
        /// Gets or sets the cost centers.
        /// </summary>
        /// <value>
        /// The cost centers.
        /// </value>
        public ICollection<CostCenterDto> CostCenters { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        /// <value>
        /// user.
        /// </value>        
        public ICollection<UserDto> Users { get; set; }
    }
}
