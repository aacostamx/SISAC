//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Business logic operations
    /// </summary>
    public class ManifestDepartureBusiness : IManifestDepartureBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The manifest repository
        /// </summary>
        private readonly IManifestDepartureRepository manifestRepository;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// The itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// The time configuration repository
        /// </summary>
        private readonly IManifestTimeConfigRepository timeConfigRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="timeConfigRepository">The time configuration repository.</param>
        public ManifestDepartureBusiness(
            IUnitOfWork unitOfWork,
            IManifestDepartureRepository manifestRepository,
            IAirportRepository airportRepository,
            IItineraryRepository itineraryRepository,
            IManifestTimeConfigRepository timeConfigRepository)
        {
            this.unitOfWork = unitOfWork;
            this.manifestRepository = manifestRepository;
            this.airportRepository = airportRepository;
            this.itineraryRepository = itineraryRepository;
            this.timeConfigRepository = timeConfigRepository;
        }

        /// <summary>
        /// Gets the manifest departure for flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// The manifest departure for the flight.
        /// </returns>
        public ManifestDepartureDto GetManifestDepartureForFlight(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            try
            {
                string airportName = string.Empty;
                ManifestDepartureDto manifestDto = new ManifestDepartureDto();
                ManifestDeparture entity = this.manifestRepository.GetManifestDepartureForItinerary(sequence, airlineCode, flightNumber, itineraryKey);

                // If the manifest does not exist
                if (entity == null)
                {
                    // Gets the itinerary information
                    Itinerary itinerary = this.itineraryRepository.GetItineraryWithManifestsInformation(sequence, airlineCode, flightNumber, itineraryKey);
                    if (itinerary != null)
                    {
                        bool isNational;

                        // Sets information from the itinerary
                        ItineraryDto itineraryDto = Mapper.Map<ItineraryDto>(itinerary);
                        manifestDto.AirlineCode = itinerary.AirlineCode;
                        manifestDto.FlightNumber = itinerary.FlightNumber;
                        manifestDto.ItineraryKey = itinerary.ItineraryKey;
                        manifestDto.Sequence = itinerary.Sequence;
                        manifestDto.ArrivalStationName = this.airportRepository.GetAirportName(itinerary.ArrivalStation);
                        manifestDto.DepartureStationName = this.airportRepository.GetAirportName(itinerary.DepartureStation);
                        manifestDto.ArrivalStationCode = itinerary.ArrivalStation;
                        manifestDto.DepartureStationCode = itinerary.DepartureStation;
                        manifestDto.ScheduledDepartureTime = itinerary.DepartureDate.TimeOfDay.ToString(@"hh\:mm");
                        manifestDto.ScheduledDepartureDate = itinerary.DepartureDate.ToString("yyyy/MM/dd");

                        // Find if the airport is national or not.
                        isNational = this.airportRepository.FindById(itinerary.DepartureStation).Country.CountryCode == "MEX";

                        // Sets the passenger information
                        manifestDto = this.SetPassengerInformation(manifestDto, itineraryDto.PassengerInformation, isNational);
                        manifestDto.Itinerary = itineraryDto;
                    }
                    else
                    {
                        // The itinerary does not exist
                        manifestDto.Itinerary = new ItineraryDto();
                    }
                }
                else
                {
                    bool isNational;

                    // The manifest exits and proceeds to set the information from the entity
                    manifestDto = Mapper.Map<ManifestDepartureDto>(entity);
                    manifestDto.ArrivalStationName = this.airportRepository.GetAirportName(manifestDto.ArrivalStationCode);
                    manifestDto.DepartureStationName = this.airportRepository.GetAirportName(manifestDto.DepartureStationCode);
                    manifestDto.ScaleStationName = this.airportRepository.GetAirportName(manifestDto.ScaleStationCode);
                    manifestDto.ActualDepartureTime = entity.ActualDepartureDate.TimeOfDay.ToString(@"hh\:mm");
                    manifestDto.ActualDepartureDate = entity.ActualDepartureDate.ToString("yyyy/MM/dd");
                    manifestDto.ScheduledDepartureTime = entity.Itinerary.DepartureDate.TimeOfDay.ToString(@"hh\:mm");
                    manifestDto.ScheduledDepartureDate = entity.Itinerary.DepartureDate.ToString("yyyy/MM/dd");

                    // Find if the airport is national or not.
                    isNational = this.airportRepository.FindById(manifestDto.DepartureStationCode).Country.CountryCode == "MEX";

                    // Sets the passenger information
                    manifestDto = this.SetPassengerInformation(manifestDto, manifestDto.Itinerary.PassengerInformation, isNational);
                }

                ManifestTimeConfig time = this.timeConfigRepository.FindTimeConfigurationByAirline(airlineCode);
                manifestDto.ToleranceTime = time != null ? time.ArrivalMinutesMax : 0;
                return manifestDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Saves the manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        public bool SaveManifestDeparture(ManifestDepartureDto manifestDeparture)
        {
            if (manifestDeparture == null)
            {
                return false;
            }

            try
            {
                ManifestDeparture entity = this.manifestRepository.GetManifestDepartureForItinerary(
                    manifestDeparture.Sequence,
                    manifestDeparture.AirlineCode,
                    manifestDeparture.FlightNumber,
                    manifestDeparture.ItineraryKey);

                // Manifest does not exist and create a new one for the itinerary
                if (entity == null)
                {
                    DateTime actualDepartureDate;
                    entity = Mapper.Map<ManifestDeparture>(manifestDeparture);
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestDeparture.Delays);
                    entity.CreationDate = DateTime.Now;
                    DateTime.TryParse(manifestDeparture.ActualDepartureDate + " " + manifestDeparture.ActualDepartureTime, out actualDepartureDate);
                    entity.ActualDepartureDate = actualDepartureDate != default(DateTime) ? actualDepartureDate : new DateTime();
                    entity.Status = string.Empty;
                    entity.Delays = Mapper.Map<IList<Delay>>(manifestDeparture.Delays);
                    entity.Delays = null;
                    this.manifestRepository.Add(entity, delays);
                }
                else
                {
                    // The manifest exists and proceed to update it.
                    DateTime actualDepartureDate;
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestDeparture.Delays);
                    this.SetInformationInEntity(entity, manifestDeparture);
                    DateTime.TryParse(manifestDeparture.ActualDepartureDate + " " + manifestDeparture.ActualDepartureTime, out actualDepartureDate);
                    entity.ActualDepartureDate = actualDepartureDate != default(DateTime) ? actualDepartureDate : new DateTime();
                    this.manifestRepository.RemoveAllDelaysFromManifest(entity);
                    this.manifestRepository.Update(entity, delays);
                }

                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>List of delays in the manifest departure.</returns>
        public IList<DelayDto> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                || string.IsNullOrWhiteSpace(flightNumber)
                || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            try
            {
                List<Delay> delays = this.manifestRepository.GetDelaysForManifest(sequence, airlineCode, flightNumber, itineraryKey).ToList();
                return Mapper.Map<List<DelayDto>>(delays);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Closes the manifest.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns>
        ///   <c>true</c> if the operation was success otherwise <c>false</c>.
        /// </returns>
        public bool CloseManifest(ManifestDepartureDto manifestDeparture)
        {
            if (manifestDeparture == null 
                || string.IsNullOrWhiteSpace(manifestDeparture.AirlineCode)
                || string.IsNullOrWhiteSpace(manifestDeparture.FlightNumber)
                || string.IsNullOrWhiteSpace(manifestDeparture.ItineraryKey))
            {
                return false;
            }

            try
            {
                ManifestDeparture entity = this.manifestRepository.GetManifestDepartureForItinerary(
                    manifestDeparture.Sequence,
                    manifestDeparture.AirlineCode,
                    manifestDeparture.FlightNumber,
                    manifestDeparture.ItineraryKey);

                if (entity == null)
                {
                    DateTime actualDepartureDate;
                    entity = Mapper.Map<ManifestDeparture>(manifestDeparture);
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestDeparture.Delays);
                    entity.CreationDate = DateTime.Now;
                    DateTime.TryParse(manifestDeparture.ActualDepartureDate + " " + manifestDeparture.ActualDepartureTime, out actualDepartureDate);
                    entity.ActualDepartureDate = actualDepartureDate != default(DateTime) ? actualDepartureDate : new DateTime();
                    entity.Status = string.Empty;
                    entity.Closed = true;
                    entity.Delays = Mapper.Map<IList<Delay>>(manifestDeparture.Delays);
                    entity.Delays = null;
                    this.manifestRepository.Add(entity, delays);
                }
                else
                {
                    entity.Closed = true;
                    this.manifestRepository.Update(entity);
                }

                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Opens the manifest.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns>
        ///   <c>true</c> if the operation was success otherwise <c>false</c>.
        /// </returns>
        public bool OpenManifest(ManifestDepartureDto manifestDeparture)
        {
            if (manifestDeparture == null
                || string.IsNullOrWhiteSpace(manifestDeparture.AirlineCode)
                || string.IsNullOrWhiteSpace(manifestDeparture.FlightNumber)
                || string.IsNullOrWhiteSpace(manifestDeparture.ItineraryKey))
            {
                return false;
            }

            try
            {
                ManifestDeparture entity = this.manifestRepository.GetManifestDepartureForItinerary(
                    manifestDeparture.Sequence,
                    manifestDeparture.AirlineCode,
                    manifestDeparture.FlightNumber,
                    manifestDeparture.ItineraryKey);

                if (entity == null)
                {
                    throw new BusinessException(Messages.FailedFindRecord);
                }

                entity.Closed = false;
                this.manifestRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Sets the passenger information in the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <param name="passengerInformation">The passenger information.</param>
        /// <param name="isNational">if set to <c>true</c> [is national].</param>
        /// <returns>Returns the manifest departure with the passenger information from the itinerary.</returns>
        private ManifestDepartureDto SetPassengerInformation(ManifestDepartureDto manifest, PassengerInformationDto passengerInformation, bool isNational)
        {
            if (passengerInformation == null)
            {
                return manifest;
            }

            int innerSection;
            int transit;
            int exempt;

            innerSection = passengerInformation.LocalTeenage + passengerInformation.LocalAdults - passengerInformation.InternationalTua;
            transit = passengerInformation.TransitoryAdults + passengerInformation.TransitoryChildren + passengerInformation.TransitoryTeenage;
            exempt = passengerInformation.ConnectionAdults 
                + passengerInformation.ConnectionChildren 
                + passengerInformation.ConnectionTeenage
                + passengerInformation.Diplomatic
                + passengerInformation.ExtraCrew 
                + passengerInformation.Other;

            manifest.Infant = passengerInformation.LocalChildren;
            manifest.International = passengerInformation.InternationalTua;
            manifest.Transit = transit;
            manifest.InnerSection = innerSection;

            if (isNational)
            {
                manifest.NationalExempt = exempt;
            }
            else
            {
                manifest.InternationalExempt = exempt;
            }

            return manifest;
        }

        /// <summary>
        /// Sets the information in entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="dto">The data transfer object.</param>
        private void SetInformationInEntity(ManifestDeparture entity, ManifestDepartureDto dto)
        {
            entity.NickNameCommander = dto.NickNameCommander;
            entity.NickNameFirstOfficial = dto.NickNameFirstOfficial;
            entity.NickNameSecondOfficial = dto.NickNameSecondOfficial;
            entity.NickNameThirdOfficial = dto.NickNameThirdOfficial;
            entity.NickNameChiefCabinet = dto.NickNameChiefCabinet;
            entity.NickNameFirstSupercargo = dto.NickNameFirstSupercargo;
            entity.NickNameSecondSupercargo = dto.NickNameSecondSupercargo;
            entity.NickNameThirdSupercargo = dto.NickNameThirdSupercargo;
            entity.SupercargoRemarks = dto.SupercargoRemarks;
            entity.DepartureStation = dto.DepartureStationCode;
            entity.ScaleStation = dto.ScaleStationCode;
            entity.ArrivalStation = dto.ArrivalStationCode;
            entity.DelayRemarks = dto.DelayRemarks;
            entity.InnerSection = dto.InnerSection;
            entity.International = dto.International;
            entity.InternationalExempt = dto.InternationalExempt;
            entity.NationalExempt = dto.NationalExempt;
            entity.Transit = dto.Transit;
            entity.Infant = dto.Infant;
            entity.JetFuel = dto.JetFuel;
            entity.RealTakeoffWeight = dto.RealTakeoffWeight;
            entity.OperatingWeight = dto.OperatingWeight;
            entity.SafetyMargin = dto.SafetyMargin;
            entity.StructuralTakeoffWeight = dto.StructuralTakeoffWeight;
            entity.UserAuthorizeId = dto.UserIdAuthorize;
            entity.UserSignatureId = dto.UserIdSignature;
            entity.LicenceNumberAuthorize = dto.LicenceNumberAuthorize;
            entity.LicenceNumberSignature = dto.LicenceNumberSignature;
            entity.Position = dto.Position;
            entity.InfantsTickets = dto.InfantTickets;
            entity.Remarks = dto.Remarks;
        }
    }
}
