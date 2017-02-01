//------------------------------------------------------------------------
// <copyright file="ReconciliationTolerance.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Reconciliation Tolerance
    /// </summary>
    [Table("Catalog.ReconciliationTolerance")]
    public partial class ReconciliationTolerance
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationTolerance"/> class.
        /// </summary>
        public ReconciliationTolerance()
        { 
        
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationTolerance"/> class.
        /// </summary>
        /// <param name="ServiceCode">The service code.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="ToleranceTypeCode">The tolerance type code.</param>
        public ReconciliationTolerance(string ServiceCode, string CurrencyCode, string ToleranceTypeCode)
        {
            this.ServiceCode = ServiceCode;
            this.CurrencyCode = CurrencyCode;
            this.ToleranceTypeCode = ToleranceTypeCode;
        }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the tolerance type code.
        /// </summary>
        /// <value>
        /// The tolerance type code.
        /// </value>
        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string ToleranceTypeCode { get; set; }


        /// <summary>
        /// Gets or sets the tolerance value.
        /// </summary>
        /// <value>
        /// The tolerance value.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal ToleranceValue { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReconciliationTolerance"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the type of the tolerance.
        /// </summary>
        /// <value>
        /// The type of the tolerance.
        /// </value>
        public virtual ToleranceType ToleranceType { get; set; }
    }
}
