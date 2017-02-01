//------------------------------------------------------------------------
// <copyright file="ItineraryBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using AutoMapper;
    using Dal.Repository.Airports;
    using Dto.Airports;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Itineraries;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Itinerary Business Logic
    /// </summary>
    public class ItineraryBusiness : IItineraryBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        ///  itineraryLogRepository;
        /// </summary>
        private readonly IItineraryLogRepository itineraryLogRepository;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// The airplane repository
        /// </summary>
        private readonly IAirplaneRepository airplaneRepository;

        /// <summary>
        /// The timeline repository
        /// </summary>
        private readonly ITimelineRepository timelineRepository;

        /// <summary>
        /// The manifest arrival repository
        /// </summary>
        private readonly IManifestArrivalRepository manifestArrivalRepository;

        /// <summary>
        /// The manifest departure repository
        /// </summary>
        private readonly IManifestDepartureRepository manifestDepartureRepository;

        /// <summary>
        /// The passenger information repository
        /// </summary>
        private readonly IPassengerInformationRepository passengerInformationRepository;

        /// <summary>
        /// The manifest departure boarding repository
        /// </summary>
        private readonly IManifestDepartureBoardingRepository manifestDepartureBoardingRepository;

        /// <summary>
        /// The airport service repository
        /// </summary>
        private readonly IAirportServiceRepository airportServiceRepository;        

        /// <summary>
        /// The national jet fuel ticket repository
        /// </summary>
        private readonly INationalJetFuelTicketRepository nationalJetFuelTicketRepository;

        /// <summary>
        /// The jet fuel ticket repository
        /// </summary>
        private readonly IJetFuelTicketRepository jetFuelTicketRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="airplaneRepository">The airplane repository.</param>
        /// <param name="itineraryLogRepository">The itinerary log repository.</param>
        /// <param name="timelineRepository">The timeline repository.</param>
        /// <param name="nationalJetFuelTicketRepository">The national jet fuel ticket repository.</param>
        /// <param name="jetFuelTicketRepository">The jet fuel ticket repository.</param>
        /// <param name="manifestArrivalRepository">The manifest arrival repository.</param>
        /// <param name="manifestDepartureRepository">The manifest departure repository.</param>
        /// <param name="manifestDepartureBoardingRepository">The manifest departure boarding repository.</param>
        /// <param name="airportServiceRepository">The airport service repository.</param>
        /// <param name="passengerInformationRepository">The passenger information repository.</param>
        public ItineraryBusiness(
            IUnitOfWork unitOfWork,
            IItineraryRepository itineraryRepository,
            IAirportRepository airportRepository,
            IAirplaneRepository airplaneRepository,
            IItineraryLogRepository itineraryLogRepository,
            ITimelineRepository timelineRepository,
            INationalJetFuelTicketRepository nationalJetFuelTicketRepository,
            IJetFuelTicketRepository jetFuelTicketRepository,
            IManifestArrivalRepository manifestArrivalRepository,
            IManifestDepartureRepository manifestDepartureRepository,
            IManifestDepartureBoardingRepository manifestDepartureBoardingRepository,
            IAirportServiceRepository airportServiceRepository,
            IPassengerInformationRepository passengerInformationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.itineraryRepository = itineraryRepository;
            this.airplaneRepository = airplaneRepository;
            this.airportRepository = airportRepository;
            this.itineraryLogRepository = itineraryLogRepository;
            this.timelineRepository = timelineRepository;
            this.nationalJetFuelTicketRepository = nationalJetFuelTicketRepository;
            this.jetFuelTicketRepository = jetFuelTicketRepository;
            this.manifestArrivalRepository = manifestArrivalRepository;
            this.manifestDepartureRepository = manifestDepartureRepository;
            this.manifestDepartureBoardingRepository = manifestDepartureBoardingRepository;
            this.airportServiceRepository = airportServiceRepository;
            this.passengerInformationRepository = passengerInformationRepository;
        }

        /// <summary>
        /// Gets all Flight by Actual Day
        /// </summary>
        /// <returns></returns>
        public IList<ItineraryDto> GetAllFlightsByDay()
        {
            string date = string.Empty;
            string DateActual = string.Empty;
            try
            {
                date = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                DateActual = this.Dateconvertion(date);
                IList<Itinerary> itinerary = this.itineraryRepository.GetAllFlightByDay(DateActual).ToList();
                IList<ItineraryDto> itineraryDto = new List<ItineraryDto>();
                itineraryDto = Mapper.Map<IList<Itinerary>, IList<ItineraryDto>>(itinerary);
                return itineraryDto.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Return a Detail of a flight itinerary
        /// </summary>
        /// <param name="sequence">sequence of flight</param>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        public ItineraryDto FindFlightById(int sequence, string airlineCode, string flightNumber, string itinerarykey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) && string.IsNullOrWhiteSpace(flightNumber) && string.IsNullOrWhiteSpace(itinerarykey) && sequence == 0)
            {
                return null;
            }

            try
            {
                Itinerary itinerary = this.itineraryRepository.FindById(sequence, airlineCode, flightNumber, itinerarykey);
                ItineraryDto itineraryDto = new ItineraryDto();
                itineraryDto = Mapper.Map<Itinerary, ItineraryDto>(itinerary);
                return itineraryDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add a new Flight Itinerary Individually
        /// </summary>
        /// <param name="itineraryDto"></param>
        /// <returns></returns>
        public bool AddFlightItinerary(ItineraryDto itineraryDto)
        {
            //Add ItineraryKey to Item
            string datecv = string.Empty;
            datecv = this.Dateconvertion(itineraryDto.DepartureDate.ToString());
            itineraryDto.ItineraryKey = datecv;

            if (itineraryDto == null)
            {
                return false;
            }

            if (this.isNewFlightItineraryIncorrectSequence(itineraryDto.AirlineCode,
                                                      itineraryDto.FlightNumber,
                                                      itineraryDto.ItineraryKey,
                                                      itineraryDto.DepartureStation))
            {
                throw new BusinessException(Messages.FailedInsertRecord, 20);
            }

            if (this.isNewFlightItineraryIncorrectDates(itineraryDto.AirlineCode,
                                                        itineraryDto.FlightNumber,
                                                        itineraryDto.ItineraryKey,
                                                        itineraryDto.DepartureDate))
            {
                throw new BusinessException(Messages.FailedInsertRecord, 21);
            }

            try
            {
                //Add Sequence to new Item
                int sequence = this.itineraryRepository.GetMaxAllFlightByKeyWithoutSequence(itineraryDto.AirlineCode,
                                                                                              itineraryDto.FlightNumber,
                                                                                              itineraryDto.ItineraryKey);
                sequence++;

                itineraryDto.Sequence = sequence;


                //Add
                Itinerary itinerary = new Itinerary();
                itinerary = Mapper.Map<ItineraryDto, Itinerary>(itineraryDto);
                this.itineraryRepository.Add(itinerary);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Delete a  Flight itinerary and Insert in log the movement.
        /// </summary>
        /// <param name="itineraryDto">Itinerary Data Object</param>
        /// <param name="remarks">Comments about the motive of delete</param>
        /// <returns>True or False</returns>
        public bool DeleteFlightItinerary(ItineraryDto itineraryDto, string remarks)//delete or edit
        {
            ItineraryLogDto itineraryLog = new ItineraryLogDto();
            if (itineraryDto != null)
            {
                int sequenceMax = this.itineraryRepository.GetMaxAllFlightByKeyWithoutSequence(itineraryDto.AirlineCode,
                                                                                              itineraryDto.FlightNumber,
                                                                                              itineraryDto.ItineraryKey);
                if (sequenceMax != itineraryDto.Sequence)
                {
                    throw new BusinessException(Messages.FailedDeleteRecord, 23);
                }

                try
                {
                    Itinerary itinerary = this.itineraryRepository.FindById(itineraryDto.Sequence, itineraryDto.AirlineCode, itineraryDto.FlightNumber, itineraryDto.ItineraryKey);
                    //Validar logica para insertar el registro en ItineraryLog
                    AddItemToItineraryLog(itinerary, remarks);
                    
                    //Borrar dependencias de Itinerario
                    DeleteTimeline(itinerary);
                    DeleteNationalJetFuelTicket(itinerary);
                    DeleteJetFuelTicket(itinerary);
                    DeleteManifestArrival(itinerary);
                    DeleteManifestDeparture(itinerary);
                    DeleteAirportService(itinerary);
                    DeletePassengerInformation(itinerary);

                    this.itineraryRepository.Delete(itinerary);
                    this.unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message.ToString());
                }

            }
            else
            {
                return false;
            }
        }

        private void DeletePassengerInformation(Itinerary itinerary)
        {
            var passengerInformation = this.passengerInformationRepository.FindById(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);

            if (passengerInformation != null)
            {
                //Delete Manifest Arrival
                this.passengerInformationRepository.Delete(passengerInformation);
            }
        }

        private void DeleteAirportService(Itinerary itinerary)
        {
            var arrivalServicesList = this.airportServiceRepository.GetArrivalServicesByItinerary(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);
            var departureServicesList = this.airportServiceRepository.GetDepartureServicesByItinerary(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);
            foreach (var item in arrivalServicesList)
            {
                this.airportServiceRepository.Delete(item);
            }
            foreach (var item in departureServicesList)
            {
                this.airportServiceRepository.Delete(item);
            }
        }

        private void DeleteManifestDeparture(Itinerary itinerary)
        {
            var manifestDeparture = this.manifestDepartureRepository.GetManifestDepartureForItinerary(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);

            if (manifestDeparture != null)
            {
                //Delete All Dalays
                this.manifestDepartureRepository.RemoveAllDelaysFromManifest(manifestDeparture);
                //Delete Boarding
                var manifestDepartureBoardingList = this.manifestDepartureBoardingRepository.GetBoardingsForManifest(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);
                foreach (var item in manifestDepartureBoardingList)
                {
                    this.manifestDepartureBoardingRepository.Delete(item);
                }
                //Delete Manifest Arrival
                this.manifestDepartureRepository.Delete(manifestDeparture);
            }
        }

        private void DeleteManifestArrival(Itinerary itinerary)
        {
            var manifestArrival = this.manifestArrivalRepository.GetManifestArrivalForItinerary(itinerary.Sequence, itinerary.AirlineCode, itinerary.FlightNumber, itinerary.ItineraryKey);

            if (manifestArrival != null)
            {
                //Delete All Dalays
                this.manifestArrivalRepository.RemoveAllDelaysFromManifest(manifestArrival);
                //Delete Manifest Arrival
                this.manifestArrivalRepository.Delete(manifestArrival);
            }
        }

        private void DeleteJetFuelTicket(Itinerary itinerary)
        {
            var jetFuelTicketList = this.jetFuelTicketRepository.GetJetFuelTickets(itinerary, "SALIDA");
            foreach (var item in jetFuelTicketList)
            {
                this.jetFuelTicketRepository.Delete(item);
            }
        }

        private void DeleteNationalJetFuelTicket(Itinerary itinerary)
        {
            var nationalJetFuelTicketList = this.nationalJetFuelTicketRepository.GetNationalJetFuelTickets(new NationalJetFuelTicket { Sequence = itinerary.Sequence, AirlineCode = itinerary.AirlineCode, FlightNumber = itinerary.FlightNumber, ItineraryKey = itinerary.ItineraryKey, OperationTypeName = "SALIDA" });
            foreach (var item in nationalJetFuelTicketList)
            {
                this.nationalJetFuelTicketRepository.Delete(item);
            }
        }

        private void DeleteTimeline(Itinerary itinerary)
        {
            var timeline = this.timelineRepository.GetTimelineByFlight(new Timeline { Sequence = itinerary.Sequence, AirlineCode = itinerary.AirlineCode, FlightNumber = itinerary.FlightNumber, ItineraryKey = itinerary.ItineraryKey });
            if (timeline != null)
            {
                this.timelineRepository.Delete(timeline);
            }
        }

        /// <summary>
        /// update the information of a flight itinerary
        /// </summary>
        /// <param name="itineraryDto"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public bool UpdateFlightItinerary(ItineraryDto itineraryDto, string remarks)//delete or edit
        {
            if (itineraryDto != null)
            {
                if (this.isFlightItineraryIncorrectDepartureDate(itineraryDto))
                {
                    throw new BusinessException(Messages.FailedInsertRecord, 21);
                }

                if (this.isFlightItineraryIncorrectArrivalDate(itineraryDto))
                {

                    throw new BusinessException(Messages.FailedInsertRecord, 22);
                }

                try
                {
                    Itinerary itinerary = this.itineraryRepository.FindById(itineraryDto.Sequence, itineraryDto.AirlineCode, itineraryDto.FlightNumber, itineraryDto.ItineraryKey);

                    //Validar logica para insertar el registro en ItineraryLog
                    AddItemToItineraryLog(itinerary, remarks);

                    //Agregar itinerary
                    itinerary.EquipmentNumber = itineraryDto.EquipmentNumber;
                    itinerary.DepartureDate = itineraryDto.DepartureDate;
                    //itinerary.DepartureStation = itineraryDto.DepartureStation;
                    itinerary.ArrivalDate = itineraryDto.ArrivalDate;

                    //Solo si es editable arrival
                    if (itinerary.EditArrival == true)
                    {
                        itinerary.ArrivalStation = itineraryDto.ArrivalStation;
                    }
                    this.itineraryRepository.Update(itinerary);
                    this.unitOfWork.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message.ToString());
                }
            }
            else
            {
                return false;
            }
        }

        private void AddItemToItineraryLog(Itinerary itinerary, string remarks)
        {
            ItineraryDto itineraryDtoPrevio = Mapper.Map<Itinerary, ItineraryDto>(itinerary);
            ItineraryLogDto itineraryLogDto = new ItineraryLogDto();
            ItineraryLog itineraryLog = new ItineraryLog();

            itineraryLogDto = Mapper.Map<ItineraryDto, ItineraryLogDto>(itineraryDtoPrevio);
            itineraryLogDto.EndDate = DateTime.Now;
            itineraryLogDto.Remarks = remarks;

            itineraryLog = Mapper.Map<ItineraryLogDto, ItineraryLog>(itineraryLogDto);
            this.itineraryLogRepository.Add(itineraryLog);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Add Itinerary Log With Out Commit
        /// </summary>
        /// <param name="itinerary"></param>
        /// <param name="remarks"></param>
        private void AddItineraryLog(Itinerary itinerary, string remarks = "Actulizado desde la carga masiva")
        {
            ItineraryDto itineraryDtoPrevio = Mapper.Map<Itinerary, ItineraryDto>(itinerary);
            ItineraryLogDto itineraryLogDto = new ItineraryLogDto();
            ItineraryLog itineraryLog = new ItineraryLog();

            itineraryLogDto = Mapper.Map<ItineraryDto, ItineraryLogDto>(itineraryDtoPrevio);
            itineraryLogDto.EndDate = DateTime.Now;
            itineraryLogDto.Remarks = remarks;

            itineraryLog = Mapper.Map<ItineraryLogDto, ItineraryLog>(itineraryLogDto);
            this.itineraryLogRepository.Add(itineraryLog);
        }

        /// <summary>
        /// Method to convert the data for the itineraryDto
        /// </summary>
        /// <param name="itineraryFDto"></param>
        /// <param name="airlineCode">Airline Code</param>
        /// <returns></returns>
        public IList<string> ValidateItinerayFile(IList<ItineraryFileDto> itineraryFDto, string airlineCode)
        {
            IList<string> errors = new List<string>();
            IList<ItineraryDto> itineraryListDto = new List<ItineraryDto>();
            IList<string> datelist = new List<string>();
            string dateconvert = string.Empty;
            try
            {
                var datedistinct = (from ssi in itineraryFDto
                                    group ssi by new { ssi.FLT_ORG } into g
                                    select new { FLT_ORG = g.Key.FLT_ORG }).ToList();

                foreach (var date in datedistinct)
                {
                    dateconvert = this.Dateconvertion(date.FLT_ORG);
                    datelist.Add(dateconvert);
                }
                //Omite registros con campos vacios
                var itinerayWOS = (from ssi in itineraryFDto
                                   group ssi by new { ssi.FLT, ssi.AC_REG, ssi.FLT_ORG, ssi.F4, ssi.OUT, ssi.OFF, ssi.ON, ssi.IN, ssi.SKD, ssi.DESIGNATOR } into g
                                   select new
                                   {
                                       FLT = g.Key.FLT,
                                       AC_REG = g.Key.AC_REG,
                                       FLT_ORG = g.Key.FLT_ORG,
                                       F4 = g.Key.F4,
                                       OUT = g.Key.OUT,
                                       OFF = g.Key.OFF,
                                       ON = g.Key.ON,
                                       IN = g.Key.IN,
                                       SKD = g.Key.SKD,
                                       DESIGNATOR = g.Key.DESIGNATOR
                                   }).Where(e => (e.IN != string.Empty)
                                              && (e.OUT != string.Empty)
                                              && (e.IN.Length == 4)
                                              && (e.OUT.Length == 4)).ToList();
                //--------------------------------------------------
                //Validación de Aeropuertos de salida y llegada de archivo con catálogos de Aeropuertos.
                var airportsDistinct = (from ssa in itinerayWOS
                                        group ssa by new { ssa.FLT, ssa.F4, ssa.SKD } into h
                                        select new { FLT = h.Key.FLT, F4 = h.Key.F4, SKD = h.Key.SKD }).ToList();
                IList<AirportDto> airportDtoList = new List<AirportDto>();
                airportDtoList = Mapper.Map<IList<AirportDto>>(this.airportRepository.GetActivesAirports());
                foreach (var itemAirport in airportsDistinct)
                {
                    var airportdeparture = airportDtoList.FirstOrDefault(e => e.StationCode == itemAirport.F4);
                    if (airportdeparture == null)
                    {
                        errors.Add("There is no Departure Airport with code: " + itemAirport.F4 + " and flight: " + itemAirport.FLT + ".");
                    }

                    var airportArrival = airportDtoList.FirstOrDefault(f => f.StationCode == itemAirport.SKD);
                    if (airportArrival == null)
                    {
                        errors.Add("There is no Arrival Airport with code: " + itemAirport.SKD + " and flight: " + itemAirport.FLT + ".");
                    }
                }
                //--------------------------------------------------
                //Validación de Matriculas de Avión si existen en BD
                var equipmentnumberDistinct = (from sse in itinerayWOS
                                               group sse by new { sse.FLT, sse.AC_REG } into p
                                               select new { FLT = p.Key.FLT, AC_REG = p.Key.AC_REG }).ToList();
                IList<AirplaneDto> airplaneDtoList = new List<AirplaneDto>();
                airplaneDtoList = Mapper.Map<IList<AirplaneDto>>(this.airplaneRepository.GetActiveAirplane());
                foreach (var itemAirplane in equipmentnumberDistinct)
                {
                    var equipmentnumber = airplaneDtoList.FirstOrDefault(j => j.EquipmentNumber == itemAirplane.AC_REG);
                    if (equipmentnumber == null)
                    {
                        errors.Add("There is no airplane with the following Equipment Number: " + itemAirplane.AC_REG + " for the flight: " + itemAirplane.FLT + ".");
                    }
                }

                //validacion de Vuelos Repetidos entre archivos y BD
                IList<ItineraryDto> itineraryListDtoToCompare = new List<ItineraryDto>();
                List<ItineraryDto> itineraryListGnlDto = new List<ItineraryDto>();
                foreach (var itemdate in datelist)
                {
                    itineraryListDtoToCompare = Mapper.Map<IList<ItineraryDto>>(this.itineraryRepository.GetAllFlightByDay(itemdate));
                    itineraryListGnlDto.AddRange(itineraryListDtoToCompare);
                }
                //Validacion de Vuelos de archivo con BD.
                int countErrors = 0;
                if (itineraryListGnlDto.Count != 0)
                {
                    foreach (var item in itinerayWOS)
                    {
                        ////Cantidad de errores a mostrar
                        if (countErrors <= 10)
                        {
                            string date = string.Empty;
                            string date1x1 = string.Empty;
                            string datetobecompared = string.Empty;
                            date1x1 = Convert.ToDateTime(item.FLT_ORG).ToShortDateString();
                            datetobecompared = this.Dateconvertion(date1x1);


                            if (itineraryListGnlDto.Count > 0)
                            {
                                foreach (var itemBD in itineraryListGnlDto)
                                {
                                    if (airlineCode == itemBD.AirlineCode && item.FLT == itemBD.FlightNumber && item.AC_REG == itemBD.EquipmentNumber && datetobecompared == itemBD.ItineraryKey)
                                    {
                                        errors.Add("There a Flight already registered with the following characteristics: Airline: " + itemBD.AirlineCode +
                                                                                                                        ", Flight Number: " + itemBD.FlightNumber +
                                                                                                                        ", Equipment Numner: " + itemBD.EquipmentNumber +
                                                                                                                        ", Flight Date: " + itemBD.ItineraryKey + ".");
                                        countErrors = countErrors + 1;
                                    }
                                }
                            }

                            if (item.F4 == item.SKD)
                            {
                                errors.Add("The Departure Station is equal to Arrival Station in Flight: " + item.FLT + ".");
                            }
                        }
                    }
                }
                //-----------------------------------------------------
                if (errors.Count == 0)
                {
                    foreach (var item in itinerayWOS)
                    {
                        ItineraryDto itineraryDto = new ItineraryDto();
                        string date = string.Empty;
                        string datecv = string.Empty;
                        DateTime dateflightD = new DateTime();
                        DateTime dateflightA = new DateTime();
                        //Tratamiento de Fechas
                        date = Convert.ToDateTime(item.FLT_ORG).ToShortDateString();
                        datecv = this.Dateconvertion(date);
                        dateflightD = Convert.ToDateTime(date);
                        //------------------------------------
                        itineraryDto.AirlineCode = airlineCode;
                        itineraryDto.FlightNumber = item.FLT;
                        itineraryDto.ItineraryKey = datecv;
                        itineraryDto.EquipmentNumber = item.AC_REG;
                        //Tratamiento de Horas para Departure Date
                        TimeSpan timedp = this.TimeSpanConversion(item.OUT);
                        itineraryDto.DepartureDate = dateflightD.Add(timedp);
                        //------------------------------------
                        itineraryDto.DepartureStation = item.F4;
                        //Tratamiento de Horas pra Arrival Date
                        TimeSpan timeAr = this.TimeSpanConversion(item.IN);
                        if (Convert.ToInt16(item.IN) < Convert.ToInt16(item.OUT))
                        {
                            dateflightA = dateflightD.AddDays(1);
                            itineraryDto.ArrivalDate = dateflightA.Add(timeAr);
                        }
                        else
                        {
                            itineraryDto.ArrivalDate = dateflightD.Add(timeAr);
                        }
                        //--------------------------------------
                        itineraryDto.ArrivalStation = item.SKD;
                        itineraryListDto.Add(itineraryDto);
                    }
                    itineraryListDto = UtilityBusiness.AddSequenceItineraryDto(itineraryListDto);
                    this.AddMassiveItinerary(itineraryListDto);
                    return errors;
                }
                else
                {
                    return errors;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }


        /// <summary>
        /// Gets the details of a flight in case exists the methis return a true and the insert fails.
        /// </summary>
        /// <param name="departureCode">sequence of flight</param>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        private bool isNewFlightItineraryIncorrectSequence(string airlineCode, string flightNumber, string itinerarykey, string departureCode)
        {
            int sequenceValMax = this.itineraryRepository.GetMaxAllFlightByKeyWithoutSequence(airlineCode,
                                                                                             flightNumber,
                                                                                             itinerarykey);

            Itinerary itineraries = this.itineraryRepository.FindById(sequenceValMax, airlineCode, flightNumber, itinerarykey);

            if ((itineraries != null && itineraries.ArrivalStation == departureCode) || itineraries == null)
                return false;
            else
                return true;
        }


        private bool isNewFlightItineraryIncorrectDates(string airlineCode, string flightNumber, string itinerarykey, DateTime departureDate)
        {
            //Se compara contra el ultimo en secuencia
            int sequenceValMax = this.itineraryRepository.GetMaxAllFlightByKeyWithoutSequence(airlineCode,
                                                                                         flightNumber,
                                                                                         itinerarykey);

            Itinerary itineraries = this.itineraryRepository.FindById(sequenceValMax, airlineCode, flightNumber, itinerarykey);

            //No es incorrecto mientras cumpla con que la fecha de llegada sea menor o igual a la se salida del que agregare
            if ((itineraries != null && itineraries.ArrivalDate < departureDate) || itineraries == null)
                return false;
            else
                return true;
        }

        private bool isFlightItineraryIncorrectDepartureDate(ItineraryDto itineraryExisting)
        {
            //Anterior
            Itinerary itineraryPreview = this.itineraryRepository.FindById(itineraryExisting.Sequence - 1, itineraryExisting.AirlineCode, itineraryExisting.FlightNumber, itineraryExisting.ItineraryKey);
            ////Siguiente
            //Itinerary itineraryNext = this.itineraryRepository.FindById(itineraryExisting.Sequence + 1, itineraryExisting.AirlineCode, itineraryExisting.FlightNumber, itineraryExisting.ItineraryKey);

            //No es incorrecto mientras cumpla con que la fecha de llegada anterior sea menor o igual a la se salida del que agregare
            if ((itineraryPreview != null && itineraryPreview.ArrivalDate < itineraryExisting.DepartureDate) || itineraryPreview == null)
                return false;
            else
                return true;

            ////No es incorrecto mientras cumpla con que la fecha de llegada que agregare sea menor o igual a la se salida del siguiente
            //if ((itineraryNext != null && itineraryExisting.ArrivalDate < itineraryNext.DepartureDate) || itineraryNext == null)
            //    return false;
            //else
            //    return true;
        }

        private bool isFlightItineraryIncorrectArrivalDate(ItineraryDto itineraryExisting)
        {
            ////Anterior
            //Itinerary itineraryPreview = this.itineraryRepository.FindById(itineraryExisting.Sequence - 1, itineraryExisting.AirlineCode, itineraryExisting.FlightNumber, itineraryExisting.ItineraryKey);
            //Siguiente
            Itinerary itineraryNext = this.itineraryRepository.FindById(itineraryExisting.Sequence + 1, itineraryExisting.AirlineCode, itineraryExisting.FlightNumber, itineraryExisting.ItineraryKey);

            ////No es incorrecto mientras cumpla con que la fecha de llegada anterior sea menor o igual a la se salida del que agregare
            //if ((itineraryPreview != null && itineraryPreview.ArrivalDate < itineraryExisting.DepartureDate) || itineraryPreview == null)
            //    return false;
            //else
            //    return true;

            //No es incorrecto mientras cumpla con que la fecha de llegada que agregare sea menor o igual a la se salida del siguiente
            if ((itineraryNext != null && itineraryExisting.ArrivalDate < itineraryNext.DepartureDate) || itineraryNext == null)
                return false;
            else
                return true;
        }

        private bool AddMassiveItinerary(IList<ItineraryDto> itineraryDtoList)
        {
            foreach (ItineraryDto itineraryDto in itineraryDtoList)
            {
                if (itineraryDto == null)
                {
                    return false;
                }
            }

            try
            {
                foreach (ItineraryDto itineraryDto in itineraryDtoList)
                {
                    Itinerary itinerary = new Itinerary();
                    itinerary = Mapper.Map<Itinerary>(itineraryDto);
                    this.itineraryRepository.Add(itinerary);
                    this.unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Convert the date to yyyyMMdd
        /// </summary>
        /// <param name="date">date to convert</param>
        /// <returns></returns>
        private string Dateconvertion(string date)
        {
            string datetime = string.Empty;
            DateTime datetimeconverted = new DateTime();
            string month = string.Empty;
            string day = string.Empty;
            datetimeconverted = Convert.ToDateTime(date);
            if (datetimeconverted.Month.ToString().Length == 1)
            {
                month = "0" + datetimeconverted.Month.ToString();
            }
            else
            {
                month = datetimeconverted.Month.ToString();
            }

            if (datetimeconverted.Day.ToString().Length == 1)
            {
                day = "0" + datetimeconverted.Day.ToString();
            }
            else
            {
                day = datetimeconverted.Day.ToString();
            }
            datetime = datetimeconverted.Year + month + day;
            return datetime;
        }

        /// <summary>
        /// Obtiene los valores de Hora y minutos
        /// </summary>
        /// <param name="HHmm">string witn the time.</param>
        /// <returns></returns>
        private TimeSpan TimeSpanConversion(string HHmm)
        {
            string time = string.Empty;
            string hour = string.Empty;
            string mins = string.Empty;
            int i = 0;

            time = HHmm;
            hour = time.Substring(0, 2);
            i = time.Length;
            mins = time.Substring((i - 2), 2);
            TimeSpan timeHHmm = new TimeSpan(Convert.ToInt16(hour), Convert.ToInt16(mins), 00);
            return timeHHmm;
        }

        /// <summary>
        /// Count All Fligths
        /// </summary>
        /// <returns></returns>
        public int CountAll()
        {
            int total = 0;
            try
            {
                total = this.itineraryRepository.CountAll();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return total;
        }

        /// <summary>
        /// Get only page items
        /// </summary>
        /// <param name="pagesize">page size</param>
        /// <param name="pagenumber">page number</param>
        /// <returns>itineraries order by departureDate</returns>
        public IList<ItineraryDto> paginationListbyDay(int pagesize, int pagenumber)
        {
            IList<ItineraryDto> itineraries = new List<ItineraryDto>();
            IList<Itinerary> itinerariesEntity = new List<Itinerary>();
            try
            {
                itinerariesEntity = this.itineraryRepository.paginationListbyDay(pagesize, pagenumber);
                itineraries = Mapper.Map<IList<ItineraryDto>>(itinerariesEntity);
                GetTime(itineraries);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return itineraries;
        }

        /// <summary>
        /// Count All by Day
        /// </summary>
        /// <returns></returns>
        public int CountAllDay()
        {
            int total = 0;
            try
            {
                total = this.itineraryRepository.CountAllDay();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return total;
        }

        /// <summary>
        /// Advance Search Itinerary
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<ItineraryDto> AdvanceSearchItinerary(ItinerarySearchDto search)
        {
            IList<ItineraryDto> itineraries = new List<ItineraryDto>();
            IList<Itinerary> entities = new List<Itinerary>();
            try
            {
                int skip = (search.Pagenumber - 1) * search.Pagesize;
                entities = this.itineraryRepository.AdvanceSearchItinerary();
                entities = SearchBussiness(search, entities);
                entities = entities.OrderBy(c => c.DepartureDate)
                    .Skip(skip).Take(search.Pagesize).ToList();
                itineraries = Mapper.Map<IList<ItineraryDto>>(entities);
                GetTime(itineraries);

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return itineraries;
        }

        public IList<ItineraryDto> AdvanceSearchItineraryPrevious(ItinerarySearchDto search)
        {
            IList<ItineraryDto> itineraries = new List<ItineraryDto>();
            IList<Itinerary> entities = new List<Itinerary>();
            try
            {
                int skip = (search.Pagenumber - 1) * search.Pagesize;
                entities = this.itineraryRepository.AdvanceSearchItinerary();
                entities = SearchBussinessPrevious(search, entities);
                entities = entities.OrderBy(c => c.DepartureDate)
                    .Skip(skip).Take(search.Pagesize).ToList();
                itineraries = Mapper.Map<IList<ItineraryDto>>(entities);
                GetTime(itineraries);

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return itineraries;
        }

        /// <summary>
        /// Get time departure and arrival
        /// </summary>
        /// <param name="itineraries"></param>
        private static void GetTime(IList<ItineraryDto> itineraries)
        {
            foreach (var item in itineraries)
            {
                item.DepartureTime = new TimeSpan(item.DepartureDate.Hour, item.DepartureDate.Minute, 0);
                item.ArriveTime = new TimeSpan(item.ArrivalDate.Hour, item.ArrivalDate.Minute, 0);
            }
        }

        /// <summary>
        /// Count Advance Search Itinerary
        /// </summary>
        /// <param name="search"></param> 
        /// <returns></returns>
        public int CountAdvanceSearchItinerary(ItinerarySearchDto search)
        {
            int countSearch = 0;
            IList<Itinerary> entities = new List<Itinerary>();
            try
            {
                entities = this.itineraryRepository.AdvanceSearchItinerary();
                entities = SearchBussiness(search, entities);

                countSearch = entities.Count();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return countSearch;
        }

        /// <summary>
        /// Counts the advance search itinerary previous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int CountAdvanceSearchItineraryPrevious(ItinerarySearchDto search)
        {
            int countSearch = 0;
            IList<Itinerary> entities = new List<Itinerary>();
            try
            {
                entities = this.itineraryRepository.AdvanceSearchItinerary();
                entities = SearchBussinessPrevious(search, entities);

                countSearch = entities.Count();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return countSearch;
        }

        /// <summary>
        /// Search Bussiness logic
        /// </summary>
        /// <param name="search"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        private static IList<Itinerary> SearchBussiness(ItinerarySearchDto search, IList<Itinerary> entities)
        {
            if (!string.IsNullOrEmpty(search.AirlineCode))
                entities = entities.Where(c => c.AirlineCode == search.AirlineCode).ToList();

            if (!string.IsNullOrEmpty(search.FlightNumber))
                entities = entities.Where(c => c.FlightNumber == search.FlightNumber).ToList();

            if (!string.IsNullOrEmpty(search.EquipmentNumber))
                entities = entities.Where(c => c.EquipmentNumber == search.EquipmentNumber).ToList();

            if (!string.IsNullOrEmpty(search.LocalizationStation))
            {
                entities = entities.Where(c => c.DepartureStation == search.LocalizationStation
               || c.ArrivalStation == search.LocalizationStation).ToList();
            }
            else
            {
                if (!string.IsNullOrEmpty(search.DepartureStation))
                    entities = entities.Where(c => c.DepartureStation == search.DepartureStation).ToList();

                if (!string.IsNullOrEmpty(search.ArrivalStation))
                    entities = entities.Where(c => c.ArrivalStation == search.ArrivalStation).ToList();
            }

            if (search.DepartureDate != null && search.DepartureDate != DateTime.MinValue && search.ArrivalDate != null && search.ArrivalDate != DateTime.MinValue)
            {
                if (search.DepartureDate.Date == search.ArrivalDate.Date)
                {
                    entities = entities.Where(c => c.DepartureDate.Date == search.DepartureDate.Date).ToList();
                }
                else
                {
                    entities = entities.Where(c => c.DepartureDate.Date >= search.DepartureDate.Date
                        && c.DepartureDate.Date <= search.ArrivalDate.Date).ToList();
                }
            }

            return entities;
        }

        /// <summary>
        /// Searches the bussiness previous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        private static IList<Itinerary> SearchBussinessPrevious(ItinerarySearchDto search, IList<Itinerary> entities)
        {
            //AirlineCode NULL
            //ArrivalStation Debe ser DepartureStation de Vuelo Actual pues buscare conexiones anteriores
            //ArrivarDate Debe ser menor o igual que DepartureDate de Vuelo Actual pues buscare conexiones anteriores

            if (!string.IsNullOrEmpty(search.FlightNumber))
                entities = entities.Where(c => c.FlightNumber == search.FlightNumber).ToList();

            if (!string.IsNullOrEmpty(search.EquipmentNumber))
                entities = entities.Where(c => c.EquipmentNumber == search.EquipmentNumber).ToList();

            if (!string.IsNullOrEmpty(search.DepartureStation))
                entities = entities.Where(c => c.DepartureStation == search.DepartureStation).ToList();

            if (!string.IsNullOrEmpty(search.ArrivalStation))
                entities = entities.Where(c => c.ArrivalStation == search.ArrivalStation).ToList();

            if (search.DepartureDate != null && search.DepartureDate != DateTime.MinValue)
            {
                entities = entities.Where(c => c.DepartureDate.Date == search.DepartureDate.Date).ToList();
            }

            if (search.ArrivalDate != null && search.ArrivalDate != DateTime.MinValue)
            {
                entities = entities.Where(c => c.ArrivalDate.Date <= search.ArrivalDate.Date).ToList();
            }

            return entities;
        }

        /// <summary>
        /// Add Or Update Itinerary
        /// </summary>
        /// <param name="itinerariesDto"></param>
        /// <returns></returns>
        public bool AddOrUpdateItinerary(IList<ItineraryDto> itinerariesDto)
        {
            var sucess = false;

            try
            {
                var itinerariesEnt = new List<Itinerary>();

                foreach (var item in itinerariesDto)
                {
                    var itinerary = Mapper.Map<Itinerary>(item);
                    var dbItinerary = this.itineraryRepository.FindByParams(itinerary);

                    if (dbItinerary != null)
                    {
                        if (item.EquipmentNumber != dbItinerary.EquipmentNumber
                            || item.ArrivalStation != dbItinerary.ArrivalStation)
                        {
                            AddItineraryLog(dbItinerary);
                            dbItinerary.EquipmentNumber = item.EquipmentNumber;
                            dbItinerary.ArrivalStation = item.ArrivalStation;
                            this.itineraryRepository.Update(dbItinerary);
                        }
                    }
                    else
                    {
                        itinerary.Add = true;
                    }
                    itinerariesEnt.Add(itinerary);
                }

                if (itinerariesEnt != null && itinerariesEnt.Count > 0)
                {
                    var orderItineraries = UtilityBusiness.AddSequenceItinerary(itinerariesEnt);
                    var addList = orderItineraries.Where(c => c.Add).ToList();
                    if (addList != null && addList.Count > 0)
                    {
                        this.itineraryRepository.AddRangeItinerary(addList);
                    }
                }

                this.unitOfWork.Commit();
                sucess = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.TraceError(ex.Message, ex);
                if (ex.InnerException != null)
                {
                    Trace.TraceError(ex.Message, ex.InnerException);
                }

                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return sucess;
        }

        /// <summary>
        /// Gets the details of a flight with only the passenger information.
        /// </summary>
        /// <param name="sequence">The sequence of flight.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight Number of the airplane.</param>
        /// <param name="itineraryKey">The itinerary identifier.</param>
        /// <returns>
        /// The information of a flight with the passenger information object.
        /// </returns>
        public ItineraryDto FindItineraryWithPassengerInformation(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                && string.IsNullOrWhiteSpace(flightNumber)
                && string.IsNullOrWhiteSpace(itineraryKey)
                && sequence == 0)
            {
                return null;
            }

            try
            {
                Itinerary itinerary = this.itineraryRepository.GetItineraryWithDeclarationsAndPassengerInformation(sequence, airlineCode, flightNumber, itineraryKey);
                ItineraryDto itineraryDto = new ItineraryDto();
                itineraryDto = Mapper.Map<Itinerary, ItineraryDto>(itinerary);
                return itineraryDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}