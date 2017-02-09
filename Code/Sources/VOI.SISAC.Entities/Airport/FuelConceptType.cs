//--------------------------------------------------------------------
// <copyright file="FuelConceptType.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Finance;

    /// <summary>
    /// Class FuelConceptType
    /// </summary>
    [Table("Airport.FuelConceptType")]
    public partial class FuelConceptType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConceptType"/> class.
        /// </summary>
        public FuelConceptType()
        {
            this.NationalFuelContractConcepts = new HashSet<NationalFuelContractConcept>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
        }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        [Key]
        [StringLength(5)]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the fuel concept type.
        /// </summary>
        /// <value>
        /// The name of the fuel concept type.
        /// </value>
        [Required]
        [StringLength(60)]
        public string FuelConceptTypeName { get; set; }

        /// <summary>
        /// Gets or sets the international fuel contract concept.
        /// </summary>
        /// <value>
        /// The international fuel contract concept.
        /// </value>
        public virtual ICollection<InternationalFuelContractConcept> InternationalFuelContractConcept { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concepts.
        /// </summary>
        /// <value>
        /// The national fuel contract concepts.
        /// </value>
        public virtual ICollection<NationalFuelContractConcept> NationalFuelContractConcepts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel rates.
        /// </summary>
        /// <value>
        /// The national fuel rates.
        /// </value>
        public virtual ICollection<NationalFuelRate> NationalFuelRates { get; set; }
    }
}