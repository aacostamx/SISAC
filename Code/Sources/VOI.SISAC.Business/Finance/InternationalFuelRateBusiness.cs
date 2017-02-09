//------------------------------------------------------------------------
// <copyright file="InternationalFuelRateBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using Common;
    using Entities.Finance;
    using ExceptionBusiness;
    using MapConfiguration;
    using Resources;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using DAL = VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Implement Interface of International Fuel Rate Business
    /// </summary>
    public class InternationalFuelRateBusiness : IInternationalFuelRateBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// international Fuel Rate Repository
        /// </summary>
        private readonly IInternationalFuelRateRepository internationalfuelRateRepository;

        /// <summary>
        /// international Fuel Contract Repository
        /// </summary>
        private readonly IInternationalFuelContractRepository internationalFuelContractRepository;

        /// <summary>
        /// interface Massive Upload Business
        /// </summary>
        private readonly IMassiveUploadBusiness massiveUploadBusiness;

        /// <summary>
        /// Interface Generic Catalog Business
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// Interface International Fuel Contract Business
        /// </summary>
        private readonly IInternationalFuelContractBusiness internationalFuelContractBusiness;

        /// <summary>
        /// Interface International Fuel Contract Concept Business
        /// </summary>
        private readonly IInternationalFuelContractConceptBusiness internationalFuelContractConceptBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelRateBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">unit Of Work parameter</param>
        /// <param name="internationalfuelRateRepository">international fuel Rate Repository</param>
        /// <param name="massiveUploadBusiness">massive Upload Business</param>
        /// <param name="generic">The generic</param>
        /// <param name="internationalFuelContractRepository">international Fuel Contract Repository</param>
        /// <param name="internationalFuelContractBusiness">international Fuel Contract Business</param>
        /// <param name="internationalFuelContractConceptBusiness">international Fuel Contract Concept Business</param>
        public InternationalFuelRateBusiness(
            IUnitOfWork unitOfWork,
            IInternationalFuelRateRepository internationalfuelRateRepository, 
            IMassiveUploadBusiness massiveUploadBusiness, 
            IGenericCatalogBusiness generic, 
            IInternationalFuelContractRepository internationalFuelContractRepository, 
            IInternationalFuelContractBusiness internationalFuelContractBusiness, 
            IInternationalFuelContractConceptBusiness internationalFuelContractConceptBusiness)
        {
            this.unitOfWork = unitOfWork;
            this.massiveUploadBusiness = massiveUploadBusiness;
            this.generic = generic;
            this.internationalFuelContractBusiness = internationalFuelContractBusiness;
            this.internationalfuelRateRepository = internationalfuelRateRepository;
            this.internationalFuelContractRepository = internationalFuelContractRepository;
            this.internationalFuelContractConceptBusiness = internationalFuelContractConceptBusiness;
        }

        /// <summary>
        /// Get all International Fuel Rates
        /// </summary>
        /// <returns>a Lists of Fuel Rates</returns>
        public IList<InternationalFuelRateDto> GetAllInternationalFuelRates()
        {
            try
            {
                IList<InternationalFuelRate> internationalfuelrates = this.internationalfuelRateRepository.GetAll().ToList();
                IList<InternationalFuelRateDto> internationalFuelRatesList = new List<InternationalFuelRateDto>();
                internationalFuelRatesList = Mapper.Map<IList<InternationalFuelRate>, IList<InternationalFuelRateDto>>(internationalfuelrates);
                return internationalFuelRatesList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Find International Fuel Rate By Id
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>a list of International Fuel Rate</returns>
        public InternationalFuelRateDto FindInternationalFuelRateById(long id)
        {
            if (id == 0)
            {
                return null;
            }

            try
            {
                InternationalFuelRate internationalFuelRate = this.internationalfuelRateRepository.FindById(id);
                InternationalFuelRateDto internationalFuelRateList = new InternationalFuelRateDto();
                internationalFuelRateList = Mapper.Map<InternationalFuelRate, InternationalFuelRateDto>(internationalFuelRate);
                return internationalFuelRateList;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Delete Fuel Rates
        /// </summary>
        /// <param name="intFuelRates"> InternationalFuelRate object </param>
        /// <returns>Delete a list of fuel rates</returns>
        public bool DeleteInternationalFuelRate(InternationalFuelRateDto intFuelRates)
        {
            try
            {
                InternationalFuelRate internationalfuelrate = this.internationalfuelRateRepository.FindById(intFuelRates.InternationalFuelRateID);
                this.internationalfuelRateRepository.Delete(internationalfuelrate);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Update International Fuel Rate
        /// </summary>
        /// <param name="intFuelRates">InternationalFuelRateD Object</param>
        /// <returns>indicates if true record the register</returns>
        public bool UpdateInternationalFuelRate(InternationalFuelRateDto intFuelRates)
        {
            if (intFuelRates == null)
            {
                return false;
            }

            try
            {
                InternationalFuelRate internationalfuelrate = this.internationalfuelRateRepository.FindById(intFuelRates.InternationalFuelRateID);
                internationalfuelrate.Rate = intFuelRates.Rate;
                this.internationalfuelRateRepository.Update(internationalfuelrate);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Rate in base of Dates Parameters
        /// </summary>
        /// <param name="internationalFuelRatedto">InternationalFuelRate Object</param>
        /// <returns>A list of Fuel Rates</returns>
        public IList<InternationalFuelRateDto> GetAllSearchInternationalFuelRates(InternationalFuelRateDto internationalFuelRatedto)
        {
            try
            {
                IList<InternationalFuelRate> internationalFuelRate = this.internationalfuelRateRepository.GetAllSearchInternationalFuelRate(Mapper.Map<InternationalFuelRateDto, InternationalFuelRate>(internationalFuelRatedto)).ToList();
                IList<InternationalFuelRateDto> internationalFuelRateDto = new List<InternationalFuelRateDto>();

                internationalFuelRateDto = Mapper.Map<IList<InternationalFuelRate>, IList<InternationalFuelRateDto>>(internationalFuelRate);
                return internationalFuelRateDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Delete a Range of Rates in base of dates parameters
        /// </summary>
        /// <param name="intFuelRates">InternationalFuelRate Object</param>
        /// <returns>a list of fuel rates</returns>
        public IList<InternationalFuelRateDto> DeleteRangeofFuelRates(InternationalFuelRateDto intFuelRates)
        {
            IList<InternationalFuelRate> internationalFuelRate = new List<InternationalFuelRate>();
            try
            {
                this.internationalfuelRateRepository.DeleteAllInternationalFuelRate(Mapper.Map<InternationalFuelRateDto, InternationalFuelRate>(intFuelRates)).ToList();
                IList<InternationalFuelRateDto> internationalFuelRateDto = new List<InternationalFuelRateDto>();
                internationalFuelRateDto = Mapper.Map<IList<InternationalFuelRate>, IList<InternationalFuelRateDto>>(internationalFuelRate);
                this.unitOfWork.Commit();
                return internationalFuelRateDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }
        
        /// <summary>
        /// Get actives contracts
        /// </summary>
        /// <param name="fileFuelRatesDto">FileFuelRates Object</param>
        /// <returns>List of contracts</returns>
        public IList<InternationalFuelContractDto> GetContracts(IList<InternationalFuelRateFileDto> fileFuelRatesDto)
        {
            try
            {
                IList<InternationalFuelContract> internationalFuelContractActives = this.internationalFuelContractRepository.GetAll().ToList();
                IList<InternationalFuelContract> internationalFuelContractInactives = this.internationalFuelContractRepository.GetInactivesFuelContracts().ToList();
                IList<InternationalFuelContract> internationalFuelContractActivesLastEfectiveDate = new List<InternationalFuelContract>();
                var contractDistinct = (from ssi in internationalFuelContractActives
                                       group ssi by new { ssi.EffectiveDate, ssi.AirlineCode, ssi.StationCode, ssi.ServiceCode, ssi.ProviderNumberPrimary } into g
                                       select new { EffectiveDate = g.Key.EffectiveDate, AirlineCode = g.Key.AirlineCode, StationCode = g.Key.StationCode, ServiceCode = g.Key.ServiceCode, ProviderNumberPrimary = g.Key.ProviderNumberPrimary, Count = g.Count() }).ToList();

                IList<InternationalFuelContract> contractDistinctList = new List<InternationalFuelContract>();
                var trueFalse = string.Empty;

                foreach (var con in contractDistinct)
                {
                    trueFalse = string.Empty;
                    if (internationalFuelContractInactives.Count > 0)
                    {
                        trueFalse = internationalFuelContractInactives.Where(e => e.EffectiveDate == con.EffectiveDate
                                                                                   && e.AirlineCode == con.AirlineCode
                                                                                   && e.StationCode == con.StationCode
                                                                                   && e.ServiceCode == con.ServiceCode
                                                                                   && e.ProviderNumberPrimary == con.ProviderNumberPrimary).Select(e => e.EndDateContract).FirstOrDefault().ToString();
                    }

                    if (trueFalse == string.Empty)
                    {
                       contractDistinctList.Add(new InternationalFuelContract() { EffectiveDate = con.EffectiveDate, AirlineCode = con.AirlineCode, StationCode = con.StationCode, ServiceCode = con.ServiceCode, ProviderNumberPrimary = con.ProviderNumberPrimary });
                    }
                }

                InternationalFuelContract contractLast = new InternationalFuelContract();
                IList<InternationalFuelContract> contractFoundList = new List<InternationalFuelContract>();
                foreach (InternationalFuelContract contractItem in contractDistinctList)
                {
                    contractFoundList = this.internationalFuelContractRepository.FindByContractKeyExcludeEffectiveDate(contractItem);
                    contractItem.EffectiveDate = contractFoundList.Max(p => p.EffectiveDate);
                    contractLast = this.internationalFuelContractRepository.FindById(contractItem);

                    internationalFuelContractActivesLastEfectiveDate.Add(contractLast);
                }

                IList<InternationalFuelContractDto> internationalFuelContractActivesLastEfectiveDateDto = new List<InternationalFuelContractDto>();

                internationalFuelContractActivesLastEfectiveDateDto = Mapper.Map<IList<InternationalFuelContract>, IList<InternationalFuelContractDto>>(internationalFuelContractActivesLastEfectiveDate);
                return internationalFuelContractActivesLastEfectiveDateDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Validates if a duplicated fuel rates exists.
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>a parameter true or false</returns>
        private bool IsInternationalFuelRateDuplicated(long id)
        {
            InternationalFuelRate fuelRates = this.internationalfuelRateRepository.FindById(id);
            return fuelRates != null ? true : false;
        }
        
    }
}