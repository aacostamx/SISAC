//------------------------------------------------------------------------
// <copyright file="AirportServiceContractBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;    
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Operations for Airport service contract
    /// </summary>
    public class AirportServiceContractBusiness : IAirportServiceContractBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The contract repository
        /// </summary>
        private readonly IAirportServiceContractRepository contractRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceContractBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="contractRepository">The contract repository.</param>
        public AirportServiceContractBusiness(IUnitOfWork unitOfWork, IAirportServiceContractRepository contractRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractRepository = contractRepository;
        }

        #region IAirportServiceContractBusiness Members
        /// <summary>
        /// Finds the contract by its composite id.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>
        /// The specified contract.
        /// </returns>
        public AirportServiceContractDto FindById(
            DateTime? effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            if (effectiveDate == null)
            {
                return null;
            }

            try
            {
                AirportServiceContract contractEntity = this.contractRepository.FindById(
                    (DateTime)effectiveDate,
                    airlineCode,
                    stationCode,
                    serviceCode,
                    providerNumber,
                    costCenterNumber);
                AirportServiceContractDto contractDto = new AirportServiceContractDto();
                contractDto = Mapper.Map<AirportServiceContract, AirportServiceContractDto>(contractEntity);
                Trace.TraceInformation("Business. Finish mappin entity-dto");
                return contractDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets a list of the effective contracts.
        /// </summary>
        /// <returns>
        /// List of effective contracts.
        /// </returns>
        public IList<AirportServiceContractDto> GetEffectiveContracts()
        {
            try
            {
                DateTime currentDate = this.contractRepository.GetServerDate();
                List<AirportServiceContract> activeContracts = this.contractRepository.GetAll().ToList();
                List<AirportServiceContract> effectiveDates = activeContracts
                    .Where(c => c.EffectiveDate <= currentDate)
                    .GroupBy(con => new
                    {
                        con.AirlineCode,
                        con.StationCode,
                        con.ServiceCode,
                        con.ProviderNumber,
                        con.CostCenterNumber
                    })
                    .Select(n => new AirportServiceContract
                    {
                        EffectiveDate = n.Max(c => c.EffectiveDate),
                        AirlineCode = n.Key.AirlineCode,
                        StationCode = n.Key.StationCode,
                        ServiceCode = n.Key.ServiceCode,
                        ProviderNumber = n.Key.ProviderNumber,
                        CostCenterNumber = n.Key.CostCenterNumber
                    })
                    .ToList();

                List<AirportServiceContract> effectiveContractToday = activeContracts
                                            .Join(
                                                effectiveDates,
                                                c => new 
                                                { 
                                                    c.EffectiveDate, 
                                                    c.AirlineCode, 
                                                    c.StationCode, 
                                                    c.ServiceCode, 
                                                    c.ProviderNumber, 
                                                    c.CostCenterNumber 
                                                },
                                                d => new 
                                                { 
                                                    d.EffectiveDate, 
                                                    d.AirlineCode, 
                                                    d.StationCode, 
                                                    d.ServiceCode, 
                                                    d.ProviderNumber, 
                                                    d.CostCenterNumber 
                                                },
                                                (c, d) => c)
                                           .Where(c => c.Status).ToList();

                List<AirportServiceContractDto> effectiveContractTodayDto = new List<AirportServiceContractDto>();
                if (effectiveContractToday != null)
                {
                    effectiveContractTodayDto = Mapper.Map<List<AirportServiceContract>, List<AirportServiceContractDto>>(effectiveContractToday);
                }

                return effectiveContractTodayDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the contracts for airport and airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>
        /// List of contracts for airline and airport.
        /// </returns>
        public IList<AirportServiceContractDto> GetContractsForAirportAirline(string airlineCode, string airportCode)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the contracts by the parameters past in the object.
        /// </summary>
        /// <param name="parameters">The parameters to match in the query.</param>
        /// <returns>
        /// List of contracts that match with the specified parameters.
        /// </returns>
        public IList<AirportServiceContractDto> GetContractsByParameters(AirportServiceContractDto parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            try
            {
                IList<AirportServiceContract> airports;
                IList<AirportServiceContractDto> airportsDto;
                AirportServiceContract airport = Mapper.Map<AirportServiceContractDto, AirportServiceContract>(parameters);
                airports = this.contractRepository.GetContractsByParameters(airport);
                airportsDto = Mapper.Map<IList<AirportServiceContract>, IList<AirportServiceContractDto>>(airports);
                return airportsDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the airport service contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract.</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        public bool AddAirportSerciveContract(AirportServiceContractDto airportServiceContractDto)
        {
            if (airportServiceContractDto == null)
            {
                return false;
            }

            if (this.IsServiceContractDuplicate(airportServiceContractDto))
            {
                throw new BusinessException(Messages.DuplicateValue, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                AirportServiceContract airportServiceContract = new AirportServiceContract();
                airportServiceContractDto.Status = true;
                airportServiceContract = Mapper.Map<AirportServiceContract>(airportServiceContractDto);
                this.contractRepository.Add(airportServiceContract);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the airport service contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        public bool UpdateAirportSerciveContract(AirportServiceContractDto airportServiceContractDto)
        {
            try
            {
                if (airportServiceContractDto != null)
                {
                    AirportServiceContract serviceContract = this.contractRepository.FindById(
                              airportServiceContractDto.EffectiveDate,
                              airportServiceContractDto.AirlineCode,
                              airportServiceContractDto.StationCode,
                              airportServiceContractDto.ServiceCode,
                              airportServiceContractDto.ProviderNumber,
                              airportServiceContractDto.CostCenterNumber);

                    serviceContract.AccountingAccountNumber = airportServiceContractDto.AccountingAccountNumber;
                    serviceContract.LiabilityAccountNumber = airportServiceContractDto.LiabilityAccountNumber;
                    serviceContract.OperationTypeId = airportServiceContractDto.OperationTypeId;
                    serviceContract.ServiceTypeCode = airportServiceContractDto.ServiceTypeCode;
                    serviceContract.AirportFeeFlag = airportServiceContractDto.AirportFeeFlag;
                    serviceContract.AirportFeeCode = airportServiceContractDto.AirportFeeCode;
                    serviceContract.AirportFeeValue = airportServiceContractDto.AirportFeeValue;
                    serviceContract.LocalTaxFlag = airportServiceContractDto.LocalTaxFlag;
                    serviceContract.LocalTaxCode = airportServiceContractDto.LocalTaxCode;
                    serviceContract.LocalTaxValue = airportServiceContractDto.LocalTaxValue;
                    serviceContract.FederalTaxFlag = airportServiceContractDto.FederalTaxFlag;
                    serviceContract.FederalTaxCode = airportServiceContractDto.FederalTaxCode;
                    serviceContract.FederalTaxValue = airportServiceContractDto.FederalTaxValue;
                    serviceContract.StateTaxFlag = airportServiceContractDto.StateTaxFlag;
                    serviceContract.StateTaxCode = airportServiceContractDto.StateTaxCode;
                    serviceContract.StateTaxValue = airportServiceContractDto.StateTaxValue;
                    serviceContract.Rate = airportServiceContractDto.Rate;
                    serviceContract.CurrencyCode = airportServiceContractDto.CurrencyCode;
                    serviceContract.MultiRateFlag = airportServiceContractDto.MultiRateFlag;
                    serviceContract.AirplanetWeightFlag = airportServiceContractDto.AirplanetWeightFlag;
                    serviceContract.AirplaneWeightCode = airportServiceContractDto.AirplaneWeightCode;
                    serviceContract.AirplaneWeightUnit = airportServiceContractDto.AirplaneWeightUnit;
                    serviceContract.AirplaneWeightMultiplier = airportServiceContractDto.AirplaneWeightMultiplier;
                    serviceContract.ServiceRecordFlag = airportServiceContractDto.ServiceRecordFlag;
                    serviceContract.CalculationTypeId = airportServiceContractDto.CalculationTypeId;

                    this.contractRepository.Update(serviceContract);
                    this.unitOfWork.Commit();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the airport service contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        public bool DeleteAirportSerciveContract(AirportServiceContractDto airportServiceContractDto)
        {
            if (airportServiceContractDto == null)
            {
                return false;
            }

            try
            {
                AirportServiceContract airportServiceContract = this.contractRepository.FindById(
                     airportServiceContractDto.EffectiveDate,
                     airportServiceContractDto.AirlineCode,
                     airportServiceContractDto.StationCode,
                     airportServiceContractDto.ServiceCode,
                     airportServiceContractDto.ProviderNumber,
                     airportServiceContractDto.CostCenterNumber);
                if (airportServiceContract == null)
                {
                    return false;
                }

                this.contractRepository.Delete(airportServiceContract);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Inactivate the airport service contract.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <param name="endDateContract">The end date contract.</param>
        /// <returns>
        ///   <c>true</c> if inactivate successfully, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public bool InactivateAirportSerciveContract(
            DateTime? effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber,
            DateTime? endDateContract)
        {
            // Validates dates don't be null
            if (effectiveDate == null || endDateContract == null)
            {
                return false;
            }

            // End date must be greater than effective date
            if (effectiveDate.Value.CompareTo(endDateContract.Value) >= 0)
            {
                return false;
            }

            try
            {
                AirportServiceContract airportServiceContract = this.contractRepository.FindById(
                    (DateTime)effectiveDate,
                     airlineCode,
                     stationCode,
                     serviceCode,
                     providerNumber,
                     costCenterNumber);

                if (airportServiceContract == null)
                {
                    throw new BusinessException(Messages.FailedFindRecord);
                }

                AirportServiceContractDto airportServiceContractDto = new AirportServiceContractDto
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = airportServiceContract.AirlineCode,
                    StationCode = airportServiceContract.StationCode,
                    ServiceCode = airportServiceContract.ServiceCode,
                    ProviderNumber = airportServiceContract.ProviderNumber,
                    CostCenterNumber = airportServiceContract.CostCenterNumber,
                };

                if (this.IsServiceContractDuplicate(airportServiceContractDto))
                {
                    throw new BusinessException(Messages.DuplicateValue, Messages.DuplicatePrimaryKey, 10);
                }

                AirportServiceContract airportServiceContractInactive = new AirportServiceContract
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = airportServiceContract.AirlineCode,
                    StationCode = airportServiceContract.StationCode,
                    ServiceCode = airportServiceContract.ServiceCode,
                    ProviderNumber = airportServiceContract.ProviderNumber,
                    CostCenterNumber = airportServiceContract.CostCenterNumber,
                    AccountingAccountNumber = airportServiceContract.AccountingAccountNumber,
                    LiabilityAccountNumber = airportServiceContract.LiabilityAccountNumber,
                    OperationTypeId = airportServiceContract.OperationTypeId,
                    ServiceTypeCode = airportServiceContract.ServiceTypeCode,
                    AirportFeeFlag = airportServiceContract.AirportFeeFlag,
                    AirportFeeCode = airportServiceContract.AirportFeeCode,
                    AirportFeeValue = airportServiceContract.AirportFeeValue,
                    LocalTaxFlag = airportServiceContract.LocalTaxFlag,
                    LocalTaxCode = airportServiceContract.LocalTaxCode,
                    LocalTaxValue = airportServiceContract.LocalTaxValue,
                    FederalTaxFlag = airportServiceContract.FederalTaxFlag,
                    FederalTaxCode = airportServiceContract.FederalTaxCode,
                    FederalTaxValue = airportServiceContract.FederalTaxValue,
                    StateTaxFlag = airportServiceContract.StateTaxFlag,
                    StateTaxCode = airportServiceContract.StateTaxCode,
                    StateTaxValue = airportServiceContract.StateTaxValue,
                    Rate = airportServiceContract.Rate,
                    CurrencyCode = airportServiceContract.CurrencyCode,
                    MultiRateFlag = airportServiceContract.MultiRateFlag,
                    AirplanetWeightFlag = airportServiceContract.AirplanetWeightFlag,
                    AirplaneWeightCode = airportServiceContract.AirplaneWeightCode,
                    AirplaneWeightUnit = airportServiceContract.AirplaneWeightUnit,
                    AirplaneWeightMultiplier = airportServiceContract.AirplaneWeightMultiplier,
                    ServiceRecordFlag = airportServiceContract.ServiceRecordFlag,
                    CalculationTypeId = airportServiceContract.CalculationTypeId,
                    Status = false
                };

                this.contractRepository.Add(airportServiceContractInactive);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }
        #endregion

        /// <summary>
        /// Determines whether [is service contract duplicate] [the specified service contract].
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns><c>true</c> if was find <c>false</c> otherwise.</returns>
        private bool IsServiceContractDuplicate(AirportServiceContractDto serviceContract)
        {
            AirportServiceContract contract = this.contractRepository.FindById(
                serviceContract.EffectiveDate,
                serviceContract.AirlineCode,
                serviceContract.StationCode,
                serviceContract.ServiceCode,
                serviceContract.ProviderNumber,
                serviceContract.CostCenterNumber);

            return contract != null ? true : false;
        }
    }
}
