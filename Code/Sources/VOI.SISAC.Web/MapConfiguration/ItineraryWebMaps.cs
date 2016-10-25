//------------------------------------------------------------------------
// <copyright file="ItineraryWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using Models.Files;
    using System.Linq;

    /// <summary>
    /// Configurations for the maps between Entities and Dto's
    /// </summary>
    public class ItineraryWebMaps : Profile
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
                return "ItineraryWebMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Map();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private static void Map()
        {
            Mapper.CreateMap<ItineraryDto, ItineraryVO>()
                .ReverseMap();

            Mapper.CreateMap<ItineraryLogDto, ItineraryLogVO>()
                .ReverseMap();

            Mapper.CreateMap<ItineraryFile, ItineraryDto>()
                .ForMember(p => p.AirlineCode, r => r.MapFrom(s => s.AirlineCode))
                .ForMember(p => p.FlightNumber, r => r.MapFrom(s => s.FLTNUM))
                .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.ACREGNUMBER))
                .ForMember(p => p.DepartureStation, r => r.MapFrom(s => s.DepartureStation))
                .ForMember(p => p.ArrivalStation, r => r.MapFrom(s => s.ArrivalStation))
                .ForMember(p => p.DepartureDate, r => r.MapFrom(s => s.DepartureDate))
                .ForMember(p => p.ArrivalDate, r => r.MapFrom(s => s.ArrivalDate))
                .ForMember(p => p.ItineraryKey, r => r.MapFrom(s => s.ItineraryKey))
                .ReverseMap();

            Mapper.CreateMap<GendecDepartureDto, GendecDepartureVO>()
               .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber))
               .ForMember(p => p.DepartureDate, r => r.MapFrom(s => s.Itinerary.DepartureDate.ToShortDateString()))
               .ForMember(p => p.TimeDeparture, r => r.MapFrom(s => s.Itinerary.DepartureDate.Hour + ":" + s.Itinerary.DepartureDate.Minute))
               .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
               .ForMember(p => p.DepartureStation, r => r.MapFrom(s => s.Itinerary.DepartureStation))
               .ForMember(p => p.ArrivalStation, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
               .ReverseMap();

            Mapper.CreateMap<GendecArrivalDto, GendecArrivalVO>()
               .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber))
               .ForMember(p => p.ArrivalDate, r => r.MapFrom(s => s.Itinerary.ArrivalDate.ToShortDateString()))
               .ForMember(p => p.TimeArrival, r => r.MapFrom(s => s.Itinerary.ArrivalDate.Hour + ":" + s.Itinerary.ArrivalDate.Minute))
               .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
               .ForMember(p => p.DepartureStation, r => r.MapFrom(s => s.Itinerary.DepartureStation))
               .ForMember(p => p.ArrivalStation, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
               .ReverseMap();

            Mapper.CreateMap<GendecDepartureDto, GendecArrivalVO>()
                .ReverseMap();

            Mapper.CreateMap<ItinerarySearchDto, ItinerarySearchVO>()
                .ReverseMap();

            Mapper.CreateMap<ItineraryUploadDto, ItineraryUploadVO>()
                .ReverseMap();

            Mapper.CreateMap<ManifestDepartureDto, ManifestDepartureVO>()
                .ReverseMap();

            Mapper.CreateMap<ManifestArrivalDto, ManifestArrivalVO>()
                .ForMember(c => c.LastStationCode, f => f.MapFrom(w => w.LastScaleStationCode))
                .ReverseMap();

            Mapper.CreateMap<ManifestArrivalVO, ManifestArrivalDto>()
                .ForMember(c => c.LastScaleStationCode, f => f.MapFrom(w => w.LastStationCode))
                .ReverseMap();
        }
    }
}