//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoicePolicy.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entity fo National Jet Fuel Invoice Policy
    /// </summary>
    public class NationalJetFuelInvoicePolicy
    {
        /// <summary>
        /// Gets or sets the policy result identifier. Primary key.
        /// </summary>
        /// <value>
        /// The policy result identifier.
        /// </value>
        public long PolicyResultId { get; set; }

        /// <summary>
        /// Gets or sets the remittance identifier. Foreing key.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        public string RemittanceId { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>
        /// The document number.
        /// </value>
        public int DocumentNumber { get; set; }

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
        public string IDREG { get; set; }

        /// <summary>
        /// Gets or sets the bldat.
        /// </summary>
        /// <value>
        /// The bldat.
        /// </value>
        public string BLDAT { get; set; }

        /// <summary>
        /// Gets or sets the blart.
        /// </summary>
        /// <value>
        /// The blart.
        /// </value>
        public string BLART { get; set; }

        /// <summary>
        /// Gets or sets the burks.
        /// </summary>
        /// <value>
        /// The burks.
        /// </value>
        public string BUKRS { get; set; }

        /// <summary>
        /// Gets or sets the budat.
        /// </summary>
        /// <value>
        /// The budat.
        /// </value>
        public string BUDAT { get; set; }

        /// <summary>
        /// Gets or sets the waers.
        /// </summary>
        /// <value>
        /// The waers.
        /// </value>
        public string WAERS { get; set; }

        /// <summary>
        /// Gets or sets the kursf.
        /// </summary>
        /// <value>
        /// The kursf.
        /// </value>
        public decimal KURSF { get; set; }

        /// <summary>
        /// Gets or sets the XBLNR.
        /// </summary>
        /// <value>
        /// The XBLNR.
        /// </value>
        public string XBLNR { get; set; }

        /// <summary>
        /// Gets or sets the BKTXT.
        /// </summary>
        /// <value>
        /// The BKTXT.
        /// </value>
        public string BKTXT { get; set; }

        /// <summary>
        /// Gets or sets the newbs.
        /// </summary>
        /// <value>
        /// The newbs.
        /// </value>
        public string NEWBS { get; set; }

        /// <summary>
        /// Gets or sets the newko.
        /// </summary>
        /// <value>
        /// The newko.
        /// </value>
        public string NEWKO { get; set; }

        /// <summary>
        /// Gets or sets the newko.
        /// </summary>
        /// <value>
        /// The newko.
        /// </value>
        public string NEWUM { get; set; }

        /// <summary>
        /// Gets or sets the newbk.
        /// </summary>
        /// <value>
        /// The newbk.
        /// </value>
        public string NEWBK { get; set; }

        /// <summary>
        /// Gets or sets the WRBTR.
        /// </summary>
        /// <value>
        /// The WRBTR.
        /// </value>
        public decimal WRBTR { get; set; }

        /// <summary>
        /// Gets or sets the DMB e2.
        /// </summary>
        /// <value>
        /// The DMB e2.
        /// </value>
        public decimal DMBE2 { get; set; }

        /// <summary>
        /// Gets or sets the MWSKZ.
        /// </summary>
        /// <value>
        /// The MWSKZ.
        /// </value>
        public string MWSKZ { get; set; }

        /// <summary>
        /// Gets or sets the XMWST.
        /// </summary>
        /// <value>
        /// The XMWST.
        /// </value>
        public string XMWST { get; set; }

        /// <summary>
        /// Gets or sets the gsber.
        /// </summary>
        /// <value>
        /// The gsber.
        /// </value>
        public string GSBER { get; set; }

        /// <summary>
        /// Gets or sets the kostl.
        /// </summary>
        /// <value>
        /// The kostl.
        /// </value>
        public string KOSTL { get; set; }

        /// <summary>
        /// Gets or sets the aufnr.
        /// </summary>
        /// <value>
        /// The aufnr.
        /// </value>
        public string AUFNR { get; set; }

        /// <summary>
        /// Gets or sets the PRCTR.
        /// </summary>
        /// <value>
        /// The PRCTR.
        /// </value>
        public string PRCTR { get; set; }

        /// <summary>
        /// Gets or sets the fkber.
        /// </summary>
        /// <value>
        /// The fkber.
        /// </value>
        public string FKBER { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        public string SEGMENT { get; set; }

        /// <summary>
        /// Gets or sets the werks.
        /// </summary>
        /// <value>
        /// The werks.
        /// </value>
        public string WERKS { get; set; }

        /// <summary>
        /// Gets or sets the fistl.
        /// </summary>
        /// <value>
        /// The fistl.
        /// </value>
        public string FISTL { get; set; }

        /// <summary>
        /// Gets or sets the ZFBDT.
        /// </summary>
        /// <value>
        /// The ZFBDT.
        /// </value>
        public string ZFBDT { get; set; }

        /// <summary>
        /// Gets or sets the valut.
        /// </summary>
        /// <value>
        /// The valut.
        /// </value>
        public string VALUT { get; set; }

        /// <summary>
        /// Gets or sets the zuonr.
        /// </summary>
        /// <value>
        /// The zuonr.
        /// </value>
        public string ZUONR { get; set; }

        /// <summary>
        /// Gets or sets the SGTXT.
        /// </summary>
        /// <value>
        /// The SGTXT.
        /// </value>
        public string SGTXT { get; set; }

        /// <summary>
        /// Gets or sets the menge.
        /// </summary>
        /// <value>
        /// The menge.
        /// </value>
        public decimal MENGE { get; set; }

        /// <summary>
        /// Gets or sets the meins.
        /// </summary>
        /// <value>
        /// The meins.
        /// </value>
        public string MEINS { get; set; }

        /// <summary>
        /// Gets or sets the geber.
        /// </summary>
        /// <value>
        /// The geber.
        /// </value>
        public string GEBER { get; set; }

        /// <summary>
        /// Gets or sets the nototal.
        /// </summary>
        /// <value>
        /// The nototal.
        /// </value>
        public string NOTOTAL { get; set; }

        /// <summary>
        /// Gets or sets the lifnr.
        /// </summary>
        /// <value>
        /// The lifnr.
        /// </value>
        public string LIFNR { get; set; }

        /// <summary>
        /// Gets or sets the kunnr.
        /// </summary>
        /// <value>
        /// The kunnr.
        /// </value>
        public string KUNNR { get; set; }

        /// <summary>
        /// Gets or sets the anred.
        /// </summary>
        /// <value>
        /// The anred.
        /// </value>
        public string ANRED { get; set; }

        /// <summary>
        /// Gets or sets the nam e1.
        /// </summary>
        /// <value>
        /// The nam e1.
        /// </value>
        public string NAME1 { get; set; }

        /// <summary>
        /// Gets or sets the nam e1.
        /// </summary>
        /// <value>
        /// The nam e1.
        /// </value>
        public string NAME2 { get; set; }

        /// <summary>
        /// Gets or sets the nam e1.
        /// </summary>
        /// <value>
        /// The nam e1.
        /// </value>
        public string NAME3 { get; set; }

        /// <summary>
        /// Gets or sets the bankl.
        /// </summary>
        /// <value>
        /// The bankl.
        /// </value>
        public string BANKL { get; set; }

        /// <summary>
        /// Gets or sets the bankn.
        /// </summary>
        /// <value>
        /// The bankn.
        /// </value>
        public string BANKN { get; set; }

        /// <summary>
        /// Gets or sets the ZLSCH.
        /// </summary>
        /// <value>
        /// The ZLSCH.
        /// </value>
        public string ZLSCH { get; set; }

        /// <summary>
        /// Gets or sets the bkref.
        /// </summary>
        /// <value>
        /// The bkref.
        /// </value>
        public string BKREF { get; set; }

        /// <summary>
        /// Gets or sets the belnr.
        /// </summary>
        /// <value>
        /// The belnr.
        /// </value>
        public string BELNR { get; set; }

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
        /// Gets or sets the national jet fuel invoice controls.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice controls.
        /// </value>
        public virtual NationalJetFuelInvoiceControl NationalJetFuelInvoiceControl { get; set; }
    }
}
