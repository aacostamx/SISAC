//------------------------------------------------------------------------
// <copyright file="PassengerInformationVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Web.Models.VO.Security;
    using VOI.SISAC.Web.Models.VO.Itineraries;

    /// <summary>
    /// Passenger Information View Object
    /// </summary>
    public class PassengerInformationVO
    {
        /// <summary>
        /// Gets or sets the departure date.
        /// </summary>
        /// <value>
        /// The departure date.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "DepartureDate", ResourceType = typeof(Resources.Resource))]
        public string DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the time departure.
        /// </summary>
        /// <value>
        /// The time departure.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "DepartureTime", ResourceType = typeof(Resources.Resource))]
        public string TimeDeparture { get; set; }

        /// <summary>
        /// Gets or sets the airplane model.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "AirplaneModel", ResourceType = typeof(Resources.Resource))]
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "EquipmentNumber", ResourceType = typeof(Resources.Resource))]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure.
        /// </summary>
        /// <value>
        /// The departure.
        /// </value>
        public string Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival.
        /// </summary>
        /// <value>
        /// The arrival.
        /// </value>
        public string Arrival { get; set; }

        /// <summary>
        /// Gets the flight.
        /// </summary>
        /// <value>
        /// The flight.
        /// </value>
        [Display(Name = "Flight", ResourceType = typeof(Resources.Resource))]
        public string Flight
        {
            get
            {
                return this.Departure + "-" + this.Arrival;
            }
        }

    /// <summary>
    /// Gets or sets the sequence.
        /// Foreign key.
    /// </summary>
    /// <value>
    /// The sequence.
    /// </value>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
    public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the adults in cabin a.
        /// </summary>
        /// <value>
        /// The adults in cabin a.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int AdultsCabinA { get; set; }

        /// <summary>
        /// Gets or sets the adults in cabin b.
        /// </summary>
        /// <value>
        /// The adults in cabin b.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int AdultsCabinB { get; set; }

        /// <summary>
        /// Gets or sets the teenage in cabin a.
        /// </summary>
        /// <value>
        /// The teenage in cabin a.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TeenageCabinA { get; set; }

        /// <summary>
        /// Gets or sets the teenage in cabin b.
        /// </summary>
        /// <value>
        /// The teenage in cabin b.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TeenageCabinB { get; set; }

        /// <summary>
        /// Gets or sets the children in cabin a.
        /// </summary>
        /// <value>
        /// The children in cabin a.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ChildrenCabinA { get; set; }

        /// <summary>
        /// Gets or sets the children in cabin b.
        /// </summary>
        /// <value>
        /// The children in cabin b.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ChildrenCabinB { get; set; }

        /// <summary>
        /// Gets or sets the local adults.
        /// </summary>
        /// <value>
        /// The local adults.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int LocalAdults { get; set; }

        /// <summary>
        /// Gets or sets the local teenage.
        /// </summary>
        /// <value>
        /// The local teenage.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int LocalTeenage { get; set; }

        /// <summary>
        /// Gets or sets the local children.
        /// </summary>
        /// <value>
        /// The local children.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int LocalChildren { get; set; }

        /// <summary>
        /// Gets or sets the transitory adults.
        /// </summary>
        /// <value>
        /// The transitory adults.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TransitoryAdults { get; set; }

        /// <summary>
        /// Gets or sets the transitory teenage.
        /// </summary>
        /// <value>
        /// The transitory teenage.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TransitoryTeenage { get; set; }

        /// <summary>
        /// Gets or sets the transitory children.
        /// </summary>
        /// <value>
        /// The transitory children.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TransitoryChildren { get; set; }

        /// <summary>
        /// Gets or sets the connection adults.
        /// </summary>
        /// <value>
        /// The connection adults.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ConnectionAdults { get; set; }

        /// <summary>
        /// Gets or sets the connection teenage.
        /// </summary>
        /// <value>
        /// The connection teenage.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ConnectionTeenage { get; set; }

        /// <summary>
        /// Gets or sets the connection children.
        /// </summary>
        /// <value>
        /// The connection children.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ConnectionChildren { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic.
        /// </summary>
        /// <value>
        /// The diplomatic.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int Diplomatic { get; set; }

        /// <summary>
        /// Gets or sets the extra crew.
        /// </summary>
        /// <value>
        /// The extra crew.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ExtraCrew { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int Other { get; set; }

        /// <summary>
        /// Gets or sets the local baggage quantity.
        /// </summary>
        /// <value>
        /// The local baggage.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int LocalBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the transitory baggage quantity.
        /// </summary>
        /// <value>
        /// The transitory baggage quantity.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TransitoryBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the connection baggage quantity.
        /// </summary>
        /// <value>
        /// The connection baggage quantity.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ConnectionBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic baggage quantity.
        /// </summary>
        /// <value>
        /// The diplomatic baggage quantity.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int DiplomaticBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the extra crew baggage quantity.
        /// </summary>
        /// <value>
        /// The extra crew baggage quantity.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ExtraCrewBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the other baggage quantity.
        /// </summary>
        /// <value>
        /// The other baggage quantity.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int OtherBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the local baggage weight.
        /// </summary>
        /// <value>
        /// The local baggage weight.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int LocalBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the transitory baggage weight.
        /// </summary>
        /// <value>
        /// The transitory baggage weight.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int TransitoryBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the connection baggage weight.
        /// </summary>
        /// <value>
        /// The connection baggage weight.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ConnectionBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic.
        /// </summary>
        /// <value>
        /// The diplomatic.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int DiplomaticBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the extra crew baggage weight.
        /// </summary>
        /// <value>
        /// The extra crew baggage weight.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int ExtraCrewBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the other baggage weight.
        /// </summary>
        /// <value>
        /// The other baggage weight.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int OtherBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the observation.
        /// </summary>
        /// <value>
        /// The observation.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observations", ResourceType = typeof(Resources.Resource))]
        public string Observation { get; set; }

        /// <summary>
        /// Gets or sets the international TUA.
        /// </summary>
        /// <value>
        /// The international TUA.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        public int InternationalTua { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "ResponsableASC", ResourceType = typeof(Resources.Resource))]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the previous sequence.
        /// </summary>
        /// <value>
        /// The previous sequence.
        /// </value>
        public int? PreviousSequence { get; set; }

        /// <summary>
        /// Gets or sets the previous airline code.
        /// </summary>
        /// <value>
        /// The previous airline code.
        /// </value>
        public string PreviousAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the previous flight number.
        /// </summary>
        /// <value>
        /// The previous flight number.
        /// </value>
        public string PreviousFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous itinerary key.
        /// </summary>
        /// <value>
        /// The previous itinerary key.
        /// </value>
        public string PreviousItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [applies previous flight].
        /// </summary>
        /// <value>
        /// <c>true</c> if [applies previous flight]; otherwise, <c>false</c>.
        /// </value>
        public bool AppliesPreviousFlight { get; set; }

        /// <summary>
        /// Gets or sets User Information
        /// </summary>
        public UserVO User { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryVO ItineraryVo { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public AdditionalPassengerInformationVO AdditionalInformation { get; set; }
    }
}