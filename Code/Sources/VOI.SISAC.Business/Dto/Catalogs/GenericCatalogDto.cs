//------------------------------------------------------------------------
// <copyright file="GenericCatalogDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// Generic Catalog
    /// </summary>
    public class GenericCatalogDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the code + description.
        /// </summary>
        /// <value>
        /// The code + description.
        /// </value>
        public string CodeAndDescription
        {
            get
            {
                return this.Id + " - " + this.Description;
            }
        }

        /// <summary>
        /// Gets or sets the code + description.
        /// </summary>
        /// <value>
        /// The code + description.
        /// </value>
        public string DescriptionAndCode
        {
            get
            {
                return this.Description + " - " + this.Id ;
            }
        }
    }
}