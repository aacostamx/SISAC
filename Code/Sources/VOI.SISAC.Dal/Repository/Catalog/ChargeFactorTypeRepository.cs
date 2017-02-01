//------------------------------------------------------------------------
// <copyright file="ChargeFactorTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using Entities.Catalog;
    using Infrastructure;
    using System.Linq;

    /// <summary>
    /// Repository ChargeFactorType
    /// </summary>
    public class ChargeFactorTypeRepository : Repository<ChargeFactorType>, IChargeFactorTypeRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory"></param>
        public ChargeFactorTypeRepository(IDbFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChargeFactorType FindById(int id)
        {
            return this.DbContext.ChargeFactorType.FirstOrDefault(c => c.ChargeFactorTypeID == id);
        }
    }
}
