//------------------------------------------------------------------------
// <copyright file="StatusOnBoardConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Interface  Status On Board Repository
    /// </summary>
    public interface IStatusOnBoardRepository : IRepository<StatusOnBoard>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        StatusOnBoard FindById(string id);

        /// <summary>
        /// Validates if exist status on board code.
        /// </summary>
        /// <param name="statusOnBoardCode">The status on board code.</param>
        /// <returns></returns>
        List<string> ValidateIfExistStatusOnBoardCode(List<string> statusOnBoardCode);
    }
}
