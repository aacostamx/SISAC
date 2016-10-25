//------------------------------------------------------------------------
// <copyright file="IServiceCalculationTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Interface Service Calculation Type Business
    /// </summary>
    public interface IServiceCalculationTypeBusiness
    {
        /// <summary>
        /// Gets the type of all service calculation.
        /// </summary>
        /// <returns>IList ServiceCalculationTypeDto</returns>
        IList<ServiceCalculationTypeDto> GetAllServiceCalculationType();
    }
}
