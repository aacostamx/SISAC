//------------------------------------------------------------------------
// <copyright file="IServiceRepository.cs" company="AACOSTA">
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
    /// Service Repository Interface
    /// </summary>
    public interface IServiceRepository : IRepository<Service>
    {
        /// <summary>
        /// Find the Service by the identifier
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>A list of Service</returns>
        Service FindById(string id);

        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns>A list of Services</returns>
        IList<Service> GetServices();

        /// <summary>
        /// Validate if the services exist.
        /// </summary>
        /// <param name="serviceCodes">The service codes to validate.</param>
        /// <returns>Returns a list with the Service Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfServiceExist(IList<string> serviceCodes);
    } 
}
