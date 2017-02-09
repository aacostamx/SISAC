//----------------------------------------------------------------------------
// <copyright file="IPermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Security;
    
    /// <summary>
    /// PermissionBusiness
    /// </summary>
    public interface IPermissionBusiness
    {
        /// <summary>
        /// GetPermits
        /// </summary>
        /// <returns></returns>
        IList<PermissionDto> GetPermits();

        /// <summary>
        /// FindPermissionById
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        PermissionDto FindPermissionById(string permissionCode);

        /// <summary>
        /// AddPermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        bool AddPermission(PermissionDto permissionDto);

        /// <summary>
        /// DeletePermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        bool DeletePermission(PermissionDto permissionDto);

        /// <summary>
        /// UpdatePermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        bool UpdatePermission(PermissionDto permissionDto);
    }
}
