//------------------------------------------------------------------------
// <copyright file="LoginVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login veiw model
    /// </summary>
    public class LoginVO
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Resource))]
        public bool RememberMe { get; set; }
    }
}