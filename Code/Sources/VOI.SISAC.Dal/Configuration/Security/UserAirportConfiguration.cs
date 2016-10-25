
namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    public class UserAirportConfiguration : EntityTypeConfiguration<UserAirport>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAirportConfiguration"/> class.
        /// </summary>
        public UserAirportConfiguration()
        {
            // Relationships

            this.Property(e => e.StationCode)
                .IsUnicode(false);

        }
    }
}
