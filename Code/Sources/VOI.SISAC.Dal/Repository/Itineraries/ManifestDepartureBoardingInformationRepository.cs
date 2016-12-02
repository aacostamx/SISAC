//-----------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// ManifestDepartureBoardingInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoarding}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IManifestDepartureBoardingInformationRepository" />
    public class ManifestDepartureBoardingInformationRepository : Repository<ManifestDepartureBoardingInformation>, IManifestDepartureBoardingInformationRepository
    {
        #region Contructor        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBoardingRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ManifestDepartureBoardingInformationRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        #region ManifestDepartureBoardingInformationRepository Members        

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ManifestDepartureBoardingInformation FindById(long id)
        {
            var manifestDepartureBoardingInformation = this.DbContext.ManifestDepartureBoardingInformations.Where(c => c.BoardingInformationID == id).FirstOrDefault();
            return manifestDepartureBoardingInformation;
        }

        /// <summary>
        /// Gets the information by boarding identifier.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        public IList<ManifestDepartureBoardingInformation> GetInformationByBoardingID(long boardingID, string airplaneModel)
        {
            ManifestDepartureBoardingInformation item = new ManifestDepartureBoardingInformation();
            IList<ManifestDepartureBoardingInformation> information = new List<ManifestDepartureBoardingInformation>(); 

    
            var airplaneType = this.DbContext.AirplaneTypes
                .Include(c => c.CompartmentType)
                .Include(c => c.CompartmentType.CompartmentTypeInformations)
                .Include(c => c.CompartmentType.CompartmentTypeConfigs)
                .FirstOrDefault(c => c.AirplaneModel == airplaneModel);

           

            information = this.DbContext.ManifestDepartureBoardingInformations
                .Include(c => c.CompartmentTypeInformation)
                .Include(c => c.CompartmentTypeConfig)
                .Where(c => c.BoardingID == boardingID)
                .ToList();    
    
            //Si no existe informacion en BD carga la que tiene por default la matricula
            if (information.Count == 0)
            { 
                foreach (var i in airplaneType.CompartmentType.CompartmentTypeInformations)
                {                    
                    foreach (var j in airplaneType.CompartmentType.CompartmentTypeConfigs)
                    {
                        item = new ManifestDepartureBoardingInformation
                        {
                            BoardingID = boardingID,
                            CompartmentTypeInformationID = i.CompartmentTypeInformationID,
                            CompartmentTypeID = j.CompartmentTypeID,
                            Checked = false
                        };

                        item.CompartmentTypeInformation = this.DbContext.CompartmentTypeInformations.FirstOrDefault(c => c.CompartmentTypeInformationID == i.CompartmentTypeInformationID);
                        item.CompartmentTypeConfig = this.DbContext.CompartmentTypeConfigs.FirstOrDefault(c => c.CompartmentTypeID == j.CompartmentTypeID);

                        information.Add(item);                                    
                    }            
                }                                        
            }

            return information;
        }
        
        #endregion
    }
}
