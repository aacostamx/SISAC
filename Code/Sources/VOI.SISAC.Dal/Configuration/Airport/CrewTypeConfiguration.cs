//------------------------------------------------------------------------
// <copyright file="CrewTypeConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------

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
    /// Class CrewTypeConfiguration
    /// </summary>
    public class CrewTypeConfiguration : EntityTypeConfiguration<CrewType>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Airport";

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewTypeConfiguration"/> class.
        /// </summary>
        public CrewTypeConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("CrewType", this.schemaName);

            // Define Primary key
            this.HasKey<string>(a => a.CrewTypeID);

            // Define table´s properties
            this.Property(a => a.CrewTypeID)
                .HasMaxLength(4)
                .IsRequired()
                .HasColumnName("CrewTypeID");

            this.Property(a => a.CrewTypeName)
               .HasMaxLength(50)
               .IsRequired()
               .HasColumnName("CrewTypeName");
        }
    }
}
