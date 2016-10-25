

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for User Profile Role.
    /// </summary>
    public class UserProfileRoleConfiguration: EntityTypeConfiguration<UserProfileRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRoleConfiguration"/> class.
        /// </summary>
        public UserProfileRoleConfiguration()
        {
            // Relationships

            this.Property(e => e.ProfileCode)
                .IsUnicode(false);

            this.Property(e => e.RoleCode)
                .IsUnicode(false);

        }
    }
}
