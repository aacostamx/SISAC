//------------------------------------------------------------------------
// <copyright file="DepartmentVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class DepartmentVO
    /// </summary>
    public class DepartmentVO
    {
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>
        /// The company code.
        /// </value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the description area.
        /// </summary>
        /// <value>
        /// The description area.
        /// </value>
        public string DescriptionArea { get; set; }

        /// <summary>
        /// Gets or sets the description department.
        /// </summary>
        /// <value>
        /// The description department.
        /// </value>
        public string DescriptionDepartment { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public Nullable<System.DateTime> CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public Nullable<System.DateTime> EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Department"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ICollection<UserVO> Users { get; set; }
    }
}