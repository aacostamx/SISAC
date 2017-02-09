//------------------------------------------------------------------------
// <copyright file="MapperConfig.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.App_Start
{
    using AutoMapper;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Web.MapConfiguration;

    /// <summary>
    /// Class Automapper Configuration
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(c =>
            {
                // Web Configuration
                c.AddProfile<AirportWebMaps>();
                c.AddProfile<CatalogWebMaps>();
                c.AddProfile<FinanceWebMaps>();
                c.AddProfile<ItineraryWebMaps>();
                c.AddProfile<SecurityWebMaps>();
                c.AddProfile<ProcessWebMaps>();


                // Business Configuration
                c.AddProfile<AirportMaps>();
                c.AddProfile<CatalogMaps>();
                c.AddProfile<FinanceMaps>();
                c.AddProfile<ItinerariesMaps>();
                c.AddProfile<SecurityMaps>();
                c.AddProfile<ProcessMaps>();
            });
        }
    }
}
