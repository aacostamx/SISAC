//------------------------------------------------------------------------
// <copyright file="JetFuelProvisionBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Dal.Repository.Process;
    using VOI.SISAC.Entities.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;

    /// <summary>
    /// Jet fuel provission operations
    /// </summary>
    public class JetFuelProvisionBusiness : IJetFuelProvisionBusiness
    {
        /// <summary>
        /// The log error
        /// </summary>
        private readonly IJetFuelProvisionRepository provissionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelProvisionBusiness"/> class.
        /// </summary>
        /// <param name="provissionRepository">The provission repository.</param>
        public JetFuelProvisionBusiness(IJetFuelProvisionRepository provissionRepository)
        {
            this.provissionRepository = provissionRepository;
        }

        /// <summary>
        /// Advances the search.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// A list of provision depending on the parameters given.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<JetFuelProvisionDto> AdvanceSearch(JetFuelCalculationResultParametersDto parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            try
            {
                IList<JetFuelProvision> provisions = new List<JetFuelProvision>();
                if (parameters.IsOpenPeriod)
                {
                    provisions = this.provissionRepository.GetProvisionsByPeriod(
                        parameters.StartDate,
                        parameters.EndDate);
                }
                else 
                {
                    provisions = this.provissionRepository.GetProvisionsByPeriod(parameters.PeriodCode);
                }
                
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// Gets the group report.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<JetFuelProvisionDto> GetGroupReport()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the detailed report.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<JetFuelProvisionDto> GetDetailedReport()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the policy.
        /// </summary>
        /// <returns></returns>
        public IList<JetFuelProvisionDto> CreatePolicy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provisionses the by query.
        /// </summary>
        /// <param name="provisions">The provisions.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private List<JetFuelProvision> ProvisionsByQuery(List<JetFuelProvision> provisions, JetFuelCalculationResultParametersDto parameters)
        {
            if (parameters.AirlineCode == null)
            {
                parameters.AirlineCode = new List<string>();
            }

            if (parameters.StationCode != null)
            {
                parameters.StationCode = new List<string>();
            }

            if (parameters.ServiceCode != null)
            {
                parameters.ServiceCode = new List<string>();
            }

            if (parameters.ProviderNumber != null)
            {
                parameters.ProviderNumber = new List<string>();
            }

            provisions.Where(c=>
                parameters.AirlineCode.Contains(c.AirlineCode)
                && parameters.ServiceCode.Contains(c.ServiceCode)
                && parameters.ProviderNumber.Contains(c.ProviderNumber));

            return null;
        }
    }
}
