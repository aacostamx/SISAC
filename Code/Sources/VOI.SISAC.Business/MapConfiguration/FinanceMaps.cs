//------------------------------------------------------------------------
// <copyright file="FinanceMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Configurations for the maps between Entities and Dto's
    /// </summary>
    public class FinanceMaps : Profile
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
                return "FinanceMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
            this.AirportServiceContractMap();
            this.ExchangeRatesMap();
            NationalFuelContractMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceMaps"/> class.
        /// </summary>
        public FinanceMaps()
        { }

        /// <summary>
        /// ExchangeRatesMap Map Method
        /// </summary>
        private void ExchangeRatesMap()
        {
            CreateMap<ExchangeRates, ExchangeRatesDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            /*
             * En esta sección se registran las conversiones que realizará Auto
             * 
             * Automapper convierte de manera automática las conversiones
             * entre objetos que sus propiedades tienen el mismo nombre
             * y tipo de dato. Para las propiedades que no coinciden,
             * es necesario hacer la conversión de manera explícita.
             * 
             * El siguiente código ilustra la manera de hacer las conversiones
             * de manera explísita.
            */

            // Para convertir de un Currency a un CurrencyDto,
            // se hará de la siguiente manera:
            CreateMap<Currency, CurrencyDto>()

                // t: Entidad destino (CurrencyDto).
                // f: Etindad origen (Currency).
                // r: Entidad origen (Currency).
                // La propiedad CurrencyName de la entidad destino,
                // sera igual a la propiedad CurrencyName de la entidad origen.
                .ForMember(t => t.CurrencyCode, f => f.MapFrom(r => r.CurrencyCode))
                .ForMember(t => t.CurrencyName, f => f.MapFrom(r => r.CurrencyName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                // La conversión sera también en doble vía de CurrencyDto a Currency.
                .ReverseMap();


            CreateMap<AccountingAccount, AccountingAccountDto>()

                // t: Entidad destino (AccountingAccountDto).
                // f: Etindad origen (AccountingAccount).
                // r: Entidad origen (AccountingAccount).
                // La propiedad AccountingAccountName de la entidad destino,
                // sera igual a la propiedad AccountingAccountName de la entidad origen.
                .ForMember(t => t.AccountingAccountNumber, f => f.MapFrom(r => r.AccountingAccountNumber))
                .ForMember(t => t.AccountingAccountName, f => f.MapFrom(r => r.AccountingAccountName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                // La conversión sera también en doble vía de AccountingAccountDto a AccountingAccount.
                .ReverseMap();

            CreateMap<LiabilityAccount, LiabilityAccountDto>()

                // t: Entidad destino (AccountingAccountDto).
                // f: Etindad origen (AccountingAccount).
                // r: Entidad origen (AccountingAccount).
                // La propiedad AccountingAccountName de la entidad destino,
                // sera igual a la propiedad AccountingAccountName de la entidad origen.
                .ForMember(t => t.LiabilityAccountNumber, f => f.MapFrom(r => r.LiabilityAccountNumber))
                .ForMember(t => t.LiabilityAccountName, f => f.MapFrom(r => r.LiabilityAccountName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                // La conversión sera también en doble vía de AccountingAccountDto a AccountingAccount.
                .ReverseMap();

            /*
             * En caso que se tengan colecciones dentro de nuestras entidades
             * que se necesiten mapear entre ellas y ya se ha especificado 
             * su conversión, se puede realizar la conversión de la siguiente manera:
             *   
             *  .ForMember(t => t.Requisitos, f => f.MapFrom(c => c.Requisitos))
             */

            // ********* Set here the configurations for each map. ********* //

            /*
             * Se realiza el mapeo de CostCenter a CostCenterDto
             * */

            CreateMap<CostCenter, CostCenterDto>()

                // t: Entidad destino (CostCenterDto).
                // f: Entidad origen (CostCenter).
                // r: Entidad origen (CostCenter).
                // La propiedad CCNumber de la entidad destino,
                // sera igual a la propiedad CCNumber de la entidad origen.
                //.ForMember(t => t.CCNumber, f => f.MapFrom(r => r.CCNumber))
                //.ForMember(t => t.CCName, f => f.MapFrom(r => r.CCName))
                //.ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                //.ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineCode))
                //.ForMember(t => t.Airline, f => f.MapFrom(r => r.Airline))

                // La conversión sera también en doble vía de CostCenterDto a CostCenter.
                .ReverseMap();

            //Provider Section
            CreateMap<Provider, ProviderDto>()
                .ForMember(t => t.ProviderNumber, f => f.MapFrom(r => r.ProviderNumber))
                .ForMember(t => t.ProviderName, f => f.MapFrom(r => r.ProviderName))
                .ForMember(t => t.ProviderShortName, f => f.MapFrom(r => r.ProviderShortName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();

            //Taxes Section
            CreateMap<Tax, TaxDto>()
                .ForMember(t => t.TaxCode, f => f.MapFrom(r => r.TaxCode))
                .ForMember(t => t.TaxName, f => f.MapFrom(r => r.TaxName))
                //.ForMember(t => t.TaxValue, f => f.MapFrom(r => r.TaxValue))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();

            //InternationalFuelContract Section
            CreateMap<InternationalFuelContract, InternationalFuelContractDto>()
                .ReverseMap();

            //InternationalFuelContractConcept Section
            CreateMap<InternationalFuelContractConcept, InternationalFuelContractConceptDto>()
                .ReverseMap();

            //InternationalFuelRate Section
            CreateMap<InternationalFuelRate, InternationalFuelRateDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Airports the service contract map.
        /// </summary>
        private void AirportServiceContractMap()
        {
            CreateMap<AirportServiceContract, AirportServiceContractDto>()
                .ReverseMap();
        }

        /// <summary>
        /// National fuel contract maps.
        /// </summary>
        private void NationalFuelContractMap()
        {
            // Contract maps
            CreateMap<NationalFuelContract, NationalFuelContractDto>()
                .ForMember(c => c.AirlineName, r => r.MapFrom(f => f.Airline.AirlineName))
                .ForMember(c => c.StationName, r => r.MapFrom(f => f.Airport.StationName))
                .ForMember(c => c.ServiceDescription, r => r.MapFrom(f => f.Service.ServiceName))
                .ForMember(c => c.ProviderName, r => r.MapFrom(f => f.Provider.ProviderName))
                .ForMember(c => c.AccountingAccountName, r => r.MapFrom(f => f.AccountingAccount.AccountingAccountName))
                .ForMember(c => c.LiabilityAccountName, r => r.MapFrom(f => f.LiabilityAccount.LiabilityAccountName))
                .ForMember(c => c.CostCenterName, r => r.MapFrom(f => f.CostCenter.CCName))
                .ForMember(c => c.CurrencyName, r => r.MapFrom(f => f.Currency.CurrencyName))
                .ForMember(c => c.FederalTaxDescription, r => r.MapFrom(f => f.FederalTax.TaxName))
                .ForMember(c => c.OperationTypeName, r => r.MapFrom(f => f.OperationType.OperationName))
                .ForMember(c => c.CCNumber, r => r.MapFrom(f => f.CostCenterNumber));

            CreateMap<NationalFuelContractDto, NationalFuelContract>()
                .ForMember(c => c.CostCenterNumber, r => r.MapFrom(f => f.CCNumber));

            // Concept maps
            CreateMap<NationalFuelContractConcept, NationalFuelContractConceptDto>()
                .ForMember(c => c.ChargeFactorTypeName, r => r.MapFrom(f => f.ChargeFactorType.ChargeFactorTypeName))
                .ForMember(c => c.FuelConceptName, r => r.MapFrom(f => f.FuelConcept.FuelConceptName))
                .ForMember(c => c.FuelConceptTypeName, r => r.MapFrom(f => f.FuelConceptType.FuelConceptTypeName))
                .ForMember(c => c.ProviderName, r => r.MapFrom(f => f.Provider.ProviderName));

            CreateMap<NationalFuelContractConceptDto, NationalFuelContractConcept>();

            // Rate maps
            CreateMap<NationalFuelRate, NationalFuelRateDto>()
                .ForMember(c => c.StationName, r => r.MapFrom(f => f.Airport.StationName))
                .ForMember(c => c.ServiceName, r => r.MapFrom(f => f.Service.ServiceName))
                .ForMember(c => c.ProviderName, r => r.MapFrom(f => f.Provider.ProviderName))
                .ForMember(c => c.FuelConceptTypeName, r => r.MapFrom(f => f.FuelConceptType.FuelConceptTypeName))
                .ForMember(c => c.CurrencyName, r => r.MapFrom(f => f.Currency.CurrencyName))
                .ForMember(c => c.ScheduleTypeName, r => r.MapFrom(f => f.ScheduleType.ScheduleTypeName));

            CreateMap<NationalFuelRateDto, NationalFuelRate>();
        }
    }
}