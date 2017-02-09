//------------------------------------------------------------------------
// <copyright file="EncryptHelper.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using Effortless.Net.Encryption;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Encrypt Helper Class
    /// </summary>
    public static class EncryptHelper
    {
        /// <summary>
        /// Key Encrypt 
        /// </summary>
        private const string key = "sisacy4v0l4ri$V0";

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(EncryptHelper));

        /// <summary>
        /// The class name
        /// </summary>
        private static readonly string className = "EncryptHelper";

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public static string EncryptString(string param)
        {
            string encrypted = string.Empty;

            byte[] keyArray = new byte[0];
            byte[] ivArray = new byte[32];

            try
            {
                keyArray = GetBytes(key);
                encrypted = Strings.Encrypt(param, keyArray, ivArray);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("EncryptString", className, null));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format("EncryptString", className, null));
                Trace.TraceError(ex.Message, ex);
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public static string DecryptString(string param)
        {
            string decrypted = string.Empty;
            byte[] keyArray = new byte[0];
            byte[] ivArray = new byte[32];

            try
            {
                keyArray = GetBytes(key);
                decrypted = Strings.Decrypt(param, keyArray, ivArray);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("DecryptString", className, null));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format("DecryptString", className, null));
                Trace.TraceError(ex.Message, ex);
            }

            return decrypted;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            try
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}