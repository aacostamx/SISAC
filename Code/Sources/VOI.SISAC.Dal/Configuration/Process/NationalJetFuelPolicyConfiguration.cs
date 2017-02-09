//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelPolicyConfiguration Class
    /// </summary>
    public class NationalJetFuelPolicyConfiguration : EntityTypeConfiguration<NationalJetFuelPolicy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicyConfiguration"/> class.
        /// </summary>
        public NationalJetFuelPolicyConfiguration()
        {
            this.Property(e => e.TPREC)
               .IsUnicode(false);

            this.Property(e => e.IDREG)
                .IsUnicode(false);

            this.Property(e => e.BLDAT)
                .IsUnicode(false);

            this.Property(e => e.BLART)
                .IsUnicode(false);

            this.Property(e => e.BUKRS)
                .IsUnicode(false);

            this.Property(e => e.BUDAT)
                .IsUnicode(false);

            this.Property(e => e.WAERS)
                .IsUnicode(false);

            this.Property(e => e.KURSF)
                .HasPrecision(19, 4);

            this.Property(e => e.XBLNR)
                .IsUnicode(false);

            this.Property(e => e.BKTXT)
                .IsUnicode(false);

            this.Property(e => e.NEWBS)
                .IsUnicode(false);

            this.Property(e => e.NEWKO)
                .IsUnicode(false);

            this.Property(e => e.NEWUM)
                .IsUnicode(false);

            this.Property(e => e.NEWBK)
                .IsUnicode(false);

            this.Property(e => e.WRBTR)
                .HasPrecision(19, 4);

            this.Property(e => e.DMBE2)
                .HasPrecision(19, 4);

            this.Property(e => e.MWSKZ)
                .IsUnicode(false);

            this.Property(e => e.XMWST)
                .IsUnicode(false);

            this.Property(e => e.GSBER)
                .IsUnicode(false);

            this.Property(e => e.KOSTL)
                .IsUnicode(false);

            this.Property(e => e.AUFNR)
                .IsUnicode(false);

            this.Property(e => e.PRCTR)
                .IsUnicode(false);

            this.Property(e => e.FKBER)
                .IsUnicode(false);

            this.Property(e => e.SEGMENT)
                .IsUnicode(false);

            this.Property(e => e.WERKS)
                .IsUnicode(false);

            this.Property(e => e.FISTL)
                .IsUnicode(false);

            this.Property(e => e.ZFBDT)
                .IsUnicode(false);

            this.Property(e => e.VALUT)
                .IsUnicode(false);

            this.Property(e => e.ZUONR)
                .IsUnicode(false);

            this.Property(e => e.SGTXT)
                .IsUnicode(false);

            this.Property(e => e.MENGE)
                .HasPrecision(19, 4);

            this.Property(e => e.MEINS)
                .IsUnicode(false);

            this.Property(e => e.GEBER)
                .IsUnicode(false);

            this.Property(e => e.NOTOTAL)
                .IsUnicode(false);

            this.Property(e => e.LIFNR)
                .IsUnicode(false);

            this.Property(e => e.KUNNR)
                .IsUnicode(false);

            this.Property(e => e.NAME1)
                .IsUnicode(false);

            this.Property(e => e.NAME2)
                .IsUnicode(false);

            this.Property(e => e.NAME3)
                .IsUnicode(false);

            this.Property(e => e.BANKL)
                .IsUnicode(false);

            this.Property(e => e.BANKN)
                .IsUnicode(false);

            this.Property(e => e.ZLSCH)
                .IsUnicode(false);

            this.Property(e => e.BKREF)
                .IsUnicode(false);

            this.Property(e => e.BELNR)
                .IsUnicode(false);

            this.Property(e => e.XBLNR2)
                .IsUnicode(false);

            this.Property(e => e.MENV)
                .IsUnicode(false);

            this.Property(e => e.MSGID)
                .IsUnicode(false);

            this.Property(e => e.RFCLOG)
                .IsUnicode(false);

        }
    }
}
