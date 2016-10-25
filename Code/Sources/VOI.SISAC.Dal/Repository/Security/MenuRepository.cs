//------------------------------------------------------------------------
// <copyright file="MenuRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Security;

    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
         #region Contructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="AirlineRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public MenuRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        public Menu FindById(string id)
        {
            Menu menu = this.DbContext.Menus.Where(c => c.MenuCode == id).FirstOrDefault();
            return menu; 
        }
    }
}
