//------------------------------------------------------------------------
// <copyright file="IMenuBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Security;

    public interface IMenuBusiness
    {
        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <returns></returns>
        IList<MenuDto> GetAllMenu();
    }
}
