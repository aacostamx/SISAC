//------------------------------------------------------------------------
// <copyright file="Currency.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Entities.Airport
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// AirportGroup
    /// </summary>
    [Table("Airport.AirportGroup")]
    public partial class AirportGroup
    {
        /// <summary>
        /// AirportGroupCode
        /// </summary>
        [Key]
        [StringLength(8)]
        public string AirportGroupCode { get; set; }

        /// <summary>
        /// AirportGroupName
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AirportGroupName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Airport
        /// </summary>
        public virtual ICollection<Airport> Airport { get; set; }

        /// <summary>
        /// AirportGroup
        /// </summary>
        public AirportGroup()
        {
            Airport = new List<Airport>();
        }
    }
}

