//------------------------------------------------------------------------
// <copyright file="ChargeFactorType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Catalog;
    using Dto.Catalogs;
    using Entities.Catalog;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ChargeFactorType
    /// </summary>
    public class ChargeFactorTypeBusiness : IChargeFactorTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// ChargeFactorTypeRepository Repository
        /// </summary>
        private readonly IChargeFactorTypeRepository ChargeFactorTypeRepository;

        /// <summary>
        /// ChargeFactorType Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="ChargeFactorTypeRepository"></param>
        public ChargeFactorTypeBusiness(IUnitOfWork unitOfWork, IChargeFactorTypeRepository ChargeFactorTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.ChargeFactorTypeRepository = ChargeFactorTypeRepository;
        }

        /// <summary>
        /// FindChargeFactorTypeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChargeFactorTypeDto FindChargeFactorTypeById(int id)
        {
            try
            {
                ChargeFactorType model = this.ChargeFactorTypeRepository.FindById(id);
                ChargeFactorTypeDto dto = Mapper.Map<ChargeFactorType, ChargeFactorTypeDto>(model);
                return dto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// GetAllChargeFactorTypes
        /// </summary>
        /// <returns></returns>
        public IList<ChargeFactorTypeDto> GetAllChargeFactorTypes()
        {
            try
            {
                IList<ChargeFactorType> chargeModel = this.ChargeFactorTypeRepository.GetAll();
                IList<ChargeFactorTypeDto> chargeDto = Mapper.Map<IList<ChargeFactorType>, IList<ChargeFactorTypeDto>>(chargeModel);
                return chargeDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }
    }
}
