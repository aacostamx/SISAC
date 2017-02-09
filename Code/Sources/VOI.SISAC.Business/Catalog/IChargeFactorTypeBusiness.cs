//------------------------------------------------------------------------
// <copyright file="IChargeFactorType.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using Dto.Catalogs;
    using System.Collections.Generic;

    /// <summary>
    /// IChargeFactorType
    /// </summary>
    public interface IChargeFactorTypeBusiness
    {
        /// <summary>
        /// GetAllChargeFactorTypes
        /// </summary>
        /// <returns></returns>
        IList<ChargeFactorTypeDto> GetAllChargeFactorTypes();

        /// <summary>
        /// FindChargeFactorTypeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ChargeFactorTypeDto FindChargeFactorTypeById(int id);
    }
}
