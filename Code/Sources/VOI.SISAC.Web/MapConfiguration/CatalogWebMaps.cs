//------------------------------------------------------------------------
// <copyright file="CatalogWebMaps.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using Business.Dto.Paged;
    using Models.VO.Paged;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Class Country Web Maps
    /// </summary>
    public class CatalogWebMaps : Profile
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
                return "CatalogWebMaps";
            }
        }

        protected override void Configure()
        {
            this.Map();
            this.ChargeFactorTypeWebMap();
            this.ScheduleTypeWebMap();
            this.ToleranceTypeWebMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogWebMaps"/> class.
        /// </summary>
        public CatalogWebMaps()
        { }

        private void ScheduleTypeWebMap()
        {
            CreateMap<ScheduleTypeDto, ScheduleTypeVO>()
                .ReverseMap();
        }

        private void ToleranceTypeWebMap()
        {
            CreateMap<ToleranceTypeDto, ToleranceTypeVO>()
                .ReverseMap();
        }

        private void ChargeFactorTypeWebMap()
        {
            CreateMap<ChargeFactorTypeDto, ChargeFactorTypeVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            CreateMap<ReconciliationToleranceDto, ReconciliationToleranceVO>()
                .ReverseMap();

            CreateMap<CountryDto, CountryVO>()
                .ReverseMap();

            CreateMap<FunctionalAreaDto, FunctionalAreaVO>()
                .ReverseMap();

            CreateMap<GenderDto, GenderVO>()
               .ReverseMap();

            CreateMap<AirplaneWeightMeasureTypeDto, AirplaneWeightMeasureTypeVO>()
               .ReverseMap();

            CreateMap<AirplaneWeightTypeDto, AirplaneWeightTypeVO>()
                .ReverseMap();

            CreateMap<OperationTypeDto, OperationTypeVO>()
              .ReverseMap();

            CreateMap<ServiceCalculationTypeDto, ServiceCalculationTypeVO>()
              .ReverseMap();

            CreateMap<ServiceTypeDto, ServiceTypeVO>()
              .ReverseMap();

            CreateMap<StatusOnBoardDto, StatusOnBoardVO>()
              .ReverseMap();

            CreateMap<PagedDto, PagedVO>()
              .ReverseMap();

            CreateMap<MovementTypeDto, MovementTypeVO>()
              .ReverseMap();

            CreateMap<ProcedureCalculationDto, ProcedureCalculationVO>()
              .ReverseMap();
        }
    }
}