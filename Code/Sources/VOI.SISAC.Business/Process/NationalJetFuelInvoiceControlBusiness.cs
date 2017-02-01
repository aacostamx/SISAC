//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceControlBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using AutoMapper;
    using Dto.Enums;
    using Entities.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Process;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// National Jet Fuel Invoice Control Business
    /// </summary>
    public class NationalJetFuelInvoiceControlBusiness : INationalJetFuelInvoiceControlBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The invoice control repository
        /// </summary>
        private readonly INationalJetFuelInvoiceControlRepository invoiceControlRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControlBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="invoiceControlRepository">The invoice control repository.</param>
        public NationalJetFuelInvoiceControlBusiness(IUnitOfWork unitOfWork, INationalJetFuelInvoiceControlRepository invoiceControlRepository)
        {
            this.unitOfWork = unitOfWork;
            this.invoiceControlRepository = invoiceControlRepository;
        }

        /// <summary>
        /// Gets the national invoice information paginated.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// List of <see cref="NationalJetFuelInvoiceControlDto" />.
        /// </returns>
        public IList<NationalJetFuelInvoiceControlDto> GetNationalInvoicePaginated(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            try
            {
                List<NationalJetFuelInvoiceControlDto> invoiceControlDto = new List<NationalJetFuelInvoiceControlDto>();
                List<NationalJetFuelInvoiceControl> invoiceControl = this.invoiceControlRepository.GetPaginatedData(pageSize, skip).ToList();
                invoiceControlDto = Mapper.Map<List<NationalJetFuelInvoiceControlDto>>(invoiceControl);

                return invoiceControlDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }


        /// <summary>
        /// Counts the national jet fuel invoice records.
        /// </summary>
        /// <returns>
        /// The total number of national jet fuel invoices.
        /// </returns>
        public int CountNationalInvoiceRecords()
        {
            try
            {
                int total;
                return total = this.invoiceControlRepository.CountTotalRecords();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates the remittance identifier.
        /// </summary>
        /// <param name="remittanceInfo">The remittance information.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<string> ValidateRemittanceID(DataTable remittanceInfo, string airlineCode)
        {
            if (remittanceInfo == null)
            {
                return null;
            }

            try
            {
                List<string> remittancesFound = new List<string>();
                List<RemittanceIDValidate> remittances = new List<RemittanceIDValidate>();
                remittances = this.invoiceControlRepository.ValidateRemittanceID(remittanceInfo, airlineCode).ToList();

                if (remittances.FirstOrDefault(c => c.Verify == 99) != null && remittances.FirstOrDefault(c => c.Verify == 99).Verify == 99)
                {
                    remittancesFound.Add("Contact the Database Administrator.");
                    return remittancesFound;
                }

                remittancesFound.AddRange(remittances.Where(c => c.Verify == 1).Select(c => '{' + c.RemittanceID + ',' + c.MonthYear + ',' + c.Period + '}'));
                return remittancesFound;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates the manual reconcile.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<string> ValidateManualReconcile(DataTable reconcileInfo)
        {
            if (reconcileInfo == null)
            {
                return null;
            }

            try
            {
                List<string> remittancesFound = new List<string>();
                List<RemittanceIDValidate> remittances = new List<RemittanceIDValidate>();
                remittances = this.invoiceControlRepository.ValidateManualReconcile(reconcileInfo).ToList();

                if (remittances.FirstOrDefault(c => c.Verify == 99) != null && remittances.FirstOrDefault(c => c.Verify == 99).Verify == 99)
                {
                    remittancesFound.Add("Contact the Database Administrator.");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 98) != null && remittances.FirstOrDefault(c => c.Verify == 98).Verify == 98)
                {
                    remittancesFound.Add("Some records were not loaded, check Load Log.");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 97) != null && remittances.FirstOrDefault(c => c.Verify == 97).Verify == 97)
                {
                    remittancesFound.Add("Different errors to invalid Equipment Number");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 96) != null && remittances.FirstOrDefault(c => c.Verify == 96).Verify == 96)
                {
                    remittancesFound.Add("Remittance is closed in National Jet Fuel Invoice Control");
                    return remittancesFound;
                }

                remittancesFound.AddRange(remittances.Where(c => c.Verify == 1).Select(c => '{' + c.RemittanceID + ',' + c.MonthYear + ',' + c.Period + '}'));
                return remittancesFound;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates the nonconformity.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<string> ValidateNonconformity(DataTable reconcileInfo)
        {
            if (reconcileInfo == null)
            {
                return null;
            }

            try
            {
                List<string> remittancesFound = new List<string>();
                List<RemittanceIDValidate> remittances = new List<RemittanceIDValidate>();
                remittances = this.invoiceControlRepository.ValidateNonconformity(reconcileInfo).ToList();

                if (remittances.FirstOrDefault(c => c.Verify == 99) != null && remittances.FirstOrDefault(c => c.Verify == 99).Verify == 99)
                {
                    remittancesFound.Add("Contact the Database Administrator.");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 98) != null && remittances.FirstOrDefault(c => c.Verify == 98).Verify == 98)
                {
                    remittancesFound.Add("Some records were not loaded, check Load Log.");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 97) != null && remittances.FirstOrDefault(c => c.Verify == 97).Verify == 97)
                {
                    remittancesFound.Add("Different errors to invalid Equipment Number");
                    return remittancesFound;
                }

                if (remittances.FirstOrDefault(c => c.Verify == 96) != null && remittances.FirstOrDefault(c => c.Verify == 96).Verify == 96)
                {
                    remittancesFound.Add("Remittance is closed in National Jet Fuel Invoice Control");
                    return remittancesFound;
                }

                remittancesFound.AddRange(remittances.Where(c => c.Verify == 1).Select(c => '{' + c.RemittanceID + ',' + c.MonthYear + ',' + c.Period + '}'));
                return remittancesFound;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deleteds the national invoice policy.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns></returns>
        public int DeletedNationalInvoicePolicy(NationalJetFuelInvoiceControlDto invoice)
        {
            var deleted = 0;

            try
            {
                deleted = this.invoiceControlRepository.DeletedInvoicePolicy(new NationalJetFuelInvoiceControl() { RemittanceID = invoice.RemittanceID, MonthYear = invoice.MonthYear, Period = invoice.Period });
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Validates the invoice policy.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// -1: An error during the process.
        /// 0: No errors found.
        /// 1: Error. The policy has been created.
        /// 2: Error caused because there are errors in the remittanse.
        /// </returns>
        public int ValidateInvoicePolicy(NationalJetFuelInvoiceControlDto invoiceControl)
        {
            if (invoiceControl == null || invoiceControl.RemittanceID == null)
            {
                return -1;
            }

            try
            {
                NationalJetFuelInvoiceControl invoiceEntity = Mapper.Map<NationalJetFuelInvoiceControl>(invoiceControl);
                return this.invoiceControlRepository.ValidateInvoicePolicy(invoiceEntity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords, exception);
            }
        }

        /// <summary>
        /// Creates the policies for remittance.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// Greater than zero. The process was completed successfuly.
        /// Less or equal than zero. An error in the process.
        /// </returns>
        public int CreatePoliciesForRemittance(NationalJetFuelInvoiceControlDto invoiceControl)
        {
            if (invoiceControl == null
                || invoiceControl.RemittanceID == null
                || invoiceControl.Period == null
                || invoiceControl.DateBaseline == null
                || invoiceControl.DatePosting == null
                || invoiceControl.DateValue == null
                || invoiceControl.MonthYear == null
            )
            {
                return -1;
            }

            try
            {
                NationalJetFuelInvoiceControl invoiceEntity = Mapper.Map<NationalJetFuelInvoiceControl>(invoiceControl);
                return this.invoiceControlRepository.CreatePolicyDocument(invoiceEntity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords, exception);
            }
        }

        /// <summary>
        /// Gets the national jet fuel reconcile control.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<NationalJetFuelInvoiceControlDto> GetTopNationalJetFuelReconcileControl(string date)
        {
            var invoices = new List<NationalJetFuelInvoiceControlDto>();

            try
            {
                invoices = Mapper.Map<List<NationalJetFuelInvoiceControlDto>>(this.invoiceControlRepository.GetTopNationalJetFuelReconcileControl(date).ToList());
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return invoices;
        }

        /// <summary>
        /// Gets the invoice control.
        /// </summary>
        /// <param name="invoiceKey">The invoice key.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public NationalJetFuelInvoiceControlDto GetInvoiceControl(NationalJetFuelInvoiceControlDto invoiceKey)
        {
            var invoices = new NationalJetFuelInvoiceControlDto();

            try
            {
                invoices = Mapper.Map<NationalJetFuelInvoiceControlDto>(this.invoiceControlRepository.FindNationalJetFuelInvoiceControl(new NationalJetFuelInvoiceControl { RemittanceID = invoiceKey.RemittanceID, MonthYear = invoiceKey.MonthYear, Period = invoiceKey.Period }));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return invoices;
        }

        /// <summary>
        /// Starts the national reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int StartNationalReconcileProcess(NationalJetFuelInvoiceControlDto processDto)
        {
            var start = 0;
            if (processDto == null)
            {
                return start;
            }
            try
            {
                var process = new NationalJetFuelInvoiceControl();
                process = this.invoiceControlRepository.FindNationalReconcileProcessNoTracking(new NationalJetFuelInvoiceControl { RemittanceID = processDto.RemittanceID, MonthYear = processDto.MonthYear, Period = processDto.Period });

                if (process != null && !string.IsNullOrEmpty(process.RemittanceID) && !string.IsNullOrEmpty(process.MonthYear) && !string.IsNullOrEmpty(process.Period))
                {
                    process.ProcessedByUserName = processDto.ProcessedByUserName;
                    this.invoiceControlRepository.Update(process);
                    this.unitOfWork.Commit();
                }
                if (processDto.TypeProcess == NationalReconcileTypeProcessDto.NationalReconcileProcessPending)
                {
                    process.TypeProcess = NationalReconcileTypeProcess.NationalReconcileProcessPending;
                    start = this.invoiceControlRepository.StartNationalReconcileProcess(process);
                }
                else
                {
                    process.TypeProcess = NationalReconcileTypeProcess.NationalReconcileProcessAll;
                    start = this.invoiceControlRepository.StartNationalReconcileProcess(process);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return start;
        }

        /// <summary>
        /// Reverts the reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int RevertReconcileProcess(NationalJetFuelInvoiceControlDto processDto)
        {
            var revert = 0;

            try
            {
                var process = new NationalJetFuelInvoiceControl { RemittanceID = processDto.RemittanceID, MonthYear = processDto.MonthYear, Period = processDto.Period, TypeProcess = NationalReconcileTypeProcess.NationalReconcileProcessRevert };
                revert = this.invoiceControlRepository.RevertReconcileProcess(process);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return revert;
        }

        /// <summary>
        /// Reverts the manual reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int RevertManualReconcileProcess(NationalJetFuelInvoiceControlDto processDto)
        {
            var revert = 0;

            try
            {
                var process = new NationalJetFuelInvoiceControl { RemittanceID = processDto.RemittanceID, MonthYear = processDto.MonthYear, Period = processDto.Period, TypeProcess = NationalReconcileTypeProcess.NationalReconcileProcessRevert };
                revert = this.invoiceControlRepository.RevertManualReconcileProcess(process);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return revert;
        }

        /// <summary>
        /// Reverts the nonconformity process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public int RevertNonconformityProcess(NationalJetFuelInvoiceControlDto processDto)
        {
            var revert = 0;

            try
            {
                var process = new NationalJetFuelInvoiceControl { RemittanceID = processDto.RemittanceID, MonthYear = processDto.MonthYear, Period = processDto.Period, TypeProcess = NationalReconcileTypeProcess.NationalReconcileProcessRevert };
                revert = this.invoiceControlRepository.RevertNonconformityProcess(process);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return revert;
        }

        /// <summary>
        /// Closes the nonconformity.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool CloseNonconformity(NationalJetFuelInvoiceControlDto processDto)
        {
            if (processDto == null)
            {
                return false;
            }

            try
            {
                NationalJetFuelInvoiceControl process = this.invoiceControlRepository.FindNationalJetFuelInvoiceControl(Mapper.Map<NationalJetFuelInvoiceControl>(processDto));
                process.NonconformityStatusCode = "CLOSED";
                //process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                //process.ConfirmationDate = processDto.ConfirmationDate;
                this.invoiceControlRepository.Update(process);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Opens the nonconformity.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool OpenNonconformity(NationalJetFuelInvoiceControlDto processDto)
        {
            if (processDto == null)
            {
                return false;
            }

            try
            {
                NationalJetFuelInvoiceControl process = this.invoiceControlRepository.FindNationalJetFuelInvoiceControl(Mapper.Map<NationalJetFuelInvoiceControl>(processDto));
                process.NonconformityStatusCode = "OPEN";
                //process.ConfirmedByUserName = processDto.ConfirmedByUserName;
                //process.ConfirmationDate = processDto.ConfirmationDate;
                this.invoiceControlRepository.Update(process);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}
