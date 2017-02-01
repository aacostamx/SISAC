//------------------------------------------------------------------------
// <copyright file="JetFuelTicket.cs" company="Volaris">
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
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// JetFuelTicket Entity
    /// </summary>
    [Table("Airport.JetFuelTicket")]
    public class JetFuelTicket
    {
        #region Properties for columns

        public long JetFuelTicketID { get; set; }

        public int Sequence { get; set; }

        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        [Required]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        [Required]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        [Required]
        [StringLength(20)]
        public string OperationTypeName { get; set; }

        [Required]
        [StringLength(12)]
        public string ServiceCode { get; set; }
        
        public DateTime FuelingDate { get; set; }

        public TimeSpan FuelingTime { get; set; }

        [Required]
        [StringLength(8)]
        public string JetFuelProviderNumber { get; set; }

        [Required]
        [StringLength(8)]
        public string IntoPlaneProviderNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string TicketNumber { get; set; }

        public int FueledQtyGals { get; set; }

        public int? RemainingQry { get; set; }

        public int? RequestedQry { get; set; }

        public int? FueledQry { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DensityFactor { get; set; }

        public int AorUserID { get; set; }

        [StringLength(150)]
        public string SupplierResponsible { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }


        #endregion

        #region Properties for relationships

        /// <summary>
        /// Provider
        /// </summary>
        public virtual Provider JetFuelProvider { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public virtual Provider IntoPlaneProvider { get; set; }


        /// <summary>
        /// Itinerary
        /// </summary>
        public virtual Itinerary Itinerary { get; set; }
        
        /// <summary>
        /// User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public virtual Service Service { get; set; }

        #endregion
    }
}
