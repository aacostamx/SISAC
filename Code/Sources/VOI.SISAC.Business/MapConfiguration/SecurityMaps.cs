//------------------------------------------------------------------------
// <copyright file="ItinerariesMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Business.MapConfiguration
{
    using System;
    using AutoMapper;
    using VOI.SISAC.Business.Dto;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Itineraries;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    ///  Configurations for the maps between Entities and Dto's
    /// </summary>
    public class SecurityMaps : AutoMapper.Profile
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
                return "SecurityMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper" /> class from this method.
        /// </summary>
        public SecurityMaps()
        {
            
        }

        private void Map()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<UserAirport, UserAirportDto>()
                .ReverseMap();

            CreateMap<UserProfileRole, UserProfileRoleDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDto>()
                .ReverseMap();

            //CreateMap<UserType, UserTypeDto>()
            //    ..ReverseMap();

            CreateMap<PageReport, PageReportDto>()
                .ReverseMap();

            CreateMap<ModulePermission, ModulePermissionDto>()
                .ReverseMap();

            CreateMap<Role, RoleDto>()
                .ReverseMap();

            CreateMap<UserAirport, UserAirportDto>()
                .ReverseMap();

            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Module, ModuleDto>()
               .ReverseMap();

            CreateMap<Entities.Security.Profile, ProfileDto>()
                .ReverseMap();

            CreateMap<ProfileRole, ProfileRoleDto>()
                .ReverseMap();

            CreateMap<Menu, MenuDto>()
               .ReverseMap();

            CreateMap<Department, DepartmentDto>()
               .ReverseMap();

            CreateMap<UserProfileRole, UserProfileRoleDto>()
               .ReverseMap();

        }
    }
}
