//------------------------------------------------------------------------
// <copyright file="Crew.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Catalog;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Class Crew
    /// </summary>
    public class Crew
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
        public string CrewTypeID { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the country of residence.
        /// </summary>
        /// <value>
        /// The country of residence.
        /// </value>
        public string CountryOfResidence { get; set; }

        /// <summary>
        /// Gets or sets the place birth city.
        /// </summary>
        /// <value>
        /// The place birth city.
        /// </value>
        public string PlaceBirthCity { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string CountryOfBird { get; set; }

        /// <summary>
        /// Gets or sets the date of bird.
        /// </summary>
        /// <value>
        /// The date of bird.
        /// </value>
        public DateTime? DateOfBird { get; set; }

        /// <summary>
        /// Gets or sets the citizenship.
        /// </summary>
        /// <value>
        /// The citizenship.
        /// </value>
        public string Citizenship { get; set; }

        /// <summary>
        /// Gets or sets the status on board.
        /// </summary>
        /// <value>
        /// The status on board.
        /// </value>
        public string StatusOnBoardCode { get; set; }

        /// <summary>
        /// Gets or sets the home address.
        /// </summary>
        /// <value>
        /// The home address.
        /// </value>
        public string HomeAddress { get; set; }

        /// <summary>
        /// Gets or sets the home address city.
        /// </summary>
        /// <value>
        /// The home address city.
        /// </value>
        public string HomeAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the home address.
        /// </summary>
        /// <value>
        /// The state of the home address.
        /// </value>
        public string HomeAddressState { get; set; }

        /// <summary>
        /// Gets or sets the home address zip code.
        /// </summary>
        /// <value>
        /// The home address zip code.
        /// </value>
        public string HomeAddressZipCode { get; set; }

        /// <summary>
        /// Gets or sets the home address country.
        /// </summary>
        /// <value>
        /// The home address country.
        /// </value>
        public string HomeAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the passport number.
        /// </summary>
        /// <value>
        /// The passport number.
        /// </value>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Gets or sets the passport country issuance.
        /// </summary>
        /// <value>
        /// The passport country issuance.
        /// </value>
        public string PassportCountryIssuance { get; set; }

        /// <summary>
        /// Gets or sets the passport expiration.
        /// </summary>
        /// <value>
        /// The passport expiration.
        /// </value>
        public DateTime? PassportExpiration { get; set; }

        /// <summary>
        /// Gets or sets the license number.
        /// </summary>
        /// <value>
        /// The license number.
        /// </value>
        public string LicenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the license country issuance.
        /// </summary>
        /// <value>
        /// The license country issuance.
        /// </value>
        public string LicenceCountryIssuance { get; set; }

        /// <summary>
        /// Gets or sets the license number expiration.
        /// </summary>
        /// <value>
        /// The license number expiration.
        /// </value>
        public DateTime? LicenceNumberExpiration { get; set; }

        /// <summary>
        /// Gets or sets the name of the nick.
        /// </summary>
        /// <value>
        /// The name of the nick.
        /// </value>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the nick name sabre.
        /// </summary>
        /// <value>
        /// The nick name sabre.
        /// </value>
        public string NickNameSabre { get; set; }

        /// <summary>
        /// Gets or sets the visa expiration date.
        /// </summary>
        /// <value>
        /// The visa expiration date.
        /// </value>
        public DateTime? VisaExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the employee number.
        /// </summary>
        /// <value>
        /// The employee number.
        /// </value>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Crew"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Gets or sets the type of the crew.
        /// </summary>
        /// <value>
        /// The type of the crew.
        /// </value>
        public virtual CrewType CrewType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<GendecDeparture> GendecDeparturesCrew { get; set; }

        /// <summary>
        /// Gets or sets de Gendec Arrival Crew
        /// </summary>
        public virtual ICollection<GendecArrival> GendecArrivalsCrew { get; set; }
    }
}