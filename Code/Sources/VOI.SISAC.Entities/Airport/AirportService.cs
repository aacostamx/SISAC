//------------------------------------------------------------------------
// <copyright file="AirportService.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// AirportService
    /// </summary>
    [Table("Airport.AirportService")]
    public class AirportService
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
        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// FlightNumber
        /// </summary>
        [Required]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// ItineraryKey
        /// </summary>
        [Required]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Required]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        [Required]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [Required]
        [StringLength(10)]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// ApronPosition
        /// </summary>
        [Required]
        [StringLength(8)]
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
        [StringLength(50)]
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
        [StringLength(10)]
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// OperationTypeName
        /// </summary>
        [StringLength(20)]
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
        [StringLength(250)]
        public string Remarks { get; set; }

        /// <summary>
        /// DrinkingWater
        /// </summary>
        public virtual DrinkingWater DrinkingWater { get; set; }

        /// <summary>
        /// Gpu
        /// </summary>
        public virtual Gpu Gpu { get; set; }

        /// <summary>
        /// GpuObservation
        /// </summary>
        public virtual GpuObservation GpuObservation { get; set; }

        /// <summary>
        /// Itinerary
        /// </summary>
        public virtual Itinerary Itinerary { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public virtual Provider Provider { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public virtual Service Service { get; set; }

        /// <summary>
        /// ServicesAirport
        /// </summary>
        [NotMapped]
        public IList<AirportService> ServicesAirport = new List<AirportService>();
    }
}
