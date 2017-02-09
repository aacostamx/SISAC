//------------------------------------------------------------------------
// <copyright file="MenuBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;

    public class MenuBusiness : IMenuBusiness
    {
        private readonly IMenuRepository menuRepository;

        public MenuBusiness(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository; 
        }
        public IList<MenuDto> GetAllMenu()
        {
            try
            {
                IList<Menu> menu = this.menuRepository.GetAll();
                IList<MenuDto> menuDto = new List<MenuDto>();
                menuDto = Mapper.Map<IList<Menu>, IList<MenuDto>>(menu);

                return menuDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }
    }
}
