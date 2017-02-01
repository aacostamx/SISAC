//------------------------------------------------------------------------
// <copyright file="ItinerariesMaps.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    ///  Configurations for the maps between Entities and Dto's
    /// </summary>
    public class ItinerariesMaps : Profile
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
                return "ItinerariesMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItinerariesMaps"/> class.
        /// </summary>
        public ItinerariesMaps()
        { }

        /// <summary>
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            CreateMap<Itinerary, ItineraryDto>()
                .ForMember(c => c.GendecDepartureIsClose, f => f.MapFrom(r => r.GendecDepartures.Closed))
                .ForMember(c => c.GendecArrivalIsClose, f => f.MapFrom(r => r.GendecArrivals.Closed))
                .ReverseMap();

            CreateMap<ItineraryLog, ItineraryLogDto>()
                .ReverseMap();

            CreateMap<ItineraryDto, ItineraryLogDto>()
                .ReverseMap();

            CreateMap<GendecDeparture, GendecDepartureDto>()
               .ReverseMap();

            CreateMap<GendecArrival, GendecArrivalDto>()
                .ReverseMap();

            CreateMap<ItinerarySearch, ItinerarySearchDto>()
                .ReverseMap();

            CreateMap<ManifestDeparture, ManifestDepartureDto>()
                .ForMember(c => c.ArrivalStationCode, f => f.MapFrom(w => w.ArrivalStation))
                .ForMember(c => c.DepartureStationCode, f => f.MapFrom(w => w.DepartureStation))
                .ForMember(c => c.ScaleStationCode, f => f.MapFrom(w => w.ScaleStation))
                .ForMember(c => c.UserIdAuthorize, f => f.MapFrom(w => w.UserAuthorizeId))
                .ForMember(c => c.UserIdSignature, f => f.MapFrom(w => w.UserSignatureId))
                .ForMember(c => c.InfantTickets, f => f.MapFrom(w => w.InfantsTickets))
                .ReverseMap();

            CreateMap<AdditionalDepartureInformation, AdditionalDepartureInformationDto>()
                .ReverseMap();

            CreateMap<AdditionalArrivalInformation, AdditionalArrivalInformationDto>()
                .ReverseMap();

            CreateMap<ManifestDepartureBoarding, ManifestDepartureBoardingDto>()
                .ReverseMap();

            CreateMap<ManifestDepartureBoardingDetail, ManifestDepartureBoardingDetailDto>()
                .ForMember(c => c.CompartmentTypeName, f => f.MapFrom(r => r.CompartmentTypeConfig.CompartmentTypeName))
                .ForMember(c => c.MaximumWeight, f => f.MapFrom(r => r.CompartmentTypeConfig.MaximumWeight))
                .ReverseMap();

            CreateMap<ManifestDepartureBoardingInformation, ManifestDepartureBoardingInformationDto>()
                .ForMember(c => c.CompartmentTypeInformationName, f => f.MapFrom(r => r.CompartmentTypeInformation.CompartmentTypeInformationName))
                .ForMember(c => c.CompartmentTypeName, f => f.MapFrom(r => r.CompartmentTypeConfig.CompartmentTypeName))
                .ReverseMap();

            CreateMap<ManifestArrival, ManifestArrivalDto>()
                .ForMember(c => c.ArrivalStationCode, f => f.MapFrom(w => w.ArrivalStation))
                .ForMember(c => c.DepartureStationCode, f => f.MapFrom(w => w.DepartureStation))
                .ForMember(c => c.LastScaleStationCode, f => f.MapFrom(w => w.LastScaleStation))
                .ForMember(c => c.UserIdAuthorize, f => f.MapFrom(w => w.UserAuthorizeId))
                .ForMember(c => c.UserIdSignature, f => f.MapFrom(w => w.UserSignatureId))
                .ReverseMap();

            CreateMap<TimelineDto, Timeline>()
                .ReverseMap();

            CreateMap<TimelineMovementDto, TimelineMovement>()
                .ReverseMap();
        }
    }
}
