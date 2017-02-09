//------------------------------------------------------------------------
// <copyright file="ExchangeRates.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ExchangeRates Class
    /// </summary>
    [Table("Finance.ExchangeRates")]
    public partial class ExchangeRates
    {
        /// <summary>
        /// Constructor ExchangeRates
        /// </summary>
        public ExchangeRates()
        {

        }

        /// <summary>
        /// Constructor ExchangeRates
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="CurrencyCode"></param>
        public ExchangeRates(int Year, int Month, string CurrencyCode)
        {
            this.Year = Year;
            this.Month = Month;
            this.CurrencyCode = CurrencyCode;
        }

        /// <summary>
        /// Year
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Month { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Average Exchange Rate
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public virtual Currency Currency { get; set; }
    }
}
