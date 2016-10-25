//------------------------------------------------------------------------
// <copyright file="PageTicketConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Security
{ 
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Security;

    public class PageReportConfiguration : EntityTypeConfiguration<PageReport>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageReportConfiguration"/> class.
        /// </summary>
        public PageReportConfiguration()
        {
            // Name of the Table
            this.ToTable("PageReport", "Security");

            // Primary Key
            this.HasKey<string>(s => s.PageName);

            
            this.Property(c => c.PageName)
                .HasMaxLength(100)
                .HasColumnName("PageName");

            this.Property(c => c.PathReport)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("PathReport");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
