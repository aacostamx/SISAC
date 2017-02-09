//------------------------------------------------------------------------
// <copyright file="MovementTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{

    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Movement Type Repository
    /// </summary>
    public class MovementTypeRepository : Repository<MovementType>, IMovementTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public MovementTypeRepository(IDbFactory factory) : base(factory) { }
    }
}
