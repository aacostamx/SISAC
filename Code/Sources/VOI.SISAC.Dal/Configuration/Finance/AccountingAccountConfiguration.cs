//------------------------------------------------------------------------
// <copyright file="AccountingAccountConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

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
    /// Database configuration for AccountingAccount entity.
    /// </summary>
    public class AccountingAccountConfiguration : EntityTypeConfiguration<AccountingAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingAccountConfiguration"/> class.
        /// </summary>
        public AccountingAccountConfiguration()
        {
            // Name of the Table
            this.ToTable("AccountingAccount", "Finance");

            // Primary Key
            this.HasKey<string>(s => s.AccountingAccountNumber);

            // Relations for the properties
            this.Property(c => c.AccountingAccountNumber)
                .HasMaxLength(8)
                .HasColumnName("AccountingAccountNumber");

            this.Property(c => c.AccountingAccountName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("AccountingAccountName");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
