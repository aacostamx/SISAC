
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
    /// Database configuration for Module.
    /// </summary>
    public class ModulePermissionConfiguration : EntityTypeConfiguration<ModulePermission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePermissionConfiguration"/> class.
        /// </summary>
        public ModulePermissionConfiguration()
        {
            // Properties
            this.Property(e => e.ModuleCode)
                .IsUnicode(false);

            this.Property(e => e.PermissionCode)
                .IsUnicode(false);

            // Table & Column Mappings
            this.HasMany(e => e.Roles)
                .WithMany(e => e.ModulePermissions)
                .Map(m => m.ToTable("RoleModulePermission", "Security").MapLeftKey(new[] { "ModuleCode", "PermissionCode" }).MapRightKey("RoleCode"));

            this.HasMany(e => e.Users)
                .WithMany(e => e.ModulePermissions)
                .Map(m => m.ToTable("UserModulePermission", "Security").MapLeftKey(new[] { "ModuleCode", "PermissionCode" }).MapRightKey("UserID"));
        }
    }
}
