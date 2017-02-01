//------------------------------------------------------------------------
// <copyright file="CompartmentTypeInformationConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// CompartmentTypeInformation Configurations
    /// </summary>
    public class CompartmentTypeInformationConfiguration : EntityTypeConfiguration<CompartmentTypeInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentTypeInformationConfiguration"/> class.
        /// </summary>
        public CompartmentTypeInformationConfiguration()
        {
            // Table name, Schema
            this.ToTable("CompartmentTypeInformation", "Airport");

            // Primary key
            this.HasKey<int>(c => c.CompartmentTypeInformationID);

            // Columns configurations
            this.Property(c => c.CompartmentTypeInformationID)
                .IsRequired()
                .HasColumnName("CompartmentTypeInformationID");

            this.Property(c => c.CompartmentTypeCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("CompartmentTypeCode");

            this.Property(c => c.CompartmentTypeInformationName)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnName("CompartmentTypeInformationName");

            this.Property(c => c.CompartmentTypeInformationLevel)
                .IsRequired()
                .HasColumnName("CompartmentTypeInformationLevel");

            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<CompartmentType>(s => s.CompartmentType)
                .WithMany(s => s.CompartmentTypeInformations)
                .HasForeignKey(s => s.CompartmentTypeCode);
        }
    }
}
