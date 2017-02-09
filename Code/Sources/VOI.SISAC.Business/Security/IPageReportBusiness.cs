//------------------------------------------------------------------------
// <copyright file="IPageReportBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
