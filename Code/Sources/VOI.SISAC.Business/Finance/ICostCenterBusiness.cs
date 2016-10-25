//------------------------------------------------------------------------
// <copyright file="ICostCenterBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Interface ICostCenterBusiness
    /// </summary>
    public interface ICostCenterBusiness
    {
        /// <summary>
        /// Gets all cost center.
        /// </summary>
        /// <returns>IList CostCenter</returns>
        IList<CostCenterDto> GetAllCostCenter();

        /// <summary>
        /// Finds the cost centery by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Cost Center</returns>
        CostCenterDto FindCostCenteryById(string id);

        /// <summary>
        /// Adds the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if added else false</returns>
        bool AddCostCenter(CostCenterDto entity);

        /// <summary>
        /// Deletes the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if deleted else false</returns>
        bool DeleteCostCenter(CostCenterDto entity);

        /// <summary>
        /// Updates the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if updated else false</returns>
        bool UpdateCostCenter(CostCenterDto entity);

        /// <summary>
        /// Gets the actives cost center.
        /// </summary>
        /// <returns>IList CostCenter</returns>
        IList<CostCenterDto> GetActivesCostCenter();
    }
}
