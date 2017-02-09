//------------------------------------------------------------------------
// <copyright file="ChargeFactorTypeConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class
    /// </summary>
    public class ChargeFactorTypeConfiguration : EntityTypeConfiguration<ChargeFactorType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChargeFactorTypeConfiguration()
        {
            Property(e => e.ChargeFactorTypeName)
            .IsUnicode(false);

            HasMany(e => e.InternationalFuelContractConcept)
            .WithRequired(e => e.ChargeFactorType)
            .WillCascadeOnDelete(false);
        }
    }
}
