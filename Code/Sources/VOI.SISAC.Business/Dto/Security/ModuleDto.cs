//------------------------------------------------------------------------
// <copyright file="ModuleDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// 
    /// </summary>
    public class ModuleDto
    {
        /// <summary>
        /// Gets or sets Module ID
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets Moudule Name
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets Action Name
        /// </summary>        
        public string MenuCode { get; set; }

        /// <summary>
        /// Gets or sets Controller Name
        /// </summary>        
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public virtual MenuDto Menu { get; set; }

        /// <summary>
        /// Gets or sets Module Permissions
        /// </summary>
        public ICollection<ModulePermissionDto> ModulePermissions { get; set; }
    }
}
