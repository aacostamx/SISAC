//------------------------------------------------------------------------
// <copyright file="ItineraryModelVO.cs" company="Volaris">
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
    /// Itinerary Model View Object
    /// </summary>
    public class ItineraryModelVO
    {
        /// <summary>
        /// Itinerary Data Object
        /// </summary>
        public IList<ItineraryVO> itineraryVO { get; set; }

        /// <summary>
        /// Airplane Data Object
        /// </summary>
        public IList<AirplaneVO> AirplaneVO { get; set; }

        /// <summary>
        /// Airport Data Object
        /// </summary>
        public IList<AirportVO> AirportVO { get; set; }

        /// <summary>
        /// Airline Data Object
        /// </summary>
        public IList<AirlineVO> AirlineVO { get; set; }
    }
}