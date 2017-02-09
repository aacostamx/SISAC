//-----------------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceDetailRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// National Jet Fuel Invoice Detail Repository
    /// </summary>
    public class NationalJetFuelInvoiceDetailRepository : Repository<NationalJetFuelInvoiceDetail>, INationalJetFuelInvoiceDetailRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceDetailRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelInvoiceDetailRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Gets the national invoices detail.
        /// </summary>
        /// <returns></returns>
        public IList<NationalJetFuelInvoiceDetail> GetNationalInvoicesDetail()
        {
            var invoices = new List<NationalJetFuelInvoiceDetail>();

            try
            {
                invoices = this.DbContext.NationalJetFuelInvoiceDetail
                    .Include(c => c.NationalJetFuelInvoiceControl)
                    .Include(c => c.NationalJetFuelInvoiceControl.RemittanceStatus)
                    .Include(c => c.NationalJetFuelInvoiceControl.DocumentStatus)
                    .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return invoices;
        }

        /// <summary>
        /// Gets the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public IList<NationalJetFuelInvoiceControl> GetSearchInvoiceControlDetail(RemittanceSearch search)
        {
            var skip = 0;
            var totalRows = 0;
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
                    IQueryable<NationalJetFuelInvoiceDetail> query = this.DbContext.NationalJetFuelInvoiceDetail
                    .Include(c => c.NationalJetFuelInvoiceControl)
                    .Include(c => c.NationalJetFuelInvoiceControl.CalculationStatus)
                    .Include(c => c.NationalJetFuelInvoiceControl.ConfirmationStatus)
                    .Include(c => c.NationalJetFuelInvoiceControl.StatusProcess)
                    .Include(c => c.NationalJetFuelInvoiceControl.RemittanceStatus)
                    .Include(c => c.NationalJetFuelInvoiceControl.DocumentStatus);

                    if (!string.IsNullOrEmpty(search.MonthYear))
                    {                        
                        query = query
                            .Where(c => c.NationalJetFuelInvoiceControl.MonthYear.Equals(search.MonthYear));
                    }
                    if (!string.IsNullOrEmpty(search.Period))
                    {
                        query = query.Where(c => c.NationalJetFuelInvoiceControl.Period.Equals(search.Period));
                    }
                    if (!string.IsNullOrEmpty(search.RemittanceId))
                    {
                        query = query.Where(c => c.RemittanceID.Contains(search.RemittanceId));
                    }
                    if (!string.IsNullOrEmpty(search.InvoiceNumber))
                    {
                        query = query.Where(c => c.ElectronicInvoiceNumber.Contains(search.InvoiceNumber));
                    }
                    if (!string.IsNullOrEmpty(search.StationCode))
                    {
                        query = query.Where(c => c.StationCode.Equals(search.StationCode));
                    }

                    query.ToList();

                    var details = query.AsEnumerable().Select(c => c.NationalJetFuelInvoiceControl).GroupBy(c => new { c.RemittanceID, c.MonthYear, c.Period }).Select(c => c.Last()).ToList();

                    totalRows = details.Count();

                    invoices = details
                        .Skip(skip)
                        .Take(search.PageSize)
                        .ToList();

                    invoices = invoices.Select(c => { c.TotalRows = totalRows; return c; }).ToList();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return invoices;
        }


    }
}
