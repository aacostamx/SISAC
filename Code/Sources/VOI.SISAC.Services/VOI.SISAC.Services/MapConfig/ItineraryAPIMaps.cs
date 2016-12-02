//------------------------------------------------------------------------
// <copyright file="ItineraryAPIMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Services.MapConfig
{
    using Business.Dto.Itineraries;
    using global::AutoMapper;
    using Models.Files;

    /// <summary>
    /// Itinerary API Maps Class
    /// </summary>
    public class ItineraryAPIMaps : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ItineraryAPIMaps";
            }
        }

        protected override void Configure()
        {
            Map();
        }

        private static void Map()
        {
            Mapper.CreateMap<ItineraryDto, ItineraryFile>()
                .ForMember(t => t.FLTNUM, f => f.MapFrom(r => r.FlightNumber))
                .ForMember(t => t.ACREGNUMBER, f => f.MapFrom(r => r.EquipmentNumber))
                .ForMember(t => t.DEP, f => f.MapFrom(r => r.DepartureStation))
                .ForMember(t => t.DST, f => f.MapFrom(r => r.ArrivalStation))
                .ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineCode))
                .ReverseMap();
        }
    }
}