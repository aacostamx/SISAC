//------------------------------------------------------------------------
// <copyright file="Airline.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Class Airline
    /// </summary>
    public partial class Airline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Airline"/> class.
        /// </summary>
        public Airline()
        {
            this.CostCenters = new HashSet<CostCenter>();
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
            this.Users = new List<User>();
        }

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
        /// Gets or sets a value indicating whether this <see cref="Airline"/> is status.
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

        /// <summary>
        /// Gets or sets the cost centers.
        /// </summary>
        /// <value>
        /// The cost centers.
        /// </value>
        public virtual ICollection<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Gets or sets the cost centers.
        /// </summary>
        /// <value>
        /// The cost centers.
        /// </value>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// </summary>
        /// <value>
        /// The airplane.
        /// </value>
        public virtual ICollection<Airplane> Airplanes { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }
    }
}