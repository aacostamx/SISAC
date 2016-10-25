//----------------------------------------------------------------------------
// <copyright file="JetFuelTicketBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// JetFuelTicketBusiness
    /// </summary>
    public class JetFuelTicketBusiness : IJetFuelTicketBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Jet Fuel Ticket Repository
        /// </summary>
        private readonly IJetFuelTicketRepository jetFuelTicketRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelTicketBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="jetFuelTicketRepository"></param>
        public JetFuelTicketBusiness(
            IUnitOfWork unitOfWork,
            IJetFuelTicketRepository jetFuelTicketRepository)
        {
            this.jetFuelTicketRepository = jetFuelTicketRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetJetFuelTickets
        /// </summary>
        /// <param name="itineraryDto"></param>
        /// <param name="operationTypeName"></param>
        /// <returns></returns>
        public IList<JetFuelTicketDto> GetJetFuelTickets(ItineraryDto itineraryDto, string operationTypeName)
        {
            try
            {
                IList<JetFuelTicket> jetFuelTickets =
                    this.jetFuelTicketRepository.GetJetFuelTickets(Mapper.Map<ItineraryDto, Itinerary>(itineraryDto), operationTypeName).ToList();
                IList<JetFuelTicketDto> jetFuelTicketsDto = new List<JetFuelTicketDto>();

                jetFuelTicketsDto = Mapper.Map<IList<JetFuelTicket>, IList<JetFuelTicketDto>>(jetFuelTickets);
                return jetFuelTicketsDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// FindJetFuelTicketById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JetFuelTicketDto FindJetFuelTicketById(long id)
        {
            try
            {
                JetFuelTicket jetFuelTicket = this.jetFuelTicketRepository.FindById(id);
                JetFuelTicketDto jetFuelTicketDto = new JetFuelTicketDto();
                jetFuelTicketDto = Mapper.Map<JetFuelTicket, JetFuelTicketDto>(jetFuelTicket);

                return jetFuelTicketDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// AddJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddJetFuelTicket(JetFuelTicketDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            //if (this.IsFuelConceptIDDuplicate(entity.FuelConceptID))
            //{
            //    throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            //}

            try
            {
                this.jetFuelTicketRepository.Add(Mapper.Map<JetFuelTicket>(entity));
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// DeleteJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteJetFuelTicket(JetFuelTicketDto entity)
        {
            try
            {
                JetFuelTicket jetFuelTicket = this.jetFuelTicketRepository.FindById(entity.JetFuelTicketID);

                if (jetFuelTicket != null)
                {
                    this.jetFuelTicketRepository.Delete(jetFuelTicket);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// UpdateJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateJetFuelTicket(JetFuelTicketDto entity)
        {
            try
            {
                if (entity != null)
                {
                    JetFuelTicket jetFuelTicket = this.jetFuelTicketRepository.FindById(entity.JetFuelTicketID);

                    jetFuelTicket.OperationTypeName = entity.OperationTypeName;
                    jetFuelTicket.FuelingDate = entity.FuelingDate;
                    jetFuelTicket.FuelingTime = entity.FuelingTime;
                    jetFuelTicket.JetFuelProviderNumber = entity.JetFuelProviderNumber;
                    jetFuelTicket.IntoPlaneProviderNumber = entity.IntoPlaneProviderNumber;
                    jetFuelTicket.TicketNumber = entity.TicketNumber;
                    jetFuelTicket.FueledQtyGals = entity.FueledQtyGals;
                    jetFuelTicket.RemainingQry = entity.RemainingQry;
                    jetFuelTicket.RequestedQry = entity.RequestedQry;
                    jetFuelTicket.FueledQry = entity.FueledQry;
                    jetFuelTicket.DensityFactor = entity.DensityFactor;
                    jetFuelTicket.AorUserID = entity.AorUserID;
                    jetFuelTicket.SupplierResponsible = entity.SupplierResponsible;
                    jetFuelTicket.Remarks = entity.Remarks;
                    this.jetFuelTicketRepository.Update(jetFuelTicket);
                    this.unitOfWork.Commit();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }
    }
}
