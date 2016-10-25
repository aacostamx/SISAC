

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
    /// Database configuration for Profile Role.
    /// </summary>
    public class ProfileRoleConfiguration: EntityTypeConfiguration<ProfileRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRoleConfiguration"/> class.
        /// </summary>
        public ProfileRoleConfiguration()
        {
            // Properties
            // Table & Column Mappings
            this.Property(e => e.ProfileCode)
                .IsUnicode(false);

            this.Property(e => e.RoleCode)
                .IsUnicode(false);

            //relaciones
            this.HasMany(e => e.UserProfileRoles)
                .WithRequired(e => e.ProfileRoles)
                .HasForeignKey(e => new { e.ProfileCode, e.RoleCode })
                .WillCascadeOnDelete(false);
        }
    }
}
