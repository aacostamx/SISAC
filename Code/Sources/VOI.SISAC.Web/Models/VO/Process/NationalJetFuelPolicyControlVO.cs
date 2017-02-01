//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyControlVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// NationalJetFuelPolicyControlVO class
    /// </summary>
    public class NationalJetFuelPolicyControlVO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyControlVO"/> class.
        /// </summary>
        public NationalJetFuelPolicyControlVO()
        {
            this.AirportCodesList = new List<string>();
            this.ProviderCodesList = new List<string>();
            this.ServiceCodesList = new List<string>();
        }

        /// <summary>
        /// Gets or sets the national policy identifier.
        /// </summary>
        /// <value>
        /// The national policy identifier.
        /// </value>
        public long NationalPolicyID { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public string DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the service codes.
        /// </summary>
        /// <value>
        /// The service codes.
        /// </value>
        public string ServiceCodes { get; set; }

        /// <summary>
        /// Gets or sets the provider codes.
        /// </summary>
        /// <value>
        /// The provider codes.
        /// </value>
        public string ProviderCodes { get; set; }

        /// <summary>
        /// Gets or sets the airport codes.
        /// </summary>
        /// <value>
        /// The airport codes.
        /// </value>
        public string AirportCodes { get; set; }

        /// <summary>
        /// Gets or sets the start date real.
        /// </summary>
        /// <value>
        /// The start date real.
        /// </value>
        [Display(Name = "StartDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public DateTime StartDateReal { get; set; }

        /// <summary>
        /// Gets or sets the end date real.
        /// </summary>
        /// <value>
        /// The end date real.
        /// </value>
        [Display(Name = "EndDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public DateTime EndDateReal { get; set; }

        /// <summary>
        /// Gets or sets the start date complementary.
        /// </summary>
        /// <value>
        /// The start date complementary.
        /// </value>
        [Display(Name = "StartDate", ResourceType = typeof(Resources.Resource))]
        public DateTime? StartDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the end date complementary.
        /// </summary>
        /// <value>
        /// The end date complementary.
        /// </value>
        [Display(Name = "EndDate", ResourceType = typeof(Resources.Resource))]
        public DateTime? EndDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [Display(Name = "BaseDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
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
        public DateTime DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>
        /// The header text.
        /// </value>
        [Display(Name = "HeaderText", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        [StringLength(5)]
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        /// <value>
        /// The item text.
        /// </value>
        [Display(Name = "ItemText", ResourceType = typeof(Resources.Resource))]
        [StringLength(30)]
        public string ItemText { get; set; }

        /// <summary>
        /// Gets or sets the name of the send by user.
        /// </summary>
        /// <value>
        /// The name of the send by user.
        /// </value>
        public string SendByUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the service codes list.
        /// </summary>
        /// <value>
        /// The service codes list.
        /// </value>
        public IList<string> ServiceCodesList { get; set; }

        /// <summary>
        /// Gets or sets the provider codes list.
        /// </summary>
        /// <value>
        /// The provider codes list.
        /// </value>
        public IList<string> ProviderCodesList { get; set; }

        /// <summary>
        /// Gets or sets the airport codes list.
        /// </summary>
        /// <value>
        /// The airport codes list.
        /// </value>
        public IList<string> AirportCodesList { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel policy.
        /// </summary>
        /// <value>
        /// The national jet fuel policy.
        /// </value>
        public IList<NationalJetFuelPolicyVO> NationalJetFuelPolicy { get; set; }
    }
}