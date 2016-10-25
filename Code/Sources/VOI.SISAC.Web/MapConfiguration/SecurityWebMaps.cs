//------------------------------------------------------------------------
// <copyright file="SecurityWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
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

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Map();
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private static void Map()
        {
            Mapper.CreateMap<UserDto, UserVO>()
                .ReverseMap();

            Mapper.CreateMap<UserAirportDto, UserAirportVO>()
                .ReverseMap();

            Mapper.CreateMap<UserProfileRoleDto, UserProfileRoleVO>()
                .ReverseMap();

            Mapper.CreateMap<DepartmentDto, DepartmentVO>()
                .ReverseMap();

            Mapper.CreateMap<MenuDto, MenuVO>()
                .ReverseMap();

            Mapper.CreateMap<PageReportDto, PageReportVO>()
                .ReverseMap();

            Mapper.CreateMap<ModulePermissionDto, ModulePermissionVO>()
                .ReverseMap();

            Mapper.CreateMap<RoleDto, RoleVO>()
                .ReverseMap();

            Mapper.CreateMap<PermissionDto, PermissionVO>()
                .ReverseMap();

            Mapper.CreateMap<ModuleDto, ModuleVO>()
               .ReverseMap();

            Mapper.CreateMap<ProfileDto, ProfileVO>()
                .ReverseMap();

            Mapper.CreateMap<ProfileRoleDto, ProfileRoleVO>()
                .ReverseMap();

            Mapper.CreateMap<ProfileDto, ProfileRoleDto>()
                .ReverseMap();
        }
    }
}