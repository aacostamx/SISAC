//------------------------------------------------------------------------
// <copyright file="SecurityWebMaps.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Web.Models.VO.Security;

    /// <summary>
    /// Configurations for the maps between Entities and Dto's
    /// </summary>
    public class SecurityWebMaps : AutoMapper.Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return "SecurityWebMaps";
            }
        }

        protected override void Configure()
        {
            Map();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityWebMaps"/> class.
        /// </summary>
        public SecurityWebMaps()
        {
            
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            CreateMap<UserDto, UserVO>()
                .ReverseMap();

            CreateMap<UserAirportDto, UserAirportVO>()
                .ReverseMap();

            CreateMap<UserProfileRoleDto, UserProfileRoleVO>()
                .ReverseMap();

            CreateMap<DepartmentDto, DepartmentVO>()
                .ReverseMap();

            CreateMap<MenuDto, MenuVO>()
                .ReverseMap();

            CreateMap<PageReportDto, PageReportVO>()
                .ReverseMap();

            CreateMap<ModulePermissionDto, ModulePermissionVO>()
                .ReverseMap();

            CreateMap<RoleDto, RoleVO>()
                .ReverseMap();

            CreateMap<PermissionDto, PermissionVO>()
                .ReverseMap();

            CreateMap<ModuleDto, ModuleVO>()
                .ReverseMap();

            CreateMap<ProfileDto, ProfileVO>()
                .ReverseMap();

            CreateMap<ProfileRoleDto, ProfileRoleVO>()
                .ReverseMap();

            CreateMap<ProfileDto, ProfileRoleDto>()
                .ReverseMap();
        }
    }
}