//------------------------------------------------------------------------
// <copyright file="ManifestArrivalVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Airport;

    /// <summary>
    /// Manifest departure view object
    /// </summary>
    public class ManifestArrivalVO
    {
        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>
        /// The airline.
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
        /// Gets or sets the nick name commander.
        /// </summary>
        /// <value>
        /// The nick name commander.
        /// </value>
        [Display(Name = "NickNameCommander", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameCommander { get; set; }

        /// <summary>
        /// Gets or sets the nick name first official.
        /// </summary>
        /// <value>
        /// The nick name first official.
        /// </value>
        [Display(Name = "NickNameFirstOfficial", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameFirstOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name second official.
        /// </summary>
        /// <value>
        /// The nick name second official.
        /// </value>
        [Display(Name = "NickNameSecondOfficial", ResourceType = typeof(Resources.Resource))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameSecondOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name third official.
        /// </summary>
        /// <value>
        /// The nick name third official.
        /// </value>
        [Display(Name = "NickNameThirdOfficial", ResourceType = typeof(Resources.Resource))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameThirdOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name chief cabinet.
        /// </summary>
        /// <value>
        /// The nick name chief cabinet.
        /// </value>
        [Display(Name = "NickNameChiefCabinet", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameChiefCabinet { get; set; }

        /// <summary>
        /// Gets or sets the nick name first supercargo.
        /// </summary>
        /// <value>
        /// The nick name first supercargo.
        /// </value>
        [Display(Name = "NickNameFirstSupercargo", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameFirstSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the nick name second supercargo.
        /// </summary>
        /// <value>
        /// The nick name second supercargo.
        /// </value>
        [Display(Name = "NickNameSecondSupercargo", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameSecondSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the nick name third supercargo.
        /// </summary>
        /// <value>
        /// The nick name third supercargo.
        /// </value>
        [Display(Name = "NickNameThirdSupercargo", ResourceType = typeof(Resources.Resource))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameThirdSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the supercargo remarks.
        /// </summary>
        /// <value>
        /// The supercargo remarks.
        /// </value>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax150")]
        public string SupercargoRemarks { get; set; }

        /// <summary>
        /// Gets or sets the departure station code.
        /// </summary>
        /// <value>
        /// The departure station code.
        /// </value>
        [Display(Name = "DepartureStation", ResourceType = typeof(Resources.Resource))]
        public string DepartureStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the departure station.
        /// </summary>
        /// <value>
        /// The name of the departure station.
        /// </value>
        [Display(Name = "DepartureStation", ResourceType = typeof(Resources.Resource))]
        public string DepartureStationName { get; set; }

        /// <summary>
        /// Gets or sets the scale station code.
        /// </summary>
        /// <value>
        /// The scale station code.
        /// </value>
        [Display(Name = "PreviousScale", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string LastStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the scale station.
        /// </summary>
        /// <value>
        /// The name of the scale station.
        /// </value>
        /// <summary>
        /// Gets or sets the scale station code.
        /// </summary>
        /// <value>
        /// The scale station code.
        /// </value>
        public string LastStationName { get; set; }

        /// <summary>
        /// Gets or sets the arrival station code.
        /// </summary>
        /// <value>
        /// The arrival station code.
        /// </value>
        [Display(Name = "ArrivalStation", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string ArrivalStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the arrival station.
        /// </summary>
        /// <value>
        /// The name of the arrival station.
        /// </value>
        [Display(Name = "ArrivalStation", ResourceType = typeof(Resources.Resource))]
        public string ArrivalStationName { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ScheduledDepartureDate", ResourceType = typeof(Resources.Resource))]
        public string ScheduledArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ScheduledDepartureTime", ResourceType = typeof(Resources.Resource))]
        public string ScheduledArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ActualDepartureDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ActualArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ActualDepartureTime", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ActualArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the delay remarks.
        /// </summary>
        /// <value>
        /// The delay remarks.
        /// </value>
        [Display(Name = "DelayRemarks", ResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax150")]
        public string DelayRemarks { get; set; }

        /// <summary>
        /// Gets or sets the adult passenger.
        /// </summary>
        /// <value>
        /// The adult passenger.
        /// </value>
        [Display(Name = "Adults", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int AdultPassenger { get; set; }

        /// <summary>
        /// Gets or sets the minor passenger.
        /// </summary>
        /// <value>
        /// The minor passenger.
        /// </value>
        [Display(Name = "Teenage", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int MinorPassenger { get; set; }

        /// <summary>
        /// Gets or sets the infant passenger.
        /// </summary>
        /// <value>
        /// The infant passenger.
        /// </value>
        [Display(Name = "Children", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int InfantPassenger { get; set; }

        /// <summary>
        /// Gets or sets the luggage quantity.
        /// </summary>
        /// <value>
        /// The luggage quantity.
        /// </value>
        [Display(Name = "Luggage", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int LuggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the luggage weight.
        /// </summary>
        /// <value>
        /// The luggage weight.
        /// </value>
        [Display(Name = "Luggage", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal LuggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the charge quantity.
        /// </summary>
        /// <value>
        /// The charge quantity.
        /// </value>
        [Display(Name = "Charge", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int ChargeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the charge weight.
        /// </summary>
        /// <value>
        /// The charge weight.
        /// </value>
        [Display(Name = "Charge", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal ChargeWeight { get; set; }

        /// <summary>
        /// Gets or sets the mail quantity.
        /// </summary>
        /// <value>
        /// The mail quantity.
        /// </value>
        [Display(Name = "Mail", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int MailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the mail weight.
        /// </summary>
        /// <value>
        /// The mail weight.
        /// </value>
        [Display(Name = "Mail", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal MailWeight { get; set; }

        /// <summary>
        /// Gets or sets the user identifier signature.
        /// </summary>
        /// <value>
        /// The user identifier signature.
        /// </value>
        [Display(Name = "UserSignature", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string UserIdSignature { get; set; }

        /// <summary>
        /// Gets or sets the user identifier authorize.
        /// </summary>
        /// <value>
        /// The user identifier authorize.
        /// </value>
        [Display(Name = "UserAuthorize", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string UserIdAuthorize { get; set; }

        /// <summary>
        /// Gets or sets the licence number signature.
        /// </summary>
        /// <value>
        /// The licence number signature.
        /// </value>
        [Display(Name = "LicenceNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax20")]
        public string LicenceNumberSignature { get; set; }

        /// <summary>
        /// Gets or sets the licence number authorize.
        /// </summary>
        /// <value>
        /// The licence number authorize.
        /// </value>
        [Display(Name = "LicenceNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax20")]
        public string LicenceNumberAuthorize { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [Display(Name = "Position", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string Position { get; set; }

        //[Display(Name = "InfantTickets", ResourceType = typeof(Resources.Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
        //          ErrorMessageResourceName = "RequiredField")]
        /// <summary>
        /// Gets or sets the jet fuel arrival.
        /// </summary>
        /// <value>
        /// The jet fuel arrival.
        /// </value>
        public decimal JetFuelArrival { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is close.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is close; otherwise, <c>false</c>.
        /// </value>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public int Action { get; set; }

        /// <summary>
        /// Gets or sets the tolerance time.
        /// </summary>
        /// <value>
        /// The tolerance time.
        /// </value>
        public decimal ToleranceTime { get; set; }

        /// <summary>
        /// Gets or sets the pilot.
        /// </summary>
        /// <value>
        /// The pilot.
        /// </value>
        [Display(Name = "Pilots", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Pilot { get; set; }

        /// <summary>
        /// Gets or sets the surcharge.
        /// </summary>
        /// <value>
        /// The surcharge.
        /// </value>
        [Display(Name = "Surcharges", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Surcharge { get; set; }

        /// <summary>
        /// Gets or sets the extra crew.
        /// </summary>
        /// <value>
        /// The extra crew.
        /// </value>
        [Display(Name = "ExtraCrews", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int ExtraCrew { get; set; }

        /// <summary>
        /// Gets or sets the type flight.
        /// </summary>
        /// <value>
        /// The type flight.
        /// </value>
        [Display(Name = "TypeFlight", ResourceType = typeof(Resources.Resource))]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax10")]
        public string TypeFlight { get; set; }

        /// <summary>
        /// Gets or sets the slot allocated time.
        /// </summary>
        /// <value>
        /// The slot allocated time.
        /// </value>
        [Display(Name = "SlotAllocatedTime", ResourceType = typeof(Resources.Resource))]
        public string SlotAllocatedTime { get; set; }

        /// <summary>
        /// Gets or sets the slot coordinated time.
        /// </summary>
        /// <value>
        /// The slot coordinated time.
        /// </value>
        [Display(Name = "SlotCoordinatedTime", ResourceType = typeof(Resources.Resource))]
        public string SlotCoordinatedTime { get; set; }

        /// <summary>
        /// Gets or sets the overnight end time.
        /// </summary>
        /// <value>
        /// The overnight end time.
        /// </value>
        [Display(Name = "OvernightEndTime", ResourceType = typeof(Resources.Resource))]
        public string OvernightEndTime { get; set; }

        /// <summary>
        /// Gets or sets the maneuver start time.
        /// </summary>
        /// <value>
        /// The maneuver start time.
        /// </value>
        [Display(Name = "ManeuverStartTime", ResourceType = typeof(Resources.Resource))]
        public string ManeuverStartTime { get; set; }

        /// <summary>
        /// Gets or sets the position output time.
        /// </summary>
        /// <value>
        /// The position output time.
        /// </value>
        [Display(Name = "PositionOutputTime", ResourceType = typeof(Resources.Resource))]
        public string PositionOutputTime { get; set; }

        /// <summary>
        /// Gets or sets the delay description.
        /// </summary>
        /// <value>
        /// The delay description.
        /// </value>
        [Display(Name = "DelayDescription1", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax200")]
        public string FirstDelayDescription { get; set; }

        /// <summary>
        /// Gets or sets the delay description.
        /// </summary>
        /// <value>
        /// The delay description.
        /// </value>
        [Display(Name = "DelayDescription2", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax200")]
        public string SecondDelayDescription { get; set; }

        /// <summary>
        /// Gets or sets the delay description.
        /// </summary>
        /// <value>
        /// The delay description.
        /// </value>
        [Display(Name = "DelayDescription3", ResourceType = typeof(Resources.Resource))]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax200")]
        public string ThirdDelayDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryVO Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the delays.
        /// </summary>
        /// <value>
        /// The delays.
        /// </value>
        public IList<DelayVO> Delays { get; set; }
    }
}