//-----------------------------------------------------------------------------
// <copyright file="InternationalFuelContractBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International Fuel Contract Business
    /// </summary>
    public class InternationalFuelContractBusiness : IInternationalFuelContractBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The FunctionalArea repository
        /// </summary>
        private readonly IInternationalFuelContractRepository internationalFuelContractRepository;

        /// <summary>
        /// The Massive Upload Business
        /// </summary>
        private readonly IMassiveUploadBusiness massiveUploadBusiness;

        /// <summary>
        /// The Generic Catalog Business
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelContractBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="internationalFuelContractRepository">The International Fuel Contract Repository</param>
        /// <param name="massiveUploadBusiness">The Massive Upload Business</param>
        /// <param name="generic">The Generic Catalog Business</param>
        public InternationalFuelContractBusiness(
            IUnitOfWork unitOfWork,
            IInternationalFuelContractRepository internationalFuelContractRepository,
            IMassiveUploadBusiness massiveUploadBusiness,
            IGenericCatalogBusiness generic)
        {
            this.internationalFuelContractRepository = internationalFuelContractRepository;
            this.massiveUploadBusiness = massiveUploadBusiness;
            this.generic = generic;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get All Searched International Fuel Contracts
        /// </summary>
        /// <param name="entity">entity parameter</param>
        /// <param name="statusSearch">status Search</param>
        /// <returns> List of InternationalFuelContractDto</returns>
        public IList<InternationalFuelContractDto> GetAllSearchedInternationalFuelContracts(InternationalFuelContractDto entity, string statusSearch)
        {
            try
            {
                IList<InternationalFuelContract> internationalFuelContract = this.internationalFuelContractRepository.SearchInternationalFuelContracts(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity), statusSearch).ToList();
                IList<InternationalFuelContractDto> internationalFuelContractDto = new List<InternationalFuelContractDto>();

                internationalFuelContractDto = Mapper.Map<IList<InternationalFuelContract>, IList<InternationalFuelContractDto>>(internationalFuelContract);
                return internationalFuelContractDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the InternationalFuelContract by identifier.
        /// </summary>
        /// <param name="entity">The InternationalFuelContract Entity.</param>
        /// <returns>
        /// Entity InternationalFuelContract.
        /// </returns>
        public InternationalFuelContractDto FindInternationalFuelContractById(InternationalFuelContractDto entity)
        {
            try
            {
                InternationalFuelContract internationalFuelContract = this.internationalFuelContractRepository.FindById(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity));
                InternationalFuelContractDto internationalFuelContractDto = new InternationalFuelContractDto();
                internationalFuelContractDto = Mapper.Map<InternationalFuelContract, InternationalFuelContractDto>(internationalFuelContract);

                return internationalFuelContractDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the InternationalFuelContract.
        /// </summary>
        /// <param name="internationalFuelContractDto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddInternationalFuelContract(InternationalFuelContractDto internationalFuelContractDto)
        {
            if (internationalFuelContractDto.InternationalFuelContractConcepts.Count() > 0)
            {
                internationalFuelContractDto.InternationalFuelContractConcepts.ToList().ForEach(c =>
               {
                   c.EffectiveDate = internationalFuelContractDto.EffectiveDate;
                   c.AirlineCode = internationalFuelContractDto.AirlineCode;
                   c.StationCode = internationalFuelContractDto.StationCode;
                   c.ServiceCode = internationalFuelContractDto.ServiceCode;
                   c.ProviderNumberPrimary = internationalFuelContractDto.ProviderNumberPrimary;
               });
            }

            if (internationalFuelContractDto == null)
            {
                return false;
            }

            if (this.IsInternationalFuelContractDuplicate(internationalFuelContractDto))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                InternationalFuelContract internationalFuelContract = new InternationalFuelContract();
                internationalFuelContractDto.Status = true;
                internationalFuelContract = Mapper.Map<InternationalFuelContract>(internationalFuelContractDto);
                this.internationalFuelContractRepository.Add(internationalFuelContract);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }        

        /// <summary>
        /// Deletes the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteInternationalFuelContract(InternationalFuelContractDto entity)
        {
            if (entity == null)
            {
                return false;
            } 

            try
            {
                InternationalFuelContract internationalFuelContract = this.internationalFuelContractRepository.FindById(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity));

                if (internationalFuelContract != null)
                {
                    //// Para borrado lógico
                    this.internationalFuelContractRepository.DeleteEntity(internationalFuelContract);
                    this.unitOfWork.Commit();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Inactive the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="endDateContract">The DateEndContract.</param>
        /// <returns>true if was updated else false</returns>
        public bool InactiveInternationalFuelContract(InternationalFuelContractDto entity, DateTime? endDateContract)
        {            
            if (endDateContract == null)
            {
                return false;
            }

            // End date must be greater than effective date
            if (entity.EffectiveDate.CompareTo(endDateContract.Value) >= 0)
            {
                return false;
            }

            try
            {
                InternationalFuelContractDto internationalFuelContractDto = new InternationalFuelContractDto();
                InternationalFuelContract internationalFuelContract = this.internationalFuelContractRepository.FindById(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity));
                
                if (internationalFuelContract == null)
                {
                    throw new BusinessException(Messages.FailedFindRecord);
                }

                internationalFuelContractDto = new InternationalFuelContractDto
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = internationalFuelContract.AirlineCode,
                    StationCode = internationalFuelContract.StationCode,
                    ServiceCode = internationalFuelContract.ServiceCode,
                    ProviderNumberPrimary = internationalFuelContract.ProviderNumberPrimary,
                    CCNumber = internationalFuelContract.CCNumber,
                    AccountingAccountNumber = internationalFuelContract.AccountingAccountNumber,
                    LiabilityAccountNumber = internationalFuelContract.LiabilityAccountNumber,
                    CurrencyCode = internationalFuelContract.CurrencyCode,
                    AircraftRegistCCFlag = internationalFuelContract.AircraftRegistCCFlag,
                    OperationTypeID = internationalFuelContract.OperationTypeID,
                    Status = false
                };

                if (this.IsInternationalFuelContractDuplicate(internationalFuelContractDto))
                {
                    throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
                }

                InternationalFuelContract fuelContractInactive = new InternationalFuelContract
                {
                    EffectiveDate = (DateTime)endDateContract,
                    AirlineCode = internationalFuelContract.AirlineCode,
                    StationCode = internationalFuelContract.StationCode,
                    ServiceCode = internationalFuelContract.ServiceCode,
                    ProviderNumberPrimary = internationalFuelContract.ProviderNumberPrimary,
                    CCNumber = internationalFuelContract.CCNumber,
                    AccountingAccountNumber = internationalFuelContract.AccountingAccountNumber,
                    LiabilityAccountNumber = internationalFuelContract.LiabilityAccountNumber,
                    CurrencyCode = internationalFuelContract.CurrencyCode,
                    AircraftRegistCCFlag = internationalFuelContract.AircraftRegistCCFlag,
                    OperationTypeID = internationalFuelContract.OperationTypeID,
                    Status = false
                };

                this.internationalFuelContractRepository.Add(fuelContractInactive);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateInternationalFuelContract(InternationalFuelContractDto entity)
        {
            try
            {
                if (entity != null)
                {
                    InternationalFuelContract internationalFuelContract = this.internationalFuelContractRepository.FindById(Mapper.Map<InternationalFuelContractDto, InternationalFuelContract>(entity));
                    internationalFuelContract.AircraftRegistCCFlag = entity.AircraftRegistCCFlag;
                    internationalFuelContract.CCNumber = entity.CCNumber;
                    internationalFuelContract.AccountingAccountNumber = entity.AccountingAccountNumber;
                    internationalFuelContract.LiabilityAccountNumber = entity.LiabilityAccountNumber;
                    internationalFuelContract.OperationTypeID = entity.OperationTypeID;
                    internationalFuelContract.CurrencyCode = entity.CurrencyCode;

                    this.internationalFuelContractRepository.Update(internationalFuelContract);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives InternationalFuelContract.
        /// </summary>
        /// <returns>InternationalFuelContracts Actives.</returns>
        public IList<InternationalFuelContractDto> GetActivesLastEffectiveDateInternationalFuelContracts()
        {
            try
            {
                DateTime currentDate = this.internationalFuelContractRepository.GetServerDate();
                List<InternationalFuelContract> activeContracts = this.internationalFuelContractRepository.GetAll().ToList();
                List<InternationalFuelContract> effectiveDates = activeContracts
                    .Where(c => c.EffectiveDate <= currentDate)
                    .GroupBy(con => new
                    {
                        con.AirlineCode,
                        con.StationCode,
                        con.ServiceCode,
                        con.ProviderNumberPrimary,
                    })
                    .Select(n => new InternationalFuelContract
                    {
                        EffectiveDate = n.Max(c => c.EffectiveDate),
                        AirlineCode = n.Key.AirlineCode,
                        StationCode = n.Key.StationCode,
                        ServiceCode = n.Key.ServiceCode,
                        ProviderNumberPrimary = n.Key.ProviderNumberPrimary,
                    })
                    .ToList();

                List<InternationalFuelContract> effectiveContractToday = activeContracts
                                            .Join(
                                                effectiveDates,
                                                c => new
                                                {
                                                    c.EffectiveDate,
                                                    c.AirlineCode,
                                                    c.StationCode,
                                                    c.ServiceCode,
                                                    c.ProviderNumberPrimary
                                                },
                                                d => new
                                                {
                                                    d.EffectiveDate,
                                                    d.AirlineCode,
                                                    d.StationCode,
                                                    d.ServiceCode,
                                                    d.ProviderNumberPrimary
                                                },
                                                (c, d) => c)
                                           .Where(c => c.Status).ToList();

                List<InternationalFuelContractDto> effectiveContractTodayDto = new List<InternationalFuelContractDto>();
                if (effectiveContractToday != null)
                {
                    effectiveContractTodayDto = Mapper.Map<List<InternationalFuelContract>, List<InternationalFuelContractDto>>(effectiveContractToday);
                }

                return effectiveContractTodayDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// ValidateFuelContracts the InternationalFuelContract.
        /// </summary>
        /// <param name="contracts">The entity.</param>
        /// <returns>IList de string</returns>
        public IList<string> ValidateFuelContracts(IList<InternationalFuelContractDto> contracts)
        {
            IList<string> errores = new List<string>();
            IList<string> exitosaValidacion = new List<string>();
            errores = this.massiveUploadBusiness.InternationalFuelContractAddRange(contracts);

            if (errores.Count == 0)
            {
                IList<GenericCatalogDto> serviceList = new List<GenericCatalogDto>();
                IList<GenericCatalogDto> serviceListFilter = new List<GenericCatalogDto>();
                serviceList = this.generic.GetServiceCatalog();
                foreach (GenericCatalogDto item in serviceList)
                {
                    if (item.Id.Contains("-EXT"))
                    {
                        serviceListFilter.Add(item);
                    }
                }

                foreach (InternationalFuelContractDto itemContract in contracts)
                {
                    string keyContract;
                    keyContract = "{ EffectiveDate: " + itemContract.EffectiveDate.ToString() + " , Airline:"
                                + itemContract.AirlineCode.ToString() + " , Airport:"
                                + itemContract.StationCode.ToString() + " , Service:"
                                + itemContract.ServiceCode.ToString() + " , Provider Number:"
                                + itemContract.ProviderNumberPrimary.ToString() + " }";

                    //// validar reglas de negocio
                    ////Servicios de Combustible

                    ////valida que sea servicio de combustible
                    ValidateFuelService(errores, itemContract, keyContract, serviceListFilter);

                    ////Si aplica centro de costos buscar que sea compatible con aerolinea
                    ValidateCostCenterAirline(errores, itemContract, keyContract);

                    ////Si aplica matricula como CC entonces CC debe ser vacio
                    ValidateCostCenterEmpty(errores, itemContract, keyContract);

                    ////Si no aplica matricula como CC entonces CC no debe ser vacio 
                    ValidateCostCenterFill(errores, itemContract, keyContract);
                    
                    ////Que no haya conceptos repetidos
                    ValidateConceptsDuplicatedFullKey(errores, itemContract, keyContract);

                    #region posible cambio posterior en carga

                    //////Validar que no haya conceptos repetidos por providerNumber-FuelConceptTypeCode solo ara tipo jetfuel e into-plane
                    //if (itemContract.InternationalFuelContractConcepts.Count() > 0)
                    //{
                    //    int numConcepts = itemContract.InternationalFuelContractConcepts.Select(c => c.ProviderNumber + c.FuelConceptTypeCode).ToList().Count();
                    //    int numConceptsDistinct = itemContract.InternationalFuelContractConcepts.Select(c => c.ProviderNumber + c.FuelConceptTypeCode).ToList().Distinct().Count();

                    //    if (numConceptsDistinct < numConcepts)
                    //    {
                    //        errores.Add("Contract repeated concepts in " + keyContract);              
                    //    }                                            
                    //}

                    #endregion

                    ////Validar que haya un jet-fuel igual a proveedor primario y que haya un into-plane
                    ValidateJetFuelIntoPlane(errores, itemContract, keyContract);

                    if (this.IsInternationalFuelContractDuplicate(itemContract))
                    {
                        errores.Add("Effective Dateis is less than Maximun Date or Existing Contract in " + keyContract);
                    }
                }
            }

            if (errores.Count == 0)
            {
                this.AddMassiveFuelContracts(contracts);
                return errores;      
            }
            else
            {
                return errores;         
            }
        }

        private static void ValidateJetFuelIntoPlane(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract)
        {
            if (itemContract.InternationalFuelContractConcepts.Count() > 0)
            {
                var jetFuel = itemContract.InternationalFuelContractConcepts.FirstOrDefault(c => c.ProviderNumber.Contains(itemContract.ProviderNumberPrimary) && c.FuelConceptTypeCode == "JTFL");
                var intoPlane = itemContract.InternationalFuelContractConcepts.FirstOrDefault(c => c.FuelConceptTypeCode == "INTPL");

                if (jetFuel == null)
                {
                    errores.Add("You must have at least one fuel concept name JET FUEL with the primary providers  of contract in " + keyContract);
                }

                if (intoPlane == null)
                {
                    errores.Add("You must have at least one concept name INTO-PLANE' in " + keyContract);
                }
            }
        }

        private static void ValidateConceptsDuplicatedFullKey(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract)
        {
            if (itemContract.InternationalFuelContractConcepts.Count() > 0)
            {
                var conceptDistinct = (from ssi in itemContract.InternationalFuelContractConcepts         //// here I choose each field I want to group by
                                       group ssi by new { ssi.FuelConceptID, ssi.ProviderNumber } into g
                                       select new { FuelConceptID = g.Key.FuelConceptID, ProviderNumber = g.Key.ProviderNumber }).ToList();
                ////validar que los conceptos sean unicos
                if (conceptDistinct.Count() != itemContract.InternationalFuelContractConcepts.Count())
                {
                    errores.Add("There are repeated Concepts in Contract " + keyContract);
                }
            }
        }

        private static void ValidateCostCenterFill(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract)
        {
            if ((!itemContract.AircraftRegistCCFlag) && string.IsNullOrEmpty(itemContract.CCNumber))
            {
                errores.Add("If AircraftRegistCCFlag is set to 0 then Cost Center must hava a value in " + keyContract);
            }
        }

        private static void ValidateCostCenterEmpty(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract)
        {
            itemContract.CCNumber = itemContract.CCNumber == string.Empty ? null : itemContract.CCNumber;
            if (itemContract.AircraftRegistCCFlag && !string.IsNullOrEmpty(itemContract.CCNumber))
            {
                errores.Add("If AircraftRegistCCFlag is set to 1 then Cost Center must be empty in " + keyContract);
            }
        }

        private void ValidateCostCenterAirline(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract)
        {
            if (!itemContract.AircraftRegistCCFlag)
            {
                var costCenterList = this.generic.GetCostCenterCatalog(itemContract.AirlineCode, string.Empty);
                var costCenterFound = costCenterList.FirstOrDefault(e => e.Id == itemContract.CCNumber);
                if (costCenterFound == null)
                {
                    errores.Add("The cost center" + itemContract.CCNumber + " does not match with the airline in " + keyContract);
                }
            }
        }

        private void ValidateFuelService(IList<string> errores, InternationalFuelContractDto itemContract, string keyContract, IList<GenericCatalogDto> serviceListFilter)
        {            
            var serviceFound = serviceListFilter.FirstOrDefault(e => e.Id == itemContract.ServiceCode);
            if (serviceFound == null)
            {
                errores.Add("Service " + itemContract.ServiceCode.ToString() + " not Valid in " + keyContract);
            }
        }

        /// <summary>
        /// Add Massive Fuel Contracts
        /// </summary>
        /// <param name="contracts">contracts List</param>
        /// <returns>true or false </returns>
        private bool AddMassiveFuelContracts(IList<InternationalFuelContractDto> contracts)
        {            
            foreach (InternationalFuelContractDto internationalFuelContractDto in contracts)
            {
                internationalFuelContractDto.Status = true;
                if (internationalFuelContractDto.InternationalFuelContractConcepts.Count() > 0)
                {
                    ////copiar key en conceptos
                    internationalFuelContractDto.InternationalFuelContractConcepts.ToList().ForEach(c =>
                    {
                        c.EffectiveDate = internationalFuelContractDto.EffectiveDate;
                        c.AirlineCode = internationalFuelContractDto.AirlineCode;
                        c.StationCode = internationalFuelContractDto.StationCode;
                        c.ServiceCode = internationalFuelContractDto.ServiceCode;
                        c.ProviderNumberPrimary = internationalFuelContractDto.ProviderNumberPrimary;
                    });
                }
            }

            try
            {
                this.internationalFuelContractRepository.AddRange(Mapper.Map<IList<InternationalFuelContract>>(contracts));    
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// GetActivesFuelContracts
        /// </summary>
        /// <returns></returns>
        public IList<InternationalFuelContractDto> GetActivesFuelContracts(string airlineCode, string stationCode, string operationTypeName)
        {
            try
            {
                IList<InternationalFuelContractDto> internationalFuelContractList = new List<InternationalFuelContractDto>();

                internationalFuelContractList = Mapper.Map<List<InternationalFuelContract>, 
                                                           List<InternationalFuelContractDto>>
                                (this.internationalFuelContractRepository.GetActivesFuelContracts(airlineCode, stationCode).ToList());

                if(operationTypeName == "SALIDA")
                return internationalFuelContractList.Where(x => x.OperationTypeID == 1 || x.OperationTypeID == 3).ToList();
                else
                return internationalFuelContractList.Where(x => x.OperationTypeID == 2 || x.OperationTypeID == 3).ToList();
                
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Is International Fuel Contract Duplicate
        /// </summary>
        /// <param name="internationalFuelContractDto">international Fuel Contract Dto</param>
        /// <returns>true or false</returns>
        private bool IsInternationalFuelContractDuplicate(InternationalFuelContractDto internationalFuelContractDto)
        {
            DateTime maxDate = this.internationalFuelContractRepository.GetAllContractsByMaxDate(Mapper.Map<InternationalFuelContract>(internationalFuelContractDto));
            return internationalFuelContractDto.EffectiveDate.Date > maxDate.Date ? false : true;
        }
    }
}
