//------------------------------------------------------------------------
// <copyright file="RemittanceStatus.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// RemittanceStatus Class
    /// </summary>
    [Table("Process.RemittanceStatus")]
    public partial class RemittanceStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemittanceStatus"/> class.
        /// </summary>
        public RemittanceStatus()
        {
            NationalJetFuelInvoiceControl = new List<NationalJetFuelInvoiceControl>();
        }

        /// <summary>
        /// Gets or sets the remittance status code.
        /// </summary>
        /// <value>
        /// The remittance status code.
        /// </value>
        [Key]
        [StringLength(4)]
        public string RemittanceStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the remittance status.
        /// </summary>
        /// <value>
        /// The name of the remittance status.
        /// </value>
        [Required]
        [StringLength(50)]
        public string RemittanceStatusName { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice control.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice control.
        /// </value>
        public virtual IList<NationalJetFuelInvoiceControl> NationalJetFuelInvoiceControl { get; set; }
    }
}
