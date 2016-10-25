//------------------------------------------------------------------------
// <copyright file="ModuleBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Module operations
    /// </summary>
    public class ModuleBusiness : IModuleBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The module repository
        /// </summary>
        private readonly IModuleRepository moduleRepository;

         #region Constructor        
                
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="moduleRepository">The module repository.</param>
        public ModuleBusiness(IUnitOfWork unitOfWork, IModuleRepository moduleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.moduleRepository = moduleRepository;
        }
        #endregion

        /// <summary>
        /// Finds the module by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Module data transfer object.</returns>
        public ModuleDto FindModuleById(string id)
        {
            try
            {
                Module moduleEntity = this.moduleRepository.FindById(id);
                ModuleDto moduleDto = new ModuleDto();
                moduleDto = Mapper.Map<Module, ModuleDto>(moduleEntity);

                return moduleDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <returns>The Module data transfer object.</returns>
        public IList<ModuleDto> GetModule()
        {
            try
            {
                IList<Module> modules = this.moduleRepository.GetAll();
                IList<ModuleDto> modulesDto = new List<ModuleDto>();
                modulesDto = Mapper.Map<IList<Module>, IList<ModuleDto>>(modules);

                return modulesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the module.
        /// </summary>
        /// <param name="moduleDto">The module data transfer object.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        public bool AddModule(ModuleDto moduleDto)
        {
            if (moduleDto != null)
            {
                if (this.IsModuleCodeDuplicate(moduleDto.ModuleCode))
                {
                    throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
                }

                try
                {
                    Module module = Mapper.Map<ModuleDto, Module>(moduleDto);
                    this.moduleRepository.Add(module);
                    this.unitOfWork.Commit();

                    return true;
                }
                catch (Exception exception)
                {
                    throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
                }   
            }

            return false;
        }

        /// <summary>
        /// Updates the module.
        /// </summary>
        /// <param name="moduleDto">The module data transfer object.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        public bool UpdateModule(ModuleDto moduleDto)
        {
            try
            {
                Module module = this.moduleRepository.FindById(moduleDto.ModuleCode);
                module.ModuleName = moduleDto.ModuleName;
                module.MenuCode = moduleDto.MenuCode;
                module.ControllerName = moduleDto.ControllerName;
                this.moduleRepository.Update(module);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="moduleDto">The module data transfer object.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        public bool DeleteModule(ModuleDto moduleDto)
        {
            try
            {
                Module module = this.moduleRepository.FindById(moduleDto.ModuleCode);

                if (module != null)
                {
                    this.moduleRepository.Delete(module);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether [is module code duplicate] [the specified module code].
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns><c>true</c> if the code is duplicated.</returns>
        private bool IsModuleCodeDuplicate(string moduleCode)
        {
            Module module = this.moduleRepository.FindById(moduleCode);
            return module != null ? true : false;
        }
    }
}
