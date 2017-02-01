//------------------------------------------------------------------------
// <copyright file="JetFuelTicketVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using VOI.SISAC.Web.Models.VO.Security;

    /// <summary>
    /// Jet Fuel Ticket VO
    /// </summary>
    public class JetFuelTicketVO
    {
        #region Properties for columns

        /// <summary>
        /// JetFuelTicketID
        /// </summary>
        public long JetFuelTicketID { get; set; }

        /// <summary>
        /// Sequence
        /// </summary>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int Sequence { get; set; }

        /// <summary>
        /// AirlineCode
        /// </summary>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// FlightNumber
        /// </summary>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// ItineraryKey
        /// </summary>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// OperationTypeName
        /// </summary>
        [Display(Name = "OperationTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        [Display(Name = "ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// FuelingDate
        /// </summary>
        [Display(Name = "FuelingDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
            , ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime FuelingDate { get; set; }

        /// <summary>
        /// Opening time
        /// </summary>
        [Display(Name = "FuelingTime", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan FuelingTime { get; set; }

        /// <summary>
        /// JetFuelProviderNumber
        /// </summary>
        [Display(Name = "JetFuelProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string JetFuelProviderNumber { get; set; }

        /// <summary>
        /// IntoPlaneProviderNumber
        /// </summary>
        [Display(Name = "IntoPlaneProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string IntoPlaneProviderNumber { get; set; }

        /// <summary>
        /// TicketNumber
        /// </summary>
        [Display(Name = "TicketNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax10")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// FueledQtyGals
        /// </summary>
        [Display(Name = "FueledQtyGals", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public int FueledQtyGals { get; set; }

        /// <summary>
        /// RemainingQry
        /// </summary>
        [Display(Name = "RemainingQry", ResourceType = typeof(Resources.Resource))]
        [Range(0, int.MaxValue)]
        public int? RemainingQry { get; set; }

        /// <summary>
        /// RequestedQry
        /// </summary>
        [Display(Name = "RequestedQry", ResourceType = typeof(Resources.Resource))]
        [Range(0, int.MaxValue)]
        public int? RequestedQry { get; set; }

        /// <summary>
        /// FueledQry
        /// </summary>
        [Display(Name = "FueledQry", ResourceType = typeof(Resources.Resource))]
        [Range(0, int.MaxValue)]
        public int? FueledQry { get; set; }

        /// <summary>
        /// DensityFactor
        /// </summary>
        [Display(Name = "DensityFactor", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,5})+([\.\,])?([0-9]{1,3})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// AorUserID
        /// </summary>
        [Display(Name = "AorUserID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int AorUserID { get; set; }

        /// <summary>
        /// SupplierResponsible
        /// </summary>
        [Display(Name = "SupplierResponsible", ResourceType = typeof(Resources.Resource))]
        public string SupplierResponsible { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resource))]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax250")]
        public string Remarks { get; set; }

        #endregion

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderVO JetFuelProvider { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderVO IntoPlaneProvider { get; set; }

        /// <summary>
        /// UserAor
        /// </summary>
        public UserVO User { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public ServiceVO Service { get; set; }

        /// <summary>
        /// Itinerary
        /// </summary>
        public ItineraryVO Itinerary { get; set; }
    }
}