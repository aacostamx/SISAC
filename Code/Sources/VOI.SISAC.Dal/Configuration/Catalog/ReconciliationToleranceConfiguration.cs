//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// ReconciliationToleranceConfiguration class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VOI.SISAC.Entities.Catalog.ReconciliationTolerance}" />
    public class ReconciliationToleranceConfiguration : EntityTypeConfiguration<ReconciliationTolerance>
    {

        public ReconciliationToleranceConfiguration()
        {
            this.Property(e => e.ServiceCode)
                .IsUnicode(false);

            this.Property(e => e.CurrencyCode)
                .IsUnicode(false);

            this.Property(e => e.ToleranceTypeCode)
                .IsUnicode(false);

            this.Property(e => e.ToleranceValue)
                .HasPrecision(8, 2);
        }
    }
}
