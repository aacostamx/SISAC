//------------------------------------------------------------------------
// <copyright file="CountryVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Country VO
    /// </summary>
    public class CountryVO
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [Display(Name = "CountryCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), 
            ErrorMessageResourceName = "LengthMinThree")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country code short.
        /// </summary>
        /// <value>
        /// The country code short.
        /// </value>
        [Display(Name = "CountryCodeShort", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMinTwo")]        
        public string CountryCodeShort { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        [Display(Name = "CountryName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax50")]       
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CountryVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }
    }
}