//------------------------------------------------------------------------
// <copyright file="IStatusOnBoardBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// 
    /// </summary>
    public interface IStatusOnBoardBusiness
    {
        /// <summary>
        /// Gets all status on board.
        /// </summary>
        /// <returns>IList Status On Board</returns>
        IList<StatusOnBoardDto> GetAllStatusOnBoard();
    }
}
