//------------------------------------------------------------------------
// <copyright file="ProcessWebMaps.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Dto.Enums;
    using Models.Enums;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Process;

    /// <summary>
    /// Maps for classes in the schema Process
    /// </summary>
    public class ProcessWebMaps : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return "ProcessWebMaps";
            }
        }

        protected override void Configure()
        {
            CreateMap<JetFuelProcessDto, PeriodVO>()
                .ReverseMap();

            CreateMap<JetFuelProcessVO, JetFuelProcessDto>()
                .ReverseMap();

            CreateMap<JetFuelPolicyControlParamsVO, JetFuelPolicyControlDto>()
                .ForMember(dto => dto.AirportCodes, opt => opt.MapFrom(vo => string.Join(",", vo.AirportCodes)))
                .ForMember(dto => dto.ProviderCodes, opt => opt.MapFrom(vo => string.Join(",", vo.ProviderCodes)))
                .ForMember(dto => dto.ServiceCodes, opt => opt.MapFrom(vo => string.Join(",", vo.ServiceCodes)));

            CreateMap<JetFuelPolicyControlDto, JetFuelPolicyControlParamsVO>()
                .ForMember(vo => vo.AirportCodes, opt => opt.MapFrom(dto => dto.AirportCodes.Split(',').ToList()))
                .ForMember(vo => vo.ProviderCodes, opt => opt.MapFrom(dto => dto.ProviderCodes.Split(',').ToList()))
                .ForMember(vo => vo.ServiceCodes, opt => opt.MapFrom(dto => dto.ServiceCodes.Split(',').ToList()));


            CreateMap<JetFuelPolicyControlDto, JetFuelPoliciesHistoryVO>()
                .ReverseMap();

            CreateMap<JetFuelPoliciesHistoryDto, JetFuelPoliciesHistoryVO>()
                .ReverseMap();

            CreateMap<JetFuelPolicyDto, JetFuelPolicyVO>()
                .ReverseMap();

            CreateMap<JetFuelPolicyVO, PolicyResponseInformation>()
                .ReverseMap();

            CreateMap<JetFuelPolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            NationalMaps();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessWebMaps"/> class.
        /// </summary>
        public ProcessWebMaps()
        {
            
        }

        /// <summary>
        /// Maps the national objects.
        /// </summary>
        private void NationalMaps()
        {
            CreateMap<NationalJetFuelProcessVO, NationalJetFuelProcessDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelProcessDto, PeriodVO>();

            CreateMap<NationalJetFuelLogErrorVO, NationalJetFuelLogErrorDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelCostVO, NationalJetFuelCostDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicyHistoryDto, NationalJetFuelPolicyHistoryVO>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicyControlDto, NationalJetFuelPolicyHistoryVO>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicyControlVO, NationalJetFuelPolicyControlDto>()
                .ForMember(dto => dto.AirportCodes, param => param.MapFrom(vo => string.Join(",", vo.AirportCodesList)))
                .ForMember(dto => dto.ProviderCodes, param => param.MapFrom(vo => string.Join(",", vo.ProviderCodesList)))
                .ForMember(dto => dto.ServiceCodes, param => param.MapFrom(vo => string.Join(",", vo.ServiceCodesList)));

            CreateMap<NationalJetFuelPolicyControlDto, NationalJetFuelPolicyControlVO>()
                .ForMember(vo => vo.AirportCodesList, param => param.MapFrom(dto => dto.AirportCodes.Split(',').ToList()))
                .ForMember(vo => vo.ProviderCodesList, param => param.MapFrom(dto => dto.ProviderCodes.Split(',').ToList()))
                .ForMember(vo => vo.ServiceCodesList, param => param.MapFrom(dto => dto.ServiceCodes.Split(',').ToList()));

            CreateMap<NationalJetFuelPolicyDto, NationalJetFuelPolicyVO>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            CreateMap<NationalFuelInvoiceControlVO, NationalJetFuelInvoiceControlDto>()
                .ForMember(dto => dto.RemittanceID, r => r.MapFrom(vo => vo.RemittanceId))
                .ReverseMap();

            CreateMap<NationalJetFuelReconcileControlVO, NationalJetFuelInvoiceControlDto>()
                .ReverseMap();

            CreateMap<ConfirmationStatusVO, ConfirmationStatusDto>()
                .ReverseMap();

            CreateMap<DocumentStatusVO, DocumentStatusDto>()
                .ReverseMap();

            CreateMap<RemittanceStatusDto, RemittanceStatusVO>()
                .ReverseMap();

            CreateMap<CalculationStatusDto, CalculationStatusVO>()
                .ReverseMap();

            CreateMap<StatusProcessDto, StatusProcessVO>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoicePolicyVO, NationalJetFuelInvoicePolicyDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoicePolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            CreateMap<RemittanceSearchVO, RemittanceSearchDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoiceDetailVO, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            CreateMap<NationalInvoiceControlCreateParameter, NationalFuelInvoiceControlVO>()
                .ForMember(para => para.RemittanceId, c => c.MapFrom(vo => vo.RemittanceIdentifier))
                .ReverseMap();
        }
    }
}