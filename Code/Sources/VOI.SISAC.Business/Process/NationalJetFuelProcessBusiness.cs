//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Process;
    using Dto.Process;
    using Entities.Enums;
    using Entities.Process;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NationalJetFuelProcess Business Class
    /// </summary>
    public class NationalJetFuelProcessBusiness : INationalJetFuelProcessBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The LiabilityAccount repository
        /// </summary>
        private readonly INationalJetFuelProcessRepository nationalJetFuelRepository;

        /// <summary>
        /// NationalJetFuelProcessBusiness Constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="nationalJetFuelRepository">The national jet fuel repository.</param>
        public NationalJetFuelProcessBusiness(IUnitOfWork unitOfWork, INationalJetFuelProcessRepository nationalJetFuelRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nationalJetFuelRepository = nationalJetFuelRepository;
        }

        /// <summary>
        /// Add NationalJetFuelProcess
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns></returns>
        public bool AddNationalJetFuelProcess(NationalJetFuelProcessDto processDto)
        {
            var added = false;

            try
            {
                var jetFuelEntity = new NationalJetFuelProcess();
                jetFuelEntity = Mapper.Map<NationalJetFuelProcess>(processDto);
                this.nationalJetFuelRepository.Add(jetFuelEntity);
                added = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return added;
        }

        /// <summary>
        /// Delete NationalJetFuelProcess
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns></returns>
        public bool DeleteNationalJetFuelProcess(NationalJetFuelProcessDto processDto)
        {
            var deleted = false;

            if (processDto == null)
            {
                return deleted;
            }

            try
            {
                var jetFuelEntitty = new NationalJetFuelProcess();
                jetFuelEntitty = this.nationalJetFuelRepository.FindNationalJetFuelProcess(processDto.PeriodCode);
                deleted = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Start Jet Fuel Process
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns></returns>
        public bool StartNationalJetFuelProcess(NationalJetFuelProcessDto processDto)
        {
            var start = false;
            if (processDto == null)
            {
                return start;
            }
            try
            {
                NationalJetFuelProcess process = new NationalJetFuelProcess();
                process = this.nationalJetFuelRepository.FindNationalJetFuelProcessNoTracking(new NationalJetFuelProcess(processDto.PeriodCode));

                if (process != null && !string.IsNullOrEmpty(process.PeriodCode))
                {
                    process.ProcessedByUserName = processDto.ProcessedByUserName;
                    this.nationalJetFuelRepository.Update(process);
                    this.unitOfWork.Commit();
                }
                if (processDto.TypeProcess == Dto.Enums.NationalTypeProcessDto.NationalJetFuelProcessPending)
                {
                    process.TypeProcess = NationalTypeProcess.NationalJetFuelProcessPending;
                    start = this.nationalJetFuelRepository.StartNationalJetFuelProcess(process);
                }
                else
                {
                    process.TypeProcess = NationalTypeProcess.NationalJetFuelProcessAll;
                    start = this.nationalJetFuelRepository.StartNationalJetFuelProcess(process);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return start;
        }

        /// <summary>
        /// Find NationalJetFuelProcess
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public NationalJetFuelProcessDto FindNationalJetFuelProcess(NationalJetFuelProcessDto process)
        {
            if (process == null && string.IsNullOrEmpty(process.PeriodCode))
            {
                return new NationalJetFuelProcessDto();
            }

            try
            {
                NationalJetFuelProcess jetFuelProcessEntity = new NationalJetFuelProcess();
                NationalJetFuelProcessDto jetFuelProcessDB = new NationalJetFuelProcessDto();

                jetFuelProcessEntity = this.nationalJetFuelRepository.FindNationalJetFuelProcess(process.PeriodCode);
                jetFuelProcessDB = Mapper.Map<NationalJetFuelProcessDto>(jetFuelProcessEntity);

                return jetFuelProcessDB;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get All NationalJetFuelProcesses
        /// </summary>
        /// <returns></returns>
        public IList<NationalJetFuelProcessDto> GetAllNationalJetFuelProcesses()
        {
            try
            {
                IList<NationalJetFuelProcessDto> jetFuelProcessesDto = new List<NationalJetFuelProcessDto>();
                IList<NationalJetFuelProcess> jetFuelProcessesEntity = new List<NationalJetFuelProcess>();

                jetFuelProcessesEntity = this.nationalJetFuelRepository.GetAll();
                jetFuelProcessesDto = Mapper.Map<IList<NationalJetFuelProcessDto>>(jetFuelProcessesEntity);

                return jetFuelProcessesDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Update NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcessDto"></param>
        /// <returns></returns>
        public bool UpdateNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcessDto)
        {
            var updated = false;

            if (jetFuelProcessDto == null)
            {
                return updated;
            }

            try
            {
                NationalJetFuelProcess jetFuelEntity = new NationalJetFuelProcess();

                jetFuelEntity = Mapper.Map<NationalJetFuelProcess>(jetFuelProcessDto);
                this.nationalJetFuelRepository.Update(jetFuelEntity);
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
        /// Get Current Period
        /// </summary>
        /// <returns></returns>
        public IList<NationalJetFuelProcessDto> GetCurrentPeriod()
        {
            try
            {
                var startDate = new DateTime();
                var endDate = new DateTime();
                IList<NationalJetFuelProcessDto> currentPeriodDto = new List<NationalJetFuelProcessDto>();
                IList<NationalJetFuelProcess> currentPeriodEntity = new List<NationalJetFuelProcess>();

                //NOV 01 (YEAR - 1)
                startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
                //FEB 28 (YEAR + 1)
                endDate = new DateTime((DateTime.Now.Year + 1), 2, DateTime.DaysInMonth(DateTime.Now.Day, 2));

                currentPeriodEntity = this.nationalJetFuelRepository.GetCurrentPeriod(startDate, endDate);
                currentPeriodDto = Mapper.Map<IList<NationalJetFuelProcessDto>>(currentPeriodEntity);

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
        public bool ClosePeriod(NationalJetFuelProcessDto processDto)
        {
            var closed = false;

            if (processDto == null || string.IsNullOrWhiteSpace(processDto.PeriodCode))
            {
                return closed;
            }

            try
            {
                NationalJetFuelProcess process = this.nationalJetFuelRepository.FindNationalJetFuelProcess(processDto.PeriodCode);
                process.ConfirmationStatusCode = "CLO";
                process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                process.ConfirmationDate = processDto.ConfirmationDate;
                this.nationalJetFuelRepository.Update(process);
                this.unitOfWork.Commit();
                closed = true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
            return closed;
        }

        /// <summary>
        /// Opens the period.
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns>
        ///   <c>true</c> if the operation was correct, otherwise <c>flase</c>.
        /// </returns>
        public bool OpenPeriod(NationalJetFuelProcessDto processDto)
        {
            var open = false;

            if (processDto == null || string.IsNullOrWhiteSpace(processDto.PeriodCode))
            {
                return open;
            }

            try
            {
                NationalJetFuelProcess process = this.nationalJetFuelRepository.FindNationalJetFuelProcess(processDto.PeriodCode);
                process.ConfirmationStatusCode = "OPE";
                process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                process.ConfirmationDate = processDto.ConfirmationDate;
                this.nationalJetFuelRepository.Update(process);
                this.unitOfWork.Commit();
                open = true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
            return open;
        }

        /// <summary>
        /// Determines whether the process is running.
        /// </summary>
        /// <returns><c>true</c> if the process is running otherwise <c>false</c>.</returns>
        public bool IsProcessOnExcecution(string periodCode)
        {
            try
            {
                NationalJetFuelProcess fuelProcess = this.nationalJetFuelRepository.FindNationalJetFuelProcess(periodCode);
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
                int processErrors = this.nationalJetFuelRepository.CountErrorsInPeriod(periodCode);
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
        public bool RevertNationalJetFuelProcess(NationalJetFuelProcessDto processDto)
        {
            bool revert = false;
            if (processDto == null)
            {
                return false;
            }
            try
            {
                NationalJetFuelProcess jetFuelEntity = new NationalJetFuelProcess();
                jetFuelEntity = Mapper.Map<NationalJetFuelProcess>(processDto);
                revert = this.nationalJetFuelRepository.RevertNationalJetFuelProcess(jetFuelEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return revert;
        }
    }
}
