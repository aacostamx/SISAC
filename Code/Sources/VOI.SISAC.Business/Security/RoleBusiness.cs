//----------------------------------------------------------------------------
// <copyright file="RoleBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// RoleBusiness
    /// </summary>
    public class RoleBusiness : IRoleBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Role Repository
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// RoleBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="roleRepository"></param>
        public RoleBusiness(
            IUnitOfWork unitOfWork,
            IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <returns></returns>
        public IList<RoleDto> GetRoles()
        {
            try
            {
                IList<Role> roleList = this.roleRepository.GetRoles().ToList();
                IList<RoleDto> roleListDto = new List<RoleDto>();

                roleListDto = Mapper.Map<IList<Role>, IList<RoleDto>>(roleList);
                return roleListDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// FindRoleById
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public RoleDto FindRoleById(string roleCode)
        {
            try
            {
                Role role = this.roleRepository.FindById(roleCode);
                RoleDto roleDto = new RoleDto();
                roleDto = Mapper.Map<Role, RoleDto>(role);

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        public bool AddRole(RoleDto roleDto)
        {
            if (roleDto == null)
            {
                return false;
            }

            if (this.IsRoleCodeDuplicate(roleDto.RoleCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                this.roleRepository.Add(Mapper.Map<Role>(roleDto));
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        public bool DeleteRole(RoleDto roleDto)
        {
            try
            {
                Role role = this.roleRepository.FindById(roleDto.RoleCode);

                if (role != null)
                {
                    this.roleRepository.Delete(role);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// UpdateRole
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        public bool UpdateRole(RoleDto roleDto)
        {
            try
            {
                if (roleDto != null)
                {
                    Role role = this.roleRepository.FindById(roleDto.RoleCode);

                    if (role != null)
                    {
                        role.RoleName = roleDto.RoleName;
                        this.roleRepository.Update(role);
                        this.unitOfWork.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// IsRoleCodeDuplicate
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        private bool IsRoleCodeDuplicate(string roleCode)
        {
            Role encontrado = this.roleRepository.FindById(roleCode);
            return encontrado != null ? true : false;
        }

        /// <summary>
        /// AddRoleModule
        /// </summary>
        /// <param name="roleModule"></param>
        /// <returns></returns>
        public bool AddRoleModule(RoleDto roleModule)
        {
            IList<ModulePermission> modules;
            Role role = new Role();
            if (roleModule == null)
            {
                return false;
            }
            try
            {
                role = Mapper.Map<Role>(roleModule);
                modules = role.ModulePermissions.ToList();
                role.ModulePermissions = new List<ModulePermission>();
                this.roleRepository.AddRoleModule(role, modules);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Find by Role As No Tracking
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public RoleDto FindRoleByIdNoTracking(string roleCode)
        {
            try
            {
                Role role = this.roleRepository.FindByIdNoTracking(roleCode);
                RoleDto roleDto = new RoleDto();
                roleDto = Mapper.Map<Role, RoleDto>(role);

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Edit Role Module
        /// </summary>
        /// <param name="roleModule"></param>
        /// <returns></returns>
        public bool EditRoleModule(RoleDto roleModule)
        {
            Role role = new Role();
            IList<ModulePermission> modulePermissions = new List<ModulePermission>();
            IList<ModulePermissionDto> modules = new List<ModulePermissionDto>();
            IList<ModulePermission> addModules = new List<ModulePermission>();

            if (roleModule.ModulePermissions == null)
            {
                return false;
            }
            try
            {
                role = this.roleRepository.FindById(roleModule.RoleCode);
                modulePermissions = role.ModulePermissions.ToList();

                foreach (var item in modulePermissions)
                {
                    var exists = roleModule.ModulePermissions
                        .Any(c => c.ModuleCode == item.ModuleCode && c.PermissionCode == item.PermissionCode);

                    if (!exists)
                    {
                        role.ModulePermissions.Remove(item);
                    }
                }

                foreach (var item in roleModule.ModulePermissions)
                {
                    var exists = modulePermissions
                        .Any(c => c.ModuleCode == item.ModuleCode && c.PermissionCode == item.PermissionCode);

                    if (!exists)
                    {
                        modules.Add(item);
                    }
                }
                addModules = Mapper.Map<IList<ModulePermission>>(modules);
                this.roleRepository.AddRoleModule(role, addModules);

                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Count User Profile Roles
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public int CountUserProfileRoles(string roleCode)
        {
            int count = 0;
            try
            {
                Role role = this.roleRepository.RoleUserProfileRoles(roleCode);

                if (role.ProfileRoles != null)
                {
                    foreach (var item in role.ProfileRoles)
                    {
                        foreach (var roles in item.UserProfileRoles)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return count;
        }
    }
}
