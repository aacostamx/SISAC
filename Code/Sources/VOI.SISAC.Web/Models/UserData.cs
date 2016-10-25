//------------------------------------------------------------------------
// <copyright file="AuthenticateUserHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// User data
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets the airports alloed.
        /// </summary>
        /// <value>
        /// The airports alloed.
        /// </value>
        public IList<string> AirportsAlloed { get; set; }
    }
}