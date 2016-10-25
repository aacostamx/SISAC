//------------------------------------------------------------------------
// <copyright file="UserIdentity.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models
{
    using System;
    using System.Security.Principal;
    using System.Web.Security;

    /// <summary>
    /// User identity
    /// </summary>
    [Serializable]
    public class UserIdentity : IIdentity, IPrincipal
    {
        /// <summary>
        /// The ticket
        /// </summary>
        private FormsAuthenticationTicket ticket;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentity"/> class.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        public UserIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        public string AuthenticationType
        {
            get { return "Forms Authentication"; }
        }

        /// <summary>
        /// Gets a value indicating whether if the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name
        {
            get { return this.ticket.Name; }
        }

        /// <summary>
        /// Gets the user custom data.
        /// </summary>
        /// <value>
        /// The user custom data.
        /// </value>
        public string UserId
        {
            get { return this.ticket.UserData; }
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}