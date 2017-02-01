//------------------------------------------------------------------------
// <copyright file="AirportMaps.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Airport Maps
    /// </summary>
    public class AirportMaps : Profile
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
                return "AirportMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Map();
            AirportMapping();
            AirportGroupMapping();
            GpuMapping();
            GpuObservationMapping();
            AirplaneMaps();
            CompartmentTypesMaps();
            AirplaneTypeMaps();
            DrinkingWaterMaps();
            AirportScheduleMaps();
            CrewTypeMap();
            CrewMap();
            AirlineMap();
            FuelConceptMaps();
            DelayMaps();
            ManifestTimeConfigMaps();
            FuelConceptTypeMaps();
            AirportServiceMaps();
            JetFuelTicketMaps();
            PassengerInformationMaps();
            NationalJetFuelTicketMaps();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportMaps"/> class.
        /// </summary>
        public AirportMaps()
        { }

        private void NationalJetFuelTicketMaps()
        {
            CreateMap<NationalJetFuelTicket, NationalJetFuelTicketDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Service Maps
        /// </summary>
        private void PassengerInformationMaps()
        {
            CreateMap<PassengerInformation, PassengerInformationDto>()
                .ReverseMap();
            CreateMap<AdditionalPassengerInformation, AdditionalPassengerInformationDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Airport Service Maps
        /// </summary>
        private void JetFuelTicketMaps()
        {
            CreateMap<JetFuelTicket, JetFuelTicketDto>()                
                .ReverseMap();
        }

        /// <summary>
        /// Airport Service Maps
        /// </summary>
        private void AirportServiceMaps()
        {
            CreateMap<AirportService, AirportServiceDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps Fuels concept type object.
        /// </summary>
        private void FuelConceptTypeMaps()
        {
            CreateMap<FuelConceptType, FuelConceptTypeDto>()
                .ForMember(t => t.FuelConceptTypeCode, f => f.MapFrom(r => r.FuelConceptTypeCode))
                .ForMember(t => t.FuelConceptTypeName, f => f.MapFrom(r => r.FuelConceptTypeName))
                .ForMember(t => t.InternationalFuelContractConcept, f => f.MapFrom(r => r.InternationalFuelContractConcept))
                .ReverseMap();
        }

        /// <summary>
        /// Configures the ManifestTimeConfig class
        /// </summary>
        private void ManifestTimeConfigMaps()
        {
            CreateMap<ManifestTimeConfig, ManifestTimeConfigDto>()
                .ForMember(t => t.ManifestTimeConfigID, f => f.MapFrom(r => r.ManifestTimeConfigID))
                .ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineCode))
                .ForMember(t => t.ArrivalMinutesMin, f => f.MapFrom(r => r.ArrivalMinutesMin))
                .ForMember(t => t.ArrivalMinutesMax, f => f.MapFrom(r => r.ArrivalMinutesMax))
                .ForMember(t => t.DepartureMinutesMin, f => f.MapFrom(r => r.DepartureMinutesMin))
                .ForMember(t => t.DepartureMinutesMax, f => f.MapFrom(r => r.DepartureMinutesMax))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();
        }

        /// <summary>
        /// Configures the Delay class
        /// </summary>
        private void DelayMaps()
        {
            CreateMap<Delay, DelayDto>()
                    .ForMember(t => t.DelayCode, f => f.MapFrom(r => r.DelayCode))
                    .ForMember(t => t.DelayName, f => f.MapFrom(r => r.DelayName))
                    .ForMember(t => t.FunctionalAreaID, f => f.MapFrom(r => r.FunctionalAreaID))
                    .ForMember(t => t.UnderControl, f => f.MapFrom(r => r.UnderControl))
                    .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                    .ReverseMap();
        }

        /// <summary>
        /// Configures the Gpu Observation class
        /// </summary>
        private void GpuObservationMapping()
        {
            CreateMap<GpuObservation, GpuObservationDto>()
                    .ForMember(t => t.GpuObservationCode, f => f.MapFrom(r => r.GpuObservationCode))
                    .ForMember(t => t.GpuObservationCodeName, f => f.MapFrom(r => r.GpuObservationCodeName))
                    .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                    .ReverseMap();
        }

        /// <summary>
        /// Configures the Airport Group Class
        /// </summary>
        private void AirportGroupMapping()
        {
            CreateMap<AirportGroup, AirportGroupDto>()
                .ForMember(t => t.AirportGroupCode, f => f.MapFrom(r => r.AirportGroupCode))
                .ForMember(t => t.AirportGroupName, f => f.MapFrom(r => r.AirportGroupName))
                .ReverseMap();
        }

        /// <summary>
        /// Configure the Airport Mapping Class
        /// </summary>
        private void AirportMapping()
        {
            CreateMap<Airport, AirportDto>()
                    .ForMember(t => t.StationCode, f => f.MapFrom(r => r.StationCode))
                    .ForMember(t => t.CountryCode, f => f.MapFrom(r => r.CountryCode))
                    .ForMember(t => t.OpeningTime, f => f.MapFrom(r => r.OpeningTime))
                    .ForMember(t => t.ClosingTime, f => f.MapFrom(r => r.ClosingTime))
                    .ForMember(t => t.AirportGroupCode, f => f.MapFrom(r => r.AirportGroupCode))
                    .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                    .ForMember(t => t.Country, f => f.MapFrom(r => r.Country))
                    .ForMember(t => t.Gpu, f => f.MapFrom(r => r.Gpu))

                    .ReverseMap();
        }

        /// <summary>
        /// Configures the Gpu classes
        /// </summary>
        private void GpuMapping()
        {
            CreateMap<Gpu, GpuDto>()
                .ForMember(t => t.GpuCode, f => f.MapFrom(r => r.GpuCode))
                .ForMember(t => t.GpuName, f => f.MapFrom(r => r.GpuName))
                .ForMember(t => t.StationCode, f => f.MapFrom(r => r.StationCode))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ForMember(t => t.Airport, f => f.MapFrom(r => r.Airport))
                .ReverseMap();
        }

        /// <summary>
        /// Configures the mappers between classes.
        /// </summary>
        private void Map()
        {
            /*
             * En esta sección se registran las conversiones que realizará Auto
             * 
             * Automapper convierte de manera automática las conversiones
             * entre objetos que sus propiedades tienen el mismo nombre
             * y tipo de dato. Para las propiedades que no coinciden,
             * es necesario hacer la conversión de manera explícita.
             * 
             * El siguiente código ilustra la manera de hacer las conversiones
             * de manera explísita.
            */

            // ********* Seccion de Servicios ************ //
            CreateMap<Service, ServiceDto>()
                .ForMember(s => s.ServiceCode, f => f.MapFrom(r => r.ServiceCode))
                .ForMember(s => s.ServiceName, f => f.MapFrom(r => r.ServiceName))
                .ForMember(s => s.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();

            CreateMap<AirportService, AirportServiceDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Compartment types maps.
        /// </summary>
        private void CompartmentTypesMaps()
        {
            CreateMap<CompartmentType, CompartmentTypeDto>()
                .ForMember(t => t.CompartmentTypeCode, f => f.MapFrom(r => r.CompartmentTypeCode))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ForMember(t => t.AirplaneTypes, f => f.MapFrom(r => r.AirplaneTypes))
                .ReverseMap();
        }

        /// <summary>
        /// Airplanes maps.
        /// </summary>
        private void AirplaneMaps()
        {
            /*
             * En caso que se tengan colecciones dentro de nuestras entidades
             * que se necesiten mapear entre ellas y ya se ha especificado 
             * su conversión, se puede realizar la conversión de la siguiente manera:
             *   
             *  .ForMember(t => t.Requisitos, f => f.MapFrom(c => c.Requisitos))
             */

            CreateMap<Airplane, AirplaneDto>()
                .ForMember(t => t.EquipmentNumber, f => f.MapFrom(r => r.EquipmentNumber))
                .ForMember(t => t.AirplaneModel, f => f.MapFrom(r => r.AirplaneModel))
                .ForMember(t => t.MaximumTakeoffWeight, f => f.MapFrom(r => r.MaximumTakeoffWeight))
                .ForMember(t => t.WeightInPound, f => f.MapFrom(r => r.WeightInPound))
                .ForMember(t => t.WeightInTonnes, f => f.MapFrom(r => r.WeightInTonnes))
                .ForMember(t => t.EmptyOperatingWeight, f => f.MapFrom(r => r.EmptyOperatingWeight))
                .ForMember(t => t.FilmingMaximumWeight, f => f.MapFrom(r => r.FilmingMaximumWeight))
                .ForMember(t => t.TakeoffWeightInTonnes, f => f.MapFrom(r => r.TakeoffWeightInTonnes))
                .ForMember(t => t.GroupWeight, f => f.MapFrom(r => r.GroupWeight))
                .ForMember(t => t.MaximumLandingWeight, f => f.MapFrom(r => r.MaximumLandingWeight))
                .ForMember(t => t.MaximumZeroFuelWeight, f => f.MapFrom(r => r.MaximumZeroFuelWeight))
                .ForMember(t => t.PassengerCapacity, f => f.MapFrom(r => r.PassengerCapacity))
                .ForMember(t => t.CrewCapacity, f => f.MapFrom(r => r.CrewCapacity))
                .ForMember(t => t.Magnitude, f => f.MapFrom(r => r.Magnitude))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ForMember(t => t.SerialNumber, f => f.MapFrom(r => r.SerialNumber))
                .ReverseMap();
        }

        /// <summary>
        /// Airplane types maps.
        /// </summary>
        private void AirplaneTypeMaps()
        {
            // Para convertir de un AirplaneType a un AirplaneTypeDto,
            // se hará de la siguiente manera:
            CreateMap<AirplaneType, AirplaneTypeDto>()

                // t: Entidad destino (AirplaneTypeDto).
                // f: Etindad origen (AirplaneType).
                // r: Entidad origen (AirplaneType).
                // La propiedad AirplaneModel de la entidad destino,
                // sera igual a la propiedad AirplaneModel de la entidad origen.
                // Y así para cada una de las propiedades.
                .ForMember(t => t.AirplaneModel, f => f.MapFrom(r => r.AirplaneModel))
                .ForMember(t => t.CompartmentTypeCode, f => f.MapFrom(r => r.CompartmentTypeCode))
                .ForMember(t => t.MaximumTakeoffWeight, f => f.MapFrom(r => r.MaximumTakeoffWeight))
                .ForMember(t => t.WeightInPound, f => f.MapFrom(r => r.WeightInPound))
                .ForMember(t => t.WeightInTonnes, f => f.MapFrom(r => r.WeightInTonnes))
                .ForMember(t => t.EmptyOperatingWeight, f => f.MapFrom(r => r.EmptyOperatingWeight))
                .ForMember(t => t.FilmingMaximumWeight, f => f.MapFrom(r => r.FilmingMaximumWeight))
                .ForMember(t => t.TakeoffWeightInTonnes, f => f.MapFrom(r => r.TakeoffWeightInTonnes))
                .ForMember(t => t.GroupWeight, f => f.MapFrom(r => r.GroupWeight))
                .ForMember(t => t.MaximumLandingWeight, f => f.MapFrom(r => r.MaximumLandingWeight))
                .ForMember(t => t.MaximumZeroFuelWeight, f => f.MapFrom(r => r.MaximumZeroFuelWeight))
                .ForMember(t => t.PassengerCapacity, f => f.MapFrom(r => r.PassengerCapacity))
                .ForMember(t => t.CrewCapacity, f => f.MapFrom(r => r.CrewCapacity))
                .ForMember(t => t.Magnitude, f => f.MapFrom(r => r.Magnitude))
                .ForMember(t => t.FuelInLiters, f => f.MapFrom(r => r.FuelInLiters))
                .ForMember(t => t.FuelInKg, f => f.MapFrom(r => r.FuelInKg))
                .ForMember(t => t.FuelInGallon, f => f.MapFrom(r => r.FuelInGallon))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                .ForMember(t => t.Airplanes, f => f.MapFrom(r => r.Airplanes))

                // La conversión sera también en doble vía de CurrencyDto a Currency.
                .ReverseMap();
        }

        /// <summary>
        /// Crews the map.
        /// </summary>
        private void CrewMap()
        {
            CreateMap<Crew, CrewDto>()
                .ForMember(t => t.CrewID, f => f.MapFrom(r => r.CrewID))
                .ForMember(t => t.CrewTypeID, f => f.MapFrom(r => r.CrewTypeID))
                .ForMember(t => t.LastName, f => f.MapFrom(r => r.LastName))
                .ForMember(t => t.FirstName, f => f.MapFrom(r => r.FirstName))
                .ForMember(t => t.MiddleName, f => f.MapFrom(r => r.MiddleName))
                .ForMember(t => t.Gender, f => f.MapFrom(r => r.Gender))
                .ForMember(t => t.CountryOfResidence, f => f.MapFrom(r => r.CountryOfResidence))
                .ForMember(t => t.PlaceBirthCity, f => f.MapFrom(r => r.PlaceBirthCity))
                .ForMember(t => t.State, f => f.MapFrom(r => r.State))
                .ForMember(t => t.CountryOfBird, f => f.MapFrom(r => r.CountryOfBird))
                .ForMember(t => t.DateOfBird, f => f.MapFrom(r => r.DateOfBird))
                .ForMember(t => t.Citizenship, f => f.MapFrom(r => r.Citizenship))
                .ForMember(t => t.StatusOnBoardCode, f => f.MapFrom(r => r.StatusOnBoardCode))
                .ForMember(t => t.HomeAddress, f => f.MapFrom(r => r.HomeAddress))
                .ForMember(t => t.HomeAddressCity, f => f.MapFrom(r => r.HomeAddressCity))
                .ForMember(t => t.HomeAddressState, f => f.MapFrom(r => r.HomeAddressState))
                .ForMember(t => t.HomeAddressZipCode, f => f.MapFrom(r => r.HomeAddressZipCode))
                .ForMember(t => t.HomeAddressCountry, f => f.MapFrom(r => r.HomeAddressCountry))
                .ForMember(t => t.PassportNumber, f => f.MapFrom(r => r.PassportNumber))
                .ForMember(t => t.PassportCountryIssuance, f => f.MapFrom(r => r.PassportCountryIssuance))
                .ForMember(t => t.PassportExpiration, f => f.MapFrom(r => r.PassportExpiration))
                .ForMember(t => t.LicenceNumber, f => f.MapFrom(r => r.LicenceNumber))
                .ForMember(t => t.LicenceCountryIssuance, f => f.MapFrom(r => r.LicenceCountryIssuance))
                .ForMember(t => t.LicenceNumberExpiration, f => f.MapFrom(r => r.LicenceNumberExpiration))
                .ForMember(t => t.NickName, f => f.MapFrom(r => r.NickName))
                .ForMember(t => t.NickNameSabre, f => f.MapFrom(r => r.NickNameSabre))
                .ForMember(t => t.VisaExpirationDate, f => f.MapFrom(r => r.VisaExpirationDate))
                .ForMember(t => t.EmployeeNumber, f => f.MapFrom(r => r.EmployeeNumber))
                .ForMember(t => t.CreatedDate, f => f.MapFrom(r => r.CreatedDate))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();
        }

        /// <summary>
        /// Airlines the map.
        /// </summary>
        private void AirlineMap()
        {
            // Se realiza el mapeo de Airline a AirlineDto             
            CreateMap<Airline, AirlineDto>()

                // t: Entidad destino (AirlineDto).
                // f: Entidad origen (Airline).
                // r: Entidad origen (Airline).
                // La propiedad AirlineCode de la entidad destino,
                // sera igual a la propiedad AirlineCode de la entidad origen.
                .ForMember(t => t.AirlineCode, f => f.MapFrom(r => r.AirlineCode))
                .ForMember(t => t.AirlineName, f => f.MapFrom(r => r.AirlineName))
                .ForMember(t => t.AirlineShortName, f => f.MapFrom(r => r.AirlineShortName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))

                // La conversión sera también en doble vía de AirlineDto a Airline.
                .ReverseMap();
        }

        /// <summary>
        /// Crews the type map.
        /// </summary>
        private void CrewTypeMap()
        {
            // Se realiza el mapeo de Airline a AirlineDto             
            CreateMap<CrewType, CrewTypeDto>()

                // t: Entidad destino (CrewTypeDto).
                // f: Entidad origen (CrewType).
                // r: Entidad origen (CrewType).
                // La propiedad CrewTypeID de la entidad destino,
                // sera igual a la propiedad CrewTypeID de la entidad origen.
                .ForMember(t => t.CrewTypeID, f => f.MapFrom(r => r.CrewTypeID))
                .ForMember(t => t.CrewTypeName, f => f.MapFrom(r => r.CrewTypeName))

                // La conversión sera también en doble vía de CrewTypeDto a CrewType.
                .ReverseMap();
        }

        /// <summary>
        /// Maps Drinking water objects.
        /// </summary>
        private void DrinkingWaterMaps()
        {
            CreateMap<DrinkingWater, DrinkingWaterDto>()
                .ForMember(t => t.DrinkingWaterId, f => f.MapFrom(r => r.DrinkingWaterId))
                .ForMember(t => t.EquipmentNumber, f => f.MapFrom(r => r.EquipmentNumber))
                .ForMember(t => t.DrinkingWaterName, f => f.MapFrom(r => r.DrinkingWaterName))
                .ForMember(t => t.Value, f => f.MapFrom(r => r.Value))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();
        }

        private void AirportScheduleMaps()
        {
            CreateMap<AirportSchedule, AirportScheduleDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps Fuel concept objects.
        /// </summary>
        private void FuelConceptMaps()
        {
            CreateMap<FuelConcept, FuelConceptDto>()
                .ForMember(t => t.FuelConceptID, f => f.MapFrom(r => r.FuelConceptID))
                .ForMember(t => t.FuelConceptName, f => f.MapFrom(r => r.FuelConceptName))
                .ForMember(t => t.Status, f => f.MapFrom(r => r.Status))
                .ReverseMap();
        }
    }
}