//------------------------------------------------------------------------
// <copyright file="JetFuelLogErrorBusiness.cs" company="Volaris">
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
    /// Jet fuel log error operations
    /// </summary>
    public class JetFuelLogErrorBusiness : IJetFuelLogErrorBusiness
    {
        /// <summary>
        /// The log error
        /// </summary>
        private readonly IJetFuelLogErrorRepository logError;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly IJetFuelProcessRepository process;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorBusiness" /> class.
        /// </summary>
        /// <param name="logError">The log error.</param>
        /// <param name="process">The process.</param>
        public JetFuelLogErrorBusiness(IJetFuelLogErrorRepository logError, IJetFuelProcessRepository process)
        {
            this.logError = logError;
            this.process = process;
        }

        /// <summary>
        /// Gets the errors for period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// Errors of the period.
        /// </returns>
        public IList<JetFuelLogErrorDto> GetErrorsForPeriod(string periodCode)
        {
            if (periodCode == null)
            {
                return null;
            }

            List<JetFuelLogErrorDto> errorsDto = new List<JetFuelLogErrorDto>();
            List<JetFuelLogError> errors = new List<JetFuelLogError>();

            if (this.ValidateProcessOnExcecution(periodCode))
            {
                throw new BusinessException(Messages.CalculationProcessOnExecution, 24);
            }

            try
            {
                errors = this.logError.GetLogErrorByPeriodCode(periodCode).ToList();
                errorsDto = Mapper.Map<List<JetFuelLogError>, List<JetFuelLogErrorDto>>(errors);
                return errorsDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether the process is running.
        /// </summary>
        /// <returns><c>true</c> if the process is running otherwise <c>false</c>.</returns>
        private bool ValidateProcessOnExcecution(string periodCode)
        {
            try
            {
                JetFuelProcess fuelProcess = this.process.FindJetFuelProcess(periodCode);
                return fuelProcess.StatusProcessCode == "RUN";
            }
            catch(Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}
