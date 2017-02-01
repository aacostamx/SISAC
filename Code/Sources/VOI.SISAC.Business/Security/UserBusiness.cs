//------------------------------------------------------------------------
// <copyright file="UserBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Entities.Security;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// UserBusiness
    /// </summary>
    public class UserBusiness : IUserBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The accountingAccount repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userRepository">The userRepository</param>
        public UserBusiness(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public IList<UserDto> GetUsersStation(string stationCode)
        {
            try
            {
                IList<User> users = this.userRepository.GetActivesAORUsersByStationCode(stationCode).ToList();
                IList<UserDto> usersDto = new List<UserDto>();

                usersDto = Mapper.Map<IList<User>, IList<UserDto>>(users);
                return usersDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// GetActiveUsers
        /// </summary>
        /// <returns></returns>
        public IList<UserDto> GetActiveUsers()
        {
            try
            {
                IList<User> userList = this.userRepository.GetActivesUsers();
                IList<UserDto> userDtoList = new List<UserDto>();
                userDtoList = Mapper.Map<IList<User>, IList<UserDto>>(userList);
                return userDtoList;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// FindUserById
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserDto FindUserById(long userID)
        {
            try
            {
                User user = this.userRepository.FindById(userID);
                UserDto userDto = new UserDto();
                userDto = Mapper.Map<User, UserDto>(user);

                return userDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool AddUser(UserDto userDto)
        {
            User user = new User();
            if (userDto == null)
            {
                return false;
            }

            if (this.IsUserNameDuplicate(userDto.UserName))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                user = Mapper.Map<UserDto, User>(userDto);

                IList<UserAirport> airports = user.UserAirports.ToList();
                IList<UserProfileRole> profileRoles = user.UserProfileRoles.ToList();
                IList<ModulePermission> modulePermissions = user.ModulePermissions.ToList();

                user.UserAirports = new List<UserAirport>();
                user.UserProfileRoles = new List<UserProfileRole>();
                user.ModulePermissions = new List<ModulePermission>();
                user.Status = true;

                //Add User
                user.CreationDate = this.userRepository.GetServerDate();
                this.userRepository.Add(user);
                this.unitOfWork.Commit();

                //Add Airports to User
                this.userRepository.AddUserAirports(user, airports);
                this.unitOfWork.Commit();

                //Add ProfileRole to User
                this.userRepository.AddUserProfileRole(user, profileRoles);
                this.unitOfWork.Commit();

                //Add ModulePermission to User
                this.userRepository.AddUserModulePermission(user, modulePermissions);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        private bool IsUserNameDuplicate(string userName)
        {
            User encontrado = this.userRepository.FindByUserName(userName);
            return encontrado != null ? true : false;
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool UpdateUser(UserDto userDto)
        {
            User user = new User();
            if (userDto.UserID == 0)
            {
                return false;
            }

            if (this.IsUserNameDuplicateEdit(userDto.UserName, userDto.UserID))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                
                user = Mapper.Map<UserDto, User>(userDto);
                
                IList<UserAirport> airports = user.UserAirports.ToList();
                IList<UserProfileRole> profileRoles = user.UserProfileRoles.ToList();
                IList<ModulePermission> modulePermissions = user.ModulePermissions.ToList();

                //quitar relaciones actuales
                user.UserAirports = new List<UserAirport>();
                user.UserProfileRoles = new List<UserProfileRole>();
                user.ModulePermissions = new List<ModulePermission>();
                user.JetFuelTicket = new List<JetFuelTicket>();
                user.PassengerInformation = new List<PassengerInformation>();
                //user.Status = true;


                //Update User Data Basic without relations
                this.userRepository.Update(user);
                this.unitOfWork.Commit();

                //Delete User Relations
                this.userRepository.DeleteUserRelations(user);
                this.unitOfWork.Commit();

                //Add Airports to User
                this.userRepository.AddUserAirports(user, airports);
                this.unitOfWork.Commit();

                //Add ProfileRole to User
                this.userRepository.AddUserProfileRole(user, profileRoles);
                this.unitOfWork.Commit();

                //Add ModulePermission to User
                this.userRepository.AddUserModulePermission(user, modulePermissions);
                this.unitOfWork.Commit();

                return true;                
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        private bool IsUserNameDuplicateEdit(string userName, long userId)
        {
            User encontrado = this.userRepository.FindByUserNameEdit(userName, userId);
            return encontrado != null ? true : false;
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool DeleteUser(UserDto userDto)
        {
            try
            {
                User user = this.userRepository.FindById(userDto.UserID);

                if (user != null)
                {
                    user.Status = false;
                    this.userRepository.Update(user);
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
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User information.</returns>
        public UserDto GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            try
            {
                User user = this.userRepository.GetUserByUserName(userName);
                UserDto userDto = new UserDto();
                if (user != null)
                {
                   userDto = Mapper.Map<User, UserDto>(user);
                }

                return userDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Resources.Messages.FailedFindRecord, exception);
            }
        }
    }
}
