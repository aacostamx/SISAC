// <copyright file="StatusOnBoardRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

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
    /// Class Status On Board Repository
    /// </summary>
    public class StatusOnBoardRepository : Repository<StatusOnBoard>, IStatusOnBoardRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusOnBoardRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public StatusOnBoardRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IStatusOnBoardRepository Members

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public StatusOnBoard FindById(string id)
        {
            return this.DbContext.StatusOnBoard.FirstOrDefault(c => c.StatusOnBoardCode == id);
        }

        /// <summary>
        /// Validates if exist status on board code.
        /// </summary>
        /// <param name="statusOnBoardCode">The status on board code.</param>
        /// <returns></returns>
        public List<string> ValidateIfExistStatusOnBoardCode(List<string> statusOnBoardCode)
        {
            List<string> notFound = new List<string>();
            List<StatusOnBoard> statusOnBoards = this.DbContext.StatusOnBoard.ToList();

            notFound = statusOnBoardCode.Except(statusOnBoards.Select(c => c.StatusOnBoardCode)).ToList();
            
            return notFound;
        }
        #endregion
    }
}
