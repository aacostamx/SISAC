//------------------------------------------------------------------------
// <copyright file="AirportServiceDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using Finances;
    using Itineraries;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Airport Service Dto
    /// </summary>
    public class AirportServiceDto
    {
        /// <summary>
        /// AirportServiceID
        /// </summary>
        public long AirportServiceID { get; set; }

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
        /// StationCode
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// ApronPosition
        /// </summary>
        public string ApronPosition { get; set; }

        /// <summary>
        /// QtyServices
        /// </summary>
        public int QtyServices { get; set; }

        /// <summary>
        /// QtyHours
        /// </summary>
        public int QtyHours { get; set; }

        /// <summary>
        /// StartDateService
        /// </summary>
        public DateTime StartDateService { get; set; }

        /// <summary>
        /// StartTimeService
        /// </summary>
        public TimeSpan StartTimeService { get; set; }

        /// <summary>
        /// EndDateService
        /// </summary>
        public DateTime EndDateService { get; set; }

        /// <summary>
        /// EndTimeService
        /// </summary>
        public TimeSpan EndTimeService { get; set; }

        /// <summary>
        /// DrinkingWaterID
        /// </summary>
        public long? DrinkingWaterID { get; set; }

        /// <summary>
        /// GpuCode
        /// </summary>
        public string GpuCode { get; set; }

        /// <summary>
        /// GpuStartMeter
        /// </summary>
        public double? GpuStartMeter { get; set; }

        /// <summary>
        /// GpuEndMeter
        /// </summary>
        public double? GpuEndMeter { get; set; }

        /// <summary>
        /// GpuObservationCode
        /// </summary>
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// OperationTypeName
        /// </summary>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// FuelBeforeLanding
        /// </summary>
        public double? FuelBeforeLanding { get; set; }

        /// <summary>
        /// FuelLoaded
        /// </summary>
        public double? FuelLoaded { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// DrinkingWater
        /// </summary>
        public DrinkingWaterDto DrinkingWater { get; set; }

        /// <summary>
        /// Gpu
        /// </summary>
        public GpuDto Gpu { get; set; }

        /// <summary>
        /// GpuObservation
        /// </summary>
        public GpuObservationDto GpuObservation { get; set; }

        /// <summary>
        /// Itinerary
        /// </summary>
        public ItineraryDto Itinerary { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderDto Provider { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// ServicesAirport
        /// </summary>
        public IList<AirportServiceDto> ServicesAirport = new List<AirportServiceDto>();
    }
}
