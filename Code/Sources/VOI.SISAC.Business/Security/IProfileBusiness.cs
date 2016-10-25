//------------------------------------------------------------------------
// <copyright file="IProfileBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Security;

    public interface IProfileBusiness
    {
        /// <summary>
        /// Get All Profiles
        /// </summary>
        /// <returns>List of Profile data transfer object.</returns>
        IList<ProfileDto> GetAllProfiles();

        /// <summary>
        /// Get the Profile by the identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Profile data transfer object.</returns>
        ProfileDto FindProfileById(string id);

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDto">Profile data transfer object.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        bool AddProfile(ProfileDto profileDto);

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profileDto">Profile data transfer object.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        bool UpdateProfile(ProfileDto profileDto);

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        bool DeleteProfile(string profileCode);
    }
}
