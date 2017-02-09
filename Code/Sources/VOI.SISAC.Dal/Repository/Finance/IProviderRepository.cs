//------------------------------------------------------------------------
// <copyright file="IProviderRepository.cs" company="AACOSTA">
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
    /// Provider Repository Interface
    /// </summary>
    public interface IProviderRepository : IRepository<Provider>
    {
        /// <summary>
        /// Finds the entity's identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Provider Entity</returns>
        Provider FindById(string id);

        /// <summary>
        /// Get all Providers
        /// </summary>
        /// <returns>Services markes as Active</returns>
        IList<Provider> GetProviders();

        /// <summary>
        /// Validate if the providers exist.
        /// </summary>
        /// <param name="providerNumbers">The provider numbers to validate.</param>
        /// <returns>Returns a list with the Providers Number that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfProviderExist(IList<string> providerNumbers);
    }
}
