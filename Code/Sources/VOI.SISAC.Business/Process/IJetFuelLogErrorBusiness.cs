//------------------------------------------------------------------------
// <copyright file="IJetFuelLogErrorBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface for jet fuel log  error
    /// </summary>
    public interface IJetFuelLogErrorBusiness
    {
        /// <summary>
        /// Gets the errors for period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>Errors of the period.</returns>
        IList<JetFuelLogErrorDto> GetErrorsForPeriod(string periodCode);
    }
}
