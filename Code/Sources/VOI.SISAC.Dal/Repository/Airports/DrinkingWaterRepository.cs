//------------------------------------------------------------------------
// <copyright file="DrinkingWaterRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// DrinkingWater repository
    /// </summary>
    public class DrinkingWaterRepository : Repository<DrinkingWater>, IDrinkingWaterRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkingWaterRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public DrinkingWaterRepository(IDbFactory factory) 
            : base(factory) 
        { 
        }
        #endregion

        /// <summary>
        /// Finds the Entity by its identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>
        /// The Entity specified.
        /// </returns>
        public DrinkingWater FindById(long id)
        {
            var water = this.DbContext.DrinkingWaters.FirstOrDefault(c => c.DrinkingWaterId == id);
            return water;
        }

        /// <summary>
        /// Gets the actives Enities.
        /// </summary>
        /// <returns>
        /// List of actives Entities.
        /// </returns>
        public IList<DrinkingWater> GetActiveDrinkingWater()
        {
            return this.DbContext.DrinkingWaters.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Finds the all drinkin water related in an airplane.
        /// </summary>
        /// <param name="id">The airplane's identifier.</param>
        /// <param name="onlyActives">if set to <c>true</c> gets the drinking waters actives, otherwise gets all drinking waters.</param>
        /// <returns>
        /// List of drinking waters related with an airplane.
        /// </returns>
        public IList<DrinkingWater> GetDrinkingWatersByAirplaneId(string id, bool onlyActives)
        {
            if (onlyActives)
            {
                return this.DbContext.DrinkingWaters.Where(c => c.EquipmentNumber == id && c.Status).ToList();
            }

            return this.DbContext.DrinkingWaters.Where(c => c.EquipmentNumber == id).ToList();
        }
    }
}