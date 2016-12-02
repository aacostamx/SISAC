//------------------------------------------------------------------------
// <copyright file="AdditionalPassengerInformationVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    /// <summary>
    /// Additional passenger information view object
    /// </summary>
    public class AdditionalPassengerInformationVO
    {
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
        /// Gets or sets the adult national.
        /// </summary>
        /// <value>
        /// The adult national.
        /// </value>
        public int AdultNational { get; set; }

        /// <summary>
        /// Gets or sets the adult international.
        /// </summary>
        /// <value>
        /// The adult international.
        /// </value>
        public int AdultInternational { get; set; }

        /// <summary>
        /// Gets or sets the minor national.
        /// </summary>
        /// <value>
        /// The minor national.
        /// </value>
        public int MinorNational { get; set; }

        /// <summary>
        /// Gets or sets the minor international.
        /// </summary>
        /// <value>
        /// The minor international.
        /// </value>
        public int MinorInternational { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic national.
        /// </summary>
        /// <value>
        /// The diplomatic national.
        /// </value>
        public int DiplomaticNational { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic international.
        /// </summary>
        /// <value>
        /// The diplomatic international.
        /// </value>
        public int DiplomaticInternational { get; set; }

        /// <summary>
        /// Gets or sets the commission national.
        /// </summary>
        /// <value>
        /// The commission national.
        /// </value>
        public int CommissionNational { get; set; }

        /// <summary>
        /// Gets or sets the commission international.
        /// </summary>
        /// <value>
        /// The commission international.
        /// </value>
        public int CommissionInternational { get; set; }

        /// <summary>
        /// Gets or sets the infant national.
        /// </summary>
        /// <value>
        /// The infant national.
        /// </value>
        public int InfantNational { get; set; }

        /// <summary>
        /// Gets or sets the infant international.
        /// </summary>
        /// <value>
        /// The infant international.
        /// </value>
        public int InfantInternational { get; set; }

        /// <summary>
        /// Gets or sets the transitory national.
        /// </summary>
        /// <value>
        /// The transitory national.
        /// </value>
        public int TransitoryNational { get; set; }

        /// <summary>
        /// Gets or sets the transitory international.
        /// </summary>
        /// <value>
        /// The transitory international.
        /// </value>
        public int TransitoryInternational { get; set; }

        /// <summary>
        /// Gets or sets the connection national.
        /// </summary>
        /// <value>
        /// The connection national.
        /// </value>
        public int ConnectionNational { get; set; }

        /// <summary>
        /// Gets or sets the connection international.
        /// </summary>
        /// <value>
        /// The connection international.
        /// </value>
        public int ConnectionInternational { get; set; }

        /// <summary>
        /// Gets or sets the other national.
        /// </summary>
        /// <value>
        /// The other national.
        /// </value>
        public int OtherNational { get; set; }

        /// <summary>
        /// Gets or sets the other international.
        /// </summary>
        /// <value>
        /// The other international.
        /// </value>
        public int OtherInternational { get; set; }

        /// <summary>
        /// Gets or sets the pax dni.
        /// </summary>
        /// <value>
        /// The pax dni.
        /// </value>
        public int PaxDni { get; set; }
    }
}