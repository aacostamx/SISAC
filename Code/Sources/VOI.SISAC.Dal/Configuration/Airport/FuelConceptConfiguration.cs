//------------------------------------------------------------------------
// <copyright file="FuelConceptConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Database configuration for FuelConcept entity.
    /// </summary>
    public class FuelConceptConfiguration : EntityTypeConfiguration<FuelConcept>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConceptConfiguration"/> class.
        /// </summary>
        public FuelConceptConfiguration()
        {
            // Name of the Table
            this.ToTable("FuelConcept", "Airport");
            
            // Primary Key
            this.HasKey<long>(s => s.FuelConceptID);

            // Relations for the properties
            this.Property(c => c.FuelConceptID)
                .HasColumnName("FuelConceptID");

            this.Property(c => c.FuelConceptName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("FuelConceptName");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
