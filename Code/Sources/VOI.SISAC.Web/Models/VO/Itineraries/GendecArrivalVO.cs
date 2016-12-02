//------------------------------------------------------------------------
// <copyright file="GendecArrivalVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Airport;

    /// <summary>
    /// Arrival general document view object
    /// </summary>
    public class GendecArrivalVO
    {
        /// <summary>
        /// Gets or sets the departure date.
        /// </summary>
        /// <value>
        /// The departure date.
        /// </value>
        public string DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the arrival date.
        /// </summary>
        /// <value>
        /// The arrival date.
        /// </value>
        public string ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets the time arrival.
        /// </summary>
        /// <value>
        /// The time arrival.
        /// </value>
        public string TimeArrival { get; set; }

        /// <summary>
        /// Gets or sets the airplane model.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure station.
        /// </summary>
        /// <value>
        /// The departure station.
        /// </value>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets the arrival station.
        /// </summary>
        /// <value>
        /// The arrival station.
        /// </value>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// Gets the flight.
        /// </summary>
        /// <value>
        /// The flight.
        /// </value>
        public string Flight
        {
            get
            {
                return this.DepartureStation + "-" + this.ArrivalStation;
            }
        }
        /// <summary>
        /// Sequence of Flight
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Airline Code of the Flight
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Date of Flight
        /// </summary>
        public string Itinerarykey { get; set; }

        /// <summary>
        /// Total Pax
        /// </summary>
        [Display(Name = "TotalPax", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TotalPax { get; set; }

        /// <summary>
        /// Total number of the crew members
        /// </summary>
        [Display(Name = "TotalCrew", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        public int TotalCrew { get; set; }

        /// <summary>
        /// Block Time
        /// </summary>
        [Display(Name = "BlockTime", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? BlockTime { get; set; }

        /// <summary>
        /// Manifest Number
        /// </summary>
        [Display(Name = "ManifestNumber", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "LengthMax8")]
        public string ManifestNumber { get; set; }

        /// <summary>
        /// Gate Number
        /// </summary>
        [Display(Name = "GateNumber", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "LengthMax8")]
        public string GateNumber { get; set; }

        /// <summary>
        /// Departure Date
        /// </summary>
        [Display(Name = "ActualTimeOfArrival", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? ActualTimeOfArrival { get; set; }

        /// <summary>
        /// Airport departure
        /// </summary>
        public string ArrivalPlace { get; set; }

        /// <summary>
        /// Time of landing
        /// </summary>
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? Disembanking { get; set; }

        /// <summary>
        /// Flight Departure Description
        /// </summary>
        public string FlightArrivalDescription { get; set; }

        /// <summary>
        /// Crew Member
        /// </summary>
        [Display(Name = "ResponsableASC", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        public int Member { get; set; }

        /// <summary>
        /// Authorized Agent
        /// </summary>
        public string AuthorizedAgent { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resource))]
        public string Remarks { get; set; }

        /// <summary>
        /// Statof of the Gendec
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public byte Action { get; set; }

        /// <summary>
        /// Gets or sets the crews.
        /// </summary>
        /// <value>
        /// The crews.
        /// </value>
        public ICollection<CrewVO> Crews { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryVO Itinerary { get; set; }
    }
}