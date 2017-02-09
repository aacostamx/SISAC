//------------------------------------------------------------------------
// <copyright file="CrewFile.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------


namespace VOI.SISAC.Web.Models.Files
{
    using System;
    using FileHelpers;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// 
    /// </summary>}
    [DelimitedRecord("\t")]
    public class CrewFile
    {
        /// <summary>
        /// The crew type identifier
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CrewTypeID;

        /// <summary>
        /// The last name
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LastName;

        /// <summary>
        /// The first name
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FirstName;

        /// <summary>
        /// The middle name
        /// </summary>
        [FieldConverter(typeof(UpperStringHelper))]
        public string MiddleName;

        /// <summary>
        /// The gender
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string Gender;

        /// <summary>
        /// The country of residence
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CountryOfResidence;

        /// <summary>
        /// The place birth city
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string PlaceBirthCity;

        /// <summary>
        /// The state
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string State;

        /// <summary>
        /// The country of bird
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CountryOfBird;

        /// <summary>
        /// The date of bird
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime DateOfBird;

        /// <summary>
        /// The citizenship
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string Citizenship;

        /// <summary>
        /// The status on board
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StatusOnBoardCode;

        /// <summary>
        /// The home address
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string HomeAddress;

        /// <summary>
        /// The home address city
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string HomeAddressCity;

        /// <summary>
        /// The home address state
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string HomeAddressState;

        /// <summary>
        /// The home address zip code
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string HomeAddressZipCode;

        /// <summary>
        /// The home address country
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string HomeAddressCountry;

        /// <summary>
        /// The passport number
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string PassportNumber;

        /// <summary>
        /// The passport country issuance
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string PassportCountryIssuance;

        /// <summary>
        /// The passport expiration
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime PassportExpiration;

        /// <summary>
        /// The licence number
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LicenceNumber;

        /// <summary>
        /// The licence country issuance
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LicenceCountryIssuance;

        /// <summary>
        /// The licence number expiration
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime LicenceNumberExpiration;

        /// <summary>
        /// The nick name
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string NickName;

        /// <summary>
        /// The nick name sabre
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string NickNameSabre;

        /// <summary>
        /// The visa expiration date
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime VisaExpirationDate;

        /// <summary>
        /// The employee number
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string EmployeeNumber;

        ///// <summary>
        ///// The created date
        ///// </summary>
        //[FieldNotEmpty]
        //[FieldConverter(typeof(ConvertDate))]
        //public DateTime CreatedDate;
    }
}