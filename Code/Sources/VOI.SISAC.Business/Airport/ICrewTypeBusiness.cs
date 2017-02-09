//------------------------------------------------------------------------
// <copyright file="ICrewTypeBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// interface ICrewType business
    /// </summary>
    public interface ICrewTypeBusiness
    {
        /// <summary>
        /// Gets the type of the actives crew.
        /// </summary>
        /// <returns>IList CrewTypeDto </returns>
        IList<CrewTypeDto> GetActivesCrewType();
    }
}
