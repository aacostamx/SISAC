//------------------------------------------------------------------------
// <copyright file="CostCenterDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Dto Cost Center
    /// </summary>
    public class CostCenterDto
    {
        /// <summary>
        /// Gets or sets the cc number.
        /// </summary>
        /// <value>
        /// The CCNumber
        /// </value>
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the cc.
        /// </summary>
        /// <value>
        /// The name of the cc.
        /// </value>
        public string CCName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CostCenterDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public AirlineDto Airline { get; set; }

        /// <summary>
        /// Gets or sets Internatinal Fuel Contract
        /// </summary>
        public ICollection<InternationalFuelContractDto> InternationalFuelContract { get; set; }
    }
}
