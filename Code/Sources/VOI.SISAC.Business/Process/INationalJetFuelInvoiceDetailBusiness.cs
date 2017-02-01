//------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoiceDetailBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using Dto.Process;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for National Jet Fuel Invoice Detail Business
    /// </summary>
    public interface INationalJetFuelInvoiceDetailBusiness
    {
        /// <summary>
        /// Gets the search invoices detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceDetailDto> GetSearchInvoicesDetail(RemittanceSearchDto search);

        /// <summary>
        /// Gets the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceControlDto> GetSearchInvoiceControlDetail(RemittanceSearchDto search);

        /// <summary>
        /// Counts the search invoice control detail.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        int CountSearchInvoiceControlDetail(RemittanceSearchDto search);

        /// <summary>
        /// Counts the national invoices detail.
        /// </summary>
        /// <returns></returns>
        int CountNationalInvoicesDetail();
    }
}
