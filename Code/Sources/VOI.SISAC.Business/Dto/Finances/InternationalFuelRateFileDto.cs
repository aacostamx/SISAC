using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOI.SISAC.Business.Dto.Finances
{
    /// <summary>
    /// InternationalFuelRateFile Data Object
    /// </summary>
    public class InternationalFuelRateFileDto
    {
        /// <summary>
        /// LineNumber
        /// </summary>
        public int LineNumber; 

        /// <summary>
        /// EffectiveDate
        /// </summary>
        public DateTime EffectiveDate;

        /// <summary>
        /// AirlineCode
        /// </summary>
        public string AirlineCode;

        /// <summary>
        /// StationCode
        /// </summary>
        public string StationCode;

        /// <summary>
        /// ServiceCode
        /// </summary>
        public string ServiceCode;

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        public string ProviderNumberPrimary;

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        public string FuelConceptName;

        /// <summary>
        /// ProviderNumber
        /// </summary>
        public string ProviderNumber;

        /// <summary>
        /// RateStartDate
        /// </summary>
        public DateTime RateStartDate;

        /// <summary>
        /// RateEndDate
        /// </summary>
        public DateTime RateEndDate;

        /// <summary>
        /// Rate
        /// </summary>
        public Decimal Rate;
    }
}
