//------------------------------------------------------------------------
// <copyright file="ProcedureCalculation.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ProcedureCalculation class
    /// </summary>
    [Table("Catalog.ProcedureCalculation")]
    public partial class ProcedureCalculation
    {
        /// <summary>
        /// Gets or sets the procedure code.
        /// </summary>
        /// <value>
        /// The procedure code.
        /// </value>
        [Key]
        [StringLength(15)]
        public string ProcedureCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        [Required]
        [StringLength(150)]
        public string ProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the name of the procedure database.
        /// </summary>
        /// <value>
        /// The name of the procedure database.
        /// </value>
        [Required]
        [StringLength(150)]
        public string ProcedureDBName { get; set; }
    }
}
