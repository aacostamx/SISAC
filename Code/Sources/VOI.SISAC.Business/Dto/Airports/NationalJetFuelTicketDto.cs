

namespace VOI.SISAC.Business.Dto.Airports
{
    using Finances;
    using Itineraries;
    using Security;
    using System;

    /// <summary>
    /// NationalJetFuelTicketDto Class
    /// </summary>
    public class NationalJetFuelTicketDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketDto"/> class.
        /// </summary>
        public NationalJetFuelTicketDto() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketDto"/> class.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        public NationalJetFuelTicketDto(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            this.Sequence = Sequence;
            this.AirlineCode = AirlineCode;
            this.FlightNumber = FlightNumber;
            this.ItineraryKey = ItineraryKey;
            this.OperationTypeName = OperationTypeName;
        }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        public long NationalJetFuelTicketID { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the fueling date start.
        /// </summary>
        /// <value>
        /// The fueling date start.
        /// </value>
        public DateTime FuelingDateStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling time start.
        /// </summary>
        /// <value>
        /// The fueling time start.
        /// </value>
        public TimeSpan FuelingTimeStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling date end.
        /// </summary>
        /// <value>
        /// The fueling date end.
        /// </value>
        public DateTime FuelingDateEnd { get; set; }

        /// <summary>
        /// Gets or sets the fueling time end.
        /// </summary>
        /// <value>
        /// The fueling time end.
        /// </value>
        public TimeSpan FuelingTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider number.
        /// </summary>
        /// <value>
        /// The jet fuel provider number.
        /// </value>
        public string JetFuelProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider number.
        /// </summary>
        /// <value>
        /// The into plane provider number.
        /// </value>
        public string IntoPlaneProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty LTS.
        /// </summary>
        /// <value>
        /// The fueled qty LTS.
        /// </value>
        public int FueledQtyLts { get; set; }

        /// <summary>
        /// Gets or sets the remaining qty KGS.
        /// </summary>
        /// <value>
        /// The remaining qty KGS.
        /// </value>
        public int? RemainingQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the requested qty KGS.
        /// </summary>
        /// <value>
        /// The requested qty KGS.
        /// </value>
        public int? RequestedQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty KGS.
        /// </summary>
        /// <value>
        /// The fueled qty KGS.
        /// </value>
        public int? FueledQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the density factor.
        /// </summary>
        /// <value>
        /// The density factor.
        /// </value>
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the aor user identifier.
        /// </summary>
        /// <value>
        /// The aor user identifier.
        /// </value>
        public int AorUserID { get; set; }

        /// <summary>
        /// Gets or sets the supplier responsible.
        /// </summary>
        /// <value>
        /// The supplier responsible.
        /// </value>
        public string SupplierResponsible { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryDto Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider.
        /// </summary>
        /// <value>
        /// The jet fuel provider.
        /// </value>
        public ProviderDto JetFuelProvider { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider.
        /// </summary>
        /// <value>
        /// The into plane provider.
        /// </value>
        public ProviderDto IntoPlaneProvider { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserDto User { get; set; }
    }
}
