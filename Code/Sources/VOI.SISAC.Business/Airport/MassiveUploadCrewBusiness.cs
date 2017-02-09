//------------------------------------------------------------------------
// <copyright file="MassiveUploadCrewBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class MassiveUploadCrewBusiness
    /// </summary>
    public class MassiveUploadCrewBusiness : IMassiveUploadCrewBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The crew repository
        /// </summary>
        private readonly ICrewRepository crewRepository;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly ICountryRepository countryRepository;

        /// <summary>
        /// The crew type repository
        /// </summary>
        private readonly ICrewTypeRepository crewTypeRepository;

        /// <summary>
        /// The gender business
        /// </summary>
        private readonly IGenderBusiness genderBusiness;

        /// <summary>
        /// The status on board business
        /// </summary>
        public readonly IStatusOnBoardRepository statusOnBoardRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassiveUploadCrewBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="crewRepository">The crew repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="crewTypeRepository">The crew type repository.</param>
        /// <param name="genderBusiness">The gender business.</param>
        /// <param name="statusOnBoardBusiness">The status On Board Repository.</param>
        public MassiveUploadCrewBusiness(
            IUnitOfWork unitOfWork,
            ICrewRepository crewRepository,
            ICountryRepository countryRepository,
            ICrewTypeRepository crewTypeRepository,
            IGenderBusiness genderBusiness,
            IStatusOnBoardRepository statusOnBoardRepository)
        {
            this.unitOfWork = unitOfWork;
            this.crewRepository = crewRepository;
            this.countryRepository = countryRepository;
            this.crewTypeRepository = crewTypeRepository;
            this.genderBusiness = genderBusiness;
            this.statusOnBoardRepository = statusOnBoardRepository;
        }

        #region IMassiveUploadCrewBusiness Members

        /// <summary>
        /// Crews the add range.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns>List of string</returns>
        public List<string> CrewAddRange(IList<CrewDto> crewDto)
        {
            // If there isn't any items
            if (crewDto == null || crewDto.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            // Validates that the Catalogs exist (Country)
            errors = this.ValidateCatalogs(crewDto).ToList();
            if (errors == null || errors.Count > 0)
            {
                return errors;
            }

            // Validates that the Employe Number not exist
            errors = this.ValidateFields(crewDto).ToList();
            if (errors == null || errors.Count > 0)
            {
                return errors;
            }

            foreach (CrewDto item in crewDto)
            {
                item.Status = true;
                item.CreatedDate = DateTime.Now;
            }

            // In here goes the insertion in the DB
            IList<Crew> crews = Mapper.Map<IList<CrewDto>, IList<Crew>>(crewDto);
            this.crewRepository.AddRange(crews);
            this.unitOfWork.Commit();

            return errors;
        }

        /// <summary>
        /// Validates the catalogs.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns>List of string</returns>
        private IList<string> ValidateCatalogs(IList<CrewDto> crewDto)
        {
            List<string> errors = new List<string>();

            // Validate CrewType
            errors.AddRange(this.ValidateCrewType(crewDto.Select(c => c.CrewTypeID).ToList()));

            // Validate Gender
            errors.AddRange(this.ValidateGender(crewDto.Select(c => c.Gender).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountry(crewDto.Select(c => c.CountryOfBird).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountry(crewDto.Select(c => c.CountryOfResidence).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountry(crewDto.Select(c => c.Citizenship).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountryTwoCharacters(crewDto.Select(c => c.HomeAddressCountry).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountryTwoCharacters(crewDto.Select(c => c.LicenceCountryIssuance).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateCountryTwoCharacters(crewDto.Select(c => c.PassportCountryIssuance).ToList()));

            // Validate Country
            errors.AddRange(this.ValidateStatusOnBoard(crewDto.Select(c => c.StatusOnBoardCode).ToList()));

            return errors;
        }

        /// <summary>
        /// Validates the type of the crew.
        /// </summary>
        /// <param name="crewTypeID">The crew type identifier.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateCrewType(IList<string> crewTypeID)
        {
            List<string> errors = new List<string>();
            List<string> result = this.crewTypeRepository.ValidatedIfCrewTypeExist(crewTypeID);
            if (result.Count > 0)
            {
                errors.Add("CrewTypeID code(s) not found");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateGender(IList<string> gender)
        {
            List<string> errors = new List<string>();
            List<string> result = this.genderBusiness.ValidatedIfGenderExist(gender).ToList();
            if (result.Count > 0)
            {
                errors.Add("Gender code(s) not found");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the countries exist.
        /// </summary>
        /// <param name="countryCodes">The countries codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private List<string> ValidateCountry(IList<string> countryCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.countryRepository.ValidatedIfCountryExist(countryCodes);
            if (result.Count > 0)
            {
                errors.Add("Country code(s) not found");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates the country two characters.
        /// </summary>
        /// <param name="countryCodes">The country codes.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateCountryTwoCharacters(IList<string> countryCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.countryRepository.ValidatedIfCountryTwoCharactersExist(countryCodes);
            if (result.Count > 0)
            {
                errors.Add("Country code(s) not found");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateFields(IList<CrewDto> crewDto)
        {
            List<string> errors = new List<string>();

            // Validate Employe Number
            errors.AddRange(this.ValidateEmployeNumber(crewDto.Select(c => c.EmployeeNumber).ToList()));

            // Validate NickName
            errors.AddRange(this.ValidateNickName(crewDto.Select(c => c.NickName).ToList()));

            // Validate NickNameSabre
            errors.AddRange(this.ValidateNickNameSabre(crewDto.Select(c => c.NickNameSabre).ToList()));

            return errors;
        }

        /// <summary>
        /// Validas the employe number.
        /// </summary>
        /// <param name="employeNumber">The employe number.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateEmployeNumber(List<string> employeNumber)
        {
            List<string> errors = new List<string>();
            List<string> result = this.crewRepository.ValidateIfExistEmployeNumber(employeNumber);
            if (result.Count > 0)
            {
                errors.Add("Employe Number alredy exist");

                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validas the name of the nick.
        /// </summary>
        /// <param name="nickName">Name of the nick.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateNickName(List<string> nickName)
        {
            List<string> errors = new List<string>();
            List<string> result = this.crewRepository.ValidateIfExistNickName(nickName);
            if (result.Count > 0)
            {
                errors.Add("NickName alredy exist");

                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validas the nick name sabre.
        /// </summary>
        /// <param name="nickNameSabre">The nick name sabre.</param>
        /// <returns>List of string</returns>
        private List<string> ValidateNickNameSabre(List<string> nickNameSabre)
        {
            List<string> errors = new List<string>();
            List<string> result = this.crewRepository.ValidateIfExistNickNameSabre(nickNameSabre);
            if (result.Count > 0)
            {
                errors.Add("NickNameSabre Number alredy exist");

                errors.AddRange(result);
            }

            return errors;
        }

        private List<string> ValidateStatusOnBoard(List<string> statusOnBoardCode)
        {
            List<string> errors = new List<string>();
            List<string> result = this.statusOnBoardRepository.ValidateIfExistStatusOnBoardCode(statusOnBoardCode);
            if (result.Count > 0)
            {
                errors.Add("Status On Board Code(s) not found");

                errors.AddRange(result);
            }

            return errors;
        }
        #endregion
    }
}
