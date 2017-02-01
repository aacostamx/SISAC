//--------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// UserRepository
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public UserRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IUserRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FuelConcept Entity.</returns>
        public User FindById(long id)
        {
            var user = this.DbContext.Users.Where(c => c.UserID == id)
                                           .Include(p => p.UserProfileRoles)
                                           .Include(p => p.UserAirports)
                                           .Include(p => p.ModulePermissions).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// FindByUserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindByUserName(string userName)
        {
            var user = this.DbContext.Users.Where(c => c.UserName == userName).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// FindByUserNameEdit
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindByUserNameEdit(string userName, long userId)
        {
            var user = this.DbContext.Users.Where(c => c.UserName == userName && c.UserID != userId).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Gets the Actives FuelConcepts.
        /// </summary>
        /// <returns>FuelConcepts marked as Actives.</returns>
        public IList<User> GetActivesUsers()
        {
            return this.DbContext.Users.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// GetActivesAORUsersByStationCode
        /// </summary>
        /// <returns></returns>
        public IList<User> GetActivesAORUsersByStationCode(string stationCode)
        {
            return this.DbContext.Users.Where(c => c.Status && c.UserAirports.Any(d => d.StationCode == stationCode)) // && d.Principal == true
                       .Include(p => p.UserProfileRoles.Select(q => q.ProfileRoles.Role))                       
                       .Include(p => p.UserAirports).ToList();
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userAirports"></param>
        /// <returns></returns>
        public bool AddUserAirports(User user, IList<UserAirport> userAirports)
        {
            // Loop para airports
            foreach (UserAirport item in userAirports)
            {
                item.UserID = user.UserID;
                this.DbContext.UserAirports.Add(item);
            }

            return true;
        }

        /// <summary>
        /// AddUserProfileRole
        /// </summary>
        /// <param name="user"></param>
        /// <param name="profileRoles"></param>
        /// <returns></returns>
        public bool AddUserProfileRole(User user, IList<UserProfileRole> profileRoles)
        {
            //Loop para profile
            foreach (UserProfileRole item in profileRoles)
            {
                item.UserID = user.UserID;
                this.DbContext.UserProfileRoles.Add(item);
            }

            return true;
        }

        /// <summary>
        /// AddUserModulePermission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="modulePermissions"></param>
        /// <returns></returns>
        public bool AddUserModulePermission(User user, IList<ModulePermission> modulePermissions)
        {
            User userAttach = this.DbContext.Users.Where(c => c.UserID == user.UserID).FirstOrDefault<User>();

            foreach (ModulePermission item in modulePermissions)
            {
                this.DbContext.Users.Attach(userAttach);
                ModulePermission module = this.DbContext.ModulePermissions.
                       Where(c => c.ModuleCode == item.ModuleCode && c.PermissionCode == item.PermissionCode)
                       .FirstOrDefault<ModulePermission>();

                if (module != null)
                {
                    this.DbContext.ModulePermissions.Attach(module);
                    userAttach.ModulePermissions.Add(module);
                }
            }

            return true;
        }

        /// <summary>
        /// DeleteUserRelations
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUserRelations(User user)
        {
            User userRelations = this.DbContext.Users.Where(c => c.UserID == user.UserID).Include(p => p.UserProfileRoles)
                                           .Include(p => p.UserAirports)
                                           .Include(p => p.ModulePermissions).FirstOrDefault<User>();

            IList<UserAirport> airports = userRelations.UserAirports.ToList();
            IList<UserProfileRole> profileRoles = userRelations.UserProfileRoles.ToList();
            IList<ModulePermission> modulePermissions = userRelations.ModulePermissions.ToList();

            //remove airports
            this.DbContext.UserAirports.RemoveRange(airports);
            //remove profile role
            this.DbContext.UserProfileRoles.RemoveRange(profileRoles);

            //remove module permission
            foreach (ModulePermission item in modulePermissions)
            {
                this.DbContext.Users.Attach(userRelations);
                ModulePermission module = this.DbContext.ModulePermissions.
                       Where(c => c.ModuleCode == item.ModuleCode && c.PermissionCode == item.PermissionCode)
                       .FirstOrDefault<ModulePermission>();

                if (module != null)
                {
                    this.DbContext.ModulePermissions.Attach(module);
                    userRelations.ModulePermissions.Remove(module);
                }
            }
     
            return true;
        }

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User information.</returns>
        public User GetUserByUserName(string userName)
        {
            User user = this.DbContext.Users
                .Where(c => c.UserName == userName)
                .Include(p => p.ModulePermissions)
                .Include(p => p.UserAirports)
                .FirstOrDefault();

            return user;
        }
        #endregion
    }
}
