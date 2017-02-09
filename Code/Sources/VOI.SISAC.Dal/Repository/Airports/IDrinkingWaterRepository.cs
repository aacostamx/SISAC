//------------------------------------------------------------------------
// <copyright file="IDrinkingWaterRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Interface for specific operation for DrinkingWater
    /// </summary>
    public interface IDrinkingWaterRepository : IRepository<DrinkingWater>
    {
        /// <summary>
        /// Finds the Entity by its identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>The Entity specified.</returns>
        DrinkingWater FindById(long id);

        /// <summary>
        /// Gets the actives Entities.
        /// </summary>
        /// <returns>List of actives Entities.</returns>
        IList<DrinkingWater> GetActiveDrinkingWater();

        /// <summary>
        /// Finds the all drinkin water related in an airplane.
        /// </summary>
        /// <param name="id">The airplane's identifier.</param>
        /// <param name="onlyActives">if set to <c>true</c> gets the drinking waters actives, otherwise gets all drinking waters.</param>
        /// <returns>
        /// List of drinking waters related with an airplane.
        /// </returns>
        IList<DrinkingWater> GetDrinkingWatersByAirplaneId(string id, bool onlyActives);
    }
}