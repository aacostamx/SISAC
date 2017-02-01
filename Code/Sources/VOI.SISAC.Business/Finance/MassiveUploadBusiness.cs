//------------------------------------------------------------------------
// <copyright file="MassiveUploadBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
    /// Massive Upload Operations
    /// </summary>
    public class MassiveUploadBusiness : IMassiveUploadBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

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
        /// The service type repository
        /// </summary>
        private readonly IServiceTypeRepository serviceTypeRepository;

        /// <summary>
        /// The service calculation type repository
        /// </summary>
        private readonly IServiceCalculationTypeRepository serviceCalculationTypeRepository;

        /// <summary>
        /// The airplane weight type repository
        /// </summary>
        private readonly IAirplaneWeightTypeRepository airplaneWeightTypeRepository;

        /// <summary>
        /// The airplane weight measure type repository
        /// </summary>
        private readonly IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository;

        /// <summary>
        /// The airport service contract repository
        /// </summary>
        private readonly IAirportServiceContractRepository airportServiceContractRepository;

        /// <summary>
        /// international Fuel Rate Repository
        /// </summary>
        private readonly IInternationalFuelRateRepository internationalfuelRateRepository;

        /// <summary>
        /// international Fuel Contract Repository
        /// </summary>
        private readonly IInternationalFuelContractRepository internationalFuelContractRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="MassiveUploadBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
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
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="serviceCalculationTypeRepository">The service calculation type repository.</param>
        /// <param name="airplaneWeightTypeRepository">The airplane weight type repository.</param>
        /// <param name="airplaneWeightMeasureTypeRepository">The airplane weight measure type repository.</param>
        /// <param name="airportServiceContractRepository">The airport service contract repository.</param>
        /// <param name="internationalFuelContractRepository">The international Fuel Contract repository.</param>
        /// <param name="internationalfuelRateRepository">The international fuel Rate repository.</param>
        public MassiveUploadBusiness(
            IUnitOfWork unitOfWork,
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
            IChargeFactorTypeRepository chargeFactorTypeRepository,
            IServiceTypeRepository serviceTypeRepository,
            IServiceCalculationTypeRepository serviceCalculationTypeRepository,
            IAirplaneWeightTypeRepository airplaneWeightTypeRepository,
            IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository,
            IAirportServiceContractRepository airportServiceContractRepository,
            IInternationalFuelContractRepository internationalFuelContractRepository,
            IInternationalFuelRateRepository internationalfuelRateRepository)
        {
            this.airportRepository = airportRepository;
            this.airlineRepository = airlineRepository;
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
            this.serviceTypeRepository = serviceTypeRepository;
            this.serviceCalculationTypeRepository = serviceCalculationTypeRepository;
            this.airplaneWeightTypeRepository = airplaneWeightTypeRepository;
            this.airplaneWeightMeasureTypeRepository = airplaneWeightMeasureTypeRepository;
            this.unitOfWork = unitOfWork;
            this.airportServiceContractRepository = airportServiceContractRepository;
            this.internationalFuelContractRepository = internationalFuelContractRepository;
            this.internationalfuelRateRepository = internationalfuelRateRepository;
        }

        /// <summary>
        /// Airports the service contract add range.
        /// </summary>
        /// <param name="contracts">Contracts to insert.</param>
        /// <returns>
        /// List of errors found, if there is not any error return NULL.
        /// </returns>
        public IList<string> AirportServiceContractAddRange(IList<AirportServiceContractDto> contracts)
        {
            // If there isn't any items
            if (contracts == null || contracts.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            // Validates that the Catalogs exist
            errors = this.ValidateCatalogs(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Adds the specific ID for the fields that only have description
            errors = this.AirportServiceContractLoadCode(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Validates that the cost center and the airline are related
            foreach (AirportServiceContractDto item in contracts)
            {
                item.Status = true;
                if (!this.ValidateCostCenterAirlineRelationship(item.AirlineCode, item.CostCenterNumber))
                {
                    errors.Add("The cost center" + item.CostCenterNumber + " does not match with the airline " + item.AirlineCode);
                }
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            // Validate dependency amoung Airplane's Weight and Airplane Weight Name, Aircraft Weight Uom, Airplane Weight Multiplier
            errors = ValidateDependencyAirplaneWeight(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Validate denpendency amoung the Taxes fields
            errors = ValidateCombosRelationship(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Validate dependency amoung MultiRateFlag and Rate and CurrencyCode
            errors = ValidateDependencyMultiRateFlag(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            //validate when Service Calculation Type == 4 "CÁLCULO ESPECIAL" applies StoredName
            errors = ValidateCalculationTypeStored(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Validate Max date records
            errors = this.IsServiceContractDuplicate(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Insertion in the DB
            try
            {
                IList<AirportServiceContract> contractEntities = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContract>>(contracts);
                this.airportServiceContractRepository.AddRange(contractEntities);
                this.unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }

            return errors;
        }

        /// <summary>
        /// Fuel contract add range.
        /// </summary>
        /// <param name="contracts">Contracts to insert.</param>
        /// <returns>
        /// List of errors found, if there is not any error return NULL.
        /// </returns>
        public IList<string> InternationalFuelContractAddRange(IList<InternationalFuelContractDto> contracts)
        {
            // If there isn't any items
            if (contracts == null || contracts.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            // Validates that the Catalogs exist
            errors = this.ValidateCatalogs(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Adds the specific ID for the fields that only have description
            errors = this.InternationalFuelContractLoadCode(contracts).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            //// Validate Max date records
            //// Note to Ira: Call function to validate max date in here

            return errors;
        }


        /// <summary>
        /// Fuel rates add range.
        /// </summary>
        /// <param name="rates">Rates to insert.</param>
        /// <returns>
        /// List of errors found, if there is not any error return NULL.
        /// </returns>
        public IList<string> InternationalFuelRatesAddRange(IList<InternationalFuelRateFileDto> rates)
        {
            // If there isn't any items
            if (rates == null || rates.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            // Validates that the Catalogs exist
            errors = this.ValidateCatalogs(rates).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            // Adds the specific ID for the fields that only have description
            //errors = this.InternationalFuelContractLoadCode(rates).ToList();
            //if (errors.Count > 0)
            //{
            //    return errors;
            //}

            #region validacion archivo y BD

            //Lista de tarifas que se ingresaran
            IList<InternationalFuelRateDto> internationalFuelRatesFoundInFileListDto = new List<InternationalFuelRateDto>();

            //Lista de Conceptos y Tarifas en BD
            IList<InternationalFuelContractConceptDto> conceptsListDB = new List<InternationalFuelContractConceptDto>();
            IList<InternationalFuelRateDto> ratesListDB = new List<InternationalFuelRateDto>();

            errors = ValidateFileInformation(rates).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            errors = GetConceptsRatesFromDB(rates, conceptsListDB, ratesListDB).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            errors = GetConceptID(rates, internationalFuelRatesFoundInFileListDto, conceptsListDB).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }

            errors = ValidateDBInformation(internationalFuelRatesFoundInFileListDto, ratesListDB).ToList();
            if (errors.Count > 0)
            {
                return errors;
            }
            #endregion

            try
            {
                foreach (InternationalFuelRateDto internationalFuelRateDto in internationalFuelRatesFoundInFileListDto)
                {
                    InternationalFuelRate internationalFuelRate = new InternationalFuelRate();
                    internationalFuelRate = Mapper.Map<InternationalFuelRate>(internationalFuelRateDto);
                    this.internationalfuelRateRepository.Add(internationalFuelRate);
                    this.unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return errors;
        }

        
        #region Private methods
        /// <summary>
        /// Validates the dependency airplane weight.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private static IList<string> ValidateDependencyAirplaneWeight(IList<AirportServiceContractDto> contracts)
        {
            List<string> errors = new List<string>();
            foreach (AirportServiceContractDto item in contracts)
            {
                if (item.AirplanetWeightFlag
                    && (string.IsNullOrWhiteSpace(item.AirplaneWeightCode)
                    || item.AirplaneWeightUnit == null
                    || item.AirplaneWeightMultiplier == null))
                {
                    errors.Add("If AirplanetWeightFlag is set to 1 then AirplaneWeightDescription, AirplaneWeightUnit and AirplaneWeightMultiplier must hava a value.");
                }
                else if (item.AirplanetWeightFlag == false
                    && (!string.IsNullOrWhiteSpace(item.AirplaneWeightCode)
                    || item.AirplaneWeightUnit != null
                    || item.AirplaneWeightMultiplier != null))
                {
                    errors.Add("If AirplanetWeightFlag is set to 0 then AirplaneWeightDescription, AirplaneWeightUnit and AirplaneWeightMultiplier must be empty");
                }
            }

            return errors;
        }
        
        /// <summary>
        /// Validates the dependency Multi Rate Flag.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private static IList<string> ValidateDependencyMultiRateFlag(IList<AirportServiceContractDto> contracts)
        {
            List<string> errors = new List<string>();
            foreach (AirportServiceContractDto item in contracts)
            {
                if (item.MultiRateFlag == false
                    && (string.IsNullOrWhiteSpace(item.CurrencyCode)
                    || item.Rate == null))
                {
                    errors.Add("If MultiRateFlag is set to 0 then CurrencyCode and Rate must hava a value.");
                }
                else if (item.MultiRateFlag
                    && (!string.IsNullOrWhiteSpace(item.CurrencyCode)
                    || item.Rate != null))
                {
                    errors.Add("If MultiRateFlag is set to 1 then CurrencyCode and Rate must be empty");
                }
            }

            return errors;
        }

        private static IList<string> ValidateCalculationTypeStored(IList<AirportServiceContractDto> contracts)
        {
            List<string> errors = new List<string>();
            foreach (AirportServiceContractDto item in contracts)
            {
                //lalo20170130
                //if (item.CalculationTypeId == 4
                //    && string.IsNullOrWhiteSpace(item.ProcedureName))
                //{
                //    errors.Add("If CalculationTypeId is set to 4 (CÁLCULO ESPECIAL) then ProcedureName must hava a value.");
                //}
                //else if (item.CalculationTypeId != 4
                //    && !string.IsNullOrWhiteSpace(item.ProcedureName))
                //{
                //    errors.Add("If CalculationTypeId is not set to 4 (CÁLCULO ESPECIAL) then ProcedureName must be empty.");
                //}
            }

            return errors;
        }

        /// <summary>
        /// Validates the combos relationship.
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns>List of errors.</returns>
        private static List<string> ValidateCombosRelationship(IList<AirportServiceContractDto> serviceContract)
        {
            List<string> errors = new List<string>();

            foreach (AirportServiceContractDto item in serviceContract)
            {
                if (item.AirportFeeCode != null && item.AirportFeeValue == null)
                {
                    errors.Add("When the field Airport Fee Description has a value then the field Airport Fee Value is required.");
                }

                if (item.AirportFeeValue != null && item.AirportFeeCode == null)
                {
                    errors.Add("When the field Airport Fee Value has a value then the field Airport Fee Description is required.");
                }

                if (item.LocalTaxCode != null && item.LocalTaxValue == null)
                {
                    errors.Add("When the field Local Tax Descripcion has a value then the field Local Tax Value is required.");
                }

                if (item.LocalTaxValue != null && item.LocalTaxCode == null)
                {
                    errors.Add("When the field Local Tax Value has a value then the field Local Tax Descripcion is required.");
                }

                if (item.StateTaxCode != null && item.StateTaxValue == null)
                {
                    errors.Add("When the field State Tax Descripcion has a value then the field State Tax Value is required.");
                }

                if (item.StateTaxValue != null && item.StateTaxCode == null)
                {
                    errors.Add("When the field State Tax Value has a value then the field State Tax Descripcion is required.");
                }

                if (item.FederalTaxCode != null && item.FederalTaxValue == null)
                {
                    errors.Add("When the field Federal Tax Descripcion has a value then the field Federal Tax Value is required.");
                }

                if (item.FederalTaxValue != null && item.FederalTaxCode == null)
                {
                    errors.Add("When the field Federal Tax Value has a value then the field Federal Tax Descripcion is required.");
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
        private IList<string> ValidateService(IList<string> serviceCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.serviceRepository.ValidatedIfServiceExist(serviceCodes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Service code(s) not found.");
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
        /// Validates if the fuel concept exist.
        /// </summary>
        /// <param name="fuel Concepts">The fuel concept.</param>
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
        /// <param name="acceptNull">if set to <c>true</c> ignores NULL values to validate.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateCurrency(IList<string> currencies, bool acceptNull)
        {
            List<string> errors = new List<string>();
            List<string> result = this.currencyRepository.ValidatedIfCurrencyExist(currencies).ToList();

            if (acceptNull && result.Contains(null))
            {
                ////List<string> nullValues = result.FindAll(item => item == null);
                result.RemoveAll(item => item == null);
            }

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
        /// <param name="acceptNull">if set to <c>true</c> ignores NULL values to validate.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private IList<string> ValidateTax(IList<string> taxes, bool acceptNull)
        {
            List<string> errors = new List<string>();
            List<string> result = this.taxRepository.ValidatedIfTaxExist(taxes).ToList();

            if (acceptNull && result.Contains(null))
            {
                ////List<string> nullValues = result.FindAll(item => item == null);
                result.RemoveAll(item => item == null);
            }

            if (result.Count > 0)
            {
                errors.Add("Tax code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates catalogs existence.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> ValidateCatalogs(IList<AirportServiceContractDto> contracts)
        {
            List<string> errors = new List<string>();

            // Validate Airline
            errors.AddRange(this.ValidateAirline(contracts.Select(c => c.AirlineCode).ToList()));

            // Validate Airport
            errors.AddRange(this.ValidateAirport(contracts.Select(c => c.StationCode).ToList()));

            // Validate Service
            errors.AddRange(this.ValidateService(contracts.Select(c => c.ServiceCode).ToList()));

            // Validate Cost Center
            errors.AddRange(this.ValidateCostCenter(contracts.Select(c => c.CostCenterNumber).ToList()));

            // Validate Provider
            errors.AddRange(this.ValidateProvider(contracts.Select(c => c.ProviderNumber).ToList()));

            // Validate Accounting Account
            errors.AddRange(this.ValidateAccountingAccount(contracts.Select(c => c.AccountingAccountNumber).ToList()));

            // Validate Liability Account
            errors.AddRange(this.ValidateLiabilityAccount(contracts.Select(c => c.LiabilityAccountNumber).ToList()));

            // Validate Currency
            errors.AddRange(this.ValidateCurrency(contracts.Select(c => c.CurrencyCode).ToList(), true));

            // Validate Federal Tax
            errors.AddRange(this.ValidateTax(contracts.Select(c => c.FederalTaxCode).ToList(), true));

            // Validate State Tax
            errors.AddRange(this.ValidateTax(contracts.Select(c => c.StateTaxCode).ToList(), true));

            // Validate Local Tax
            errors.AddRange(this.ValidateTax(contracts.Select(c => c.LocalTaxCode).ToList(), true));

            // Validate Airport Tax
            errors.AddRange(this.ValidateTax(contracts.Select(c => c.AirportFeeCode).ToList(), true));

            return errors;
        }

        /// <summary>
        /// Validates catalogs existence.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> ValidateCatalogs(IList<InternationalFuelContractDto> contracts)
        {
            List<string> errors = new List<string>();

            // Validate Airline
            errors.AddRange(this.ValidateAirline(contracts.Select(c => c.AirlineCode).ToList()));

            // Validate Airport
            errors.AddRange(this.ValidateAirport(contracts.Select(c => c.StationCode).ToList()));

            // Validate Service
            errors.AddRange(this.ValidateService(contracts.Select(c => c.ServiceCode).ToList()));

            // Validate Provider
            errors.AddRange(this.ValidateProvider(contracts.Select(c => c.ProviderNumberPrimary).ToList()));

            ////var boleanos = contracts.Select(c => c.AircraftRegistCCFlag).ToList();

            //// Validate Cost Center
            ////errors.AddRange(this.ValidateCostCenter(contracts.Select(c => c.CCNumber).ToList()));

            // Validate Accounting Account
            errors.AddRange(this.ValidateAccountingAccount(contracts.Select(c => c.AccountingAccountNumber).ToList()));

            // Validate Liability Account
            errors.AddRange(this.ValidateLiabilityAccount(contracts.Select(c => c.LiabilityAccountNumber).ToList()));

            // Validate Currency
            errors.AddRange(this.ValidateCurrency(contracts.Select(c => c.CurrencyCode).ToList(), false));

            // concepts
            foreach (InternationalFuelContractDto internationalFuelContractDto in contracts)
            {
                if (internationalFuelContractDto.InternationalFuelContractConcepts.Count() > 0)
                {
                    IList<string> lista = internationalFuelContractDto.InternationalFuelContractConcepts.Select(c => c.ProviderNumber).ToList();
                    errors.AddRange(this.ValidateProvider(lista));
                }
            }

            return errors;
        }

        /// <summary>
        /// Validates catalogs existence.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <returns>List of errors.</returns>
        private IList<string> ValidateCatalogs(IList<InternationalFuelRateFileDto> rates)
        {
            List<string> errors = new List<string>();

            // Validate Airline
            errors.AddRange(this.ValidateAirline(rates.Select(c => c.AirlineCode).ToList()));

            // Validate Airport
            errors.AddRange(this.ValidateAirport(rates.Select(c => c.StationCode).ToList()));

            // Validate Service
            errors.AddRange(this.ValidateService(rates.Select(c => c.ServiceCode).ToList()));

            // Validate Provider
            errors.AddRange(this.ValidateProvider(rates.Select(c => c.ProviderNumberPrimary).ToList()));

            //// Validate Fuel Concept Name
            errors.AddRange(this.ValidateFuelConcept(rates.Select(c => c.FuelConceptName).ToList()));

            // Validate Rate Start Date
            ////errors.AddRange(this.ValidateAccountingAccount(rates.Select(c => c.AccountingAccountNumber).ToList()));

            ////// Validate Rate End Date
            ////errors.AddRange(this.ValidateLiabilityAccount(rates.Select(c => c.LiabilityAccountNumber).ToList()));

            

            return errors;
        }

        /// <summary>
        /// Airports the service contract load code.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> AirportServiceContractLoadCode(IList<AirportServiceContractDto> contracts)
        {
            //IList<Procedure> procedureFullList = this.procedureRepository.GetAll();
            IList<OperationType> operationTypeFullList = this.operationTypeRepository.GetAll();
            IList<ServiceType> serviceTypeFullList = this.serviceTypeRepository.GetAll();
            IList<ServiceCalculationType> serviceCalculationTypeFullList = this.serviceCalculationTypeRepository.GetAll();
            IList<AirplaneWeightType> airplaneWeightTypeFullList = this.airplaneWeightTypeRepository.GetAll();
            IList<AirplaneWeightMeasureType> airplaneWeightMeasureTypeFullList = this.airplaneWeightMeasureTypeRepository.GetAll();
            List<string> errors = new List<string>();

            foreach (AirportServiceContractDto item in contracts)
            {
                //lalo20170130
                //Procedure procedure = procedureFullList.FirstOrDefault(e => e.ProcedureName == item.OperationType.ProcedureName);
                //if (procedure != null)
                //{
                //    item.ProcedureCode = procedure.ProcedureCode;
                //}
                //else
                //{
                //    errors.Add("Procedure not found: " + item.OperationType.ProcedureName + ".");
                //}

                OperationType operation = operationTypeFullList.FirstOrDefault(e => e.OperationName == item.OperationType.OperationName);
                if (operation != null)
                {
                    item.OperationTypeId = operation.OperationTypeId;
                }
                else
                {
                    errors.Add("Operation type not found: " + item.OperationType.OperationName + ".");
                }

                ServiceType service = serviceTypeFullList.FirstOrDefault(e => e.ServiceTypeName == item.ServiceType.ServiceTypeName);
                if (service != null)
                {
                    item.ServiceTypeCode = service.ServiceTypeCode;
                }
                else
                {
                    errors.Add("Service type not found: " + item.ServiceType.ServiceTypeName + ".");
                }

                ServiceCalculationType serviceCalculation = serviceCalculationTypeFullList.FirstOrDefault(e => e.CalculationTypeName == item.ServiceCalculationType.CalculationTypeName);
                if (serviceCalculation != null)
                {
                    item.CalculationTypeId = serviceCalculation.CalculationTypeId;
                }
                else
                {
                    errors.Add("Calculation type not found: " + item.ServiceCalculationType.CalculationTypeName + ".");
                }

                AirplaneWeightMeasureType airplaneMeasure = airplaneWeightMeasureTypeFullList.FirstOrDefault(e => e.AirplaneWeightMeasureName == item.AirplaneWeightMeasureType.AirplaneWeightMeasureName);
                if (item.AirplanetWeightFlag)
                {
                    if (airplaneMeasure != null)
                    {
                        item.AirplaneWeightUnit = airplaneMeasure.AirplaneWeightMeasureId;
                    }
                    else
                    {
                        errors.Add("Unit of measure not found: " + item.AirplaneWeightMeasureType.AirplaneWeightMeasureName + ".");
                    }
                }

                AirplaneWeightType airplaneType = airplaneWeightTypeFullList.FirstOrDefault(e => e.AirplaneWeightName == item.AirplaneWeightType.AirplaneWeightName);
                if (item.AirplanetWeightFlag)
                {
                    if (airplaneType != null)
                    {
                        item.AirplaneWeightCode = airplaneType.AirplaneWeightCode;
                    }
                    else
                    {
                        errors.Add("Weight type not found: " + item.AirplaneWeightType.AirplaneWeightName + ".");
                    }
                }

                // set null to the non required objects
                item.ServiceCalculationType = null;
                item.ServiceType = null;
                item.OperationType = null;
                item.AirplaneWeightMeasureType = null;
                item.AirplaneWeightType = null;
            }

            return errors;
        }

        /// <summary>         
        /// Internationals the fuel contract load code.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of errors.</returns>
        private IList<string> InternationalFuelContractLoadCode(IList<InternationalFuelContractDto> contracts)
        {
            IList<OperationType> operationTypeFullList = this.operationTypeRepository.GetAll();
            IList<FuelConcept> fuelConceptFullList = this.fuelConceptRepository.GetAll();
            IList<FuelConceptType> fuelConceptTypeFullList = this.fuelConceptTypeRepository.GetAll();
            IList<ChargeFactorType> chargeFactorTypeFullList = this.chargeFactorTypeRepository.GetAll();
            List<string> errors = new List<string>();

            foreach (var item in contracts)
            {
                OperationType operation = operationTypeFullList.FirstOrDefault(e => e.OperationName == item.OperationType.OperationName);
                if (operation != null)
                {
                    item.OperationTypeID = operation.OperationTypeId;
                }
                else
                {
                    errors.Add("Operation type not found: " + item.OperationType.OperationName);
                }
            }

            foreach (InternationalFuelContractDto internationalFuelContractDto in contracts)
            {
                if (internationalFuelContractDto.InternationalFuelContractConcepts.Count() > 0)
                {
                    internationalFuelContractDto.InternationalFuelContractConcepts.ToList().ForEach(c =>
                    {
                        FuelConcept fuelConcept = fuelConceptFullList.FirstOrDefault(e => e.FuelConceptName == c.FuelConcept.FuelConceptName);
                        if (fuelConcept != null)
                        {
                            c.FuelConceptID = fuelConcept.FuelConceptID;
                        }
                        else
                        {
                            errors.Add("Fuel Concept not found: " + c.FuelConcept.FuelConceptName);
                        }

                        FuelConceptType fuelConceptType = fuelConceptTypeFullList.FirstOrDefault(e => e.FuelConceptTypeName == c.FuelConceptType.FuelConceptTypeName);
                        if (fuelConceptType != null)
                        {
                            c.FuelConceptTypeCode = fuelConceptType.FuelConceptTypeCode;
                        }
                        else
                        {
                            errors.Add("Fuel Concept Type not found: " + c.FuelConceptType.FuelConceptTypeName);
                        }

                        ChargeFactorType chargeFactorType = chargeFactorTypeFullList.FirstOrDefault(e => e.ChargeFactorTypeName == c.ChargeFactorType.ChargeFactorTypeName);
                        if (chargeFactorType != null)
                        {
                            c.ChargeFactorTypeID = chargeFactorType.ChargeFactorTypeID;
                        }
                        else
                        {
                            errors.Add("Charge Factor Type not found: " + c.ChargeFactorType.ChargeFactorTypeName);
                        }

                        ////c.FuelConceptID = fuelConceptFullList.FirstOrDefault(e => e.FuelConceptName == c.FuelConcept.FuelConceptName).FuelConceptID;
                        ////c.FuelConceptTypeCode = fuelConceptTypeFullList.FirstOrDefault(e => e.FuelConceptTypeName == c.FuelConceptType.FuelConceptTypeName).FuelConceptTypeCode;
                        ////c.ChargeFactorTypeID = chargeFactorTypeFullList.FirstOrDefault(e => e.ChargeFactorTypeName == c.ChargeFactorType.ChargeFactorTypeName).ChargeFactorTypeID;

                        // nullear los objetos que no requeridos
                        c.FuelConcept = null;
                        c.FuelConceptType = null;
                        c.ChargeFactorType = null;
                        c.InternationalFuelContract = null;
                        c.Provider = null;
                        c.InternationalFuelRate = null;
                    });

                    // nullear los objetos que no requeridos
                    internationalFuelContractDto.OperationType = null;
                }
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
        /// Determines whether [is service contract duplicate] [the specified service contract].
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns>List of errors.</returns>
        private List<string> IsServiceContractDuplicate(IList<AirportServiceContractDto> serviceContract)
        {
            List<string> errors = new List<string>();
            foreach (AirportServiceContractDto item in serviceContract)
            {
                DateTime maxDate = this.airportServiceContractRepository.GetAllContractsByMaxDate(
                    item.AirlineCode,
                    item.StationCode,
                    item.ServiceCode,
                    item.ProviderNumber,
                    item.CostCenterNumber);

                if (maxDate.Date >= item.EffectiveDate.Date)
                {
                    errors.Add("The record, Airline: " + item.AirlineCode + ", Station: " + item.StationCode + ", Service: "
                                + item.ServiceCode + ", Provider: " + item.ProviderNumber + ", Cost Center: " + item.CostCenterNumber
                                + ", with the current Effective Date " + item.EffectiveDate.Date.ToShortDateString()
                                + " must be greater than Maximun Date: " + maxDate.Date.ToShortDateString() + ".");
                }
            }

            return errors;
        }

        /// <summary>
        /// Finds the InternationalFuelContract by identifier.
        /// </summary>
        /// <param name="entity">The InternationalFuelContract Entity.</param>
        /// <returns>
        /// Entity InternationalFuelContract.
        /// </returns>
        private InternationalFuelContractDto FindInternationalFuelContractById(InternationalFuelContractDto entity)
        {
            try
            {
                InternationalFuelContract internationalFuelContract = this.internationalFuelContractRepository.FindById(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity));
                InternationalFuelContractDto internationalFuelContractDto = new InternationalFuelContractDto();
                internationalFuelContractDto = Mapper.Map<InternationalFuelContract, InternationalFuelContractDto>(internationalFuelContract);

                return internationalFuelContractDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        private static IList<string> ValidateDBInformation(IList<InternationalFuelRateDto> internationalFuelRatesFoundInFileListDto, IList<InternationalFuelRateDto> ratesListDB)
        {

            List<string> errors = new List<string>();

            //Verifica duplicidad de tarifas en cuanto a fechas en BD
            foreach (var item in internationalFuelRatesFoundInFileListDto)
            {
                //rates en DB
                var ratesDB = ratesListDB.Where(c => c.InternationalFuelContractConceptID == item.InternationalFuelContractConceptID).ToList();

                //Si encontro al menos una tarifa en BD
                if (ratesDB.Count > 0)
                {
                    //verifica el registro nuevo en "fecha de inicio" sea mayor extrictamente que el ultimo encontrado de ese "concepto" en "fecha de fin". 
                    //Sino se cumple lo anterior se notifica error
                    DateTime dateMax = ratesDB.Max(c => c.RateEndDate);
                    if (dateMax > item.RateStartDate)
                    {
                        // InternationalFuelRateID trae LineNumber
                        errors.Add("Start Date: " + item.RateStartDate.ToShortDateString() + " must be greatter than " + dateMax.ToShortDateString() + ", Line: " + item.InternationalFuelRateID);
                        //Nuevos Registros
                        item.InternationalFuelRateID = 0;
                    }
                }
            }

            return errors;
        }

        private static IList<string> GetConceptID(IList<InternationalFuelRateFileDto> rates, IList<InternationalFuelRateDto> internationalFuelRatesFoundInFileListDto, IList<InternationalFuelContractConceptDto> conceptsListDB)
        {
            List<string> errors = new List<string>();

            //Obtener informacion de Id de concepto, FuelConceptName y ProviderNumber
            var conceptsInformationDB = (from sri in conceptsListDB
                                         group sri by new { sri.EffectiveDate, sri.AirlineCode, sri.StationCode, sri.ServiceCode, sri.ProviderNumberPrimary, sri.InternationalFuelContractConceptID, sri.FuelConcept.FuelConceptName, sri.ProviderNumber } into f
                                         select new { EffectiveDate = f.Key.EffectiveDate, AirlineCode = f.Key.AirlineCode, StationCode = f.Key.StationCode, ServiceCode = f.Key.ServiceCode, ProviderNumberPrimary = f.Key.ProviderNumberPrimary, InternationalFuelContractConceptID = f.Key.InternationalFuelContractConceptID, FuelConceptName = f.Key.FuelConceptName, ProviderNumber = f.Key.ProviderNumber }).ToList();

            foreach (var item in rates)
            {
                //Buscamos que cada registro de ratesList se encuentre en la DB
                var idConceptFound = conceptsInformationDB.FirstOrDefault(e => e.EffectiveDate == item.EffectiveDate
                                                                        && e.AirlineCode == item.AirlineCode
                                                                        && e.StationCode == item.StationCode
                                                                        && e.ServiceCode == item.ServiceCode
                                                                        && e.ProviderNumberPrimary == item.ProviderNumberPrimary
                                                                        && e.FuelConceptName == item.FuelConceptName //FuelConceptName
                                                                        && e.ProviderNumber == item.ProviderNumber);

                //Registar sino encontro en BD 
                if (idConceptFound == null)
                {
                    errors.Add("There is no Concept for the Rate with Fuel Concept " + item.FuelConceptName + " and provider " + item.ProviderNumber +
                                    " for the contract: " + item.EffectiveDate.ToShortDateString() + " , Airline Code: " + item.AirlineCode + " , Station Code: " + item.StationCode +
                                    " , Service Code: " + item.ServiceCode + " , Provider Number: " + item.ProviderNumberPrimary + ", Line: " + item.LineNumber);
                }
                else
                {
                    //Si lo encontro se almacena para ser guardado en addrange
                    internationalFuelRatesFoundInFileListDto.Add(new InternationalFuelRateDto
                    {
                        InternationalFuelContractConceptID = idConceptFound.InternationalFuelContractConceptID,
                        RateStartDate = item.RateStartDate.Date,
                        RateEndDate = item.RateEndDate.Date,
                        Rate = item.Rate,
                        InternationalFuelRateID = item.LineNumber//Guardamos LineNumber para informar posteriormente
                    });
                }
            }

            return errors;
        }

        private IList<string> ValidateFileInformation(IList<InternationalFuelRateFileDto> rates)
        {
            List<string> errors = new List<string>();

            //Verifica duplicidad de tarifas en cuanto a fechas en mismo archivo
            List<string> datesString = new List<string>();
            int numberOfDays = 0;
            string conceptKey="";    
            //Resources
            string startDateText = "Start Date";
            string endDateText = "End Date";
            string effectiveDateText = "Effective Date";

            foreach (var item in rates)
            {
                //numero de dias por rate
                numberOfDays = item.RateEndDate.Subtract(item.RateStartDate).Days + 1;                
                //key
                conceptKey = '&' + item.EffectiveDate.ToShortDateString() 
                    + '&' + item.AirlineCode + '&' + item.StationCode 
                    + '&' + item.ServiceCode + '&' + item.ProviderNumberPrimary 
                    + '&' + item.FuelConceptName + '&' + item.ProviderNumber;

                //acumulado de fechas existentes en el intervalo de cada rate
                if (numberOfDays > 0)
                {
                    datesString.AddRange(Enumerable.Range(0, numberOfDays)
                                          .Select(i => item.RateStartDate.AddDays(i).ToShortDateString() + conceptKey + '|' + item.LineNumber).ToList());
                }

                //verificar que fecha inicio sea menor a fecha fin. Sino marca error
                if (item.RateStartDate > item.RateEndDate)
                {
                    errors.Add(endDateText + ": " + item.RateEndDate.ToShortDateString() + " must be greater than " + startDateText + ": " + item.RateStartDate.ToShortDateString() + ", Line: " + item.LineNumber);
                }

                //verificar que fecha inicio sea menor a fecha de efectividad de contrato. Sino marca error
                if (item.RateStartDate < item.EffectiveDate)
                {
                    errors.Add(startDateText + ": " + item.RateStartDate.ToShortDateString() + " must be greater than " + effectiveDateText + ": " + item.EffectiveDate.ToShortDateString() + ", Line: " + item.LineNumber);
                }
            }

            //Ver fechas que empalman
            foreach (var item in datesString)
            {
                var dateSplit = item.Split('|');
                var datesListFound = datesString.Where(c => c.Contains(dateSplit[0])).ToList();
                if (datesListFound.Count > 1)
                {
                    errors.Add("Match Date: " + dateSplit[0].Substring(0, 10) + ", Line: " + dateSplit[1]);                                    
                }
            }

            return errors;
        }

        /// <summary>
        /// Gets the concepts rates from database.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <param name="conceptsListDB">The concepts list database.</param>
        /// <param name="ratesListDB">The rates list database.</param>
        /// <returns>List of errors found.</returns>
        private IList<string> GetConceptsRatesFromDB(
            IList<InternationalFuelRateFileDto> rates,
            IList<InternationalFuelContractConceptDto> conceptsListDB,
            IList<InternationalFuelRateDto> ratesListDB)
        {
            List<string> errors = new List<string>();

            // Agrupa los Contratos del archivo de carga masivo
            var contractDistinctFile = (from ssi 
                                        in rates
                                        group ssi by new 
                                        { 
                                            ssi.EffectiveDate, 
                                            ssi.AirlineCode, 
                                            ssi.StationCode, 
                                            ssi.ServiceCode, 
                                            ssi.ProviderNumberPrimary 
                                        } into g
                                        select new 
                                        { 
                                            EffectiveDate = g.Key.EffectiveDate,
                                            AirlineCode = g.Key.AirlineCode,
                                            StationCode = g.Key.StationCode,
                                            ServiceCode = g.Key.ServiceCode, 
                                            ProviderNumberPrimary = g.Key.ProviderNumberPrimary, 
                                            Count = g.Count() 
                                        }).ToList();

            // Verificar que exista el contrato por cada uno de los distintos que hay en el Archivo
            foreach (var item in contractDistinctFile)
            {
                // Busca contrato en BD
                var contractDB = this.FindInternationalFuelContractById(
                    new InternationalFuelContractDto
                    {
                        EffectiveDate = item.EffectiveDate,
                        AirlineCode = item.AirlineCode,
                        StationCode = item.StationCode,
                        ServiceCode = item.ServiceCode,
                        ProviderNumberPrimary = item.ProviderNumberPrimary
                    });

                if (contractDB == null)
                {
                    errors.Add("There are no contract for the following Effective Date: " + 
                        item.EffectiveDate.ToShortDateString() + 
                        ", Airline Code: " + 
                        item.AirlineCode +
                        ", Station Code: " + 
                        item.StationCode + 
                        ", Service Code " + 
                        item.ServiceCode +
                        ", Provider Number: " + 
                        item.ProviderNumberPrimary);
                }
                else
                {
                    // Si hay contratos hace lista de conceptsListDB (conceptos) y ratesListDB (tarifas)
                    foreach (var concept in contractDB.InternationalFuelContractConcepts)
                    {
                        conceptsListDB.Add(concept);
                        foreach (var rateDB in concept.InternationalFuelRate)
                        {
                            ratesListDB.Add(rateDB);
                        }
                    }                    
                }
            }

            return errors;
        }        
        #endregion
    }
}
