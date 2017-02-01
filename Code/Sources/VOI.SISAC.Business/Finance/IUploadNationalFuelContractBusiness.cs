//-----------------------------------------------------------------------------
// <copyright file="IUploadNationalFuelContractBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Interface for upload national fuel contracts
    /// </summary>
    public interface IUploadNationalFuelContractBusiness
    {
        /// <summary>
        /// Uploads the national fuel contracts.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors, if there is any.</returns>
        IList<string> UploadNationalFuelContracts(IList<NationalFuelContractDto> contracts);
    }
}
