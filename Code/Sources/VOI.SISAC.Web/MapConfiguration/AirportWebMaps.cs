//------------------------------------------------------------------------
// <copyright file="AirportWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Airport;

    /// <summary>
    /// Class Airport Web Maps
    /// </summary>
    public class AirportWebMaps : Profile
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
                return "AirportWebMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
            this.AirportMap();
            this.AirportGroupMap();
            this.GpuMap();
            this.GpuObservationMap();
            this.DelayMap();
            this.ManifestTimeConfigMap();
            this.FuelConceptTypeWebMap();
            this.AirportServiceMap();
            this.JetFuelTicketMap();
            this.PassengerInformationMap();
            this.NationalJetFuelTicketMap();
        }

        /// <summary>
        /// Nationals the jet fuel ticket map.
        /// </summary>
        private void NationalJetFuelTicketMap()
        {
            Mapper.CreateMap<NationalJetFuelTicketDto, NationalJetFuelTicketVO>()
                .ReverseMap();
        }

        /// <summary>
        /// JetFuelTicketMap
        /// </summary>
        private void JetFuelTicketMap()
        {
            Mapper.CreateMap<JetFuelTicketDto, JetFuelTicketVO>()
                .ReverseMap();
        }

        /// <summary>
        /// AirportServiceMap
        /// </summary>
        private void AirportServiceMap()
        {
            Mapper.CreateMap<AirportServiceDto, AirportServiceVO>()
                .ReverseMap();
        }

        /// <summary>
        /// FuelConceptTypeWebMap
        /// </summary>
        private void FuelConceptTypeWebMap()
        {
            Mapper.CreateMap<FuelConceptTypeDto, FuelConceptTypeVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Manifest Time Config Map
        /// </summary>
        private void ManifestTimeConfigMap()
        {
            Mapper.CreateMap<ManifestTimeConfigDto, ManifestTimeConfigVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Delay Map Config
        /// </summary>
        private void DelayMap()
        {
            Mapper.CreateMap<DelayDto, DelayVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Gpu Observation Map Config
        /// </summary>
        private void GpuObservationMap()
        {
            Mapper.CreateMap<GpuObservationDto, GpuObservationVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Gpu Map Config
        /// </summary>
        private void GpuMap()
        {
            Mapper.CreateMap<GpuDto, GpuVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Group Map Config
        /// </summary>
        private void AirportGroupMap()
        {
            Mapper.CreateMap<AirportGroupDto, AirportGroupVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Map configuration
        /// </summary>
        private void AirportMap()
        {
            Mapper.CreateMap<AirportDto, AirportVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Passenger Information Configuration
        /// </summary>
        private void PassengerInformationMap()
        {
            Mapper.CreateMap<PassengerInformationDto, PassengerInformationVO>()
                .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber ))
                .ForMember(p => p.DepartureDate, r => r.MapFrom(s => s.Itinerary.DepartureDate.ToShortDateString()))
                .ForMember(p => p.TimeDeparture, r => r.MapFrom(s => s.Itinerary.DepartureDate.Hour + ":"+ s.Itinerary.DepartureDate.Minute ))
                .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
                .ForMember(p => p.Departure, r => r.MapFrom(s => s.Itinerary.DepartureStation))
                .ForMember(p => p.Arrival, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
                .ReverseMap();
        }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            Mapper.CreateMap<CrewDto, CrewVO>()
                .ReverseMap();

            Mapper.CreateMap<AirlineDto, AirlineVO>()
                .ReverseMap();

            Mapper.CreateMap<FuelConceptDto, FuelConceptVO>()
                .ReverseMap();

            Mapper.CreateMap<ServiceDto, ServiceVO>()
                .ReverseMap();

            Mapper.CreateMap<AirplaneDto, AirplaneVO>()
                .ReverseMap();

            Mapper.CreateMap<AirplaneTypeDto, AirplaneTypeVO>()
                .ReverseMap();

            Mapper.CreateMap<DrinkingWaterDto, DrinkingWaterVO>()
                .ReverseMap();

            Mapper.CreateMap<AirportScheduleDto, AirportScheduleVO>()
                .ReverseMap();

            Mapper.CreateMap<CrewTypeDto, CrewTypeVO>()
                .ReverseMap();

            Mapper.CreateMap<CrewFile, CrewDto>()
                .ReverseMap();
        }
    }
}