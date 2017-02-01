//------------------------------------------------------------------------
// <copyright file="IRoleRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// IRoleRepository
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Role FindById(string roleCode);

        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Role FindByIdNoTracking(string roleCode);

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <returns></returns>
        IList<Role> GetRoles();

        /// <summary>
        /// Add Role Modules
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        bool AddRoleModule(Role roles, IList<ModulePermission> modules);

        /// <summary>
        /// Role User Profile Roles
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Role RoleUserProfileRoles(string roleCode);
    }
}
