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

        protected override void Configure()
        {
            Map();
            GendecMap();
            ManifesDepartureMap();
            ManifesArrivalMap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryWebMaps"/> class.
        /// </summary>
        public ItineraryWebMaps()
        {
            
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            CreateMap<ItineraryDto, ItineraryVO>()
                .ReverseMap();

            CreateMap<ItineraryLogDto, ItineraryLogVO>()
                .ReverseMap();

            CreateMap<ItineraryFile, ItineraryDto>()
                .ReverseMap();


            CreateMap<ItinerarySearchDto, ItinerarySearchVO>()
                .ReverseMap();

            CreateMap<ItineraryUploadDto, ItineraryUploadVO>()
                .ReverseMap();

            CreateMap<TimelineDto, TimelineVO>()
                .ForMember(c => c.StationCode, f => f.MapFrom(w => w.Itinerary.DepartureStation))
                .ForMember(c => c.EquipmentNumber, f => f.MapFrom(w => w.Itinerary.EquipmentNumber))
                .ForMember(c => c.TimelineMovements, f => f.MapFrom(w => w.TimelineMovements.Where(c => c.AirlineCode == w.AirlineCode && c.Sequence == w.Sequence && c.FlightNumber == w.FlightNumber && c.ItineraryKey == w.ItineraryKey)))
                .ReverseMap();

            CreateMap<TimelineMovementDto, TimelineMovementVO>()
                .ForMember(c => c.OperationDescription, f => f.MapFrom(w => w.OperationType.OperationName))
                .ForMember(c => c.MovementTypeDescription, f => f.MapFrom(w => w.MovementType.MovementDescription))
                .ForMember(c => c.ProviderName, f => f.MapFrom(w => w.Provider.ProviderName))
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void GendecMap()
        {
            CreateMap<GendecDepartureDto, GendecDepartureVO>()
               .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber))
               .ForMember(p => p.DepartureDate, r => r.MapFrom(s => s.Itinerary.DepartureDate.ToShortDateString()))
               .ForMember(p => p.TimeDeparture, r => r.MapFrom(s => s.Itinerary.DepartureDate.Hour + ":" + s.Itinerary.DepartureDate.Minute))
               .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
               .ForMember(p => p.DepartureStation, r => r.MapFrom(s => s.Itinerary.DepartureStation))
               .ForMember(p => p.ArrivalStation, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
               .ReverseMap();

            CreateMap<GendecArrivalDto, GendecArrivalVO>()
               .ForMember(p => p.EquipmentNumber, r => r.MapFrom(s => s.Itinerary.EquipmentNumber))
               .ForMember(p => p.ArrivalDate, r => r.MapFrom(s => s.Itinerary.ArrivalDate.ToShortDateString()))
               .ForMember(p => p.TimeArrival, r => r.MapFrom(s => s.Itinerary.ArrivalDate.Hour + ":" + s.Itinerary.ArrivalDate.Minute))
               .ForMember(p => p.AirplaneModel, r => r.MapFrom(s => s.Itinerary.Airplane.AirplaneModel))
               .ForMember(p => p.DepartureStation, r => r.MapFrom(s => s.Itinerary.DepartureStation))
               .ForMember(p => p.ArrivalStation, r => r.MapFrom(s => s.Itinerary.ArrivalStation))
               .ReverseMap();

            CreateMap<GendecDepartureDto, GendecArrivalVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void ManifesDepartureMap()
        {
            CreateMap<ManifestDepartureDto, ManifestDepartureVO>()
                .ForMember(p => p.Pilot, r => r.MapFrom(s => s.AdditionalDepartureInformation.Pilot))
                .ForMember(p => p.Surcharge, r => r.MapFrom(s => s.AdditionalDepartureInformation.Surcharge))
                .ForMember(p => p.ExtraCrew, r => r.MapFrom(s => s.AdditionalDepartureInformation.ExtraCrew))
                .ForMember(p => p.SlotAllocatedTime, r => r.MapFrom(s => s.AdditionalDepartureInformation.SlotAllocatedTime))
                .ForMember(p => p.SlotCoordinatedTime, r => r.MapFrom(s => s.AdditionalDepartureInformation.SlotCoordinatedTime))
                .ForMember(p => p.ManeuverStartTime, r => r.MapFrom(s => s.AdditionalDepartureInformation.ManeuverStartTime))
                .ForMember(p => p.OvernightEndTime, r => r.MapFrom(s => s.AdditionalDepartureInformation.OvernightEndTime))
                .ForMember(p => p.PositionOutputTime, r => r.MapFrom(s => s.AdditionalDepartureInformation.PositionOutputTime))
                .ForMember(p => p.TypeFlight, r => r.MapFrom(s => s.AdditionalDepartureInformation.TypeFlight))
                .ForMember(p => p.FirstDelayDescription, r => r.MapFrom(s => s.AdditionalDepartureInformation.DelayDescription1))
                .ForMember(p => p.SecondDelayDescription, r => r.MapFrom(s => s.AdditionalDepartureInformation.DelayDescription2))
                .ForMember(p => p.ThirdDelayDescription, r => r.MapFrom(s => s.AdditionalDepartureInformation.DelayDescription3))
                .ReverseMap();

            CreateMap<ManifestDepartureVO, AdditionalDepartureInformationDto>()
                .ForMember(p => p.Pilot, r => r.MapFrom(s => s.Pilot))
                .ForMember(p => p.Surcharge, r => r.MapFrom(s => s.Surcharge))
                .ForMember(p => p.ExtraCrew, r => r.MapFrom(s => s.ExtraCrew))
                .ForMember(p => p.SlotAllocatedTime, r => r.MapFrom(s => s.SlotAllocatedTime))
                .ForMember(p => p.SlotCoordinatedTime, r => r.MapFrom(s => s.SlotCoordinatedTime))
                .ForMember(p => p.ManeuverStartTime, r => r.MapFrom(s => s.ManeuverStartTime))
                .ForMember(p => p.OvernightEndTime, r => r.MapFrom(s => s.OvernightEndTime))
                .ForMember(p => p.PositionOutputTime, r => r.MapFrom(s => s.PositionOutputTime))
                .ForMember(p => p.TypeFlight, r => r.MapFrom(s => s.TypeFlight))
                .ForMember(p => p.DelayDescription1, r => r.MapFrom(s => s.FirstDelayDescription))
                .ForMember(p => p.DelayDescription2, r => r.MapFrom(s => s.SecondDelayDescription))
                .ForMember(p => p.DelayDescription3, r => r.MapFrom(s => s.ThirdDelayDescription))
                .ReverseMap();

            CreateMap<ManifestDepartureBoardingDto, ManifestDepartureBoardingVO>()
                .ReverseMap();

            CreateMap<ManifestDepartureBoardingDetailDto, ManifestDepartureBoardingDetailVO>()
                .ReverseMap();

            CreateMap<ManifestDepartureBoardingInformationDto, ManifestDepartureBoardingInformationVO>()
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void ManifesArrivalMap()
        {
            CreateMap<ManifestArrivalVO, AdditionalArrivalInformationDto>()
                .ForMember(p => p.Pilot, r => r.MapFrom(s => s.Pilot))
                .ForMember(p => p.Surcharge, r => r.MapFrom(s => s.Surcharge))
                .ForMember(p => p.ExtraCrew, r => r.MapFrom(s => s.ExtraCrew))
                .ForMember(p => p.SlotAllocatedTime, r => r.MapFrom(s => s.SlotAllocatedTime))
                .ForMember(p => p.SlotCoordinatedTime, r => r.MapFrom(s => s.SlotCoordinatedTime))
                .ForMember(p => p.ManeuverStartTime, r => r.MapFrom(s => s.ManeuverStartTime))
                .ForMember(p => p.OvernightEndTime, r => r.MapFrom(s => s.OvernightEndTime))
                .ForMember(p => p.PositionOutputTime, r => r.MapFrom(s => s.PositionOutputTime))
                .ForMember(p => p.TypeFlight, r => r.MapFrom(s => s.TypeFlight))
                .ForMember(p => p.DelayDescription1, r => r.MapFrom(s => s.FirstDelayDescription))
                .ForMember(p => p.DelayDescription2, r => r.MapFrom(s => s.SecondDelayDescription))
                .ForMember(p => p.DelayDescription3, r => r.MapFrom(s => s.ThirdDelayDescription))
                .ReverseMap();

            CreateMap<ManifestArrivalDto, ManifestArrivalVO>()
                .ForMember(c => c.LastStationCode, f => f.MapFrom(w => w.LastScaleStationCode))
                .ForMember(p => p.Pilot, r => r.MapFrom(s => s.AdditionalArrivalInformation.Pilot))
                .ForMember(p => p.Surcharge, r => r.MapFrom(s => s.AdditionalArrivalInformation.Surcharge))
                .ForMember(p => p.ExtraCrew, r => r.MapFrom(s => s.AdditionalArrivalInformation.ExtraCrew))
                .ForMember(p => p.SlotAllocatedTime, r => r.MapFrom(s => s.AdditionalArrivalInformation.SlotAllocatedTime))
                .ForMember(p => p.SlotCoordinatedTime, r => r.MapFrom(s => s.AdditionalArrivalInformation.SlotCoordinatedTime))
                .ForMember(p => p.ManeuverStartTime, r => r.MapFrom(s => s.AdditionalArrivalInformation.ManeuverStartTime))
                .ForMember(p => p.OvernightEndTime, r => r.MapFrom(s => s.AdditionalArrivalInformation.OvernightEndTime))
                .ForMember(p => p.PositionOutputTime, r => r.MapFrom(s => s.AdditionalArrivalInformation.PositionOutputTime))
                .ForMember(p => p.TypeFlight, r => r.MapFrom(s => s.AdditionalArrivalInformation.TypeFlight))
                .ForMember(p => p.FirstDelayDescription, r => r.MapFrom(s => s.AdditionalArrivalInformation.DelayDescription1))
                .ForMember(p => p.SecondDelayDescription, r => r.MapFrom(s => s.AdditionalArrivalInformation.DelayDescription2))
                .ForMember(p => p.ThirdDelayDescription, r => r.MapFrom(s => s.AdditionalArrivalInformation.DelayDescription3))
                .ReverseMap();

            CreateMap<ManifestArrivalVO, ManifestArrivalDto>()
                .ForMember(c => c.LastScaleStationCode, f => f.MapFrom(w => w.LastStationCode))
                .ReverseMap();
        }
    }
}