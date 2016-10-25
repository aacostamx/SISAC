//------------------------------------------------------------------------
// <copyright file="ProcessWebMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------

namespace VOI.SISAC.Web.MapConfiguration
{
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

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<JetFuelProcessDto, PeriodVO>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelProcessVO, JetFuelProcessDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicyControlParamsVO, JetFuelPolicyControlDto>()
                .ForMember(dto => dto.AirportCodes, opt => opt.MapFrom(vo => string.Join(",", vo.AirportCodes)))
                .ForMember(dto => dto.ProviderCodes, opt => opt.MapFrom(vo => string.Join(",", vo.ProviderCodes)))
                .ForMember(dto => dto.ServiceCodes, opt => opt.MapFrom(vo => string.Join(",", vo.ServiceCodes)));

            Mapper.CreateMap<JetFuelPolicyControlDto, JetFuelPolicyControlParamsVO>()
                .ForMember(vo => vo.AirportCodes, opt => opt.MapFrom(dto => dto.AirportCodes.Split(',').ToList()))
                .ForMember(vo => vo.ProviderCodes, opt => opt.MapFrom(dto => dto.ProviderCodes.Split(',').ToList()))
                .ForMember(vo => vo.ServiceCodes, opt => opt.MapFrom(dto => dto.ServiceCodes.Split(',').ToList()));


            Mapper.CreateMap<JetFuelPolicyControlDto, JetFuelPoliciesHistoryVO>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPoliciesHistoryDto, JetFuelPoliciesHistoryVO>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicyDto, JetFuelPolicyVO>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicyVO, PolicyResponseInformation>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            NationalMaps();
        }

        /// <summary>
        /// Maps the national objects.
        /// </summary>
        private static void NationalMaps()
        {
            Mapper.CreateMap<NationalJetFuelProcessVO, NationalJetFuelProcessDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelProcessDto, PeriodVO>();

            Mapper.CreateMap<NationalJetFuelLogErrorVO, NationalJetFuelLogErrorDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelCostVO, NationalJetFuelCostDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicyHistoryDto, NationalJetFuelPolicyHistoryVO>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicyControlDto, NationalJetFuelPolicyHistoryVO>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicyControlVO, NationalJetFuelPolicyControlDto>()
                .ForMember(dto => dto.AirportCodes, param => param.MapFrom(vo => string.Join(",", vo.AirportCodesList)))
                .ForMember(dto => dto.ProviderCodes, param => param.MapFrom(vo => string.Join(",", vo.ProviderCodesList)))
                .ForMember(dto => dto.ServiceCodes, param => param.MapFrom(vo => string.Join(",", vo.ServiceCodesList)));

            Mapper.CreateMap<NationalJetFuelPolicyControlDto, NationalJetFuelPolicyControlVO>()
                .ForMember(vo => vo.AirportCodesList, param => param.MapFrom(dto => dto.AirportCodes.Split(',').ToList()))
                .ForMember(vo => vo.ProviderCodesList, param => param.MapFrom(dto => dto.ProviderCodes.Split(',').ToList()))
                .ForMember(vo => vo.ServiceCodesList, param => param.MapFrom(dto => dto.ServiceCodes.Split(',').ToList()));

            Mapper.CreateMap<NationalJetFuelPolicyDto, NationalJetFuelPolicyVO>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            Mapper.CreateMap<NationalFuelInvoiceControlVO, NationalJetFuelInvoiceControlDto>()
                .ForMember(dto => dto.RemittanceID, r => r.MapFrom(vo => vo.RemittanceId))
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelReconcileControlVO, NationalJetFuelInvoiceControlDto>()
                .ReverseMap();

            Mapper.CreateMap<ConfirmationStatusVO, ConfirmationStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<DocumentStatusVO, DocumentStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<RemittanceStatusDto, RemittanceStatusVO>()
                .ReverseMap();

            Mapper.CreateMap<CalculationStatusDto, CalculationStatusVO>()
                .ReverseMap();

            Mapper.CreateMap<StatusProcessDto, StatusProcessVO>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoicePolicyVO, NationalJetFuelInvoicePolicyDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoicePolicyVO, PolicyRequestInformation>()
                .ReverseMap();

            Mapper.CreateMap<RemittanceSearchVO, RemittanceSearchDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoiceDetailVO, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalInvoiceControlCreateParameter, NationalFuelInvoiceControlVO>()
                .ForMember(para => para.RemittanceId, c => c.MapFrom(vo => vo.RemittanceIdentifier))
                .ReverseMap();
        }
    }
}