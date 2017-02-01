//------------------------------------------------------------------------
// <copyright file="ExchangeRatesDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    /// <summary>
    /// Data Transfer Object for Currency
    /// </summary>
    public class ExchangeRatesDto
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeRatesDto()
        {

        }

        /// <summary>
        /// ExchangeRatesDto Constructor
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="CurrencyCode"></param>
        public ExchangeRatesDto(int Year, int Month, string CurrencyCode)
        {
            this.Year = Year;
            this.Month = Month;
            this.CurrencyCode = CurrencyCode;
        }
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Average Exchange Rate
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public CurrencyDto Currency { get; set; }
    }
}
