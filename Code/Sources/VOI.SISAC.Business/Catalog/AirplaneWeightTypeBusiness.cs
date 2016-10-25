// <copyright file="AirplaneWeightTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Class Airplane Weight Type Business
    /// </summary>
    public class AirplaneWeightTypeBusiness : IAirplaneWeightTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IAirplaneWeightTypeRepository airplaneWeightTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airplaneWeightTypeRepository">The airplane weight type repository.</param>
        public AirplaneWeightTypeBusiness(IUnitOfWork unitOfWork, IAirplaneWeightTypeRepository airplaneWeightTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airplaneWeightTypeRepository= airplaneWeightTypeRepository;
        }

        #region IAirplaneWeightTypeBusiness Members

        /// <summary>
        /// Gets the type of all airplane weight.
        /// </summary>
        /// <returns>
        /// IList AirplaneWeightTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Catalogs.AirplaneWeightTypeDto> GetAllAirplaneWeightType()
        {
            try
            {
                IList<AirplaneWeightType> airplaneWeightTypes = this.airplaneWeightTypeRepository.GetAll().ToList();
                IList<AirplaneWeightTypeDto> AirplaneWeightTypesDto = new List<AirplaneWeightTypeDto>();

                AirplaneWeightTypesDto = Mapper.Map<IList<AirplaneWeightType>, IList<AirplaneWeightTypeDto>>(airplaneWeightTypes);
                return AirplaneWeightTypesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
