//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyControlBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Process;
    using Dto.Process;
    using Entities.Finance;
    using Entities.Process;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Jet Fuel Policy Control Business
    /// </summary>
    public class NationalJetFuelPolicyControlBusiness : INationalJetFuelPolicyControlBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly INationalJetFuelPolicyControlRepository NationalPolicyCTRLRepository;

        /// <summary>
        /// policyRepository
        /// </summary>
        private readonly INationalJetFuelPolicyRepository NationalPolicyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyControlBusiness"/> class.
        /// </summary>
        /// <param name="NationalPolicyCTRLRepository">The national policy control repository.</param>
        /// <param name="NationalPolicyRepository">The national policy repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public NationalJetFuelPolicyControlBusiness(INationalJetFuelPolicyControlRepository NationalPolicyCTRLRepository,
            INationalJetFuelPolicyRepository NationalPolicyRepository, IUnitOfWork unitOfWork)
        {
            this.NationalPolicyCTRLRepository = NationalPolicyCTRLRepository;
            this.NationalPolicyRepository = NationalPolicyRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public NationalJetFuelPolicyControlDto FindNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var policyDto = new NationalJetFuelPolicyControlDto();

            try
            {
                policyDto = Mapper.Map<NationalJetFuelPolicyControlDto>
                    (this.NationalPolicyCTRLRepository.FindNationalPolicyControl(new NationalJetFuelPolicyControl(policyCTRL.NationalPolicyID)));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }

            return policyDto;
        }

        /// <summary>
        /// Creates the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool CreateNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var created = false;

            if (policyCTRL == null)
            {
                return created;
            }

            try
            {
                this.NationalPolicyCTRLRepository.Add(Mapper.Map<NationalJetFuelPolicyControl>(policyCTRL));
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
        /// Deletes the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var deleted = false;
            var policy = new NationalJetFuelPolicyControl();

            try
            {
                policy = this.NationalPolicyCTRLRepository.FindNationalPolicyControl(new NationalJetFuelPolicyControl(policyCTRL.NationalPolicyID));

                if (policy != null && policy.NationalPolicyID > 0)
                {
                    this.NationalPolicyCTRLRepository.Delete(policy);
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
        /// Updates the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var updated = false;
            var policy = new NationalJetFuelPolicyControl();

            if (policyCTRL == null)
            {
                return updated;
            }

            try
            {
                policy = this.NationalPolicyCTRLRepository.FindNationalPolicyControl(new NationalJetFuelPolicyControl(policyCTRL.NationalPolicyID));
                policy.AirlineCode = policyCTRL.AirlineCode;
                policy.AirportCodes = policyCTRL.AirportCodes;
                policy.ConfirmedByUserName = policyCTRL.ConfirmedByUserName;
                policy.CreationDate = policyCTRL.CreationDate;
                policy.DateBaseline = policyCTRL.DateBaseline;
                policy.DatePosting = policyCTRL.DatePosting;
                policy.DateValue = policyCTRL.DateValue;
                policy.EndDateComplementary = policyCTRL.EndDateComplementary;
                policy.EndDateReal = policyCTRL.EndDateReal;
                policy.HeaderText = policyCTRL.HeaderText;
                policy.ItemText = policyCTRL.ItemText;
                policy.ProviderCodes = policyCTRL.ProviderCodes;
                policy.SendByUserName = policyCTRL.SendByUserName;
                policy.ServiceCodes = policyCTRL.ServiceCodes;
                policy.StartDateComplementary = policyCTRL.StartDateComplementary;
                policy.StartDateReal = policyCTRL.StartDateReal;
                policy.Status = policyCTRL.Status;
                this.NationalPolicyCTRLRepository.Update(policy);
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
        /// Gets all national policies control.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyControlDto> GetAllNationalPoliciesCTRL()
        {
            var policiesDto = new List<NationalJetFuelPolicyControlDto>();

            try
            {
                policiesDto = Mapper.Map<List<NationalJetFuelPolicyControlDto>>(this.NationalPolicyCTRLRepository.GetAll());
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return policiesDto;
        }

        /// <summary>
        /// Creates the national policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public long CreateNationalPolicy(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var createdPolicy = -1L;

            if (policyCTRL == null)
            {
                return createdPolicy;
            }

            try
            {
                createdPolicy = this.NationalPolicyCTRLRepository.CreatePolicyParams(Mapper.Map<NationalJetFuelPolicyControl>(policyCTRL));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return createdPolicy;
        }

        /// <summary>
        /// Validates the type change for currency.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<string> ValidateTypeChangeForCurrency(NationalJetFuelPolicyControlDto policyCTRL)
        {
            var typeChange = new List<string>();
            var currencies = new List<CurrencyTypeChage>();

            if (policyCTRL == null)
            {
                return typeChange;
            }

            try
            {
                currencies = this.NationalPolicyCTRLRepository.CheckPolicyCurrencies(Mapper.Map<NationalJetFuelPolicyControl>(policyCTRL)).ToList();
                if (currencies == null || currencies.Count == 0)
                {
                    typeChange.AddRange(currencies.Where(c => c.Verify == 0).Select(d => d.CurrencyCode));
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return typeChange;
        }

        /// <summary>
        /// Cancels the national jet fuel policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        public bool CancelNationalJetFuelPolicy(NationalJetFuelPolicyControlDto policyCTRL)
        {
            bool canceled = false;

            try
            {
                canceled = this.NationalPolicyCTRLRepository.CancelNationalJetFuelPolicy(policyCTRL.NationalPolicyID);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return canceled;
        }

        #region Policies History Methods
        /// <summary>
        /// Counts all national jet fuel policies records.
        /// </summary>
        /// <returns>
        /// The number of  national jet fuel policies.
        /// </returns>
        public int CountPoliciesByYear()
        {
            int count = 0;

            try
            {
                count = this.NationalPolicyCTRLRepository.CountPoliciesByYear();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return count;
        }

        /// <summary>
        /// Counts all policies seach.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The number of national jet fuel policies by parameters.
        /// </returns>
        public int CountAllPoliciesSeach(NationalJetFuelPolicyHistoryDto parameters)
        {
            var count = 0;

            try
            {
                var entities = this.NationalPolicyCTRLRepository.GetPagedPoliciesHistorySearch();
                entities = SearchParams(parameters, entities);
                count = entities.Count();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return count;
        }

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national jet fuel policies paginated.
        /// </returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyControlDto> GetPagedPoliciesHistory(int pageSize, int pageNumber)
        {
            List<NationalJetFuelPolicyControlDto> policiesDto = new List<NationalJetFuelPolicyControlDto>();

            try
            {
                policiesDto = Mapper.Map<List<NationalJetFuelPolicyControlDto>>(this.NationalPolicyCTRLRepository.GetPagedPoliciesHistory(pageSize, pageNumber));
                policiesDto = this.CountPoliciesProcessed(policiesDto);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return policiesDto;
        }

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelPolicyControlDto> GetPagedPoliciesHistorySearch(NationalJetFuelPolicyHistoryDto parameters)
        {
            int skip = (parameters.Pagenumber - 1) * parameters.Pagesize;
            List<NationalJetFuelPolicyControlDto> policiesDto = new List<NationalJetFuelPolicyControlDto>();

            try
            {
                var entities = this.NationalPolicyCTRLRepository.GetPagedPoliciesHistorySearch();
                entities = SearchParams(parameters, entities);
                entities = entities.OrderByDescending(c => c.CreationDate)
                        .Skip(skip)
                        .Take(parameters.Pagesize)
                        .ToList();
                policiesDto = Mapper.Map<List<NationalJetFuelPolicyControlDto>>(entities);
                policiesDto = this.CountPoliciesProcessed(policiesDto);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return policiesDto;
        }

        /// <summary>
        /// Searches the parameters.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="entities">The entities.</param>
        /// <returns>List of national jet fuel policy control filtered.</returns>
        private IList<NationalJetFuelPolicyControl> SearchParams(NationalJetFuelPolicyHistoryDto search, IList<NationalJetFuelPolicyControl> entities)
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

            if (search.NationalPolicyId > 0)
            {
                entities = entities.Where(c => c.NationalPolicyID.ToString().Contains(search.NationalPolicyId.ToString())).ToList();
            }
            else if (search.BeginPolicyId > 0 && search.EndPolicyId > 0)
            {
                entities = entities.Where(c => c.NationalPolicyID >= search.BeginPolicyId
                        && c.NationalPolicyID <= search.EndPolicyId).ToList();
            }

            return entities;
        }

        /// <summary>
        /// Counts the policies processed.
        /// </summary>
        /// <param name="policiesDto">The policies dto.</param>
        private List<NationalJetFuelPolicyControlDto> CountPoliciesProcessed(List<NationalJetFuelPolicyControlDto> policiesDto)
        {
            // Types of status
            string sucess = "S";
            string unsucessful = "E";

            // Variables for stored succesful or unsuccessful documents
            List<int> documentNumberSucess = new List<int>();
            List<int> documentNumberUnsucessful = new List<int>();

            // Finds successful or unsuccessful documents
            foreach (var item in policiesDto)
            {
                // Gets the policies for thier ID
                NationalJetFuelPolicy policy = new NationalJetFuelPolicy() { NationalPolicyID = item.NationalPolicyID };
                IList<NationalJetFuelPolicy> entity = this.NationalPolicyRepository.GetNationalPoliciesControl(policy);

                // Successful documents
                documentNumberSucess = entity.Where(c => c.MENV == sucess).Select(c => c.DocumentNumber).ToList();

                // Unsuccessful documents
                documentNumberUnsucessful = entity.Where(c => c.MENV == unsucessful).Select(c => c.DocumentNumber).ToList();

                // Counts the documents
                item.TotalSucess = documentNumberSucess.Distinct().Count();
                item.TotalErrors = documentNumberUnsucessful.Distinct().Count();
                item.TotalProcess = documentNumberSucess.Distinct().Count() + documentNumberUnsucessful.Distinct().Count();
            }

            return policiesDto;
        }
        #endregion
    }
}
