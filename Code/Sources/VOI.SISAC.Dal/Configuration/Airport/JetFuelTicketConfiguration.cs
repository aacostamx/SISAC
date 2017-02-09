//------------------------------------------------------------------------
// <copyright file="JetFuelTicketConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// JetFuelTicketConfiguration
    /// </summary>
    public class JetFuelTicketConfiguration : EntityTypeConfiguration<JetFuelTicket>
    {
        /// <summary>
        /// JetFuelTicketConfiguration
        /// </summary>
        public JetFuelTicketConfiguration()
        {
            Property(e => e.AirlineCode)
                .IsUnicode(false);

            Property(e => e.FlightNumber)
                .IsUnicode(false);

            Property(e => e.ItineraryKey)
                .IsUnicode(false);

            Property(e => e.OperationTypeName)
                .IsUnicode(false);

            Property(e => e.ServiceCode)
                .IsUnicode(false);

            Property(e => e.JetFuelProviderNumber)
                .IsUnicode(false);

            Property(e => e.IntoPlaneProviderNumber)
                .IsUnicode(false);

            Property(e => e.TicketNumber)
                .IsUnicode(false);

            Property(e => e.DensityFactor)
                .HasPrecision(8, 3);

            Property(e => e.SupplierResponsible)
                .IsUnicode(false);

            Property(e => e.Remarks)
                .IsUnicode(false);
        }
    }
}
