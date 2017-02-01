//------------------------------------------------------------------------
// <copyright file="JetFuelTicketVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using Finance;
    using Itineraries;
    using Security;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// NationalJetFuelTicketVO Class
    /// </summary>
    public class NationalJetFuelTicketVO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketVO"/> class.
        /// </summary>
        public NationalJetFuelTicketVO() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketVO"/> class.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        public NationalJetFuelTicketVO(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            this.Sequence = Sequence;
            this.AirlineCode = AirlineCode;
            this.FlightNumber = FlightNumber;
            this.ItineraryKey = ItineraryKey;
            this.OperationTypeName = OperationTypeName;
            this.FuelingDateStart = DateTime.Now;
            this.FuelingDateEnd = DateTime.Now;
            this.FueledQtyLts = 0;
            this.RemainingQtyKgs = 0;
            this.RequestedQtyKgs = 0;
            this.FueledQtyKgs = 0;

        }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        [Display(Name = "ID")]
        public long NationalJetFuelTicketID { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        [Display(Name = "OperationTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Display(Name = "ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the fueling date start.
        /// </summary>
        /// <value>
        /// The fueling date start.
        /// </value>
        [Display(Name = "FuelingDateStart", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        public DateTime FuelingDateStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling time start.
        /// </summary>
        /// <value>
        /// The fueling time start.
        /// </value>
        [Display(Name = "FuelingTimeStart", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan FuelingTimeStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling date end.
        /// </summary>
        /// <value>
        /// The fueling date end.
        /// </value>
        [Display(Name = "FuelingDateEnd", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        public DateTime FuelingDateEnd { get; set; }

        /// <summary>
        /// Gets or sets the fueling time end.
        /// </summary>
        /// <value>
        /// The fueling time end.
        /// </value>
        [Display(Name = "FuelingTimeEnd", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan FuelingTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        [Display(Name = "ApronPosition", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider number.
        /// </summary>
        /// <value>
        /// The jet fuel provider number.
        /// </value>
        [Display(Name = "JetFuelProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string JetFuelProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider number.
        /// </summary>
        /// <value>
        /// The into plane provider number.
        /// </value>
        [Display(Name = "IntoPlaneProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string IntoPlaneProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        [Display(Name = "TicketNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax10")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty LTS.
        /// </summary>
        /// <value>
        /// The fueled qty LTS.
        /// </value>
        [Display(Name = "FueledQtyLts", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public int FueledQtyLts { get; set; }

        /// <summary>
        /// Gets or sets the remaining qty KGS.
        /// </summary>
        /// <value>
        /// The remaining qty KGS.
        /// </value>
        [Display(Name = "RemainingQtyKgs", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public int? RemainingQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the requested qty KGS.
        /// </summary>
        /// <value>
        /// The requested qty KGS.
        /// </value>
        [Display(Name = "RequestedQtyKgs", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public int? RequestedQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty KGS.
        /// </summary>
        /// <value>
        /// The fueled qty KGS.
        /// </value>
        [Display(Name = "FueledQtyKgs", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public int? FueledQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the density factor.
        /// </summary>
        /// <value>
        /// The density factor.
        /// </value>
        [Display(Name = "DensityFactor", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,5})+([\.\,])?([0-9]{1,3})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the aor user identifier.
        /// </summary>
        /// <value>
        /// The aor user identifier.
        /// </value>
        [Display(Name = "AorUserID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public int AorUserID { get; set; }

        /// <summary>
        /// Gets or sets the supplier responsible.
        /// </summary>
        /// <value>
        /// The supplier responsible.
        /// </value>
        [Display(Name = "SupplierResponsible", ResourceType = typeof(Resources.Resource))]
        public string SupplierResponsible { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax250")]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryVO Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider.
        /// </summary>
        /// <value>
        /// The jet fuel provider.
        /// </value>
        public ProviderVO JetFuelProvider { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider.
        /// </summary>
        /// <value>
        /// The into plane provider.
        /// </value>
        public ProviderVO IntoPlaneProvider { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ServiceVO Service { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserVO User { get; set; }
    }
}