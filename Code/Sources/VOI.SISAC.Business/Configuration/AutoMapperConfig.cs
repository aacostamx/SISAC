//------------------------------------------------------------------------
// <copyright file="AutoMapperConfig.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Business.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using VOI.SISAC.Business.MapConfiguration;

    /// <summary>
    /// Class Automapper Configuration
    /// NOTE: This class is only when you use Automapper in the business
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile<AirportMaps>();
                c.AddProfile<CatalogMaps>();
                c.AddProfile<FinanceMaps>();
                c.AddProfile<ItinerariesMaps>();
                c.AddProfile<ProcessMaps>();
            });
        }
    }
}
