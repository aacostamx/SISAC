//------------------------------------------------------------------------
// <copyright file="ModuleVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class VO Module
    /// </summary>
    public class ModuleVO
    {
        /// <summary>
        /// Gets or sets the module code.
        /// </summary>
        /// <value>
        /// The module code.
        /// </value>
        [Display(Name = "ModuleCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, MinimumLength = 1, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax10")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        [Display(Name = "ModuleName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax150")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        [Display(Name = "MenuCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax150")]
        public string MenuCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        [Display(Name = "ControllerName", ResourceType = typeof(Resources.Resource))]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax150")]
        public string ControllerName { get; set; }


        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public MenuVO Menu { get; set; }

        /// <summary>
        /// Gets or sets the module permissions.
        /// </summary>
        /// <value>
        /// The module permissions.
        /// </value>
        public ICollection<ModulePermissionVO> ModulePermissions { get; set; }
    }
}