//------------------------------------------------------------------------
// <copyright file="TimelineMovementVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    /// <summary>
    /// Timeline Movement View Object
    /// </summary>
    public class TimelineMovementVO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the operation description.
        /// </summary>
        /// <value>
        /// The operation description.
        /// </value>
        public string OperationDescription { get; set; }

        /// <summary>
        /// Gets or sets the movement type code.
        /// </summary>
        /// <value>
        /// The movement type code.
        /// </value>
        public string MovementTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the movement type description.
        /// </summary>
        /// <value>
        /// The movement type description.
        /// </value>
        public string MovementTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the movement date.
        /// </summary>
        /// <value>
        /// The movement date.
        /// </value>
        public DateTime MovementDate { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the remaining fuel.
        /// </summary>
        /// <value>
        /// The remaining fuel.
        /// </value>
        public decimal? RemainingFuel { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }
    }
}