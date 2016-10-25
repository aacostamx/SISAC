namespace VOI.SISAC.Entities.Airport
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// GpuObservation
    /// </summary>
    [Table("Airport.GpuObservation")]
    public class GpuObservation
    {
        /// <summary>
        /// GpuObservationCode
        /// </summary>
        [Key]
        [StringLength(10)]
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// GpuObservationCodeName
        /// </summary>
        [Required]
        [StringLength(150)]
        public string GpuObservationCodeName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }
}
