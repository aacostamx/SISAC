//------------------------------------------------------------------------
// <copyright file="User.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using Process;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Airport;
    /// <summary>
    /// Class User
    /// </summary>
    [Table("Security.User")]
    public partial class User
    {
        public User()
        {
            this.UserProfileRoles = new List<UserProfileRole>();
            this.UserAirports = new List<UserAirport>();
            this.ModulePermissions = new List<ModulePermission>();
            this.JetFuelTicket = new List<JetFuelTicket>();
            this.PassengerInformation = new List<PassengerInformation>();
        }

        /// <summary>
        /// Gets or sets UserID
        /// </summary>
        [Key]
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets Airline Code
        /// </summary>
        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }
        /// <summary>
        /// Gets or sets Employee Number
        /// </summary>
        [Required]
        [StringLength(10)]
        public string EmployeeNumber { get; set; }
        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets Password Encripted
        /// </summary>
        [StringLength(50)]
        public string PasswordEncripted { get; set; }
        /// <summary>
        /// Gets or sets Name
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets FirstName
        /// </summary>
        [Required]
        [StringLength(80)]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets LastName
        /// </summary>
        [StringLength(80)]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets Email
        /// </summary>
        [StringLength(90)]
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
        ///// <summary>
        ///// Gets or sets License
        ///// </summary>
        //[Column(TypeName = "numeric")]
        //public decimal? License { get; set; }
        /// <summary>
        /// Gets or sets user volaris
        /// </summary>
        public bool UserVolaris { get; set; }
        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Gets or sets CompanyDepartment
        /// </summary>
        public virtual Department Departments { get; set; }
        /// <summary>
        /// Gets or sets Airlines
        /// </summary>
        public virtual Airline Airlines { get; set; }
        ///// <summary>
        ///// Gets or sets UserType
        ///// </summary>
        //public virtual UserType UserType { get; set; }
        /// <summary>
        /// Gets or sets UserProfileRoles
        /// </summary>
        public virtual ICollection<UserProfileRole> UserProfileRoles { get; set; }
        /// <summary>
        /// Gets or sets UserAirports
        /// </summary>
        public virtual ICollection<UserAirport> UserAirports { get; set; }
        /// <summary>
        /// Gets or sets ModulePermissions
        /// </summary>
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }
        /// <summary>
        /// Gets or sets JetFuelTickets
        /// </summary>
        public virtual ICollection<JetFuelTicket> JetFuelTicket { get; set; }

        /// <summary>
        /// Gets or sets Passenger Informations
        /// </summary>
        public virtual ICollection<PassengerInformation> PassengerInformation { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel tickets.
        /// </summary>
        /// <value>
        /// The national jet fuel tickets.
        /// </value>
        public virtual ICollection<NationalJetFuelTicket> NationalJetFuelTickets { get; set; }
    }
}
