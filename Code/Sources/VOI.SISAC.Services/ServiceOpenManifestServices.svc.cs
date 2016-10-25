// -----------------------------------------------------------------------
// <copyright file="ServiceOpenManifestServices.cs" company="Volaris">
// Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
// -----------------------------------------------------------------------

namespace VOI.SISAC.Services
{
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Itineraries;

    /// <summary>
    /// ServiceOpenManifestServices
    /// </summary>
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ServiceOpenManifestServices : IServiceOpenManifestServices
    {
        private readonly IGendecArrivalBusiness gendecArrivalBusiness;
        private readonly IGendecDepartureBusiness gendecDepartureBusiness;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gendecArrivalBusiness"></param>
        public ServiceOpenManifestServices(IGendecArrivalBusiness gendecArrivalBusiness
                                           ,IGendecDepartureBusiness gendecDepartureBusiness)
        {
            this.gendecArrivalBusiness = gendecArrivalBusiness;
            this.gendecDepartureBusiness = gendecDepartureBusiness;
        }

        /// <summary>
        /// ServiceOpenManifestServices
        /// </summary>
        //public ServiceOpenManifestServices()
        //{
        //}

        /// <summary>
        /// OpenGendecArrival
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        public void OpenGendecArrival(string sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            int sequenceNumber;
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            if (int.TryParse(sequence, out sequenceNumber))
            {
                gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(sequenceNumber, airlinecode, flightnumber, itinerarykey);
            }
            

            this.gendecArrivalBusiness.OpenGendecArrival(gendecArrivalDto);
        }


        /// <summary>
        /// OpenGendecArrival
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        public void OpenGendecDeparture(string sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            int sequenceNumber;
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            if (int.TryParse(sequence, out sequenceNumber))
            {
                ///gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(sequenceNumber, airlinecode, flightnumber, itinerarykey);
                gendecDepartureDto.Sequence = sequenceNumber;
                gendecDepartureDto.AirlineCode = airlinecode;
                gendecDepartureDto.FlightNumber = flightnumber;
                gendecDepartureDto.Itinerarykey = itinerarykey;
            }


            this.gendecDepartureBusiness.OpenGendecDeparture(gendecDepartureDto);
        }

    }
}
