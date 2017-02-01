//------------------------------------------------------------------------
// <copyright file="CompartmentTypeBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// CompartmentType operations
    /// </summary>
    public class CompartmentTypeBusiness : ICompartmentTypeBusiness
    {
        /// <summary>
        /// The component type repository
        /// </summary>
        private readonly ICompartmentTypeRepository compartmentTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentTypeBusiness"/> class.
        /// </summary>
        /// <param name="compartmentTypeRepository">The compartment type repository.</param>
        public CompartmentTypeBusiness(ICompartmentTypeRepository compartmentTypeRepository)
        {
            this.compartmentTypeRepository = compartmentTypeRepository;
        }

        /// <summary>
        /// Gets all compartment types.
        /// </summary>
        /// <returns>
        /// All compartments.
        /// </returns>
        public IList<CompartmentTypeDto> GetAllCompartmentType()
        {
            try
            {
                IList<CompartmentTypeDto> compartmentTypeDto = new List<CompartmentTypeDto>();
                IList<CompartmentType> compartmentTypeEntity;
                compartmentTypeEntity = this.compartmentTypeRepository.GetAll();
                compartmentTypeDto = Mapper.Map<IList<CompartmentType>, IList<CompartmentTypeDto>>(compartmentTypeEntity);
                return compartmentTypeDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Finds the compartment types by its identifier.
        /// </summary>
        /// <param name="id">The CompartmentType identifier.</param>
        /// <returns>
        /// CompartmentType Entity.
        /// </returns>
        public CompartmentTypeDto FindCompartmentById(string id)
        {
            CompartmentTypeDto compartmentTypeDto = new CompartmentTypeDto();
            try
            {                
                CompartmentType compartmentTypeEntity;
                compartmentTypeEntity = this.compartmentTypeRepository.FindById(id);
                compartmentTypeDto = Mapper.Map<CompartmentType, CompartmentTypeDto>(compartmentTypeEntity);
                return compartmentTypeDto;
            }
            catch (Exception)
            {
                return compartmentTypeDto;
            }
        }

        /// <summary>
        /// Gets the all actives compartment types.
        /// </summary>
        /// <returns>
        /// List of actives CompartmentTypes.
        /// </returns>
        public IList<CompartmentTypeDto> GetActiveCompartmentType()
        {
            IList<CompartmentTypeDto> compartmentTypeDto = new List<CompartmentTypeDto>();
            try
            {                
                IList<CompartmentType> compartmentTypeEntity;
                compartmentTypeEntity = this.compartmentTypeRepository.GetActiveCompartmentType();
                compartmentTypeDto = Mapper.Map<IList<CompartmentType>, IList<CompartmentTypeDto>>(compartmentTypeEntity);
                return compartmentTypeDto;
            }
            catch (Exception)
            {
                return compartmentTypeDto;
            }
        }
    }
}
