//------------------------------------------------------------------------
// <copyright file="ITaxRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// Tax Repository Interface
    /// </summary>
    public interface ITaxRepository : IRepository<Tax>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Tax Entity.</returns>
        Tax FindById(string id);

        /// <summary>
        /// Gets the actives Taxes.
        /// </summary>
        /// <returns>Taxes marked as Actives.</returns>
        IList<Tax> GetActivesTaxes();

        /// <summary>
        /// Validate if the taxes exist.
        /// </summary>
        /// <param name="taxCodes">The taxes codes to validate.</param>
        /// <returns>Returns a list with the Tax Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfTaxExist(IList<string> taxCodes);
    }
}
