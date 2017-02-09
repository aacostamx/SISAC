//------------------------------------------------------------------------
// <copyright file="UserAirport.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class User Profile Role
    /// </summary>
    [Table("Security.UserAirport")]
    public partial class UserAirport
    {
        /// <summary>
        /// UserID
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// Principal
        /// </summary>
        public bool Principal { get; set; }

        /// <summary>
        /// relacion Users
        /// </summary>
        public virtual User Users { get; set; }

        /// <summary>
        /// relacion Airports
        /// </summary>
        public virtual Airport Airports { get; set; }
    }
}
