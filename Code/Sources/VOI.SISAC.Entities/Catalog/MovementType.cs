//------------------------------------------------------------------------
// <copyright file="MovementType.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using Itineraries;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// MovementType Entity
    /// </summary>
    [Table("Catalog.MovementType")]
    public partial class MovementType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementType"/> class.
        /// </summary>
        public MovementType()
        {
            TimelineMovements = new List<TimelineMovement>();
        }

        /// <summary>
        /// Gets or sets the movement type code.
        /// </summary>
        /// <value>
        /// The movement type code.
        /// </value>
        [Key]
        [StringLength(5)]
        public string MovementTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the movement description.
        /// </summary>
        /// <value>
        /// The movement description.
        /// </value>
        [Required]
        [StringLength(100)]
        public string MovementDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MovementType"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the timeline movements.
        /// </summary>
        /// <value>
        /// The timeline movements.
        /// </value>
        public virtual IList<TimelineMovement> TimelineMovements { get; set; }
    }
}
