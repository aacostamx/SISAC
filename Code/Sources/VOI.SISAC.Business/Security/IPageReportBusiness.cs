//------------------------------------------------------------------------
// <copyright file="IPageReportBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// IPageReportBusiness
    /// </summary>
    public interface IPageReportBusiness
    {
        PageReportDto GetPageReportByPageName(string pageName);
    }
}
