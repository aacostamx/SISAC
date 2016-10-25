//------------------------------------------------------------------------
// <copyright file="IGenericNotificationBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Common
{
    using Dto.Itineraries;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Generic;


    /// <summary>
    /// IGenericNotificationBusiness
    /// </summary>
    public interface IGenericNotificationBusiness
    {
        /// <summary>
        /// Sendings the email.
        /// </summary>
        /// <param name="myModel">My model.</param>
        /// <returns></returns>
        IList<string> SendingEmail(MailModelDto myModel);

        /// <summary>
        /// Mails the configuration itinerary.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        string MailConfigurationItinerary(ItineraryMailDto mail);
    }
}
