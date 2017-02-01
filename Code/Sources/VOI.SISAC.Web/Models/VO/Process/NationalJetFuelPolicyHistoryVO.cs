//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyHistoryVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Helpers;
    using Newtonsoft.Json;
    
    /// <summary>
    /// National Jet Fuel Policy History View Object
    /// </summary>
    public class NationalJetFuelPolicyHistoryVO
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the policy identifier.
        /// </summary>
        /// <value>
        /// The policy identifier.
        /// </value>
        [Display(Name = "PolicyId", ResourceType = typeof(Resources.Resource))]
        public long NationalPolicyId { get; set; }

        /// <summary>
        /// Gets or sets the start policy identifier.
        /// </summary>
        /// <value>
        /// The start policy identifier.
        /// </value>
        [Display(Name = "BeginPolicyId", ResourceType = typeof(Resources.Resource))]
        public long BeginPolicyId { get; set; }

        /// <summary>
        /// Gets or sets the end policy identifier.
        /// </summary>
        /// <value>
        /// The end policy identifier.
        /// </value>
        [Display(Name = "EndPolicyId", ResourceType = typeof(Resources.Resource))]
        public long EndPolicyId { get; set; }

        /// <summary>
        /// Gets or sets the start creation date.
        /// </summary>
        /// <value>
        /// The start creation date.
        /// </value>
        public DateTime? StartCreationDate { get; set; }

        /// <summary>
        /// Gets or sets the end creation date.
        /// </summary>
        /// <value>
        /// The end creation date.
        /// </value>
        public DateTime? EndCreationDate { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonTimeHourFormat))]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the start date real.
        /// </summary>
        /// <value>
        /// The start date real.
        /// </value>
        [Display(Name = "StartDateReal", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartDateReal { get; set; }

        /// <summary>
        /// Gets or sets the end date real.
        /// </summary>
        /// <value>
        /// The end date real.
        /// </value>
        [Display(Name = "EndDateReal", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? EndDateReal { get; set; }

        /// <summary>
        /// Gets or sets the start date complentary.
        /// </summary>
        /// <value>
        /// The start date complentary.
        /// </value>
        [JsonConverter(typeof(JsonDateConverter))]
        [Display(Name = "StartDateComplementary", ResourceType = typeof(Resources.Resource))]
        public DateTime? StartDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the end date complementary.
        /// </summary>
        /// <value>
        /// The end date complementary.
        /// </value>
        [JsonConverter(typeof(JsonDateConverter))]
        [Display(Name = "EndDateComplementary", ResourceType = typeof(Resources.Resource))]
        public DateTime EndDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [Display(Name = "DateBaseline", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [Display(Name = "DateValue", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        [Display(Name = "DatePosting", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>
        /// The header text.
        /// </value>
        [Display(Name = "HeaderText", ResourceType = typeof(Resources.Resource))]
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        /// <value>
        /// The item text.
        /// </value>
        [Display(Name = "ItemText", ResourceType = typeof(Resources.Resource))]
        public string ItemText { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(Name = "SendByUserName", ResourceType = typeof(Resources.Resource))]
        public string SendByUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        [Display(Name = "ConfirmedByUserName", ResourceType = typeof(Resources.Resource))]
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the total process.
        /// </summary>
        /// <value>
        /// The total process.
        /// </value>
        public int TotalProcess { get; set; }

        /// <summary>
        /// Gets or sets the total sucess.
        /// </summary>
        /// <value>
        /// The total sucess.
        /// </value>
        public int TotalSucess { get; set; }

        /// <summary>
        /// Gets or sets the total errors.
        /// </summary>
        /// <value>
        /// The total errors.
        /// </value>
        public int TotalErrors { get; set; }
    }
}