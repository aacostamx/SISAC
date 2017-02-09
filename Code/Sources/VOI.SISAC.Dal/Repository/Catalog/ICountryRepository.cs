//------------------------------------------------------------------------
// <copyright file="ICountryRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Interface ICountryRepository
    /// </summary>
    public interface ICountryRepository : IRepository<Country>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Country Entity</returns>
        Country FindById(string id);

        /// <summary>
        /// Gets the active country.
        /// </summary>
        /// <returns>ICollection Country</returns>
        ICollection<Country> GetActiveCountry();

        /// <summary>
        /// Validateds if country exist.
        /// </summary>
        /// <param name="countryCodes">The country codes.</param>
        /// <returns></returns>
        List<string> ValidatedIfCountryExist(IList<string> countryCodes);

        /// <summary>
        /// Validateds if country exist.
        /// </summary>
        /// <param name="countryCodes">The country codes.</param>
        /// <returns></returns>
        List<string> ValidatedIfCountryTwoCharactersExist(IList<string> countryCodes);
    }
}
