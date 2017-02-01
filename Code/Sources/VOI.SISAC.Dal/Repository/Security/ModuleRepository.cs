//------------------------------------------------------------------------
// <copyright file="ModuleRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Class Module Repository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Security.Module}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Security.IModuleRepository" />
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        #region Contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ModuleRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Module Entity</returns>
        public Module FindById(string id)
        {
            Module module = this.DbContext.Modules.Where(c => c.ModuleCode == id)
                .Include(c => c.ModulePermissions)
                .Include(c => c.Menu).FirstOrDefault();
            return module;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public override void Delete(Module entity)
        {
            List<ModulePermission> modulePermission = this.DbContext.ModulePermissions
                .Where(c => c.ModuleCode == entity.ModuleCode)
                .Include(c => c.Roles)
                .Include(c => c.Users)
                .ToList();

            this.DbContext.ModulePermissions.RemoveRange(modulePermission);
            this.DbContext.Modules.Remove(entity);
        }
    }
}
