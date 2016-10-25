//------------------------------------------------------------------------
// <copyright file="ServiceConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Service configuration
    /// </summary>
    public class ServiceConfiguration : EntityTypeConfiguration<Service>
    {
        /// <summary>
        /// Service Configuration Object
        /// </summary>
        public ServiceConfiguration()
        {
            this.ToTable("Service", "Airport");

            this.HasKey<string>(c => c.ServiceCode);

            this.Property(c => c.ServiceCode)
                .HasMaxLength(12)
                .HasColumnName("ServiceCode");

            this.Property(c => c.ServiceName)
                .HasMaxLength(150)
                .HasColumnName("ServiceName");

            this.Property(c => c.Status)
                .HasColumnName("Status");

            ///Configuracion JetFuelTicket Service
            this.HasMany(e => e.JetFuelTickets)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ServiceCode)
                .WillCascadeOnDelete(false);
        }
    }
}
