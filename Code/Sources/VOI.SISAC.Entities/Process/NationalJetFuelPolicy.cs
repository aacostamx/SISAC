//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicy.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// NationalJetFuelPolicy Class
    /// </summary>
    [Table("Process.NationalJetFuelPolicy")]
    public class NationalJetFuelPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicy"/> class.
        /// </summary>
        public NationalJetFuelPolicy() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicy"/> class.
        /// </summary>
        /// <param name="PolicyResultID">The policy result identifier.</param>
        public NationalJetFuelPolicy(long PolicyResultID)
        {
            this.PolicyResultID = PolicyResultID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelPolicy"/> class.
        /// </summary>
        /// <param name="NationalPolicyID">The national policy identifier.</param>
        /// <param name="IDREG">The idreg.</param>
        public NationalJetFuelPolicy(long NationalPolicyID, string IDREG)
        {
            this.NationalPolicyID = NationalPolicyID;
            this.IDREG = IDREG;
        }

        /// <summary>
        /// Gets or sets the policy result identifier.
        /// </summary>
        /// <value>
        /// The policy result identifier.
        /// </value>
        [Key]
        public long PolicyResultID { get; set; }

        /// <summary>
        /// Gets or sets the national policy identifier.
        /// </summary>
        /// <value>
        /// The national policy identifier.
        /// </value>
        public long NationalPolicyID { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>
        /// The document number.
        /// </value>
        public int DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        public long? NationalJetFuelTicketID { get; set; }

        /// <summary>
        /// Gets or sets the tprec.
        /// </summary>
        /// <value>
        /// The tprec.
        /// </value>
        [StringLength(50)]
        public string TPREC { get; set; }

        /// <summary>
        /// Gets or sets the idreg.
        /// </summary>
        /// <value>
        /// The idreg.
        /// </value>
        [StringLength(12)]
        public string IDREG { get; set; }

        /// <summary>
        /// Gets or sets the bldat.
        /// </summary>
        /// <value>
        /// The bldat.
        /// </value>
        [StringLength(10)]
        public string BLDAT { get; set; }

        /// <summary>
        /// Gets or sets the blart.
        /// </summary>
        /// <value>
        /// The blart.
        /// </value>
        [StringLength(2)]
        public string BLART { get; set; }

        /// <summary>
        /// Gets or sets the bukrs.
        /// </summary>
        /// <value>
        /// The bukrs.
        /// </value>
        [StringLength(8)]
        public string BUKRS { get; set; }

        /// <summary>
        /// Gets or sets the budat.
        /// </summary>
        /// <value>
        /// The budat.
        /// </value>
        [StringLength(10)]
        public string BUDAT { get; set; }

        /// <summary>
        /// Gets or sets the waers.
        /// </summary>
        /// <value>
        /// The waers.
        /// </value>
        [StringLength(5)]
        public string WAERS { get; set; }

        /// <summary>
        /// Gets or sets the kursf.
        /// </summary>
        /// <value>
        /// The kursf.
        /// </value>
        [Column(TypeName = "money")]
        public decimal? KURSF { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XBLNR.
        /// </value>
        [StringLength(16)]
        public string XBLNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The BKTXT.
        /// </value>
        [StringLength(25)]
        public string BKTXT { get; set; }

        /// <summary>
        /// Gets or sets the newbs.
        /// </summary>
        /// <value>
        /// The newbs.
        /// </value>
        [StringLength(2)]
        public string NEWBS { get; set; }

        /// <summary>
        /// Gets or sets the newko.
        /// </summary>
        /// <value>
        /// The newko.
        /// </value>
        [StringLength(17)]
        public string NEWKO { get; set; }

        /// <summary>
        /// Gets or sets the newum.
        /// </summary>
        /// <value>
        /// The newum.
        /// </value>
        [StringLength(1)]
        public string NEWUM { get; set; }

        /// <summary>
        /// Gets or sets the newbk.
        /// </summary>
        /// <value>
        /// The newbk.
        /// </value>
        [StringLength(4)]
        public string NEWBK { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The WRBTR.
        /// </value>
        [Column(TypeName = "money")]
        public decimal? WRBTR { get; set; }

        /// <summary>
        /// Gets or sets the DMB e2.
        /// </summary>
        /// <value>
        /// The DMB e2.
        /// </value>
        [Column(TypeName = "money")]
        public decimal? DMBE2 { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The MWSKZ.
        /// </value>
        [StringLength(2)]
        public string MWSKZ { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XMWST.
        /// </value>
        [StringLength(1)]
        public string XMWST { get; set; }

        /// <summary>
        /// Gets or sets the gsber.
        /// </summary>
        /// <value>
        /// The gsber.
        /// </value>
        [StringLength(4)]
        public string GSBER { get; set; }

        /// <summary>
        /// Gets or sets the kostl.
        /// </summary>
        /// <value>
        /// The kostl.
        /// </value>
        [StringLength(10)]
        public string KOSTL { get; set; }

        /// <summary>
        /// Gets or sets the aufnr.
        /// </summary>
        /// <value>
        /// The aufnr.
        /// </value>
        [StringLength(12)]
        public string AUFNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The PRCTR.
        /// </value>
        [StringLength(10)]
        public string PRCTR { get; set; }

        /// <summary>
        /// Gets or sets the fkber.
        /// </summary>
        /// <value>
        /// The fkber.
        /// </value>
        [StringLength(16)]
        public string FKBER { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        [StringLength(1)]
        public string SEGMENT { get; set; }

        /// <summary>
        /// Gets or sets the werks.
        /// </summary>
        /// <value>
        /// The werks.
        /// </value>
        [StringLength(4)]
        public string WERKS { get; set; }

        /// <summary>
        /// Gets or sets the fistl.
        /// </summary>
        /// <value>
        /// The fistl.
        /// </value>
        [StringLength(16)]
        public string FISTL { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The ZFBDT.
        /// </value>
        [StringLength(10)]
        public string ZFBDT { get; set; }

        /// <summary>
        /// Gets or sets the valut.
        /// </summary>
        /// <value>
        /// The valut.
        /// </value>
        [StringLength(10)]
        public string VALUT { get; set; }

        /// <summary>
        /// Gets or sets the zuonr.
        /// </summary>
        /// <value>
        /// The zuonr.
        /// </value>
        [StringLength(18)]
        public string ZUONR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The SGTXT.
        /// </value>
        [StringLength(50)]
        public string SGTXT { get; set; }

        /// <summary>
        /// Gets or sets the menge.
        /// </summary>
        /// <value>
        /// The menge.
        /// </value>
        [Column(TypeName = "money")]
        public decimal? MENGE { get; set; }

        /// <summary>
        /// Gets or sets the meins.
        /// </summary>
        /// <value>
        /// The meins.
        /// </value>
        [StringLength(3)]
        public string MEINS { get; set; }

        /// <summary>
        /// Gets or sets the geber.
        /// </summary>
        /// <value>
        /// The geber.
        /// </value>
        [StringLength(10)]
        public string GEBER { get; set; }

        /// <summary>
        /// Gets or sets the nototal.
        /// </summary>
        /// <value>
        /// The nototal.
        /// </value>
        [StringLength(6)]
        public string NOTOTAL { get; set; }

        /// <summary>
        /// Gets or sets the lifnr.
        /// </summary>
        /// <value>
        /// The lifnr.
        /// </value>
        [StringLength(10)]
        public string LIFNR { get; set; }

        /// <summary>
        /// Gets or sets the kunnr.
        /// </summary>
        /// <value>
        /// The kunnr.
        /// </value>
        [StringLength(10)]
        public string KUNNR { get; set; }

        /// <summary>
        /// Gets or sets the anred.
        /// </summary>
        /// <value>
        /// The anred.
        /// </value>
        [MaxLength(15)]
        public byte[] ANRED { get; set; }

        /// <summary>
        /// Gets or sets the nam e1.
        /// </summary>
        /// <value>
        /// The nam e1.
        /// </value>
        [StringLength(35)]
        public string NAME1 { get; set; }

        /// <summary>
        /// Gets or sets the nam e2.
        /// </summary>
        /// <value>
        /// The nam e2.
        /// </value>
        [StringLength(35)]
        public string NAME2 { get; set; }

        /// <summary>
        /// Gets or sets the nam e3.
        /// </summary>
        /// <value>
        /// The nam e3.
        /// </value>
        [StringLength(35)]
        public string NAME3 { get; set; }

        /// <summary>
        /// Gets or sets the bankl.
        /// </summary>
        /// <value>
        /// The bankl.
        /// </value>
        [StringLength(15)]
        public string BANKL { get; set; }

        /// <summary>
        /// Gets or sets the bankn.
        /// </summary>
        /// <value>
        /// The bankn.
        /// </value>
        [StringLength(18)]
        public string BANKN { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The ZLSCH.
        /// </value>
        [StringLength(1)]
        public string ZLSCH { get; set; }

        /// <summary>
        /// Gets or sets the bkref.
        /// </summary>
        /// <value>
        /// The bkref.
        /// </value>
        [StringLength(20)]
        public string BKREF { get; set; }

        /// <summary>
        /// Gets or sets the belnr.
        /// </summary>
        /// <value>
        /// The belnr.
        /// </value>
        [StringLength(10)]
        public string BELNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XBLN r2.
        /// </value>
        [StringLength(16)]
        public string XBLNR2 { get; set; }

        /// <summary>
        /// Gets or sets the menv.
        /// </summary>
        /// <value>
        /// The menv.
        /// </value>
        [StringLength(2)]
        public string MENV { get; set; }

        /// <summary>
        /// Gets or sets the msgid.
        /// </summary>
        /// <value>
        /// The msgid.
        /// </value>
        [StringLength(20)]
        public string MSGID { get; set; }

        /// <summary>
        /// Gets or sets the rfclog.
        /// </summary>
        /// <value>
        /// The rfclog.
        /// </value>
        [Column(TypeName = "text")]
        public string RFCLOG { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel policy control.
        /// </summary>
        /// <value>
        /// The national jet fuel policy control.
        /// </value>
        public virtual NationalJetFuelPolicyControl NationalJetFuelPolicyControl { get; set; }
    }
}
