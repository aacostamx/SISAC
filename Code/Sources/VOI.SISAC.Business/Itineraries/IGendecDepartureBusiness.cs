//------------------------------------------------------------------------
// <copyright file="IGenderDepartureBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// interface of Gendec Departure Business
    /// </summary>
    public interface IGendecDepartureBusiness
    {
        /// <summary>
        /// Obtain the Gendec Departure of a Departure Flight.
        /// </summary>
        /// <returns>GendecDepartureDto Entity</returns>
        GendecDepartureDto GetGendecDeparture(int sequence,string airlinecode, string flightnumber, string itinerarykey);

        /// <summary>
        /// Add Gendec with  Crew
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddGendec(GendecDepartureDto gendecDepartureDto);

        /// <summary>
        /// Update Gendec with  Crew
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns><c>true</c> if was edited <c>false</c> otherwise.</returns>
        bool EditGendec(GendecDepartureDto gendecDepartureDto);

        /// <summary>
        /// Delete Crew of a Gendec
        /// </summary>
        /// <param name="CrewID"></param>
        /// <param name="gendecDepartureDto"></param>
        /// <returns>GendecDepartureDto Entity</returns>
        GendecDepartureDto DeleteGendecCrew(long CrewID, GendecDepartureDto gendecDepartureDto);

        /// <summary>
        /// Close Gendec Departure
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns></returns>
        bool CloseGendecDeparture(GendecDepartureDto gendecDepartureDto);

        /// <summary>
        /// Open Gendec Departure
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns></returns>
        bool OpenGendecDepartureButton(GendecDepartureDto gendecDepartureDto);

        IList<string> SendingEmail(GendecDepartureDto gendecDepartureDto);

        bool OpenGendecDeparture(GendecDepartureDto gendecDepartureDto);
    }
}
