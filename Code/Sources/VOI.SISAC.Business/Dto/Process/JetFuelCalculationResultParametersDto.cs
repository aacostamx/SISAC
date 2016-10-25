//------------------------------------------------------------------------
// <copyright file="JetFuelCalculationResultParametersDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System;
    using System.Collections.Generic;    

    public class JetFuelCalculationResultParametersDto
    {
        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public IList<string> StationCode { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public IList<string> AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public IList<string> ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public IList<string> ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is open period.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is open period otherwise <c>false</c>.
        /// </value>
        public bool IsOpenPeriod { get; set; }
    }
}
