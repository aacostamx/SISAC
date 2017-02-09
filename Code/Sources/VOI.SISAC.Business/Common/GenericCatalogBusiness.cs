//------------------------------------------------------------------------
// <copyright file="GenericCatalogBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Common
{
    using Dal.Repository.Process;
    using Entities.Process;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Security;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Generic Catalog Business
    /// </summary>
    public class GenericCatalogBusiness : IGenericCatalogBusiness
    {
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
        /// The provider repository
        /// </summary>
        private readonly IProviderRepository providerRepository;

        /// <summary>
        /// The cost center repository
        /// </summary>
        private readonly ICostCenterRepository costCenterRepository;

        /// <summary>
        /// The accounting account repository
        /// </summary>
        private readonly IAccountingAccountRepository accountingAccountRepository;

        /// <summary>
        /// The liability account repository
        /// </summary>
        private readonly ILiabilityAccountRepository liabilityAccountRepository;

        /// <summary>
        /// The tax repository
        /// </summary>
        private readonly ITaxRepository taxRepository;

        /// <summary>
        /// The currency repository
        /// </summary>
        private readonly ICurrencyRepository currencyRepository;

        /// <summary>
        /// The airplane weight type repository
        /// </summary>
        private readonly IAirplaneWeightTypeRepository airplaneWeightTypeRepository;

        /// <summary>
        /// The airplane weight measure type repository
        /// </summary>
        private readonly IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository;

        /// <summary>
        /// The operation type repository
        /// </summary>
        private readonly IOperationTypeRepository operationTypeRepository;

        /// <summary>
        /// The service calculation type repository
        /// </summary>
        private readonly IServiceCalculationTypeRepository serviceCalculationTypeRepository;

        /// <summary>
        /// The service type repository
        /// </summary>
        private readonly IServiceTypeRepository serviceTypeRepository;

        /// <summary>
        /// The IFuelConceptRepository type repository
        /// </summary>
        private readonly IFuelConceptRepository fuelConceptRepository;

        /// <summary>
        /// The IChargeFactorTypeRepository type repository
        /// </summary>
        private readonly IChargeFactorTypeRepository chargeFactorTypeRepository;

        /// <summary>
        /// The IFuelConcept type repository
        /// </summary>
        private readonly IFuelConceptTypeRepository fuelConceptTypeRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// The Crew Repository
        /// </summary>
        private readonly ICrewRepository crewRepository;

        /// <summary>
        /// The Crew TypeRepository
        /// </summary>
        private readonly ICrewTypeRepository crewTypeRepository;

        /// <summary>
        /// The menu repository
        /// </summary>
        private readonly IMenuRepository menuRepository;

        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IPermissionRepository permissionRepository;

        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IDepartmentRepository departmentRepository;

        /// <summary>
        /// The profile repository
        /// </summary>
        private readonly IProfileRepository profileRepository;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// The user business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The Jet Fuel Business
        /// </summary>
        private readonly IJetFuelProcessRepository jetFuelProcessRepository;

        /// <summary>
        /// The NTL jet fuel process repository
        /// </summary>
        private readonly INationalJetFuelProcessRepository ntlJetFuelProcessRepository;

        /// <summary>
        /// The delay repository
        /// </summary>
        private readonly IDelayRepository delayRepository;

        private readonly IToleranceTypeRepository toleranceTypeRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCatalogBusiness"/> class.
        /// </summary>
        /// <param name="airlineRepository">The airline repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="providerRepository">The provider repository.</param>
        /// <param name="costCenterRepository">The cost center repository.</param>
        /// <param name="accountingAccountRepository">The accounting account repository.</param>
        /// <param name="liabilityAccountRepository">The liability account repository.</param>
        /// <param name="taxRepository">The tax repository.</param>
        /// <param name="currencyRepository">The currency repository.</param>
        /// <param name="airplaneWeightTypeRepository">The airplane weight type repository.</param>
        /// <param name="airplaneWeightMeasureTypeRepository">The airplane weight measure type repository.</param>
        /// <param name="operationTypeRepository">The operation type repository.</param>
        /// <param name="serviceCalculationTypeRepository">The service calculation type repository.</param>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="fuelConceptRepository">The fuel concept repository.</param>
        /// <param name="fuelConceptTypeRepository">The fuel concept type repository.</param>
        /// <param name="chargeFactorTypeRepository">The charge factor type repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="crewRepository">The crew repository.</param>
        /// <param name="crewTypeRepository">The crew type repository.</param>
        /// <param name="menuRepository">The menu repository.</param>
        /// <param name="permissionRepository">The permission repository.</param>
        /// <param name="departmentRepository">The department repository.</param>
        /// <param name="profileRepository">The profile repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userBusiness">The user business.</param>
        /// <param name="jetFuelProcessRepository">The jet fuel process repository.</param>
        /// <param name="ntlJetFuelProcessRepository">The NTL jet fuel process repository.</param>
        /// <param name="delayRepository">The delay repository.</param>
        /// <param name="toleranceTypeRepository">The tolerance type repository.</param>
        public GenericCatalogBusiness(
            IAirlineRepository airlineRepository,
            IAirportRepository airportRepository,
            IServiceRepository serviceRepository,
            IProviderRepository providerRepository,
            ICostCenterRepository costCenterRepository,
            IAccountingAccountRepository accountingAccountRepository,
            ILiabilityAccountRepository liabilityAccountRepository,
            ITaxRepository taxRepository,
            ICurrencyRepository currencyRepository,
            IAirplaneWeightTypeRepository airplaneWeightTypeRepository,
            IAirplaneWeightMeasureTypeRepository airplaneWeightMeasureTypeRepository,
            IOperationTypeRepository operationTypeRepository,
            IServiceCalculationTypeRepository serviceCalculationTypeRepository,
            IServiceTypeRepository serviceTypeRepository,
            IFuelConceptRepository fuelConceptRepository,
            IFuelConceptTypeRepository fuelConceptTypeRepository,
            IChargeFactorTypeRepository chargeFactorTypeRepository,
            IUserRepository userRepository,
            ICrewRepository crewRepository,
            ICrewTypeRepository crewTypeRepository,
            IMenuRepository menuRepository,
            IPermissionRepository permissionRepository,
            IDepartmentRepository departmentRepository,
            IProfileRepository profileRepository,
            IRoleRepository roleRepository,
            IUserBusiness userBusiness,
            IJetFuelProcessRepository jetFuelProcessRepository,
            INationalJetFuelProcessRepository ntlJetFuelProcessRepository,
            IDelayRepository delayRepository,
            IToleranceTypeRepository toleranceTypeRepository)
        {
            this.airlineRepository = airlineRepository;
            this.airportRepository = airportRepository;
            this.serviceRepository = serviceRepository;
            this.providerRepository = providerRepository;
            this.costCenterRepository = costCenterRepository;
            this.accountingAccountRepository = accountingAccountRepository;
            this.liabilityAccountRepository = liabilityAccountRepository;
            this.taxRepository = taxRepository;
            this.currencyRepository = currencyRepository;
            this.airplaneWeightMeasureTypeRepository = airplaneWeightMeasureTypeRepository;
            this.airplaneWeightTypeRepository = airplaneWeightTypeRepository;
            this.operationTypeRepository = operationTypeRepository;
            this.serviceCalculationTypeRepository = serviceCalculationTypeRepository;
            this.serviceTypeRepository = serviceTypeRepository;
            this.fuelConceptRepository = fuelConceptRepository;
            this.fuelConceptTypeRepository = fuelConceptTypeRepository;
            this.chargeFactorTypeRepository = chargeFactorTypeRepository;
            this.crewRepository = crewRepository;
            this.crewTypeRepository = crewTypeRepository;
            this.userRepository = userRepository;
            this.menuRepository = menuRepository;
            this.permissionRepository = permissionRepository;
            this.departmentRepository = departmentRepository;
            this.profileRepository = profileRepository;
            this.roleRepository = roleRepository;
            this.userBusiness = userBusiness;
            this.jetFuelProcessRepository = jetFuelProcessRepository;
            this.ntlJetFuelProcessRepository = ntlJetFuelProcessRepository;
            this.delayRepository = delayRepository;
            this.toleranceTypeRepository = toleranceTypeRepository;
        }

        #region IGenericCatalogBusiness Members

        /// <summary>
        /// Gets the airline catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for airlines.
        /// </returns>
        public IList<GenericCatalogDto> GetAirlineCatalog()
        {
            try
            {
                var airlinesDto = this.airlineRepository.GetActiveAirline();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in airlinesDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.AirlineCode,
                        Description = item.AirlineName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the airports catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Airports.
        /// </returns>
        public IList<GenericCatalogDto> GetAirportsCatalog()
        {
            try
            {
                var airportDto = this.airportRepository.GetActivesAirports();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in airportDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.StationCode,
                        Description = item.StationName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the national airports catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetNationalAirportsCatalog()
        {
            try
            {
                var airportDto = this.airportRepository.GetActivesAirports().Where(c => c.CountryCode.Equals("MEX"));
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in airportDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.StationCode,
                        Description = item.StationName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the service catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Service.
        /// </returns>
        public IList<GenericCatalogDto> GetServiceCatalog()
        {
            try
            {
                var serviceDto = this.serviceRepository.GetServices();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in serviceDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ServiceCode,
                        Description = item.ServiceName
                    });
                }

                return generic.OrderBy(c => c.DescriptionAndCode).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the provider catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Provider.
        /// </returns>
        public IList<GenericCatalogDto> GetProviderCatalog()
        {
            try
            {
                var providerDto = this.providerRepository.GetProviders();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in providerDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ProviderNumber,
                        Description = item.ProviderName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList(); ;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the cost center catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for CostCenter.
        /// </returns>
        public IList<GenericCatalogDto> GetCostCenterCatalog()
        {
            try
            {
                var costCenterDto = this.costCenterRepository.GetActiveCostCenter();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in costCenterDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.CCNumber,
                        Description = item.CCName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the cost center catalog by Airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="selectItem">The select item.</param>
        /// <returns>
        /// Generic Catalog with information for CostCenter.
        /// </returns>
        public IList<GenericCatalogDto> GetCostCenterCatalog(string airlineCode, string selectItem)
        {
            try
            {
                var costCenterDto = this.costCenterRepository.GetActiveCostCenter(airlineCode);
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                generic.Add(new GenericCatalogDto() { Id = string.Empty, Description = selectItem });
                foreach (var item in costCenterDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.CCNumber,
                        Description = item.CCNumber + " - " + item.CCName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the accounting accounts catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Accounting Account.
        /// </returns>
        public IList<GenericCatalogDto> GetAccountingAccountsCatalog()
        {
            try
            {
                var accountingAccountsDto = this.accountingAccountRepository.GetActivesAccountingAccounts();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in accountingAccountsDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.AccountingAccountNumber,
                        Description = item.AccountingAccountName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the liability accounts catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Liability Accounts.
        /// </returns>
        public IList<GenericCatalogDto> GetLiabilityAccountsCatalog()
        {
            try
            {
                var liabilityAccountsDto = this.liabilityAccountRepository.GetActiveLiabilityAccounts();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in liabilityAccountsDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.LiabilityAccountNumber,
                        Description = item.LiabilityAccountName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the taxes catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Taxes.
        /// </returns>
        public IList<GenericCatalogDto> GetTaxesCatalog()
        {
            try
            {
                var taxesDto = this.taxRepository.GetActivesTaxes();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in taxesDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.TaxCode,
                        Description = item.TaxName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList(); 
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the currency catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Currency.
        /// </returns>
        public IList<GenericCatalogDto> GetCurrencyCatalog()
        {
            try
            {
                var currencyDto = this.currencyRepository.GetActiveCurrency();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in currencyDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.CurrencyCode,
                        Description = item.CurrencyName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList(); 
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the airplane weight measure type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for AirplaneWeightMeasureType.
        /// </returns>
        public IList<GenericCatalogDto> GetAirplaneWeightMeasureTypeCatalog()
        {
            try
            {
                var airplaneMeasureTypeDto = this.airplaneWeightMeasureTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in airplaneMeasureTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.AirplaneWeightMeasureId.ToString(),
                        Description = item.AirplaneWeightMeasureName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the airplane weight type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for AirplaneWeightType.
        /// </returns>
        public IList<GenericCatalogDto> GetAirplaneWeightTypeCatalog()
        {
            try
            {
                var airplaneTypeDto = this.airplaneWeightTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in airplaneTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.AirplaneWeightCode,
                        Description = item.AirplaneWeightName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the operation type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for OperationType.
        /// </returns>
        public IList<GenericCatalogDto> GetOperationTypeCatalog()
        {
            try
            {
                var operationTypeDto = this.operationTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in operationTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.OperationTypeId.ToString(),
                        Description = item.OperationName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the service calculation type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Service Calculation Type.
        /// </returns>
        public IList<GenericCatalogDto> GetServiceCalculationTypeCatalog()
        {
            try
            {
                var serviceCalculationTypeDto = this.serviceCalculationTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in serviceCalculationTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.CalculationTypeId.ToString(),
                        Description = item.CalculationTypeName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the service type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for ServiceType.
        /// </returns>
        public IList<GenericCatalogDto> GetServiceTypeCatalog()
        {
            try
            {
                var serviceTypeDto = this.serviceTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in serviceTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ServiceTypeCode,
                        Description = item.ServiceTypeName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// GetFuelConceptCatalog Combo box
        /// </summary>
        /// <returns>List of Generic Catalog</returns>
        public IList<GenericCatalogDto> GetFuelConceptCatalog()
        {
            try
            {
                var fuelConceptDto = this.fuelConceptRepository.GetActivesFuelConcepts();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();

                foreach (var item in fuelConceptDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.FuelConceptID.ToString(),
                        Description = item.FuelConceptName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList(); ;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// GetFuelConceptTypeCatalog combo box
        /// </summary>
        /// <returns>List of Generic Catalog</returns>
        public IList<GenericCatalogDto> GetFuelConceptTypeCatalog()
        {
            try
            {
                var fuelConceptTypeDto = this.fuelConceptTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();

                foreach (var item in fuelConceptTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.FuelConceptTypeCode,
                        Description = item.FuelConceptTypeName
                    });
                }

                return generic.OrderBy(c => c.CodeAndDescription).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Get Charge Factor Type Catalog Combo box
        /// </summary>
        /// <returns>List of Generic Catalog</returns>
        public IList<GenericCatalogDto> GetChargeFactorTypeCatalog()
        {
            try
            {
                var chargeDto = this.chargeFactorTypeRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();

                foreach (var item in chargeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ChargeFactorTypeID.ToString(),
                        Description = item.ChargeFactorTypeName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList(); ;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the user type catalog.
        /// </summary>
        /// <returns>
        /// Generic Catalog with information for Users.
        /// </returns>
        public IList<GenericCatalogDto> GetUserCatalog()
        {
            try
            {
                var userDto = this.userRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in userDto)
                {
                    string lastName;
                    if (string.IsNullOrEmpty(item.LastName))
                    {
                        lastName = string.Empty;
                    }
                    else
                    {
                        lastName = item.LastName;
                    }

                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.UserID.ToString(),
                        Description = item.UserID.ToString() + " - " + item.Name + " " + item.FirstName + " " + item.LastName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the crew catalog.
        /// </summary>
        /// <returns>List of Generic Catalog</returns>
        public IList<GenericCatalogDto> GetCrewCatalog()
        {
            try
            {
                var crewDto = this.crewRepository.GetActivePilots();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in crewDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = Convert.ToString(item.CrewID),
                        Description = item.LastName + " " + item.FirstName + " " + item.MiddleName + "-" + item.CrewTypeID + "-" + item.NickName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Get Crew Types
        /// </summary>
        /// <returns>List of generic catalog.</returns>
        public IList<GenericCatalogDto> GetCrewSobCatalog()
        {
            try
            {
                var crewTypeDto = this.crewRepository.GetActiveStewardess();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in crewTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = Convert.ToString(item.CrewID),
                        Description = item.LastName + " " + item.FirstName + " " + item.MiddleName + "-" + item.CrewTypeID + "-" + item.NickName
                    });
                }

                return generic.OrderBy(c => c.Description).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns>List of generic catalog.</returns>
        public IList<GenericCatalogDto> GetMenu()
        {
            try
            {
                IList<Menu> menus = this.menuRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in menus)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = Convert.ToString(item.MenuCode),
                        Description = item.MenuName
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns>List of generic catalog.</returns>
        public IList<GenericCatalogDto> GetPermission()
        {
            try
            {
                IList<Permission> menus = this.permissionRepository.GetAll();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (Permission item in menus)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.PermissionCode,
                        Description = item.PermissionName
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// GetDepartmentCatalog
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetDepartmentCatalog()
        {
            try
            {
                var departmentDto = this.departmentRepository.GetActiveDepartments();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in departmentDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.DepartmentId.ToString(),
                        Description = item.DescriptionArea + "-" + item.DescriptionDepartment
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the profile catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetProfileCatalog()
        {
            try
            {
                var profileDto = this.profileRepository.GetProfiles();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in profileDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ProfileCode.ToString(),
                        Description = item.ProfileCode + "-" + item.ProfileName
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the profiles catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetRoleCatalog()
        {
            try
            {
                var roleDto = this.roleRepository.GetRoles();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in roleDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.RoleCode.ToString(),
                        Description = item.RoleCode + "-" + item.RoleName
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// GetUserCatalog by airport and roleName
        /// </summary>
        /// <param name="airport"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetUserCatalog(string airport, string roleName)
        {
            try
            {
                //Lista completa de proveedores
                //IList<GenericCatalogDto> userListComplete = new List<GenericCatalogDto>();

                //Lista completa de proveedores
                IList<UserDto> userListCustom = new List<UserDto>();
                userListCustom = this.userBusiness.GetUsersStation(airport);


                //Lista final con información requerida
                IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

                //LLenamos lista completa
                //userListComplete = this.GetUserCatalog();

                //Buscamos los compatibles en lista completa
                GenericCatalogDto find;
                
                foreach (UserDto item in userListCustom)
                {
                    find = new GenericCatalogDto();
                    if (item.UserProfileRoles.Any(c => c.ProfileRoles.Role.RoleName.Contains(roleName) && c.Principal == true))
                    {
                        string lastName = string.IsNullOrEmpty(item.LastName) ? string.Empty : item.LastName;
                        find.Description = item.Name + " " + item.FirstName + " " + item.LastName;
                        find.Id = item.UserID.ToString();
                    }
                    else
                    {
                        find = null;
                    }

                    if (find != null)
                    {
                        userListFinal.Add(find);
                    }
                }

                return userListFinal;
            }
            catch (Exception excetion)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Get Jet Fuel Process Catalog
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetJetFuelProcessCatalog()
        {
            try
            {
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                IList<JetFuelProcess> currentPeriodEntity = new List<JetFuelProcess>();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();

                // NOV 01 (YEAR - 1)
                startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);

                // FEB 28 (YEAR + 1)
                endDate = new DateTime((DateTime.Now.Year + 1), 2, DateTime.DaysInMonth(DateTime.Now.Year + 1, 2));

                currentPeriodEntity = this.jetFuelProcessRepository.GetCurrentPeriod(startDate, endDate);

                foreach (var item in currentPeriodEntity)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = Convert.ToString(item.PeriodCode),
                        Description = item.PeriodCode
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }


        /// <summary>
        /// Gets the national jet fuel process catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetNationalJetFuelProcessCatalog()
        {
            try
            {
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                IList<NationalJetFuelProcess> currentPeriodEntity = new List<NationalJetFuelProcess>();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();

                // NOV 01 (YEAR - 1)
                startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);

                // FEB 28 (YEAR + 1)
                endDate = new DateTime((DateTime.Now.Year + 1), 2, DateTime.DaysInMonth(DateTime.Now.Year + 1, 2));

                currentPeriodEntity = this.ntlJetFuelProcessRepository.GetCurrentPeriod(startDate, endDate); 

                foreach (var item in currentPeriodEntity)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = Convert.ToString(item.PeriodCode),
                        Description = item.PeriodCode
                    });
                }

                return generic;
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the national jet fuel services catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetNationalJetFuelServicesCatalog()
        {
            try
            {
                var serviceDto = this.serviceRepository.GetServices().Where(c => c.ServiceCode.Equals("CM"));
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in serviceDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ServiceCode,
                        Description = item.ServiceName
                    });
                }

                return generic.OrderBy(c => c.DescriptionAndCode).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }

        /// <summary>
        /// Gets the delay catalog.
        /// </summary>
        /// <returns>
        /// Generic catalog for the delays information.
        /// </returns>
        public IList<GenericCatalogDto> GetDelayCatalog()
        {
            try
            {
                IList<Delay> delayDto = this.delayRepository.GetActivesDelays();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in delayDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.DelayCode,
                        Description = item.DelayName
                    });
                }

                return generic.OrderBy(c => c.DescriptionAndCode).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }
        }


        /// <summary>
        /// Gets the tolerance type catalog.
        /// </summary>
        /// <returns></returns>
        public IList<GenericCatalogDto> GetToleranceTypeCatalog()
        {
            try
            {
                IList<ToleranceType> toleranceTypeDto = this.toleranceTypeRepository.GetActivesToleranceTypes();
                IList<GenericCatalogDto> generic = new List<GenericCatalogDto>();
                foreach (var item in toleranceTypeDto)
                {
                    generic.Add(new GenericCatalogDto
                    {
                        Id = item.ToleranceTypeCode,
                        Description = item.ToleranceTypeName
                    });
                }

                return generic.OrderBy(c => c.DescriptionAndCode).ToList();
            }
            catch (Exception)
            {
                return new List<GenericCatalogDto>();
            }        
        }
        
        #endregion
    }
}