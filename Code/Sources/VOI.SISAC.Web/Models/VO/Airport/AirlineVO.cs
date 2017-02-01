

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Models.VO.Security;

    /// <summary>
    /// Class Airline VO
    /// </summary>
    public class AirlineVO
    {
        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMin2")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the airline.
        /// </summary>
        /// <value>
        /// The name of the airline.
        /// </value>
        [Display(Name = "AirlineName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(40, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax40")]
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the airline.
        /// </summary>
        /// <value>
        /// The short name of the airline.
        /// </value>
        [Display(Name = "AirlineShortName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMin3")]
        public string AirlineShortName { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirlineVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>
        /// The company code.
        /// </value>
        [Display(Name = "CompanyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax10")]
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the division.
        /// </summary>
        /// <value>
        /// The division.
        /// </value>
        [Display(Name = "Division", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax10")]
        public string Division { get; set; }

        /// </summary>
        /// <value>
        /// The division.
        /// </value>
        [Display(Name = "BusinessName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax100")]
        public string BusinessName { get; set; }

        /// <summary>
        /// Gets or sets the cost centers.
        /// </summary>
        /// <value>
        /// The cost centers.
        /// </value>        
        public ICollection<CostCenterVO> CostCenters { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        /// <value>
        /// user.
        /// </value>        
        public ICollection<UserVO> Users { get; set; }
    }
}