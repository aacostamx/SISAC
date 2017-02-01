//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyControlBusiness.cs" company="Volaris">
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
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Jet Fuel Policy Control Business
    /// </summary>
    public class JetFuelPolicyControlBusiness : IJetFuelPolicyControlBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly IJetFuelPolicyControlRepository policyControlRepository;

        /// <summary>
        /// policyRepository
        /// </summary>
        private readonly IJetFuelPolicyRepository policyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelPolicyControlBusiness"/> class.
        /// </summary>
        /// <param name="policyControlRepository">The policy control repository.</param>
        /// <param name="policyRepository">The policy repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public JetFuelPolicyControlBusiness(IJetFuelPolicyControlRepository policyControlRepository, IJetFuelPolicyRepository policyRepository, IUnitOfWork unitOfWork)
        {
            this.policyControlRepository = policyControlRepository;
            this.policyRepository = policyRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds by identifier.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns>
        /// The policy.
        /// </returns>
        public JetFuelPolicyControlDto FindById(long policyId)
        {
            try
            {
                JetFuelPolicyControl policy = this.policyControlRepository.FindById(policyId);
                JetFuelPolicyControlDto policyDto = new JetFuelPolicyControlDto();
                policyDto = Mapper.Map<JetFuelPolicyControl, JetFuelPolicyControlDto>(policy);
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
        /// <param name="policyControl">The control policy.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Create(JetFuelPolicyControlDto policyControl)
        {
            if (policyControl == null)
            {
                return false;
            }

            try
            {
                JetFuelPolicyControl entity = new JetFuelPolicyControl();
                entity = Mapper.Map<JetFuelPolicyControl>(policyControl);
                this.policyControlRepository.Add(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the specified policy identifier.
        /// </summary>
        /// <param name="policyControlId">The policy control identifier.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Delete(long policyControlId)
        {
            try
            {
                JetFuelPolicyControl policy = this.policyControlRepository.FindById(policyControlId);

                if (policy != null)
                {
                    // Para borrado lógico
                    this.policyControlRepository.Delete(policy);
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
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the specified policy control.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        public bool Update(JetFuelPolicyControlDto policyControl)
        {
            if (policyControl == null)
            {
                return false;
            }

            try
            {
                JetFuelPolicyControl entity = this.policyControlRepository.FindById(policyControl.PolicyId);
                entity.AirlineCode = policyControl.AirlineCode;
                entity.AirportCodes = policyControl.AirportCodes;
                entity.ConfirmedByUserName = policyControl.ConfirmedByUserName;
                entity.CreationDate = policyControl.CreationDate;
                entity.DateBaseline = policyControl.DateBaseline;
                entity.DatePosting = policyControl.DatePosting;
                entity.DateValue = policyControl.DateValue;
                entity.EndDateComplementary = policyControl.EndDateComplementary;
                entity.EndDateReal = policyControl.EndDateReal;
                entity.HeaderText = policyControl.HeaderText;
                entity.ItemText = policyControl.ItemText;
                entity.ProviderCodes = policyControl.ProviderCodes;
                entity.SendByUserName = policyControl.SendByUserName;
                entity.ServiceCodes = policyControl.ServiceCodes;
                entity.StartDateComplementary = policyControl.StartDateComplementary;
                entity.StartDateReal = policyControl.StartDateReal;
                entity.Status = policyControl.Status;

                this.policyControlRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// List of <see cref="JetFuelPolicyControlDto" />.
        /// </returns>
        public IList<JetFuelPolicyControlDto> GetAll()
        {
            try
            {
                IList<JetFuelPolicyControl> entity = this.policyControlRepository.GetAll();
                IList<JetFuelPolicyControlDto> policy = new List<JetFuelPolicyControlDto>();
                policy = Mapper.Map<IList<JetFuelPolicyControl>, IList<JetFuelPolicyControlDto>>(entity);
                return policy;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Creates the specified control policy.
        /// </summary>
        /// <param name="policyControl">The control policy.</param>
        /// <returns>
        /// The policy identifier asigned. If the number is negative, the insertion faild.
        /// </returns>
        public long CreatePolicy(JetFuelPolicyControlDto policyControl)
        {
            if (policyControl == null)
            {
                return -1;
            }

            try
            {
                JetFuelPolicyControl entity = new JetFuelPolicyControl();
                entity = Mapper.Map<JetFuelPolicyControl>(policyControl);
                return this.policyControlRepository.CreatePolicyParameters(entity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Counts the policies by year.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int CountPoliciesByYear()
        {
            int count = 0;

            try
            {
                count = this.policyControlRepository.CountPoliciesByYear();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return count;
        }

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="Pagesize">The pagesize.</param>
        /// <param name="Pagenumber">The pagenumber.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<JetFuelPolicyControlDto> GetPagedPoliciesHistory(int Pagesize, int Pagenumber)
        {
            var policiesDto = new List<JetFuelPolicyControlDto>();

            try
            {
                policiesDto = Mapper.Map<List<JetFuelPolicyControlDto>>(this.policyControlRepository.GetPagedPoliciesHistory(Pagesize, Pagenumber));

                CountPoliciesProcessed(policiesDto);

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return policiesDto;
        }

        /// <summary>
        /// Counts the policies processed.
        /// </summary>
        /// <param name="policiesDto">The policies dto.</param>
        private void CountPoliciesProcessed(List<JetFuelPolicyControlDto> policiesDto)
        {
            //Status Exitoso
            string sucess = "S";
            string unsucessful = "E";
            //variables para almacenar documentos (exitosos y no exitosos)
            List<int> documentNumberSucess = new List<int>();
            List<int> documentNumberUnsucessful = new List<int>();

            //update Documentos exitosos, no exitosos y totales
            foreach (var item in policiesDto)
            {
                //llamar a policy por Id
                IList<JetFuelPolicy> entity = this.policyRepository.GetPoliciesByPolicyControlId(item.PolicyId);
                IList<JetFuelPolicyDto> policy = new List<JetFuelPolicyDto>();
                policy = Mapper.Map<IList<JetFuelPolicy>, IList<JetFuelPolicyDto>>(entity);

                //Documentos exitosos
                documentNumberSucess = policy.Where(c => c.MENV == sucess).Select(c => c.DocumentNumber).ToList();
                documentNumberUnsucessful = policy.Where(c => c.MENV == unsucessful).Select(c => c.DocumentNumber).ToList();
                //Lista Exitosos y no Exitosos
                var policySucess = policy.Where(c => documentNumberSucess.Contains(c.DocumentNumber)).ToList();
                var policyUnsucessful = policy.Where(c => documentNumberUnsucessful.Contains(c.DocumentNumber)).ToList();

                //Documentos exitosos, no exitosos y totales
                item.TotalSucess = documentNumberSucess.Distinct().Count();
                item.TotalErrors = documentNumberUnsucessful.Distinct().Count();
                item.TotalProcess = documentNumberSucess.Distinct().Count() + documentNumberUnsucessful.Distinct().Count();
            }
        }

        /// <summary>
        /// Counts all policies seach.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int CountAllPoliciesSeach(JetFuelPoliciesHistoryDto search)
        {
            var count = 0;

            try
            {
                var entities = this.policyControlRepository.GetPagedPoliciesHistorySearch();
                entities = SearchParams(search, entities);
                count = entities.Count();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return count;
        }

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <param name="search">The policies.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<JetFuelPolicyControlDto> GetPagedPoliciesHistorySearch(JetFuelPoliciesHistoryDto search)
        {
            var skip = (search.Pagenumber - 1) * search.Pagesize;
            var policiesDto = new List<JetFuelPolicyControlDto>();

            try
            {
                var entities = this.policyControlRepository.GetPagedPoliciesHistorySearch();
                entities = SearchParams(search, entities);
                entities = entities.OrderByDescending(c => c.CreationDate)
                        .Skip(skip)
                        .Take(search.Pagesize)
                        .ToList();
                policiesDto = Mapper.Map<List<JetFuelPolicyControlDto>>(entities);
                CountPoliciesProcessed(policiesDto);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return policiesDto;
        }

        /// <summary>
        /// Validates the type change for currency.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>List of Currencies that mmiss its change type.</returns>
        public IList<string> ValidateTypeChangeForCurrency(JetFuelPolicyControlDto policyControl)
        {
            if (policyControl == null)
            {
                return null;
            }

            try
            {
                List<string> currenciesNotFound = new List<string>();
                JetFuelPolicyControl entity = new JetFuelPolicyControl();
                entity = Mapper.Map<JetFuelPolicyControl>(policyControl);
                List<CurrencyTypeChage> currencies = new List<CurrencyTypeChage>();
                currencies = this.policyControlRepository.VerifyPolicy(entity).ToList();

                if (currencies == null || currencies.Count == 0)
                {
                    currenciesNotFound.Add("No existing data for the information given.");
                    return currenciesNotFound;
                }

                currenciesNotFound.AddRange(currencies.Where(c => c.Verify == 0).Select(c => c.CurrencyCode));
                return currenciesNotFound;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Searches the parameters.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        private IList<JetFuelPolicyControl> SearchParams(JetFuelPoliciesHistoryDto search, IList<JetFuelPolicyControl> entities)
        {
            if (search.StartCreationDate != null && search.StartCreationDate != DateTime.MinValue && search.EndCreationDate != null && search.EndCreationDate != DateTime.MinValue)
            {
                if (search.StartCreationDate.Date == search.EndCreationDate.Date)
                {
                    entities = entities.Where(c => c.CreationDate.Date == search.StartCreationDate.Date).ToList();
                }
                else
                {
                    entities = entities.Where(c => c.CreationDate.Date >= search.StartCreationDate.Date
                        && c.CreationDate.Date <= search.EndCreationDate.Date).ToList();
                }
            }
            if (search.PolicyId > 0)
            {
                entities = entities.Where(c => c.PolicyId.ToString().Contains(search.PolicyId.ToString())).ToList();
            }
            else if (search.BeginPolicyId > 0 && search.EndPolicyId > 0)
            {
                entities = entities.Where(c => c.PolicyId >= search.BeginPolicyId
                        && c.PolicyId <= search.EndPolicyId).ToList();
            }

            return entities;
        }

        /// <summary>
        /// Cancels the jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool CancelJetFuelPolicy(long policyID)
        {
            var canceled = false;

            try
            {
                canceled = this.policyControlRepository.CancelJetFuelPolicy(policyID);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return canceled;
        }
    }
}
