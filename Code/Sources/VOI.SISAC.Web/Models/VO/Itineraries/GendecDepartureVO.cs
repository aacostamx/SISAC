//------------------------------------------------------------------------
// <copyright file="GendecDepartureVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Airport;

    public class GendecDepartureVO
    {
        public string DepartureDate { get; set; }

        public string TimeDeparture { get; set; }

        public string AirplaneModel { get; set; }

        public string EquipmentNumber { get; set; }

        public string DepartureStation { get; set; }

        public string ArrivalStation { get; set; }

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
        [Display(Name = "ActualTimeOfDeparture", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan ActualTimeOfDeparture { get; set; }

        /// <summary>
        /// Airport departure
        /// </summary>
        public string DeparturePlace { get; set; }

        /// <summary>
        /// Time of landing
        /// </summary>
        public TimeSpan? Embarking { get; set; }

        /// <summary>
        /// Flight Departure Description
        /// </summary>
        public string FlightDepartureDescription { get; set; }

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