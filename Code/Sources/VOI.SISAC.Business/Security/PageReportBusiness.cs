//------------------------------------------------------------------------
// <copyright file="PageReportBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// UserBusiness
    /// </summary>
    public class PageReportBusiness : IPageReportBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The IPageReport Repository 
        /// </summary>
        private readonly IPageReportRepository pageReportRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userRepository">The userRepository</param>
        public PageReportBusiness(IUnitOfWork unitOfWork, IPageReportRepository pageReportRepository)
        {
            this.unitOfWork = unitOfWork;
            this.pageReportRepository = pageReportRepository;
        }

        public PageReportDto GetPageReportByPageName(string pageName)
        {
            try
            {
                PageReport pageReport = this.pageReportRepository.FindById(pageName);
                PageReportDto pageReportDto = new PageReportDto();

                pageReportDto = Mapper.Map<PageReport, PageReportDto>(pageReport);
                return pageReportDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

    }
}
