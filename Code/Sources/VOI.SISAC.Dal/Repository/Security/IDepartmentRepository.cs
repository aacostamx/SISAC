//-------------------------------------------------------------------
// <copyright file="IDepartmentRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{

    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    public interface IDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// GetActiveDepartments
        /// </summary>
        /// <returns></returns>
        IList<Department> GetActiveDepartments();
    }
}
