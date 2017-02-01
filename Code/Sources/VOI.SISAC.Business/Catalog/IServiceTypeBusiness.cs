//------------------------------------------------------------------------
// <copyright file="IServiceTypeBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Interface Service Type Business
    /// </summary>
    public interface IServiceTypeBusiness
    {
        /// <summary>
        /// Gets the type of all service.
        /// </summary>
        /// <returns>IList ServiceTypeDto</returns>
        IList<ServiceTypeDto> GetAllServiceType();
    }
}
