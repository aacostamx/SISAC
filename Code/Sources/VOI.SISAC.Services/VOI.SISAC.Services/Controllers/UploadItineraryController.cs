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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;
    using Web.Models.Files;
    using Web.Models.VO.Itineraries;
    using Web.Resources;


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
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        private string smtpClient { get; set; }

        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        /// <value>
        /// The user token.
        /// </value>
        private string userToken { get; set; }

        /// <summary>
        /// Gets or sets the password token.
        /// </summary>
        /// <value>
        /// The password token.
        /// </value>
        private string passwordToken { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        private string subject { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        private string to { get; set; }

        /// <summary>
        /// Gets or sets the name of the attached file.
        /// </summary>
        /// <value>
        /// The name of the attached file.
        /// </value>
        private string attachedFileName { get; set; }

        public bool loginEnable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadItineraryController"/> class.
        /// </summary>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        public UploadItineraryController(IItineraryBusiness itineraryBusiness,
            IAirplaneBusiness airplaneBusiness, IAirportBusiness airportBusiness,
            IGenericNotificationBusiness genericNotificationBusiness)
        {
            this.itineraryBusiness = itineraryBusiness;
            this.airplaneBusiness = airplaneBusiness;
            this.airportBusiness = airportBusiness;
            this.genericNotificationBusiness = genericNotificationBusiness;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public List<string> Get([FromUri]ItineraryUploadVO file)
        {
            var sucess = false;

            try
            {
                Logger.Info(string.Format("Inicia peticion GET de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.WriteLine(string.Format("Inicia peticion GET de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                file = ValidateParams(file);
                file.lines = ReadY4GenRep();
                sucess = ValidateY4GenRep(file);
                Logger.Info(string.Format("Finaliza peticion GET de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.WriteLine(string.Format("Finaliza peticion GET de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - UploadItinerarycontroller - GET");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - UploadItinerarycontroller - GET");
                Trace.TraceError(ex.Message, ex);
            }

            return file.lines;
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        public List<string> Post(ItineraryUploadVO file)
        {
            var sucess = false;

            try
            {
                Logger.Info(string.Format("Inicia peticion POST de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.WriteLine(string.Format("Inicia peticion POST de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                file = ValidateParams(file);
                file.lines = ReadY4GenRep();
                sucess = ValidateY4GenRep(file);
                Logger.Info(string.Format("Finaliza peticion POST de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.WriteLine(string.Format("Finaliza peticion POST de SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - UploadItinerarycontroller: POST");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - UploadItinerarycontroller: POST");
                Trace.TraceError(ex.Message, ex);
            }

            return file.lines;
        }

        /// <summary>
        /// Reads the Y4 genrep.
        /// </summary>
        /// <returns></returns>
        private List<string> ReadY4GenRep()
        {
            var lines = new List<string>();
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
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                if (lines.Count < 0)
                {
                    Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                    Logger.Error("Error: El archivo Y4GENREP.TXT no contiene datos o no fue posible acceder");
                    Trace.TraceError("Error: El archivo Y4GENREP.TXT no contiene datos o no fue posible acceder");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Logger.Error("Error: SISAC API - ReadY4GENREP");
                Logger.Error(ex.Message, ex);
                Trace.TraceError("Error: SISAC API - ReadY4GENREP");
                Trace.TraceError(ex.Message, ex);
            }

            return lines;
        }

        /// <summary>
        /// Validates the Y4 gen rep.
        /// </summary>
        /// <param name="file">The input.</param>
        /// <returns></returns>
        private bool ValidateY4GenRep(ItineraryUploadVO file)
        {
            {
                IDictionary<string, string> colNamesLength = new Dictionary<string, string>();
                List<ItineraryFile> itinerariesFile = new List<ItineraryFile>();
                List<ItineraryFile> itinerariesFileError = new List<ItineraryFile>();
                IList<ItineraryDto> itinerariesDto = new List<ItineraryDto>();
                var errors = new List<string>();
                var columnsLength = new List<string>();
                bool record = false, dayBefore = false, dayAfter = false, sucess = false;
                int countErrors = 0;
                var mailDto = new MailModelDto();
                var mailInfoDto = new ItineraryMailDto();

                this.smtpClient = WebConfigurationManager.AppSettings["SmtpClient"];
                this.userToken = WebConfigurationManager.AppSettings["UserToken"];
                this.passwordToken = WebConfigurationManager.AppSettings["PasswordToken"];
                this.subject = WebConfigurationManager.AppSettings["Subject"];
                this.to = WebConfigurationManager.AppSettings["To"];
                this.attachedFileName = WebConfigurationManager.AppSettings["AttachedFileName"];

                try
                {
                    //Loop for each line on the txt file
                    for (int i = 0; i < file.lines.Count; i++)
                    {
                        //Validate flag
                        record = string.IsNullOrEmpty(file.lines[i]) ? false : record;

                        //If record flag is on, begins recording info
                        if (record)
                        {
                            getFileInfo(colNamesLength, itinerariesFile, file.lines, file.AirlineCodeCombobox, i);
                        }

                        //Search Delimiter and start flag
                        searchDelimite(ref colNamesLength, file.lines, ref columnsLength, ref record, i);
                    }

                    //Total Number Lines
                    mailInfoDto.numberLines = file.lines.Count;

                    //Valida que exitan el día anterior y el día posterior a la selección en el archivo
                    dayBefore = itinerariesFile.Exists(c => c.DepartureDate.Date == file.StartDate.Date);
                    dayAfter = itinerariesFile.Exists(c => c.DepartureDate.Date == file.EndDate.Date);

                    if (dayBefore && dayAfter)
                    {
                        //Get al itineraries between StartDate - 1 day and EndDate + 1 apply to DepartureDate
                        itinerariesFile = itinerariesFile.Where(c => c.DepartureDate.Date >= file.StartDate.Date && c.DepartureDate <= file.EndDate.Date).ToList();

                        //Number of flight
                        mailInfoDto.flightsFile = itinerariesFile.Count;

                        //Validate all file errors
                        countErrors = validateFileErrors(itinerariesFile, itinerariesFileError, errors, countErrors);

                        var totalGroupFligths = itinerariesFile.GroupBy(c => new { c.DepartureDate.Date }).Select(y => new ItineraryGroupMailDto { ItineraryDate = y.Key.Date, TotalGroupFlights = y.Count() }).ToList();

                        //Eliminando los que tienen error 
                        itinerariesFile.RemoveAll(c => itinerariesFileError.Contains(c));

                        var changeSchedule = itinerariesFile.GroupBy(c => new { c.FLTNUM, c.ItineraryKey, c.DepartureStation, c.ArrivalStation }).Where(d => d.Count() > 1).Select(y => new { itinerary = y.Key, Counter = y.Count() }).ToList();

                        foreach (var item in changeSchedule)
                        {
                            var itineraryLine = itinerariesFile.Where(c =>
                            c.FLTNUM == item.itinerary.FLTNUM &&
                            c.FLTNUM == item.itinerary.FLTNUM &&
                            c.ItineraryKey == item.itinerary.ItineraryKey &&
                            c.DepartureStation == item.itinerary.DepartureStation &&
                            c.ArrivalStation == item.itinerary.ArrivalStation)
                            .FirstOrDefault();

                            errors.Add(Resource.FlightNumber + ": '" + item.itinerary.FLTNUM + "' " + Resource.ChangeSchedule + " " + Resource.Line + itineraryLine.Line);
                        }

                        //Elimina los duplicados agruparlo por vuelo, llave, salida y llegada (toma el último)
                        itinerariesFile = itinerariesFile.GroupBy(c => new { c.FLTNUM, c.ItineraryKey, c.DepartureStation, c.ArrivalStation }).Select(c => c.Last()).ToList();

                        //Elimina los duplicados agruparlo por vuelo, llave y salida (toma el último)
                        itinerariesFile = itinerariesFile.GroupBy(c => new { c.FLTNUM, c.ItineraryKey, c.DepartureStation }).Select(c => c.Last()).ToList();

                        //Se agrupan por fecha de salida y se saca el total de cada dia
                        mailInfoDto.ItineraryGroups = itinerariesFile.GroupBy(c => new { c.DepartureDate.Date }).Select(y => new ItineraryGroupMailDto { ItineraryDate = y.Key.Date, TotalGroupProcess = y.Count() }).ToList();

                        //Procesa la informacion para mostrar en el correo
                        InfoMailTable(mailInfoDto, totalGroupFligths);

                        ///Total flights Processed
                        mailInfoDto.flightsProcessed = itinerariesFile.Count;

                        //Validate columns 
                        itinerariesDto = Mapper.Map<List<ItineraryDto>>(itinerariesFile);
                        itineraryBusiness.AddOrUpdateItinerary(itinerariesDto);
                        sucess = true;

                        mailDto = new MailModelDto()
                        {
                            SmtpClient = smtpClient,
                            UserToken = userToken,
                            PasswordToken = passwordToken,
                            Subject = subject,
                            To = to,
                            Errors = errors,
                            AttachedFileName = attachedFileName
                        };
                    }
                }
                catch (BusinessException ex)
                {
                    Logger.Error("Error: SISAC API - ValidateY4GenRep");
                    Logger.Error(ex.Message, ex);
                    Trace.TraceError("Error: SISAC API - ValidateY4GenRep");
                    Trace.TraceError(ex.Message, ex);
                    errors.Add(ex.Message);
                    errors.Add(ex.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Error("Error: SISAC API - ValidateY4GenRep");
                    Logger.Error(ex.Message, ex);
                    Trace.TraceError("Error: SISAC API - ValidateY4GenRep");
                    Trace.TraceError(ex.Message, ex);
                    errors.Add(ex.Message);
                    errors.Add(ex.ToString());
                }

                SendingMail(errors, mailDto, mailInfoDto);

                return sucess;
            }
        }

        #region Auxiliary Methods
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
        private ItineraryUploadVO ValidateParams(ItineraryUploadVO file)
        {
            if (file == null)
            {
                file = new ItineraryUploadVO();
            }

            file.AirlineCodeCombobox = string.IsNullOrWhiteSpace(file.AirlineCodeCombobox) ? "Y4" : file.AirlineCodeCombobox;
            file.StartDate = file.StartDate == null || file.StartDate == DateTime.MinValue ? DateTime.Now.AddDays(-1).Date : file.StartDate;
            file.EndDate = file.EndDate == null || file.EndDate == DateTime.MinValue ? DateTime.Now.AddDays(1).Date : file.EndDate;

            return file;

        }

        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <param name="colNamesLength">Length of the col names.</param>
        /// <param name="itinerariesFile">The itineraries file.</param>
        /// <param name="lines">The lines.</param>
        /// <param name="airline">The airline.</param>
        /// <param name="i">The i.</param>
        private void getFileInfo(IDictionary<string, string> colNamesLength, List<ItineraryFile> itinerariesFile, List<string> lines, string airline, int i)
        {
            ItineraryFile fileRow = new ItineraryFile();

            fileRow.Line = i + 1;
            fileRow.AirlineCode = airline;

            var coords = colNamesLength.Where(c => c.Key == "FLTNUM").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.FLTNUM = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "ACREGNUMBER").Select(c => c.Value).FirstOrDefault().Trim();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.ACREGNUMBER = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "FLTORGDATELT").Select(c => c.Value).FirstOrDefault().Trim();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.FLTORGDATELT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "DEP").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.DEP = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "DST").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.DST = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "SKDDST").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.SKDDST = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "STDLT").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.STDLT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "STALT").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.STALT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            fileRow.DepartureStation = fileRow.DEP;
            fileRow.ArrivalStation = fileRow.DST;
            setDates(fileRow);

            itinerariesFile.Add(fileRow);
        }

        /// <summary>
        /// Sets the dates.
        /// </summary>
        /// <param name="fileRow">The file row.</param>
        private void setDates(ItineraryFile fileRow)
        {
            DateTime date = new DateTime();
            if (DateTime.TryParse(fileRow.FLTORGDATELT, out date))
            {
                fileRow.DepartureDate = date;
                fileRow.ArrivalDate = date;
                TimeSpan time;
                if (!string.IsNullOrEmpty(fileRow.STDLT)
                    && fileRow.STDLT.Length > 2
                    && TimeSpan.TryParse(fileRow.STDLT.Insert(2, ":"), out time))
                {
                    fileRow.DepartureDate = fileRow.DepartureDate.Add(time);
                }

                if (!string.IsNullOrEmpty(fileRow.STALT)
                    && fileRow.STALT.Length > 2
                    && TimeSpan.TryParse(fileRow.STALT.Insert(2, ":"), out time))
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
        }

        /// <summary>
        /// Searches the delimite.
        /// </summary>
        /// <param name="colNamesLength">Length of the col names.</param>
        /// <param name="lines">The lines.</param>
        /// <param name="columnsLength">Length of the columns.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="i">The i.</param>
        private void searchDelimite(ref IDictionary<string, string> colNamesLength, List<string> lines, ref List<string> columnsLength, ref bool record, int i)
        {
            var delimiter = "---";

            if (lines[i].Contains(delimiter))
            {
                var lastHeader = lines[i - 1];
                var firstHeader = lines[i - 2];
                columnsLength = lines[i].Split(' ').ToList();
                if (columnsLength != null && columnsLength.Count > 0)
                {
                    record = true;
                    columnsLength.RemoveAt(0);
                    columnsLength.RemoveAt(columnsLength.Count - 1);
                    var initialPos = 0;
                    colNamesLength = new Dictionary<string, string>();
                    for (int j = 0; j < columnsLength.Count; j++)
                    {
                        var column = columnsLength[j].Length + 1;
                        var columnName = string.Empty;
                        columnName = firstHeader.Substring(initialPos, column).Trim();
                        columnName += lastHeader.Substring(initialPos, column).Trim();
                        columnName = columnName.Replace(" ", "");
                        var coords = initialPos.ToString() + " " + column.ToString();
                        colNamesLength.Add(columnName, coords);
                        initialPos += column;
                    }
                }
            }
        }

        /// <summary>
        /// Validates the file errors.
        /// </summary>
        /// <param name="itinerariesFile">The itineraries file.</param>
        /// <param name="itinerariesFileError">The itineraries file error.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="countErrors">The count errors.</param>
        /// <returns></returns>
        private int validateFileErrors(List<ItineraryFile> itinerariesFile, List<ItineraryFile> itinerariesFileError, List<string> errors, int countErrors)
        {
            //Validate actives records
            var validEquipmentNumbers = airplaneBusiness.GetActivesAirplane().Select(c => c.EquipmentNumber).ToList();
            var validStations = airportBusiness.GetActivesAirports().Select(c => c.StationCode).ToList();

            //string para informar errores
            string equipmentNumberText = Resource.EquipmentNumber;
            string departureAirportText = Resource.DepartureAirport;
            string arrivalAirportText = Resource.ArrivalAirport;
            string requiredText = Resource.RequiredField;
            string notFoundDBText = Resource.NotFoundDB;
            string lineText = Resource.Line;

            string fltNumText = Resource.FlightNumber;
            string airlineText = Resource.Airline;
            string departureDate = Resource.DepartureDate;
            string hourDeparture = Resource.DepartureTime;
            string hourArrival = Resource.ArriveTime;

            foreach (ItineraryFile item in itinerariesFile)
            {
                var exitsEquipment = validEquipmentNumbers.Contains(item.ACREGNUMBER);
                var exitsDepStation = validStations.Contains(item.DEP);
                var exitsDetStation = validStations.Contains(item.DST);

                //Validacion de campos no null or espacios
                Dictionary<int, string> fields;
                Dictionary<int, string> fieldsErrorMessenge;
                LoadDictionaries(equipmentNumberText, departureAirportText, arrivalAirportText, fltNumText, airlineText, departureDate, hourDeparture, hourArrival, item, out fields, out fieldsErrorMessenge);

                foreach (var stringItem in fields)
                {
                    if (string.IsNullOrEmpty(stringItem.Value))
                    {
                        errors.Add(fieldsErrorMessenge[stringItem.Key] + " " + requiredText + " " + lineText + item.Line);
                        countErrors++;
                    }
                }

                //Validacion de campos que deben estar en catalogos de la BD
                if (!exitsEquipment)
                {
                    errors.Add(equipmentNumberText + ": '" + item.ACREGNUMBER + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDepStation)
                {
                    errors.Add(departureAirportText + ": '" + item.DEP + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDetStation)
                {
                    errors.Add(arrivalAirportText + ": '" + item.DST + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de departure y arrival iguales
                if (item.DepartureStation == item.ArrivalStation)
                {
                    errors.Add(departureAirportText + "-" + arrivalAirportText + ": " + item.DepartureStation + "-" + item.ArrivalStation + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de vuelo con Z como ultimo caracter, se debe ignorar
                if (Right(item.FLTNUM, 1) == "Z")
                {
                    errors.Add(fltNumText + ": " + item.FLTNUM + " " + lineText + item.Line);
                    countErrors++;
                }

                //remove from collection
                if (countErrors > 0)
                {
                    itinerariesFileError.Add(item);
                }

                countErrors = 0;
            }

            return countErrors;
        }

        /// <summary>
        /// Loads the dictionaries.
        /// </summary>
        /// <param name="equipmentNumberText">The equipment number text.</param>
        /// <param name="departureAirportText">The departure airport text.</param>
        /// <param name="arrivalAirportText">The arrival airport text.</param>
        /// <param name="fltNumText">The FLT number text.</param>
        /// <param name="airlineText">The airline text.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="hourDeparture">The hour departure.</param>
        /// <param name="hourArrival">The hour arrival.</param>
        /// <param name="item">The item.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="fieldsErrorMessenge">The fields error messenge.</param>
        private void LoadDictionaries(string equipmentNumberText, string departureAirportText, string arrivalAirportText, string fltNumText, string airlineText, string departureDate, string hourDeparture, string hourArrival, ItineraryFile item, out Dictionary<int, string> fields, out Dictionary<int, string> fieldsErrorMessenge)
        {
            fields = new Dictionary<int, string>();
            fields.Add(1, item.FLTNUM);
            fields.Add(2, item.AirlineCode);
            fields.Add(3, item.ACREGNUMBER);
            fields.Add(4, item.FLTORGDATELT);
            fields.Add(5, item.DepartureStation);
            fields.Add(6, item.ArrivalStation);
            fields.Add(7, item.STDLT);
            fields.Add(8, item.STALT);

            fieldsErrorMessenge = new Dictionary<int, string>();
            fieldsErrorMessenge.Add(1, fltNumText);
            fieldsErrorMessenge.Add(2, airlineText);
            fieldsErrorMessenge.Add(3, equipmentNumberText);
            fieldsErrorMessenge.Add(4, departureDate);
            fieldsErrorMessenge.Add(5, departureAirportText);
            fieldsErrorMessenge.Add(6, arrivalAirportText);
            fieldsErrorMessenge.Add(7, hourDeparture);
            fieldsErrorMessenge.Add(8, hourArrival);
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
        #endregion
    }
}
