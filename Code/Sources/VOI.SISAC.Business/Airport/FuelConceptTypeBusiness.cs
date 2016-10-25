//--------------------------------------------------------------------
// <copyright file="FuelConceptTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using Dto.Airports;
    using System;
    using System.Collections.Generic;
    using Dal.Infrastructure;
    using Dal.Repository.Airports;
    using ExceptionBusiness;
    using Resources;
    using Entities.Airport;
    using AutoMapper;

    /// <summary>
    /// FuelConceptTypeBusiness
    /// </summary>
    public class FuelConceptTypeBusiness : IFuelConceptTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IFuelConceptTypeRepository FuelConceptTypeRepository;

        /// <summary>
        /// FuelConceptTypeBusiness Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="FuelConceptTypeRepository"></param>
        public FuelConceptTypeBusiness(IUnitOfWork unitOfWork, IFuelConceptTypeRepository FuelConceptTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.FuelConceptTypeRepository = FuelConceptTypeRepository;
        }

        /// <summary>
        /// GetAllFuelConceptTypes
        /// </summary>
        /// <returns></returns>
        public IList<FuelConceptTypeDto> GetAllFuelConceptTypes()
        {
            try
            {
                IList<FuelConceptType> FuelConceptTypeModel = this.FuelConceptTypeRepository.GetAll();
                IList<FuelConceptTypeDto> dto = Mapper.Map<IList<FuelConceptType>, IList<FuelConceptTypeDto>>(FuelConceptTypeModel);
                return dto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }
    }
}
