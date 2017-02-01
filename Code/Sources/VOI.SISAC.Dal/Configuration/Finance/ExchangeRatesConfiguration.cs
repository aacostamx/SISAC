//------------------------------------------------------------------------
// <copyright file="ExchangeRatesConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using Entities.Finance;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// ExchangeRatesConfiguration Class
    /// </summary>
    public class ExchangeRatesConfiguration : EntityTypeConfiguration<ExchangeRates>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeRatesConfiguration()
        {
            this.Property(e => e.CurrencyCode)
                .IsUnicode(false);

            this.Property(e => e.Rate)
                .HasPrecision(18, 5);
        }
    }
}
