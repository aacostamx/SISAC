//------------------------------------------------------------------------
// <copyright file="ManifestDepartureVO.cs" company="Volaris">
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
    public class ManifestDepartureVO
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
        [Display(Name = "NextScale", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string ScaleStationCode { get; set; }

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
        [Display(Name = "ScaleStation", ResourceType = typeof(Resources.Resource))]
        public string ScaleStationName { get; set; }

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
        public string ScheduledDepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ScheduledDepartureTime", ResourceType = typeof(Resources.Resource))]
        public string ScheduledDepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ActualDepartureDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ActualDepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        [Display(Name = "ActualDepartureTime", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ActualDepartureTime { get; set; }

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
        /// Gets or sets the inner trace.
        /// </summary>
        /// <value>
        /// The inner trace.
        /// </value>
        [Display(Name = "InnerSection", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int InnerSection { get; set; }

        /// <summary>
        /// Gets or sets the international.
        /// </summary>
        /// <value>
        /// The international.
        /// </value>
        [Display(Name = "International", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int International { get; set; }

        /// <summary>
        /// Gets or sets the international exempt.
        /// </summary>
        /// <value>
        /// The international exempt.
        /// </value>
        [Display(Name = "InternationalExempt", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int InternationalExempt { get; set; }

        /// <summary>
        /// Gets or sets the national exempt.
        /// </summary>
        /// <value>
        /// The national exempt.
        /// </value>
        [Display(Name = "NationalExempt", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int NationalExempt { get; set; }

        /// <summary>
        /// Gets or sets the transit.
        /// </summary>
        /// <value>
        /// The transit.
        /// </value>
        [Display(Name = "Transit", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Transit { get; set; }

        /// <summary>
        /// Gets or sets the infant.
        /// </summary>
        /// <value>
        /// The infant.
        /// </value>
        [Display(Name = "Infant", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Infant { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel.
        /// </summary>
        /// <value>
        /// The jet fuel.
        /// </value>
        [Display(Name = "JetFuelKg", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal JetFuel { get; set; }

        /// <summary>
        /// Gets or sets the real takeoff weight.
        /// </summary>
        /// <value>
        /// The real takeoff weight.
        /// </value>
        [Display(Name = "RealTakeoffWeightKg", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal RealTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the operating weight.
        /// </summary>
        /// <value>
        /// The operating weight.
        /// </value>
        [Display(Name = "OperatingWeightKg", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal OperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the safety margin.
        /// </summary>
        /// <value>
        /// The safety margin.
        /// </value>
        [Display(Name = "SafetyMarginKg", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal SafetyMargin { get; set; }

        /// <summary>
        /// Gets or sets the structural takeoff weight.
        /// </summary>
        /// <value>
        /// The structural takeoff weight.
        /// </value>
        [Display(Name = "StructuralTakeoffWeightKg", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public decimal StructuralTakeoffWeight { get; set; }

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

        /// <summary>
        /// Gets or sets the infant tickets.
        /// </summary>
        /// <value>
        /// The infant tickets.
        /// </value>
        [Display(Name = "InfantTickets", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int InfantTickets { get; set; }

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