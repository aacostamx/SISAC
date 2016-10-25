//------------------------------------------------------------------------
// <copyright file="ProfileRoleRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Profile role repository
    /// </summary>
    public class ProfileRoleRepository : Repository<ProfileRole>, IProfileRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRoleRepository"/> class.
        /// </summary>
        /// <param name="factory">factory parameter</param>
        public ProfileRoleRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Finds the roles by profile identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Profile FindRolesByProfileId(string id)
        {
            Profile profile = new Profile();
            profile = this.DbContext.Profiles.Where(p => p.ProfileCode == id).FirstOrDefault();
            return profile;
        }

        /// <summary>
        /// Adds profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roles">The roles.</param>
        public void AddProfileAndRole(string profileCode, IList<Role> roles)
        {
            foreach (Role item in roles)
            {
                this.DbContext.ProfileRoles.Add(new ProfileRole
                {
                    ProfileCode = profileCode,
                    RoleCode = item.RoleCode
                });
            }
        }

        /// <summary>
        /// Adds profile and role.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="role">The role.</param>
        public void AddProfileRole(Profile profile, Role role)
        {
            this.DbContext.ProfileRoles.Add(new ProfileRole
            {
                ProfileCode = profile.ProfileCode,
                RoleCode = role.RoleCode
            });
        }

        /// <summary>
        /// Gets the roles by profile.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// The roles for the profile.
        /// </returns>
        public IList<Role> GetRolesByProfile(string profileCode)
        {
            List<string> roleCode = this.DbContext.ProfileRoles
                .Where(c => c.ProfileCode == profileCode)
                .Select(d => d.RoleCode)
                .ToList();

            List<Role> roles = this.DbContext.Roles
                .Where(c => roleCode.Contains(c.RoleCode))
                .ToList();

            return roles;
        }

        /// <summary>
        /// Finds by id.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roleCode">The role code.</param>
        /// <returns>
        /// The profile role entity.
        /// </returns>
        public ProfileRole FindBy(string profileCode, string roleCode)
        {
            return this.DbContext.ProfileRoles
                .Include(c => c.UserProfileRoles)
                .FirstOrDefault(c => c.ProfileCode == profileCode && c.RoleCode == roleCode);
        }

        /// <summary>
        /// Gets the profile role by profile code.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// List of profile role.
        /// </returns>
        public IList<ProfileRole> GetProfileRoleByProfileCode(string profileCode)
        {
            return this.DbContext.ProfileRoles.Where(c => c.ProfileCode == profileCode).ToList();
        }

        /// <summary>
        /// Gets the profile roles by role code.
        /// </summary>
        /// <param name="roleCode">The role code.</param>
        /// <returns>
        /// List of profile role.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<ProfileRole> GetProfileRoleByRoleCode(string roleCode)
        {
            return this.DbContext.ProfileRoles.Where(c => c.RoleCode == roleCode).ToList();
        }

        /// <summary>
        /// Deletes profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roles">The roles.</param>
        public void DeleteProfileAndRoles(string profileCode, IList<Role> roles)
        {
            foreach (Role item in roles)
            {
                ProfileRole profileRole = this.DbContext.ProfileRoles
                    .FirstOrDefault(c => c.ProfileCode == profileCode && c.RoleCode == item.RoleCode);
                this.DbContext.ProfileRoles.Remove(profileRole);
            }
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>List of profile roles.</returns>
        public override IList<ProfileRole> GetAll()
        {
            IList<ProfileRole> profiles = this.DbContext.ProfileRoles
                .Include(c => c.UserProfileRoles)
                .ToList();
            return profiles;
        }
    }
}
