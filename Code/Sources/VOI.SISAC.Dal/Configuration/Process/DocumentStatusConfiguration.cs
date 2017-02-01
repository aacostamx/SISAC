//------------------------------------------------------------------------
// <copyright file="DocumentStatusConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// DocumentStatusConfiguration Class
    /// </summary>
    public class DocumentStatusConfiguration : EntityTypeConfiguration<DocumentStatus>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentStatusConfiguration()
        {
            Property(e => e.DocumentStatusCode)
                .IsUnicode(false);

            Property(e => e.DocumentStatusName)
                .IsUnicode(false);
        }
    }
}
