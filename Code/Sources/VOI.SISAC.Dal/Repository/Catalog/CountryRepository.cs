//------------------------------------------------------------------------
// <copyright file="CountryRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.ExceptionDal;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class Country Repository
    /// </summary>
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        #region Contructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CountryRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return Country</returns>
        public Country FindById(string id)
        {
            var country = this.DbContext.Countries.Where(c => c.CountryCode == id).FirstOrDefault();
            return country;           
        }

        /// <summary>
        /// Gets the active country.
        /// </summary>
        /// <returns>Collection Country</returns>
        public ICollection<Country> GetActiveCountry()
        {
            return this.DbContext.Countries.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validates if country exist.
        /// </summary>
        /// <param name="countryCodes">The country codes.</param>
        /// <returns>List of string</returns>
        public List<string> ValidatedIfCountryExist(IList<string> countryCodes)
        {
            List<string> notFound = new List<string>();
            IList<Country> countries = this.DbContext.Countries.Where(c => c.Status).ToList();

            notFound = countryCodes.Except(countries.Select(c => c.CountryCode)).ToList();
            return notFound;
        }

        /// <summary>
        /// Validates if country exist.
        /// </summary>
        /// <param name="countryCodes">The country codes.</param>
        /// <returns>List of string </returns>
        public List<string> ValidatedIfCountryTwoCharactersExist(IList<string> countryCodes)
        {
            List<string> notFound = new List<string>();
            IList<Country> countries = this.DbContext.Countries.Where(c => c.Status).ToList();

            notFound = countryCodes.Except(countries.Select(c => c.CountryCodeShort)).ToList();
            return notFound;
        }
    }
}
