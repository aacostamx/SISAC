//------------------------------------------------------------------------
// <copyright file="CrewModelVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Class View Model Crew
    /// </summary>
    public class CrewModelVO
    {
        /// <summary>
        /// Gets or sets the crew dto.
        /// </summary>
        /// <value>
        /// The crew dto.
        /// </value>
        public CrewVO CrewVO { get; set; }

        /// <summary>
        /// Gets or sets the genders.
        /// </summary>
        /// <value>
        /// The genders.
        /// </value>
        public IList<GenderVO> Genders { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public IList<CountryVO> Countries { get; set; }

        /// <summary>
        /// Gets or sets the crew types.
        /// </summary>
        /// <value>
        /// The crew types.
        /// </value>
        public IList<CrewTypeVO> CrewTypes { get; set; }

        /// <summary>
        /// Gets or sets the status on boards.
        /// </summary>
        /// <value>
        /// The status on boards.
        /// </value>
        public IList<StatusOnBoardVO> StatusOnBoards { get; set; }
    }
}