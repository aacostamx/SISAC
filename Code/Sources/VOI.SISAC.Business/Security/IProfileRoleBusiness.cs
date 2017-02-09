//------------------------------------------------------------------------
// <copyright file="IProfileRoleBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Interface of Profile Roles Business
    /// </summary>
    public interface IProfileRoleBusiness
    {
        /// <summary>
        /// Adds or deletes a profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="rolesDto">The roles.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>
        /// </returns>
        bool UpdatesProfileAndRoles(string profileCode, IList<RoleDto> rolesDto);

        /// <summary>
        /// Finds the roles by profile code.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// List of <see cref="GenericCatalogDto" />.
        /// </returns>
        IList<GenericCatalogDto> FindRolesByProfileCode(string profileCode);

        /// <summary>
        /// Gets all the roles and marks the roles that are selected for the profile.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// The roles for the profile.
        /// </returns>
        IList<RolesMarkedByProfileDto> GetAllRolesByProfile(string profileCode);

        /// <summary>
        /// Get All Profiles
        /// </summary>
        /// <returns>List of Profile roles.</returns>
        IList<ProfileRoleDto> GetAllProfiles();

        /// <summary>
        /// Finds the roles by profile code.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of roles by profile.</returns>
        IList<GenericCatalogDto> FindRolesByProfileCode(string id, string selectItem);
    }
}
