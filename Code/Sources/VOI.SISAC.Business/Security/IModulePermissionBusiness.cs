//------------------------------------------------------------------------
// <copyright file="IModulePermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// Module Permission Business Interface
    /// </summary>
    public interface IModulePermissionBusiness
    {
        /// <summary>
        /// Adds the module permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissionsDto">The permissions.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c></returns>
        bool UpdatesModuleAndPermissions(string moduleCode, IList<PermissionDto> permissionsDto);

        /// <summary>
        /// Gets the permission by module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// The permission for the module.
        /// </returns>
        IList<GenericCatalogDto> GetPermissionByModule(string moduleCode);

        /// <summary>
        /// Gets all the permissions and marks the permissions that are selected for the module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// The permission for the module.
        /// </returns>
        IList<PermissionMarkedByModuleDto> GetAllPermissionByModule(string moduleCode);

        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <returns></returns>
        IList<ModulePermissionDto> GetAllModules();
    }
}
