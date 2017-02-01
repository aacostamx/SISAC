//------------------------------------------------------------------------
// <copyright file="ICrewBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    using Dto.Paged;

    /// <summary>
    /// Interface Business Crew
    /// </summary>
    public interface ICrewBusiness
    {
        /// <summary>
        /// Gets all crew.
        /// </summary>
        /// <returns> IList CrewDto </returns>
        IList<CrewDto> GetAllCrew();

        /// <summary>
        /// Finds the crew by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return Crew Data transfer object.</returns>
        CrewDto FindCrewById(long id);

        /// <summary>
        /// Adds the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>return bool</returns>
        bool AddCrew(CrewDto entity);

        /// <summary>
        /// Deletes the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>return bool</returns>
        bool DeleteCrew(CrewDto entity);

        /// <summary>
        /// Updates the crew.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>return bool</returns>
        bool UpdateCrew(CrewDto entity);

        /// <summary>
        /// Gets the actives crew.
        /// </summary>
        /// <returns>IList CrewDto  </returns>
        IList<CrewDto> GetActivesCrew();

        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="crewDto">The crew dto.</param>
        /// <returns>number of integer matches</returns>
        List<string> ValidateFields(CrewDto crewDto);

        /// <summary>
        /// Gets all crew by identifier.
        /// </summary>
        /// <param name="CrewID">The crew identifier.</param>
        /// <returns>Return all the information of the crew member.</returns>
        CrewDto GetAllCrewByID(long CrewID);

        /// <summary>
        /// Finds the crew by employee number.
        /// </summary>
        /// <param name="employeeNumber">The employee number.</param>
        /// <returns></returns>
        CrewDto FindCrewByEmployeeNumber(string employeeNumber);

        /// <summary>
        /// Gets the crew paged.
        /// </summary>
        /// <param name="pagedDto">The paged dto.</param>
        /// <returns></returns>
        IList<CrewDto> GetCrewPaged(PagedDto pagedDto);

        /// <summary>
        /// Totals the crew.
        /// </summary>
        /// <returns></returns>
        int TotalCrew();

        /// <summary>
        /// Gets the active pilots.
        /// </summary>
        /// <returns>A list of actives captains and copilots.</returns>
        IList<CrewDto> GetActivePilots();

        /// <summary>
        /// Gets the active stewadess.
        /// </summary>
        /// <returns>A list of actives stewadess.</returns>
        IList<CrewDto> GetActiveStewardess();
    }
}
