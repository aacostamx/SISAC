//------------------------------------------------------------------------
// <copyright file="IGendecArrivalBusinnes.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Gendec Arrival Business Interface
    /// </summary>
    public interface IGendecArrivalBusiness
    {
        /// <summary>
        /// Obtain the Arrival Departure of a Departure Flight.
        /// </summary>
        /// <returns></returns>
        GendecArrivalDto GetGendecArrival(int sequence, string airlinecode, string flightnumber, string itinerarykey);

        IList<string> ValidateGendecArrivalInformation(GendecArrivalDto gendecArrivalDto);

        IList<string> SendingEmail(GendecArrivalDto gendecArrivalDto);

        bool AddGendecArrival(GendecArrivalDto gendecArrivalDto);

        bool UpdateGendecArrival(GendecArrivalDto gendecArrivalDto);

        bool CloseGendecArrival(GendecArrivalDto gendecArrivalDto);

        bool OpenGendecArrival(GendecArrivalDto gendecArrivalDto);
    }
}
