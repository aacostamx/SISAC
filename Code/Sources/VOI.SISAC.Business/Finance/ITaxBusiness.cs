//------------------------------------------------------------------------
// <copyright file="ITaxBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Interfase ITaxBusiness
    /// </summary>
    public interface ITaxBusiness
    {
        /// <summary>
        /// Gets all Taxes.
        /// </summary>
        /// <returns>List of Taxes.</returns>
        IList<TaxDto> GetAllTax();

        /// <summary>
        /// Finds the Tax by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity Tax.</returns>
        TaxDto FindTaxById(string id);

        /// <summary>
        /// Adds the Tax.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddTax(TaxDto entity);

        /// <summary>
        /// Delete the Tax.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteTax(TaxDto entity);

        /// <summary>
        /// Update the Tax.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateTax(TaxDto entity);

        /// <summary>
        /// Gets the Actives Taxes.
        /// </summary>
        /// <returns>Taxes Actives.</returns>
        IList<TaxDto> GetActivesTaxes();
    }
}
