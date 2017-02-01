//------------------------------------------------------------------------
// <copyright file="IModulePermissionRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Module Permission Repository Interface
    /// </summary>
    public interface IModulePermissionRepository : IRepository<ModulePermission>
    {
        /// <summary>
        /// Adds Module and its permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissions">The permissions.</param>
        void AddModuleAndPermissions(string moduleCode, IList<Permission> permissions);

        /// <summary>
        /// Adds Module and permission.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="permission">The permission.</param>
        void AddModulePermission(Module module, Permission permission);

        /// <summary>
        /// Gets the permission by module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>The permission for the module.</returns>
        IList<Permission> GetPermissionByModule(string moduleCode);

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissionCode">The permission code.</param>
        /// <returns>The module permission entity.</returns>
        ModulePermission FindBy(string moduleCode, string permissionCode);

        /// <summary>
        /// Gets the module permissions by module code.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>List of module permission.</returns>
        IList<ModulePermission> GetModulePermissionsByModuleCode(string moduleCode);

        /// <summary>
        /// Gets the module permission by permission code.
        /// </summary>
        /// <param name="permissionCode">The permission code.</param>
        /// <returns>List of module permission.</returns>
        IList<ModulePermission> GetModulePermissionByPermissionCode(string permissionCode);

        /// <summary>
        /// Deletes module and its permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissions">The permissions.</param>
        void DeleteModuleAndPermissions(string moduleCode, IList<Permission> permissions);
    }
}
