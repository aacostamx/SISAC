//------------------------------------------------------------------------
// <copyright file="NationalJetFuelLogErrorRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Jet fuel log error repository
    /// </summary>
    public class NationalJetFuelLogErrorRepository : Repository<NationalJetFuelLogError>, INationalJetFuelLogErrorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelLogErrorRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelLogErrorRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Gets the national log error by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        public IList<NationalJetFuelLogError> GetLogErrorByPeriodCode(string periodCode)
        {
            return this.DbContext.NationalJetFuelLogError.Where(c => c.PeriodCode == periodCode).ToList();
        }
    }
}