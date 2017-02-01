//------------------------------------------------------------------------
// <copyright file="ItineraryVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;
    using Airport;

    /// <summary>
    /// Itinerary View Object
    /// </summary>
    public class ItineraryVO
    {
        /// <summary>
        /// Data of the flight sequence
        /// </summary>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
        public int Sequence { get; set; }

        /// <summary>
        /// CarrierCode = Airline Code
        /// </summary>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number of the Airplane
        /// </summary>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Primary Key = Departure Date + CarrierCode + Flight Number + DepartureStation + DepartureArrival
        /// </summary>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets Equipment Number
        /// </summary>
        [Display(Name = "EquipmentNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "EquipmentNumber")]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets Departure Date
        /// </summary>
        [Display(Name = "DepartureDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets Departure Airport
        /// </summary>
        [Display(Name = "DepartureStation", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string DepartureStation { get; set; }


        /// <summary>
        /// DepartureTime Temporal Variable
        /// </summary>
        private TimeSpan _DepartureTime;

        /// <summary>
        /// ArrivalTime @"{0:hh\:mm tt}" formato AM PM
        /// </summary>
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan DepartureTime
        {
            get
            {
                _DepartureTime = new TimeSpan(this.DepartureDate.Hour, this.DepartureDate.Minute, 0);
                return _DepartureTime;
            }
            set
            {
                _DepartureTime = value;
            }
        }

        /// <summary>
        /// Gets or sets Arrival Date
        /// </summary>
        [Display(Name = "ArrivalDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets Arrival Airport
        /// </summary>
        [Display(Name = "ArrivalStation", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string ArrivalStation { get; set; }

        /// <summary>
        /// EditArrival 
        /// </summary>
        public bool? EditArrival { get; set; }

        /// <summary>
        /// Arrival Time Temporal Variable
        /// </summary>
        private TimeSpan _ArravalTime;

        /// <summary>
        /// ArrivalTime @"{0:hh\:mm tt}" formato AM PM
        /// </summary>
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan ArrivalTime
        {
            get
            {
                _ArravalTime = new TimeSpan(this.ArrivalDate.Hour, this.ArrivalDate.Minute, 0);
                return _ArravalTime;
            }
            set
            {
                _ArravalTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets a list of Logs in itinerary
        /// </summary>
        public ICollection<ItineraryLogVO> itinerariesLogs { get; set; }

        /// <summary>
        /// AirportServices
        /// </summary>
        public ICollection<AirportServiceVO> AirportServices { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// </summary>
        /// <value>
        /// The airplane.
        /// </value>
        public AirplaneVO Airplane { get; set; }

        /// <summary>
        /// JetFuelTickets
        /// </summary>
        public ICollection<JetFuelTicketVO> JetFuelTickets { get; set; }
    }
}