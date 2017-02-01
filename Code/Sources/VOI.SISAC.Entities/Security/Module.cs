//------------------------------------------------------------------------
// <copyright file="Module.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Module
    /// </summary>
    public partial class Module
    {
        public Module()
        {
            this.ModulePermissions = new List<ModulePermission>();
        }

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
        /// Gets or sets Module Permissions
        /// </summary>
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public virtual Menu Menu { get; set; }
    }
}
