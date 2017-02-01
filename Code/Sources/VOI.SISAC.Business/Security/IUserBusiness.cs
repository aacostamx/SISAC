//------------------------------------------------------------------------
// <copyright file="IUserBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// IUserBusiness
    /// </summary>
    public interface IUserBusiness
    {
        /// <summary>
        /// GetUsersStation
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        IList<UserDto> GetUsersStation(string stationCode);

        /// <summary>
        /// GetActiveUsers
        /// </summary>
        /// <returns></returns>
        IList<UserDto> GetActiveUsers();

        /// <summary>
        /// FindUserById
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserDto FindUserById(long userID);

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool AddUser(UserDto userDto);

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool UpdateUser(UserDto userDto);

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool DeleteUser(UserDto userDto);

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User information.</returns>
        UserDto GetUserByUserName(string userName);
    }    
}
