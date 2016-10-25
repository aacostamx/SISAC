//------------------------------------------------------------------------
// <copyright file="AuthenticateUserHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    using Resources;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Authenticate User Helper
    /// </summary>
    public static class AuthenticateUserHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AuthenticateUserHelper));

        /// <summary>
        /// Catalog name
        /// </summary>
        private static readonly string className = "AuthenticateUserHelper";

        /// <summary>
        /// Is Domain User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool isDomainUser(string userName, string pass)
        {
            bool domainUser = false;
            VolarisAuthenticate.ADWS auth = new VolarisAuthenticate.ADWS();
            VolarisAuthenticate.AD_Response response = new VolarisAuthenticate.AD_Response();
            try
            {
                response = auth.ValidateUser(userName, pass);
                if (!string.IsNullOrEmpty(response.ResponseCode))
                {
                    domainUser = response.ResponseCode.Equals("0") ? true : false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, className, userName));
                Logger.Error(ex.Message, ex);
                Logger.Error("WebService response:" + response.ErrorMessage);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, className, userName));
                Trace.TraceError(ex.Message, ex);
                Trace.TraceError("WebService response:" + response.ErrorMessage);
            }
            return domainUser;
        }
    }
}