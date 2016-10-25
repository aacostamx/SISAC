//------------------------------------------------------------------------
// <copyright file="GenderBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Utils;

    /// <summary>
    /// Class Gender Business
    /// </summary>
    public class GenderBusiness : IGenderBusiness
    {
        #region IGenderBusiness Members        
        /// <summary>
        /// Gets all gender.
        /// </summary>
        /// <returns>List GenderDto </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public List<GenderDto> GetAllGender()
        {
            try
            {
                List<GenderDto> genders = new List<GenderDto>();
                foreach (var gender in 
                    Enum.GetNames(typeof(VOI.SISAC.Business.Utils.EnumSisac.GENDER)).ToList())
                {
                    genders.Add(new GenderDto 
                    {
                        GenderCode = EnumSisac.GetEnumDescription((EnumSisac.GENDER)Enum.Parse(typeof(EnumSisac.GENDER), gender)),
                        GenderName = gender
                    });
                }

                return genders;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Validateds if gender exist.
        /// </summary>
        /// <param name="gendersCodes">The genders codes.</param>
        /// <returns></returns>
        public List<string> ValidatedIfGenderExist(IList<string> gendersCodes)
        {
            List<string> notFound = new List<string>();
            List<GenderDto> genders = this.GetAllGender();

            notFound = gendersCodes.Except(genders.Select(c => c.GenderCode)).ToList();
            return notFound;
        }

        #endregion
    }
}
