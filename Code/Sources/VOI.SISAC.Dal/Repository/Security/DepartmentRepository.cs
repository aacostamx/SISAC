//-------------------------------------------------------------------
// <copyright file="DepartmentRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public DepartmentRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IDepartmentRepository Members

        /// <summary>
        /// GetActiveDepartments
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetActiveDepartments()
        {
            return this.DbContext.Departments.Where(c => c.Status).ToList();
        }

        #endregion 
    }
}
