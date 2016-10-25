//------------------------------------------------------------------------
// <copyright file="GendecDepatureModelVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using VOI.SISAC.Web.Models.VO.Airport;

    /// <summary>
    /// Gendec Depature Model View Object
    /// </summary>
    public class GendecDepatureModelVO
    {
        /// <summary>
        /// Gendec Departure View Object
        /// </summary>
        public GendecDepartureVO gendecDepartureVO { get; set; }
        /// <summary>
        /// Itinerary Data Object
        /// </summary>
        public ItineraryVO itineraryVO { get; set; }

        /// <summary>
        /// Airplane Data Object
        /// </summary>
        public AirplaneVO AirplaneVO { get; set; }

        /// <summary>
        /// Crew View Object
        /// </summary>
        public IList<CrewVO> CrewVO { get; set; }
    }
}