//------------------------------------------------------------------------
// <copyright file="IAirplaneWeightTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Interface IAirplane Weight Type Business
    /// </summary>
    public interface IAirplaneWeightTypeBusiness
    {
        /// <summary>
        /// Gets the type of all airplane weight.
        /// </summary>
        /// <returns>IList AirplaneWeightTypeDto</returns>
        IList<AirplaneWeightTypeDto> GetAllAirplaneWeightType();
    }
}
