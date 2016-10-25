//------------------------------------------------------------------------
// <copyright file="IServiceBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Contract for Service Operations
    /// </summary>
    public interface IServiceBusiness
    {
        /// <summary>
        /// Get All Services
        /// </summary>
        /// <returns></returns>
        IList<ServiceDto> GetAllService();

        /// <summary>
        /// Get the Service by the identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceDto FindServiceById(string id);

        /// <summary>
        /// Add new service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddService(ServiceDto entity);

        /// <summary>
        /// Delete a service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteService(ServiceDto entity);

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateService(ServiceDto entity);

        /// <summary>
        /// Get the actve services.
        /// </summary>
        /// <returns></returns>
        IList<ServiceDto> GetActivesService();
    }
}
