//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceDetailBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using Dal.Repository.Process;
    using Dal.Infrastructure;
    using System;
    using System.Collections.Generic;
    using Dto.Process;
    using ExceptionBusiness;
    using Resources;
    using AutoMapper;
    using System.Linq;
    using Entities.Process;


    /// <summary>
    /// National Jet Fuel Invoice Detail Business
    /// /// </summary>
    public class NationalJetFuelInvoiceDetailBusiness : INationalJetFuelInvoiceDetailBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The invoice control repository
        /// </summary>
        private readonly INationalJetFuelInvoiceDetailRepository invoiceDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceDetailBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="invoiceDetailRepository">The invoice detail repository.</param>
        public NationalJetFuelInvoiceDetailBusiness(IUnitOfWork unitOfWork, INationalJetFuelInvoiceDetailRepository invoiceDetailRepository)
        {
            this.unitOfWork = unitOfWork;
            this.invoiceDetailRepository = invoiceDetailRepository;
        }

        /// <summary>
        /// Gets the search invoices detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelInvoiceDetailDto> GetSearchInvoicesDetail(RemittanceSearchDto search)
        {
            var skip = 0;
            var entity = new List<NationalJetFuelInvoiceDetail>();
            var invoiceDto = new List<NationalJetFuelInvoiceDetailDto>();

            try
            {
                skip = (search.PageNumber - 1) * search.PageSize;
                entity = this.invoiceDetailRepository.GetNationalInvoicesDetail().ToList();

                //if (search.MonthYear != null && search.MonthYear != DateTime.MinValue)
                //{
                //    entity = entity.Where(c => c.NationalJetFuelInvoiceControl.MonthYear.ToString() == search.MonthYear.ToString()).ToList();
                //}
                if (!string.IsNullOrEmpty(search.MonthYear))
                {
                    entity = entity.Where(c => c.NationalJetFuelInvoiceControl.MonthYear.ToString() == search.MonthYear.ToString()).ToList();
                }
                if (!string.IsNullOrEmpty(search.Period))
                {
                    entity = entity.Where(c => c.NationalJetFuelInvoiceControl.Period.Equals(search.Period)).ToList();
                }
                if (!string.IsNullOrEmpty(search.RemittanceId))
                {
                    entity = entity.Where(c => c.RemittanceID.Contains(search.RemittanceId)).ToList();
                }
                if (!string.IsNullOrEmpty(search.InvoiceNumber))
                {
                    entity = entity.Where(c => c.InvoiceNumber.Contains(search.InvoiceNumber)).ToList();
                }
                if (!string.IsNullOrEmpty(search.StationCode))
                {
                    entity = entity.Where(c => c.StationCode.Equals(search.StationCode)).ToList();
                }

                entity.OrderBy(c => c.RemittanceID).Skip(skip).Take(search.PageSize).ToList();
                invoiceDto = Mapper.Map<List<NationalJetFuelInvoiceDetailDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return invoiceDto;
        }

        /// <summary>
        /// Counts the national invoices detail.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int CountNationalInvoicesDetail()
        {
            var total = 0;

            try
            {
                total = this.invoiceDetailRepository.CountTotalRecords();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return total;
        }

        /// <summary>
        /// Gets the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelInvoiceControlDto> GetSearchInvoiceControlDetail(RemittanceSearchDto search)
        {
            var invoices = new List<NationalJetFuelInvoiceControl>();
            var invoicesDto = new List<NationalJetFuelInvoiceControlDto>();

            try
            {
                invoices = this.invoiceDetailRepository.GetSearchInvoiceControlDetail(Mapper.Map<RemittanceSearch>(search)).ToList();
                invoicesDto = Mapper.Map<List<NationalJetFuelInvoiceControlDto>>(invoices);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return invoicesDto;
        }

        /// <summary>
        /// Counts the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int CountSearchInvoiceControlDetail(RemittanceSearchDto search)
        {
            var skip = 0;
            var count = 0;
            var entity = new List<NationalJetFuelInvoiceDetail>();
            var invoices = new List<NationalJetFuelInvoiceControl>();

            try
            {
                if (!string.IsNullOrEmpty(search.MonthYear) ||
                    !string.IsNullOrEmpty(search.Period) ||
                    !string.IsNullOrEmpty(search.RemittanceId) ||
                    !string.IsNullOrEmpty(search.InvoiceNumber) ||
                    !string.IsNullOrEmpty(search.StationCode))
                {
                    skip = (search.PageNumber - 1) * search.PageSize;
                    entity = this.invoiceDetailRepository.GetNationalInvoicesDetail().ToList();

                    if (!string.IsNullOrEmpty(search.MonthYear))
                    {
                        entity = entity.Where(c => c.NationalJetFuelInvoiceControl.MonthYear.ToString() == search.MonthYear.ToString()).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.Period))
                    {
                        entity = entity.Where(c => c.NationalJetFuelInvoiceControl.Period.Equals(search.Period)).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.RemittanceId))
                    {
                        entity = entity.Where(c => c.RemittanceID.Contains(search.RemittanceId)).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.InvoiceNumber))
                    {
                        entity = entity.Where(c => c.InvoiceNumber == search.InvoiceNumber).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.StationCode))
                    {
                        entity = entity.Where(c => c.StationCode.Equals(search.StationCode)).ToList();
                    }

                    invoices = entity.Select(c => c.NationalJetFuelInvoiceControl).GroupBy(c => new { c.RemittanceID }).Select(c => c.Last()).ToList();
                    count = invoices.Count; 
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return count;
        }
    }
}
