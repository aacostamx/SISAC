

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for Profile.
    /// </summary>
    class ProfileConfiguration: EntityTypeConfiguration<Profile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileConfiguration"/> class.
        /// </summary>
        public ProfileConfiguration()
        {

            // Properties
            this.Property(e => e.ProfileCode)
                .IsUnicode(false);

            this.Property(e => e.ProfileName)
                .IsUnicode(false);


            // Table & Column Mappings
            this.HasMany(e => e.ProfileRoles)
                .WithRequired(e => e.Profile)
                .WillCascadeOnDelete(false);
        }
    }
}
