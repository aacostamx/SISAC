//------------------------------------------------------------------------
// <copyright file="CatalogMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Entities.Catalog;
    using Entities.Paged;
    using Dto.Paged;

    /// <summary>
    /// Catalog maps
    /// </summary>
    public class CatalogMaps : Profile
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
                return "CatalogMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Map();
            CountryMap();
            ChargeFactorTypeMap();
            AirplaneWeightMeasureTypeMap();
            AirplaneWeightTypeMap();
            OperationTypeMap();
            ServiceCalculationTypeMap();
            ServiceTypeMap();
            StatusOnBoardMap();
            ScheduleTypeMap();

            this.ChargeFactorTypeMap();
            this.ReconciliationToleranceMap();
            this.ToleranceTypeMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogMaps"/> class.
        /// </summary>
        public CatalogMaps()
        { }

        /// <summary>
        /// Reconciliations the tolerance map.
        /// </summary>
        private void ReconciliationToleranceMap()
        {
            CreateMap<ReconciliationTolerance, ReconciliationToleranceDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Tolerances the type map.
        /// </summary>
        private void ToleranceTypeMap()
        {
            CreateMap<ToleranceType, ToleranceTypeDto>()
                .ReverseMap();
        }


        /// <summary>
        /// Schedules the type map.
        /// </summary>
        private void ScheduleTypeMap()
        {
            CreateMap<ScheduleType, ScheduleTypeDto>()
                .ReverseMap();
        }

        /// <summary>
        /// ChargeFactorType Mapping
        /// </summary>
        private void ChargeFactorTypeMap()
        {
            CreateMap<ChargeFactorType, ChargeFactorTypeDto>()
                .ForMember(t => t.ChargeFactorTypeID, f => f.MapFrom(r => r.ChargeFactorTypeID))
                .ForMember(t => t.ChargeFactorTypeName, f => f.MapFrom(r => r.ChargeFactorTypeName))
                .ForMember(t => t.InternationalFuelContractConcept, f => f.MapFrom(r => r.InternationalFuelContractConcept))
                .ReverseMap();
        }

        /// <summary>
        /// Airplanes the weight measure type map.
        /// </summary>
        public void AirplaneWeightMeasureTypeMap()
        {
            CreateMap<AirplaneWeightMeasureType, AirplaneWeightMeasureTypeDto>()
            .ForMember(t => t.AirplaneWeightMeasureId, f => f.MapFrom(r => r.AirplaneWeightMeasureId))
            .ForMember(t => t.AirplaneWeightMeasureName, f => f.MapFrom(r => r.AirplaneWeightMeasureName))
            .ReverseMap();

        }

        /// <summary>
        /// Airplanes the weight type map.
        /// </summary>
        public void AirplaneWeightTypeMap()
        {
            CreateMap<AirplaneWeightType, AirplaneWeightTypeDto>()
            .ForMember(t => t.AirplaneWeightCode, f => f.MapFrom(r => r.AirplaneWeightCode))
            .ForMember(t => t.AirplaneWeightName, f => f.MapFrom(r => r.AirplaneWeightName))
            .ReverseMap();
        }

        /// <summary>
        /// Operations the type map.
        /// </summary>
        public void OperationTypeMap()
        {
            CreateMap<OperationType, OperationTypeDto>()
            .ForMember(t => t.OperationTypeId, f => f.MapFrom(r => r.OperationTypeId))
            .ForMember(t => t.OperationName, f => f.MapFrom(r => r.OperationName))
            .ReverseMap();
        }

        /// <summary>
        /// Services the calculation type map.
        /// </summary>
        public void ServiceCalculationTypeMap()
        {
            CreateMap<ServiceCalculationType, ServiceCalculationTypeDto>()
            .ForMember(t => t.CalculationTypeId, f => f.MapFrom(r => r.CalculationTypeId))
            .ForMember(t => t.CalculationTypeName, f => f.MapFrom(r => r.CalculationTypeName))
            .ReverseMap();
        }

        /// <summary>
        /// Services the type map.
        /// </summary>
        public void ServiceTypeMap()
        {
            CreateMap<ServiceType, ServiceTypeDto>()
            .ForMember(t => t.ServiceTypeCode, f => f.MapFrom(r => r.ServiceTypeCode))
            .ForMember(t => t.ServiceTypeName, f => f.MapFrom(r => r.ServiceTypeName))
            .ReverseMap();
        }

        /// <summary>
        /// Statuses the on board map.
        /// </summary>
        public void StatusOnBoardMap()
        {
            CreateMap<StatusOnBoard, StatusOnBoardDto>()
           .ForMember(t => t.StatusOnBoardCode, f => f.MapFrom(r => r.StatusOnBoardCode))
           .ForMember(t => t.StatusOnBoardName, f => f.MapFrom(r => r.StatusOnBoardName))
           .ReverseMap();
        }

        private void CountryMap()
        {
            CreateMap<Country, CountryDto>()
                .ForMember(t => t.CountryCode, f => f.MapFrom(r => r.CountryCode))
                .ForMember(t => t.CountryName, f => f.MapFrom(r => r.CountryName))
                .ForMember(t => t.CountryCodeShort, f => f.MapFrom(r => r.CountryCodeShort))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();
        }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            CreateMap<FunctionalArea, FunctionalAreaDto>()
                .ForMember(t => t.FunctionalAreaID, f => f.MapFrom(r => r.FunctionalAreaID))
                .ForMember(t => t.FunctionalAreaName, f => f.MapFrom(r => r.FunctionalAreaName))
                .ForMember(t => t.FunctionalAreaDescription, f => f.MapFrom(r => r.FunctionalAreaDescription))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();

            CreateMap<Paged, PagedDto>()
                .ReverseMap();

            CreateMap<MovementType, MovementTypeDto>()
                .ReverseMap();

            CreateMap<ProcedureCalculation, ProcedureCalculationDto>()
                .ReverseMap();
        }
    }
}
