//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;
    using VOI.SISAC.Business.Resources;

    /// <summary>
    /// Module permission business
    /// </summary>
    public class ModulePermissionBusiness : IModulePermissionBusiness
    {
        /// <summary>
        /// The module permission repository
        /// </summary>
        private readonly IModulePermissionRepository modulePermissionRepository;

        /// <summary>
        /// The module repository
        /// </summary>
        private readonly IModuleRepository moduleRepository;

        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IPermissionRepository permissionRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePermissionBusiness" /> class.
        /// </summary>
        /// <param name="modulePermissionRepository">The module permission.</param>
        /// <param name="moduleRepository">The module repository.</param>
        /// <param name="permissionRepository">The permission repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public ModulePermissionBusiness(
            IModulePermissionRepository modulePermissionRepository,
            IModuleRepository moduleRepository,
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork)
        {
            this.modulePermissionRepository = modulePermissionRepository;
            this.moduleRepository = moduleRepository;
            this.permissionRepository = permissionRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds or deletes a module and its permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissionsDto">The permissions.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>
        /// </returns>
        public bool UpdatesModuleAndPermissions(string moduleCode, IList<PermissionDto> permissionsDto)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return false;
            }

            if (permissionsDto == null || permissionsDto.Count == 0)
            {
                return false;
            }

            try
            {
                IList<Permission> permissionsUnmarked = new List<Permission>();
                IList<Permission> permissionsMarked = new List<Permission>();

                // First separate the permission between marked and unmarked.                
                foreach (PermissionDto item in permissionsDto)
                {
                    Permission permission = Mapper.Map<PermissionDto, Permission>(item);
                    if (item.IsSelected)
                    {
                        permissionsMarked.Add(permission);
                    }
                    else
                    {
                        permissionsUnmarked.Add(permission);
                    }
                }

                this.InsertPermissionsInModule(permissionsMarked, moduleCode);
                this.DeletePermissionsInModule(permissionsUnmarked, moduleCode);
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the permission by module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// The permission for the module.
        /// </returns>
        public IList<GenericCatalogDto> GetPermissionByModule(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return null;
            }

            try
            {
                List<GenericCatalogDto> permissionsDto = new List<GenericCatalogDto>();
                List<Permission> permissions = this.modulePermissionRepository.GetPermissionByModule(moduleCode).ToList();
                if (permissions != null || permissions.Count > 0)
                {
                    foreach (Permission item in permissions)
                    {
                        permissionsDto.Add(new GenericCatalogDto
                        {
                            Id = item.PermissionCode,
                            Description = item.PermissionName
                        });
                    }
                }

                return permissionsDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets all the permissions and marks the permissions that are selected for the module.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// The permission for the module.
        /// </returns>
        public IList<PermissionMarkedByModuleDto> GetAllPermissionByModule(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return null;
            }

            try
            {
                // Gets all permissions and marks the related with the module
                List<PermissionMarkedByModuleDto> permissionsDto = this.MarkSelectedModulesInPermission(moduleCode);

                // Marks the permissions that have a relationship with other tables
                this.MarkIfHasRelationship(moduleCode, permissionsDto);
                return permissionsDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <returns>List of module permissions</returns>
        public IList<ModulePermissionDto> GetAllModules()
        {
            IList<ModulePermission> modules = new List<ModulePermission>();
            IList<ModulePermissionDto> modulesDto = new List<ModulePermissionDto>();
            try
            {
                modules = this.modulePermissionRepository.GetAll();
                modulesDto = Mapper.Map<IList<ModulePermissionDto>>(modules);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return modulesDto;
        }

        /// <summary>
        /// Inserts the permissions in the module.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="moduleCode">The module code.</param>
        private void InsertPermissionsInModule(IList<Permission> permissions, string moduleCode)
        {
            // Finds the permissions that are linked with the module.
            List<string> permissionCodes = this.modulePermissionRepository.GetPermissionByModule(moduleCode)
                .Select(c => c.PermissionCode)
                .ToList();

            // From the list, gets the permissions that are not linked.
            List<Permission> permissionsToInsert = permissions
                .Where(c => !permissionCodes.Contains(c.PermissionCode))
                .ToList();

            this.modulePermissionRepository.AddModuleAndPermissions(moduleCode, permissionsToInsert);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes the permissions in the module.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <param name="moduleCode">The module code.</param>
        /// <remarks>
        /// Only records that are not linked with another entity will be deleted.
        /// The records that have relationship are not considered to delete.
        /// </remarks>
        private void DeletePermissionsInModule(IList<Permission> permissions, string moduleCode)
        {
            // Finds the permissions that are linked with the module
            List<string> permissionCodes = this.modulePermissionRepository
                .GetPermissionByModule(moduleCode)
                .Select(c => c.PermissionCode)
                .ToList();

            // From the list, gets the permissions that match
            List<Permission> permissionsFound = permissions
                .Where(c => permissionCodes.Contains(c.PermissionCode))
                .ToList();

            // Select the permissions that have no relations with Role and/or User
            List<Permission> permissionsToDelete = new List<Permission>();
            foreach (Permission item in permissionsFound)
            {
                ModulePermission modulePermission = this.modulePermissionRepository
                    .FindBy(moduleCode, item.PermissionCode);

                // Only select the module permission records that are not related with another entity. 
                // If there are records that are related, these are not considered to delete.
                if (modulePermission.Roles.Count <= 0 && modulePermission.Users.Count <= 0)
                {
                    permissionsToDelete.Add(item);
                }
            }
            
            this.modulePermissionRepository.DeleteModuleAndPermissions(moduleCode, permissionsToDelete);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Marks the selected modules in permission.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>List of permissions.</returns>
        /// <remarks>Gets all the permission and marks the permissions that are link with the module.</remarks>
        private List<PermissionMarkedByModuleDto> MarkSelectedModulesInPermission(string moduleCode) 
        {
            List<PermissionMarkedByModuleDto> permissionsDto = new List<PermissionMarkedByModuleDto>();
            
            // Gets the permissions link with the module
            List<Permission> permissionsByModule = this.modulePermissionRepository.GetPermissionByModule(moduleCode).ToList();
            
            // Gets all permissions
            List<Permission> allPermissions = this.permissionRepository.GetAll().ToList();

            // Maps the permissions into the PermissionMarkedByModuleDto object
            if (allPermissions != null)
            {
                foreach (Permission item in allPermissions)
                {
                    permissionsDto.Add(new PermissionMarkedByModuleDto
                    {
                        PermissionCode = item.PermissionCode,
                        PermissionName = item.PermissionName,
                        Selected = false
                    });
                }
            }
            
            // Marks the permission that are related with the module.
            if (permissionsByModule != null)
            {
                List<PermissionMarkedByModuleDto> permissionsChecked = permissionsDto
                    .Where(c =>
                        permissionsByModule.Select(p => p.PermissionCode).Contains(c.PermissionCode))
                   .ToList();

                foreach (PermissionMarkedByModuleDto item in permissionsChecked)
                {
                    item.Selected = true;
                }
            }

            return permissionsDto;
        }

        /// <summary>
        /// Marks if has relationship.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <param name="permissions">The permissions.</param>
        private void MarkIfHasRelationship(string moduleCode, List<PermissionMarkedByModuleDto> permissions)
        {
            foreach (PermissionMarkedByModuleDto item in permissions)
            {
                // Select the permissions that have a relations with Role and/or User
                ModulePermission modulePermission = this.modulePermissionRepository
                    .FindBy(moduleCode, item.PermissionCode);

                if (modulePermission == null)
                {
                    continue;
                }

                // Only select the module permission records that are not related with another entity. 
                // If there are records that are related, these are not considered to delete.
                if (modulePermission.Roles.Count > 0 && modulePermission.Users.Count > 0)
                {
                    item.HasRelationship = true;
                }
            }
        }
    }
}
