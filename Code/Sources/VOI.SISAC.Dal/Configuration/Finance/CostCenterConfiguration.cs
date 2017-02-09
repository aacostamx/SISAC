//------------------------------------------------------------------------
// <copyright file="CostCenterConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Class Configuration CostCenter
    /// </summary>
    public class CostCenterConfiguration : EntityTypeConfiguration<CostCenter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterConfiguration"/> class.
        /// </summary>
        public CostCenterConfiguration()
        {
            // Define table´s name and name schema 
            this.ToTable("CostCenter", "Finance");

            // Define Primary key
            this.HasKey<string>(c => c.CCNumber);

            // Define table´s properties
            this.Property(c => c.CCNumber)
                .HasMaxLength(14)
                .HasColumnName("CCNumber");

            this.Property(c => c.AirlineCode)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("AirlineCode");

            this.Property(c => c.CCName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("CCName");

            this.Property(c => c.Status)
                .HasColumnName("Status"); 
        }
    }   
}
