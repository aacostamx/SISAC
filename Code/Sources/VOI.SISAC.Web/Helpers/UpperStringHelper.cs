// <copyright file="UpperStringHelper.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using FileHelpers;

    /// <summary>
    /// UpperStringHelper
    /// </summary>
    public class UpperStringHelper : ConverterBase
    {
        /// <summary>
        /// StringToField
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public override object StringToField(string from)
        {
            if (string.IsNullOrEmpty(from))
            {
                return from;
            }

            return from.ToUpper();
        }
    }
}