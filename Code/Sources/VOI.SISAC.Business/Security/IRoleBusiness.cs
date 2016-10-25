//----------------------------------------------------------------------------
// <copyright file="IRoleBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// IRoleBusiness
    /// </summary>
    public interface IRoleBusiness
    {
        /// <summary>
        /// GetRoles
        /// </summary>
        /// <returns></returns>
        IList<RoleDto> GetRoles();

        /// <summary>
        /// FindRoleById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        RoleDto FindRoleById(string roleCode);

        /// <summary>
        /// FindRoleById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        RoleDto FindRoleByIdNoTracking(string roleCode);

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AddRole(RoleDto dto);

        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool DeleteRole(RoleDto dto);

        /// <summary>
        /// UpdateRole
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool UpdateRole(RoleDto dto);

        /// <summary>
        /// AddRoleModule
        /// </summary>
        /// <param name="roleModule"></param>
        /// <returns></returns>
        bool AddRoleModule(RoleDto roleModule);

        /// <summary>
        /// Edit Role Modules
        /// </summary>
        /// <param name="roleModule"></param>
        /// <returns></returns>
        bool EditRoleModule(RoleDto roleModule);

        /// <summary>
        /// Count User Profile Roles
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        int CountUserProfileRoles(string roleCode);
    }
}