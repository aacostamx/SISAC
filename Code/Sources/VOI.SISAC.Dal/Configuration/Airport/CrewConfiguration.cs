//------------------------------------------------------------------------
// <copyright file="CrewConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Catalog;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Class Configuration Crew
    /// </summary>
    public class CrewConfiguration : EntityTypeConfiguration<Crew>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Airport";

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewConfiguration"/> class.
        /// </summary>
        public CrewConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("Crew", this.schemaName);

            // Define Primary key
            this.HasKey<long>(a => a.CrewID);

            // Define table´s properties
            this.Property(a => a.CrewID)
                .HasColumnName("CrewID");

            this.Property(a => a.CrewTypeID)
               .HasMaxLength(4)
               .IsRequired()
               .HasColumnName("CrewTypeID");

            this.Property(a => a.LastName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("LastName");

            this.Property(a => a.FirstName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("FirstName");

            this.Property(a => a.MiddleName)
                .HasMaxLength(100)
                .IsOptional()
                .HasColumnName("MiddleName");

            this.Property(a => a.Gender)
                .HasMaxLength(1)
                .IsRequired()
                .HasColumnName("Gender");

            this.Property(a => a.CountryOfResidence)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("CountryOfResidence");

            this.Property(a => a.PlaceBirthCity)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("PlaceBirthCity");

            this.Property(a => a.State)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("State");

            this.Property(a => a.CountryOfBird)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("CountryOfBird");

            this.Property(a => a.DateOfBird)
                .IsRequired()
                .HasColumnName("DateOfBird");

            this.Property(a => a.Citizenship)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("Citizenship");

            this.Property(a => a.StatusOnBoardCode)
                .HasMaxLength(5)
                .IsRequired()
                .HasColumnName("StatusOnBoardCode");

            this.Property(a => a.HomeAddress)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("HomeAddress");

            this.Property(a => a.HomeAddressCity)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("HomeAddressCity");

            this.Property(a => a.HomeAddressState)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("HomeAddressState");

            this.Property(a => a.HomeAddressZipCode)
                .HasMaxLength(10)
                .IsRequired()
                .HasColumnName("HomeAddressZipCode");

            this.Property(a => a.HomeAddressCountry)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("HomeAddressCountry");

            this.Property(a => a.PassportNumber)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("PassportNumber");

            this.Property(a => a.PassportCountryIssuance)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("PassportCountryIssuance");

            this.Property(a => a.PassportExpiration)
                .IsRequired()
                .HasColumnName("PassportExpiration");

            this.Property(a => a.LicenceNumber)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("LicenceNumber");

            this.Property(a => a.LicenceCountryIssuance)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("LicenceCountryIssuance");

            this.Property(a => a.LicenceNumberExpiration)
                .IsRequired()
                .HasColumnName("LicenceNumberExpiration");

            this.Property(a => a.NickName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("NickName");

            this.Property(a => a.NickNameSabre)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("NickNameSabre");

            this.Property(a => a.VisaExpirationDate)
                .IsRequired()
                .HasColumnName("VisaExpirationDate");

            this.Property(a => a.EmployeeNumber)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("EmployeeNumber");

            this.Property(a => a.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate");

            this.Property(a => a.Status)
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<Country>(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryOfBird);

            this.HasMany<GendecDeparture>(e => e.GendecDeparturesCrew)
               .WithMany(e => e.Crews)
               .Map(m => m.ToTable("GendecDepartureCrew", "Itinerary")
               .MapLeftKey("CrewID")
               .MapRightKey(new[] { "Sequence", "AirlineCode", "FlightNumber", "ItineraryKey" }));

            this.HasMany<GendecArrival>(f => f.GendecArrivalsCrew)
                .WithMany(f => f.Crews)
                .Map(m => m.ToTable("GendecArrivalCrew", "Itinerary")
               .MapLeftKey("CrewID")
               .MapRightKey(new[] { "Sequence", "AirlineCode", "FlightNumber", "ItineraryKey" }));

        }
    }
}
