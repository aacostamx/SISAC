//------------------------------------------------------------------------
// <copyright file="AirportServiceConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// AirportServiceConfiguration
    /// </summary>
    public class AirportServiceConfiguration : EntityTypeConfiguration<AirportService>
    {
        /// <summary>
        /// AirportServiceConfiguration
        /// </summary>
        public AirportServiceConfiguration()
        {

            Property(e => e.AirlineCode)
            .IsUnicode(false);

            Property(e => e.FlightNumber)
            .IsUnicode(false);

            Property(e => e.ItineraryKey)
            .IsUnicode(false);

            Property(e => e.StationCode)
            .IsUnicode(false);

            Property(e => e.ServiceCode)
            .IsUnicode(false);

            Property(e => e.ProviderNumber)
            .IsUnicode(false);

            Property(e => e.ApronPosition)
            .IsUnicode(false);

            Property(e => e.GpuCode)
            .IsUnicode(false);

            Property(e => e.GpuObservationCode)
            .IsUnicode(false);

            Property(e => e.OperationTypeName)
            .IsUnicode(false);

            Property(e => e.Remarks)
            .IsUnicode(false);
        }
    }
}
