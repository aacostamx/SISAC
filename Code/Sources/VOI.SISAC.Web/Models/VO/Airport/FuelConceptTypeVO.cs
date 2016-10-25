namespace VOI.SISAC.Web.Models.VO.Airport
{
    using Finance;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// FuelConceptTypeVO Class
    /// </summary>
    public class FuelConceptTypeVO
    {
        /// <summary>
        /// Key Fuel Concept Type Code
        /// </summary>
        [Display(Name = "FuelConceptTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(5, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax5")]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Fuel Concept Type
        /// </summary>
        [Display(Name = "FuelConceptTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(60, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax60")]
        public string FuelConceptTypeName { get; set; }

        /// <summary>
        /// InternationalFuelContractConcept Relationship
        /// </summary>
        public ICollection<InternationalFuelContractConceptVO> InternationalFuelContractConcept { get; set; }
    }
}