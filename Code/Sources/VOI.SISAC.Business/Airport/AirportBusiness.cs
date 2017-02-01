//------------------------------------------------------------------------
// <copyright file="AirportBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using AutoMapper;
    using Entities.Airport;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;

    /// <summary>
    /// class AirporBusiness than implement IAirportBusiness
    /// </summary>
    public class AirportBusiness : IAirportBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airportRepository">The airport repository.</param>
        public AirportBusiness(IUnitOfWork unitOfWork, IAirportRepository airportRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airportRepository = airportRepository;
        }

        /// <summary>
        /// Gets all airport.
        /// </summary>
        /// <returns>
        /// List of Airports.
        /// </returns>
        public IList<AirportDto> GetAllAirport()
        {
            try
            {
                IList<Airport> airportModel = this.airportRepository.GetAll();
                IList<AirportDto> airportDTO = Mapper.Map<IList<Airport>, IList<AirportDto>>(airportModel);
                return airportDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the airport by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity Airport.
        /// </returns>
        public AirportDto FindAirportById(string id)
        {
            try
            {
                Airport airportModel = this.airportRepository.FindById(id);
                AirportDto airportDTO = Mapper.Map<Airport, AirportDto>(airportModel);
                return airportDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the airport.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddAirport(AirportDto dto)
        {
            if (this.IsDuplicate(dto.StationCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                Airport airportModel = Mapper.Map<AirportDto, Airport>(dto);
                airportModel.Status = true;
                this.airportRepository.Add(airportModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the airport.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteAirport(AirportDto dto)
        {
            try
            {
                Airport airportModel = this.airportRepository.FindById(dto.StationCode);
                airportModel.Status = false;
                this.airportRepository.Update(airportModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical airport delete
        /// </summary>
        /// <param name="dto">airport</param>
        /// <returns>boolean</returns>
        public bool PhysicalDeleteAirport(AirportDto dto)
        {
            try
            {
                Airport airportModel = Mapper.Map<AirportDto, Airport>(dto);
                this.airportRepository.Delete(airportModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the airport.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateAirport(AirportDto dto)
        {
            try
            {
                Airport airportModel = Mapper.Map<AirportDto, Airport>(dto);
                airportModel.Status = true;
                this.airportRepository.Update(airportModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives Airports.
        /// </summary>
        /// <returns>Airports Actives.</returns>
        public IList<AirportDto> GetActivesAirports()
        {
            try
            {
                IList<Airport> airportModel = this.airportRepository.GetActivesAirports();
                IList<AirportDto> airportDTO = Mapper.Map<IList<Airport>, IList<AirportDto>>(airportModel);
                return airportDTO.OrderBy(c => c.StationCode).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Determines whether entity Code duplicate is.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsDuplicate(string id)
        {
            Airport entity = this.airportRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}
