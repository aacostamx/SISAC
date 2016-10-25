//------------------------------------------------------------------------
// <copyright file="NationalJetFuelLogErrorBusiness.cs" company="Volaris">
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
    /// National Jet fuel log error operations
    /// </summary>
    public class NationalJetFuelLogErrorBusiness : INationalJetFuelLogErrorBusiness
    {
        /// <summary>
        /// The log error
        /// </summary>
        private readonly INationalJetFuelLogErrorRepository logError;

        /// <summary>
        /// The log error
        /// </summary>
        private readonly INationalJetFuelProcessRepository process;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorBusiness" /> class.
        /// </summary>
        /// <param name="logError">The log error.</param>
        /// <param name="process">The process.</param>
        public NationalJetFuelLogErrorBusiness(INationalJetFuelLogErrorRepository logError, INationalJetFuelProcessRepository process)
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
        public IList<NationalJetFuelLogErrorDto> GetErrorsForPeriod(string periodCode)
        {
            if (periodCode == null)
            {
                return null;
            }

            List<NationalJetFuelLogErrorDto> errorsDto = new List<NationalJetFuelLogErrorDto>();
            List<NationalJetFuelLogError> errors = new List<NationalJetFuelLogError>();

            if (this.ValidateProcessOnExcecution(periodCode))
            {
                throw new BusinessException(Messages.CalculationProcessOnExecution, 24);
            }

            try
            {
                errors = this.logError.GetLogErrorByPeriodCode(periodCode).ToList();
                errorsDto = Mapper.Map<List<NationalJetFuelLogError>, List<NationalJetFuelLogErrorDto>>(errors);
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
                NationalJetFuelProcess fuelProcess = this.process.FindNationalJetFuelProcess(periodCode);
                return fuelProcess.StatusProcessCode == "RUN";
            }
            catch(Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}
