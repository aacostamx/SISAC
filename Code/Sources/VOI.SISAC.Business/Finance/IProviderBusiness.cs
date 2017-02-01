//------------------------------------------------------------------------
// <copyright file="IProviderBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Contract for provider operations
    /// </summary>
    public interface IProviderBusiness
    {
        /// <summary>
        /// Get all providers
        /// </summary>
        IList<ProviderDto> GetAllProvider();

        /// <summary>
        /// Get the provider by the identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns></returns>
        ProviderDto FindProviderById(string id);

        /// <summary>
        /// Add a new Provider
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddProvider(ProviderDto entity);

        /// <summary>
        /// Delete a provider
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteProvider(ProviderDto entity);

        /// <summary>
        /// Update a Provider
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateProvider(ProviderDto entity);

        /// <summary>
        /// get the active providers
        /// </summary>
        /// <returns></returns>
        IList<ProviderDto> GetActivesProvider();
    }
}
