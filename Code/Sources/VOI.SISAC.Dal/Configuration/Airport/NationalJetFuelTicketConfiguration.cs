//------------------------------------------------------------------------
// <copyright file="NationalJetFuelTicketConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelTicketConfiguration
    /// </summary>
    public class NationalJetFuelTicketConfiguration : EntityTypeConfiguration<NationalJetFuelTicket>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketConfiguration"/> class.
        /// </summary>
        public NationalJetFuelTicketConfiguration()
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

            Property(e => e.ApronPosition)
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
