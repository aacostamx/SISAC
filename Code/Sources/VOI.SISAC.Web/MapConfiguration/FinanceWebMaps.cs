//------------------------------------------------------------------------
// <copyright file="FinanceWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using Business.Dto.Airports;
    using Business.Dto.Catalogs;
    using Models.Files;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Web.Models.VO.Finance;

    /// <summary>
    /// Configurations for the maps between Entities and Dto's
    /// </summary>
    public class FinanceWebMaps : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return "FinanceWebMaps";
            }
        }

        protected override void Configure()
        {
            Map();
            AirporServiceContractMap();
            ContractParametersMap();
            FileHelperMap();
            FileServiceContractMap();
            ExchangeRatesMap();
            NationalFuelContractMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceWebMaps"/> class.
        /// </summary>
        public FinanceWebMaps()
        {
            
        }

        private void ExchangeRatesMap()
        {
            CreateMap<ExchangeRatesDto, ExchangeRatesVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Files the helper map.
        /// </summary>
        private void FileHelperMap()
        {
            CreateMap<InternationalFuelContractFile, InternationalFuelContractConceptDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, FuelConceptDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, FuelConceptTypeDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, ChargeFactorTypeDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, ProviderDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, OperationTypeDto>()
                .ReverseMap();

            CreateMap<InternationalFuelContractFile, InternationalFuelContractDto>()
                .ForMember(c => c.OperationType, r => r.ResolveUsing(f => new OperationTypeDto() { OperationName = f.OperationName }))
                .ReverseMap();
        }

        /// <summary>
        /// Files the service contract map.
        /// </summary>
        private void FileServiceContractMap()
        {
            CreateMap<AirportServiceContractFile, AirportServiceContractDto>()
                .ForMember(c => c.OperationType, r => r.ResolveUsing(f => new OperationTypeDto() { OperationName = f.OperationName }))
                .ForMember(c => c.ServiceType, r => r.ResolveUsing(f => new ServiceTypeDto() { ServiceTypeName = f.ServiceTypeName }))
                .ForMember(c => c.ServiceCalculationType, r => r.ResolveUsing(f => new ServiceCalculationTypeDto() { CalculationTypeName = f.CalculationTypeName }))
                .ForMember(c => c.AirplaneWeightType, r => r.ResolveUsing(f => new AirplaneWeightTypeDto() { AirplaneWeightName = f.AirplaneWeightName }))
                .ForMember(c => c.AirplaneWeightMeasureType, r => r.ResolveUsing(f => new AirplaneWeightMeasureTypeDto() { AirplaneWeightMeasureName = f.AirplaneWeightMeasureName }))
                .ReverseMap();
            CreateMap<InternationalFuelRatesFile, InternationalFuelRateDto>()
                .ReverseMap();

            CreateMap<InternationalFuelRatesFile, InternationalFuelContractDto>()
                .ReverseMap();

            CreateMap<InternationalFuelRatesFile, InternationalFuelRateFileDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            CreateMap<CurrencyDto, CurrencyVO>()
                .ReverseMap();

            CreateMap<ProviderDto, ProviderVO>()
              .ReverseMap();

            CreateMap<TaxDto, TaxVO>()
                .ReverseMap();

            CreateMap<AccountingAccountDto, AccountingAccountVO>()
                .ReverseMap();

            CreateMap<LiabilityAccountDto, LiabilityAccountVO>()
                .ReverseMap();

            CreateMap<CostCenterDto, CostCenterVO>()
                .ReverseMap();

            CreateMap<InternationalFuelContractDto, InternationalFuelContractVO>()
                .ReverseMap();

            CreateMap<InternationalFuelContractConceptDto, InternationalFuelContractConceptVO>()
                .ReverseMap();

            CreateMap<InternationalFuelRateDto, InternationalFuelRateVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps between AirportServiceContract Dto and VO.
        /// </summary>
        private void AirporServiceContractMap()
        {
            CreateMap<AirportServiceContractDto, AirportServiceContractVO>()
                 .ForMember(t => t.AirlineName, f => f.MapFrom(r => r.Airline.AirlineName))
                 .ForMember(t => t.StationName, f => f.MapFrom(r => r.Airport.StationName))
                 .ForMember(t => t.ServiceName, f => f.MapFrom(r => r.Service.ServiceName))
                 .ForMember(t => t.ProviderName, f => f.MapFrom(r => r.Provider.ProviderName))
                 .ForMember(t => t.CCName, f => f.MapFrom(r => r.CostCenter.CCName))
                 .ForMember(t => t.AccountingAccountName, f => f.MapFrom(r => r.AccountingAccount.AccountingAccountName))
                 .ForMember(t => t.LiabilityAccountName, f => f.MapFrom(r => r.LiabilityAccount.LiabilityAccountName))
                 .ForMember(t => t.OperationName, f => f.MapFrom(r => r.OperationType.OperationName))
                 .ForMember(t => t.ServiceTypeName, f => f.MapFrom(r => r.ServiceType.ServiceTypeName))
                 .ForMember(t => t.AirportTaxName, f => f.MapFrom(r => r.AirportTax.TaxName))
                 .ForMember(t => t.LocalTaxName, f => f.MapFrom(r => r.LocalTax.TaxName))
                 .ForMember(t => t.StateTaxName, f => f.MapFrom(r => r.StateTax.TaxName))
                 .ForMember(t => t.FederalTaxName, f => f.MapFrom(r => r.FederalTax.TaxName))
                 .ForMember(t => t.CurrencyName, f => f.MapFrom(r => r.Currency.CurrencyName))
                 .ForMember(t => t.CalculationTypeName, f => f.MapFrom(r => r.ServiceCalculationType.CalculationTypeName))
                 .ForMember(t => t.AirplaneWeightName, f => f.MapFrom(r => r.AirplaneWeightType.AirplaneWeightName))
                 .ForMember(t => t.AirplaneWeightUnitName, f => f.MapFrom(r => r.AirplaneWeightMeasureType.AirplaneWeightMeasureName))
                 .ReverseMap();
        }

        /// <summary>
        /// Maps between AirportServiceContractDto and ContractParametersVO.
        /// </summary>
        private void ContractParametersMap()
        {
            CreateMap<ContractParametersVO, AirportServiceContractDto>()
                .ForMember(t => t.EffectiveDate, f => f.MapFrom(r => r.EffectiveDateParameter))
                .ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineDescription))
                .ForMember(t => t.StationCode, f => f.MapFrom(r => r.AirportDescription))
                .ForMember(t => t.ServiceCode, f => f.MapFrom(r => r.ServiceDescription))
                .ForMember(t => t.ProviderNumber, f => f.MapFrom(r => r.ProviderDescription))
                .ForMember(t => t.CostCenterNumber, f => f.MapFrom(r => r.CostCenterNumber))
                .ReverseMap();
        }

        /// <summary>
        /// Maps the nationals fuel contract objects.
        /// </summary>
        private void NationalFuelContractMap()
        {
            CreateMap<NationalFuelContractVO, NationalFuelContractDto>()
                .ReverseMap();
            CreateMap<NationalFuelContractConceptVO, NationalFuelContractConceptDto>()
                .ReverseMap();

            // From parameters to dto
            CreateMap<NationalFuelContractSearchVO, NationalFuelContractDto>()
                .ReverseMap();

            // From file to Dto and vice versa
            CreateMap<NationalFuelContractFile, NationalFuelContractDto>()
                .ReverseMap();
            CreateMap<NationalFuelContractFile, NationalFuelContractConceptDto>()
                .ReverseMap();

            // From file to Dto and vice versa
            CreateMap<NationalFuelRateFile, NationalFuelRateDto>()
                .ReverseMap();
            CreateMap<NationalFuelRateVO, NationalFuelRateDto>()
                .ReverseMap();
        }
    }
}