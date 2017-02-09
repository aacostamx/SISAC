//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyControl.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// NationalJetFuelPolicyControl Class
    /// </summary>
    [Table("Process.NationalJetFuelPolicyControl")]
    public class NationalJetFuelPolicyControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyControl"/> class.
        /// </summary>
        public NationalJetFuelPolicyControl()
        {
            NationalJetFuelPolicy = new List<NationalJetFuelPolicy>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyControl"/> class.
        /// </summary>
        /// <param name="NationalPolicyID">The national policy identifier.</param>
        public NationalJetFuelPolicyControl(long NationalPolicyID)
        {
            this.NationalPolicyID = NationalPolicyID;
        }

        /// <summary>
        /// Gets or sets the national policy identifier.
        /// </summary>
        /// <value>
        /// The national policy identifier.
        /// </value>
        [Key]
        public long NationalPolicyID { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        [Required]
        [StringLength(30)]
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
        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the service codes.
        /// </summary>
        /// <value>
        /// The service codes.
        /// </value>
        [Required]
        public string ServiceCodes { get; set; }

        /// <summary>
        /// Gets or sets the provider codes.
        /// </summary>
        /// <value>
        /// The provider codes.
        /// </value>
        [Required]
        public string ProviderCodes { get; set; }

        /// <summary>
        /// Gets or sets the airport codes.
        /// </summary>
        /// <value>
        /// The airport codes.
        /// </value>
        [Required]
        public string AirportCodes { get; set; }

        /// <summary>
        /// Gets or sets the start date real.
        /// </summary>
        /// <value>
        /// The start date real.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime StartDateReal { get; set; }

        /// <summary>
        /// Gets or sets the end date real.
        /// </summary>
        /// <value>
        /// The end date real.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime EndDateReal { get; set; }

        /// <summary>
        /// Gets or sets the start date complementary.
        /// </summary>
        /// <value>
        /// The start date complementary.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime StartDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the end date complementary.
        /// </summary>
        /// <value>
        /// The end date complementary.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime EndDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>
        /// The header text.
        /// </value>
        [Required]
        [StringLength(100)]
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        /// <value>
        /// The item text.
        /// </value>
        [StringLength(100)]
        public string ItemText { get; set; }

        /// <summary>
        /// Gets or sets the name of the send by user.
        /// </summary>
        /// <value>
        /// The name of the send by user.
        /// </value>
        [StringLength(50)]
        public string SendByUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        [StringLength(50)]
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel policy.
        /// </summary>
        /// <value>
        /// The national jet fuel policy.
        /// </value>
        public virtual IList<NationalJetFuelPolicy> NationalJetFuelPolicy { get; set; }
    }
}
