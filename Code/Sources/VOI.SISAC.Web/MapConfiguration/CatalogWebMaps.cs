//------------------------------------------------------------------------
// <copyright file="CatalogWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using Business.Dto.Paged;
    using Models.VO.Paged;    /// <summary>
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

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
            this.ChargeFactorTypeWebMap();
            this.ScheduleTypeWebMap();
            this.ToleranceTypeWebMap();
        }

        private void ScheduleTypeWebMap()
        {
            Mapper.CreateMap<ScheduleTypeDto, ScheduleTypeVO>()
                .ReverseMap();
        }

        private void ToleranceTypeWebMap()
        {
            Mapper.CreateMap<ToleranceTypeDto, ToleranceTypeVO>()
                .ReverseMap();
        }

        private void ChargeFactorTypeWebMap()
        {
            Mapper.CreateMap<ChargeFactorTypeDto, ChargeFactorTypeVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            Mapper.CreateMap<ReconciliationToleranceDto, ReconciliationToleranceVO>()
                .ReverseMap();

            Mapper.CreateMap<CountryDto, CountryVO>()
                .ReverseMap();

            Mapper.CreateMap<FunctionalAreaDto, FunctionalAreaVO>()
                .ReverseMap();

            Mapper.CreateMap<GenderDto, GenderVO>()
               .ReverseMap();

            Mapper.CreateMap<AirplaneWeightMeasureTypeDto, AirplaneWeightMeasureTypeVO>()
               .ReverseMap();

            Mapper.CreateMap<AirplaneWeightTypeDto, AirplaneWeightTypeVO>()
            .ReverseMap();

            Mapper.CreateMap<OperationTypeDto, OperationTypeVO>()
            .ReverseMap();

            Mapper.CreateMap<ServiceCalculationTypeDto, ServiceCalculationTypeVO>()
            .ReverseMap();

            Mapper.CreateMap<ServiceTypeDto, ServiceTypeVO>()
            .ReverseMap();

            Mapper.CreateMap<StatusOnBoardDto, StatusOnBoardVO>()
            .ReverseMap();

            Mapper.CreateMap<PagedDto, PagedVO>()
            .ReverseMap();
        }
    }
}