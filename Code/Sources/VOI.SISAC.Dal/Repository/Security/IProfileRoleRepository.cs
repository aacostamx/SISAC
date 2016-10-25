//------------------------------------------------------------------------
// <copyright file="IProfileRoleRepository.cs" company="Volaris">
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
    /// Interface ProfileRole Repository
    /// </summary>
    public interface IProfileRoleRepository : IRepository<ProfileRole>
    {
        /// <summary>
        /// Adds profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roles">The roles.</param>
        void AddProfileAndRole(string profileCode, IList<Role> roles);

        /// <summary>
        /// Adds profile and role.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="role">The role.</param>
        void AddProfileRole(Profile profile, Role role);

        /// <summary>
        /// Gets the roles by profile.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>The roles for the profile.</returns>
        IList<Role> GetRolesByProfile(string profileCode);

        /// <summary>
        /// Finds by id.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roleCode">The role code.</param>
        /// <returns>The profile role entity.</returns>
        ProfileRole FindBy(string profileCode, string roleCode);

        /// <summary>
        /// Gets the profile role by profile code.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// List of profile role.
        /// </returns>
        IList<ProfileRole> GetProfileRoleByProfileCode(string profileCode);

        /// <summary>
        /// Gets the profile roles by role code.
        /// </summary>
        /// <param name="roleCode">The role code.</param>
        /// <returns>List of profile role.</returns>
        IList<ProfileRole> GetProfileRoleByRoleCode(string roleCode);

        /// <summary>
        /// Deletes profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roles">The roles.</param>
        void DeleteProfileAndRoles(string profileCode, IList<Role> roles);
    }
}
