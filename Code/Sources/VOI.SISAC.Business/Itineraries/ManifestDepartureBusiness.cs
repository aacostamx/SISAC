//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
        /// The manifest boarding repository
        /// </summary>
        private readonly IManifestDepartureBoardingRepository manifestBoardingRepository;

        /// <summary>
        /// The manifest boarding repository
        /// </summary>
        private readonly IManifestDepartureBoardingInformationRepository manifestBoardingInformationRepository;

        /// <summary>
        /// The manifest boarding detail repository
        /// </summary>
        private readonly IManifestDepartureBoardingDetailRepository manifestBoardingDetailRepository;

        /// <summary>
        /// The additional departure information repository
        /// </summary>
        private readonly IAdditionalDepartureInformationRepository additionalDepartureInformationRepository;

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
        /// Initializes a new instance of the <see cref="ManifestDepartureBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="timeConfigRepository">The time configuration repository.</param>
        /// <param name="manifestBoardingRepository">The manifest boarding repository.</param>
        /// <param name="additionalDepartureInformationRepository">The additional departure information repository.</param>
        /// <param name="manifestBoardingInformationRepository">The manifest boarding information repository.</param>
        /// <param name="manifestBoardingDetailRepository">The manifest boarding detail repository.</param>
        public ManifestDepartureBusiness(
            IUnitOfWork unitOfWork,
            IManifestDepartureRepository manifestRepository,
            IAirportRepository airportRepository,
            IItineraryRepository itineraryRepository,
            IManifestTimeConfigRepository timeConfigRepository,
            IManifestDepartureBoardingRepository manifestBoardingRepository,
            IAdditionalDepartureInformationRepository additionalDepartureInformationRepository,
            IManifestDepartureBoardingInformationRepository manifestBoardingInformationRepository,
            IManifestDepartureBoardingDetailRepository manifestBoardingDetailRepository)
        {
            this.unitOfWork = unitOfWork;
            this.manifestRepository = manifestRepository;
            this.airportRepository = airportRepository;
            this.itineraryRepository = itineraryRepository;
            this.timeConfigRepository = timeConfigRepository;
            this.manifestBoardingRepository = manifestBoardingRepository;
            this.additionalDepartureInformationRepository = additionalDepartureInformationRepository;
            this.manifestBoardingInformationRepository = manifestBoardingInformationRepository;
            this.manifestBoardingDetailRepository = manifestBoardingDetailRepository;
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
                    Itinerary itinerary = this.itineraryRepository.GetItineraryWithDeclarationsAndPassengerInformation(sequence, airlineCode, flightNumber, itineraryKey);
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
                IList<ManifestDepartureBoarding> boardings = new List<ManifestDepartureBoarding>();
                AdditionalDepartureInformation additional = new AdditionalDepartureInformation();

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

                    //Boarding
                    boardings = Mapper.Map<IList<ManifestDepartureBoarding>>(manifestDeparture.ManifestDepartureBoardings);

                    //Informacion de Carga  Boarding
                    foreach (var item in boardings)
                    {
                        var boardingInfo = manifestDeparture.ManifestDepartureBoardings.Where(c => c.Position == item.Position).Select(c => c.ManifestDepartureBoardingInformationDtos).ToList();
                        var boardingDetailInfo = manifestDeparture.ManifestDepartureBoardings.Where(c => c.Position == item.Position).Select(c => c.ManifestDepartureBoardingDetailDtos).ToList();

                        if (boardingInfo != null)
                        {
                            item.ManifestDepartureBoardingInformations = Mapper.Map<IList<ManifestDepartureBoardingInformation>>(boardingInfo[0]);
                        }

                        if (boardingDetailInfo != null)
                        {
                            item.ManifestDepartureBoardingDetails = Mapper.Map<IList<ManifestDepartureBoardingDetail>>(boardingDetailInfo[0]);
                        }
                    }

                    // Additional Information
                    if (manifestDeparture.AdditionalDepartureInformation != null)
                    {
                        additional = Mapper.Map<AdditionalDepartureInformation>(manifestDeparture.AdditionalDepartureInformation);
                    }

                    this.SetInformationInEntity(entity, manifestDeparture);
                    DateTime.TryParse(manifestDeparture.ActualDepartureDate + " " + manifestDeparture.ActualDepartureTime, out actualDepartureDate);
                    entity.ActualDepartureDate = actualDepartureDate != default(DateTime) ? actualDepartureDate : new DateTime();
                    this.manifestRepository.RemoveAllDelaysFromManifest(entity);
                    this.manifestRepository.Update(entity, delays);
                    this.UpdateBoarding(entity, boardings);
                    this.UpdateAdditional(entity, additional);
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
        /// Gets the boarding for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>Information about boarding in the manifest.</returns>
        public IList<ManifestDepartureBoardingDto> GetBoardingForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                || string.IsNullOrWhiteSpace(flightNumber)
                || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            try
            {
                List<ManifestDepartureBoarding> boardings = this.manifestBoardingRepository.GetBoardingsForManifest(sequence, airlineCode, flightNumber, itineraryKey).ToList();
                return Mapper.Map<List<ManifestDepartureBoardingDto>>(boardings);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the boarding information for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<ManifestDepartureBoardingInformationDto> GetBoardingInformationForManifest(long boardingID, string airplaneModel)
        {
            if (string.IsNullOrWhiteSpace(boardingID.ToString()))
            {
                return null;
            }

            try
            {
                List<ManifestDepartureBoardingInformation> boardings = this.manifestBoardingInformationRepository.GetInformationByBoardingID(boardingID, airplaneModel).ToList();
                return Mapper.Map<List<ManifestDepartureBoardingInformationDto>>(boardings);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the boarding detail for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<ManifestDepartureBoardingDetailDto> GetBoardingDetailForManifest(long boardingID, string airplaneModel)
        {
            if (string.IsNullOrWhiteSpace(boardingID.ToString()))
            {
                return null;
            }

            try
            {
                List<ManifestDepartureBoardingDetail> boardings = this.manifestBoardingDetailRepository.GetDetailByBoardingID(boardingID, airplaneModel).ToList();
                return Mapper.Map<List<ManifestDepartureBoardingDetailDto>>(boardings);
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
                ////IList<ManifestDepartureBoarding> boardings = new List<ManifestDepartureBoarding>();
                ////AdditionalDepartureInformation additional = new AdditionalDepartureInformation();

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

                    ////boardings = Mapper.Map<IList<ManifestDepartureBoarding>>(manifestDeparture.ManifestDepartureBoardings);

                    ////if (manifestDeparture.AdditionalDepartureInformation != null)
                    ////{
                    ////    additional = Mapper.Map<AdditionalDepartureInformation>(manifestDeparture.AdditionalDepartureInformation);
                    ////}

                    this.manifestRepository.Update(entity);
                    ////this.UpdateBoarding(entity, boardings);
                    ////this.UpdateAdditional(entity, additional);
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
        /// Updates the additional.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="additional">The additional.</param>
        private void UpdateAdditional(ManifestDeparture entity, AdditionalDepartureInformation additional)
        {
            AdditionalDepartureInformation additionalDB = this.additionalDepartureInformationRepository.FindById(entity.Sequence, entity.AirlineCode, entity.FlightNumber, entity.ItineraryKey);

            if (additionalDB != null)
            {
                if (string.IsNullOrEmpty(additional.AirlineCode))
                {
                    // delete
                    this.additionalDepartureInformationRepository.Delete(additionalDB);
                }
                else
                {
                    // update
                    additionalDB.Pilot = additional.Pilot;
                    additionalDB.Surcharge = additional.Surcharge;
                    additionalDB.ExtraCrew = additional.ExtraCrew;
                    additionalDB.TypeFlight = additional.TypeFlight;
                    additionalDB.SlotAllocatedTime = additional.SlotAllocatedTime;
                    additionalDB.SlotCoordinatedTime = additional.SlotCoordinatedTime;
                    additionalDB.OvernightEndTime = additional.OvernightEndTime;
                    additionalDB.ManeuverStartTime = additional.ManeuverStartTime;
                    additionalDB.PositionOutputTime = additional.PositionOutputTime;
                    additionalDB.DelayDescription1 = additional.DelayDescription1;
                    additionalDB.DelayDescription2 = additional.DelayDescription2;
                    additionalDB.DelayDescription3 = additional.DelayDescription3;
                    this.additionalDepartureInformationRepository.Update(additionalDB);
                }
            }
            else
            {
                // insert
                if (!string.IsNullOrEmpty(additional.AirlineCode))
                {
                    this.additionalDepartureInformationRepository.Add(additional);
                }
            }
        }

        /// <summary>
        /// Updates the boarding.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <param name="boardings">The boarding.</param>
        private void UpdateBoarding(ManifestDeparture manifestDeparture, IList<ManifestDepartureBoarding> boardings)
        {
            // 5 Stations
            List<int> positionsAll = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> positionsBD = new List<int>();
            List<int> positionsRes = new List<int>();
            DateTime creationDate = DateTime.Now;

            // Lista de Position en DB
            positionsBD = boardings.Select(c => c.Position).ToList();

            // Lista Restante de ALL - BD
            positionsRes = positionsAll.Except(positionsBD).ToList();

            foreach (var item in positionsRes)
            {
                boardings.Add(new ManifestDepartureBoarding
                {
                    Sequence = manifestDeparture.Sequence,
                    AirlineCode = manifestDeparture.AirlineCode,
                    FlightNumber = manifestDeparture.FlightNumber,
                    ItineraryKey = manifestDeparture.ItineraryKey,
                    Position = item
                });
            }

            foreach (ManifestDepartureBoarding item in boardings)
            {
                ManifestDepartureBoarding boardingEntity = this.manifestBoardingRepository.FindById(item.Sequence, item.AirlineCode, item.FlightNumber, item.ItineraryKey, item.Position);

                // update
                if (boardingEntity != null)
                {
                    // delete
                    if (string.IsNullOrEmpty(item.Station))
                    {
                        this.manifestBoardingRepository.Delete(boardingEntity);
                    }
                    else
                    {
                        // Update de campos de Boarding
                        boardingEntity.Station = item.Station;
                        boardingEntity.PassengerAdult = item.PassengerAdult;
                        boardingEntity.PassengerInfant = item.PassengerInfant;
                        boardingEntity.PassengerMinor = item.PassengerMinor;
                        boardingEntity.LuggageQuantity = item.LuggageQuantity;
                        boardingEntity.LuggageKgs = item.LuggageKgs;
                        boardingEntity.ChargeQuantity = item.ChargeQuantity;
                        boardingEntity.ChargeKgs = item.ChargeKgs;
                        boardingEntity.MailQuantity = item.MailQuantity;
                        boardingEntity.MailKgs = item.MailKgs;

                        // Update Information (Si hay datos en BD y se edita en front los check)
                        if ((boardingEntity.ManifestDepartureBoardingInformations != null && boardingEntity.ManifestDepartureBoardingInformations.Count > 0)
                            && (item.ManifestDepartureBoardingInformations != null && item.ManifestDepartureBoardingInformations.Count > 0))
                        {
                            foreach (var info in boardingEntity.ManifestDepartureBoardingInformations)
                            {
                                if (info.BoardingInformationID > 0)
                                {
                                    info.Checked = item.ManifestDepartureBoardingInformations.FirstOrDefault(c => c.BoardingInformationID == info.BoardingInformationID).Checked;
                                }
                            }
                        }

                        // Insert Information (Si no hay datos en BD y se edita en front los check)
                        if ((boardingEntity.ManifestDepartureBoardingInformations != null && boardingEntity.ManifestDepartureBoardingInformations.Count == 0)
                            && (item.ManifestDepartureBoardingInformations != null && item.ManifestDepartureBoardingInformations.Count > 0))
                        {
                            boardingEntity.ManifestDepartureBoardingInformations = item.ManifestDepartureBoardingInformations;
                        }

                        // Update Detail Information (Si hay datos en BD y se edita en front los input)
                        if ((boardingEntity.ManifestDepartureBoardingDetails != null && boardingEntity.ManifestDepartureBoardingDetails.Count > 0)
                            && (item.ManifestDepartureBoardingDetails != null && item.ManifestDepartureBoardingDetails.Count > 0))
                        {
                            foreach (var info in boardingEntity.ManifestDepartureBoardingDetails)
                            {
                                if (info.BoardingDetailID > 0)
                                {
                                    info.LuggageQuantity = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).LuggageQuantity;
                                    info.LuggageKgs = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).LuggageKgs;
                                    info.ChargeQuantity = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).ChargeQuantity;
                                    info.ChargeKgs = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).ChargeKgs;
                                    info.Remarks = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).Remarks;
                                    info.RampResponsible = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).RampResponsible;
                                    info.AorUserID = item.ManifestDepartureBoardingDetails.FirstOrDefault(c => c.BoardingDetailID == info.BoardingDetailID).AorUserID;
                                }
                            }
                        }

                        // Insert Detail Information (Si no hay datos en BD y se edita en front los input)
                        if ((boardingEntity.ManifestDepartureBoardingDetails != null && boardingEntity.ManifestDepartureBoardingDetails.Count == 0)
                            && (item.ManifestDepartureBoardingDetails != null && item.ManifestDepartureBoardingDetails.Count > 0))
                        {
                            boardingEntity.ManifestDepartureBoardingDetails = item.ManifestDepartureBoardingDetails;
                        }

                        this.manifestBoardingRepository.Update(boardingEntity);
                    }
                }
                else
                {
                    // insert
                    if (!string.IsNullOrEmpty(item.Station))
                    {
                        this.manifestBoardingRepository.Add(item);
                    }
                }
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
