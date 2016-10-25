//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Default operations.
    /// </summary>
    /// <typeparam name="TEntity">The Entity</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of Entities.</returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Marks the specified entity as new.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Marks the specified entity to be removed.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Marks the specified entity as modified.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Gets the server date.
        /// </summary>
        /// <returns>The current data base server date.</returns>
        DateTime GetServerDate();

        /// <summary>
        /// Counts the total of records.
        /// </summary>
        /// <returns>The toal of records</returns>
        int CountTotalRecords();

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
