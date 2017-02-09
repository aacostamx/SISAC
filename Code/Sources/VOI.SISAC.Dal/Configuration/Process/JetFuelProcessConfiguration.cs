//------------------------------------------------------------------------
// <copyright file="JetFuelProcessConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// JetFuelProcessConfiguration Class
    /// </summary>
    public class JetFuelProcessConfiguration : EntityTypeConfiguration<JetFuelProcess>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JetFuelProcessConfiguration()
        {

            Property(e => e.PeriodCode)
                .IsUnicode(false);

            Property(e => e.StatusProcessCode)
                .IsUnicode(false);


            Property(e => e.ProcessProgress)
                .HasPrecision(5, 2);


            Property(e => e.CalculationStatusCode)
                .IsUnicode(false);

            Property(e => e.ConfirmationStatusCode)
                .IsUnicode(false);

            HasMany(e => e.JetFuelLogErrors)
                .WithRequired(e => e.JetFuelProcess)
                .WillCascadeOnDelete(false);

            //HasMany(e => e.JetFuelProvisions)
            //    .WithRequired(e => e.JetFuelProcess)
            //    .WillCascadeOnDelete(false);

        }
    }
}
