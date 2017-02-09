//------------------------------------------------------------------------
// <copyright file="ToleranceType.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Catalog.ToleranceType")]
    public partial class ToleranceType
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ToleranceType"/> class.
        /// </summary>
        public ToleranceType()
        {
            this.ReconciliationTolerances = new HashSet<ReconciliationTolerance>();
        }


        /// <summary>
        /// Gets or sets the tolerance type code.
        /// </summary>
        /// <value>
        /// The tolerance type code.
        /// </value>
        [Key]
        [StringLength(10)]
        public string ToleranceTypeCode { get; set; }


        /// <summary>
        /// Gets or sets the name of the tolerance type.
        /// </summary>
        /// <value>
        /// The name of the tolerance type.
        /// </value>
        [Required]
        [StringLength(50)]
        public string ToleranceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public virtual ICollection<ReconciliationTolerance> ReconciliationTolerances { get; set; }
    }
}
