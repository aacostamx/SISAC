//------------------------------------------------------------------------
// <copyright file="DbFactory.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Database Factory
    /// </summary>
    public class DbFactory : Disposable, IDbFactory
    {
        /// <summary>
        /// The database context
        /// </summary>
        private SisacContext context;

        /// <summary>
        /// Initializes Database Context.
        /// </summary>
        /// <returns>Instance of <see cref="SisacContext"/>.</returns>
        public SisacContext Init()
        {
            return this.context ?? (this.context = new SisacContext());
        }

        /// <summary>
        /// Override this to dispose custom objects
        /// </summary>
        protected override void DisposeCore()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }
    }
}
