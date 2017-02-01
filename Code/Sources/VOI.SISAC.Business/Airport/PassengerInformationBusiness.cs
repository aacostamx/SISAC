//------------------------------------------------------------------------
// <copyright file="PassengerInformationBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using ExceptionBusiness;
    using Resources;    
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Airport;


    /// <summary>
    /// Passenger Information Business Logic
    /// </summary>
    public class PassengerInformationBusiness : IPassengerInformationBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The passenger information repository
        /// </summary>
        private readonly IPassengerInformationRepository passengerInformationRepository;

        /// <summary>
        /// The airplane repository
        /// </summary>
        private readonly IAirplaneRepository airplaneRepository;

        /// <summary>
        /// The itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassengerInformationBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="passengerInformationRepository">The passenger information repository.</param>
        /// <param name="airplaneRepository">The airplane repository.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        public PassengerInformationBusiness(
            IUnitOfWork unitOfWork,
            IPassengerInformationRepository passengerInformationRepository,
            IAirplaneRepository airplaneRepository,
            IItineraryRepository itineraryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.passengerInformationRepository = passengerInformationRepository;
            this.airplaneRepository = airplaneRepository;
            this.itineraryRepository = itineraryRepository;
        }

        /// <summary>
        /// Get the passengers information of a flight.
        /// </summary>
        /// <param name="sequence">The sequence</param>
        /// <param name="airlineCode">The airline code</param>
        /// <param name="flightNumber">The flight number</param>
        /// <param name="itinerayKey">The itinerary key</param>
        /// <returns></returns>
        public PassengerInformationDto FindById(int sequence, string airlineCode, string flightNumber, string itinerayKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itinerayKey) || sequence == 0)
            {
                return null;
            }

            try
            {
                PassengerInformation passengerInformation = this.passengerInformationRepository.FindById(sequence, airlineCode, flightNumber, itinerayKey);
                PassengerInformationDto passengerInformationDto = Mapper.Map<PassengerInformation, PassengerInformationDto>(passengerInformation);

                if (passengerInformation != null && passengerInformation.AdditionalPassengerInformation != null)
                {
                    passengerInformationDto.AdditonalInformation = Mapper.Map<AdditionalPassengerInformationDto>(passengerInformation.AdditionalPassengerInformation);
                }

                return passengerInformationDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add a new Passenger Information
        /// </summary>
        /// <param name="passengerInformationDto"></param>
        /// <returns>True if success.</returns>
        public bool AddPassengerInformation(PassengerInformationDto passengerInformationDto)
        {
            if (passengerInformationDto == null)
            {
                return false;
            }

            try
            { 
                PassengerInformation passengerInformation = new PassengerInformation();
                passengerInformation = Mapper.Map<PassengerInformationDto, PassengerInformation>(passengerInformationDto);
                if (passengerInformationDto.AdditonalInformation != null)
                {
                    passengerInformation.AdditionalPassengerInformation = Mapper.Map<AdditionalPassengerInformation>(passengerInformationDto.AdditonalInformation);
                    passengerInformation.AdditionalPassengerInformation.Sequence = passengerInformation.Sequence;
                    passengerInformation.AdditionalPassengerInformation.AirlineCode = passengerInformation.AirlineCode;
                    passengerInformation.AdditionalPassengerInformation.FlightNumber = passengerInformation.FlightNumber;
                    passengerInformation.AdditionalPassengerInformation.ItineraryKey = passengerInformation.ItineraryKey;
                }

                this.passengerInformationRepository.Add(passengerInformation);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Update Passenger Information
        /// </summary>
        /// <param name="passengerInformationDto">Passenger Information Data trasfer object</param>
        /// <returns>True if success</returns>
        public bool UpdatePassengerInformation(PassengerInformationDto passengerInformationDto)
        {
            try
            {
                PassengerInformation passengerInformation = this.passengerInformationRepository.FindById(passengerInformationDto.Sequence, passengerInformationDto.AirlineCode, passengerInformationDto.FlightNumber, passengerInformationDto.ItineraryKey);
                passengerInformation.AdultsCabinA = passengerInformationDto.AdultsCabinA;
                passengerInformation.AdultsCabinB = passengerInformationDto.AdultsCabinB;
                passengerInformation.TeenageCabinA = passengerInformationDto.TeenageCabinA;
                passengerInformation.TeenageCabinB = passengerInformationDto.TeenageCabinB;
                passengerInformation.ChildrenCabinA = passengerInformationDto.ChildrenCabinA;
                passengerInformation.ChildrenCabinB = passengerInformationDto.ChildrenCabinB;

                passengerInformation.LocalAdults = passengerInformationDto.LocalAdults;
                passengerInformation.LocalTeenage = passengerInformationDto.LocalTeenage;
                passengerInformation.LocalChildren = passengerInformationDto.LocalChildren;
                passengerInformation.TransitoryAdults = passengerInformationDto.TransitoryAdults;
                passengerInformation.TransitoryTeenage = passengerInformationDto.TransitoryTeenage;
                passengerInformation.TransitoryChildren = passengerInformationDto.TransitoryChildren;
                passengerInformation.ConnectionAdults = passengerInformationDto.ConnectionAdults;
                passengerInformation.ConnectionTeenage = passengerInformationDto.ConnectionTeenage;
                passengerInformation.ConnectionChildren = passengerInformationDto.ConnectionChildren;
                passengerInformation.Diplomatic = passengerInformationDto.Diplomatic;
                passengerInformation.ExtraCrew = passengerInformationDto.ExtraCrew;
                passengerInformation.Other = passengerInformationDto.Other;
                passengerInformation.LocalBaggageQuantity = passengerInformationDto.LocalBaggageQuantity;
                passengerInformation.TransitoryBaggageQuantity = passengerInformationDto.TransitoryBaggageQuantity;
                passengerInformation.ConnectionBaggageQuantity = passengerInformationDto.ConnectionBaggageQuantity;
                passengerInformation.DiplomaticBaggageQuantity = passengerInformationDto.DiplomaticBaggageQuantity;
                passengerInformation.ExtraCrewBaggageQuantity = passengerInformationDto.ExtraCrewBaggageQuantity;
                passengerInformation.OtherBaggageQuantity = passengerInformationDto.OtherBaggageQuantity;
                passengerInformation.LocalBaggageWeight = passengerInformationDto.LocalBaggageWeight;
                passengerInformation.TransitoryBaggageWeight = passengerInformationDto.TransitoryBaggageWeight;
                passengerInformation.ConnectionBaggageWeight = passengerInformationDto.ConnectionBaggageWeight;
                passengerInformation.DiplomaticBaggageWeight = passengerInformationDto.DiplomaticBaggageWeight;
                passengerInformation.ExtraCrewBaggageWeight = passengerInformationDto.ExtraCrewBaggageWeight;
                passengerInformation.OtherBaggageWeight = passengerInformationDto.OtherBaggageWeight;
                passengerInformation.Observation = passengerInformationDto.Observation;
                passengerInformation.UserId = passengerInformationDto.UserId;

                //Previous Flight
                passengerInformation.PreviousSequence = passengerInformationDto.PreviousSequence;
                passengerInformation.PreviousAirlineCode = passengerInformationDto.PreviousAirlineCode;
                passengerInformation.PreviousFlightNumber = passengerInformationDto.PreviousFlightNumber;
                passengerInformation.PreviousItineraryKey = passengerInformationDto.PreviousItineraryKey;

                if (passengerInformationDto.AdditonalInformation != null)
                {
                    passengerInformation.AdditionalPassengerInformation = Mapper.Map<AdditionalPassengerInformation>(passengerInformationDto.AdditonalInformation);
                }

                this.passengerInformationRepository.Update(passengerInformation);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Validates the passenger information.
        /// </summary>
        /// <param name="passengerInformationDto">The passenger information dto.</param>
        /// <param name="isMexicanAirport">if set to <c>true</c> set rules for national airports.</param>
        /// <returns>
        /// List of erros, if exist.
        /// </returns>
        public IList<string> ValidatePassengerInformation(PassengerInformationDto passengerInformationDto, bool isMexicanAirport, bool isInternationalAirport)
        {
            IList<string> errors = new List<string>();
            PassengerInformation passengerInformation = new PassengerInformation();

            // Finds if a Passenger information already exists
            passengerInformation = this.passengerInformationRepository.FindById(
                passengerInformationDto.Sequence, 
                passengerInformationDto.AirlineCode, 
                passengerInformationDto.FlightNumber, 
                passengerInformationDto.ItineraryKey);

            // If not exist, create a new one
            if (passengerInformation == null)
            {
                // If is a national airport, validates that the Totals match
                if (isMexicanAirport)
                {
                    errors = ValidateTotals(passengerInformationDto);

                    if (errors.Count > 0)
                    {
                        return errors;
                    }
                }
                    
                this.AddPassengerInformation(passengerInformationDto);
            }
            else
            {
                // Otherwise update
                if (isMexicanAirport)
                {
                    // If is a national airport, validates that the Totals match
                    errors = ValidateTotals(passengerInformationDto);

                    //Cuando Aplique destino internacional y origen nacional, se debe validar que otros exentos debe ser mayor o igual que TUA internacional de vuelo anterior
                    if (isInternationalAirport)
                    {
                        int sequenceDefault = passengerInformation.PreviousSequence ?? 0;

                        if (!string.IsNullOrEmpty(passengerInformation.PreviousAirlineCode)
                         && !string.IsNullOrEmpty(passengerInformation.PreviousFlightNumber)
                         && !string.IsNullOrEmpty(passengerInformation.PreviousItineraryKey))
                        {
                            PassengerInformationDto passengerPreviousDto = new PassengerInformationDto();
                            passengerPreviousDto = this.FindById(sequenceDefault,
                                passengerInformation.PreviousAirlineCode,
                                passengerInformation.PreviousFlightNumber,
                                passengerInformation.PreviousItineraryKey);
                            if (passengerPreviousDto != null)
                            {
                                if (passengerInformationDto.Other < passengerPreviousDto.InternationalTua)
                                {
                                    errors.Add("Other exempt must be greater than or equal to International TUA of the related Itinerary");
                                }
                            }
                        }
                    }

                    if (errors.Count > 0)
                    {
                        return errors;
                    }
                }                
                
                this.UpdatePassengerInformation(passengerInformationDto);
            }

            return errors;
        }

        /// <summary>
        /// Validates the totals.
        /// </summary>
        /// <param name="passengerInformationDto">The passenger information dto.</param>
        /// <returns>List of erros.</returns>
        private static IList<string> ValidateTotals(PassengerInformationDto passengerInformationDto)
        {
            IList<string> errors = new List<string>();
            int SumCabinAdults = 0;
            int SumCabinTeen = 0;
            int SumCabinChil = 0;
            int SumTotalAdult = 0;
            int SumTotalTeen = 0;
            int SumTotalChil = 0;
            int SumSinTUA = 0;
            
            // Se obtiene los totales por cabina
            SumCabinAdults = passengerInformationDto.AdultsCabinA
                             + passengerInformationDto.AdultsCabinB;

            SumCabinTeen = passengerInformationDto.TeenageCabinA
                           + passengerInformationDto.TeenageCabinB;

            SumCabinChil = passengerInformationDto.ChildrenCabinA
                           + passengerInformationDto.ChildrenCabinB;
            
            // Se Obtiene los totales por desgloce de persona
            SumTotalAdult = passengerInformationDto.LocalAdults 
                            + passengerInformationDto.TransitoryAdults
                            + passengerInformationDto.ConnectionAdults 
                            + passengerInformationDto.Diplomatic 
                            + passengerInformationDto.ExtraCrew 
                            + passengerInformationDto.Other;

            SumTotalTeen = passengerInformationDto.LocalTeenage
                           + passengerInformationDto.TransitoryTeenage
                           + passengerInformationDto.ConnectionTeenage;

            SumTotalChil = passengerInformationDto.LocalChildren
                           + passengerInformationDto.TransitoryChildren
                           + passengerInformationDto.ConnectionChildren;

            SumSinTUA = passengerInformationDto.Diplomatic
                        + passengerInformationDto.ExtraCrew;

            if (SumCabinAdults != SumTotalAdult || SumCabinTeen != SumTotalTeen || SumCabinChil != SumTotalChil)
            {
                errors.Add("The number of passenger doesnt match");
            }            

            return errors;
        }
    }
}