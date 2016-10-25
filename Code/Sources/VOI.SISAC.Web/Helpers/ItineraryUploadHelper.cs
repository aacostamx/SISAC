//------------------------------------------------------------------------
// <copyright file="ItineraryUploadHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Web.Helpers
{
    using Models.Files;
    using Models.VO.Itineraries;
    using Resources;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// ItineraryUploadHelper class
    /// </summary>
    public static class ItineraryUploadHelper
    {
        /// <summary>
        /// Streams to list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="lines">The lines.</param>
        public static void streamToList(ItineraryUploadVO input, List<string> lines)
        {
            using (StreamReader sr = new StreamReader(input.file.InputStream, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }
        }

        /// <summary>
        /// Searches the delimite.
        /// </summary>
        /// <param name="colNamesLength">Length of the col names.</param>
        /// <param name="lines">The lines.</param>
        /// <param name="columnsLength">Length of the columns.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="i">The i.</param>
        public static void searchDelimite(ref IDictionary<string, string> colNamesLength, List<string> lines, ref List<string> columnsLength, ref bool record, int i)
        {
            var delimiter = "---";

            if (lines[i].Contains(delimiter))
            {
                var lastHeader = lines[i - 1];
                var firstHeader = lines[i - 2];
                columnsLength = lines[i].Split(' ').ToList();
                if (columnsLength != null && columnsLength.Count > 0)
                {
                    record = true;
                    columnsLength.RemoveAt(0);
                    columnsLength.RemoveAt(columnsLength.Count - 1);
                    var initialPos = 0;
                    colNamesLength = new Dictionary<string, string>();
                    for (int j = 0; j < columnsLength.Count; j++)
                    {
                        var column = columnsLength[j].Length + 1;
                        var columnName = string.Empty;
                        columnName = firstHeader.Substring(initialPos, column).Trim();
                        columnName += lastHeader.Substring(initialPos, column).Trim();
                        columnName = columnName.Replace(" ", "");
                        var coords = initialPos.ToString() + " " + column.ToString();
                        colNamesLength.Add(columnName, coords);
                        initialPos += column;
                    }
                }
            }
        }

        public int validateFileErrors(List<ItineraryFile> itinerariesFile, List<ItineraryFile> itinerariesFileError, List<string> errors, int countErrors)
        {
            //Validate actives records
            var validEquipmentNumbers = airplaneBusiness.GetActivesAirplane().Select(c => c.EquipmentNumber).ToList();
            var validStations = airportBusiness.GetActivesAirports().Select(c => c.StationCode).ToList();

            //string para informar errores
            string equipmentNumberText = Resource.EquipmentNumber;
            string departureAirportText = Resource.DepartureAirport;
            string arrivalAirportText = Resource.ArrivalAirport;
            string requiredText = Resource.RequiredField;
            string notFoundDBText = Resource.NotFoundDB;
            string lineText = Resource.Line;

            string fltNumText = Resource.FlightNumber;
            string airlineText = Resource.Airline;
            string departureDate = Resource.DepartureDate;
            string hourDeparture = Resource.DepartureTime;
            string hourArrival = Resource.ArriveTime;

            foreach (ItineraryFile item in itinerariesFile)
            {
                var exitsEquipment = validEquipmentNumbers.Contains(item.ACREGNUMBER);
                var exitsDepStation = validStations.Contains(item.DEP);
                var exitsDetStation = validStations.Contains(item.DST);

                //Validacion de campos no null or espacios
                Dictionary<int, string> fields;
                Dictionary<int, string> fieldsErrorMessenge;
                LoadDictionaries(equipmentNumberText, departureAirportText, arrivalAirportText, fltNumText, airlineText, departureDate, hourDeparture, hourArrival, item, out fields, out fieldsErrorMessenge);

                foreach (var stringItem in fields)
                {
                    if (string.IsNullOrEmpty(stringItem.Value))
                    {
                        errors.Add(fieldsErrorMessenge[stringItem.Key] + " " + requiredText + " " + lineText + item.Line);
                        countErrors++;
                    }
                }

                //Validacion de campos que deben estar en catalogos de la BD
                if (!exitsEquipment)
                {
                    errors.Add(equipmentNumberText + ": '" + item.ACREGNUMBER + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDepStation)
                {
                    errors.Add(departureAirportText + ": '" + item.DEP + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDetStation)
                {
                    errors.Add(arrivalAirportText + ": '" + item.DST + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de departure y arrival iguales
                if (item.DepartureStation == item.ArrivalStation)
                {
                    errors.Add(departureAirportText + "-" + arrivalAirportText + ": " + item.DepartureStation + "-" + item.ArrivalStation + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de vuelo con Z como ultimo caracter, se debe ignorar
                if (Right(item.FLTNUM, 1) == "Z")
                {
                    //errors.Add(fltNumText + ": " + item.FLTNUM + " " + lineText + item.Line);
                    countErrors++;
                }

                //remove from collection
                if (countErrors > 0)
                {
                    itinerariesFileError.Add(item);
                }

                countErrors = 0;
            }

            return countErrors;
        }

    }
}