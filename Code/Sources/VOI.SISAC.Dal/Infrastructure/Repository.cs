//------------------------------------------------------------------------
// <copyright file="Repository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Repository with the basic operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class Repository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly IDbSet<TEntity> dbSet;

        /// <summary>
        /// The data context.
        /// </summary>
        private SisacContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        protected Repository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = this.DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets the database factory.
        /// </summary>
        /// <value>
        /// The database factory.
        /// </value>
        protected IDbFactory DbFactory { get; private set; }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected SisacContext DbContext
        {
            get { return this.dataContext ?? (this.dataContext = this.DbFactory.Init()); }
        }

        /// <summary>
        /// Adds a new record into the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public virtual void Update(TEntity entity)
        {
            this.dbSet.Attach(entity);
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        /// <summary>
        /// Gets all entity's records.
        /// </summary>
        /// <returns>All the entity's records.</returns>
        public virtual IList<TEntity> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Finds the list.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="tracking">if set to <c>true</c> [tracking].</param>
        /// <returns></returns>
        public virtual IList<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
        {
            var find = new List<TEntity>();

            if (tracking)
            {
                find = this.dbSet.Where(predicate).ToList();
            }
            else
            {
                find = this.dbSet.AsNoTracking().Where(predicate).ToList();
            }

            return find;
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="tracking">if set to <c>true</c> [tracking].</param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
        {
            if (tracking)
            {
                return this.dbSet.Where(predicate).FirstOrDefault();
            }
            else
            {
                return this.dbSet.AsNoTracking().Where(predicate).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the server date.
        /// </summary>
        /// <returns>The current data base server date.</returns>
        public virtual DateTime GetServerDate()
        {
            return this.DbContext.Database.SqlQuery<DateTime>("SELECT (GETDATE())").FirstOrDefault();
        }

        /// <summary>
        /// Counts the total of records.
        /// </summary>
        /// <returns>The toal of records</returns>
        public virtual int CountTotalRecords()
        {
            return this.dbSet.Count();
        }
    }
}
