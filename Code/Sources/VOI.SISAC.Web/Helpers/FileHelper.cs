//------------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using System;
    using System.IO;

    /// <summary>
    /// Class to handle files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Gets the plain text file.
        /// </summary>
        /// <param name="pathFile">The path file.</param>
        /// <returns>The file in a byte array.</returns>
        public static byte[] GetPlainTextFile(string pathFile)
        {
            try
            {
                byte[] data;
                using (FileStream fileStream = System.IO.File.Open(pathFile, FileMode.Open, FileAccess.Read))
                {
                    data = new byte[fileStream.Length];
                    int br = fileStream.Read(data, 0, data.Length);
                }

                return data;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}