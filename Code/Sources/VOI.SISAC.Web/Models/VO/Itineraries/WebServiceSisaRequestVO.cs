//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    #region ~~~ Usings ~~~

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion ~~~ End of Usings ~~~


    public class WebServiceSisaRequestVO
    {
        #region ~~~ Fields ~~~

        private string flightNumber;
        private string maticulaAeronave;
        private string departureStation;
        private string arrivalStation;
        private string departureDate;

        //private List<ElapsedTime> elapsedTimes;

        #endregion ~~~ End of Fields ~~~

        #region ~~~ Atributes ~~~

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        public string DepartureStation
        {
            get { return departureStation; }
            set { departureStation = value; }
        }

        public string ArrivalStation
        {
            get { return arrivalStation; }
            set { arrivalStation = value; }
        }

        public String DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }

        public String MaticulaAeronave
        {
            get { return maticulaAeronave; }
            set { maticulaAeronave = value; }
        }

        #endregion ~~~ End of Atributes ~~~
    }
}