//------------------------------------------------------------------------
// <copyright file="CurrencyTypeChage.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    /// <summary>
    /// Currency type chage
    /// </summary>
    public class CurrencyTypeChage
    {
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency's code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the verify.
        /// </summary>
        /// <value>
        /// The verify.
        /// </value>
        public int Verify { get; set; }
    }
}
