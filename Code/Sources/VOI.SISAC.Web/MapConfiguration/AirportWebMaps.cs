//------------------------------------------------------------------------
// <copyright file="AirportWebMaps.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
        /// Initializes a new instance of the <see cref="AirportWebMaps"/> class.
        /// </summary>
        public AirportWebMaps()
        {
            
        }

        /// <summary>
        /// Nationals the jet fuel ticket map.
        /// </summary>
        private void NationalJetFuelTicketMap()
        {
            CreateMap<NationalJetFuelTicketDto, NationalJetFuelTicketVO>()
                .ReverseMap();
        }

        /// <summary>
        /// JetFuelTicketMap
        /// </summary>
        private void JetFuelTicketMap()
        {
            CreateMap<JetFuelTicketDto, JetFuelTicketVO>()
                .ReverseMap();
        }

        /// <summary>
        /// AirportServiceMap
        /// </summary>
        private void AirportServiceMap()
        {
            CreateMap<AirportServiceDto, AirportServiceVO>()
                .ReverseMap();
        }

        /// <summary>
        /// FuelConceptTypeWebMap
        /// </summary>
        private void FuelConceptTypeWebMap()
        {
            CreateMap<FuelConceptTypeDto, FuelConceptTypeVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Manifest Time Config Map
        /// </summary>
        private void ManifestTimeConfigMap()
        {
            CreateMap<ManifestTimeConfigDto, ManifestTimeConfigVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Delay Map Config
        /// </summary>
        private void DelayMap()
        {
            CreateMap<DelayDto, DelayVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Gpu Observation Map Config
        /// </summary>
        private void GpuObservationMap()
        {
            CreateMap<GpuObservationDto, GpuObservationVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Gpu Map Config
        /// </summary>
        private void GpuMap()
        {
            CreateMap<GpuDto, GpuVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Group Map Config
        /// </summary>
        private void AirportGroupMap()
        {
            CreateMap<AirportGroupDto, AirportGroupVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Map configuration
        /// </summary>
        private void AirportMap()
        {
            CreateMap<AirportDto, AirportVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Passenger Information Configuration
        /// </summary>
        private void PassengerInformationMap()
        {
            CreateMap<PassengerInformationDto, PassengerInformationVO>()
                .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber))
                .ForMember(p => p.DepartureDate, r => r.MapFrom(s => s.Itinerary.DepartureDate.ToShortDateString()))
                .ForMember(p => p.TimeDeparture, r => r.MapFrom(s => s.Itinerary.DepartureDate.Hour + ":" + s.Itinerary.DepartureDate.Minute))
                .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
                .ForMember(p => p.Departure, r => r.MapFrom(s => s.Itinerary.DepartureStation))
                .ForMember(p => p.Arrival, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
                .ReverseMap();

            CreateMap<AdditionalPassengerInformationDto, AdditionalPassengerInformationVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            CreateMap<CrewDto, CrewVO>()
                .ReverseMap();

            CreateMap<AirlineDto, AirlineVO>()
                .ReverseMap();

            CreateMap<FuelConceptDto, FuelConceptVO>()
                .ReverseMap();

            CreateMap<ServiceDto, ServiceVO>()
                .ReverseMap();

            CreateMap<AirplaneDto, AirplaneVO>()
                .ReverseMap();

            CreateMap<AirplaneTypeDto, AirplaneTypeVO>()
                .ReverseMap();

            CreateMap<DrinkingWaterDto, DrinkingWaterVO>()
                .ReverseMap();

            CreateMap<AirportScheduleDto, AirportScheduleVO>()
                .ReverseMap();

            CreateMap<CrewTypeDto, CrewTypeVO>()
                .ReverseMap();

            CreateMap<CrewFile, CrewDto>()
                .ReverseMap();
        }
    }
}