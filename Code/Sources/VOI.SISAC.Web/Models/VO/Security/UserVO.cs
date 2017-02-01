//------------------------------------------------------------------------
// <copyright file="UserVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Web.Models.Attributes;
    using VOI.SISAC.Web.Models.VO.Airport;
    /// <summary>
    /// Class User
    /// </summary>
    public class UserVO
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the employee number.
        /// </summary>
        /// <value>
        /// The employee number.
        /// </value>
        [Display(Name = "EmployeeNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password encripted.
        /// </summary>
        /// <value>
        /// The password encripted.
        /// </value>
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string PasswordEncripted { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        [Display(Name = "Department", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int DepartmentID { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        /// <value>
        /// The entry date.
        /// </value>
        [Display(Name = "EntryDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
            , ErrorMessageResourceName = "DateTimeValidation")]
        public System.DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
            , ErrorMessageResourceName = "DateTimeValidation")]
        public System.DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user volaris].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user volaris]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "UserVolaris", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public bool UserVolaris { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }
        
        /// <summary>
        /// Departments
        /// </summary>
        public virtual DepartmentVO Departments { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public AirlineVO Airlines { get; set; }

        /// <summary>
        /// Gets or sets UserProfileRoles
        /// </summary>
        public ICollection<UserProfileRoleVO> UserProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the user airports.
        /// </summary>
        /// <value>
        /// The user airports.
        /// </value>
        public ICollection<UserAirportVO> UserAirports { get; set; }

        /// <summary>
        /// Gets or sets the module permissions.
        /// </summary>
        /// <value>
        /// The module permissions.
        /// </value>
        public ICollection<ModulePermissionVO> ModulePermissions { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel ticket.
        /// </summary>
        /// <value>
        /// The jet fuel ticket.
        /// </value>
        public virtual ICollection<JetFuelTicketVO> JetFuelTicket { get; set; }

        /// <summary>
        /// Gets or sets the passenger information.
        /// </summary>
        /// <value>
        /// The passenger information.
        /// </value>
        public ICollection<PassengerInformationVO> PassengerInformation { get; set; }

        /// <summary>
        /// Airports
        /// </summary>
        public ICollection<AirportVO> Airports { get; set; }

        /// <summary>
        /// Profiles
        /// </summary>
        public ICollection<ProfileVO> Profiles { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public ICollection<RoleVO> Roles { get; set; }

        /// <summary>
        /// Gets the nombre usuario.
        /// </summary>
        /// <value>
        /// The nombre usuario.
        /// </value>
        [Display(Name = "NombreUsuario", ResourceType = typeof(VOI.SISAC.Web.Resources.Resource))]
        public string NombreUsuario
        {
            get
            {
                if (String.IsNullOrEmpty(this.LastName))
                    return this.UserID + " - " + this.Name + " " + this.FirstName;
                else
                    return this.UserID + " - " + this.Name + " " + this.FirstName + " "+ this.LastName;
            }
        }
    }
}

