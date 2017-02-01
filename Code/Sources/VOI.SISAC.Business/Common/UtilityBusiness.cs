//------------------------------------------------------------------------
// <copyright file="UtilityBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Common
{
    using Entities.Itineraries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Itineraries;

    /// <summary>
    /// Static Class Utility Business
    /// </summary>
    public static class UtilityBusiness
    {
        /// <summary>
        /// AddSequenceItineraryDto
        /// </summary>
        /// <param name="itieraryDto">List of itineries</param>
        /// <returns>orden list of itineries</returns>
        public static IList<ItineraryDto> AddSequenceItineraryDto(IList<ItineraryDto> itieraryDto)
        {
            var itineraryFileSequence = itieraryDto
                .OrderBy(x => x.ItineraryKey)
                .ThenBy(x => x.FlightNumber).ThenBy(x => x.EquipmentNumber).ThenBy(x => x.DepartureDate)
                .GroupBy(x => new { x.ItineraryKey, x.FlightNumber, x.EquipmentNumber })
                .Select(group => new { Group = group, Count = group.Count() })
                .SelectMany(groupWithCount =>
                    groupWithCount.Group.Select(b => b)
                    .Zip(
                        Enumerable.Range(1, groupWithCount.Count),
                        (j, i) => new
                        {
                            j.AirlineCode,
                            j.FlightNumber,
                            j.ItineraryKey,
                            j.EquipmentNumber,
                            j.DepartureDate,
                            j.DepartureStation,
                            j.ArrivalDate,
                            j.ArrivalStation,
                            RowNumber = i
                        }
                    )
                );

            itieraryDto = null;
            itieraryDto = itineraryFileSequence.Select(item => new ItineraryDto()
            {
                AirlineCode = item.AirlineCode,
                FlightNumber = item.FlightNumber,
                ItineraryKey = item.ItineraryKey,
                EquipmentNumber = item.EquipmentNumber,
                DepartureDate = item.DepartureDate,
                DepartureStation = item.DepartureStation,
                ArrivalDate = item.ArrivalDate,
                ArrivalStation = item.ArrivalStation,
                Sequence = item.RowNumber
            }).ToList<ItineraryDto>();

            return itieraryDto;
        }


        /// <summary>
        /// Add Sequence to Itinerary Entity
        /// </summary>
        /// <param name="itierary">List of itineries</param>
        /// <returns>orden list of itineries</returns>
        public static IList<Itinerary> AddSequenceItinerary(IList<Itinerary> itierary)
        {

            try
            {
                var itineraryFileSequence = itierary
            .OrderBy(x => x.ItineraryKey)
            .ThenBy(x => x.FlightNumber).ThenBy(x => x.DepartureDate)
            .GroupBy(x => new { x.ItineraryKey, x.FlightNumber })
            .Select(group => new { Group = group, Count = group.Count() })
            .SelectMany(groupWithCount =>
                groupWithCount.Group.Select(b => b)
                .Zip(
                    Enumerable.Range(1, groupWithCount.Count),
                    (j, i) => new
                    {
                        j.AirlineCode,
                        j.FlightNumber,
                        j.ItineraryKey,
                        j.EquipmentNumber,
                        j.DepartureDate,
                        j.DepartureStation,
                        j.ArrivalDate,
                        j.ArrivalStation,
                        RowNumber = i,
                        j.Add
                    }
                )
            );

                itierary = null;
                itierary = itineraryFileSequence.Select(item => new Itinerary()
                {
                    AirlineCode = item.AirlineCode,
                    FlightNumber = item.FlightNumber,
                    ItineraryKey = item.ItineraryKey,
                    EquipmentNumber = item.EquipmentNumber,
                    DepartureDate = item.DepartureDate,
                    DepartureStation = item.DepartureStation,
                    ArrivalDate = item.ArrivalDate,
                    ArrivalStation = item.ArrivalStation,
                    Sequence = item.RowNumber,
                    Add = item.Add
                }).ToList<Itinerary>();

                return itierary;
            }
            catch (Exception)
            {
                IList<Itinerary> error = new List<Itinerary>();
                return error;
            }
        }
    }
}
