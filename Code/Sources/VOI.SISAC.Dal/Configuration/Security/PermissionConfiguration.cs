

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
    /// Database configuration for Permission.
    /// </summary>
    public class PermissionConfiguration: EntityTypeConfiguration<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionConfiguration"/> class.
        /// </summary>
        public PermissionConfiguration()
        {
            // Properties
            this.Property(e => e.PermissionCode)
                .IsUnicode(false);

            this.Property(e => e.PermissionName)
                .IsUnicode(false);
            
            // Table & Column Mappings
            this.HasMany(e => e.ModulePermissions)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);
        }
    }
}
