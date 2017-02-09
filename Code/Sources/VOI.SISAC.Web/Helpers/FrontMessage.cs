//------------------------------------------------------------------------
// <copyright file="FrontMessage.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{
    /// <summary>
    /// Provides messages to display in the application.
    /// </summary>
    public static class FrontMessage
    {
        /// <summary>
        /// Give me my message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The corresponding message.</returns>
        public static string GetExceptionErrorMessage(int code = 0)
        {
            switch (code)
            {
                // Code for Contracts 
                case 5:
                    return "Elements in the file must have the same information by contract";

                // Code for duplicated primary key error.
                case 10:
                    return "There is already a record with the same {0}.";

                // Code for value duplicated error.
                case 11:
                    return "Cannot exist the same value {0}.";

                // Date greater than error.
                case 12:
                    return "The date must be greater than the {0}.";

                case 13:
                    return "Select a value from the list.";

                // Not sequence airport.
                case 20:
                    return Resources.Resource.LastSameFlight.ToString();

                // Not valid date of departure.
                case 21:
                    return Web.Resources.Resource.DepartureDateGreater.ToString();

                // Not valid date of both of them items.
                case 22:
                    return Resources.Resource.ArrivalDateSmaller.ToString();

                // Not valid date of departure.
                case 23:
                    return Resources.Resource.LastDelete.ToString();

                // Calculation process running
                case 24:
                    return Resources.Resource.ProcessRunningError;

                // Calculation process running
                case 25:
                    return Resources.Resource.TimesSchedule;

                // If there is any matches.
                default:
                    return "There was an error while performing this operation, please try again. If the problem persist, contact the administrator.";
            }
        }
    }
}