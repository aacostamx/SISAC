//------------------------------------------------------------------------
// <copyright file="ProcedureCalculationConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using Entities.Catalog;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// ProcedureCalculationConfiguration Class
    /// </summary>
    public class ProcedureCalculationConfiguration : EntityTypeConfiguration<ProcedureCalculation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureCalculationConfiguration"/> class.
        /// </summary>
        public ProcedureCalculationConfiguration()
        {
            this.Property(e => e.ProcedureCode)
                .IsUnicode(false);

            this.Property(e => e.ProcedureName)
                .IsUnicode(false);
            
            this.Property(e => e.ProcedureDBName)
                .IsUnicode(false);
        }
    }
}
