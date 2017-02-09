//------------------------------------------------------------------------
// <copyright file="UserAirportVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using VOI.SISAC.Web.Models.VO.Airport;
namespace VOI.SISAC.Web.Models.VO.Security
{
    /// <summary>
    /// UserAirportVO
    /// </summary>
    public class UserAirportVO
    {
        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// Principal
        /// </summary>
        public bool Principal { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public virtual UserVO Users { get; set; }

        /// <summary>
        /// Airports
        /// </summary>
        public virtual AirportVO Airports { get; set; }
    }
}