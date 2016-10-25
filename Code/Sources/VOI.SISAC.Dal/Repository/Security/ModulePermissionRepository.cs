//------------------------------------------------------------------------
// <copyright file="ModulePermissionRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Module Permission Repository
    /// </summary>
    public class ModulePermissionRepository : Repository<ModulePermission>, IModulePermissionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePermissionRepository" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ModulePermissionRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Adds Module and its permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissions">The permissions.</param>
        public void AddModuleAndPermissions(string moduleCode, IList<Permission> permissions)
        {
            foreach (Permission item in permissions)
            {
                this.DbContext.ModulePermissions.Add(new ModulePermission
                {
                    ModuleCode = moduleCode,
                    PermissionCode = item.PermissionCode
                });
            }
        }

        /// <summary>
        /// Adds Module and permission.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="permission">The permission.</param>
        public void AddModulePermission(Module module, Permission permission)
        {
            this.DbContext.ModulePermissions.Add(new ModulePermission
            {
                ModuleCode = module.ModuleCode,
                PermissionCode = permission.PermissionCode
            });
        }

        /// <summary>
        /// Gets the permission by module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>The permission for the module.</returns>
        public IList<Permission> GetPermissionByModule(string moduleCode)
        {
            List<string> permissionCode = this.DbContext.ModulePermissions
                .Where(c => c.ModuleCode == moduleCode)
                .Select(d => d.PermissionCode)
                .ToList();

            List<Permission> permissions = this.DbContext.Permissions
                .Where(c => permissionCode.Contains(c.PermissionCode))
                .ToList();

            return permissions;
        }

        /// <summary>
        /// Deletes module and its permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissions">The permissions.</param>
        public void DeleteModuleAndPermissions(string moduleCode, IList<Permission> permissions)
        {
            foreach (Permission item in permissions)
            {
                ModulePermission modulePermission = this.DbContext.ModulePermissions
                    .FirstOrDefault(c => c.ModuleCode == moduleCode && c.PermissionCode == item.PermissionCode);
                this.DbContext.ModulePermissions.Remove(modulePermission);
            }
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissionCode">The permission code.</param>
        /// <returns>
        /// The module permission entity.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ModulePermission FindBy(string moduleCode, string permissionCode)
        {
            return this.DbContext.ModulePermissions
                .Include(c => c.Roles)
                .Include(c => c.Users)
                .FirstOrDefault(c => c.ModuleCode == moduleCode && c.PermissionCode == permissionCode);
        }

        /// <summary>
        /// Gets the module permissions by module code.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// List of module permission.
        /// </returns>
        public IList<ModulePermission> GetModulePermissionsByModuleCode(string moduleCode)
        {
            return this.DbContext.ModulePermissions.Where(c => c.ModuleCode == moduleCode).ToList();
        }

        /// <summary>
        /// Gets the module permission by permission code.
        /// </summary>
        /// <param name="permissionCode">The permission code.</param>
        /// <returns>
        /// List of module permission.
        /// </returns>
        public IList<ModulePermission> GetModulePermissionByPermissionCode(string permissionCode)
        {
            return this.DbContext.ModulePermissions.Where(c => c.PermissionCode == permissionCode).ToList();
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>List of module permissions.</returns>
        public override IList<ModulePermission> GetAll()
        {
            IList<ModulePermission> modules = this.DbContext.ModulePermissions
                .Include(c => c.Module)
                .Include(c => c.Module.Menu)
                .Include(c => c.Permission)
                .ToList();
            return modules;
        }
    }
}
