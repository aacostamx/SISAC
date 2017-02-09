//--------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using VOI.SISAC.Entities.Finance;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// International FuelContract Concept Configuration
    /// </summary>
    public class InternationalFuelContractConceptConfiguration : EntityTypeConfiguration<InternationalFuelContractConcept>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InternationalFuelContractConceptConfiguration()
        {

            Property(e => e.AirlineCode)
            .IsUnicode(false);

            Property(e => e.StationCode)
                .IsUnicode(false);

            Property(e => e.ServiceCode)
                .IsUnicode(false);

            Property(e => e.ProviderNumberPrimary)
                .IsUnicode(false);

            Property(e => e.FuelConceptTypeCode)
                .IsUnicode(false);

            Property(e => e.ProviderNumber)
                .IsUnicode(false);

            HasMany(e => e.InternationalFuelRate)
                .WithRequired(e => e.InternationalFuelContractConcept)
                .WillCascadeOnDelete(true);
        }

    }
}
