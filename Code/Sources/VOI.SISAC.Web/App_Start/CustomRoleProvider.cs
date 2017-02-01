//------------------------------------------------------------------------
// <copyright file="CustomRoleProvider.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models;
    using System.Diagnostics;
    /// <summary>
    /// Custom role provider
    /// </summary>
    public class CustomRoleProvider : RoleProvider
    {
        /*
         * A few words:
         * 
         * Due to Role Provider wasn't disigned to work with IoC and DI, we couldn't (because of the death line)
         * implemente Autofac for the DI. We manage to worked around and solve the problematic using Forms 
         * Authentication cookies instead of consulting the DB to verified the user's permission.
         * 
         * If in the future you´ll find a way to improve this code or you´ll find a better tecnology feel free 
         * to change it. This is your code now. 
         * 
         * Good luck. :)
         */

        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Determines whether is user in role.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        ///     <c>true</c> if user is in role, otherwise <c>false</c>.
        /// </returns>
        public override bool IsUserInRole(string userName, string roleName)
        {
            var inRole = false;

            if (userName == HttpContext.Current.User.Identity.Name)
            {
                UserIdentity identity = (UserIdentity)HttpContext.Current.User.Identity;
                string userData = CompressHelper.Decompress(identity.UserId);
                UserData userPermission = JsonConvert.DeserializeObject<UserData>(identity.UserId);
                inRole = userPermission.Roles.Contains(roleName);
            }

            return inRole;
        }

        /// <summary>
        /// Gets the roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Array of roles for the user.</returns>
        public override string[] GetRolesForUser(string userName)
        {
            var roles = new string[0];

            //try
            //{
            if (userName == HttpContext.Current.User.Identity.Name)
            {
                UserIdentity identity = (UserIdentity)HttpContext.Current.User.Identity;
                UserData userPermission = JsonConvert.DeserializeObject<UserData>(identity.UserId);
                roles = userPermission.Roles;
            }
            //}
            //catch (Exception ex)
            //{
            //    Trace.TraceError(ex.Message, ex);
            //}

            return roles;
        }

        /// <summary>
        /// Adds the specified user names to the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be added to the specified roles.</param>
        /// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName" /> has one or more members and do not delete <paramref name="roleName" />.</param>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch" /> and the user is a member of the specified role.
        /// </returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}