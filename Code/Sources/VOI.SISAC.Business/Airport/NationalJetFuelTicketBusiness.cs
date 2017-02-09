//------------------------------------------------------------------------
// <copyright file="NationalJetFuelTicketBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Airports;
    using Dto.Airports;
    using Dto.Itineraries;
    using Entities.Airport;
    using Entities.Itineraries;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NationalJetFuelTicketBusiness Class
    /// </summary>
    public class NationalJetFuelTicketBusiness : INationalJetFuelTicketBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The National(NTL) Fuel Ticket repository
        /// </summary>
        private readonly INationalJetFuelTicketRepository ntlFuelTicketRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ntlFuelTicketRepository">The NTL fuel ticket repository.</param>
        public NationalJetFuelTicketBusiness(IUnitOfWork unitOfWork, INationalJetFuelTicketRepository ntlFuelTicketRepository)
        {
            this.unitOfWork = unitOfWork;
            this.ntlFuelTicketRepository = ntlFuelTicketRepository;
        }

        /// <summary>
        /// Adds the national jet fuel ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool AddNationalJetFuelTicket(NationalJetFuelTicketDto ticket)
        {
            var added = false;

            try
            {
                this.ntlFuelTicketRepository.Add(Mapper.Map<NationalJetFuelTicket>(ticket));
                this.unitOfWork.Commit();
                added = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return added;
        }

        /// <summary>
        /// Deletes the national jet fuel ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteNationalJetFuelTicket(NationalJetFuelTicketDto ticket)
        {
            var deleted = false;
            var ntlJetFuelTicket = new NationalJetFuelTicket();

            try
            {
                ntlJetFuelTicket = this.ntlFuelTicketRepository.FindById(ticket.NationalJetFuelTicketID);

                if (ntlJetFuelTicket != null)
                {
                    this.ntlFuelTicketRepository.Delete(ntlJetFuelTicket);
                    this.unitOfWork.Commit();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Finds the national jet fuel ticket.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public NationalJetFuelTicketDto FindNationalJetFuelTicket(long id)
        {
            var NationalJetFuelTicketDto = new NationalJetFuelTicketDto();

            try
            {
                NationalJetFuelTicketDto = Mapper.Map<NationalJetFuelTicketDto>(this.ntlFuelTicketRepository.FindById(id));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }

            return NationalJetFuelTicketDto;
        }

        /// <summary>
        /// Gets the national jet fuel tickets.
        /// </summary>
        /// <param name="ticketDto">The itinerary dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public List<NationalJetFuelTicketDto> GetNationalJetFuelTickets(NationalJetFuelTicketDto ticketDto)
        {
            var ntlTickets = new List<NationalJetFuelTicketDto>();
            var ticket = new NationalJetFuelTicket(ticketDto.Sequence, ticketDto.AirlineCode, ticketDto.FlightNumber, ticketDto.ItineraryKey, ticketDto.OperationTypeName);

            try
            {
                ntlTickets = Mapper.Map<List<NationalJetFuelTicketDto>>(this.ntlFuelTicketRepository.GetNationalJetFuelTickets(ticket));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return ntlTickets;
        }

        /// <summary>
        /// Updates the national jet fuel ticket.
        /// </summary>
        /// <param name="ticketDto">The ticket dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateNationalJetFuelTicket(NationalJetFuelTicketDto ticketDto)
        {
            var updated = false;
            var ntlTicket = new NationalJetFuelTicket();

            try
            {
                ntlTicket = this.ntlFuelTicketRepository.FindById(ticketDto.NationalJetFuelTicketID);

                ntlTicket.OperationTypeName = ticketDto.OperationTypeName;
                ntlTicket.FuelingDateStart = ticketDto.FuelingDateStart;
                ntlTicket.FuelingTimeStart = ticketDto.FuelingTimeStart;
                ntlTicket.FuelingDateEnd = ticketDto.FuelingDateEnd;
                ntlTicket.FuelingTimeEnd = ticketDto.FuelingTimeEnd;
                ntlTicket.ApronPosition = ticketDto.ApronPosition;
                ntlTicket.JetFuelProviderNumber = ticketDto.JetFuelProviderNumber;
                ntlTicket.IntoPlaneProviderNumber = ticketDto.IntoPlaneProviderNumber;
                ntlTicket.TicketNumber = ticketDto.TicketNumber;
                ntlTicket.FueledQtyLts = ticketDto.FueledQtyLts;
                ntlTicket.RemainingQtyKgs = ticketDto.RemainingQtyKgs;
                ntlTicket.RequestedQtyKgs = ticketDto.RequestedQtyKgs;
                ntlTicket.FueledQtyKgs = ticketDto.FueledQtyKgs;
                ntlTicket.DensityFactor = ticketDto.DensityFactor;
                ntlTicket.AorUserID = ticketDto.AorUserID;
                ntlTicket.SupplierResponsible = ticketDto.SupplierResponsible;
                ntlTicket.Remarks = ticketDto.Remarks;

                this.ntlFuelTicketRepository.Update(ntlTicket);
                this.unitOfWork.Commit();
                updated = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return updated;
        }
    }
}
