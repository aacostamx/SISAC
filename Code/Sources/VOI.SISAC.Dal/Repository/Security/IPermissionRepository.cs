//------------------------------------------------------------------------
// <copyright file="IPermissionRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// IPermissionRepository
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>
    {
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Permission FindById(string permissionCode);

        /// <summary>
        /// GetPermits
        /// </summary>
        /// <returns></returns>
        IList<Permission> GetPermits();
    }
}
