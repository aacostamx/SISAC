//------------------------------------------------------------------------
// <copyright file="JsonDateConverter.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Web.Helpers
{
    using Newtonsoft.Json.Converters;
    using Resources;

    /// <summary>
    /// JsonDateConverter Class Utility
    /// </summary>
    public class JsonDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateConverter"/> class.
        /// </summary>
        public JsonDateConverter()
        {
            base.DateTimeFormat = Resource.DateTimeFormat;
        }
    }

    /// <summary>
    /// JsonTimeHourFormat Class Utility
    /// </summary>
    public class JsonTimeHourFormat : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTimeHourFormat"/> class.
        /// </summary>
        public JsonTimeHourFormat()
        {
            base.DateTimeFormat = Resource.TimeHourFormat;
        }
    }
}