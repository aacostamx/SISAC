//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoicePolicyConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    public class NationalJetFuelInvoicePolicyConfiguration : EntityTypeConfiguration<NationalJetFuelInvoicePolicy>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Process";

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoicePolicyConfiguration"/> class.
        /// </summary>
        public NationalJetFuelInvoicePolicyConfiguration()
        {
            // Defines the table's name and schema
            this.ToTable("NationalJetFuelInvoicePolicy", this.schemaName);

            // Defines the primary key
            this.HasKey<long>(c => c.PolicyResultId);

            // Defines the columns' properties
            this.Property(c => c.PolicyResultId)
                .HasColumnName("PolicyResultID")
                .IsRequired();

            this.Property(c => c.RemittanceId)
                .HasColumnName("RemittanceID")
                .IsRequired();

            this.Property(e => e.MonthYear)
                .HasColumnName("MonthYear")
                .IsRequired();

            this.Property(e => e.Period)
                .HasColumnName("Period")
                .IsRequired();

            this.Property(c => c.DocumentNumber)
                .HasColumnName("DocumentNumber")
                .IsRequired();

            this.Property(c => c.TPREC)
                .HasColumnName("TPREC")
                .HasMaxLength(50);

            this.Property(c => c.IDREG)
                .HasColumnName("IDREG")
                .HasMaxLength(12);

            this.Property(c => c.BLDAT)
                .HasColumnName("BLDAT")
                .HasMaxLength(10);

            this.Property(c => c.BLART)
                .HasColumnName("BLART")
                .HasMaxLength(4);

            this.Property(c => c.BUKRS)
                .HasColumnName("BUKRS")
                .HasMaxLength(10);

            this.Property(c => c.BUDAT)
                .HasColumnName("BUDAT")
                .HasMaxLength(10);

            this.Property(c => c.WAERS)
                .HasColumnName("WAERS")
                .HasMaxLength(10);

            this.Property(c => c.KURSF)
                .HasColumnName("KURSF")
                .HasPrecision(9, 5);

            this.Property(c => c.XBLNR)
                .HasColumnName("XBLNR")
                .HasMaxLength(16);

            this.Property(c => c.BKTXT)
                .HasColumnName("BKTXT")
                .HasMaxLength(150);

            this.Property(c => c.NEWBS)
                .HasColumnName("NEWBS")
                .HasMaxLength(2);

            this.Property(c => c.NEWKO)
                .HasColumnName("NEWKO")
                .HasMaxLength(34);

            this.Property(c => c.NEWUM)
                .HasColumnName("NEWUM")
                .HasMaxLength(1);

            this.Property(c => c.NEWBK)
                .HasColumnName("NEWBK")
                .HasMaxLength(8);

            this.Property(c => c.WRBTR)
                .HasColumnName("WRBTR")
                .HasPrecision(13, 2);

            this.Property(c => c.DMBE2)
                .HasColumnName("DMBE2")
                .HasPrecision(13, 2);

            this.Property(c => c.MWSKZ)
                .HasColumnName("MWSKZ")
                .HasMaxLength(2);

            this.Property(c => c.XMWST)
                .HasColumnName("XMWST")
                .HasMaxLength(1);

            this.Property(c => c.GSBER)
                .HasColumnName("GSBER")
                .HasMaxLength(4);

            this.Property(c => c.KOSTL)
                .HasColumnName("KOSTL")
                .HasMaxLength(10);

            this.Property(c => c.AUFNR)
                .HasColumnName("AUFNR")
                .HasMaxLength(24);

            this.Property(c => c.PRCTR)
                .HasColumnName("PRCTR")
                .HasMaxLength(10);

            this.Property(c => c.FKBER)
                .HasColumnName("FKBER")
                .HasMaxLength(32);

            this.Property(c => c.SEGMENT)
                .HasColumnName("SEGMENT")
                .HasMaxLength(1);

            this.Property(c => c.WERKS)
                .HasColumnName("WERKS")
                .HasMaxLength(8);

            this.Property(c => c.FISTL)
                .HasColumnName("FISTL")
                .HasMaxLength(32);

            this.Property(c => c.ZFBDT)
                .HasColumnName("ZFBDT")
                .HasMaxLength(10);

            this.Property(c => c.VALUT)
                .HasColumnName("VALUT")
                .HasMaxLength(10);

            this.Property(c => c.ZUONR)
                .HasColumnName("ZUONR")
                .HasMaxLength(18);

            this.Property(c => c.SGTXT)
                .HasColumnName("SGTXT")
                .HasMaxLength(150);

            this.Property(c => c.MENGE)
                .HasColumnName("MENGE")
                .HasPrecision(13, 3);

            this.Property(c => c.MEINS)
                .HasColumnName("MEINS")
                .HasMaxLength(6);

            this.Property(c => c.GEBER)
                .HasColumnName("GEBER")
                .HasMaxLength(10);

            this.Property(c => c.NOTOTAL)
                .HasColumnName("NOTOTAL")
                .HasMaxLength(6);

            this.Property(c => c.LIFNR)
                .HasColumnName("LIFNR")
                .HasMaxLength(10);

            this.Property(c => c.KUNNR)
                .HasColumnName("KUNNR")
                .HasMaxLength(10);

            this.Property(c => c.ANRED)
                .HasColumnName("ANRED")
                .HasMaxLength(15);

            this.Property(c => c.NAME1)
                .HasColumnName("NAME1")
                .HasMaxLength(35);

            this.Property(c => c.NAME2)
                .HasColumnName("NAME2")
                .HasMaxLength(35);

            this.Property(c => c.NAME3)
                .HasColumnName("NAME3")
                .HasMaxLength(35);

            this.Property(c => c.BANKL)
                .HasColumnName("BANKL")
                .HasMaxLength(15);

            this.Property(c => c.BANKN)
                .HasColumnName("BANKN")
                .HasMaxLength(18);

            this.Property(c => c.ZLSCH)
                .HasColumnName("ZLSCH")
                .HasMaxLength(1);

            this.Property(c => c.BKREF)
                .HasColumnName("BKREF")
                .HasMaxLength(20);

            this.Property(c => c.BELNR)
                .HasColumnName("BELNR")
                .HasMaxLength(10);

            this.Property(c => c.MENV)
                .HasColumnName("MENV")
                .HasMaxLength(2);

            this.Property(c => c.MSGID)
                .HasColumnName("MSGID")
                .HasMaxLength(20);

            this.Property(c => c.RFCLOG)
                .HasColumnName("RFCLOG");

            // Defines the relationship
            this.HasRequired(c => c.NationalJetFuelInvoiceControl)
                .WithMany(e => e.NationalJetFuelInvoicePolicies)
                .HasForeignKey(c => new
                {
                    c.RemittanceId,
                    c.MonthYear,
                    c.Period
                });
        }
    }
}
