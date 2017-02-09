//------------------------------------------------------------------------
// <copyright file="CrewType.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Crew Type
    /// </summary>
    public class CrewType
    {
        /// <summary>
        /// Gets or sets the crew type identifier.
        /// </summary>
        /// <value>
        /// The crew type identifier.
        /// </value>
        public string CrewTypeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the crew type.
        /// </summary>
        /// <value>
        /// The name of the crew type.
        /// </value>
        public string CrewTypeName { get; set; }
    }
}
