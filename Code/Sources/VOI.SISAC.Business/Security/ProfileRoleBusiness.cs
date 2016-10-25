//------------------------------------------------------------------------
// <copyright file="ProfileRoleBusiness.cs" company="Volaris">
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
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Dal.Configuration;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;
    using VOI.SISAC.Business.Common;
    using AutoMapper;
    using MapConfiguration;
    using ExceptionBusiness;
    using Resources;
    using System.Globalization;
    using Dal.Infrastructure;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Profile roles operations
    /// </summary>
    public class ProfileRoleBusiness : IProfileRoleBusiness
    {
        /// <summary>
        /// The Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The profile role repository
        /// </summary>
        private readonly IProfileRoleRepository profileRoleRepository;

        /// <summary>
        /// The profile repository
        /// </summary>
        private readonly IProfileRepository profileRepository;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRoleBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="profileRoleRepository">The profile role repository.</param>
        /// <param name="profileRepository">The profile repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        public ProfileRoleBusiness(
            IUnitOfWork unitOfWork,
            IProfileRoleRepository profileRoleRepository,
            IProfileRepository profileRepository,
            IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
            this.roleRepository = roleRepository;
            this.profileRoleRepository = profileRoleRepository;
        }

        /// <summary>
        /// Adds or deletes a profile and its roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="rolesDto">The roles.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>
        /// </returns>
        public bool UpdatesProfileAndRoles(string profileCode, IList<RoleDto> rolesDto)
        {
            if (string.IsNullOrWhiteSpace(profileCode))
            {
                return false;
            }

            if (rolesDto == null || rolesDto.Count == 0)
            {
                return false;
            }

            try
            {
                IList<Role> rolesUnmarked = new List<Role>();
                IList<Role> roleMarked = new List<Role>();

                // First separate the role between marked and unmarked.                
                foreach (RoleDto item in rolesDto)
                {
                    Role role = Mapper.Map<RoleDto, Role>(item);
                    if (item.IsChecked)
                    {
                        roleMarked.Add(role);
                    }
                    else
                    {
                        rolesUnmarked.Add(role);
                    }
                }

                this.InsertRoleInProfile(roleMarked, profileCode);
                this.DeleteRolesInProfile(rolesUnmarked, profileCode);
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the roles by profile code.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// List of <see cref="GenericCatalogDto" />.
        /// </returns>
        public IList<GenericCatalogDto> FindRolesByProfileCode(string profileCode)
        {
            if (string.IsNullOrWhiteSpace(profileCode))
            {
                return null;
            }

            try
            {
                List<GenericCatalogDto> catalog = new List<GenericCatalogDto>();
                List<Role> roles = this.profileRoleRepository.GetRolesByProfile(profileCode).ToList();
                if (roles != null || roles.Count > 0)
                {
                    foreach (Role item in roles)
                    {
                        catalog.Add(new GenericCatalogDto
                        {
                            Id = item.RoleCode,
                            Description = item.RoleName
                        });
                    }
                }

                return catalog;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the roles by profile code.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of roles by profile.</returns>
        public IList<GenericCatalogDto> FindRolesByProfileCode(string id, string selectItem)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                List<Role> roles = new List<Role>();
                roles = this.profileRoleRepository.GetRolesByProfile(id).ToList();
                List<GenericCatalogDto> catalog = new List<GenericCatalogDto>();
                catalog.Add(new GenericCatalogDto() { Id = string.Empty, Description = selectItem });
                foreach (Role item in roles)
                {
                    catalog.Add(new GenericCatalogDto
                    {
                        Id = item.RoleCode,
                        Description = item.RoleName
                    });
                }

                return catalog;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets all the roles and marks the roles that are selected for the profile.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        /// The roles for the profile.
        /// </returns>
        public IList<RolesMarkedByProfileDto> GetAllRolesByProfile(string profileCode)
        {
            if (string.IsNullOrWhiteSpace(profileCode))
            {
                return null;
            }

            try
            {
                // Gets all roles and marks the related with the profile
                List<RolesMarkedByProfileDto> rolesDto = this.MarkSelectedRolesInProfile(profileCode);

                // Marks the roles that have a relationship with other tables
                this.MarkIfHasRelationship(profileCode, rolesDto);
                return rolesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Get All Profiles
        /// </summary>
        /// <returns>List of Profile roles.</returns>
        public IList<ProfileRoleDto> GetAllProfiles()
        {
            IList<ProfileRole> profiles = new List<ProfileRole>();
            IList<ProfileRoleDto> profilesDto = new List<ProfileRoleDto>();
            try
            {
                profiles = this.profileRoleRepository.GetAll();
                profilesDto = Mapper.Map<IList<ProfileRoleDto>>(profiles);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return profilesDto;
        }
       
        /// <summary>
        /// Marks the selected roles in profile.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>List of roles.</returns>
        /// <remarks>Gets all the roles and marks the roles that are link with the profile.</remarks>
        private List<RolesMarkedByProfileDto> MarkSelectedRolesInProfile(string profileCode)
        {
            List<RolesMarkedByProfileDto> rolesDto = new List<RolesMarkedByProfileDto>();

            // Gets the roles link with the profile
            List<Role> rolesByProfile = this.profileRoleRepository.GetRolesByProfile(profileCode).ToList();

            // Gets all roles
            List<Role> allRoles = this.roleRepository.GetAll().ToList();

            // Maps the roles into the RolesMarkedByProfileDto object
            if (allRoles != null)
            {
                foreach (Role item in allRoles)
                {
                    rolesDto.Add(new RolesMarkedByProfileDto
                    {
                        RoleCode = item.RoleCode,
                        RoleName = item.RoleName,
                        Selected = false
                    });
                }
            }

            // Marks the role that are related with the profile.
            if (rolesByProfile != null)
            {
                List<RolesMarkedByProfileDto> rolessChecked = rolesDto
                    .Where(c =>
                           rolesByProfile.Select(p => p.RoleCode).Contains(c.RoleCode))
                    .ToList();

                foreach (RolesMarkedByProfileDto item in rolessChecked)
                {
                    item.Selected = true;
                }
            }

            return rolesDto;
        }

        /// <summary>
        /// Deletes the roles in the profile.
        /// </summary>
        /// <param name="role">The roles.</param>
        /// <param name="profileCode">The profile code.</param>
        /// <remarks>
        /// Only records that are not linked with another entity will be deleted.
        /// The records that have relationship are not considered to delete.
        /// </remarks>
        private void DeleteRolesInProfile(IList<Role> role, string profileCode)
        {
            // Finds the roles that are linked with the profile
            List<string> roleCodes = this.profileRoleRepository
                .GetRolesByProfile(profileCode)
                .Select(c => c.RoleCode)
                .ToList();

            // From the list, gets the roles that match
            List<Role> rolesFound = role
                .Where(c => roleCodes.Contains(c.RoleCode))
                .ToList();

            // Select the roles that have no relations with Role and/or User
            List<Role> rolesToDelete = new List<Role>();
            foreach (Role item in rolesFound)
            {
                ProfileRole roleProfile = this.profileRoleRepository
                    .FindBy(profileCode, item.RoleCode);

                // Only select the module permission records that are not related with another entity. 
                // If there are records that are related, these are not considered to delete.
                if (roleProfile.UserProfileRoles.Count <= 0)
                {
                    rolesToDelete.Add(item);
                }
            }

            this.profileRoleRepository.DeleteProfileAndRoles(profileCode, rolesToDelete);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Inserts the roles in the profile.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="roleCode">The role code.</param>
        private void InsertRoleInProfile(IList<Role> roles, string roleCode)
        {
            // Finds the roles that are linked with the profile.
            List<string> roleCodes = this.profileRoleRepository.GetRolesByProfile(roleCode)
                .Select(c => c.RoleCode)
                .ToList();

            // From the list, gets the roles that are not linked.
            List<Role> rolesToInsert = roles
                .Where(c => !roleCodes.Contains(c.RoleCode))
                .ToList();

            this.profileRoleRepository.AddProfileAndRole(roleCode, rolesToInsert);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Marks if has relationship.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <param name="roles">The roles.</param>
        private void MarkIfHasRelationship(string profileCode, List<RolesMarkedByProfileDto> roles)
        {
            foreach (RolesMarkedByProfileDto item in roles)
            {
                // Select the permissions that have a relations with Role and/or User
                ProfileRole profileRole = this.profileRoleRepository
                    .FindBy(profileCode, item.RoleCode);

                if (profileRole == null)
                {
                    continue;
                }

                // Only select the profile role records that are not related with another entity.
                // If there are records that are related, these are not considered to delete.
                if (profileRole.UserProfileRoles.Count > 0)
                {
                    item.HasRelationship = true;
                }
            }
        }
    }
}
