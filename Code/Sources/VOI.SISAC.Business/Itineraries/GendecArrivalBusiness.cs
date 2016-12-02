//------------------------------------------------------------------------
// <copyright file="GendecArrivalBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using AutoMapper;
    using ExceptionBusiness;
    using Resources;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// General document business
    /// </summary>
    public class GendecArrivalBusiness : IGendecArrivalBusiness
    {
        /// <summary>
        /// unit Of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// General document Arrival Repository
        /// </summary>
        private readonly IGendecArrivalRepository gendecArrivalRepository;

        /// <summary>
        /// The itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// The crew repository
        /// </summary>
        private readonly ICrewRepository crewRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GendecArrivalBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="gendecArrivalRepository">The general document arrival repository.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="crewRepository">The crew repository.</param>
        public GendecArrivalBusiness(
            IUnitOfWork unitOfWork,
            IGendecArrivalRepository gendecArrivalRepository,
            IItineraryRepository itineraryRepository,
            ICrewRepository crewRepository)
        {
            this.unitOfWork = unitOfWork;
            this.gendecArrivalRepository = gendecArrivalRepository;
            this.itineraryRepository = itineraryRepository;
            this.crewRepository = crewRepository;
        }

        /// <summary>
        /// Gets the general document arrival.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// Arrival General Document data transfer object.
        /// </returns>
        public GendecArrivalDto GetGendecArrival(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            try
            {
                GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
                GendecArrival gendecArrival = this.gendecArrivalRepository.GetGendecArrival(sequence, airlineCode, flightNumber, itineraryKey);

                // The GENDEC does not exit
                if (gendecArrival == null)
                {
                    gendecArrival = new GendecArrival();
                    Itinerary itinerary = this.itineraryRepository.GetItineraryWithDeclarationsAndPassengerInformation(sequence, airlineCode, flightNumber, itineraryKey);

                    if (itinerary != null)
                    {
                        gendecArrival.Itinerary = itinerary;

                        // Finds if the arrival station is from Mexico
                        if (this.itineraryRepository.IsDepartureStationFromMexico(sequence, airlineCode, flightNumber, itineraryKey))
                        {
                            // If the airport is from Mexico, gets the departure manifest information
                            this.SetManifestDepartureInformation(gendecArrival, itinerary.ManifestDeparture);
                        }
                        else
                        {
                            // If the airport is not from Mexico, gets the departure GENDEC information
                            SetGendecDepartureInformation(gendecArrival, itinerary.GendecDepartures);
                        }
                    }
                    else
                    {
                        gendecArrival.Itinerary = new Itinerary();
                    }
                }

                gendecArrivalDto = Mapper.Map<GendecArrival, GendecArrivalDto>(gendecArrival);
                return gendecArrivalDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Validates the general document arrival information.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        /// List of possible errors. If the list is empty the operation was success.
        /// </returns>
        public IList<string> ValidateGendecArrivalInformation(GendecArrivalDto gendecArrivalDto)
        {
            IList<string> errors = new List<string>();
            GendecArrivalDto gendecArrivalDtoDB = new GendecArrivalDto();
            gendecArrivalDtoDB = Mapper.Map<GendecArrival, GendecArrivalDto>(this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey));
            if (gendecArrivalDtoDB == null)
            {   
                    this.AddGendecArrival(gendecArrivalDto);
            }
            else
            {
                    this.UpdateGendecArrival(gendecArrivalDto);
            }

            return errors;
        }

        /// <summary>
        /// Adds the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        public bool AddGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            GendecArrival gendecArrival = new GendecArrival();
            if (gendecArrivalDto == null)
            {
                return false;
            }

            try
            {
                gendecArrival = Mapper.Map<GendecArrivalDto, GendecArrival>(gendecArrivalDto);
                IList<Crew> crews = gendecArrival.Crews.ToList();
                gendecArrival.Crews = new List<Crew>();
                gendecArrival.Closed = false;
                this.gendecArrivalRepository.AddGendecArrival(gendecArrival, crews);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        public bool UpdateGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
                gendecArrival.TotalPax = gendecArrivalDto.TotalPax;
                gendecArrival.TotalCrew = gendecArrivalDto.TotalCrew;
                gendecArrival.BlockTime = gendecArrivalDto.BlockTime;
                gendecArrival.ActualTimeOfArrival = gendecArrivalDto.ActualTimeOfArrival;
                gendecArrival.ManifestNumber = gendecArrivalDto.ManifestNumber;
                gendecArrival.GateNumber = gendecArrivalDto.GateNumber;
                gendecArrival.ArrivalPlace = gendecArrivalDto.ArrivalPlace;
                gendecArrival.AuthorizedAgent = gendecArrivalDto.AuthorizedAgent;
                gendecArrival.Remarks = gendecArrivalDto.Remarks;
                gendecArrival.Disembanking = gendecArrivalDto.Disembanking;
                gendecArrival.FlightArrivalDescription = gendecArrivalDto.FlightArrivalDescription;
                gendecArrival.Member = gendecArrivalDto.Member;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Closes the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        public bool CloseGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = new GendecArrival();
                gendecArrivalDto.Itinerary = null;
                gendecArrival = Mapper.Map<GendecArrivalDto, GendecArrival>(gendecArrivalDto);
                gendecArrival.Closed = true;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Opens the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        public bool OpenGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = new GendecArrival();
                gendecArrival = this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
                gendecArrival.Closed = false;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Sets the departure general document information into GENDEC.
        /// </summary>
        /// <param name="arrival">The arrival general document.</param>
        /// <param name="departure">The departure general document.</param>
        private static void SetGendecDepartureInformation(GendecArrival arrival, GendecDeparture departure)
        {
            arrival.Crews = new List<Crew>();
            if (departure != null && departure.Crews != null)
            {
                foreach (Crew item in departure.Crews)
                {
                    arrival.Crews.Add(item);
                }
            }
        }

        /// <summary>
        /// Sets the departure manifest information into GENDEC.
        /// </summary>
        /// <param name="arrival">The arrival general document.</param>
        /// <param name="departure">The departure manifest.</param>
        private void SetManifestDepartureInformation(GendecArrival arrival, ManifestDeparture departure)
        {
            arrival.Crews = new List<Crew>();
            if (departure != null)
            {
                arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameCommander));
                arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameFirstOfficial));

                if (departure.NickNameSecondOfficial != null)
                {
                    arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameSecondOfficial));
                }

                if (departure.NickNameThirdOfficial != null)
                {
                    arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameThirdOfficial));
                }

                arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameChiefCabinet));
                arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameFirstSupercargo));
                arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameSecondSupercargo));

                if (departure.NickNameThirdSupercargo != null)
                {
                    arrival.Crews.Add(this.crewRepository.Find(c => c.NickName == departure.NickNameThirdSupercargo));
                }
            }
        }
    }
}