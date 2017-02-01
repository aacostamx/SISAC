//------------------------------------------------------------------------
// <copyright file="ItineraryAPIMaps.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Services.MapConfig
{
    using System;
    using Business.Dto.Itineraries;
    using global::AutoMapper;
    using Models.Files;

    /// <summary>
    /// Itinerary API Maps Class
    /// </summary>
    public class ItineraryAPIMaps : Profile
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
                return "ItineraryAPIMaps";
            }
        }

        protected override void Configure()
        {
            Map();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryAPIMaps"/> class.
        /// </summary>
        public ItineraryAPIMaps()
        {
            
        }

        private void Map()
        {
            CreateMap<ItineraryDto, ItineraryFile>()
                .ForMember(t => t.FLTNUM, f => f.MapFrom(r => r.FlightNumber))
                .ForMember(t => t.ACREGNUMBER, f => f.MapFrom(r => r.EquipmentNumber))
                .ForMember(t => t.DEP, f => f.MapFrom(r => r.DepartureStation))
                .ForMember(t => t.DST, f => f.MapFrom(r => r.ArrivalStation))
                .ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineCode))
                .ReverseMap();
        }
    }
}