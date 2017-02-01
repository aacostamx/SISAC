//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyControlConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelPolicyControlConfiguration Class
    /// </summary>
    public class NationalJetFuelPolicyControlConfiguration : EntityTypeConfiguration<NationalJetFuelPolicyControl>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyControlConfiguration"/> class.
        /// </summary>
        public NationalJetFuelPolicyControlConfiguration()
        {
            this.Property(e => e.DocumentType)
                .IsUnicode(false);

            this.Property(e => e.Status)
                 .IsUnicode(false);

            this.Property(e => e.AirlineCode)
                 .IsUnicode(false);

            this.Property(e => e.ServiceCodes)
                 .IsUnicode(false);

            this.Property(e => e.ProviderCodes)
                 .IsUnicode(false);

            this.Property(e => e.AirportCodes)
                 .IsUnicode(false);

            this.Property(e => e.HeaderText)
                 .IsUnicode(false);

            this.Property(e => e.ItemText)
                 .IsUnicode(false);

            this.HasMany(e => e.NationalJetFuelPolicy)
                 .WithRequired(e => e.NationalJetFuelPolicyControl)
                 .WillCascadeOnDelete(false);
        }
    }
}
