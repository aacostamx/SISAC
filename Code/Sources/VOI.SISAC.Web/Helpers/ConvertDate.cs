// <copyright file="ConvertDate.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// Class internal ConvertDate
    /// </summary>
    public class ConvertDate : ConverterBase
    {

        /// <summary>
        /// different forms for date separator : . or / or space
        /// </summary>
        /// <param name="from">the string format of date - first the day</param>
        /// <returns></returns>
        public override object StringToField(string from)
        {
            DateTime dt;

            if (DateTime.TryParseExact(from, "dd.MM.yyyy", null, DateTimeStyles.None, out dt))
            {
                return dt;
            }

            if (DateTime.TryParseExact(from, "dd/MM/yyyy", null, DateTimeStyles.None, out dt))
            { 
                return dt; 
            }

            if (DateTime.TryParseExact(from, "dd MM yyyy", null, DateTimeStyles.None, out dt))
            {
                return dt;
            }

            if (DateTime.TryParseExact(from, "dd-MM-yyyy", null, DateTimeStyles.None, out dt))
            {
                return dt;
            }

            if (DateTime.TryParseExact(from, "dd/MM/yyyy HH:mm", null, DateTimeStyles.None, out dt))
            {
                return dt;
            }

            throw new ArgumentException("Cannot make a date from " + from);

        }
    }
}