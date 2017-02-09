//------------------------------------------------------------------------
// <copyright file="CrewBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Dto.Paged;
    using Entities.Paged;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Business Crew
    /// </summary>
    public class CrewBusiness : ICrewBusiness
    {
        /// <summary>
        /// The crew repository
        /// </summary>
        private readonly ICrewRepository crewRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewBusiness"/> class.
        /// </summary>
        /// <param name="crewRepository">The crew repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CrewBusiness(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            this.crewRepository = crewRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region ICrewBusiness Members
        /// <summary>
        /// Gets all crew.
        /// </summary>
        /// <returns>IList CrewDto</returns>
        /// <exception cref="BusinessException">Exception Message</exception>
        public IList<CrewDto> GetAllCrew()
        {
            try
            {
                IList<Crew> crewEntity = this.crewRepository.GetAll().ToList();
                IList<CrewDto> crewDto = new List<CrewDto>();
                crewDto = Mapper.Map<IList<Crew>, IList<CrewDto>>(crewEntity);

                return crewDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Finds the crew by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return Crew Data transfer object.</returns>
        public CrewDto FindCrewById(long id)
        {
            try
            {
                CrewDto crewDto = new CrewDto();
                Crew crew = this.crewRepository.FindById(id);
                crewDto = Mapper.Map<CrewDto>(crew);
                return crewDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Adds the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if added else true</returns>
        /// <exception cref="BusinessException">Exception Message</exception>
        public bool AddCrew(CrewDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            try
            {
                if (entity != null)
                {
                    Crew crew = new Crew();
                    entity.Status = true;
                    entity.CreatedDate = DateTime.Now;
                    crew = Mapper.Map<Crew>(entity);
                    this.crewRepository.Add(crew);
                    this.unitOfWork.Commit();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Deletes the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if deleted else true</returns>
        /// <exception cref="BusinessException">Exception Message</exception>
        public bool DeleteCrew(CrewDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            try
            {
                Crew crew = this.crewRepository.FindById(entity.CrewID);

                if (crew != null)
                {
                    /*
                    * Si se quiere hacer un borrado permanente de la DB
                    * se debe de usar la siguiente instrucción:
                    * this.crewRepository.Delete(crew);
                    */

                    // Para borrado lógico
                    crew.Status = false;
                    this.crewRepository.Update(crew);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Updates the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if updated else true</returns>
        /// <exception cref="BusinessException">Exception Message</exception>
        public bool UpdateCrew(CrewDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            try
            {
                Crew crew = this.crewRepository.FindById(entity.CrewID);
                crew.CrewTypeID = entity.CrewTypeID;
                crew.LastName = entity.LastName;
                crew.FirstName = entity.FirstName;
                crew.MiddleName = entity.MiddleName;
                crew.Gender = entity.Gender;
                crew.CountryOfResidence = entity.CountryOfResidence;
                crew.PlaceBirthCity = entity.PlaceBirthCity;
                crew.State = entity.State;
                crew.CountryOfBird = entity.CountryOfBird;
                crew.DateOfBird = entity.DateOfBird;
                crew.Citizenship = entity.Citizenship;
                crew.StatusOnBoardCode = entity.StatusOnBoardCode;
                crew.HomeAddress = entity.HomeAddress;
                crew.HomeAddressCity = entity.HomeAddressCity;
                crew.HomeAddressState = entity.HomeAddressState;
                crew.HomeAddressZipCode = entity.HomeAddressZipCode;
                crew.HomeAddressCountry = entity.HomeAddressCountry;
                crew.PassportNumber = entity.PassportNumber;
                crew.PassportCountryIssuance = entity.PassportCountryIssuance;
                crew.PassportExpiration = entity.PassportExpiration;
                crew.LicenceNumber = entity.LicenceNumber;
                crew.LicenceCountryIssuance = entity.LicenceCountryIssuance;
                crew.LicenceNumberExpiration = entity.LicenceNumberExpiration;
                crew.NickName = entity.NickName;
                crew.NickNameSabre = entity.NickNameSabre;
                crew.VisaExpirationDate = entity.VisaExpirationDate;
                crew.EmployeeNumber = entity.EmployeeNumber;
                this.crewRepository.Update(crew);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the actives crew.
        /// </summary>
        /// <returns>IList CrewDto</returns>
        /// <exception cref="BusinessException">Exception Message</exception>
        public IList<CrewDto> GetActivesCrew()
        {
            try
            {
                IList<Crew> crewEntity = this.crewRepository.GetActiveCrew().ToList();
                IList<CrewDto> crewDto = new List<CrewDto>();
                crewDto = Mapper.Map<IList<Crew>, IList<CrewDto>>(crewEntity);

                return crewDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns>number of integer matches</returns>
        public List<string> ValidateFields(CrewDto crewDto)
        {
            List<string> errors = new List<string>();

            if (crewDto == null)
            {
                return errors;
            }

            try
            {
                errors.AddRange(this.crewRepository.ValidateFields(crewDto.EmployeeNumber, crewDto.NickName, crewDto.NickNameSabre));
                return errors;
            }
            catch(Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Gets all crew by identifier.
        /// </summary>
        /// <param name="CrewID">The crew identifier.</param>
        /// <returns>
        /// Return all the information of the crew member.
        /// </returns>
        public CrewDto GetAllCrewByID(long CrewID)
        {
            try
            {
                Crew crew = this.crewRepository.FindById(CrewID);
                CrewDto crewDto = Mapper.Map<Crew, CrewDto>(crew);
                return crewDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Finds the crew by employee number.
        /// </summary>
        /// <param name="employeeNumber">The employee number.</param>
        /// <returns></returns>
        public CrewDto FindCrewByEmployeeNumber(string employeeNumber)
        {
            if (string.IsNullOrWhiteSpace(employeeNumber))
            {
                return null;
            }

            try
            {
                Crew crew = this.crewRepository.FindCrewByEmployeeNumber(employeeNumber);
                CrewDto crewDto = Mapper.Map<Crew, CrewDto>(crew);
                return crewDto;
            }
            catch(Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Gets the crew paged.
        /// </summary>
        /// <param name="pagedDto">The paged dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<CrewDto> GetCrewPaged(PagedDto pagedDto)
        {
            var crew = new List<CrewDto>();
            try
            {
                var crewEntity = new List<Crew>();
                crewEntity = this.crewRepository.GetCrewPaged(Mapper.Map<Paged>(pagedDto));
                crew = Mapper.Map<List<CrewDto>>(crewEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }

            return crew;
        }

        /// <summary>
        /// Totals the crew.
        /// </summary>
        /// <returns></returns>
        public int TotalCrew()
        {
            var total = 0;
            try
            {
                total = this.crewRepository.CountTotalRecords();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }

            return total;
        }

        /// <summary>
        /// Gets the active pilots.
        /// </summary>
        /// <returns>A list of actives captains and copilots.</returns>
        public IList<CrewDto> GetActivePilots()
        {
            try
            {
                List<CrewDto> dto = Mapper.Map<List<CrewDto>>(this.crewRepository.GetActivePilots().ToList()); //new List<CrewDto>();//
                return dto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Gets the active stewadess.
        /// </summary>
        /// <returns>A list of actives stewadess.</returns>
        public IList<CrewDto> GetActiveStewardess()
        {
            try
            {
                List<CrewDto> dto = Mapper.Map<List<CrewDto>>(this.crewRepository.GetActiveStewardess().ToList()); //new List<CrewDto>();//
                return dto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(exception.Message, exception);
            }
        }
        #endregion
    }
}
