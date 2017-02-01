//-----------------------------------------------------------------------
// <copyright file="IDbFactory.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Database Factory Interface
    /// </summary>
    public interface IDbFactory : IDisposable
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Instance of SISACEntities.</returns>
        SisacContext Init();
    }
}
