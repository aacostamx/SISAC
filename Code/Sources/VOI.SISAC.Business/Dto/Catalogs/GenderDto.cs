//------------------------------------------------------------------------
// <copyright file="GenderDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Gender Dto
    /// </summary>
    public class GenderDto
    {
        /// <summary>
        /// Gets or sets the gender code.
        /// </summary>
        /// <value>
        /// The gender code.
        /// </value>
        public string GenderCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the gender.
        /// </summary>
        /// <value>
        /// The name of the gender.
        /// </value>
        public string GenderName { get; set; }
    }
}
