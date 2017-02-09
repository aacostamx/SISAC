//------------------------------------------------------------------------
// <copyright file="UploadNationalFuelContractController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using FileHelpers;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Resources;
    using VOI.SISAC.Business.Finance;

    /// <summary>
    /// Controller that manage the upload of multiples national fuel contracts
    /// </summary>
    [CustomAuthorize]
    public class UploadNationalFuelContractController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalFuelContractController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "National Contract";

        /// <summary>
        /// The upload contract
        /// </summary>
        private readonly IUploadNationalFuelContractBusiness uploadContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadNationalFuelContractController" /> class.
        /// </summary>
        /// <param name="uploadContract">The upload contract.</param>
        public UploadNationalFuelContractController(IUploadNationalFuelContractBusiness uploadContract)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.uploadContract = uploadContract;
        }

        /// <summary>
        /// Read Text File
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NATFUELCON-UPLF")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // IList<NationalFuelContractVO> contractList = GetContracts();
            IList<string> errors = new List<string>();

            // Validates that the file has content
            if (file == null || !file.ContentType.Equals("text/plain") || file.ContentLength <= 0)
            {
                this.TempData["ErrorMessage"] = Resource.ErrorFormatFile;
                return this.RedirectToAction("Index", "NationalFuelContract");
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                this.TempData["ErrorMessage"] = Resource.FormatFileError;
                return this.RedirectToAction("Index", "NationalFuelContract");
            }

            try
            {
                DelimitedFileEngine<NationalFuelContractFile> engine = new DelimitedFileEngine<NationalFuelContractFile>();
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                NationalFuelContractFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<string> errorResult = new List<string>();
                errorResult = this.FindErrors(engine);

                // Validates errors in the file
                if (errorResult != null && errorResult.Count > 0)
                {
                    this.TempData["ListErrorMessage"] = errorResult;
                    return this.RedirectToAction("Index", "NationalFuelContract");
                }

                List<NationalFuelContractFile> fileRecords = new List<NationalFuelContractFile>(records);
                List<NationalFuelContractDto> contractsFromFile = Mapper.Map<List<NationalFuelContractDto>>(fileRecords);
                NationalFuelContractConceptDto conceptsFromFile = new NationalFuelContractConceptDto();

                for (int i = 0; i < fileRecords.Count(); i++)
                {
                    contractsFromFile[i].NationalFuelContractConcept = new List<NationalFuelContractConceptDto>();
                    conceptsFromFile = Mapper.Map<NationalFuelContractConceptDto>(fileRecords[i]);
                    contractsFromFile[i].NationalFuelContractConcept.Add(conceptsFromFile);
                }

                // Put the concepts in their own contracts
                List<NationalFuelContractDto> contractsDtoFinal = GroupContracts(contractsFromFile);

                // Begins to validate the contracts and if is success save the contracts in DB
                errors = this.uploadContract.UploadNationalFuelContracts(contractsDtoFinal);

                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                    return this.RedirectToAction("Index", "NationalFuelContract");
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errors;
                    return this.RedirectToAction("Index", "NationalFuelContract");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.ToString();
            }

            return this.RedirectToAction("Index", "NationalFuelContract");
        }

        /// <summary>
        /// Downloads the requiered file.
        /// </summary>
        /// <returns>The template for the upload file.</returns>
        public FileContentResult Download()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.NationalFuelContractFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.NationalFuelContractFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }

        /// <summary>
        /// Groups the contracts.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>List of national fuel contract.</returns>
        private static List<NationalFuelContractDto> GroupContracts(List<NationalFuelContractDto> contracts)
        {
            List<NationalFuelContractDto> contractsResult = new List<NationalFuelContractDto>();
            NationalFuelContractDto contractNew = new NationalFuelContractDto();
            IList<NationalFuelContractDto> contractConceptNew = new List<NationalFuelContractDto>();
            var contractDistinct = (from ssi in contracts
                                    group ssi by new
                                    {
                                        ssi.EffectiveDate,
                                        ssi.AirlineCode,
                                        ssi.StationCode,
                                        ssi.ServiceCode,
                                        ssi.ProviderNumberPrimary
                                    } 
                                    into g
                                    select new
                                    {
                                        EffectiveDate = g.Key.EffectiveDate,
                                        AirlineCode = g.Key.AirlineCode,
                                        StationCode = g.Key.StationCode,
                                        ServiceCode = g.Key.ServiceCode,
                                        ProviderNumberPrimary = g.Key.ProviderNumberPrimary
                                    })
                                    .ToList();

            foreach (var item in contractDistinct)
            {
                contractNew = contracts.FirstOrDefault(c =>
                    c.EffectiveDate == item.EffectiveDate
                    && c.AirlineCode == item.AirlineCode
                    && c.StationCode == item.StationCode
                    && c.ServiceCode == item.ServiceCode
                    && c.ProviderNumberPrimary == item.ProviderNumberPrimary);

                contractConceptNew = contracts.Where(c => c.EffectiveDate == item.EffectiveDate
                                                  && c.AirlineCode == item.AirlineCode
                                                  && c.StationCode == item.StationCode
                                                  && c.ServiceCode == item.ServiceCode
                                                  && c.ProviderNumberPrimary == item.ProviderNumberPrimary).ToList();

                IList<NationalFuelContractConceptDto> conceptListNew = new List<NationalFuelContractConceptDto>();
                foreach (NationalFuelContractDto contractDto in contractConceptNew)
                {
                    if (contractDto.NationalFuelContractConcept.Count > 0)
                    {
                        conceptListNew.Add(contractDto.NationalFuelContractConcept.FirstOrDefault());
                    }
                }

                contractNew.NationalFuelContractConcept = conceptListNew;
                contractsResult.Add(contractNew);
            }

            return contractsResult;
        }

        /// <summary>
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// Find errors in the given file.
        /// </returns>
        private IList<string> FindErrors(DelimitedFileEngine<NationalFuelContractFile> file)
        {
            IList<string> fileErrors = new List<string>();

            // Finds errors in the file
            foreach (var error in file.ErrorManager.Errors)
            {
                fileErrors.Add(Resource.ErrorValidationFile + error.LineNumber);
            }

            return fileErrors;
        }
    }
}