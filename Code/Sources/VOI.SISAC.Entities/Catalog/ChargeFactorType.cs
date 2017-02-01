//------------------------------------------------------------------------
// <copyright file="ChargeFactorType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Finance;

    /// <summary>
    /// Class ChargeFactorType
    /// </summary>
    [Table("Catalog.ChargeFactorType")]
    public partial class ChargeFactorType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChargeFactorType"/> class.
        /// </summary>
        public ChargeFactorType()
        {
            this.NationalFuelContractConcepts = new HashSet<NationalFuelContractConcept>();
        }

        /// <summary>
        /// Gets or sets the charge factor type identifier.
        /// </summary>
        /// <value>
        /// The charge factor type identifier.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the charge factor type.
        /// </summary>
        /// <value>
        /// The name of the charge factor type.
        /// </value>
        [Required]
        [StringLength(20)]
        public string ChargeFactorTypeName { get; set; }

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
    }
}