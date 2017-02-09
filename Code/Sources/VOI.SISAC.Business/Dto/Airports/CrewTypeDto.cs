//------------------------------------------------------------------------
// <copyright file="CrewTypeDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Crew Type Dto
    /// </summary>
    public class CrewTypeDto
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
