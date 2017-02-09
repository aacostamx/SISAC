//------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoiceDetailRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System.Collections.Generic;

    /// <summary>
    /// Interface National Jet Fuel Invoice Detail Repository
    /// </summary>
    public interface INationalJetFuelInvoiceDetailRepository : IRepository<NationalJetFuelInvoiceDetail>
    {
        /// <summary>
        /// Gets the national invoices detail.
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceDetail> GetNationalInvoicesDetail();

        /// <summary>
        /// Gets the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceControl> GetSearchInvoiceControlDetail(RemittanceSearch search);
    }
}
