
namespace VOI.SISAC.Services
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// IServiceOpenManifestServices
    /// </summary>
    [ServiceContract]
    public interface IServiceOpenManifestServices
    {
        /// <summary>
        /// OpenGendecArrival
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "openA/{sequence}/{airlinecode}/{flightnumber}/{itinerarykey}")]
        void OpenGendecArrival(string sequence, string airlinecode, string flightnumber, string itinerarykey);


        /// <summary>
        /// Open Gendec Departure
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itinerarykey"></param>
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "openD/{sequence}/{airlinecode}/{flightnumber}/{itinerarykey}")]
        void OpenGendecDeparture(string sequence, string airlinecode, string flightnumber, string itinerarykey);
    }
}
