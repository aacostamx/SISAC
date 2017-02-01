//------------------------------------------------------------------------
// <copyright file="IDrinkingWaterBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Operation for Drinking water
    /// </summary>
    public interface IDrinkingWaterBusiness
    {
        /// <summary>
        /// Finds the drinking water by its identifier.
        /// </summary>
        /// <param name="id">The drinking water identifier.</param>
        /// <returns>The drinking water specified.</returns>
        DrinkingWaterDto FindDrinkingWaterById(long id);

        /// <summary>
        /// Adds a new drinking water.
        /// </summary>
        /// <param name="waterDto">The new drinking water.</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddDrinkingWater(DrinkingWaterDto waterDto);

        /// <summary>
        /// Deletes the DrinkingWater.
        /// </summary>
        /// <param name="waterDto">The drinking water to delete.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        bool DeleteDrinkingWater(DrinkingWaterDto waterDto);

        /// <summary>
        /// Updates the drinking water.
        /// </summary>
        /// <param name="waterDto">The drinking water to edit.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        bool UpdateDrinkingWater(DrinkingWaterDto waterDto);

        /// <summary>
        /// Gets all actives drinking water.
        /// </summary>
        /// <returns>List of actives drinking water.</returns>
        IList<DrinkingWaterDto> GetActivesDrinkingWater();

        /// <summary>
        /// Finds the all drinkin water related in an airplane.
        /// </summary>
        /// <param name="id">The Airplane equipment number.</param>
        /// <param name="onlyActives">if set to <c>true</c> gets the drinking waters actives, otherwise gets all drinking waters.</param>
        /// <returns>
        /// List of drinking waters related with an airplane.
        /// </returns>
        IList<DrinkingWaterDto> GetDrinkingWatersByAirplaneId(string id, bool onlyActives);
    }
}
