//------------------------------------------------------------------------
// <copyright file="CrewVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using Helpers;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Class Crew VO
    /// </summary>
    public class CrewVO
    {
        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public long CrewID { get; set; }

        /// <summary>
        /// Gets or sets the crew type identifier.
        /// </summary>
        /// <value>
        /// The crew type identifier.
        /// </value>
        [Display(Name = "CrewTypeID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(4, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax4")]
        public string CrewTypeID { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [Display(Name = "MiddleName", ResourceType = typeof(Resources.Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [Display(Name = "Gender", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(1, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax1")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the country of residence.
        /// </summary>
        /// <value>
        /// The country of residence.
        /// </value>
        [Display(Name = "CountryOfResidence", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax3")]
        public string CountryOfResidence { get; set; }

        /// <summary>
        /// Gets or sets the place birth city.
        /// </summary>
        /// <value>
        /// The place birth city.
        /// </value>
        [Display(Name = "PlaceBirthCity", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string PlaceBirthCity { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Display(Name = "State", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Display(Name = "CountryOfBird", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax3")]
        public string CountryOfBird { get; set; }

        /// <summary>
        /// Gets or sets the date of bird.
        /// </summary>
        /// <value> 
        /// The date of bird.
        /// </value>
        [Display(Name = "DateOfBird", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
            ,ErrorMessageResourceName = "DateTimeValidation")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? DateOfBird { get; set; }

        /// <summary>
        /// Gets or sets the citizenship.
        /// </summary>
        /// <value>
        /// The citizenship.
        /// </value>
        [Display(Name = "Citizenship", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax3")]
        public string Citizenship { get; set; }

        /// <summary>
        /// Gets or sets the status on board.
        /// </summary>
        /// <value>
        /// The status on board.
        /// </value>
        [Display(Name = "StatusOnBoard", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(5, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax5")]
        public string StatusOnBoardCode { get; set; }

        /// <summary>
        /// Gets or sets the home address.
        /// </summary>
        /// <value>
        /// The home address.
        /// </value>
        [Display(Name = "HomeAddress", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax255")]
        public string HomeAddress { get; set; }

        /// <summary>
        /// Gets or sets the home address city.
        /// </summary>
        /// <value>
        /// The home address city.
        /// </value>
        [Display(Name = "HomeAddressCity", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string HomeAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the home address.
        /// </summary>
        /// <value>
        /// The state of the home address.
        /// </value>
        [Display(Name = "HomeAddressState", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string HomeAddressState { get; set; }

        /// <summary>
        /// Gets or sets the home address zip code.
        /// </summary>
        /// <value>
        /// The home address zip code.
        /// </value>
        [Display(Name = "HomeAddressZipCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax10")]
        public string HomeAddressZipCode { get; set; }

        /// <summary>
        /// Gets or sets the home address country.
        /// </summary>
        /// <value>
        /// The home address country.
        /// </value>
        [Display(Name = "HomeAddressCountry", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax2")]
        public string HomeAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the passport number.
        /// </summary>
        /// <value>
        /// The passport number.
        /// </value>
        [Display(Name = "PassportNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax20")]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Gets or sets the passport country issuance.
        /// </summary>
        /// <value>
        /// The passport country issuance.
        /// </value>
        [Display(Name = "PassportCountryIssuance", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax2")]
        public string PassportCountryIssuance { get; set; }

        /// <summary>
        /// Gets or sets the passport expiration.
        /// </summary>
        /// <value>
        /// The passport expiration.
        /// </value>
        [Display(Name = "PassportExpiration", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? PassportExpiration { get; set; }

        /// <summary>
        /// Gets or sets the licence number.
        /// </summary>
        /// <value>
        /// The licence number.
        /// </value>
        [Display(Name = "LicenceNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax20")]
        public string LicenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the licence country issuance.
        /// </summary>
        /// <value>
        /// The licence country issuance.
        /// </value>
        [Display(Name = "LicenceCountryIssuance", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax2")]
        public string LicenceCountryIssuance { get; set; }

        /// <summary>
        /// Gets or sets the licence number expiration.
        /// </summary>
        /// <value>
        /// The licence number expiration.
        /// </value>
        [Display(Name = "LicenceNumberExpiration", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? LicenceNumberExpiration { get; set; }

        /// <summary>
        /// Gets or sets the name of the nick.
        /// </summary>
        /// <value>
        /// The name of the nick.
        /// </value>
        [Display(Name = "NickName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the nick name sabre.
        /// </summary>
        /// <value>
        /// The nick name sabre.
        /// </value>
        [Display(Name = "NickNameSabre", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string NickNameSabre { get; set; }

        /// <summary>
        /// Gets or sets the visa expiration date.
        /// </summary>
        /// <value>
        /// The visa expiration date.
        /// </value>
        [Display(Name = "VisaExpirationDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? VisaExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the employee number.
        /// </summary>
        /// <value>
        /// The employee number.
        /// </value>
        [Display(Name = "EmployeeNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [DataType(DataType.DateTime)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resource))]
        [JsonConverter(typeof(JsonDateConverter))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
        //          ErrorMessageResourceName = "RequiredField")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CrewVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public CountryVO Country { get; set; }

        /// <summary>
        /// Gets or sets the type of the crew.
        /// </summary>
        /// <value>
        /// The type of the crew.
        /// </value>
        public CrewTypeVO CrewType { get; set; }
    }
}