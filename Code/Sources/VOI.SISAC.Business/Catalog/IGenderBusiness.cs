//------------------------------------------------------------------------
// <copyright file="IGenderBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Interface IGender Business
    /// </summary>
    public interface IGenderBusiness
    {
        /// <summary>
        /// Gets all gender.
        /// </summary>
        /// <returns>List GenderDto</returns>
        List<GenderDto> GetAllGender();
        
        /// <summary>
        /// Validateds if gender exist.
        /// </summary>
        /// <param name="gendersCodes">The genders codes.</param>
        /// <returns></returns>
        List<string> ValidatedIfGenderExist(IList<string> gendersCodes);
    }
}
