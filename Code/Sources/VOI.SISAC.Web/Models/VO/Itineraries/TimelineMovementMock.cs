

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;

    public class TimelineMovementMock
    {
        public int Sequence { get; set; }

        public string AirlineCode { get; set; }

        public string FlightNumber { get; set; }

        public string ItineraryKey { get; set; }

        public int OperationTypeID { get; set; }

        public string OperationDescription { get; set; }

        public string MovementTypeCode { get; set; }

        public string MovementTypeDescription { get; set; }

        public string MovementDate { get; set; }

        public string Position { get; set; }

        public string ProviderNumber { get; set; }

        public string ProviderName { get; set; }

        public decimal RemainingFuel { get; set; }

        public string Remarks { get; set; }
    }
}