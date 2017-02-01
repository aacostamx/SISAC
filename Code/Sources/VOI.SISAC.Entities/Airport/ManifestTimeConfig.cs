//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfig.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Entity Manifiest Time Configuration Class 
    /// </summary>
    [Table("Airport.ManifestTimeConfig")]
    public partial class ManifestTimeConfig
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long ManifestTimeConfigID { get; set; }

        /// <summary>
        /// Airline Code
        /// </summary>
        [Required]
        [StringLength(2)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Arrival Minuest Min
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal ArrivalMinutesMin { get; set; }

        /// <summary>
        /// Arrival Minutes Max
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal ArrivalMinutesMax { get; set; }

        /// <summary>
        /// Departure Minutes Min
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal DepartureMinutesMin { get; set; }


        /// <summary>
        /// Departure Minutes Max
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal DepartureMinutesMax { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Airline
        /// </summary>
        public virtual Airline Airline { get; set; }
    }
}
