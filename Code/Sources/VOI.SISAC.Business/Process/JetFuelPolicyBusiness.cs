//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Process;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Jet Fuel Policy Business
    /// </summary>
    public class JetFuelPolicyBusiness : IJetFuelPolicyBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly IJetFuelPolicyRepository policyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelPolicyBusiness" /> class.
        /// </summary>
        /// <param name="policyRepository">The policy repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public JetFuelPolicyBusiness(IJetFuelPolicyRepository policyRepository, IUnitOfWork unitOfWork)
        {
            this.policyRepository = policyRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="policyResultId">The policy result identifier.</param>
        /// <returns>
        /// The policies.
        /// </returns>
        public JetFuelPolicyDto FindById(long policyResultId)
        {
            try
            {
                JetFuelPolicy policy = this.policyRepository.FindById(policyResultId);
                JetFuelPolicyDto policyDto = new JetFuelPolicyDto();
                policyDto = Mapper.Map<JetFuelPolicy, JetFuelPolicyDto>(policy);

                return policyDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Creates the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Create(JetFuelPolicyDto policy)
        {
            if (policy == null)
            {
                return false;
            }

            try
            {
                JetFuelPolicy entity = new JetFuelPolicy();
                entity = Mapper.Map<JetFuelPolicy>(policy);
                this.policyRepository.Add(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the specified policy.
        /// </summary>
        /// <param name="policyResultId">The policy identifier.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Delete(long policyResultId)
        {
            try
            {
                JetFuelPolicy policy = this.policyRepository.FindById(policyResultId);

                if (policy != null)
                {
                    // Para borrado lógico
                    this.policyRepository.Delete(policy);
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
        /// Updates the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Update(JetFuelPolicyDto policy)
        {
            if (policy == null)
            {
                return false;
            }

            try
            {
                JetFuelPolicy entity = this.policyRepository.FindById(policy.PolicyResultId);
                entity.DocumentNumber = policy.DocumentNumber;
                entity.JetFuelTicketID = policy.JetFuelTicketID;
                this.policyRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// List of <see cref="JetFuelPolicyDto" />.
        /// </returns>
        public IList<JetFuelPolicyDto> GetAll()
        {
            try
            {
                IList<JetFuelPolicy> entity = this.policyRepository.GetAll();
                IList<JetFuelPolicyDto> policy = new List<JetFuelPolicyDto>();
                policy = Mapper.Map<IList<JetFuelPolicy>, IList<JetFuelPolicyDto>>(entity);
                return policy;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets Unsuccessful
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// List of <see cref="JetFuelPolicyDto" />.
        /// </returns>
        public IList<JetFuelPolicyDto> GetUnsuccessful(long id)
        {
            try
            {
                string sucess = "S";
                List<int> documentNumberList = new List<int>();
                IList<JetFuelPolicy> entity = this.policyRepository.GetPoliciesByPolicyControlId(id);
                IList<JetFuelPolicyDto> policy = new List<JetFuelPolicyDto>();
                policy = Mapper.Map<IList<JetFuelPolicy>, IList<JetFuelPolicyDto>>(entity);

                //Filtrar documentos no exitosos
                documentNumberList = policy.Where(c => c.MENV == sucess).Select(c => c.DocumentNumber).ToList();

                var policySucess = policy.Where(c => documentNumberList.Contains(c.DocumentNumber)).ToList();
                policy = policy.Except(policySucess).ToList();

                return policy;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the policies by policy control identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of <see cref="JetFuelPolicyDto"/>.</returns>
        public IList<JetFuelPolicyDto> GetPoliciesByPolicyControlId(long id)
        {
            try
            {
                IList<JetFuelPolicy> entity = this.policyRepository.GetPoliciesByPolicyControlId(id);
                IList<JetFuelPolicyDto> policy = new List<JetFuelPolicyDto>();
                policy = Mapper.Map<IList<JetFuelPolicy>, IList<JetFuelPolicyDto>>(entity);
                return policy;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the policies data from the service response.
        /// </summary>
        /// <param name="policy">The policies.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool UpdatePoliciesFromResponse(List<JetFuelPolicyDto> policies)
        {
            if (policies == null)
            {
                return false;
            }

            try
            {
                foreach (JetFuelPolicyDto item in policies)
                {
                    JetFuelPolicy entity = this.policyRepository.FindByPolicyId(item.PolicyId, item.IDREG);
                    entity.BELNR = item.BELNR;
                    entity.XBLNR = item.XBLNR;
                    entity.MENV = item.MENV;
                    entity.MSGID = item.MSGID;
                    entity.RFCLOG = item.RFCLOG;
                    this.policyRepository.Update(entity);
                }
                
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }
    }
}
