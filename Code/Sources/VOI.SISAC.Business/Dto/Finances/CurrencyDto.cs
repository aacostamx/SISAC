//------------------------------------------------------------------------
// <copyright file="CurrencyDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Finances
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Data Transfer Object for Currency
    /// </summary>
    public class CurrencyDto
    {
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency's code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The desciption of the currency.
        /// </value>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceDto> ReconciliationTolerances { get; set; }
    }
}
