//------------------------------------------------------------------------
// <copyright file="UserAirportDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// UserAirportDto
    /// </summary>
    public class UserAirportDto
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
        public UserDto Users { get; set; }

        /// <summary>
        /// Airports
        /// </summary>
        public virtual AirportDto Airports { get; set; }
    }
}
