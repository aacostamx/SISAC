

namespace VOI.SISAC.Web.Models.VO.Process
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// National Invoice Control Create Parameters
    /// </summary>
    public class NationalInvoiceControlCreateParameter
    {
        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        public string RemittanceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        [Display(Name = "MonthYear", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        [Display(Name = "Period", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [Display(Name = "BaseDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonTimeHourFormat))]
        public DateTime DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [Display(Name = "ValueDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonTimeHourFormat))]
        public DateTime DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        [Display(Name = "PostingDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonTimeHourFormat))]
        public DateTime DatePosting { get; set; }
    }
}