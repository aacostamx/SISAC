//------------------------------------------------------------------------
// <copyright file="RemittanceIDValidate.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    public class RemittanceIDValidate
    {

        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        public string RemittanceID { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the verify.
        /// </summary>
        /// <value>
        /// The verify.
        /// </value>
        public int Verify { get; set; }
    }
}
