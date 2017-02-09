//-------------------------------------------------------------------
// <copyright file="IDepartmentRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
