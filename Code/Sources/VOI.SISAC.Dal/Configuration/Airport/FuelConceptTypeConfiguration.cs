//------------------------------------------------------------------------
// <copyright file="FuelConceptTypeConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Fuel Concept Type Configuration
    /// </summary>
    public class FuelConceptTypeConfiguration : EntityTypeConfiguration<FuelConceptType>
    {
        /// <summary>
        /// Fuel Concept Type Constructor
        /// </summary>
        public FuelConceptTypeConfiguration()
        {
            Property(e => e.FuelConceptTypeCode)
              .IsUnicode(false);

            Property(e => e.FuelConceptTypeName)
                .IsUnicode(false);

            HasMany(e => e.InternationalFuelContractConcept)
                .WithRequired(e => e.FuelConceptType)
                .WillCascadeOnDelete(false);

        }
    }
}
