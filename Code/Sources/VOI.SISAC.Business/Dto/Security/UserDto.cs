//------------------------------------------------------------------------
// <copyright file="UserDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using Process;
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Class User
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets Employee Number
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password Encripted
        /// </summary>
        public string PasswordEncripted { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Email
        /// </summary>
        public string Email { get; set; }
                
        /// <summary>
        /// Gets or sets DepartmentID
        /// </summary>
        public int DepartmentID { get; set; }
        
        ///// <summary>
        ///// Gets or sets PositionID
        ///// </summary>
        //public int PositionID { get; set; }

        ///// <summary>
        ///// Gets or sets LocationCode
        ///// </summary>
        //public string StationCode { get; set; }
        
        /// <summary>
        /// Gets or sets EntryDate
        /// </summary>
        public System.DateTime EntryDate { get; set; }
        
        /// <summary>
        /// Gets or sets CreationDate
        /// </summary>
        public System.DateTime CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UserVolaris { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserDto"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets CompanyDepartment
        /// </summary>
        public DepartmentDto Departments { get; set; }
        
        ///// <summary>
        ///// Gets or sets UserType
        ///// </summary>
        //public UserTypeDto UserType { get; set; }

        /// <summary>
        /// Gets or sets Airlines
        /// </summary>
        public AirlineDto Airlines { get; set; }

        /// <summary>
        /// Gets or sets UserProfileRoles
        /// </summary>
        public IList<UserProfileRoleDto> UserProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets UserAirports
        /// </summary>
        public IList<UserAirportDto> UserAirports { get; set; }


        /// <summary>
        /// Gets or sets ModulePermissions
        /// </summary>
        public IList<ModulePermissionDto> ModulePermissions { get; set; }
        
        /// <summary>
        /// Gets or sets JetFuelTickets
        /// </summary>
        public IList<JetFuelTicketDto> JetFuelTicket { get; set; }

        /// <summary>
        /// Gets or sets Passenger Information
        /// </summary>
        public IList<PassengerInformationDto> PassengerInformation { get; set; }
    }
}
