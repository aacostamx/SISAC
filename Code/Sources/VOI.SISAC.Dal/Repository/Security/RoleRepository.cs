//------------------------------------------------------------------------
// <copyright file="RoleRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// RoleRepository
    /// </summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public RoleRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #region RoleRepository Members

        /// <summary>
        /// AddRoleModule
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="modules">The modules.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        public bool AddRoleModule(Role role, IList<ModulePermission> modules)
        {
            this.DbContext.Roles.Attach(role);
            foreach (var item in modules)
            {
                ModulePermission module = this.DbContext.ModulePermissions.
                    Where(c => c.ModuleCode == item.ModuleCode && c.PermissionCode == item.PermissionCode)
                    .FirstOrDefault<ModulePermission>();

                if (module != null)
                {
                    this.DbContext.ModulePermissions.Attach(module);
                    role.ModulePermissions.Add(module);
                }
            }

            return true;
        }

        /// <summary>
        /// Count User Profile Roles
        /// </summary>
        /// <returns>Information of <see cref="Role"/></returns>
        public Role RoleUserProfileRoles(string roleCode)
        {
            var role = this.DbContext.Roles
                .Where(c => c.RoleCode == roleCode)
                .Include(c => c.ProfileRoles.Select(p => p.UserProfileRoles))
                .FirstOrDefault();

            return role;
        }

        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="roleCode">The role code.</param>
        /// <returns>Information of <see cref="Role"/></returns>
        public Role FindById(string roleCode)
        {
            var role = this.DbContext.Roles
                .Where(c => c.RoleCode == roleCode)
                .Include(c => c.ModulePermissions)
                .Include(c => c.ProfileRoles)
                .FirstOrDefault();

            return role;
        }

        /// <summary>
        /// Find by Id No tracking
        /// </summary>
        /// <param name="roleCode">The role code</param>
        /// <returns>Information of <see cref="Role"/></returns>
        public Role FindByIdNoTracking(string roleCode)
        {
            var role = this.DbContext.Roles.AsNoTracking()
                .Where(c => c.RoleCode == roleCode)
                .Include(c => c.ModulePermissions)
                .FirstOrDefault();
            return role;
        }

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <returns>List of Roles.</returns>
        public IList<Role> GetRoles()
        {
            return this.DbContext.Roles.Include(c => c.ModulePermissions).ToList();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public override void Delete(Role entity)
        {
            List<UserProfileRole> userProfileRole = this.DbContext.UserProfileRoles
                .Where(c => c.RoleCode == entity.RoleCode)
                .Include(c => c.ProfileRoles)
                .ToList();

            this.DbContext.UserProfileRoles.RemoveRange(userProfileRole);
            this.DbContext.Roles.Remove(entity);
        }
        #endregion
    }
}
