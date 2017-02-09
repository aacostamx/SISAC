//-----------------------------------------------------------------------------
// <copyright file="UploadNationalFuelContractBusiness.cs" company="AACOSTA">
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
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Catalog;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Operations for upload national fuel contracts
    /// </summary>
    public class UploadNationalFuelContractBusiness : IUploadNationalFuelContractBusiness
    {
        /// <summary>
        /// The national contract
        /// </summary>
        private readonly INationalFuelContractRepository nationalContractRepository;

        /// <summary>
        /// The airline repository
        /// </summary>
        private readonly IAirlineRepository airlineRepository;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// The service repository
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// The service repository
        /// </summary>
        private readonly ICostCenterRepository costCenterRepository;

        /// <summary>
        /// The provider repository
        /// </summary>
        private readonly IProviderRepository providerRepository;

        /// <summary>
        /// The accounting account repository
        /// </summary>
        private readonly IAccountingAccountRepository accountingAccountRepository;

        /// <summary>
        /// The liability account repository
        /// </summary>
        private readonly ILiabilityAccountRepository liabilityAccountRepository;

        /// <summary>
        /// The currency repository
        /// </summary>
        private readonly ICurrencyRepository currencyRepository;

        /// <summary>
        /// The currency repository
        /// </summary>
        private readonly ITaxRepository taxRepository;

        /// <summary>
        /// The operation type repository
        /// </summary>
        private readonly IOperationTypeRepository operationTypeRepository;

        /// <summary>
        /// The fuel concept repository
        /// </summary>
        private readonly IFuelConceptRepository fuelConceptRepository;

        /// <summary>
        /// The fuel concept type repository
        /// </summary>
        private readonly IFuelConceptTypeRepository fuelConceptTypeRepository;

        /// <summary>
        /// The charge factor type repository
        /// </summary>
        private readonly IChargeFactorTypeRepository chargeFactorTypeRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadNationalFuelContractBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="nationalContractRepository">The national contract repository.</param>
        /// <param name="airlineRepository">The airline repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="costCenterRepository">The cost center repository.</param>
        /// <param name="providerRepository">The provider repository.</param>
        /// <param name="accountingAccountRepository">The accounting account repository.</param>
        /// <param name="liabilityAccountRepository">The liability account repository.</param>
        /// <param name="currencyRepository">The currency repository.</param>
        /// <param name="taxRepository">The tax repository.</param>
        /// <param name="operationTypeRepository">The operation type repository.</param>
        /// <param name="fuelConceptRepository">The fuel concept repository.</param>
        /// <param name="fuelConceptTypeRepository">The fuel concept type repository.</param>
        /// <param name="chargeFactorTypeRepository">The charge factor type repository.</param>
        public UploadNationalFuelContractBusiness(
            IUnitOfWork unitOfWork,
            INationalFuelContractRepository nationalContractRepository,
            IAirlineRepository airlineRepository,
            IAirportRepository airportRepository,
            IServiceRepository serviceRepository,
            ICostCenterRepository costCenterRepository,
            IProviderRepository providerRepository,
            IAccountingAccountRepository accountingAccountRepository,
            ILiabilityAccountRepository liabilityAccountRepository,
            ICurrencyRepository currencyRepository,
            ITaxRepository taxRepository,
            IOperationTypeRepository operationTypeRepository,
            IFuelConceptRepository fuelConceptRepository,
            IFuelConceptTypeRepository fuelConceptTypeRepository,
            IChargeFactorTypeRepository chargeFactorTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nationalContractRepository = nationalContractRepository;
            this.airlineRepository = airlineRepository;
            this.airportRepository = airportRepository;
            this.serviceRepository = serviceRepository;
            this.costCenterRepository = costCenterRepository;
            this.providerRepository = providerRepository;
            this.accountingAccountRepository = accountingAccountRepository;
            this.liabilityAccountRepository = liabilityAccountRepository;
            this.currencyRepository = currencyRepository;
            this.taxRepository = taxRepository;
            this.operationTypeRepository = operationTypeRepository;
            this.fuelConceptRepository = fuelConceptRepository;
            this.fuelConceptTypeRepository = fuelConceptTypeRepository;
            this.chargeFactorTypeRepository = chargeFactorTypeRepository;
        }

        /// <summary>
        /// Uploads the national fuel contracts.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>
        /// List of errors, if there is any.
        /// </returns>
        public IList<string> UploadNationalFuelContracts(IList<NationalFuelContractDto> contracts)
        {
            // If there isn't any items
            if (contracts == null || contracts.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            try
            {
                // Validates that the Catalogs exist
                errors = this.ValidateCatalogs(contracts).ToList();
                if (errors.Count > 0)
                {
                    return errors;
                }

                // Adds the specific ID for the fields that only have description
                errors = this.NationalFuelContractLoadCode(contracts).ToList();
                if (errors.Count > 0)
                {
                    return errors;
                }

                foreach (NationalFuelContractDto itemContract in contracts)
                {
                    string keyContract;

                    keyContract = "{ EffectiveDate: " + itemContract.EffectiveDate.ToString() + " , Airline:"
                                + itemContract.AirlineCode.ToString() + " , Airport:"
                                + itemContract.StationCode.ToString() + " , Service:"
                                + itemContract.ServiceCode.ToString() + " , Provider Number:"
                                + itemContract.ProviderNumberPrimary.ToString() + " }";

                    // Validations for cost center
                    // If cost center is required, validates that cost center belongs to the airline
                    if (!itemContract.AircraftRegistCCFlag)
                    {
                        if (this.ValidateCostCenterAirlineRelationship(itemContract.AirlineCode, itemContract.CostCenterName))
                        {
                            errors.Add("The cost center " + itemContract.CCNumber + " does not match with the airline " + itemContract.AirlineCode + ". In " + keyContract);
                        }
                    }

                    // If cost center is required
                    if (itemContract.AircraftRegistCCFlag && !string.IsNullOrWhiteSpace(itemContract.CCNumber))
                    {
                        errors.Add("If Aircraft Registration Flag is set to 1, the Cost Center must be empty. In " + keyContract);
                    }

                    // If cost center is not required
                    if (!itemContract.AircraftRegistCCFlag && string.IsNullOrWhiteSpace(itemContract.CCNumber))
                    {
                        errors.Add("If Aircraft Registration Flag is set to 0, the Cost Center must have a value. In " + keyContract);
                    }

                    // Validates that won't be duplicate concepts in the contract
                    errors.AddRange(ValidateConceptsDuplicatedFullKey(itemContract, keyContract));

                    // Validates that exist at least one concept equal to PEMEX and one equal to SUMINISTRO
                    errors.AddRange(ValidateRequieredConcepts(itemContract, keyContract));

                    if (this.IsNationalFuelContractDuplicate(itemContract))
                    {
                        errors.Add("Effective Date is less than Maximun Date or Existing Contract in " + keyContract);
                    }
                }

                if (errors.Count > 0)
                {
                    return errors;
                }

                List<NationalFuelContract> contractEntities = Mapper.Map<List<NationalFuelContract>>(contracts);
                contractEntities.ForEach(c => c.Status = true);
                this.nationalContractRepository.AddRange(contractEntities);
                this.unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }

            return errors;
        }

        /// <summary>
        /// Validates that the contract has at least one fuel concept type equal to PEMEX and one equal to SUMINISTRO.
        /// Also validates that the contract and one concept, equal to PEMEX, have the same provider.
        /// </summary>
        /// <param name="itemContract">The item contract.</param>
        /// <param name="keyContract">The key contract.</param>
        /// <returns>List of errors</returns>
        private static List<string> ValidateRequieredConcepts(NationalFuelContractDto itemContract, string keyContract)
        {
            List<string> errors = new List<string>();
            if (itemContract.NationalFuelContractConcept.Count() > 0)
            {
                NationalFuelContractConceptDto jetFuel = itemContract.NationalFuelContractConcept
                    .FirstOrDefault(c => c.ProviderNumber.Contains(itemContract.ProviderNumberPrimary) && c.FuelConceptTypeCode == "PMX");

                NationalFuelContractConceptDto intoPlane = itemContract.NationalFuelContractConcept
                    .FirstOrDefault(c => c.FuelConceptTypeCode == "SUMIN");

                if (jetFuel == null)
                {
                    errors.Add("You must have at least one fuel concept name PEMEX with the primary providers of the contract. In " + keyContract);
                }

                if (intoPlane == null)
                {
                    errors.Add("You must have at least one concept name SUMINISTRO. In " + keyContract);
                }
            }

            return errors;
        }

        /// <summary>
        /// Validates the concepts duplicated full key.
        /// </summary>
        /// <param name="itemContract">The item contract.</param>
        /// <param name="keyContract">The key contract.</param>
        /// <returns>List of errors.</returns>
        private static List<string> ValidateConceptsDuplicatedFullKey(NationalFuelContractDto itemContract, string keyContract)
        {
            List<string> errors = new List<string>();
            if (itemContract.NationalFuelContractConcept.Count() > 0)
            {
                var conceptDistinct = (from ssi in itemContract.NationalFuelContractConcept
                                       group ssi by new { ssi.FuelConceptID, ssi.ProviderNumber } into g
                                       select new { FuelConceptID = g.Key.FuelConceptID, ProviderNumber = g.Key.ProviderNumber }).ToList();

                // validates the concepts are unique
                if (conceptDistinct.Count() != itemContract.NationalFuelContractConcept.Count())
                {
                    errors.Add("There are repeated Concepts in Contract " + keyContract);
                }
            }

            return errors;
        }

        /// <summary>
        /// Validates catalogs existence.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> ValidateCatalogs(IList<NationalFuelContractDto> contracts)
        {
            List<string> errors = new List<string>();

            // Validate Airline
            errors.AddRange(this.ValidateAirline(contracts.Select(c => c.AirlineCode).ToList()));

            // Validate Airport
            errors.AddRange(this.ValidateAirport(contracts.Select(c => c.StationCode).ToList()));

            // Validate Service
            errors.AddRange(this.ValidateIfServiceIsNationalFuel(contracts.Select(c => c.ServiceCode).ToList()));

            // Validate Provider
            errors.AddRange(this.ValidateProvider(contracts.Select(c => c.ProviderNumberPrimary).ToList()));

            // Validate Cost Center
            errors.AddRange(this.ValidateCostCenter(contracts.Where(c => !string.IsNullOrWhiteSpace(c.CCNumber)).Select(c => c.CCNumber).ToList()));

            // Validate Accounting Account
            errors.AddRange(this.ValidateAccountingAccount(contracts.Select(c => c.AccountingAccountNumber).ToList()));

            // Validate Liability Account
            errors.AddRange(this.ValidateLiabilityAccount(contracts.Select(c => c.LiabilityAccountNumber).ToList()));

            // Validate Currency
            errors.AddRange(this.ValidateCurrency(contracts.Select(c => c.CurrencyCode).ToList()));

            // Validate Tax
            errors.AddRange(this.ValidateTax(contracts.Select(c => c.FederalTaxCode).ToList()));

            // Concepts (only provider)
            foreach (NationalFuelContractDto nationalFuelContractDto in contracts)
            {
                if (nationalFuelContractDto.NationalFuelContractConcept.Count() > 0)
                {
                    IList<string> lista = nationalFuelContractDto.NationalFuelContractConcept.Select(c => c.ProviderNumber).ToList();
                    errors.AddRange(this.ValidateProvider(lista));
                }
            }

            return errors;
        }

        /// <summary>
        /// Validates if the airlines exist.
        /// </summary>
        /// <param name="airlineCodes">The airline codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateAirline(IList<string> airlineCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.airlineRepository.ValidatedIfAirlineExist(airlineCodes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Airline code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the airports exist.
        /// </summary>
        /// <param name="stationCodes">The station codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateAirport(IList<string> stationCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.airportRepository.ValidatedIfAirportExist(stationCodes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Airport code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the services exist.
        /// </summary>
        /// <param name="serviceCodes">The service codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateIfServiceIsNationalFuel(IList<string> serviceCodes)
        {
            List<string> errors = new List<string>();
            List<string> codes = new List<string>() { "CM" };
            List<string> result = serviceCodes.Except(codes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Service code(s) not found. The codes must be for natiocal fuel.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the providers exist.
        /// </summary>
        /// <param name="providerNumbers">The service codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateProvider(IList<string> providerNumbers)
        {
            List<string> errors = new List<string>();
            List<string> result = this.providerRepository.ValidatedIfProviderExist(providerNumbers).ToList();
            if (result.Count > 0)
            {
                errors.Add("Provider number(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the services exist.
        /// </summary>
        /// <param name="costCenterNumbers">The service codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateCostCenter(IList<string> costCenterNumbers)
        {
            List<string> errors = new List<string>();
            List<string> result = this.costCenterRepository.ValidatedIfCostCenterExist(costCenterNumbers).ToList();
            if (result.Count > 0)
            {
                errors.Add("Cost center number(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the accounting account exist.
        /// </summary>
        /// <param name="accountingAccountNumbers">The accounting account numbers.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateAccountingAccount(IList<string> accountingAccountNumbers)
        {
            List<string> errors = new List<string>();
            List<string> result = this.accountingAccountRepository.ValidatedIfAccountingAccountExist(accountingAccountNumbers).ToList();
            if (result.Count > 0)
            {
                errors.Add("Accounting account number(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the liability account exist.
        /// </summary>
        /// <param name="liabilityAccountNumbers">The liability account numbers.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateLiabilityAccount(IList<string> liabilityAccountNumbers)
        {
            List<string> errors = new List<string>();
            List<string> result = this.liabilityAccountRepository.ValidatedIfLiabilityAccountExist(liabilityAccountNumbers).ToList();
            if (result.Count > 0)
            {
                errors.Add("Liability account number(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the currencies exist.
        /// </summary>
        /// <param name="currencies">The currencies.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateCurrency(IList<string> currencies)
        {
            List<string> errors = new List<string>();
            List<string> result = this.currencyRepository.ValidatedIfCurrencyExist(currencies).ToList();
            if (result.Count > 0)
            {
                errors.Add("Currency code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the taxes exist.
        /// </summary>
        /// <param name="taxes">The taxes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateTax(IList<string> taxes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.taxRepository.ValidatedIfTaxExist(taxes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Tax code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the fuel concept exist.
        /// </summary>
        /// <param name="fuelConcepts">The fuel concept.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateFuelConcept(IList<string> fuelConcepts)
        {
            List<string> errors = new List<string>();
            List<string> result = this.fuelConceptRepository.ValidatedIfFuelConceptExist(fuelConcepts).ToList();
            if (result.Count > 0)
            {
                errors.Add("Fuel Concept(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>         
        /// Validates the cost center and airline relationship.
        /// </summary>         
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>
        /// <c>true</c> if match <c>false</c> otherwise.
        /// </returns>
        private bool ValidateCostCenterAirlineRelationship(string airlineCode, string costCenterNumber)
        {
            string airlineCodeRelated = this.costCenterRepository.GetAirlineCodeRelated(costCenterNumber);
            return airlineCode == airlineCodeRelated;
        }

        /// <summary>         
        /// Loads the codes in the object national fuel contract.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> NationalFuelContractLoadCode(IList<NationalFuelContractDto> contracts)
        {
            IList<OperationType> operationTypeFullList = this.operationTypeRepository.GetAll();
            IList<FuelConcept> fuelConceptFullList = this.fuelConceptRepository.GetAll();
            IList<FuelConceptType> fuelConceptTypeFullList = this.fuelConceptTypeRepository.GetAll();
            IList<ChargeFactorType> chargeFactorTypeFullList = this.chargeFactorTypeRepository.GetAll();
            List<string> errors = new List<string>();

            foreach (var item in contracts)
            {
                OperationType operation = operationTypeFullList.FirstOrDefault(e => e.OperationName == item.OperationTypeName);
                if (operation != null)
                {
                    item.OperationTypeID = operation.OperationTypeId;
                }
                else
                {
                    errors.Add("Operation type not found: " + item.OperationTypeName);
                }
            }

            foreach (NationalFuelContractDto nationalFuelContractDto in contracts)
            {
                if (nationalFuelContractDto.NationalFuelContractConcept.Count() > 0)
                {
                    nationalFuelContractDto.NationalFuelContractConcept.ToList().ForEach(c =>
                    {
                        FuelConcept fuelConcept = fuelConceptFullList.FirstOrDefault(e => e.FuelConceptName == c.FuelConceptName);
                        if (fuelConcept != null)
                        {
                            c.FuelConceptID = fuelConcept.FuelConceptID;
                        }
                        else
                        {
                            errors.Add("Fuel Concept not found: " + c.FuelConceptName);
                        }

                        FuelConceptType fuelConceptType = fuelConceptTypeFullList.FirstOrDefault(e => e.FuelConceptTypeName == c.FuelConceptTypeName);
                        if (fuelConceptType != null)
                        {
                            c.FuelConceptTypeCode = fuelConceptType.FuelConceptTypeCode;
                        }
                        else
                        {
                            errors.Add("Fuel Concept Type not found: " + c.FuelConceptTypeName);
                        }

                        ChargeFactorType chargeFactorType = chargeFactorTypeFullList.FirstOrDefault(e => e.ChargeFactorTypeName == c.ChargeFactorTypeName);
                        if (chargeFactorType != null)
                        {
                            c.ChargeFactorTypeID = chargeFactorType.ChargeFactorTypeID;
                        }
                        else
                        {
                            errors.Add("Charge Factor Type not found: " + c.ChargeFactorTypeName);
                        }
                    });
                }
            }

            return errors;
        }

        /// <summary>
        /// Determines whether is national fuel contract duplicate.
        /// </summary>
        /// <param name="nationalFuelContractDto">National Fuel Contract.</param>
        /// <returns>
        ///   <c>true</c> if is duplicated otherwise <c>false</c>.
        /// </returns>
        private bool IsNationalFuelContractDuplicate(NationalFuelContractDto nationalFuelContractDto)
        {
            DateTime maxDate = this.nationalContractRepository.GetContractMaxDate(
                nationalFuelContractDto.AirlineCode,
                nationalFuelContractDto.StationCode,
                nationalFuelContractDto.ServiceCode,
                nationalFuelContractDto.ProviderNumberPrimary);
            return nationalFuelContractDto.EffectiveDate.Date > maxDate.Date ? false : true;
        }
    }
}
