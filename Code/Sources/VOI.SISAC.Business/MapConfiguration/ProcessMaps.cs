//------------------------------------------------------------------------
// <copyright file="ProcessMaps.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.MapConfiguration
{
    using AutoMapper;
    using Dto.Enums;
    using Dto.Process;
    using Entities.Enums;
    using Entities.Process;
    using System.Linq;

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
        /// Configures the mappers between clases.
        /// </summary>
        private void Map()
        {
            Mapper.CreateMap<CalculationStatus, CalculationStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<ConfirmationStatus, ConfirmationStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<StatusProcess, StatusProcessDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelProvision, JetFuelProvisionDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelLogError, JetFuelLogErrorDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelProcess, JetFuelProcessDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicy, JetFuelPolicyDto>()
                .ReverseMap();

            Mapper.CreateMap<JetFuelPolicyControl, JetFuelPolicyControlDto>()
                .ReverseMap();
        }

        /// <summary>
        /// Maps for National invoice objects.
        /// </summary>
        private void NationalMaps()
        {
            Mapper.CreateMap<NationalJetFuelProcess, NationalJetFuelProcessDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelLogError, NationalJetFuelLogErrorDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelCost, NationalJetFuelCostDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicy, NationalJetFuelPolicyDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelPolicyControl, NationalJetFuelPolicyControlDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoiceControl, NationalJetFuelInvoiceControlDto>()
            .ForMember(c => c.DocumentStatusName, r => r.MapFrom(f => f.DocumentStatus.DocumentStatusName))
            .ForMember(c => c.RemittanceStatusName, r => r.MapFrom(f => f.RemittanceStatus.RemittanceStatusName))
            .ForMember(c => c.InvoiceCount, r => r.MapFrom(f => f.NationalJetFuelInvoiceDetails.Select(c => c.ElectronicInvoiceNumber).Distinct().Count()))
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoiceDetail, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoicePolicy, NationalJetFuelInvoicePolicyDto>()
                .ReverseMap();

            Mapper.CreateMap<NationalJetFuelInvoiceDetail, NationalJetFuelInvoiceDetailDto>()
                .ReverseMap();

            Mapper.CreateMap<DocumentStatus, DocumentStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<RemittanceStatus, RemittanceStatusDto>()
                .ReverseMap();

            Mapper.CreateMap<RemittanceSearch, RemittanceSearchDto>()
                .ReverseMap();
        }
    }
}
