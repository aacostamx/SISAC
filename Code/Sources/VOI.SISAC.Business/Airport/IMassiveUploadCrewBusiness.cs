//------------------------------------------------------------------------
// <copyright file="IMassiveUploadCrewBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

using System.Collections.Generic;
using VOI.SISAC.Business.Dto.Airports;

namespace VOI.SISAC.Business.Airport
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMassiveUploadCrewBusiness
    {
        /// <summary>
        /// Crews the add range.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns></returns>
        List<string> CrewAddRange(IList<CrewDto> crewDto);
    }
}
