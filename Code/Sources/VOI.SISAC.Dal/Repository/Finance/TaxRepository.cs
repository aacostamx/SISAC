//------------------------------------------------------------------------
// <copyright file="TaxRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Tax Repository
    /// </summary>
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxRepository"/> class.
        /// </summary>
        /// <param name="factory"></param>
        public TaxRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region ITaxRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Tax Entity.</returns>
        public Tax FindById(string id)
        {
            var tax = this.DbContext.Taxes.Where(c => c.TaxCode == id).FirstOrDefault();
            return tax;
        }

        /// <summary>
        /// Gets the Actives Taxes.
        /// </summary>
        /// <returns>Taxes marked as Actives.</returns>
        public IList<Tax> GetActivesTaxes()
        {
            return this.DbContext.Taxes.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the taxes exist.
        /// </summary>
        /// <param name="taxCodes">The taxes codes to validate.</param>
        /// <returns>Returns a list with the Tax Codes that do not exist.</returns>
        public IList<string> ValidatedIfTaxExist(IList<string> taxCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Tax> taxList = this.DbContext.Taxes.Where(c => c.Status).ToList();

            notFound = taxCodes.Except(taxList.Select(c => c.TaxCode)).ToList();
            return notFound;
        }
        #endregion
    }
}
