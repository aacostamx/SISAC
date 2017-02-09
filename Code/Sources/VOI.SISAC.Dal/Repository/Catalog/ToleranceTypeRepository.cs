//------------------------------------------------------------------------
// <copyright file="ToleranceTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    public class ToleranceTypeRepository : Repository<ToleranceType>, IToleranceTypeRepository
    {
        public ToleranceTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }


        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="toleranceTypeCode"></param>
        /// <returns></returns>
        public ToleranceType FindById(string toleranceTypeCode)
        {
            return this.DbContext.ToleranceTypes.FirstOrDefault(c => c.ToleranceTypeCode == toleranceTypeCode);
        }



        /// <summary>
        /// Gets the actives tolerance types.
        /// </summary>
        /// <returns></returns>
        public IList<ToleranceType> GetActivesToleranceTypes()
        {
            return this.DbContext.ToleranceTypes.ToList();
        }
    }
}
