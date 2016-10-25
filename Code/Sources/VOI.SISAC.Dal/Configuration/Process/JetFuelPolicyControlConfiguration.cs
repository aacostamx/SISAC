//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyControlConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    public class JetFuelPolicyControlConfiguration : EntityTypeConfiguration<JetFuelPolicyControl>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Process";

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorConfiguration"/> class.
        /// </summary>
        public JetFuelPolicyControlConfiguration()
        {
            // Defines the table's name and schema
            this.ToTable("JetFuelPolicyControl", this.schemaName);

            // Defines the primary key
            this.HasKey<long>(c => c.PolicyId);

            // Defines the columns' properties
            this.Property(c => c.PolicyId)
                .HasColumnName("PolicyID")
                .IsRequired();

            this.Property(c => c.CreationDate)
                .HasColumnName("CreationDate")
                .IsRequired();

            this.Property(c => c.Status)
                .HasColumnName("Status")
                .HasMaxLength(15)
                .IsRequired();

            this.Property(c => c.AirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ServiceCodes)
                .HasColumnName("ServiceCodes")
                .IsRequired();

            this.Property(c => c.ProviderCodes)
                .HasColumnName("ProviderCodes")
                .IsRequired();

            this.Property(c => c.AirportCodes)
                .HasColumnName("AirportCodes")
                .IsRequired();

            this.Property(c => c.StartDateReal)
                .HasColumnName("StartDateReal")
                .IsRequired();

            this.Property(c => c.EndDateReal)
                .HasColumnName("EndDateReal")
                .IsRequired();

            this.Property(c => c.StartDateComplementary)
                .HasColumnName("StartDateComplementary")
                .IsRequired();

            this.Property(c => c.EndDateComplementary)
                .HasColumnName("EndDateComplementary")
                .IsRequired();

            this.Property(c => c.DateBaseline)
                .HasColumnName("DateBaseline")
                .IsRequired();

            this.Property(c => c.DateBaseline)
                .HasColumnName("DateBaseline")
                .IsRequired();

            this.Property(c => c.DatePosting)
                .HasColumnName("DatePosting")
                .IsRequired();

            this.Property(c => c.HeaderText)
                .HasColumnName("HeaderText")
                .IsRequired();

            this.Property(c => c.ItemText)
                .HasColumnName("ItemText")
                .IsOptional();

            this.Property(c => c.SendByUserName)
                .HasColumnName("SendByUserName")
                .IsOptional();

            this.Property(c => c.ConfirmedByUserName)
                .HasColumnName("ConfirmedByUserName")
                .IsOptional();

            // Relationships
            this.HasMany(e => e.JetFuelPolicies)
                .WithRequired(e => e.JetFuelPolicyControl)
                .WillCascadeOnDelete(false);
        }
    }
}
