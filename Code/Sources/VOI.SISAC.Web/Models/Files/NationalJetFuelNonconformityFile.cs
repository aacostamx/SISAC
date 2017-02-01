//------------------------------------------------------------------------
// <copyright file="NationalJetFuelNonconformityFile.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using FileHelpers;
    using System;
    using Helpers;

    /// <summary>
    /// NationalJetFuelNonconformityFile
    /// </summary>
    [DelimitedRecord("\t")]
    public class NationalJetFuelNonconformityFile
    {
        [FieldIgnored]
        public int LineNumber;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int64)]
        public long ID;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirlineCode;


        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ServiceCode;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FederalTaxCode;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StationCode;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string InvoiceNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CustomerNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CustomerName;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime InvoiceDate;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ElectronicInvoiceNumber;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime ElectronicInvoiceDate;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProductNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProductDescription;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string TicketNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string OperationType;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FlightNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string EquipmentNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirplaneModel;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime StartDateTime;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EndDateTime;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal StartMeter;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal EndMeter;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal VolumeM3;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string RateType;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal JetFuelAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal FreightAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal DiscountAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal FuelingAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal SubtotalAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal TaxAmount;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal TotalAmount;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string Status;

        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ReconciliationStatus;

        [FieldConverter(ConverterKind.Decimal)]
        public Decimal? InvoiceCostVariance;
        
    }
}