//------------------------------------------------------------------------
// <copyright file="DocumentStatus.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// DocumentStatus Class
    /// </summary>
    [Table("Process.DocumentStatus")]
    public partial class DocumentStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentStatus"/> class.
        /// </summary>
        public DocumentStatus()
        {
            this.NationalJetFuelInvoiceControl = new HashSet<NationalJetFuelInvoiceControl>();
        }

        /// <summary>
        /// Gets or sets the document status code.
        /// </summary>
        /// <value>
        /// The document status code.
        /// </value>
        [Key]
        [StringLength(4)]
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the document status.
        /// </summary>
        /// <value>
        /// The name of the document status.
        /// </value>
        [Required]
        [StringLength(50)]
        public string DocumentStatusName { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice control.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice control.
        /// </value>
        public virtual ICollection<NationalJetFuelInvoiceControl> NationalJetFuelInvoiceControl { get; set; }
    }
}
