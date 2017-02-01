//------------------------------------------------------------------------
// <copyright file="ProfileRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="factory">factory parameter</param>
        public ProfileRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Get the profle by the identifier
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>a list of profile</returns>
        public Profile FindById(string id)
        {
            var profiles = this.DbContext.Profiles.Where(c => c.ProfileCode == id).FirstOrDefault();
            return profiles;
        }

        /// <summary>
        /// Get the profiles
        /// </summary>
        /// <returns>Gets a lists of profiles</returns>
        public IList<Profile> GetProfiles()
        {
            return this.DbContext.Profiles.ToList();
        }

        /// <summary>
        /// Validate if the profile exist.
        /// </summary>
        /// <param name="profilesCodes">The profiles codes to validate.</param>
        /// <returns>Returns a list with the Profiles Codes that do not exist.</returns>
        public IList<string> ValidatedIfProfileExists(IList<string> profilesCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Profile> profileList = this.DbContext.Profiles.ToList();

            notFound = profilesCodes.Except(profileList.Select(c => c.ProfileCode)).ToList();
            return notFound;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public override void Delete(Profile entity)
        {
            List<ProfileRole> profileRole = this.DbContext.ProfileRoles
                .Where(c => c.ProfileCode == entity.ProfileCode)
                .Include(c => c.UserProfileRoles)
                .Include(c => c.Role)
                .Include(c=>c.Profile)
                .ToList();

            this.DbContext.ProfileRoles.RemoveRange(profileRole);
            this.DbContext.Profiles.Remove(entity);
        }
    }
}
