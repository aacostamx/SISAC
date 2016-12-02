//------------------------------------------------------------------------
// <copyright file="AircraftMovementMessageBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Implementation for the aircraft movement message
    /// </summary>
    public class AircraftMovementMessageBusiness : IAircraftMovementMessageBusiness
    {
        /// <summary>
        /// The Itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// The national jet fuel repository
        /// </summary>
        private readonly INationalJetFuelTicketRepository nationalJetFuelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AircraftMovementMessageBusiness"/> class.
        /// </summary>
        /// <param name="itineraryRepository">The itinerary repository.</param>
        /// <param name="nationalJetFuelRepository">The national jet fuel repository.</param>
        public AircraftMovementMessageBusiness(
            IItineraryRepository itineraryRepository,
            INationalJetFuelTicketRepository nationalJetFuelRepository)
        {
            this.itineraryRepository = itineraryRepository;
            this.nationalJetFuelRepository = nationalJetFuelRepository;
        }

        /// <summary>
        /// Gets the information for the MTV message.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// Returns the information for the MVT message.
        /// </returns>
        public AircraftMovementMessageDto GetAircraftMovementMessage(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            AircraftMovementMessageDto message = new AircraftMovementMessageDto();

            try
            {
                Itinerary itinerary = this.itineraryRepository.GetItineraryInformationForMessageMVT(sequence, airlineCode, flightNumber, itineraryKey);
                if (itinerary != null)
                {
                    string commanderInfo = string.Empty;
                    string stewardessInfo = string.Empty;

                    bool isAirportFromMex = this.itineraryRepository.IsDepartureStationFromMexico(
                        itinerary.Sequence,
                        itinerary.AirlineCode,
                        itinerary.FlightNumber,
                        itinerary.ItineraryKey);

                    // Title
                    message.Title = "MVT";

                    // (C1) Departure information { eg. VOI 792/23 JUL. XAVOM. MEX }
                    message.DepartureInformation = string.Format(
                        @"VOI {0}/{1} {2}. {3}. {4}",
                        itinerary.FlightNumber, // Flight
                        itinerary.DepartureDate.ToString("dd"), // Minutes
                        itinerary.DepartureDate.ToString("MMM", CultureInfo.CreateSpecificCulture("en-US")), // Month 
                        itinerary.EquipmentNumber, // Aircraft registration
                        itinerary.DepartureStation); // Departure airport

                    // (C2) Arrival information { eg. AD 0641/0705 EA 0742 CUL }
                    message.ArrivalInformation = SetArrivalInformationForMvt(itinerary, isAirportFromMex);

                    // (iNFOCM) Sets the information from the Jet fuel tickets {eg. FI 7500/5711lts    Rem. 1520199302}
                    message.JetFuelInformation = this.SetJetFuelInformationForMvt(itinerary, isAirportFromMex);

                    // Sets the delays information { eg. DL 00:1 AT ABASTECIMIENTO TARDIO DE COMISARIATO }
                    message.DelaysInformation = SetDelaysInformationForMvt(itinerary, isAirportFromMex);

                    // Sets the crew information { eg. SI.- C. MALDONADO L. DE LOS REYES }
                    SetCrewInformation(itinerary, ref commanderInfo, ref stewardessInfo, isAirportFromMex);
                    message.CaptainsInformation = commanderInfo;
                    message.StewardessInformation = stewardessInfo;

                    // Subtitle
                    message.ChargeInformationTitle = "LDM";

                    // Set boarding and charge information
                    message.ChargeInformation = SetChargeInformation(itinerary, isAirportFromMex);
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + " " + Messages.SeeInnerException, exception);
            }

            return message;
        }

        /// <summary>
        /// Sets the crew information.
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="commanderInfo">The commander information.</param>
        /// <param name="stewardessInfo">The stewardess information.</param>
        /// <param name="isAirportFromMex">if set to <c>true</c> the airport is from Mexico.</param>
        private static void SetCrewInformation(Itinerary itinerary, ref string commanderInfo, ref string stewardessInfo, bool isAirportFromMex)
        {
            commanderInfo = "SI.- ";
            stewardessInfo = "SI.- ";
            if (isAirportFromMex)
            {
                if (itinerary.ManifestDeparture != null)
                {
                    commanderInfo += itinerary.ManifestDeparture.NickNameCommander + " " +
                        itinerary.ManifestDeparture.NickNameFirstOfficial + " " +
                        itinerary.ManifestDeparture.NickNameSecondOfficial + " " +
                        itinerary.ManifestDeparture.NickNameThirdOfficial;

                    stewardessInfo += itinerary.ManifestDeparture.NickNameChiefCabinet + " " +
                        itinerary.ManifestDeparture.NickNameFirstSupercargo + " " +
                        itinerary.ManifestDeparture.NickNameSecondSupercargo + " " +
                        itinerary.ManifestDeparture.NickNameThirdSupercargo;
                }
            }
            else
            {
                if (itinerary.GendecDepartures != null && itinerary.GendecDepartures.Crews != null)
                {
                    var crews = itinerary.GendecDepartures.Crews.ToList();
                    for (byte i = 0; i < crews.Count; i++)
                    {
                        switch (crews[i].CrewTypeID)
                        {
                            case "CAP":
                                commanderInfo += crews[i].NickName + " ";
                                break;

                            case "COP":
                                commanderInfo += crews[i].NickName + " ";
                                break;

                            case "JDC":
                                stewardessInfo += crews[i].NickName + " ";
                                break;

                            case "SOB":
                                stewardessInfo += crews[i].NickName + " ";
                                break;
                        }
                    }
                }
            }

            commanderInfo = commanderInfo.Trim();
            stewardessInfo = stewardessInfo.Trim();
        }

        /// <summary>
        /// Sets the boarding and charge information.
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="isAirportFromMex">if set to <c>true</c> the airport is from Mexico.</param>
        /// <returns>List of boarding and charge information.</returns>
        private static List<string> SetChargeInformation(Itinerary itinerary, bool isAirportFromMex)
        {
            if (!isAirportFromMex)
            {
                return new List<string>();
            }

            string chargeDesc = string.Empty;
            List<string> chargeInfo = new List<string>();

            // Finds if the manifest has Delays registered
            if (itinerary.ManifestDeparture != null && itinerary.ManifestDeparture.ManifestDepartureBoardings != null)
            {
                List<ManifestDepartureBoarding> boardings = itinerary.ManifestDeparture.ManifestDepartureBoardings.ToList();
                for (byte i = 0; i < boardings.Count; i++)
                {
                    string luggage = string.Empty;
                    string charge = string.Empty;
                    List<ManifestDepartureBoardingDetail> details = boardings[i].ManifestDepartureBoardingDetails.ToList();
                    for (byte j = 0; j < details.Count; j++)
                    {
                        luggage += details[j].LuggageQuantity + "-" + details[j].LuggageKgs + " ";
                        charge += details[j].ChargeQuantity + "-" + details[j].ChargeKgs + " ";
                    }

                    charge = string.Format(
                                @"TTL {0}/{1}/{2} T{3} B {4} C {5}",
                                boardings[i].PassengerAdult,
                                boardings[i].PassengerMinor,
                                boardings[i].PassengerInfant,
                                boardings[i].LuggageKgs + boardings[i].ChargeKgs,
                                luggage.Trim(),
                                charge.Trim());

                    chargeInfo.Add(charge);
                }
            }

            return chargeInfo;
        }

        /// <summary>
        /// Sets the delays information for MVT. {For each delay: DL 00:1 AT ABASTECIMIENTO TARDIO DE COMISARIATO}
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="isAirportFromMex">if set to <c>true</c> the airport is from Mexico.</param>
        /// <returns>List of delays.</returns>
        private static List<string> SetDelaysInformationForMvt(Itinerary itinerary, bool isAirportFromMex)
        {
            if (!isAirportFromMex)
            {
                return new List<string>();
            }

            string delayDescription = string.Empty;
            List<string> delays = new List<string>();

            // Finds if the manifest has Delays registered
            if (itinerary.ManifestDeparture != null && itinerary.ManifestDeparture.Delays != null)
            {
                for (byte i = 0; i < itinerary.ManifestDeparture.Delays.Count; i++)
                {
                    TimeSpan diff = itinerary.DepartureDate - itinerary.ManifestDeparture.ActualDepartureDate;
                    double totalMinutes = diff.TotalMinutes > 0 ? diff.TotalMinutes : diff.TotalMinutes * -1;
                    delayDescription = string.Format(
                                @"DL 00:{0} {1} - {2}",
                                totalMinutes, // The difference between the itinerary date and the actual date, in minutes.
                                itinerary.ManifestDeparture.Delays[i].DelayCode, // Delay code
                                itinerary.ManifestDeparture.Delays[i].DelayName); // Delay description

                    delays.Add(delayDescription);
                }
            }
            else
            {
                // If the Manifest does not have registered delays then finds the delays in the additional information for the MEX airport
                if (itinerary.ManifestDeparture.AdditionalDepartureInformation != null)
                {
                    TimeSpan diff = itinerary.DepartureDate.TimeOfDay - itinerary.ManifestDeparture.AdditionalDepartureInformation.PositionOutputTime.Value;
                    delayDescription = SetDelayDescription(diff.TotalMinutes, itinerary.ManifestDeparture.AdditionalDepartureInformation.DelayDescription1);
                    if (delayDescription != string.Empty)
                    {
                        delays.Add(delayDescription);
                    }

                    delayDescription = SetDelayDescription(diff.TotalMinutes, itinerary.ManifestDeparture.AdditionalDepartureInformation.DelayDescription2);
                    if (delayDescription != string.Empty)
                    {
                        delays.Add(delayDescription);
                    }

                    delayDescription = SetDelayDescription(diff.TotalMinutes, itinerary.ManifestDeparture.AdditionalDepartureInformation.DelayDescription3);
                    if (delayDescription != string.Empty)
                    {
                        delays.Add(delayDescription);
                    }
                }
            }

            return delays;
        }

        /// <summary>
        /// Sets the delay description.
        /// </summary>
        /// <param name="difference">The difference.</param>
        /// <param name="description">The description.</param>
        /// <returns>List with the delays.</returns>
        private static string SetDelayDescription(double difference, string description)
        {
            if (description == null)
            {
                return string.Empty;
            }

            string desc = description.Substring(description.IndexOf('#')) + description.Substring(0, description.IndexOf('#'));
            return string.Format(@"DL 00:{0} {1}", difference, desc);
        }

        /// <summary>
        /// Sets the arrival information for MVT. { AD 0641/0705 EA 0742 CUL }
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="isAirportFromMex">if set to <c>true</c> the airport is from Mexico.</param>
        /// <returns>The text for the arrival information.</returns>
        private static string SetArrivalInformationForMvt(Itinerary itinerary, bool isAirportFromMex)
        {
            string text = string.Empty;

            // Esta información proviene de un moviento de la Línea de Tiempo que tenga como descripción Departure. De momento se
            // tomará la fecha del Manifiesto de salida o el GENDEC de salida. Del Manifiesto se tomará [ActualDepartureStation]
            // y del GENDEC se tomará [ActualTimeOfDeparture].
            if (isAirportFromMex)
            {
                if (itinerary.ManifestDeparture != null)
                {
                    text = string.Format(
                        @"AD {0}{1}/{2}{3} EA {4}{5} {6}",
                        itinerary.ManifestDeparture.ActualDepartureDate.ToString("hh"), // Hours of the actual departure date
                        itinerary.ManifestDeparture.ActualDepartureDate.ToString("mm"), // Minutes of the actual departure date
                        itinerary.ManifestDeparture.ActualDepartureDate.ToString("hh"), // Este campo se cambiaría por el que esta en Línea de tiempo
                        itinerary.ManifestDeparture.ActualDepartureDate.ToString("mm"), // Este campo se cambiaría por el que esta en Línea de tiempo
                        itinerary.ArrivalDate.ToString("hh"), // Arrival hour from the itinerary
                        itinerary.ArrivalDate.ToString("mm"), // Arrival minute from the itinerary
                        itinerary.ArrivalStation);
                }
            }
            else
            {
                if (itinerary.GendecDepartures != null)
                {
                    text = string.Format(
                        @"AD {0}{1}/{2}{3} EA {4}{5} {6}",
                        itinerary.GendecDepartures.ActualTimeOfDeparture.ToString("hh"), // Hours of the actual departure date from GENDEC
                        itinerary.GendecDepartures.ActualTimeOfDeparture.ToString("mm"), // Minutes of the actual departure date from GENDEC
                        itinerary.GendecDepartures.ActualTimeOfDeparture.ToString("hh"), // Este campo se cambiaría por el que esta en Línea de tiempo
                        itinerary.GendecDepartures.ActualTimeOfDeparture.ToString("mm"), // Este campo se cambiaría por el que esta en Línea de tiempo
                        itinerary.ArrivalDate.ToString("hh"), // Arrival hour from the itinerary
                        itinerary.ArrivalDate.ToString("mm"), // Arrival minute from the itinerary
                        itinerary.ArrivalStation);
                }
            }

            return text;
        }

        /// <summary>
        /// Sets the jet fuel information for MVT. { FI 7500/5711LTS    Rem. 1520199302 }
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="isAirportFromMex">if set to <c>true</c> the airport is from Mexico.</param>
        /// <returns>Text for the jet fuel information.</returns>
        private string SetJetFuelInformationForMvt(Itinerary itinerary, bool isAirportFromMex)
        {
            string infoCM;
            if (isAirportFromMex)
            {
                var nationaJetFuelTicket = this.nationalJetFuelRepository.GetNationalJetFuelTickets(new Entities.Airport.NationalJetFuelTicket
                {
                    Sequence = itinerary.Sequence,
                    AirlineCode = itinerary.AirlineCode,
                    FlightNumber = itinerary.FlightNumber,
                    ItineraryKey = itinerary.ItineraryKey,
                    OperationTypeName = "SALIDA"
                });

                infoCM = nationaJetFuelTicket != null && nationaJetFuelTicket .Count > 0 ? string.Format(
                    @"FI {0}/{1}lts         Rem. {2}",
                    nationaJetFuelTicket.Sum(c => c.FueledQtyKgs), // Sums the Fuel quantity Kilogrammes
                    nationaJetFuelTicket.Sum(c => c.FueledQtyLts), // Sums the Fuel quantity litters
                    nationaJetFuelTicket.LastOrDefault().TicketNumber) // Gets the last record and sets the ticket number
                        : string.Empty;
            }
            else
            {
                infoCM = itinerary.JetFuelTickets != null && itinerary.JetFuelTickets.Count > 0 ? string.Format(
                    @"FI {0}/{1}gals        Rem. {2}",
                    itinerary.JetFuelTickets.Sum(c => c.FueledQry), // Sums the Fuel quantity Kilogrammes
                    itinerary.JetFuelTickets.Sum(c => c.FueledQtyGals), // Sums the Fuel quentity gals
                    itinerary.JetFuelTickets.LastOrDefault().TicketNumber) // Gets the last record and sets the ticket number
                    : string.Empty;
            }

            return infoCM;
        }
    }
}
