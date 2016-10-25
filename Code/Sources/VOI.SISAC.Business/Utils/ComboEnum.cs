//------------------------------------------------------------------------
// <copyright file="EnumSisac.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Utils
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class Enum Sisac
    /// </summary>
    public static class EnumSisac
    {
        /// <summary>
        /// Enum GENDER
        /// </summary>
        public enum GENDER : int
        {
            /// <summary>
            /// Description for Gender 'F'
            /// </summary>
            [Description("F")] 
             FEMENINO = 0,

            /// <summary>
            /// Description for Gender 'M'
            /// </summary>
            [Description("M")] 
             MASCULINO = 1
        }

        /// <summary>         
        /// Obtiene la propiedad de descripción del enumerador.         
        /// </summary>         
        /// <param name="value">Enum</param>         
        /// <returns>string Enum</returns>         
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}