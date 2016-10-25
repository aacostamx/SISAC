using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VOI.SISAC.Business.Dto.Catalogs;
using VOI.SISAC.Business.ExceptionBusiness;
using VOI.SISAC.Business.Resources;
using VOI.SISAC.Dal.Infrastructure;
using VOI.SISAC.Dal.Repository.Catalog;
using VOI.SISAC.Entities.Catalog;

namespace VOI.SISAC.Business.Catalog
{
    public class StatusOnBoardBusiness : IStatusOnBoardBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IStatusOnBoardRepository statusOnBoardRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusOnBoardBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="statusOnBoardRepository">The status on board repository.</param>
        public StatusOnBoardBusiness(IUnitOfWork unitOfWork, IStatusOnBoardRepository statusOnBoardRepository)
        {
            this.unitOfWork = unitOfWork;
            this.statusOnBoardRepository = statusOnBoardRepository;
        }
        
        #region IStatusOnBoardBusiness Members

        /// <summary>
        /// Gets all status on board.
        /// </summary>
        /// <returns>IList of Status on Board</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<StatusOnBoardDto> GetAllStatusOnBoard()
        {
            try
            {
                IList<StatusOnBoard> statusOnBoard = this.statusOnBoardRepository.GetAll().ToList();
                IList<StatusOnBoardDto> statusOnBoardDto = new List<StatusOnBoardDto>();

                statusOnBoardDto = Mapper.Map<IList<StatusOnBoard>, IList<StatusOnBoardDto>>(statusOnBoard);
                return statusOnBoardDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
