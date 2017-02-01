//-----------------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoicePolicyBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

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
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// Operations for National Jet Fuel Invoice Policy Business
    /// </summary>
    /// <seealso cref="VOI.SISAC.Business.Process.INationalJetFuelInvoicePolicyBusiness" />
    public class NationalJetFuelInvoicePolicyBusiness : INationalJetFuelInvoicePolicyBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The national jet fuel invoice policy repository
        /// </summary>
        private readonly INationalJetFuelInvoicePolicyRepository invoicePolicyRepository;

        /// <summary>
        /// The national jet fuel invoice contol repository
        /// </summary>
        private readonly INationalJetFuelInvoiceControlRepository invoiceControlRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoicePolicyBusiness" /> class.
        /// </summary>
        /// <param name="invoicePolicyRepository">The invoice policy repository.</param>
        /// <param name="invoicePolicyRepository">The invoice control repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public NationalJetFuelInvoicePolicyBusiness(
            INationalJetFuelInvoicePolicyRepository invoicePolicyRepository,
            INationalJetFuelInvoiceControlRepository invoiceControlRepository,
            IUnitOfWork unitOfWork)
        {
            this.invoicePolicyRepository = invoicePolicyRepository;
            this.invoiceControlRepository = invoiceControlRepository;
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Gets the policies by remittance identifier.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<NationalJetFuelInvoicePolicyDto> GetPoliciesByRemittanceId(string remittanceId, string monthYear, string period)
        {
            if (string.IsNullOrWhiteSpace(remittanceId))
            {
                return null;
            }

            try
            {
                List<NationalJetFuelInvoicePolicy> policies = this.invoicePolicyRepository.GetInvoicePoliciesByRemittanceId(remittanceId, monthYear, period).ToList();
                List<NationalJetFuelInvoicePolicyDto> policiesDto = Mapper.Map<List<NationalJetFuelInvoicePolicyDto>>(policies);
                return policiesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }


        /// <summary>
        /// Updates the national invoice policies response - PolicyId and IDREG (SAP REGISTER)
        /// </summary>
        /// <param name="invoicePolicies">The policies.</param>
        /// <returns>
        ///   <c>True</c> if success otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateNationalInvoicePolicyResponse(List<NationalJetFuelInvoicePolicyDto> invoicePolicies)
        {
            if (invoicePolicies == null)
            {
                return false;
            }

            try
            {
                NationalJetFuelInvoicePolicy parameters = new NationalJetFuelInvoicePolicy();
                string remittanceID = string.Empty;
                string monthYear = string.Empty;
                string period = string.Empty;

                foreach (NationalJetFuelInvoicePolicyDto item in invoicePolicies)
                {
                    remittanceID = item.RemittanceId;
                    monthYear = item.MonthYear;
                    period = item.Period;

                    parameters.RemittanceId = remittanceID;
                    parameters.MonthYear = monthYear;
                    parameters.Period = period;
                    parameters.IDREG = item.IDREG;
                    
                    NationalJetFuelInvoicePolicy entity = this.invoicePolicyRepository.FindInvoicePolicySAP(parameters);
                    entity.BELNR = item.BELNR;
                    entity.XBLNR = item.XBLNR;
                    entity.MENV = item.MENV;
                    entity.MSGID = item.MSGID;
                    entity.RFCLOG = item.RFCLOG;
                    this.invoicePolicyRepository.Update(entity);
                }

                //Actualizar estatus en DocumentStatusCode a SENT
                NationalJetFuelInvoiceControl controlDto = new NationalJetFuelInvoiceControl { RemittanceID = remittanceID, MonthYear = monthYear , Period = period };
                controlDto = this.invoiceControlRepository.FindNationalJetFuelInvoiceControl(controlDto);

                if (controlDto != null)
                {
                    controlDto.DocumentStatusCode = "SENT";
                    controlDto.ProcessDate = DateTime.Now;
                    this.invoiceControlRepository.Update(controlDto);
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
