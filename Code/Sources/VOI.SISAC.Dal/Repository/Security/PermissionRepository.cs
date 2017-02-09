//------------------------------------------------------------------------
// <copyright file="PermissionRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// PermissionRepository
    /// </summary>
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class. 
        /// </summary>
        /// <param name="factory"></param>
        public PermissionRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region PermissionRepository Members

        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        public Permission FindById(string permissionCode)
        {
            var permission = this.DbContext.Permissions.Where(c => c.PermissionCode == permissionCode).FirstOrDefault();
            return permission;
        }

        /// <summary>
        /// GetPermits
        /// </summary>
        /// <returns></returns>
        public IList<Permission> GetPermits()
        {             
            return this.DbContext.Permissions.ToList();
        }
        #endregion
    }
}
