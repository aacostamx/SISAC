//------------------------------------------------------------------------
// <copyright file="TaskScheduler.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.TaskScheduler
{
    using log4net;
    using Models;
    using RestSharp;
    using System;
    using System.Configuration;

    /// <summary>
    /// TaskScheduler class
    /// </summary>
    public class TaskScheduler
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(TaskScheduler));

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            logger.Info(string.Format("TaskScheduler iniciado {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
            var sucess = ConsumeWebAPI();
            logger.Info(string.Format("TaskScheduler finalizado {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
        }

        /// <summary>
        /// Consumes the web API.
        /// </summary>
        /// <returns></returns>
        private static bool ConsumeWebAPI()
        {
            logger.Info(string.Format("Inicia llamado SISAC API {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));

            var sucess = false;
            var itinerary = new ItineraryUpload()
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
                logger.Info(string.Format("Servicio finalizado {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                logger.Error("Error: " + ex.ToString());
            }

            return sucess;
        }
    }
}
