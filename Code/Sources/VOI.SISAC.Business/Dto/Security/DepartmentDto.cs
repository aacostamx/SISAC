//------------------------------------------------------------------------
// <copyright file="DepartmentDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Class DepartmentDto
    /// </summary>
    public class DepartmentDto
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
        public ICollection<UserDto> Users { get; set; }
    }
}
