//------------------------------------------------------------------------
// <copyright file="Gpu.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Gpu class
    /// </summary>
    [Table("Airport.Gpu")]
    public partial class Gpu
    {
        /// <summary>
        /// GpuCode
        /// </summary>
        [Key]
        [StringLength(50)]
        public string GpuCode { get; set; }

        /// <summary>
        /// GpuName
        /// </summary>
        [Required]
        [StringLength(150)]
        public string GpuName { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Required]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Airport
        /// </summary>
        public virtual Airport Airport { get; set; }

    }
}
