//------------------------------------------------------------------------
// <copyright file="CrewTypeBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Crew Type Businness
    /// </summary>
    public class CrewTypeBusiness : ICrewTypeBusiness
    {
        /// <summary>
        /// The crew type repository
        /// </summary>
        private readonly ICrewTypeRepository crewTypeRepository;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewTypeBusiness"/> class.
        /// </summary>
        /// <param name="crewTypeRepository">The crew type repository.</param>
        public CrewTypeBusiness(ICrewTypeRepository crewTypeRepository)
        {
            this.crewTypeRepository = crewTypeRepository;
        }
        #endregion

        #region ICrewTypeBusiness Members        
        /// <summary>
        /// Gets the type of the actives crew.
        /// </summary>
        /// <returns>
        /// IList CrewTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Airports.CrewTypeDto> GetActivesCrewType()
        {
            try
            {
                IList<CrewType> crewEntity = this.crewTypeRepository.GetAll();
                IList<CrewTypeDto> crewDto = new List<CrewTypeDto>();
                crewDto = Mapper.Map<IList<CrewType>, IList<CrewTypeDto>>(crewEntity);

                return crewDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        #endregion
    }
}
