//-----------------------------------------------------------------------------
// <copyright file="NationalFuelContractBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// National Fuel Contract Business
    /// </summary>
    public class NationalFuelContractBusiness : INationalFuelContractBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The national contract repository
        /// </summary>
        private readonly INationalFuelContractRepository nationalContractRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="nationalContractRepository">The national contract repository.</param>
        public NationalFuelContractBusiness(
            IUnitOfWork unitOfWork,
            INationalFuelContractRepository nationalContractRepository)
        {
            this.nationalContractRepository = nationalContractRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds the national fuel contract by identifier.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// The national fuel contract.
        /// </returns>
        public NationalFuelContractDto FindNationalFuelContractById(NationalFuelContractDto contract)
        {
            if (contract == null
                || contract.EffectiveDate == default(DateTime)
                || string.IsNullOrWhiteSpace(contract.AirlineCode)
                || string.IsNullOrWhiteSpace(contract.StationCode)
                || string.IsNullOrWhiteSpace(contract.ServiceCode)
                || string.IsNullOrWhiteSpace(contract.ProviderNumberPrimary))
            {
                return null;
            }

            try
            {
                NationalFuelContract entity = this.nationalContractRepository.FindByContract(
                    contract.EffectiveDate,
                    contract.AirlineCode,
                    contract.StationCode,
                    contract.ServiceCode,
                    contract.ProviderNumberPrimary);

                NationalFuelContractDto result = Mapper.Map<NationalFuelContractDto>(entity);
                return result;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        public bool AddNationalFuelContract(NationalFuelContractDto contract)
        {
            if (contract == null
                || contract.EffectiveDate == default(DateTime)
                || string.IsNullOrWhiteSpace(contract.AirlineCode)
                || string.IsNullOrWhiteSpace(contract.StationCode)
                || string.IsNullOrWhiteSpace(contract.ServiceCode)
                || string.IsNullOrWhiteSpace(contract.ProviderNumberPrimary))
            {
                return false;
            }

            if (this.IsFuelContractDuplicate(contract))
            {
                throw new BusinessException(Messages.DuplicateValue, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                if (contract.NationalFuelContractConcept.Count() > 0)
                {
                    contract.NationalFuelContractConcept.ToList().ForEach(c =>
                    {
                        c.EffectiveDate = contract.EffectiveDate;
                        c.AirlineCode = contract.AirlineCode;
                        c.StationCode = contract.StationCode;
                        c.ServiceCode = contract.ServiceCode;
                        c.ProviderNumberPrimary = contract.ProviderNumberPrimary;
                    });
                }

                NationalFuelContract entity = Mapper.Map<NationalFuelContract>(contract);
                entity.Status = true;
                this.nationalContractRepository.Add(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        public bool DeleteNationalFuelContract(NationalFuelContractDto contract)
        {
            if (contract == null
                || contract.EffectiveDate == default(DateTime)
                || string.IsNullOrWhiteSpace(contract.AirlineCode)
                || string.IsNullOrWhiteSpace(contract.StationCode)
                || string.IsNullOrWhiteSpace(contract.ServiceCode)
                || string.IsNullOrWhiteSpace(contract.ProviderNumberPrimary))
            {
                return false;
            }

            try
            {
                NationalFuelContract entity = this.nationalContractRepository.FindByContract(
                    contract.EffectiveDate,
                    contract.AirlineCode,
                    contract.StationCode,
                    contract.ServiceCode,
                    contract.ProviderNumberPrimary);

                if (entity == null)
                {
                    return false;
                }

                this.nationalContractRepository.Delete(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// true if was updated else false
        /// </returns>
        public bool UpdateNationalFuelContract(NationalFuelContractDto contract)
        {
            if (contract == null
                || contract.EffectiveDate == default(DateTime)
                || string.IsNullOrWhiteSpace(contract.AirlineCode)
                || string.IsNullOrWhiteSpace(contract.StationCode)
                || string.IsNullOrWhiteSpace(contract.ServiceCode)
                || string.IsNullOrWhiteSpace(contract.ProviderNumberPrimary))
            {
                return false;
            }

            try
            {
                NationalFuelContract entity = this.nationalContractRepository.FindByContract(
                    contract.EffectiveDate,
                    contract.AirlineCode,
                    contract.StationCode,
                    contract.ServiceCode,
                    contract.ProviderNumberPrimary);

                if (entity == null)
                {
                    return false;
                }

                entity.AccountingAccountNumber = contract.AccountingAccountNumber;
                entity.LiabilityAccountNumber = contract.LiabilityAccountNumber;
                entity.CostCenterNumber = contract.CCNumber;
                entity.CurrencyCode = contract.CurrencyCode;
                entity.FederalTaxCode = contract.FederalTaxCode;
                entity.FederalTaxFlag = contract.FederalTaxFlag;
                entity.FederalTaxValue = contract.FederalTaxValue;
                entity.OperationTypeID = contract.OperationTypeID;
                entity.AircraftRegistCCFlag = contract.AircraftRegistCCFlag;

                this.nationalContractRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the national fuel contracts paginated.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel contract paginated.
        /// </returns>
        public IList<NationalFuelContractDto> SearchNationalFuelContractsPaginated(NationalFuelContractDto parameters, int pageSize, int pageNumber)
        {
            if (parameters == null)
            {
                return null;
            }

            int skip = (pageNumber - 1) * pageSize;
            try
            {
                NationalFuelContract param = Mapper.Map<NationalFuelContract>(parameters);
                List<NationalFuelContract> contracts = new List<NationalFuelContract>();
                contracts = this.nationalContractRepository.GetContracsByParameters(param, pageSize, skip).ToList();
                return Mapper.Map<List<NationalFuelContractDto>>(contracts);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the national fuel contracts paginated.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel contract paginated.
        /// </returns>
        public IList<NationalFuelContractDto> GetNationalFuelContractsPaginated(int pageSize, int pageNumber)
        {
            int skip = (pageNumber - 1) * pageSize;
            try
            {
                DateTime currentDate = this.nationalContractRepository.GetServerDate();
                List<NationalFuelContract> activeContracts = this.nationalContractRepository.GetAllWithRealationships().ToList();
                List<NationalFuelContract> effectiveDates = activeContracts
                    .Where(c => c.EffectiveDate <= currentDate)
                    .GroupBy(con => new
                    {
                        con.AirlineCode,
                        con.StationCode,
                        con.ServiceCode,
                        con.ProviderNumberPrimary,
                    })
                    .Select(n => new NationalFuelContract
                    {
                        EffectiveDate = n.Max(c => c.EffectiveDate),
                        AirlineCode = n.Key.AirlineCode,
                        StationCode = n.Key.StationCode,
                        ServiceCode = n.Key.ServiceCode,
                        ProviderNumberPrimary = n.Key.ProviderNumberPrimary
                    })
                    .ToList();

                List<NationalFuelContract> effectiveContractToday = activeContracts
                    .Join(
                        effectiveDates,
                        c => new
                        {
                            c.EffectiveDate,
                            c.AirlineCode,
                            c.StationCode,
                            c.ServiceCode,
                            c.ProviderNumberPrimary
                        },
                        d => new
                        {
                            d.EffectiveDate,
                            d.AirlineCode,
                            d.StationCode,
                            d.ServiceCode,
                            d.ProviderNumberPrimary,
                        },
                        (c, d) => c)
                    .Where(c => c.Status).ToList()
                    .Take(pageSize)
                    .Skip(skip)
                    .OrderBy(c => c.EffectiveDate)
                    .ThenBy(c => c.AirlineCode)
                    .ThenBy(c => c.StationCode)
                    .ToList();

                List<NationalFuelContractDto> effectiveContractTodayDto = new List<NationalFuelContractDto>();
                if (effectiveContractToday != null)
                {
                    effectiveContractTodayDto = Mapper.Map<List<NationalFuelContract>, List<NationalFuelContractDto>>(effectiveContractToday);
                }

                return effectiveContractTodayDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Count the records by the parameters given.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Retunrs contracts count.
        /// </returns>
        public int CountSearchContracts(NationalFuelContractDto parameters)
        {
            try
            {
                NationalFuelContract param = Mapper.Map<NationalFuelContract>(parameters);
                int count = this.nationalContractRepository.CountContracsByParameters(param);
                return count;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Count the total effectives contracts.
        /// </summary>
        /// <returns>
        /// Retunrs the total of effectives contracts.
        /// </returns>
        public int CountEffectivesContracts()
        {
            try
            {
                DateTime currentDate = this.nationalContractRepository.GetServerDate();
                List<NationalFuelContract> activeContracts = this.nationalContractRepository.GetAll().ToList();
                List<NationalFuelContract> effectiveDates = activeContracts
                    .Where(c => c.EffectiveDate <= currentDate)
                    .GroupBy(con => new
                    {
                        con.AirlineCode,
                        con.StationCode,
                        con.ServiceCode,
                        con.ProviderNumberPrimary,
                    })
                    .Select(n => new NationalFuelContract
                    {
                        EffectiveDate = n.Max(c => c.EffectiveDate),
                        AirlineCode = n.Key.AirlineCode,
                        StationCode = n.Key.StationCode,
                        ServiceCode = n.Key.ServiceCode,
                        ProviderNumberPrimary = n.Key.ProviderNumberPrimary
                    })
                    .ToList();

                List<NationalFuelContract> effectiveContractToday = activeContracts
                    .Join(
                        effectiveDates,
                        c => new
                        {
                            c.EffectiveDate,
                            c.AirlineCode,
                            c.StationCode,
                            c.ServiceCode,
                            c.ProviderNumberPrimary
                        },
                        d => new
                        {
                            d.EffectiveDate,
                            d.AirlineCode,
                            d.StationCode,
                            d.ServiceCode,
                            d.ProviderNumberPrimary,
                        },
                        (c, d) => c)
                    .Where(c => c.Status).ToList();
                return effectiveContractToday != null ? effectiveContractToday.Count : 0;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Inactivate the national fuel contract.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="endDateContract">The end date contract.</param>
        /// <returns>
        ///   <c>true</c> if inactivate successfully, <c>false</c> otherwise.
        /// </returns>
        public bool InactivateAirportSerciveContract(
            DateTime? effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
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
                NationalFuelContract nationalContract = this.nationalContractRepository.FindByContract(
                    (DateTime)effectiveDate,
                     airlineCode,
                     stationCode,
                     serviceCode,
                     providerNumber);

                if (nationalContract == null)
                {
                    throw new BusinessException(Messages.FailedFindRecord);
                }

                NationalFuelContractDto airportServiceContractDto = new NationalFuelContractDto
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = nationalContract.AirlineCode,
                    StationCode = nationalContract.StationCode,
                    ServiceCode = nationalContract.ServiceCode,
                    ProviderNumberPrimary = nationalContract.ProviderNumberPrimary,
                };

                if (this.IsFuelContractDuplicate(airportServiceContractDto))
                {
                    throw new BusinessException(Messages.DuplicateValue, Messages.DuplicatePrimaryKey, 10);
                }

                NationalFuelContract nationalContractInactive = new NationalFuelContract
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = nationalContract.AirlineCode,
                    StationCode = nationalContract.StationCode,
                    ServiceCode = nationalContract.ServiceCode,
                    ProviderNumberPrimary = nationalContract.ProviderNumberPrimary,
                    CostCenterNumber = nationalContract.CostCenterNumber,
                    AccountingAccountNumber = nationalContract.AccountingAccountNumber,
                    LiabilityAccountNumber = nationalContract.LiabilityAccountNumber,
                    OperationTypeID = nationalContract.OperationTypeID,
                    AircraftRegistCCFlag = nationalContract.AircraftRegistCCFlag,
                    FederalTaxFlag = nationalContract.FederalTaxFlag,
                    FederalTaxCode = nationalContract.FederalTaxCode,
                    FederalTaxValue = nationalContract.FederalTaxValue,
                    CurrencyCode = nationalContract.CurrencyCode,
                    Status = false
                };

                this.nationalContractRepository.Add(nationalContractInactive);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether the contract is duplicated.
        /// </summary>
        /// <param name="contract">The fuel contract.</param>
        /// <returns><c>true</c> if the contract was found <c>false</c> otherwise.</returns>
        private bool IsFuelContractDuplicate(NationalFuelContractDto contract)
        {
            NationalFuelContract entity = this.nationalContractRepository.FindByContract(
                contract.EffectiveDate,
                contract.AirlineCode,
                contract.StationCode,
                contract.ServiceCode,
                contract.ProviderNumberPrimary);

            return entity != null ? true : false;
        }

        /// <summary>
        /// Gets Contracts by Airline and Airport
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public IList<NationalFuelContractDto> GetContractsByAirlineAirport(NationalFuelContractDto contract)
        {
            var contractsDto = new List<NationalFuelContractDto>();
            var entity = new NationalFuelContract() { AirlineCode = contract.AirlineCode, StationCode = contract.StationCode };

            try
            {
                contractsDto = Mapper.Map<List<NationalFuelContractDto>>(this.nationalContractRepository.GetContractsByAirlineAirport(entity));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return contractsDto;
        }
    }
}
