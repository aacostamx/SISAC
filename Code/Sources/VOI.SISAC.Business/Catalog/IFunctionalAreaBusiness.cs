//------------------------------------------------------------------------
// <copyright file="IFunctionalAreaBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Contract for FunctionalAreaBusiness methods
    /// </summary>
    public interface IFunctionalAreaBusiness
    {
        /// <summary>
        /// Gets all FunctionalAreas.
        /// </summary>
        /// <returns>List of FunctionalAreas.</returns>
        IList<FunctionalAreaDto> GetAllFunctionalAreas();

        /// <summary>
        /// Finds the FunctionalArea by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity FunctionalArea.</returns>
        FunctionalAreaDto FindFunctionalAreaById(long id);

        /// <summary>
        /// Adds the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddFunctionalArea(FunctionalAreaDto entity);

        /// <summary>
        /// Deletes the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteFunctionalArea(FunctionalAreaDto entity);

        /// <summary>
        /// Updates the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateFunctionalArea(FunctionalAreaDto entity);

        /// <summary>
        /// Gets the Actives FunctionalArea.
        /// </summary>
        /// <returns>FunctionalAreas Actives.</returns>
        IList<FunctionalAreaDto> GetActivesFunctionalAreas();
    }
}
