//------------------------------------------------------------------------
// <copyright file="ICostCenterRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface of CostCenter
    /// </summary>
    public interface ICostCenterRepository : IRepository<CostCenter>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return CostCenter</returns>
        CostCenter FindById(string id);

        /// <summary>
        /// Gets the active cost center.
        /// </summary>
        /// <returns>ICollection CostCenter</returns>
        ICollection<CostCenter> GetActiveCostCenter();

        /// <summary>
        /// Gets the active cost center.
        /// </summary>
        /// <returns>ICollection CostCenter</returns>
        ICollection<CostCenter> GetActiveCostCenter(string airlineCode);

        /// <summary>
        /// Validate if the cost centers exist.
        /// </summary>
        /// <param name="costCenterNumbers">The cost centers numbers to validate.</param>
        /// <returns>Returns a list with the Const Center Numbers that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfCostCenterExist(IList<string> costCenterNumbers);

        /// <summary>
        /// Gets the airline id related to the cost center.
        /// </summary>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>The airline code that it is related to the cost center.</returns>
        string GetAirlineCodeRelated(string costCenterNumber);
    }
}
