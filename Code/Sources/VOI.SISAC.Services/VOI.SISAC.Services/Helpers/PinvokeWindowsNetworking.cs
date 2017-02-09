//------------------------------------------------------------------------
// <copyright file="PinvokeWindowsNetworking.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// PinvokeWindowsNetworking class
    /// </summary>
    public class PinvokeWindowsNetworking
    {
        #region Consts        
        /// <summary>
        /// The resource connected
        /// </summary>
        const int RESOURCE_CONNECTED = 0x00000001;
        /// <summary>
        /// The resource globalnet
        /// </summary>
        const int RESOURCE_GLOBALNET = 0x00000002;
        /// <summary>
        /// The resource remembered
        /// </summary>
        const int RESOURCE_REMEMBERED = 0x00000003;

        /// <summary>
        /// The resourcetype any
        /// </summary>
        const int RESOURCETYPE_ANY = 0x00000000;
        /// <summary>
        /// The resourcetype disk
        /// </summary>
        const int RESOURCETYPE_DISK = 0x00000001;
        /// <summary>
        /// The resourcetype print
        /// </summary>
        const int RESOURCETYPE_PRINT = 0x00000002;

        /// <summary>
        /// The resourcedisplaytype generic
        /// </summary>
        const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
        /// <summary>
        /// The resourcedisplaytype domain
        /// </summary>
        const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
        /// <summary>
        /// The resourcedisplaytype server
        /// </summary>
        const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
        /// <summary>
        /// The resourcedisplaytype share
        /// </summary>
        const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
        /// <summary>
        /// The resourcedisplaytype file
        /// </summary>
        const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
        /// <summary>
        /// The resourcedisplaytype group
        /// </summary>
        const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

        /// <summary>
        /// The resourceusage connectable
        /// </summary>
        const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
        /// <summary>
        /// The resourceusage container
        /// </summary>
        const int RESOURCEUSAGE_CONTAINER = 0x00000002;

        /// <summary>
        /// The connect interactive
        /// </summary>
        const int CONNECT_INTERACTIVE = 0x00000008;
        /// <summary>
        /// The connect prompt
        /// </summary>
        const int CONNECT_PROMPT = 0x00000010;
        /// <summary>
        /// The connect redirect
        /// </summary>
        const int CONNECT_REDIRECT = 0x00000080;
        /// <summary>
        /// The connect update profile
        /// </summary>
        const int CONNECT_UPDATE_PROFILE = 0x00000001;
        /// <summary>
        /// The connect commandline
        /// </summary>
        const int CONNECT_COMMANDLINE = 0x00000800;
        /// <summary>
        /// The connect command savecred
        /// </summary>
        const int CONNECT_CMD_SAVECRED = 0x00001000;
        /// <summary>
        /// The connect localdrive
        /// </summary>
        const int CONNECT_LOCALDRIVE = 0x00000100;
        #endregion

        #region Errors        
        /// <summary>
        /// The no error
        /// </summary>
        const int NO_ERROR = 0;

        /// <summary>
        /// The error access denied
        /// </summary>
        const int ERROR_ACCESS_DENIED = 5;
        /// <summary>
        /// The error already assigned
        /// </summary>
        const int ERROR_ALREADY_ASSIGNED = 85;
        /// <summary>
        /// The error bad device
        /// </summary>
        const int ERROR_BAD_DEVICE = 1200;
        /// <summary>
        /// The error bad net name
        /// </summary>
        const int ERROR_BAD_NET_NAME = 67;
        /// <summary>
        /// The error bad provider
        /// </summary>
        const int ERROR_BAD_PROVIDER = 1204;
        /// <summary>
        /// The error cancelled
        /// </summary>
        const int ERROR_CANCELLED = 1223;
        /// <summary>
        /// The error extended error
        /// </summary>
        const int ERROR_EXTENDED_ERROR = 1208;
        /// <summary>
        /// The error invalid address
        /// </summary>
        const int ERROR_INVALID_ADDRESS = 487;
        /// <summary>
        /// The error invalid parameter
        /// </summary>
        const int ERROR_INVALID_PARAMETER = 87;
        /// <summary>
        /// The error invalid password
        /// </summary>
        const int ERROR_INVALID_PASSWORD = 1216;
        /// <summary>
        /// The error more data
        /// </summary>
        const int ERROR_MORE_DATA = 234;
        /// <summary>
        /// The error no more items
        /// </summary>
        const int ERROR_NO_MORE_ITEMS = 259;
        /// <summary>
        /// The error no net or bad path
        /// </summary>
        const int ERROR_NO_NET_OR_BAD_PATH = 1203;
        /// <summary>
        /// The error no network
        /// </summary>
        const int ERROR_NO_NETWORK = 1222;

        /// <summary>
        /// The error bad profile
        /// </summary>
        const int ERROR_BAD_PROFILE = 1206;
        /// <summary>
        /// The error cannot open profile
        /// </summary>
        const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        /// <summary>
        /// The error device in use
        /// </summary>
        const int ERROR_DEVICE_IN_USE = 2404;
        /// <summary>
        /// The error not connected
        /// </summary>
        const int ERROR_NOT_CONNECTED = 2250;
        /// <summary>
        /// The error open files
        /// </summary>
        const int ERROR_OPEN_FILES = 2401;
        /// <summary>
        /// The error path file
        /// </summary>
        const int ERROR_PATH_FILE = 53;
        /// <summary>
        /// The error multiple connections
        /// </summary>
        const int ERROR_MULTIPLE_CONNECTIONS = 1219;

        /// <summary>
        /// Error Class
        /// </summary>
        private struct ErrorClass
        {
            public int num;
            public string message;
            public ErrorClass(int num, string message)
            {
                this.num = num;
                this.message = message;
            }
        }


        // Created with excel formula:
        // ="new ErrorClass("&A1&", """&PROPER(SUBSTITUTE(MID(A1,7,LEN(A1)-6), "_", " "))&"""), "
        private static ErrorClass[] ERROR_LIST = new ErrorClass[] {
            new ErrorClass(ERROR_ACCESS_DENIED, "Error: Access Denied"),
            new ErrorClass(ERROR_ALREADY_ASSIGNED, "Error: Already Assigned"),
            new ErrorClass(ERROR_BAD_DEVICE, "Error: Bad Device"),
            new ErrorClass(ERROR_BAD_NET_NAME, "Error: Bad Net Name"),
            new ErrorClass(ERROR_BAD_PROVIDER, "Error: Bad Provider"),
            new ErrorClass(ERROR_CANCELLED, "Error: Cancelled"),
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
            new ErrorClass(ERROR_INVALID_ADDRESS, "Error: Invalid Address"),
            new ErrorClass(ERROR_INVALID_PARAMETER, "Error: Invalid Parameter"),
            new ErrorClass(ERROR_INVALID_PASSWORD, "Error: Invalid Password"),
            new ErrorClass(ERROR_MORE_DATA, "Error: More Data"),
            new ErrorClass(ERROR_NO_MORE_ITEMS, "Error: No More Items"),
            new ErrorClass(ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path"),
            new ErrorClass(ERROR_NO_NETWORK, "Error: No Network"),
            new ErrorClass(ERROR_BAD_PROFILE, "Error: Bad Profile"),
            new ErrorClass(ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile"),
            new ErrorClass(ERROR_DEVICE_IN_USE, "Error: Device In Use"),
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
            new ErrorClass(ERROR_NOT_CONNECTED, "Error: Not Connected"),
            new ErrorClass(ERROR_OPEN_FILES, "Error: Open Files"),
            new ErrorClass(ERROR_PATH_FILE, "Error: Path File"),
            new ErrorClass(ERROR_MULTIPLE_CONNECTIONS, "Error: Multiple Connections"),
        };

        private static string getErrorForNumber(int errNum)
        {
            foreach (ErrorClass er in ERROR_LIST)
            {
                if (er.num == errNum) return er.message;
            }
            return "Error: Unknown, " + errNum;
        }
        #endregion

        /// <summary>
        /// ws the net use connection.
        /// </summary>
        /// <param name="hwndOwner">The HWND owner.</param>
        /// <param name="lpNetResource">The lp net resource.</param>
        /// <param name="lpPassword">The lp password.</param>
        /// <param name="lpUserID">The lp user identifier.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="lpAccessName">Name of the lp access.</param>
        /// <param name="lpBufferSize">Size of the lp buffer.</param>
        /// <param name="lpResult">The lp result.</param>
        /// <returns></returns>
        [DllImport("Mpr.dll")]
        private static extern int WNetUseConnection(
            IntPtr hwndOwner,
            NETRESOURCE lpNetResource,
            string lpPassword,
            string lpUserID,
            int dwFlags,
            string lpAccessName,
            string lpBufferSize,
            string lpResult
        );

        /// <summary>
        /// ws the net cancel connection2.
        /// </summary>
        /// <param name="lpName">Name of the lp.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="fForce">if set to <c>true</c> [f force].</param>
        /// <returns></returns>
        [DllImport("Mpr.dll")]
        private static extern int WNetCancelConnection2(
            string lpName,
            int dwFlags,
            bool fForce
        );

        /// <summary>
        /// NETRESOURCE class
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public int dwScope = 0;
            public int dwType = 0;
            public int dwDisplayType = 0;
            public int dwUsage = 0;
            public string lpLocalName = "";
            public string lpRemoteName = "";
            public string lpComment = "";
            public string lpProvider = "";
        }

        /// <summary>
        /// Connects to remote.
        /// </summary>
        /// <param name="remoteUNC">The remote unc.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string connectToRemote(string remoteUNC, string username, string password)
        {
            return connectToRemote(remoteUNC, username, password, false);
        }

        /// <summary>
        /// Connects to remote.
        /// </summary>
        /// <param name="remoteUNC">The remote unc.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="promptUser">if set to <c>true</c> [prompt user].</param>
        /// <returns></returns>
        public static string connectToRemote(string remoteUNC, string username, string password, bool promptUser)
        {
            NETRESOURCE nr = new NETRESOURCE();
            nr.dwType = RESOURCETYPE_DISK;
            nr.lpRemoteName = remoteUNC;

            int ret;
            if (promptUser)
                ret = WNetUseConnection(IntPtr.Zero, nr, "", "", CONNECT_INTERACTIVE | CONNECT_PROMPT, null, null, null);
            else
                ret = WNetUseConnection(IntPtr.Zero, nr, password, username, 0, null, null, null);

            if (ret == NO_ERROR) return null;
            return getErrorForNumber(ret);
        }

        /// <summary>
        /// Disconnects the remote.
        /// </summary>
        /// <param name="remoteUNC">The remote unc.</param>
        /// <returns></returns>
        public static string disconnectRemote(string remoteUNC)
        {
            int ret = WNetCancelConnection2(remoteUNC, CONNECT_UPDATE_PROFILE, false);
            if (ret == NO_ERROR) return null;
            return getErrorForNumber(ret);
        }
    }
}