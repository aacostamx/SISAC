//------------------------------------------------------------------------
// <copyright file="AirportServiceVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using Finance;
    using Itineraries;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// AirportServiceVO class
    /// </summary>
    public class AirportServiceVO
    {
        /// <summary>
        /// AirportServiceID
        /// </summary>
        [Display(Name = "AirportServiceID", ResourceType = typeof(Resources.Resource))]
        public long AirportServiceID { get; set; }

        /// <summary>
        /// Sequence
        /// </summary>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
        public int Sequence { get; set; }

        /// <summary>
        /// AirlineCode
        /// </summary>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        public string AirlineCode { get; set; }

        /// <summary>
        /// FlightNumber
        /// </summary>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        public string FlightNumber { get; set; }

        /// <summary>
        /// ItineraryKey
        /// </summary>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        [Required]
        [Display(Name = "Service", ResourceType = typeof(Resources.Resource))]
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [Required]
        [Display(Name = "Provider", ResourceType = typeof(Resources.Resource))]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// ApronPosition
        /// </summary>
        [Required]
        [Display(Name = "ApronPosition", ResourceType = typeof(Resources.Resource))]
        public string ApronPosition { get; set; }

        /// <summary>
        /// QtyServices
        /// </summary>
        [Display(Name = "QtyServices", ResourceType = typeof(Resources.Resource))]
        [Range(0, int.MaxValue)]
        public int QtyServices { get; set; }

        /// <summary>
        /// QtyHours
        /// </summary>
        [Display(Name = "QtyHours", ResourceType = typeof(Resources.Resource))]
        public int QtyHours { get; set; }

        /// <summary>
        /// StartDateService
        /// </summary>
        [Display(Name = "StartDateService", ResourceType = typeof(Resources.Resource))]
        public DateTime StartDateService { get; set; }

        /// <summary>
        /// StartTimeService
        /// </summary>
        [Display(Name = "StartTimeService", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTimeService { get; set; }

        /// <summary>
        /// EndDateService
        /// </summary>
        [Display(Name = "EndDateService", ResourceType = typeof(Resources.Resource))]
        public DateTime EndDateService { get; set; }

        /// <summary>
        /// EndTimeService
        /// </summary>
        [Display(Name = "EndTimeService", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan EndTimeService { get; set; }

        /// <summary>
        /// DrinkingWaterID
        /// </summary>
        [Display(Name = "DrinkingWaterPercentageDeparture", ResourceType = typeof(Resources.Resource))]
        public long? DrinkingWaterID { get; set; }

        /// <summary>
        /// GpuCode
        /// </summary>
        [Display(Name = "GpuCode", ResourceType = typeof(Resources.Resource))]
        public string GpuCode { get; set; }

        /// <summary>
        /// GpuStartMeter
        /// </summary>
        [Display(Name = "GpuStartMeter", ResourceType = typeof(Resources.Resource))]
        public double? GpuStartMeter { get; set; }

        /// <summary>
        /// GpuEndMeter
        /// </summary>
        [Display(Name = "GpuEndMeter", ResourceType = typeof(Resources.Resource))]
        public double? GpuEndMeter { get; set; }

        /// <summary>
        /// GpuObservationCode
        /// </summary>
        [Display(Name = "GpuObservationCode", ResourceType = typeof(Resources.Resource))]
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// OperationTypeName
        /// </summary>
        [Display(Name = "OperationTypeName", ResourceType = typeof(Resources.Resource))]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// FuelBeforeLanding
        /// </summary>
        [Display(Name = "FuelBeforeLanding", ResourceType = typeof(Resources.Resource))]
        public double? FuelBeforeLanding { get; set; }

        /// <summary>
        /// FuelLoaded
        /// </summary>
        [Display(Name = "FuelLoaded", ResourceType = typeof(Resources.Resource))]
        public double? FuelLoaded { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        /// <summary>
        /// DrinkingWater
        /// </summary>
        public DrinkingWaterVO DrinkingWater { get; set; }

        /// <summary>
        /// Gpu
        /// </summary>
        public GpuVO Gpu { get; set; }

        /// <summary>
        /// GpuObservation
        /// </summary>
        public GpuObservationVO GpuObservation { get; set; }

        /// <summary>
        /// Itinerary
        /// </summary>
        public ItineraryVO Itinerary { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderVO Provider { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public ServiceVO Service { get; set; }

        /// <summary>
        /// ServicesAirport
        /// </summary>
        public IList<AirportServiceVO> ServicesAirport = new List<AirportServiceVO>();
    }
}