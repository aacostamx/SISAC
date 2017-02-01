//------------------------------------------------------------------------
// <copyright file="EmptyStringConverter.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using FileHelpers;
    using System;

    /// <summary>
    /// EmptyStringConverter 
    /// </summary>
    public class EmptyStringConverter : ConverterBase
    {
        /// <summary>
        /// StringToField
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public override object StringToField(string sourceString)
        {
            if (String.IsNullOrWhiteSpace(sourceString))
            {
                return null;
            }

            return sourceString.ToUpper();
        }
    }
}