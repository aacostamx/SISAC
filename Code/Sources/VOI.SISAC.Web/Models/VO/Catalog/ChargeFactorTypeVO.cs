//----------------------------------------------------------------
// <copyright file="ChargeFactorTypeVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ChargeFactorTypeVO
    /// </summary>
    public class ChargeFactorTypeVO
    {
        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        [Display(Name = "ChargeFactorTypeID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(4, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax4")]
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// ChargeFactorTypeName
        /// </summary>
        [Display(Name = "ChargeFactorTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax20")]
        public string ChargeFactorTypeName { get; set; }

        ///// <summary>
        ///// InternationalFuelContractConcept
        ///// </summary>
        //public ICollection<ChargeFactorTypeVO> InternationalFuelContractConcept { get; set; }
    }
}