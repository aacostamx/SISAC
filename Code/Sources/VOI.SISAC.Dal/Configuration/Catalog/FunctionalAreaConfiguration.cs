//------------------------------------------------------------------------
// <copyright file="FunctionalAreaConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// FunctionalArea Configuration
    /// </summary>
    public class FunctionalAreaConfiguration : EntityTypeConfiguration<FunctionalArea>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalAreaConfiguration"/> class.
        /// </summary>
        public FunctionalAreaConfiguration()
        {
            // Table name, Schema
            this.ToTable("FunctionalArea", "Catalog");

            // Primary key
            this.HasKey<long>(c => c.FunctionalAreaID);

            // Columns configurations
            this.Property(c => c.FunctionalAreaID)
                .HasColumnName("FunctionalAreaID");

            this.Property(c => c.FunctionalAreaName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FunctionalAreaName");

            this.Property(c => c.FunctionalAreaDescription)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FunctionalAreaDescription");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
