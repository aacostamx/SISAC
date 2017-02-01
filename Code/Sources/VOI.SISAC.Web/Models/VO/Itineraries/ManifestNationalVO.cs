//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="Volaris">
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

    public class ManifestNationalVO
    {


        #region ~~~ Fields ~~~

        private string flightNumber;
        private string maticulaAeronave;
        private string departureStation;
        private string arrivalStation;
        private string departureDate;
        private int passengerAdultNumber;
        private int passengerChildrenNumber;
        private int passengerInfantNumber;
        private string tripulacionesID;
        private string sobrecargosID;
        private string sobrecargos;
        private int commandConf;
        private int stewarsConf;

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

        public int PassengerAdultNumber
        {
            get { return passengerAdultNumber; }
            set { passengerAdultNumber = value; }
        }

        public int PassengerChildrenNumber
        {
            get { return passengerChildrenNumber; }
            set { passengerChildrenNumber = value; }
        }

        public int PassengerInfantNumber
        {
            get { return passengerInfantNumber; }
            set { passengerInfantNumber = value; }
        }

        public String TripulacionesID
        {
            get { return tripulacionesID; }
            set { tripulacionesID = value; }
        }

        public String SobrecargosID
        {
            get { return sobrecargosID; }
            set { sobrecargosID = value; }
        }

        public String Sobrecargos
        {
            get { return sobrecargos; }
            set { sobrecargos = value; }
        }

        public int CommandConf
        {
            get { return commandConf; }
            set { commandConf = value; }
        }

        public int StewarsConf
        {
            get { return stewarsConf; }
            set { stewarsConf = value; }
        }

        #endregion ~~~ End of Atributes ~~~
    }
}