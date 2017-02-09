//-----------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformationRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// ManifestDepartureBoardingDetailRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoardingDetail}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IManifestDepartureBoardingDetailRepository" />
    public class ManifestDepartureBoardingDetailRepository : Repository<ManifestDepartureBoardingDetail>, IManifestDepartureBoardingDetailRepository
    {
        #region Contructor        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBoardingRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ManifestDepartureBoardingDetailRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        #region ManifestDepartureBoardingDetailRepository Members

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ManifestDepartureBoardingDetail FindById(long id)
        {
            var manifestDepartureBoardingDetail = this.DbContext.ManifestDepartureBoardingDetails.Where(c => c.BoardingDetailID == id).FirstOrDefault();
            return manifestDepartureBoardingDetail;
        }

        /// <summary>
        /// Gets the detail by boarding identifier.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        public IList<ManifestDepartureBoardingDetail> GetDetailByBoardingID(long boardingID, string airplaneModel)
        {
            ManifestDepartureBoardingDetail item = new ManifestDepartureBoardingDetail();
            IList<ManifestDepartureBoardingDetail> detail = new List<ManifestDepartureBoardingDetail>();
            
            var airplaneType = this.DbContext.AirplaneTypes
                .Include(c => c.CompartmentType)
                .Include(c => c.CompartmentType.CompartmentTypeConfigs)
                .FirstOrDefault(c => c.AirplaneModel == airplaneModel);

            detail = this.DbContext.ManifestDepartureBoardingDetails
                .Include(c => c.CompartmentTypeConfig)
                .Where(c => c.BoardingID == boardingID)
                .ToList();

            //Si no existe informacion en BD carga la que tiene por default la matricula
            if (detail.Count == 0)
            {
                foreach (var i in airplaneType.CompartmentType.CompartmentTypeConfigs)
                {
                    item = new ManifestDepartureBoardingDetail
                    {
                        BoardingID = boardingID,
                        CompartmentTypeID = i.CompartmentTypeID
                    };

                    item.CompartmentTypeConfig = this.DbContext.CompartmentTypeConfigs.FirstOrDefault(c => c.CompartmentTypeID == i.CompartmentTypeID);

                    detail.Add(item);
                } 
            }

            return detail;
        }
        
        #endregion
    }
}
