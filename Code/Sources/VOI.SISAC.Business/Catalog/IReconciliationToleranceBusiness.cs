//------------------------------------------------------------------------
// <copyright file="IReconciliationToleranceBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// IReconciliationToleranceBusiness
    /// </summary>
    public interface IReconciliationToleranceBusiness
    {

        /// <summary>
        /// Gets all actives reconciliation tolerances.
        /// </summary>
        /// <returns></returns>
        IList<ReconciliationToleranceDto> GetAllActivesReconciliationTolerances();

        /// <summary>
        /// Finds the exchange rate by identifier.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        ReconciliationToleranceDto FindReconciliationToleranceById(ReconciliationToleranceDto toleranceDto);

        /// <summary>
        /// Adds the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        bool AddReconciliationTolerance(ReconciliationToleranceDto toleranceDto);

        /// <summary>
        /// Deletes the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        bool DeleteReconciliationTolerance(ReconciliationToleranceDto toleranceDto);

        /// <summary>
        /// Updates the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        bool UpdateReconciliationTolerance(ReconciliationToleranceDto toleranceDto);

        /// <summary>
        /// Gets all reconciliation tolerances.
        /// </summary>
        /// <returns></returns>
        IList<ReconciliationToleranceDto> GetAllReconciliationTolerances();
    }
}
