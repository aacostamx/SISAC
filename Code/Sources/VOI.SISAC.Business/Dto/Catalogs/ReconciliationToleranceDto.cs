//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{

    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Finances;

    public class ReconciliationToleranceDto
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationTolerance"/> class.
        /// </summary>
        public ReconciliationToleranceDto()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationToleranceDto"/> class.
        /// </summary>
        /// <param name="ServiceCode">The service code.</param>
        /// <param name="CurrencyCode">The currency code.</param>
        /// <param name="ToleranceTypeCode">The tolerance type code.</param>
        public ReconciliationToleranceDto(string ServiceCode, string CurrencyCode, string ToleranceTypeCode)
        {
            this.ServiceCode = ServiceCode;
            this.CurrencyCode = CurrencyCode;
            this.ToleranceTypeCode = ToleranceTypeCode;
        }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the tolerance type code.
        /// </summary>
        /// <value>
        /// The tolerance type code.
        /// </value>
        public string ToleranceTypeCode { get; set; }


        /// <summary>
        /// Gets or sets the tolerance value.
        /// </summary>
        /// <value>
        /// The tolerance value.
        /// </value>
        public decimal ToleranceValue { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReconciliationToleranceDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public CurrencyDto Currency { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public ToleranceTypeDto ToleranceType { get; set; }
    }
}
