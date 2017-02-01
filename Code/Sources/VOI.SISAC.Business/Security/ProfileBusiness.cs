//------------------------------------------------------------------------
// <copyright file="ProfileBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Security;
    using VOI.SISAC.Entities.Security;
    using AutoMapper;
    using MapConfiguration;
    using ExceptionBusiness;
    using Resources;

    /// <summary>
    /// Profiles Business Logic
    /// </summary>
    public class ProfileBusiness : IProfileBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Profile Repository
        /// </summary>
        private readonly IProfileRepository profileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="profileRepository"></param>
        public ProfileBusiness(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Get All Profiles
        /// </summary>
        /// <returns></returns>
        public IList<ProfileDto> GetAllProfiles()
        {
            try
            {
                IList<Entities.Security.Profile> profile = profileRepository.GetProfiles().ToList();
                IList<ProfileDto> profileDto = new List<ProfileDto>();
                profileDto = Mapper.Map<IList<Entities.Security.Profile>, IList<ProfileDto>>(profile);
                return profileDto.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get the Profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProfileDto FindProfileById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                Entities.Security.Profile profile = this.profileRepository.FindById(id);
                ProfileDto profileDto = new ProfileDto();
                profileDto = Mapper.Map<Entities.Security.Profile, ProfileDto>(profile);
                return profileDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDto"></param>
        /// <returns></returns>
        public bool AddProfile(ProfileDto profileDto)
        {
            if (profileDto == null)
            {
                return false;
            }
            if (this.IsProfileCodeDuplicated(profileDto.ProfileCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                if (profileDto != null)
                {
                    Entities.Security.Profile profile = new Entities.Security.Profile();
                    profile = Mapper.Map<ProfileDto, Entities.Security.Profile>(profileDto);
                    this.profileRepository.Add(profile);
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
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Update a Profile
        /// </summary>
        /// <param name="profileDto"></param>
        /// <returns></returns>
        public bool UpdateProfile(ProfileDto profileDto)
        {
            try
            {
                Entities.Security.Profile profile = this.profileRepository.FindById(profileDto.ProfileCode);
                profile.ProfileCode = profileDto.ProfileCode;
                profile.ProfileName = profileDto.ProfileName;
                this.profileRepository.Update(profile);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        public bool DeleteProfile(string profileCode)
        {
            if (profileCode == null)
            {
                return false;
            }

            try
            {
                Entities.Security.Profile profile = this.profileRepository.FindById(profileCode);

                if (profile != null)
                {
                    this.profileRepository.Delete(profile);
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
        /// Determines whether [is profile code duplicated] [the specified profile code].
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns><c>true</c> if duplicated otherwise <c>false</c>.</returns>
        private bool IsProfileCodeDuplicated(string profileCode)
        {
            Entities.Security.Profile profile = this.profileRepository.FindById(profileCode);
            return profile != null ? true : false;
        }
    }
}
