//------------------------------------------------------------------------
// <copyright file="MapperConfig.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------


namespace VOI.SISAC.Services.App_Start
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using VOI.SISAC.Business.MapConfiguration;
   // using VOI.SISAC.Services.MapConfiguration;

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
                //c.AddProfile<AirportWebMaps>();
                //c.AddProfile<CatalogWebMaps>();
                //c.AddProfile<FinanceWebMaps>();
                //c.AddProfile<ItineraryWebMaps>();
                //c.AddProfile<SecurityWebMaps>();

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