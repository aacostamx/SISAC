//------------------------------------------------------------------------
// <copyright file="AirlineBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Airline Business
    /// </summary>
    public class AirlineBusiness : IAirlineBusiness
    {
        /// <summary>
        /// The airline repository
        /// </summary>
        private readonly IAirlineRepository airlineRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="AirlineBusiness"/> class.
        /// </summary>
        /// <param name="airlineRepository">The airline repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public AirlineBusiness(IAirlineRepository airlineRepository, IUnitOfWork unitOfWork)
        {
            this.airlineRepository = airlineRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region IAirlineBusiness Members

        /// <summary>
        /// Gets all airline.
        /// </summary>
        /// <returns>View Airline</returns>
        /// <exception cref="BusinessException">Error when find</exception>
        public IList<AirlineDto> GetAllAirline()
        {
            try
            {
                IList<Airline> airlineEntity = this.airlineRepository.GetAll().ToList();
                IList<AirlineDto> airlineDto = new List<AirlineDto>();
                airlineDto = Mapper.Map<IList<Airline>, IList<AirlineDto>>(airlineEntity);

                return airlineDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the airline by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Airline</returns>
        /// <exception cref="BusinessException">Error when find</exception>
        public AirlineDto FindAirlineById(string id)
        {
            try
            {
                Airline airlineEntity = this.airlineRepository.FindById(id);
                AirlineDto airlineDto = new AirlineDto();      
                airlineDto = Mapper.Map<Airline, AirlineDto>(airlineEntity);

                return airlineDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if added else false</returns>
        /// <exception cref="BusinessException">Error when insert
        /// </exception>
        public bool AddAirline(AirlineDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsAirlineCodeDuplicate(entity.AirlineCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                Airline airlineEntity = new Airline();
                entity.Status = true;
                airlineEntity = Mapper.Map<AirlineDto, Airline>(entity);
                this.airlineRepository.Add(airlineEntity);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if deleted else false</returns>
        /// <exception cref="BusinessException">Error when delete</exception>
        public bool DeleteAirline(AirlineDto entity)
        {
            try
            {
                Airline airline = this.airlineRepository.FindById(entity.AirlineCode);

                if (airline != null)
                {
                    /*
                    * Si se quiere hacer un borrado permanente de la DB
                    * se debe de usar la siguiente instrucción:
                    * this.airlineRepository.Delete(airline);
                    */

                    // Para borrado lógico
                    airline.Status = false;
                    this.airlineRepository.Update(airline);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if updated else false</returns>
        /// <exception cref="BusinessException">Error when update</exception>
        public bool UpdateAirline(AirlineDto entity)
        {
            try
            {
                Airline airline = this.airlineRepository.FindById(entity.AirlineCode);
                airline.AirlineName = entity.AirlineName;
                airline.AirlineShortName = entity.AirlineShortName;
                airline.CompanyCode = entity.CompanyCode;
                airline.Division = entity.Division;
                airline.BusinessName = entity.BusinessName;
                this.airlineRepository.Update(airline);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives airline.
        /// </summary>
        /// <returns>List AirlineDto</returns>
        /// <exception cref="BusinessException">Error when find</exception>
        public IList<AirlineDto> GetActivesAirline()
        {
            try
            {
                IList<Airline> airlineEntity = this.airlineRepository.GetActiveAirline().ToList();
                IList<AirlineDto> airlineDto = new List<AirlineDto>();
                airlineDto = Mapper.Map<IList<Airline>, IList<AirlineDto>>(airlineEntity);

                return airlineDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether [is airline code duplicate] [the specified airline code].
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns>true or false</returns>
        public bool IsAirlineCodeDuplicate(string airlineCode)
        {
            Airline airline = this.airlineRepository.FindById(airlineCode);
            return airline != null ? true : false;
        }

        #endregion
    }
}
