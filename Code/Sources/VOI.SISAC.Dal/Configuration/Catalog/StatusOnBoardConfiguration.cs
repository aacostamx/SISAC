//------------------------------------------------------------------------
// <copyright file="StatusOnBoardConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class Status On Board Configuration
    /// </summary>
    public class StatusOnBoardConfiguration : EntityTypeConfiguration<StatusOnBoard>
    {
        public StatusOnBoardConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("StatusOnBoard", "Catalog");

            // Define Primary key
            this.HasKey<string>(c => c.StatusOnBoardCode);

            // Define table´s properties
            this.Property(c => c.StatusOnBoardCode)
                .HasMaxLength(5)
                .HasColumnName("StatusOnBoardCode");

            this.Property(c => c.StatusOnBoardName)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("StatusOnBoardName");
        }
    }
}
