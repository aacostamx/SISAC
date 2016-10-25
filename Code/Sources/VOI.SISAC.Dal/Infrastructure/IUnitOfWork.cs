//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
