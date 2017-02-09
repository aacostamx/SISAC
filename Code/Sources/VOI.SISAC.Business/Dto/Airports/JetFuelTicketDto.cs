//------------------------------------------------------------------------
// <copyright file="JetFuelTicketDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// Jet Fuel Ticket Dto
    /// </summary>
    public class JetFuelTicketDto
    {
        #region Properties for columns

        /// <summary>
        /// JetFuelTicketID
        /// </summary>
        public long JetFuelTicketID { get; set; }

        /// <summary>
        /// Sequence
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// AirlineCode
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// FlightNumber
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// ItineraryKey
        /// </summary>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// OperationTypeName
        /// </summary>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// FuelingDate
        /// </summary>
        public DateTime FuelingDate { get; set; }

        /// <summary>
        /// FuelingTime
        /// </summary>
        public TimeSpan FuelingTime { get; set; }

        /// <summary>
        /// JetFuelProviderNumber
        /// </summary>
        public string JetFuelProviderNumber { get; set; }

        /// <summary>
        /// IntoPlaneProviderNumber
        /// </summary>
        public string IntoPlaneProviderNumber { get; set; }

        /// <summary>
        /// TicketNumber
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// FueledQtyGals
        /// </summary>
        public int FueledQtyGals { get; set; }

        /// <summary>
        /// RemainingQry
        /// </summary>
        public int? RemainingQry { get; set; }

        /// <summary>
        /// RequestedQry
        /// </summary>
        public int? RequestedQry { get; set; }

        /// <summary>
        /// FueledQry
        /// </summary>
        public int? FueledQry { get; set; }

        /// <summary>
        /// DensityFactor
        /// </summary>
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// AorUserID
        /// </summary>
        public int AorUserID { get; set; }

        /// <summary>
        /// SupplierResponsible
        /// </summary>
        public string SupplierResponsible { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks { get; set; }

        #endregion

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderDto JetFuelProvider { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderDto IntoPlaneProvider { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// Itinerary
        /// </summary>
        public ItineraryDto Itinerary { get; set; }
    }
}
