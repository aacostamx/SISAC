//------------------------------------------------------------------------
// <copyright file="UploadItineraryController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.Controllers
{
    using Business.Airport;
    using Business.Common;
    using Business.Dto.Generic;
    using Business.Dto.Itineraries;
    using Business.ExceptionBusiness;
    using Business.Itineraries;
    using global::AutoMapper;
    using Helpers;
    using LumenWorks.Framework.IO.Csv;
    using Models.Files;
    using Models.VO.Itinerary;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;

    /// <summary>
    /// UploadItineraryController class
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UploadItineraryController : ApiController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(UploadItineraryController));

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// The airplane business
        /// </summary>
        private readonly IAirplaneBusiness airplaneBusiness;

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The generic notification business
        /// </summary>
        private readonly IGenericNotificationBusiness genericNotificationBusiness;

        /// <summary>
        /// The timeline business
        /// </summary>
        private readonly ITimelineBusiness timelineBusiness;

        /// <summary>
        /// The airline business
        /// </summary>
        public readonly IAirlineBusiness airlineBusiness;

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        private string user { get; set; }

        /// <summary>
        /// Gets or sets the pass.
        /// </summary>
        /// <value>
        /// The pass.
        /// </value>
        private string pass { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        private string filePath { get; set; }

        /// <summary>
        /// Gets or sets the network path.
        /// </summary>
        /// <value>
        /// The network path.
        /// </value>
        private string networkPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [login enable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [login enable]; otherwise, <c>false</c>.
        /// </value>
        public bool loginEnable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadItineraryController"/> class.
        /// </summary>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="airplaneBusiness">The airplane business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="genericNotificationBusiness">The generic notification business.</param>
        /// <param name="timelineBusiness">The timeline business.</param>
        public UploadItineraryController(IItineraryBusiness itineraryBusiness,
            IAirplaneBusiness airplaneBusiness, IAirportBusiness airportBusiness,
            IGenericNotificationBusiness genericNotificationBusiness,
            ITimelineBusiness timelineBusiness, IAirlineBusiness airlineBusiness)
        {
            this.itineraryBusiness = itineraryBusiness;
            this.airplaneBusiness = airplaneBusiness;
            this.airportBusiness = airportBusiness;
            this.genericNotificationBusiness = genericNotificationBusiness;
            this.timelineBusiness = timelineBusiness;
            this.airlineBusiness = airlineBusiness;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ItineraryUploadAPI Get([FromUri]ItineraryUploadAPI file)
        {
            try
            {
                Launcher(file);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - UploadItinerarycontroller - GET");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - UploadItinerarycontroller - GET");
                Trace.TraceError(ex.Message, ex);
            }

            return file;
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        public ItineraryUploadAPI Post(ItineraryUploadAPI file)
        {
            try
            {
                Launcher(file);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - UploadItinerarycontroller: POST");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - UploadItinerarycontroller: POST");
                Trace.TraceError(ex.Message, ex);
                file.errors.Add(ex.Message);
                file.errors.Add(ex.ToString());
            }

            return file;
        }

        /// <summary>
        /// Launchers the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private ItineraryUploadAPI Launcher(ItineraryUploadAPI file)
        {
            try
            {
                if (file.readServerFile)
                {
                    this.ReadJeppesenRep(file);
                }
                file.sucess = this.ValidateJeppesenRep(file);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - Launcher");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - Launcher");
                Trace.TraceError(ex.Message, ex);
            }

            return file;
        }

        /// <summary>
        /// Validates the Jeppesen rep.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private bool ValidateJeppesenRep(ItineraryUploadAPI file)
        {
            var sucess = false;
            var mailDto = new MailModelDto();
            var mailInfoDto = new ItineraryMailDto();
            var invalidItineraries = new List<ItineraryFile>();

            try
            {

                //Total Number Lines
                mailInfoDto.numberLines = file.itineraries.Count;

                //Number of flight
                mailInfoDto.flightsFile = file.itineraries.Count;
                validateFileErrorsJeppesen(file, invalidItineraries);

                //Total by Date
                var totalGroupFligths = file.itineraries.GroupBy(c => new { c.DepartureDate.Date }).Select(y => new ItineraryGroupMailDto { ItineraryDate = y.Key.Date, TotalGroupFlights = y.Count() }).ToList();

                //Eliminando los que tienen error 
                file.itineraries.RemoveAll(c => invalidItineraries.Contains(c));

                //Se agrupan por fecha de salida y se saca el total de cada dia
                mailInfoDto.ItineraryGroups = file.itineraries.GroupBy(c => new { c.DepartureDate.Date }).Select(y => new ItineraryGroupMailDto { ItineraryDate = y.Key.Date, TotalGroupProcess = y.Count() }).ToList();

                //Procesa la informacion para mostrar en el correo
                InfoMailTable(mailInfoDto, totalGroupFligths);

                ///Total flights Processed
                mailInfoDto.flightsProcessed = file.itineraries.Count;

                if (file.itineraries != null && file.itineraries.Count > 0)
                {
                    //Validate columns 
                    var itinerariesDto = new List<ItineraryDto>();

                    foreach (var item in file.itineraries)
                    {
                        var itineraryDto = new ItineraryDto();

                        itineraryDto.AirlineCode = item.AirlineCode;
                        itineraryDto.ArrivalDate = item.ArrivalDate;
                        itineraryDto.ArrivalStation = item.DST;
                        itineraryDto.DepartureDate = item.DepartureDate;
                        itineraryDto.DepartureStation = item.DEP;
                        itineraryDto.EquipmentNumber = item.ACREGNUMBER;
                        itineraryDto.FlightNumber = item.FLTNUM;
                        itineraryDto.ItineraryKey = item.ItineraryKey;
                        itineraryDto.Line = item.Line;

                        itinerariesDto.Add(itineraryDto);
                    }

                    itineraryBusiness.AddOrUpdateItinerary(itinerariesDto);
                    sucess = true;
                }
                else
                {
                    file.errors.Add(Resource.JeppesenFileEmpty);
                }

                mailDto = new MailModelDto()
                {
                    SmtpClient = WebConfigurationManager.AppSettings["SmtpClient"],
                    UserToken = WebConfigurationManager.AppSettings["UserToken"],
                    PasswordToken = WebConfigurationManager.AppSettings["PasswordToken"],
                    Subject = WebConfigurationManager.AppSettings["Subject"],
                    To = WebConfigurationManager.AppSettings["To"],
                    AttachedFileName = WebConfigurationManager.AppSettings["AttachedFileName"],
                    Errors = file.errors
                };
            }
            catch (BusinessException ex)
            {
                Logger.Error("Error: SISAC API - ValidateJeppesenRep");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - ValidateJeppesenRep");
                Trace.TraceError(ex.Message, ex);
                file.errors.Add(ex.Message);
                file.errors.Add(ex.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error("Error: SISAC API - ValidateJeppesenRep");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - ValidateJeppesenRep");
                Trace.TraceError(ex.Message, ex);
                file.errors.Add(ex.Message);
                file.errors.Add(ex.ToString());
            }

            if (file.email)
            {
                this.SendingMail(file.errors, mailDto, mailInfoDto);
                this.AutomaticTimelineProcess();
            }

            return sucess;
        }

        /// <summary>
        /// Validates the file errors Jeppesen.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="invalidItineraries">The invalid itineraries.</param>
        private void validateFileErrorsJeppesen(ItineraryUploadAPI file, List<ItineraryFile> invalidItineraries)
        {
            var validAirlines = airlineBusiness.GetActivesAirline().Select(c => c.AirlineCode).ToList();
            var validEquipmentNumbers = airplaneBusiness.GetActivesAirplane().Select(c => c.EquipmentNumber).ToList();
            var validStations = airportBusiness.GetActivesAirports().Select(c => c.StationCode).ToList();

            //string para informar errores
            var equipmentNumberText = Resource.EquipmentNumber;
            var departureAirportText = Resource.DepartureAirport;
            var arrivalAirportText = Resource.ArrivalAirport;
            var requiredText = Resource.RequiredField;
            var notFoundDBText = Resource.NotFoundDB;
            var lineText = Resource.Line;
            var InvalidFltType = Resource.InvalidFltType;

            var fltNumText = Resource.FlightNumber;
            var airlineText = Resource.Airline;
            var departureDate = Resource.DepartureDate;
            var hourDeparture = Resource.DepartureTime;
            var hourArrival = Resource.ArriveTime;
            var fltType = Resource.FlightType;

            foreach (var item in file.itineraries)
            {
                var countErrors = 0;
                var exitsAirline = validAirlines.Contains(item.AirlineCode);
                var exitsEquipment = validEquipmentNumbers.Contains(item.ACREGNUMBER);
                var exitsDepStation = validStations.Contains(item.DEP);
                var exitsDetStation = validStations.Contains(item.DST);

                var fields = new Dictionary<int, string>();
                var fieldsErrorMessenge = new Dictionary<int, string>();

                fields.Add(1, item.FLTNUM);
                fields.Add(2, item.AirlineCode);
                fields.Add(3, item.ACREGNUMBER);
                fields.Add(4, item.FLTORGDATELT);
                fields.Add(5, item.DEP);
                fields.Add(6, item.DST);
                fields.Add(7, item.STDLT);
                fields.Add(8, item.STALT);
                fields.Add(9, item.FLTTYPE);

                fieldsErrorMessenge.Add(1, fltNumText);
                fieldsErrorMessenge.Add(2, airlineText);
                fieldsErrorMessenge.Add(3, equipmentNumberText);
                fieldsErrorMessenge.Add(4, departureDate);
                fieldsErrorMessenge.Add(5, departureAirportText);
                fieldsErrorMessenge.Add(6, arrivalAirportText);
                fieldsErrorMessenge.Add(7, hourDeparture);
                fieldsErrorMessenge.Add(8, hourArrival);
                fieldsErrorMessenge.Add(9, fltType);

                foreach (var stringItem in fields)
                {
                    if (string.IsNullOrEmpty(stringItem.Value))
                    {
                        file.errors.Add(fieldsErrorMessenge[stringItem.Key] + " " + requiredText + " " + lineText + item.Line);
                        countErrors++;
                    }
                }

                if (!exitsAirline)
                {
                    file.errors.Add(airlineText + ": '" + item.AirlineCode + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }

                if (!exitsEquipment)
                {
                    file.errors.Add(equipmentNumberText + ": '" + item.ACREGNUMBER + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDepStation)
                {
                    file.errors.Add(departureAirportText + ": '" + item.DEP + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDetStation)
                {
                    file.errors.Add(arrivalAirportText + ": '" + item.DST + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }

                if (item.DEP == item.DST)
                {
                    file.errors.Add(departureAirportText + "-" + arrivalAirportText + ": " + item.DEP + "-" + item.DST + " " + lineText + item.Line);
                    countErrors++;
                }

                if (Right(item.FLTNUM, 1) == "Z")
                {
                    file.errors.Add(fltNumText + ": " + item.FLTNUM + " " + lineText + item.Line);
                    countErrors++;
                }

                if (item.FLTTYPE != "A")
                {
                    file.errors.Add(InvalidFltType + ": " + item.FLTTYPE + " " + lineText + item.Line);
                    countErrors++;
                }

                if (countErrors > 0)
                {
                    invalidItineraries.Add(item);
                }
            }
        }

        /// <summary>
        /// Reads the Jeppesen rep from server
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private void ReadJeppesenRep(ItineraryUploadAPI file)
        {
            var csvList = new List<string[]>();
            this.networkPath = WebConfigurationManager.AppSettings["NetworkPath"];
            this.user = WebConfigurationManager.AppSettings["NetworkUser"];
            this.pass = WebConfigurationManager.AppSettings["NetworkPass"];
            this.filePath = WebConfigurationManager.AppSettings["FilePath"];
            this.loginEnable = Convert.ToBoolean(WebConfigurationManager.AppSettings["LoginEnabled"]);

            try
            {
                if (loginEnable)
                {
                    var response = PinvokeWindowsNetworking.connectToRemote(this.networkPath, this.user, this.pass);
                }

                var fileStream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read);

                using (var csv = new CachedCsvReader(new StreamReader(fileStream, Encoding.UTF8), true))
                {
                    csvList = csv.ToList();
                }

                if (csvList != null && csvList.Count > 0)
                {
                    for (int i = 0; i < csvList.Count; i++)
                    {
                        var Jeppesen = new ItineraryFile();
                        Jeppesen.Line = i + 2;
                        Jeppesen.AirlineCode = csvList[i][0];
                        Jeppesen.FLTNUM = csvList[i][1];
                        Jeppesen.ACREGNUMBER = csvList[i][2];
                        Jeppesen.FLTORGDATELT = csvList[i][3];
                        Jeppesen.DEP = csvList[i][4];
                        Jeppesen.DST = csvList[i][10];
                        Jeppesen.SKDDST = csvList[i][9];
                        Jeppesen.STDLT = csvList[i][32];
                        Jeppesen.STALT = csvList[i][46];
                        Jeppesen.FLTTYPE = csvList[i][44];
                        setDates(Jeppesen);
                        file.itineraries.Add(Jeppesen);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - ReadJeppesenRep");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - ReadJeppesenRep");
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Automatics the timeline process.
        /// </summary>
        private void AutomaticTimelineProcess()
        {
            try
            {
                this.timelineBusiness.TimelineStartProcress(null, null);
            }
            catch (Exception ex)
            {
                Logger.Error("Error: SISAC API - AutomaticTimelineProcess - TimelineStartProcress(null, null)");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - AutomaticTimelineProcess - TimelineStartProcress(null, null)");
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Informations the mail table.
        /// </summary>
        /// <param name="mailInfoDto">The mail information dto.</param>
        /// <param name="totalGroupFligths">The total group fligths.</param>
        private static void InfoMailTable(ItineraryMailDto mailInfoDto, List<ItineraryGroupMailDto> totalGroupFligths)
        {
            if (mailInfoDto.ItineraryGroups.Count == totalGroupFligths.Count)
            {
                for (int i = 0; i < totalGroupFligths.Count; i++)
                {
                    if (totalGroupFligths[i].ItineraryDate == mailInfoDto.ItineraryGroups[i].ItineraryDate)
                    {
                        mailInfoDto.ItineraryGroups[i].TotalGroupFlights = totalGroupFligths[i].TotalGroupFlights;
                        mailInfoDto.ItineraryGroups[i].TotalGroupErrors = mailInfoDto.ItineraryGroups[i].TotalGroupFlights - mailInfoDto.ItineraryGroups[i].TotalGroupProcess;
                    }
                }
            }
        }

        /// <summary>
        /// Sendings the mail.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="mailDto">The mail dto.</param>
        /// <param name="mailInfo">The mail information.</param>
        private void SendingMail(List<string> errors, MailModelDto mailDto, ItineraryMailDto mailInfo)
        {
            try
            {
                if (errors.Count > 0)
                {
                    mailInfo.TotalErrors = errors.Count;
                    mailDto.Body = this.genericNotificationBusiness.MailConfigurationItinerary(mailInfo);
                    var emailErrors = this.genericNotificationBusiness.SendingEmail(mailDto).ToList();
                    if (emailErrors.Count > 0)
                    {
                        Logger.Error(emailErrors);
                        Trace.TraceError(emailErrors.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validates the parameters.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private ItineraryUploadAPI ValidateParams(ItineraryUploadAPI file)
        {
            if (file == null)
            {
                file = new ItineraryUploadAPI();
            }

            file.StartDate = file.StartDate == null || file.StartDate == DateTime.MinValue ? DateTime.Now.AddDays(-1).Date : file.StartDate;
            file.EndDate = file.EndDate == null || file.EndDate == DateTime.MinValue ? DateTime.Now.AddDays(1).Date : file.EndDate;

            return file;

        }

        /// <summary>
        /// Sets the dates.
        /// </summary>
        /// <param name="fileRow">The file row.</param>
        private void setDates(ItineraryFile fileRow)
        {
            var date = new DateTime();

            try
            {
                if (DateTime.TryParseExact(fileRow.FLTORGDATELT, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    fileRow.DepartureDate = date;
                    fileRow.ArrivalDate = date;
                    TimeSpan time;
                    if (!string.IsNullOrEmpty(fileRow.STDLT)
                        && fileRow.STDLT.Length > 2
                        && TimeSpan.TryParse(fileRow.STDLT, out time))
                    {
                        fileRow.DepartureDate = fileRow.DepartureDate.Add(time);
                    }

                    if (!string.IsNullOrEmpty(fileRow.STALT)
                        && fileRow.STALT.Length > 2
                        && TimeSpan.TryParse(fileRow.STALT, out time))
                    {
                        fileRow.ArrivalDate = fileRow.ArrivalDate.Add(time);
                    }

                    if (fileRow.DepartureDate > fileRow.ArrivalDate)
                    {
                        fileRow.ArrivalDate = fileRow.ArrivalDate.AddDays(1);
                    }

                    if (fileRow.DepartureDate != DateTime.MinValue)
                    {
                        fileRow.ItineraryKey = fileRow.DepartureDate.ToString("yyyyMMdd");
                    }
                }
                else
                {
                    Logger.Error(string.Format(Resource.InvalidDateFormat + ": {0} - " + Resource.Line + ": {1}", fileRow.FLTORGDATELT, fileRow.Line));
                    Trace.TraceError(string.Format(Resource.InvalidDateFormat + ": {0} - " + Resource.Line + ": {1}", fileRow.FLTORGDATELT, fileRow.Line));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Rights the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private string Right(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
    }
}
