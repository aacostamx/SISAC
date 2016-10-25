using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VOI.SISAC.Services.Models.Itinerary
{
    [DataContract]
    public class GendecServiceModel
    {
        [DataMember]
        public int sequence;

        [DataMember]
        public string airlinecode;

        [DataMember]
        public string flightnumber;

        [DataMember]
        public string itinerarykey;
    }
}