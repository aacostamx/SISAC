//-------------------------------------------------------------------
// <copyright file="FuelConceptTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities.Airport;
    using Infrastructure;

    /// <summary>
    /// FuelConceptTypeRepository
    /// </summary>
    public class FuelConceptTypeRepository : Repository<FuelConceptType>, IFuelConceptTypeRepository
    {
        /// <summary>
        /// FuelConceptTypeRepository Constructor
        /// </summary>
        /// <param name="factory"></param>
        public FuelConceptTypeRepository(IDbFactory factory) : base(factory)
        { }
    }
}
