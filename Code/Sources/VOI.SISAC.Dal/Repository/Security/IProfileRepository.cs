//------------------------------------------------------------------------
// <copyright file="IProfileRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Profile Repository Interface
    /// </summary>
    public interface IProfileRepository : IRepository<Profile>
    {
        /// <summary>
        /// Find the Profile by the identifier
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>A list of Profiles</returns>
        Profile FindById(string id);

        /// <summary>
        /// Gets all profiles.
        /// </summary>
        /// <returns>A list of Profiles</returns>
        IList<Profile> GetProfiles();

        /// <summary>
        /// Validate if the profile exist.
        /// </summary>
        /// <param name="profilesCodes">The profiles codes to validate.</param>
        /// <returns>Returns a list with the Profiles Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfProfileExists(IList<string> profilesCodes);
    }
}
