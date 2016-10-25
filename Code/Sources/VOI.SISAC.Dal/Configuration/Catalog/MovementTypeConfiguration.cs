//------------------------------------------------------------------------
// <copyright file="MovementTypeConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;


    /// <summary>
    /// MovementTypeConfiguration
    /// </summary>
    public class MovementTypeConfiguration : EntityTypeConfiguration<MovementType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementTypeConfiguration"/> class.
        /// </summary>
        public MovementTypeConfiguration()
        {
            this.Property(e => e.MovementTypeCode)
                .IsUnicode(false);

            this.Property(e => e.MovementDescription)
                .IsUnicode(false);

            this.HasMany(e => e.TimelineMovements)
                .WithRequired(e => e.MovementType)
                .WillCascadeOnDelete(false);
        }
    }
}
