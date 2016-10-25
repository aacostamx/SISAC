
namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for User.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfiguration"/> class.
        /// </summary>
        public UserConfiguration()
        {

            // Properties
            this.Property(e => e.EmployeeNumber)
                .IsUnicode(false);

            this.Property(e => e.Name)
                .IsUnicode(false);

            this.Property(e => e.PasswordEncripted)
                .IsUnicode(false);

            this.Property(e => e.FirstName)
                .IsUnicode(false);

            this.Property(e => e.LastName)
                .IsUnicode(false);

            this.Property(e => e.Email)
                .IsUnicode(false);


            //Configuracion CompanyDepartment
            this.HasRequired(t => t.Departments)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.DepartmentID);

            //Configuracion Airline
            this.HasRequired(t => t.Airlines)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.AirlineCode);

            //Configuracion UserProfileRoles
            this.HasMany(e => e.UserProfileRoles)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            //Configuracion JetFuelTicket User
            this.HasMany(e => e.JetFuelTicket)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AorUserID)
                .WillCascadeOnDelete(false);

            //Relationship with PassengerInformation
            this.HasMany(r => r.PassengerInformation)
                .WithRequired(r => r.User)
                .HasForeignKey(r => r.UserId);

            this.HasMany(e => e.NationalJetFuelTickets)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AorUserID)
                .WillCascadeOnDelete(false);
        }
    }
}
