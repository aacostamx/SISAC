//------------------------------------------------------------------------
// <copyright file="JetFuelLogErrorRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Jet fuel log error repository
    /// </summary>
    public class JetFuelLogErrorRepository : Repository<JetFuelLogError>, IJetFuelLogErrorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public JetFuelLogErrorRepository(IDbFactory factory) 
            : base(factory) 
        {
        }

        /// <summary>
        /// Gets the log errors by period code.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The errors for the period.</returns>
        public IList<JetFuelLogError> GetLogErrorByPeriodCode(string periodCode)
        {
            return this.DbContext.JetFuelLogErrors.Where(c => c.PeriodCode == periodCode).ToList();
        }
    }
}