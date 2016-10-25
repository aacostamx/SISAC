//------------------------------------------------------------------------
// <copyright file="ItinerariesMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Business.MapConfiguration
{
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

        private void Map()
        {
            Mapper.CreateMap<User, UserDto>()
                .ReverseMap();

            Mapper.CreateMap<UserAirport, UserAirportDto>()
                .ReverseMap();

            Mapper.CreateMap<UserProfileRole, UserProfileRoleDto>()
                .ReverseMap();

            Mapper.CreateMap<Department, DepartmentDto>()
                .ReverseMap();

            //Mapper.CreateMap<UserType, UserTypeDto>()
            //    .ReverseMap();

            Mapper.CreateMap<PageReport, PageReportDto>()
                .ReverseMap();

            Mapper.CreateMap<ModulePermission, ModulePermissionDto>()
                .ReverseMap();

            Mapper.CreateMap<Role, RoleDto>()
                .ReverseMap();

            Mapper.CreateMap<UserAirport, UserAirportDto>()
                .ReverseMap();

            Mapper.CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            Mapper.CreateMap<Module, ModuleDto>()
               .ReverseMap();

            Mapper.CreateMap<Entities.Security.Profile, ProfileDto>()
                .ReverseMap();

            Mapper.CreateMap<ProfileRole, ProfileRoleDto>()
                .ReverseMap();

            Mapper.CreateMap<Menu, MenuDto>()
               .ReverseMap();

            Mapper.CreateMap<Department, DepartmentDto>()
               .ReverseMap();

            Mapper.CreateMap<UserProfileRole, UserProfileRoleDto>()
               .ReverseMap();

        }
    }
}
