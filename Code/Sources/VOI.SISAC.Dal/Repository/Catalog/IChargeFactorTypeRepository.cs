//------------------------------------------------------------------------
// <copyright file="IChargeFactorTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using Entities.Catalog;
    using Infrastructure;

    /// <summary>
    /// Interface ChargeFactorType
    /// </summary>
    public interface IChargeFactorTypeRepository : IRepository<ChargeFactorType>
    {
        /// <summary>
        /// Finds by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ChargeFactorType FindById(int id);
    }
}
