//------------------------------------------------------------------------
// <copyright file="CalculationStatusConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// CalculationStatusConfiguration Class
    /// </summary>
    public class CalculationStatusConfiguration : EntityTypeConfiguration<CalculationStatus>
    {
        /// <summary>
        /// CalculationStatusConfiguration constructor
        /// </summary>
        public CalculationStatusConfiguration()
        {
            Property(e => e.CalculationStatusCode)
                .IsUnicode(false);

            Property(e => e.CalculationStatusName)
                .IsUnicode(false);
        }
    }
}
