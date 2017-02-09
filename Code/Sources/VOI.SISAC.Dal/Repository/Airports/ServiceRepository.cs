//------------------------------------------------------------------------
// <copyright file="ServiceRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Services Repository
    /// </summary>
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class.
        /// </summary>
        /// <param name="factory">factory parameter</param>
        public ServiceRepository(IDbFactory factory)
        : base(factory)
        {
        }

        #region IServiceRepository Members
        /// <summary>
        /// Get the service by the identifier
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>a list of service</returns>
        public Service FindById(string id)
        {
            var services = this.DbContext.Services.Where(c => c.ServiceCode == id).FirstOrDefault();
            return services;
        }

        /// <summary>
        /// Get the active Services
        /// </summary>
        /// <returns>Gets a lists of Services</returns>
        public IList<Service> GetServices()
        {
            return this.DbContext.Services.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the services exist.
        /// </summary>
        /// <param name="serviceCodes">The service codes to validate.</param>
        /// <returns>Returns a list with the Service Codes that do not exist.</returns>
        public IList<string> ValidatedIfServiceExist(IList<string> serviceCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Service> serviceList = this.DbContext.Services.Where(c => c.Status).ToList();

            notFound = serviceCodes.Except(serviceList.Select(c => c.ServiceCode)).ToList();
            return notFound;
        }
        #endregion
    }
}
