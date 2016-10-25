//------------------------------------------------------------------------
// <copyright file="CostCenterViewModel.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Finance;

    /// <summary>
    /// Class CostCenterViewModel
    /// </summary>
    public class CostCenterModelVO
    {
        /// <summary>
        /// Gets or sets the cost center vo.
        /// </summary>
        /// <value>
        /// The cost center vo.
        /// </value>
        public CostCenterVO CostCenterVO { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public IList<AirlineVO> Airlines { get; set; }

        /// <summary>
        /// Gets or sets the airline vo.
        /// </summary>
        /// <value>
        /// The airline vo.
        /// </value>
        public AirlineVO AirlineVo { get; set; }

        /// <summary>
        /// Gets or sets the name of the air line.
        /// </summary>
        /// <value>
        /// The name of the air line.
        /// </value>
        public string AirLineName { get; set; }
    }
}