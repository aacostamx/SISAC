//------------------------------------------------------------------------
// <copyright file="CompressHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Class for compress and decompress information
    /// </summary>
    public static class CompressHelper
    {
        /// <summary>
        /// Compress a string to bytes.
        /// </summary>
        /// <param name="stringToCompress">The string to compress.</param>
        /// <returns>Bytes compressed.</returns>
        public static byte[] Compress(string stringToCompress)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringToCompress);
            return CompressBytes(bytes);
        }

        /// <summary>
        /// Compress a string.
        /// </summary>
        /// <param name="stringToCompress">The string to compress.</param>
        /// <returns>Bytes compressed.</returns>
        public static string CompressToString(string stringToCompress)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringToCompress);
            return Encoding.Default.GetString(CompressBytes(bytes));
        }

        /// <summary>
        /// Decompress bytes to string.
        /// </summary>
        /// <param name="bytes">Bytes to decompress.</param>
        /// <returns>The string decompressed.</returns>
        public static string Decompress(byte[] bytes)
        {
            return Encoding.UTF8.GetString(DecompressBytes(bytes));
        }

        /// <summary>
        /// Decompress a string.
        /// </summary>
        /// <param name="bytes">Bytes to decompress.</param>
        /// <returns>The string decompressed.</returns>
        public static string Decompress(string stringToDecompress)
        {
            byte[] bytes = Encoding.Default.GetBytes(stringToDecompress);
            return Encoding.UTF8.GetString(DecompressBytes(bytes));
        }

        /// <summary>
        /// Method for compress a bytes array.
        /// </summary>
        /// <param name="stringToCompress">The bytes to compress.</param>
        /// <returns>Bytes compressed.</returns>
        private static byte[] CompressBytes(byte[] bytes)
        {
            using (MemoryStream msi = new MemoryStream(bytes))
            {
                using (MemoryStream mso = new MemoryStream())
                {
                    using (GZipStream gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        msi.CopyTo(gs);
                    }

                    return mso.ToArray();
                }
            }
        }

        /// <summary>
        /// Decompress bytes.
        /// </summary>
        /// <param name="bytes">Bytes to decompress.</param>
        /// <returns>Bytes decompressed.</returns>
        private static byte[] DecompressBytes(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        gs.CopyTo(mso);
                    }

                    return mso.ToArray();
                }
            }
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <remarks>
        /// If the .NET Framework is 3.5 or below use this method instead of MemoryStream's CopyTo() method.
        /// </remarks>
        private static void CopyTo(Stream source, Stream destination)
        {
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = source.Read(bytes, 0, bytes.Length)) != 0)
            {
                destination.Write(bytes, 0, cnt);
            }
        }
    }
}