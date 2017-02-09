//------------------------------------------------------------------------
// <copyright file="ProviderBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;
    using AutoMapper;
    using MapConfiguration;
    using ExceptionBusiness;
    using Resources;

    /// <summary>
    /// Provider Business Logic
    /// </summary>
    public class ProviderBusiness : IProviderBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The provider repository
        /// </summary>
        private readonly IProviderRepository providerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="providerRepository"></param>
        public ProviderBusiness(IUnitOfWork unitOfWork, IProviderRepository providerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.providerRepository = providerRepository;
        }

        /// <summary>
        /// Get all providers
        /// </summary>
        /// <returns></returns>
        public IList<ProviderDto> GetAllProvider()
        {
            try
            {
                IList<Provider> providerModel = this.providerRepository.GetAll().ToList();
                IList<ProviderDto> providerEntity = new List<ProviderDto>();
                providerEntity = Mapper.Map<IList<Provider>, IList<ProviderDto>>(providerModel);
                return providerEntity.ToList();
            }
            catch(Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get the provider in base of their identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProviderDto FindProviderById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                Provider providerModel = this.providerRepository.FindById(id);
                ProviderDto ProviderEntity = new ProviderDto();
                ProviderEntity = Mapper.Map<Provider, ProviderDto>(providerModel);
                return ProviderEntity;
            }
            catch(Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// add a new provider
        /// </summary>
        /// <param name="providerDto"></param>
        /// <returns></returns>
        public bool AddProvider(ProviderDto providerDto)
        {
            if (providerDto == null)
            {
                return false;
            }

            if (this.IsProviderNumberDuplicated(providerDto.ProviderNumber))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
                {
                    Provider providerModel = new Provider();
                    providerModel = Mapper.Map<ProviderDto, Provider>(providerDto);
                    providerModel.Status = true;
                    this.providerRepository.Add(providerModel);
                    this.unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// delete a provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool DeleteProvider(ProviderDto provider)
        {
            if (provider != null)
            {
                try
                {
                    ProviderDto providerDto = new ProviderDto();
                    Provider providerModel = this.providerRepository.FindById(provider.ProviderNumber);

                    /*
                     * Si se quiere hacer un borrado permanente de la DB
                     * se debe de usar la siguiente instrucción:
                     * this.providerDal.Delete(providerDal);
                    */
                    providerModel.Status = false;
                    this.providerRepository.Update(providerModel);
                    this.unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message.ToString());
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// update the information of provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool UpdateProvider(ProviderDto provider)
        {
            if (provider != null)
            {
                try
                {
                    if (provider.ProviderNumber != null)
                    {
                        Provider providerModel = this.providerRepository.FindById(provider.ProviderNumber);
                        if (providerModel.ProviderNumber != null)
                        {
                            providerModel.ProviderName = provider.ProviderName;
                            providerModel.ProviderShortName = provider.ProviderShortName;
                            providerModel.Status = provider.Status;

                            this.providerRepository.Update(providerModel);
                            this.unitOfWork.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message.ToString());
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the active providers
        /// </summary>
        /// <returns></returns>
        public IList<ProviderDto> GetActivesProvider()
        {
            try
            {
                IList<Provider> providerModel = this.providerRepository.GetProviders().ToList();
                IList<ProviderDto> providerDto = new List<ProviderDto>();
                providerDto = Mapper.Map<IList<Provider>, IList<ProviderDto>>(providerModel);
                //foreach (Provider provider in providerModel)
                //{
                //    providerDto.Add(new ProviderDto
                //    {
                //        ProviderNumber = provider.ProviderNumber,
                //        ProviderName = provider.ProviderName,
                //        ProviderShortName = provider.ProviderShortName,
                //        Status = provider.Status
                //    });
                //}
                return providerDto.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Validates if a duplicated provider number exists.
        /// </summary>
        /// <param name="providerNumber"></param>
        /// <returns><c>true</c>  if exists <c>false</returns>
        private bool IsProviderNumberDuplicated(string providerNumber)
        {
            Provider providers = this.providerRepository.FindById(providerNumber);
            return providers != null ? true : false;
        }
    }
}
