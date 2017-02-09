//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureTypeBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class Airplane Weigh tMeasure Type Business
    /// </summary>
    public class AirplaneWeightMeasureTypeBusiness : IAirplaneWeightMeasureTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightMeasureTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airplaneWeightMeasureTypeRepository">The airplane weight measure type repository.</param>
        public AirplaneWeightMeasureTypeBusiness(IUnitOfWork unitOfWork, IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airplaneWeightMeasureTypeRepository = airplaneWeightMeasureTypeRepository;
        }

        #region IAirplaneWeightMeasureTypeBusiness Members

        /// <summary>
        /// Gets the type of all airplane weight measure.
        /// </summary>
        /// <returns>
        /// IList AirplaneWeightMeasureTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Catalogs.AirplaneWeightMeasureTypeDto> GetAllAirplaneWeightMeasureType()
        {
            try
            {
                IList<AirplaneWeightMeasureType> airplaneWeightMeasureTypes = this.airplaneWeightMeasureTypeRepository.GetAll().ToList();
                IList<AirplaneWeightMeasureTypeDto> airplaneWeightMeasureTypesDto = new List<AirplaneWeightMeasureTypeDto>();

                airplaneWeightMeasureTypesDto = Mapper.Map<IList<AirplaneWeightMeasureType>, IList<AirplaneWeightMeasureTypeDto>>(airplaneWeightMeasureTypes);
                return airplaneWeightMeasureTypesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
