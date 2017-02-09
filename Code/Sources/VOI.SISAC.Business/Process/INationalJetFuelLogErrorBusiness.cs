//------------------------------------------------------------------------
// <copyright file="INationalJetFuelLogErrorBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface for national jet fuel log  error
    /// </summary>
    public interface INationalJetFuelLogErrorBusiness
    {
        /// <summary>
        /// Gets the errors for period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>Errors of the period.</returns>
        IList<NationalJetFuelLogErrorDto> GetErrorsForPeriod(string periodCode);
    }
}
