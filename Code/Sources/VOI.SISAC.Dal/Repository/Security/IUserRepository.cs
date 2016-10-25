//-------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FuelConcept Entity.</returns>
        User FindById(long id);

        /// <summary>
        /// FindByUserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User FindByUserName(string userName);

        /// <summary>
        /// FindByUserNameEdit
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User FindByUserNameEdit(string userName, long userId);

        /// <summary>
        /// Gets the Actives FuelConcepts.
        /// </summary>
        /// <returns>FuelConcepts marked as Actives.</returns>
        IList<User> GetActivesUsers();

        /// <summary>
        /// GetActivesAORUsersByStationCode
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        IList<User> GetActivesAORUsersByStationCode(string stationCode);

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userAirports"></param>
        /// <returns></returns>
        bool AddUserAirports(User user, IList<UserAirport> userAirports);

        /// <summary>
        /// AddUserProfileRole
        /// </summary>
        /// <param name="user"></param>
        /// <param name="profileRoles"></param>
        /// <returns></returns>
        bool AddUserProfileRole(User user, IList<UserProfileRole> profileRoles);

        /// <summary>
        /// AddUserModulePermission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="modulePermissions"></param>
        /// <returns></returns>
        bool AddUserModulePermission(User user, IList<ModulePermission> modulePermissions);

        /// <summary>
        /// DeleteUserRelations
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool DeleteUserRelations(User user);

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User information.</returns>
        User GetUserByUserName(string userName);
    }
}

