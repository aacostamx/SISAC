//------------------------------------------------------------------------
// <copyright file="ProcessMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using System.Linq;
    using System;
    using AutoMapper;
    using Dto.Enums;
    using Dto.Process;
    using Entities.Enums;
    using Entities.Process;

    /// <summary>
    /// ProcessMaps
    /// </summary>
    public class ProcessMaps : Profile
    {
        /// <summary>
        /// Gets the name of the ProcessMaps.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return "ProcessMaps";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            this.Map();
            this.NationalMaps();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMaps"/> class.
        /// </summary>
        public ProcessMaps()
        {
            
        }

        /// <summary>
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            CreateMap<CalculationStatus, CalculationStatusDto>()
                .ReverseMap();

            CreateMap<ConfirmationStatus, ConfirmationStatusDto>()
                .ReverseMap();

            CreateMap<StatusProcess, StatusProcessDto>()
                .ReverseMap();

            CreateMap<JetFuelProvision, JetFuelProvisionDto>()
                .ReverseMap();

            CreateMap<JetFuelLogError, JetFuelLogErrorDto>()
                .ReverseMap();

            CreateMap<JetFuelProcess, JetFuelProcessDto>()
                .ReverseMap();

            CreateMap<JetFuelPolicy, JetFuelPolicyDto>()
                .ReverseMap();

            CreateMap<JetFuelPolicyControl, JetFuelPolicyControlDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps for National invoice objects.
        /// </summary>
        private void NationalMaps()
        {
            CreateMap<NationalJetFuelProcess, NationalJetFuelProcessDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelLogError, NationalJetFuelLogErrorDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelCost, NationalJetFuelCostDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicy, NationalJetFuelPolicyDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelPolicyControl, NationalJetFuelPolicyControlDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoiceControl, NationalJetFuelInvoiceControlDto>()
            .ForMember(c => c.DocumentStatusName, r => r.MapFrom(f => f.DocumentStatus.DocumentStatusName))
            .ForMember(c => c.RemittanceStatusName, r => r.MapFrom(f => f.RemittanceStatus.RemittanceStatusName))
            .ForMember(c => c.InvoiceCount, r => r.MapFrom(f => f.NationalJetFuelInvoiceDetails.Select(c => c.ElectronicInvoiceNumber).Distinct().Count()))
                .ReverseMap();

            CreateMap<NationalJetFuelInvoiceDetail, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoicePolicy, NationalJetFuelInvoicePolicyDto>()
                .ReverseMap();

            CreateMap<NationalJetFuelInvoiceDetail, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            CreateMap<DocumentStatus, DocumentStatusDto>()
                .ReverseMap();

            CreateMap<RemittanceStatus, RemittanceStatusDto>()
                .ReverseMap();

            CreateMap<RemittanceSearch, RemittanceSearchDto>()
                .ReverseMap();
        }
    }
}
