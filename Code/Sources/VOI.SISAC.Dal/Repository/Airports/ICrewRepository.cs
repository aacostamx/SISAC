//------------------------------------------------------------------------
// <copyright file="ICrewRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using Entities.Paged;

    /// <summary>
    /// Interface Crew Repository
    /// </summary>
    public interface ICrewRepository : IRepository<Crew>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity Crew</returns>
        Crew FindById(long id);

        /// <summary>
        /// Gets the active crew.
        /// </summary>
        /// <returns>Collection Crew</returns>
        ICollection<Crew> GetActiveCrew();

        /// <summary>
        /// Validates if exist employee number.
        /// </summary>
        /// <param name="employeNumber">The employee number.</param>
        /// <returns>List string </returns>
        List<string> ValidateIfExistEmployeNumber(List<string> employeNumber);

        /// <summary>
        /// Validates the name of if exist nick.
        /// </summary>
        /// <param name="nickName">Name of the nick.</param>
        /// <returns>List string that matches </returns>
        List<string> ValidateIfExistNickName(List<string> nickName);

        /// <summary>
        /// Validates if exist nick name sabre.
        /// </summary>
        /// <param name="nickNameSabre">The nick name sabre.</param>
        /// <returns>List string that matches </returns>
        List<string> ValidateIfExistNickNameSabre(List<string> nickNameSabre);

        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="employeNumber">The employee number.</param>
        /// <param name="nickName">Name of the nick.</param>
        /// <param name="nickNameSabre">The nick name sabre.</param>
        /// <returns>number integer</returns>
        List<string> ValidateFields(string employeNumber, string nickName, string nickNameSabre);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="crews">The crews.</param>
        void AddRange(IList<Crew> crews);

        /// <summary>
        /// Gets the crews.
        /// </summary>
        /// <returns>The active captains and copilots.</returns>
        List<Crew> GetActivePilots();

        /// <summary>
        /// Gets the crews sob.
        /// </summary>
        /// <returns>The active stewwardess.</returns>
        List<Crew> GetActiveStewardess();

        /// <summary>
        /// Finds the crew by employee number.
        /// </summary>
        /// <param name="employeeNumber">The employee number.</param>
        /// <returns></returns>
        Crew FindCrewByEmployeeNumber(string employeeNumber);

        /// <summary>
        /// Gets the crew paged.
        /// </summary>
        /// <param name="paged">The paged.</param>
        /// <returns></returns>
        List<Crew> GetCrewPaged(Paged paged);
    }

}
