//------------------------------------------------------------------------
// <copyright file="INationalFuelRateBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    public interface INationalFuelRateBusiness
    {
        /// <summary>
        /// Finds the national fuel rate by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The national fuel rate.
        /// </returns>
        NationalFuelRateDto FindNationalFuelRateById(long id);

        /// <summary>
        /// Uploads the National Fuel Rates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        IList<string> UploadNationalFuelRates(List<NationalFuelRateDto> rates);

        /// <summary>
        /// Deletes the National Fuel Rates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        bool DeleteNationalFuelRate(long id);

        /// <summary>
        /// Deletes the National Fuel Rates in the period given.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        int DeleteNationalFuelRate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the national fuel rates pagination.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national fuel rates.</returns>
        IList<NationalFuelRateDto> GetNationalFuelRatesPagination(int pageSize, int pageNumber);

        /// <summary>
        /// Counts all.
        /// </summary>
        /// <returns>Total of records.</returns>
        int CountAll();

        /// <summary>
        /// Updates the national fuel rate.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>true if was deleted else false.</returns>
        bool UpdateNationalFuelRate(NationalFuelRateDto rate);

        /// <summary>
        /// Search the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel rates.
        /// </returns>
        IList<NationalFuelRateDto> SearchNationalFuelRateByParameters(NationalFuelRateDto parameters, int pageSize, int pageNumber);

        /// <summary>
        /// Counts the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Number of records.
        /// </returns>
        int CountNationalFuelRateByParameters(NationalFuelRateDto parameters);
    }
}
