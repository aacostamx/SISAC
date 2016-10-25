//------------------------------------------------------------------------
// <copyright file="CostCenterRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Class of CostCenterRepository
    /// </summary>
    public class CostCenterRepository : Repository<CostCenter>, ICostCenterRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CostCenterRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion        
    
        #region ICostCenterRepository Members

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Cost Center</returns>
        public CostCenter FindById(string id)
        {
            var costCenter = this.DbContext.CostCenters.Where(c => c.CCNumber == id).FirstOrDefault();
            return costCenter;
        }

        /// <summary>
        /// Gets the active cost center.
        /// </summary>
        /// <returns>Collection of Cost Center</returns>
        public ICollection<CostCenter> GetActiveCostCenter()
        {
            Trace.TraceInformation("Consultando a la base de datos en GetActiveCostCenter");
            return this.DbContext.CostCenters
                                 .Where(c => c.Status)
                                 .Include(c => c.Airline)
                                 .ToList();
            Trace.TraceInformation("Regresando de la consulta a la base de datos en GetActiveCostCenter");
        }

        /// <summary>
        /// Gets the active cost center.
        /// </summary>
        /// <returns>Collection of Cost Center</returns>
        public ICollection<CostCenter> GetActiveCostCenter(string airlineCode)
        {        
            return this.DbContext.CostCenters
                .Where(c => c.Status && c.AirlineCode == airlineCode)
                .Include(c => c.Airline)
                //.Include(c => c.AirportServiceContracts)
                //.Include(c => c.InternationalFuelContract)
                .ToList();
        }

        /// <summary>
        /// Validate if the cost centers exist.
        /// </summary>
        /// <param name="costCenterNumbers">The cost centers numbers to validate.</param>
        /// <returns>Returns a list with the Const Center Numbers that do not exist</returns>
        public IList<string> ValidatedIfCostCenterExist(IList<string> costCenterNumbers)
        {
            IList<string> notFound = new List<string>();            
            IList<CostCenter> costCenterList = this.DbContext.CostCenters.Where(c => c.Status).ToList();

            notFound = costCenterNumbers.Except(costCenterList.Select(c => c.CCNumber)).ToList();
            return notFound;
        }

        /// <summary>
        /// Gets the airline id related to the cost center.
        /// </summary>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>The airline code that it is related to the cost center.</returns>
        public string GetAirlineCodeRelated(string costCenterNumber)
        {
            var algo = this.DbContext.CostCenters.FirstOrDefault(c => c.CCNumber == costCenterNumber);
            if (algo == null)
            {
                return null;
            }
            else
            {
                return algo.AirlineCode;
            }
        }
        #endregion
    }
}
