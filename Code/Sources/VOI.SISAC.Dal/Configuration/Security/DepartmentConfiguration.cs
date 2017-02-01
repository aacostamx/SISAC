// <copyright file="CompanyDepartmentConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for Company Department.
    /// </summary>
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentConfiguration"/> class.
        /// </summary>
        public DepartmentConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentId);

            // Properties
            this.Property(t => t.CompanyCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DescriptionArea)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DescriptionDepartment)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Department", "Security");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.CompanyCode).HasColumnName("CompanyCode");
            this.Property(t => t.DescriptionArea).HasColumnName("DescriptionArea");
            this.Property(t => t.DescriptionDepartment).HasColumnName("DescriptionDepartment");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
