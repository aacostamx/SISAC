//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Runtime.Serialization;

    /// <summary>
    /// National Jet Fuel Policy View Object
    /// </summary>
    [DataContract(Name = "T_POLIZA")]
    public class NationalJetFuelPolicyVO
    {
        /// <summary>
        /// Gets or sets the policy result identifier.
        /// </summary>
        /// <value>
        /// The policy result identifier.
        /// </value>
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

        #region SAP Parameters
        /// <summary>
        /// Gets or sets the tprec.
        /// </summary>
        /// <value>
        /// The tprec.
        /// </value>
        public string TPREC { get; set; }

        /// <summary>
        /// Gets or sets the idreg.
        /// </summary>
        /// <value>
        /// The idreg.
        /// </value>
        [DataMember]
        public string IDREG { get; set; }

        /// <summary>
        /// Gets or sets the bldat.
        /// </summary>
        /// <value>
        /// The bldat.
        /// </value>
        [DataMember]
        public string BLDAT { get; set; }

        /// <summary>
        /// Gets or sets the blart.
        /// </summary>
        /// <value>
        /// The blart.
        /// </value>
        [DataMember]
        public string BLART { get; set; }

        /// <summary>
        /// Gets or sets the bukrs.
        /// </summary>
        /// <value>
        /// The bukrs.
        /// </value>
        [DataMember]
        public string BUKRS { get; set; }

        /// <summary>
        /// Gets or sets the budat.
        /// </summary>
        /// <value>
        /// The budat.
        /// </value>
        [DataMember]
        public string BUDAT { get; set; }

        /// <summary>
        /// Gets or sets the waers.
        /// </summary>
        /// <value>
        /// The waers.
        /// </value>
        [DataMember]
        public string WAERS { get; set; }

        /// <summary>
        /// Gets or sets the kursf.
        /// </summary>
        /// <value>
        /// The kursf.
        /// </value>
        [DataMember]
        public decimal KURSF { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XBLNR.
        /// </value>
        [DataMember]
        public string XBLNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The BKTXT.
        /// </value>
        [DataMember]
        public string BKTXT { get; set; }

        /// <summary>
        /// Gets or sets the newbs.
        /// </summary>
        /// <value>
        /// The newbs.
        /// </value>
        [DataMember]
        public string NEWBS { get; set; }

        /// <summary>
        /// Gets or sets the newko.
        /// </summary>
        /// <value>
        /// The newko.
        /// </value>
        [DataMember]
        public string NEWKO { get; set; }

        /// <summary>
        /// Gets or sets the newum.
        /// </summary>
        /// <value>
        /// The newum.
        /// </value>
        [DataMember]
        public string NEWUM { get; set; }

        /// <summary>
        /// Gets or sets the newbk.
        /// </summary>
        /// <value>
        /// The newbk.
        /// </value>
        [DataMember]
        public string NEWBK { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The WRBTR.
        /// </value>
        [DataMember]
        public decimal WRBTR { get; set; }

        /// <summary>
        /// Gets or sets the DMB e2.
        /// </summary>
        /// <value>
        /// The DMB e2.
        /// </value>
        [DataMember]
        public decimal? DMBE2 { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The MWSKZ.
        /// </value>
        [DataMember]
        public string MWSKZ { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XMWST.
        /// </value>
        [DataMember]
        public string XMWST { get; set; }

        /// <summary>
        /// Gets or sets the gsber.
        /// </summary>
        /// <value>
        /// The gsber.
        /// </value>
        [DataMember]
        public string GSBER { get; set; }

        /// <summary>
        /// Gets or sets the kostl.
        /// </summary>
        /// <value>
        /// The kostl.
        /// </value>
        [DataMember]
        public string KOSTL { get; set; }

        /// <summary>
        /// Gets or sets the aufnr.
        /// </summary>
        /// <value>
        /// The aufnr.
        /// </value>
        [DataMember]
        public string AUFNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The PRCTR.
        /// </value>
        [DataMember]
        public string PRCTR { get; set; }

        /// <summary>
        /// Gets or sets the fkber.
        /// </summary>
        /// <value>
        /// The fkber.
        /// </value>
        [DataMember]
        public string FKBER { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        [DataMember]
        public string SEGMENT { get; set; }

        /// <summary>
        /// Gets or sets the werks.
        /// </summary>
        /// <value>
        /// The werks.
        /// </value>
        [DataMember]
        public string WERKS { get; set; }

        /// <summary>
        /// Gets or sets the fistl.
        /// </summary>
        /// <value>
        /// The fistl.
        /// </value>
        [DataMember]
        public string FISTL { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The ZFBDT.
        /// </value>
        [DataMember]
        public string ZFBDT { get; set; }

        /// <summary>
        /// Gets or sets the valut.
        /// </summary>
        /// <value>
        /// The valut.
        /// </value>
        [DataMember]
        public string VALUT { get; set; }

        /// <summary>
        /// Gets or sets the zuonr.
        /// </summary>
        /// <value>
        /// The zuonr.
        /// </value>
        [DataMember]
        public string ZUONR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The SGTXT.
        /// </value>
        [DataMember]
        public string SGTXT { get; set; }

        /// <summary>
        /// Gets or sets the menge.
        /// </summary>
        /// <value>
        /// The menge.
        /// </value>
        [DataMember]
        public decimal MENGE { get; set; }

        /// <summary>
        /// Gets or sets the meins.
        /// </summary>
        /// <value>
        /// The meins.
        /// </value>
        [DataMember]
        public string MEINS { get; set; }

        /// <summary>
        /// Gets or sets the geber.
        /// </summary>
        /// <value>
        /// The geber.
        /// </value>
        [DataMember]
        public string GEBER { get; set; }

        /// <summary>
        /// Gets or sets the nototal.
        /// </summary>
        /// <value>
        /// The nototal.
        /// </value>
        [DataMember]
        public string NOTOTAL { get; set; }

        /// <summary>
        /// Gets or sets the lifnr.
        /// </summary>
        /// <value>
        /// The lifnr.
        /// </value>
        [DataMember]
        public string LIFNR { get; set; }

        /// <summary>
        /// Gets or sets the kunnr.
        /// </summary>
        /// <value>
        /// The kunnr.
        /// </value>
        [DataMember]
        public string KUNNR { get; set; }

        /// <summary>
        /// Gets or sets the anred.
        /// </summary>
        /// <value>
        /// The anred.
        /// </value>
        [DataMember]
        public string ANRED { get; set; }

        /// <summary>
        /// Gets or sets the nam e1.
        /// </summary>
        /// <value>
        /// The nam e1.
        /// </value>
        [DataMember]
        public string NAME1 { get; set; }

        /// <summary>
        /// Gets or sets the nam e2.
        /// </summary>
        /// <value>
        /// The nam e2.
        /// </value>
        [DataMember]
        public string NAME2 { get; set; }

        /// <summary>
        /// Gets or sets the nam e3.
        /// </summary>
        /// <value>
        /// The nam e3.
        /// </value>
        [DataMember]
        public string NAME3 { get; set; }

        /// <summary>
        /// Gets or sets the bankl.
        /// </summary>
        /// <value>
        /// The bankl.
        /// </value>
        [DataMember]
        public string BANKL { get; set; }

        /// <summary>
        /// Gets or sets the bankn.
        /// </summary>
        /// <value>
        /// The bankn.
        /// </value>
        [DataMember]
        public string BANKN { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The ZLSCH.
        /// </value>
        [DataMember]
        public string ZLSCH { get; set; }

        /// <summary>
        /// Gets or sets the bkref.
        /// </summary>
        /// <value>
        /// The bkref.
        /// </value>
        [DataMember]
        public string BKREF { get; set; }

        /// <summary>
        /// Gets or sets the belnr.
        /// </summary>
        /// <value>
        /// The belnr.
        /// </value>
        public string BELNR { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The XBLN r2.
        /// </value>
        public string XBLNR2 { get; set; }

        /// <summary>
        /// Gets or sets the menv.
        /// </summary>
        /// <value>
        /// The menv.
        /// </value>
        public string MENV { get; set; }

        /// <summary>
        /// Gets or sets the msgid.
        /// </summary>
        /// <value>
        /// The msgid.
        /// </value>
        public string MSGID { get; set; }

        /// <summary>
        /// Gets or sets the rfclog.
        /// </summary>
        /// <value>
        /// The rfclog.
        /// </value>
        public string RFCLOG { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets the national jet fuel policy control.
        /// </summary>
        /// <value>
        /// The national jet fuel policy control.
        /// </value>
        public NationalJetFuelPolicyControlVO NationalJetFuelPolicyControl { get; set; }
    }
}