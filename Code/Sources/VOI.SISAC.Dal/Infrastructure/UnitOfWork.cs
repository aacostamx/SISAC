//------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    using System;
    using System.Data.SqlClient;
    using VOI.SISAC.Dal.ExceptionDal;
    /// <summary>
    /// Unit of Work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database factory
        /// </summary>
        private readonly IDbFactory factory;

        /// <summary>
        /// The database context
        /// </summary>
        private SisacContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="factory">The database factory.</param>
        public UnitOfWork(IDbFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public SisacContext DbContext
        {
            get { return this.context ?? (this.context = this.factory.Init()); }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            this.DbContext.Commit();            
        }
    }
}
