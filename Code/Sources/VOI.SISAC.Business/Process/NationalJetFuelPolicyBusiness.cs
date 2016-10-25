//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Process;
    using Dto.Process;
    using Entities.Process;
    using ExceptionBusiness;
    using Resources;

    /// <summary>
    /// National Jet Fuel Policy Business
    /// </summary>
    public class NationalJetFuelPolicyBusiness : INationalJetFuelPolicyBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly INationalJetFuelPolicyRepository nationalPolicyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyBusiness"/> class.
        /// </summary>
        /// <param name="nationalPolicyRepository">The policy repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public NationalJetFuelPolicyBusiness(INationalJetFuelPolicyRepository nationalPolicyRepository, IUnitOfWork unitOfWork)
        {
            this.nationalPolicyRepository = nationalPolicyRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds the national jet fuel policy by PolicyResultID.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public NationalJetFuelPolicyDto FindNationalJetFuelPolicy(NationalJetFuelPolicyDto policy)
        {
            var policyDto = new NationalJetFuelPolicyDto();

            try
            {
                policyDto = Mapper.Map<NationalJetFuelPolicyDto>
                    (this.nationalPolicyRepository.FindNationalPolicy(new NationalJetFuelPolicy(policy.PolicyResultID)));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }

            return policyDto;
        }

        /// <summary>
        /// Creates the national policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool CreateNationalPolicy(NationalJetFuelPolicyDto policy)
        {
            var created = false;

            if (policy == null)
            {
                return created;
            }
            try
            {
                this.nationalPolicyRepository.Add(Mapper.Map<NationalJetFuelPolicy>(policy));
                this.unitOfWork.Commit();
                created = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return created;
        }

        /// <summary>
        /// Deletes the national policy by PolicyResultID
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteNationalPolicy(NationalJetFuelPolicyDto policy)
        {
            var deleted = false;
            var policyEntity = new NationalJetFuelPolicy();

            try
            {
                policyEntity = this.nationalPolicyRepository
                    .FindNationalPolicy(new NationalJetFuelPolicy(policy.PolicyResultID));

                if (policyEntity != null)
                {
                    this.nationalPolicyRepository.Delete(policyEntity);
                    this.unitOfWork.Commit();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Updates the national policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateNationalPolicy(NationalJetFuelPolicyDto policy)
        {
            var updated = false;
            var policyEntity = new NationalJetFuelPolicy();

            if (policy == null)
            {
                return updated;
            }
            try
            {
                policyEntity = this.nationalPolicyRepository
                    .FindNationalPolicy(new NationalJetFuelPolicy(policy.PolicyResultID));
                policyEntity.DocumentNumber = policy.DocumentNumber;
                policyEntity.NationalJetFuelTicketID = policy.NationalJetFuelTicketID;
                this.unitOfWork.Commit();
                updated = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return updated;
        }

        /// <summary>
        /// Gets all national policies.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyDto> GetAllNationalPolicies()
        {
            var policiesDto = new List<NationalJetFuelPolicyDto>();

            try
            {
                policiesDto = Mapper.Map<List<NationalJetFuelPolicyDto>>(this.nationalPolicyRepository.GetAll());
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return policiesDto;
        }

        /// <summary>
        /// Gets the unsuccessful national policies by NationalPolicyId
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyDto> GetUnsuccessfulNationalPolicies(NationalJetFuelPolicyDto policy)
        {
            var policiesDto = new List<NationalJetFuelPolicyDto>();
            var unsucessPolicies = new List<NationalJetFuelPolicyDto>();

            try
            {
                policiesDto = Mapper.Map<List<NationalJetFuelPolicyDto>>(this.nationalPolicyRepository
                    .GetNationalPoliciesControl(new NationalJetFuelPolicy() { NationalPolicyID = policy.NationalPolicyID }));

                unsucessPolicies = policiesDto.Except(policiesDto.Where(c => c.MENV == "S")).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return unsucessPolicies;
        }

        /// <summary>
        /// Gets the national policies by control - NationalPolicyId
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyDto> GetNationalPoliciesByCTRL(NationalJetFuelPolicyDto policy)
        {
            var policiesCTRL = new List<NationalJetFuelPolicyDto>();

            try
            {
                policiesCTRL = Mapper.Map<List<NationalJetFuelPolicyDto>>(this.nationalPolicyRepository
                    .GetNationalPoliciesControl(new NationalJetFuelPolicy() { NationalPolicyID = policy.NationalPolicyID }));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return policiesCTRL;
        }

        /// <summary>
        /// Updates the national policies response - PolicyId and IDREG (SAP REGISTER)
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        public bool UpdateNationalPoliciesResponse(List<NationalJetFuelPolicyDto> policies)
        {
            var updatedResponse = false;

            if (policies == null)
            {
                return updatedResponse;
            }

            try
            {
                foreach (var item in policies)
                {
                    var entityPolicies = new NationalJetFuelPolicy();
                    entityPolicies = this.nationalPolicyRepository.FindNationalPolicySAP(new NationalJetFuelPolicy(item.NationalPolicyID, item.IDREG));
                    entityPolicies.BELNR = item.BELNR;
                    entityPolicies.XBLNR = item.XBLNR;
                    entityPolicies.MENV = item.MENV;
                    entityPolicies.MSGID = item.MSGID;
                    entityPolicies.RFCLOG = item.RFCLOG;
                    this.nationalPolicyRepository.Update(entityPolicies);
                }
                this.unitOfWork.Commit();
                updatedResponse = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return updatedResponse;
        }
    }
}
