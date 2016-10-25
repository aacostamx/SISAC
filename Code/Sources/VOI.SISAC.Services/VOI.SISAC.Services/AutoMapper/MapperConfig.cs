//------------------------------------------------------------------------
// <copyright file="MapperConfig.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.AutoMapper
{
    using global::AutoMapper;
    using Business.MapConfiguration;
    using Web.MapConfiguration;

    /// <summary>
    /// MapperConfig class
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(c =>
            {
                //Web Configuration
                c.AddProfile<AirportWebMaps>();
                c.AddProfile<CatalogWebMaps>();
                c.AddProfile<FinanceWebMaps>();
                c.AddProfile<ItineraryWebMaps>();
                c.AddProfile<SecurityWebMaps>();

                // Business Configuration
                c.AddProfile<AirportMaps>();
                c.AddProfile<CatalogMaps>();
                c.AddProfile<FinanceMaps>();
                c.AddProfile<ItinerariesMaps>();
                c.AddProfile<SecurityMaps>();

            });
        }
    }
}