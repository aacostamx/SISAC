//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelProcessConfiguration Class
    /// </summary>
    public class NationalJetFuelProcessConfiguration : EntityTypeConfiguration<NationalJetFuelProcess>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcessConfiguration"/> class.
        /// </summary>
        public NationalJetFuelProcessConfiguration()
        {
            this.Property(e => e.PeriodCode)
            .IsUnicode(false);

            this.Property(e => e.StatusProcessCode)
            .IsUnicode(false);

            this.Property(e => e.ProcessProgress)
            .HasPrecision(5, 2);

            this.Property(e => e.CalculationStatusCode)
            .IsUnicode(false);

            this.Property(e => e.ConfirmationStatusCode)
            .IsUnicode(false);

            this.HasMany(e => e.NationalJetFuelCosts)
            .WithRequired(e => e.NationalJetFuelProcess)
            .WillCascadeOnDelete(false);

            this.HasMany(e => e.NationalJetFuelLogErrors)
            .WithRequired(e => e.NationalJetFuelProcess)
            .WillCascadeOnDelete(false);
        }
    }
}
