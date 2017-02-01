//------------------------------------------------------------------------
// <copyright file="MessageGendecVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    /// <summary>
    /// Class MessageGendecVO
    /// </summary>
    public class MessageGendecVO
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the already exist crew.
        /// </summary>
        /// <value>
        /// The already exist crew.
        /// </value>
        public string AlreadyExistCrew { get; set; }

        /// <summary>
        /// Gets or sets the already exist steward.
        /// </summary>
        /// <value>
        /// The already exist steward.
        /// </value>
        public string AlreadyExistSteward { get; set; }

        /// <summary>
        /// Gets or sets the choice crew.
        /// </summary>
        /// <value>
        /// The choice crew.
        /// </value>
        public string ChoiceCrew { get; set; }

        /// <summary>
        /// Gets or sets the choice steward.
        /// </summary>
        /// <value>
        /// The choice steward.
        /// </value>
        public string ChoiceSteward { get; set; }

        /// <summary>
        /// Gets or sets the required field.
        /// </summary>
        /// <value>
        /// The required field.
        /// </value>
        public string RequiredField { get; set; }

        /// <summary>
        /// Gets or sets the maximum length8 value.
        /// </summary>
        /// <value>
        /// The maximum length8 value.
        /// </value>
        public string MaxLength8Val { get; set; }

        /// <summary>
        /// Gets or sets the not exist crews.
        /// </summary>
        /// <value>
        /// The not exist crews.
        /// </value>
        public string NotExistCrews { get; set; }
    }
}