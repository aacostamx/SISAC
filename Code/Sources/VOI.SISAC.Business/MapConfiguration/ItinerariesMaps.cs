//------------------------------------------------------------------------
// <copyright file="ItinerariesMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
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
        /// Maps this instance.
        /// </summary>
        private void Map()
        {
            Mapper.CreateMap<Itinerary, ItineraryDto>()
                .ForMember(c => c.GendecDepartureIsClose, f => f.MapFrom(r => r.GendecDepartures.Closed))
                .ForMember(c => c.GendecArrivalIsClose, f => f.MapFrom(r => r.GendecArrivals.Closed))
                .ReverseMap();

            Mapper.CreateMap<ItineraryLog, ItineraryLogDto>()
                .ReverseMap();

            Mapper.CreateMap<ItineraryDto, ItineraryLogDto>()
                .ReverseMap();

            Mapper.CreateMap<GendecDeparture, GendecDepartureDto>()
               .ReverseMap();

            Mapper.CreateMap<GendecArrival, GendecArrivalDto>()
                .ReverseMap();

            Mapper.CreateMap<ItinerarySearch, ItinerarySearchDto>()
                .ReverseMap();            

            Mapper.CreateMap<ManifestDeparture, ManifestDepartureDto>()
                .ForMember(c => c.ArrivalStationCode, f => f.MapFrom(w => w.ArrivalStation))
                .ForMember(c => c.DepartureStationCode, f => f.MapFrom(w => w.DepartureStation))
                .ForMember(c => c.ScaleStationCode, f => f.MapFrom(w => w.ScaleStation))
                .ForMember(c => c.UserIdAuthorize, f => f.MapFrom(w => w.UserAuthorizeId))
                .ForMember(c => c.UserIdSignature, f => f.MapFrom(w => w.UserSignatureId))
                .ForMember(c => c.InfantTickets, f => f.MapFrom(w => w.InfantsTickets))
                .ReverseMap();

            Mapper.CreateMap<ManifestDepartureDto, ManifestDeparture>()
                .ForMember(c => c.ArrivalStation, f => f.MapFrom(w => w.ArrivalStationCode))
                .ForMember(c => c.DepartureStation, f => f.MapFrom(w => w.DepartureStationCode))
                .ForMember(c => c.ScaleStation, f => f.MapFrom(w => w.ScaleStationCode))
                .ForMember(c => c.UserAuthorizeId, f => f.MapFrom(w => w.UserIdAuthorize))
                .ForMember(c => c.UserSignatureId, f => f.MapFrom(w => w.UserIdSignature))
                .ForMember(c => c.InfantsTickets, f => f.MapFrom(w => w.InfantTickets))
                .ReverseMap();

            Mapper.CreateMap<AdditionalDepartureInformation, AdditionalDepartureInformationDto>()
                .ReverseMap();

            Mapper.CreateMap<AdditionalArrivalInformation, AdditionalArrivalInformationDto>()
                .ReverseMap();

            Mapper.CreateMap<ManifestDepartureBoarding, ManifestDepartureBoardingDto>()
                .ReverseMap();

            Mapper.CreateMap<ManifestDepartureBoardingDetail, ManifestDepartureBoardingDetailDto>()
                .ForMember(c => c.CompartmentTypeName, f => f.MapFrom(r => r.CompartmentTypeConfig.CompartmentTypeName))
                .ReverseMap();

            Mapper.CreateMap<ManifestDepartureBoardingInformation, ManifestDepartureBoardingInformationDto>()
                .ForMember(c => c.CompartmentTypeInformationName, f => f.MapFrom(r => r.CompartmentTypeInformation.CompartmentTypeInformationName))
                .ForMember(c => c.CompartmentTypeName, f => f.MapFrom(r => r.CompartmentTypeConfig.CompartmentTypeName))
                .ReverseMap();

            Mapper.CreateMap<ManifestArrival, ManifestArrivalDto>()
                .ForMember(c => c.ArrivalStationCode, f => f.MapFrom(w => w.ArrivalStation))
                .ForMember(c => c.DepartureStationCode, f => f.MapFrom(w => w.DepartureStation))
                .ForMember(c => c.LastScaleStationCode, f => f.MapFrom(w => w.LastScaleStation))
                .ForMember(c => c.UserIdAuthorize, f => f.MapFrom(w => w.UserAuthorizeId))
                .ForMember(c => c.UserIdSignature, f => f.MapFrom(w => w.UserSignatureId))
                .ReverseMap();

            Mapper.CreateMap<ManifestArrivalDto, ManifestArrival>()
                .ForMember(c => c.ArrivalStation, f => f.MapFrom(w => w.ArrivalStationCode))
                .ForMember(c => c.DepartureStation, f => f.MapFrom(w => w.DepartureStationCode))
                .ForMember(c => c.LastScaleStation, f => f.MapFrom(w => w.LastScaleStationCode))
                .ForMember(c => c.UserAuthorizeId, f => f.MapFrom(w => w.UserIdAuthorize))
                .ForMember(c => c.UserSignatureId, f => f.MapFrom(w => w.UserIdSignature))
                .ReverseMap();

            Mapper.CreateMap<TimelineDto, Timeline>()
                .ReverseMap();

            Mapper.CreateMap<TimelineMovementDto, TimelineMovement>()
                .ReverseMap();
        }
    }
}
