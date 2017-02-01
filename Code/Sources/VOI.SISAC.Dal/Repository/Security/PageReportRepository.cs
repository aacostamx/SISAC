//--------------------------------------------------------------------
// <copyright file="PageReportRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    public class PageReportRepository : Repository<PageReport>, IPageReportRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageReportRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public PageReportRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IPageReportRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>PageReport Entity.</returns>
        public PageReport FindById(string pageName)
        {
            var pageReport = this.DbContext.PageReports.Where(c => c.PageName == pageName && c.Status).FirstOrDefault();
            return pageReport;
        }

        #endregion
    }
}
