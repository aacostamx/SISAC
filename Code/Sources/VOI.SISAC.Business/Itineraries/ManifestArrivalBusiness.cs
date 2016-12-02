//------------------------------------------------------------------------
// <copyright file="ManifestArrivalBusiness.cs" company="Volaris">
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
    public class ManifestArrivalBusiness : IManifestArrivalBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The manifest repository
        /// </summary>
        private readonly IManifestArrivalRepository manifestRepository;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// The additional arrival information repository
        /// </summary>
        private readonly IAdditionalArrivalInformationRepository additionalArrivalInformationRepository;

        /// <summary>
        /// The time configuration repository
        /// </summary>
        private readonly IManifestTimeConfigRepository timeConfigRepository;

        /// <summary>
        /// The itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// The departure general declaration
        /// </summary>
        private readonly IGendecDepartureRepository gendecDeparture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestArrivalBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="timeConfigRepository">The time configuration repository.</param>
        /// <param name="additionalArrivalInformationRepository">The additional arrival information repository.</param>
        /// <param name="gendecDeparture">The general declaration departure.</param>
        public ManifestArrivalBusiness(
            IUnitOfWork unitOfWork,
            IManifestArrivalRepository manifestRepository,
            IAirportRepository airportRepository,
            IItineraryRepository itineraryRepository,
            IManifestTimeConfigRepository timeConfigRepository,
            IAdditionalArrivalInformationRepository additionalArrivalInformationRepository,
            IGendecDepartureRepository gendecDeparture)
        {
            this.unitOfWork = unitOfWork;
            this.manifestRepository = manifestRepository;
            this.airportRepository = airportRepository;
            this.itineraryRepository = itineraryRepository;
            this.timeConfigRepository = timeConfigRepository;
            this.additionalArrivalInformationRepository = additionalArrivalInformationRepository;
            this.gendecDeparture = gendecDeparture;
        }

        /// <summary>
        /// Gets the arrival manifest for flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// The arrival manifest for the flight.
        /// </returns>
        public ManifestArrivalDto GetManifestArrivalForFlight(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            try
            {
                string airportName = string.Empty;
                ManifestArrivalDto manifestDto = new ManifestArrivalDto();
                ManifestArrival entity = this.manifestRepository.GetManifestArrivalForItinerary(sequence, airlineCode, flightNumber, itineraryKey);

                // If the manifest does not exist
                if (entity == null)
                {
                    // Gets the itinerary information
                    Itinerary itinerary = this.itineraryRepository.GetItineraryWithDeclarationsAndPassengerInformation(sequence, airlineCode, flightNumber, itineraryKey);
                    if (itinerary != null)
                    {
                        // Finds if the arrival station is from Mexico
                        if (this.itineraryRepository.IsDepartureStationFromMexico(sequence, airlineCode, flightNumber, itineraryKey))
                        {
                            // If the airport is from Mexico, gets the departure manifest information
                            SetManifestDepartureInformation(manifestDto, itinerary);
                        }
                        else
                        {
                            // If the airport is not from Mexico, gets the departure GENDEC information
                            SetGendecDepartureInformation(manifestDto, itinerary);
                        }

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
                        manifestDto.ScheduledArrivalTime = itinerary.ArrivalDate.TimeOfDay.ToString(@"hh\:mm");
                        manifestDto.ScheduledArrivalDate = itinerary.ArrivalDate.ToString("yyyy/MM/dd");

                        // Sets the passenger information
                        SetPassengerInformation(manifestDto, itineraryDto.PassengerInformation);

                        // Sets the itinerary information
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
                    manifestDto = Mapper.Map<ManifestArrivalDto>(entity);
                    manifestDto.ArrivalStationName = this.airportRepository.GetAirportName(manifestDto.ArrivalStationCode);
                    manifestDto.DepartureStationName = this.airportRepository.GetAirportName(manifestDto.DepartureStationCode);
                    manifestDto.LastScaleStationName = this.airportRepository.GetAirportName(manifestDto.LastScaleStationCode);
                    manifestDto.ActualArrivalTime = entity.ActualArrivalDate.TimeOfDay.ToString(@"hh\:mm");
                    manifestDto.ActualArrivalDate = entity.ActualArrivalDate.ToString("yyyy/MM/dd");
                    manifestDto.ScheduledArrivalTime = entity.Itinerary.ArrivalDate.TimeOfDay.ToString(@"hh\:mm");
                    manifestDto.ScheduledArrivalDate = entity.Itinerary.ArrivalDate.ToString("yyyy/MM/dd");

                    // Find if the airport is national or not.
                    isNational = this.airportRepository.FindById(manifestDto.DepartureStationCode).Country.CountryCode == "MEX";
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
        /// Saves the arrival manifest.
        /// </summary>
        /// <param name="manifestArrival">Manifest arrival</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        public bool SaveManifestArrival(ManifestArrivalDto manifestArrival)
        {
            if (manifestArrival == null)
            {
                return false;
            }

            try
            {
                ManifestArrival entity = this.manifestRepository.GetManifestArrivalForItinerary(
                    manifestArrival.Sequence,
                    manifestArrival.AirlineCode,
                    manifestArrival.FlightNumber,
                    manifestArrival.ItineraryKey);
                AdditionalArrivalInformation additional = new AdditionalArrivalInformation();

                // Manifest does not exist and create a new one for the itinerary
                if (entity == null)
                {
                    DateTime actualArrivalDate;
                    entity = Mapper.Map<ManifestArrival>(manifestArrival);
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestArrival.Delays);
                    entity.CreationDate = DateTime.Now;
                    DateTime.TryParse(manifestArrival.ActualArrivalDate + " " + manifestArrival.ActualArrivalTime, out actualArrivalDate);
                    entity.ActualArrivalDate = actualArrivalDate != default(DateTime) ? actualArrivalDate : new DateTime();
                    entity.Delays = Mapper.Map<IList<Delay>>(manifestArrival.Delays);
                    entity.Delays = null;
                    this.manifestRepository.Add(entity, delays);
                }
                else
                {
                    // The manifest exists and proceed to update it.
                    DateTime actualArrivalDate;
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestArrival.Delays);

                    if (manifestArrival.AdditionalArrivalInformation != null)
                    {
                        additional = Mapper.Map<AdditionalArrivalInformation>(manifestArrival.AdditionalArrivalInformation);
                    }

                    SetInformationInEntity(entity, manifestArrival);
                    DateTime.TryParse(manifestArrival.ActualArrivalDate + " " + manifestArrival.ActualArrivalTime, out actualArrivalDate);
                    entity.ActualArrivalDate = actualArrivalDate != default(DateTime) ? actualArrivalDate : new DateTime();
                    this.manifestRepository.RemoveAllDelaysFromManifest(entity);
                    this.manifestRepository.Update(entity, delays);
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
        /// <returns>
        /// List of delays in the arrival manifest.
        /// </returns>
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
        /// <param name="manifestArrival">The arrival manifest.</param>
        /// <returns>
        ///   <c>true</c> if the operation was success otherwise <c>false</c>.
        /// </returns>
        public bool CloseManifest(ManifestArrivalDto manifestArrival)
        {
            if (manifestArrival == null
                || string.IsNullOrWhiteSpace(manifestArrival.AirlineCode)
                || string.IsNullOrWhiteSpace(manifestArrival.FlightNumber)
                || string.IsNullOrWhiteSpace(manifestArrival.ItineraryKey))
            {
                return false;
            }

            try
            {
                ManifestArrival entity = this.manifestRepository.GetManifestArrivalForItinerary(
                    manifestArrival.Sequence,
                    manifestArrival.AirlineCode,
                    manifestArrival.FlightNumber,
                    manifestArrival.ItineraryKey);

                if (entity == null)
                {
                    DateTime actualArrivalDate;
                    entity = Mapper.Map<ManifestArrival>(manifestArrival);
                    IList<Delay> delays = Mapper.Map<IList<Delay>>(manifestArrival.Delays);
                    entity.CreationDate = DateTime.Now;
                    DateTime.TryParse(manifestArrival.ActualArrivalDate + " " + manifestArrival.ActualArrivalTime, out actualArrivalDate);
                    entity.ActualArrivalDate = actualArrivalDate != default(DateTime) ? actualArrivalDate : new DateTime();
                    entity.Closed = true;
                    entity.Delays = Mapper.Map<IList<Delay>>(manifestArrival.Delays);
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
        /// <param name="manifestArrival">The manifest departure.</param>
        /// <returns>
        ///   <c>true</c> if the operation was success otherwise <c>false</c>.
        /// </returns>
        public bool OpenManifest(ManifestArrivalDto manifestArrival)
        {
            if (manifestArrival == null
                || string.IsNullOrWhiteSpace(manifestArrival.AirlineCode)
                || string.IsNullOrWhiteSpace(manifestArrival.FlightNumber)
                || string.IsNullOrWhiteSpace(manifestArrival.ItineraryKey))
            {
                return false;
            }

            try
            {
                ManifestArrival entity = this.manifestRepository.GetManifestArrivalForItinerary(
                    manifestArrival.Sequence,
                    manifestArrival.AirlineCode,
                    manifestArrival.FlightNumber,
                    manifestArrival.ItineraryKey);

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
        private static void SetPassengerInformation(ManifestArrivalDto manifest, PassengerInformationDto passengerInformation)
        {
            if (passengerInformation != null)
            {
                manifest.AdultPassenger = passengerInformation.AdultsCabinA + passengerInformation.AdultsCabinB;
                manifest.MinorPassenger = passengerInformation.TeenageCabinA + passengerInformation.TeenageCabinB;
                manifest.InfantPassenger = passengerInformation.ChildrenCabinA + passengerInformation.ChildrenCabinB;

                manifest.LuggageQuantity = passengerInformation.LocalBaggageQuantity
                    + passengerInformation.TransitoryBaggageQuantity
                    + passengerInformation.ConnectionBaggageQuantity
                    + passengerInformation.DiplomaticBaggageQuantity
                    + passengerInformation.ExtraCrewBaggageQuantity
                    + passengerInformation.OtherBaggageQuantity;

                manifest.LuggageWeight = passengerInformation.LocalBaggageWeight
                    + passengerInformation.TransitoryBaggageWeight
                    + passengerInformation.ConnectionBaggageWeight
                    + passengerInformation.DiplomaticBaggageWeight
                    + passengerInformation.ExtraCrewBaggageWeight
                    + passengerInformation.OtherBaggageWeight;
            }
        }

        /// <summary>
        /// Sets the information in entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="dto">The data transfer object.</param>
        private static void SetInformationInEntity(ManifestArrival entity, ManifestArrivalDto dto)
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

            entity.ChargeQuantity = dto.ChargeQuantity;
            entity.ChargeWeight = dto.ChargeWeight;
            entity.MailQuantity = dto.MailQuantity;
            entity.MailWeight = dto.MailWeight;

            entity.DepartureStation = dto.DepartureStationCode;
            entity.ArrivalStation = dto.ArrivalStationCode;
            entity.LastScaleStation = dto.LastScaleStationCode;
            entity.DelayRemarks = dto.DelayRemarks;

            entity.UserAuthorizeId = dto.UserIdAuthorize;
            entity.UserSignatureId = dto.UserIdSignature;
            entity.LicenceNumberAuthorize = dto.LicenceNumberAuthorize;
            entity.LicenceNumberSignature = dto.LicenceNumberSignature;

            entity.Position = dto.Position;
            entity.JetFuelArrival = dto.JetFuelArrival;
            entity.Remarks = dto.Remarks;
        }

        /// <summary>
        /// Sets the departure information.
        /// </summary>
        /// <param name="manifestDto">The manifest data transfer object.</param>
        /// <param name="itinerary">The itinerary.</param>
        private static void SetManifestDepartureInformation(ManifestArrivalDto manifestDto, Itinerary itinerary)
        {
            if (itinerary != null && itinerary.ManifestDeparture != null)
            {
                manifestDto.NickNameChiefCabinet = itinerary.ManifestDeparture.NickNameChiefCabinet;
                manifestDto.NickNameCommander = itinerary.ManifestDeparture.NickNameCommander;
                manifestDto.NickNameFirstOfficial = itinerary.ManifestDeparture.NickNameFirstOfficial;
                manifestDto.NickNameFirstSupercargo = itinerary.ManifestDeparture.NickNameFirstSupercargo;

                manifestDto.NickNameSecondOfficial = itinerary.ManifestDeparture.NickNameSecondOfficial;
                manifestDto.NickNameSecondSupercargo = itinerary.ManifestDeparture.NickNameSecondSupercargo;
                manifestDto.NickNameThirdOfficial = itinerary.ManifestDeparture.NickNameThirdOfficial;
                manifestDto.NickNameThirdSupercargo = itinerary.ManifestDeparture.NickNameThirdSupercargo;

                manifestDto.SupercargoRemarks = itinerary.ManifestDeparture.SupercargoRemarks;
            }
        }

        /// <summary>
        /// Sets the general declaration departure information.
        /// </summary>
        /// <param name="manifestDto">The manifest data transfer object.</param>
        /// <param name="itinerary">The itinerary.</param>
        private static void SetGendecDepartureInformation(ManifestArrivalDto manifestDto, Itinerary itinerary)
        {
            if (itinerary != null && itinerary.GendecDepartures != null && itinerary.GendecDepartures.Crews != null)
            {
                List<Crew> captains = new List<Crew>();
                List<Crew> stewardess = new List<Crew>();
                string remarks = string.Empty;

                captains.AddRange(itinerary.GendecDepartures.Crews.Where(c => c.CrewTypeID == "CAP" || c.CrewTypeID == "COP").OrderBy(c => c.CrewTypeID));
                stewardess.AddRange(itinerary.GendecDepartures.Crews.Where(c => c.CrewTypeID == "JDC" || c.CrewTypeID == "SOB").OrderBy(c => c.CrewTypeID));

                // Sets the commander and officials
                if (captains != null)
                {
                    for (byte i = 0; i < captains.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                manifestDto.NickNameCommander = captains[i].NickName ?? string.Empty;
                                break;
                            case 1:
                                manifestDto.NickNameFirstOfficial = captains[i].NickName ?? string.Empty;
                                break;
                            case 2:
                                manifestDto.NickNameSecondOfficial = captains[i].NickName ?? string.Empty;
                                break;
                            case 3:
                                manifestDto.NickNameThirdOfficial = captains[i].NickName ?? string.Empty;
                                break;
                        }
                    }
                }

                // Sets the chief cabinet and stewardess
                if (stewardess != null)
                {
                    for (byte i = 0; i < stewardess.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                manifestDto.NickNameChiefCabinet = stewardess[i].NickName ?? string.Empty;
                                break;
                            case 1:
                                manifestDto.NickNameFirstSupercargo = stewardess[i].NickName ?? string.Empty;
                                break;
                            case 2:
                                manifestDto.NickNameSecondSupercargo = stewardess[i].NickName ?? string.Empty;
                                break;
                            case 3:
                                manifestDto.NickNameThirdSupercargo = stewardess[i].NickName ?? string.Empty;
                                break;
                            default:
                                remarks += string.Format(@"{0} ({1}) / ", stewardess[i].NickName, stewardess[i].LicenceNumber);
                                break;
                        }
                    }

                    manifestDto.SupercargoRemarks = remarks.Trim();
                }
            }
        }

        /// <summary>
        /// Updates the additional.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="additional">The additional.</param>
        private void UpdateAdditional(ManifestArrival entity, AdditionalArrivalInformation additional)
        {
            AdditionalArrivalInformation additionalDB = this.additionalArrivalInformationRepository.FindById(entity.Sequence, entity.AirlineCode, entity.FlightNumber, entity.ItineraryKey);

            // update
            if (additionalDB != null)
            {
                if (string.IsNullOrEmpty(additional.AirlineCode))
                {
                    // delete
                    this.additionalArrivalInformationRepository.Delete(additionalDB);
                }
                else
                {
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
                    this.additionalArrivalInformationRepository.Update(additionalDB);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(additional.AirlineCode))
                {
                    // insert
                    this.additionalArrivalInformationRepository.Add(additional);
                }
            }
        }
    }
}