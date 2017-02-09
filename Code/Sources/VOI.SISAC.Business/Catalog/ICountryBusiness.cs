//------------------------------------------------------------------------
// <copyright file="ICountryBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Entities.Catalog;
    
    /// <summary>
    /// Interface Country Business
    /// </summary>
    public interface ICountryBusiness
    {
        /// <summary>
        /// Gets all country.
        /// </summary>
        /// <returns> List CountryDto </returns>
        IList<CountryDto> GetAllCountry();

        /// <summary>
        /// Finds the country by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Country Dto</returns>
        CountryDto FindCountryById(string id);

        /// <summary>
        /// Adds the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool AddCountry(CountryDto entity);

        /// <summary>
        /// Deletes the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool DeleteCountry(CountryDto entity);

        /// <summary>
        /// Updates the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool UpdateCountry(CountryDto entity);

        /// <summary>
        /// Gets the actives country.
        /// </summary>
        /// <returns>true or false</returns>
        IList<CountryDto> GetActivesCountry();
    }
}
