//------------------------------------------------------------------------
// <copyright file="JetFuelProcessBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using AutoMapper;
    using Dal.Repository.Process;
    using Entities.Enums;
    using Entities.Process;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// JetFuelProcess Business Class
    /// </summary>
    public class JetFuelProcessBusiness : IJetFuelProcessBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The LiabilityAccount repository
        /// </summary>
        private readonly IJetFuelProcessRepository jetFuelRepository;

        /// <summary>
        /// JetFuelProcessBusiness Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="jetFuelRepository"></param>
        public JetFuelProcessBusiness(IUnitOfWork unitOfWork, IJetFuelProcessRepository jetFuelRepository)
        {
            this.unitOfWork = unitOfWork;
            this.jetFuelRepository = jetFuelRepository;
        }

        /// <summary>
        /// Add JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcessDto"></param>
        /// <returns></returns>
        public bool AddJetFuelProcess(JetFuelProcessDto jetFuelProcessDto)
        {
            if (jetFuelProcessDto == null)
            {
                return false;
            }

            try
            {
                JetFuelProcess jetFuelEntity = new JetFuelProcess();

                jetFuelEntity = Mapper.Map<JetFuelProcess>(jetFuelProcessDto);
                this.jetFuelRepository.Add(jetFuelEntity);

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Delete JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcessDto"></param>
        /// <returns></returns>
        public bool DeleteJetFuelProcess(JetFuelProcessDto jetFuelProcessDto)
        {
            if (jetFuelProcessDto == null)
            {
                return false;
            }

            try
            {
                JetFuelProcess jetFuelEntitty = new JetFuelProcess();
                jetFuelEntitty = this.jetFuelRepository.FindJetFuelProcess(jetFuelProcessDto.PeriodCode);

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Start Jet Fuel Process
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        public bool StartJetFuelProcess(JetFuelProcessDto jetFuelProcess)
        {
            bool start = false;
            if (jetFuelProcess == null)
            {
                return start;
            }
            try
            {
                JetFuelProcess process = new JetFuelProcess();
                process = this.jetFuelRepository.FindJetFuelProcessNoTracking(new JetFuelProcess(jetFuelProcess.PeriodCode));

                if (process != null && !string.IsNullOrEmpty(process.PeriodCode))
                {
                    process.ProcessedByUserName = jetFuelProcess.ProcessedByUserName;
                    this.jetFuelRepository.Update(process);
                    this.unitOfWork.Commit();
                }
                if (jetFuelProcess.TypeProcess == Dto.Enums.TypeProcessDto.JetFuelProcessPending)
                {
                    process.TypeProcess = TypeProcess.JetFuelProcessPending;
                    start = this.jetFuelRepository.StartJetFuelProcess(process);
                }
                else
                {
                    process.TypeProcess = TypeProcess.JetFuelProcessAll;
                    start = this.jetFuelRepository.StartJetFuelProcess(process);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return start;
        }

        /// <summary>
        /// Find JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcessDto"></param>
        /// <returns></returns>
        public JetFuelProcessDto FindJetFuelProcess(JetFuelProcessDto jetFuelProcessDto)
        {
            if (jetFuelProcessDto == null && string.IsNullOrEmpty(jetFuelProcessDto.PeriodCode))
            {
                return new JetFuelProcessDto();
            }

            try
            {
                JetFuelProcess jetFuelProcessEntity = new JetFuelProcess();
                JetFuelProcessDto jetFuelProcessDB = new JetFuelProcessDto();

                jetFuelProcessEntity = this.jetFuelRepository.FindJetFuelProcess(jetFuelProcessDto.PeriodCode);
                jetFuelProcessDB = Mapper.Map<JetFuelProcessDto>(jetFuelProcessEntity);

                return jetFuelProcessDB;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get All JetFuelProcesses
        /// </summary>
        /// <returns></returns>
        public IList<JetFuelProcessDto> GetAllJetFuelProcesses()
        {
            try
            {
                IList<JetFuelProcessDto> jetFuelProcessesDto = new List<JetFuelProcessDto>();
                IList<JetFuelProcess> jetFuelProcessesEntity = new List<JetFuelProcess>();

                jetFuelProcessesEntity = this.jetFuelRepository.GetAll();
                jetFuelProcessesDto = Mapper.Map<IList<JetFuelProcessDto>>(jetFuelProcessesEntity);

                return jetFuelProcessesDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Update JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcessDto"></param>
        /// <returns></returns>
        public bool UpdateJetFuelProcess(JetFuelProcessDto jetFuelProcessDto)
        {
            if (jetFuelProcessDto == null)
            {
                return false;
            }

            try
            {
                JetFuelProcess jetFuelEntity = new JetFuelProcess();

                jetFuelEntity = Mapper.Map<JetFuelProcess>(jetFuelProcessDto);
                this.jetFuelRepository.Update(jetFuelEntity);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <returns></returns>
        public IList<JetFuelProcessDto> GetCurrentPeriod()
        {
            try
            {
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                IList<JetFuelProcessDto> currentPeriodDto = new List<JetFuelProcessDto>();
                IList<JetFuelProcess> currentPeriodEntity = new List<JetFuelProcess>();

                //NOV 01 (YEAR - 1)
                startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
                //FEB 28 (YEAR + 1)
                endDate = new DateTime((DateTime.Now.Year + 1), 2, DateTime.DaysInMonth(DateTime.Now.Day, 2));

                currentPeriodEntity = this.jetFuelRepository.GetCurrentPeriod(startDate, endDate);
                currentPeriodDto = Mapper.Map<IList<JetFuelProcessDto>>(currentPeriodEntity);

                return currentPeriodDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Closes the period.
        /// </summary>
        /// <param name="processDto">The process data transfer object.</param>
        /// <returns><c>true</c> if the operation was correct, otherwise <c>flase</c>.</returns>
        public bool ClosePeriod(JetFuelProcessDto processDto)
        {
            if (processDto == null || string.IsNullOrWhiteSpace(processDto.PeriodCode))
            {
                return false;
            }

            try
            {
                JetFuelProcess process = this.jetFuelRepository.FindJetFuelProcess(processDto.PeriodCode);
                process.ConfirmationStatusCode = "CLO";
                process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                process.ConfirmationDate = processDto.ConfirmationDate;
                this.jetFuelRepository.Update(process);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Opens the period.
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns>
        ///   <c>true</c> if the operation was correct, otherwise <c>flase</c>.
        /// </returns>
        public bool OpenPeriod(JetFuelProcessDto processDto)
        {
            if (processDto == null || string.IsNullOrWhiteSpace(processDto.PeriodCode))
            {
                return false;
            }

            try
            {
                JetFuelProcess process = this.jetFuelRepository.FindJetFuelProcess(processDto.PeriodCode);
                process.ConfirmationStatusCode = "OPE";
                process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                process.ConfirmationDate = processDto.ConfirmationDate;
                this.jetFuelRepository.Update(process);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether the process is running.
        /// </summary>
        /// <returns><c>true</c> if the process is running otherwise <c>false</c>.</returns>
        public bool IsProcessOnExcecution(string periodCode)
        {
            try
            {
                JetFuelProcess fuelProcess = this.jetFuelRepository.FindJetFuelProcess(periodCode);
                return fuelProcess.StatusProcessCode == "RUN";
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates if the period has erros.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        ///   <c>true</c> if the period has errors otherwise <c>false</c>.
        /// </returns>
        public bool ValidateIfPeriodHasErros(string periodCode)
        {
            try
            {
                int processErrors = this.jetFuelRepository.CountErrorsInPeriod(periodCode);
                return processErrors > 0;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns></returns>
        public bool RevertJetFuelProcess(JetFuelProcessDto processDto)
        {
            bool revert = false;
            if (processDto == null)
            {
                return false;
            }
            try
            {
                JetFuelProcess jetFuelEntity = new JetFuelProcess();
                jetFuelEntity = Mapper.Map<JetFuelProcess>(processDto);
                revert = this.jetFuelRepository.RevertJetFuelProcess(jetFuelEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return revert;
        }
    }
}
