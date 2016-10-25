
namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;
    using VO.Airport;
    using VO.Catalog;

    /// <summary>
    /// Airport ViewModel
    /// </summary>
    public class AirportModelVO
    {
        /// <summary>
        /// contains AirportDTO
        /// </summary>
        public AirportVO AirportVO { get; set; }

        /// <summary>
        /// Contains List of CountryDto
        /// </summary>
        public IList<CountryVO> Countries { get; set; }

        /// <summary>
        /// Contains AirportGroups
        /// </summary>
        public IList<AirportGroupVO> AirportGroupCodes { get; set; }

        /// <summary>
        /// Constructor AirportModelVO
        /// </summary>
        public AirportModelVO()
        {
            AirportVO = new AirportVO();
            Countries = new List<CountryVO>();
            AirportGroupCodes = new List<AirportGroupVO>();
        }
    }
}