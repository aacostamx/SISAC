//------------------------------------------------------------------------
// <copyright file="NationalJetFuelManualReconcileFile.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using FileHelpers;
    using System;
    using Helpers;

    /// <summary>
    /// NationalJetFuelManualReconcileFile
    /// </summary>
    [DelimitedRecord("\t")]
    public class NationalJetFuelManualReconcileFile
    {
        [FieldIgnored]
        public int LineNumber;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int64)]
        public long ID;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string PeriodCode;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int32)]
        public int Sequence;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirlineCode;
        
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FlightNumber;
        
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ItineraryKey;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string EquipmentNumber;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string DepartureStation;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ArrivalStation;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int64)]
        public long NationalJetFuelTicketID;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime FuelingStartDate;

        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime FuelingEndDate;

        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string TicketNumber;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int32)]
        public int FueledQtyLts;

        //OPTIONAL START
        [FieldConverter(ConverterKind.Int32)]
        public int? RemainingQtyKgs;

        [FieldConverter(ConverterKind.Int32)]
        public int? RequestedQtyKgs;

        [FieldConverter(ConverterKind.Int32)]
        public int? FueledQtyKgs;

        [FieldConverter(ConverterKind.Decimal)]
        public Decimal? DensityFactor;
        //OPTIONAL END

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal PemexSubTotal;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal SuministroSubTotal;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal FleteSubTotal;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Int32)]
        public int Iva;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal PrecioSubTotal;

        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal Total;
    }
}