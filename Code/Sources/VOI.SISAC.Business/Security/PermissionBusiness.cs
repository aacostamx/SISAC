//----------------------------------------------------------------------------
// <copyright file="PermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// PermissionBusiness
    /// </summary>
    public class PermissionBusiness : IPermissionBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Permission Repository
        /// </summary>
        private readonly IPermissionRepository permissionRepository;

        /// <summary>
        /// PermissionBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="permissionRepository"></param>
        public PermissionBusiness(
            IUnitOfWork unitOfWork,
            IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetPermits
        /// </summary>
        /// <returns></returns>
        public IList<PermissionDto> GetPermits()
        {
            try
            {
                IList<Permission> permissionList = this.permissionRepository.GetAll().ToList();
                IList<PermissionDto> permissionListDto = new List<PermissionDto>();

                permissionListDto = Mapper.Map<IList<Permission>, IList<PermissionDto>>(permissionList);
                return permissionListDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// FindPermissionById
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        public PermissionDto FindPermissionById(string permissionCode)
        {
            try
            {
                Permission permission = this.permissionRepository.FindById(permissionCode);
                PermissionDto permissionDto = new PermissionDto();
                permissionDto = Mapper.Map<Permission, PermissionDto>(permission);

                return permissionDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// AddPermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        public bool AddPermission(PermissionDto permissionDto)
        {
            if (permissionDto == null)
            {
                return false;
            }

            if (this.IsPermissionCodeDuplicate(permissionDto.PermissionCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                this.permissionRepository.Add(Mapper.Map<Permission>(permissionDto));
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// DeletePermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        public bool DeletePermission(PermissionDto permissionDto)
        {
            try
            {
                Permission permission = this.permissionRepository.FindById(permissionDto.PermissionCode);

                if (permission != null)
                {
                    this.permissionRepository.Delete(permission);
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
        /// UpdatePermission
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        public bool UpdatePermission(PermissionDto permissionDto)
        {
            try
            {
                if (permissionDto != null)
                {
                    Permission permission = this.permissionRepository.FindById(permissionDto.PermissionCode);                    

                    if (permission != null)
                    {
                        permission.PermissionName = permissionDto.PermissionName;
                        this.permissionRepository.Update(permission);
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
        /// IsPermissionCodeDuplicate
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        private bool IsPermissionCodeDuplicate(string permissionCode)
        {
            Permission encontrado = this.permissionRepository.FindById(permissionCode);
            return encontrado != null ? true : false;
        }
    }
}
