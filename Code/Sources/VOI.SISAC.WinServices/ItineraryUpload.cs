//------------------------------------------------------------------------
// <copyright file="ItineraryUpload.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.WinServices
{
    using log4net;
    using Models;
    using RestSharp;
    using System;
    using System.Configuration;
    using System.IO;
    using System.ServiceProcess;
    using System.Text;
    using System.Timers;


    /// <summary>
    /// ItineraryUpload class
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    partial class ItineraryUpload : ServiceBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(ItineraryUpload));

        /// <summary>
        /// a timer
        /// </summary>
        private static Timer aTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryUpload"/> class.
        /// </summary>
        public ItineraryUpload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            logger.Info(string.Format("Servicio iniciado {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));

            try
            {
                //Default 30 min
                var timerInterval = 1800000d;

                logger.Info(string.Format("Inicia llamado SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                var sucess = ConsumeWebAPI();
                logger.Info(string.Format("Finaliza llamado SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));

                // Create a timer
                aTimer = new Timer();

                // Hook up the Elapsed event for the timer.
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

                // Set the Interval
                double.TryParse(ConfigurationManager.AppSettings["TimerInterval"], out timerInterval);
                logger.Info(string.Format("Set Interval: ", timerInterval));
                aTimer.Interval = timerInterval;
                aTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                logger.Error("Error: " + ex.ToString());
            }
        }

        /// <summary>
        /// Called when [timed event].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            logger.Info(string.Format("Inicia llamado SISAC API - OnTimedEvent {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
            var sucess = ConsumeWebAPI();
            logger.Info(string.Format("Finaliza llamado SISAC API - OnTimedEvent {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
        }

        /// <summary>
        /// Consumes the web API.
        /// </summary>
        /// <returns></returns>
        private static bool ConsumeWebAPI()
        {
            var sucess = false;
            var itinerary = new ItineraryUploadVO()
            {
                StartDate = DateTime.Now.AddDays(-1).Date,
                EndDate = DateTime.Now.AddDays(1).Date,
                AirlineCodeCombobox = "Y4"
            };

            try
            {
                var Url = ConfigurationManager.AppSettings["Url"];
                var Action = ConfigurationManager.AppSettings["Action"];
                var restClient = new RestClient(Url);
                var request = new RestRequest(Action, Method.POST);
                request.AddJsonBody(itinerary);
                var response = restClient.Execute(request);
                sucess = response != null && response.StatusCode.ToString() == "OK" ? true : false;
                if (!sucess)
                {
                    logger.Info(string.Format("Hubo un problema al intentar conectarse a la API SISAC - api/UploadItinerary {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                    logger.Info(string.Format(sucess.ToString()));
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                logger.Error("Error: " + ex.ToString());
            }

            return sucess;
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns></returns>
        private static string readFile()
        {
            var text = string.Empty;

            try
            {
                logger.Info("Inicializando Lectura del archivo Y4GENREP.TXT...");
                var fileStream = new FileStream(@"\\10.10.32.131\\ItinerariosSISA\\Y4GENREP.TXT", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd();
                }
                logger.Info(text);
                logger.Info("Finaliza Lectura del archivo Y4GENREP.TXT...");
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                logger.Error("Error: " + ex.ToString());
            }

            return text;
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            logger.Info(string.Format("Servicio detenido {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
        }
    }
}
