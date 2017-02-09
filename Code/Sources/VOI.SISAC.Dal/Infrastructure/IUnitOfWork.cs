//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    /// <summary>
    /// Unit ok Work Interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();
    }
}
