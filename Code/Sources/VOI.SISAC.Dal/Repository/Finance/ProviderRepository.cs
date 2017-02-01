//------------------------------------------------------------------------
// <copyright file="ProviderRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;
    /// <summary>
    /// Providers Repository
    /// </summary>
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderRepository"/> class.
        /// </summary>
        /// <param name="factory">Factory.</param>
        public ProviderRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IProviderRepository Members
        /// <summary>
        ///  Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">entity identifier</param>
        /// <returns>Provider entity</returns>
        public Provider FindById(string id)
        {
            var providers = this.DbContext.Providers.Where(c => c.ProviderNumber == id).FirstOrDefault();
            return providers;
        }

        /// <summary>
        /// Get the active providers
        /// </summary>
        /// <returns>Providers marked as Active.</returns>
        public IList<Provider> GetProviders()
        {
            return this.DbContext.Providers.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the providers exist.
        /// </summary>
        /// <param name="providerNumbers">The provider numbers to validate.</param>
        /// <returns>Returns a list with the Providers Number that do not exist, if all of them exist returns NULL.</returns>
        public IList<string> ValidatedIfProviderExist(IList<string> providerNumbers)
        {
            IList<string> notFound = new List<string>();
            IList<Provider> providerList = this.DbContext.Providers.Where(c => c.Status).ToList();

            notFound = providerNumbers.Except(providerList.Select(c => c.ProviderNumber)).ToList();
            return notFound;
        }
        #endregion
    }
}
