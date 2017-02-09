//------------------------------------------------------------------------
// <copyright file="ItineraryController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.WinServices
{
    using System.ServiceProcess;

    /// <summary>
    /// Program class
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ItineraryUpload() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
