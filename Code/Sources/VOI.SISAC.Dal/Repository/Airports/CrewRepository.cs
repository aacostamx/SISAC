//------------------------------------------------------------------------
// <copyright file="CrewRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using Entities.Paged;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Repository Crew
    /// </summary>
    public class CrewRepository : Repository<Crew>, ICrewRepository
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CrewRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #endregion

        #region ICrewRepository Members        
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity Crew
        /// </returns>
        public Crew FindById(long id)
        {
            var crew = this.DbContext.Crews.Where(c => c.CrewID == id).FirstOrDefault();
            return crew;
        }

        /// <summary>
        /// Gets the active crew.
        /// </summary>
        /// <returns>
        /// Collection Crew
        /// </returns>
        public ICollection<Crew> GetActiveCrew()
        {
            return this.DbContext.Crews.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validates if exist employee number.
        /// </summary>
        /// <param name="employeNumbers">The employee numbers.</param>
        /// <returns>return List string that matches </returns>
        public List<string> ValidateIfExistEmployeNumber(List<string> employeNumbers)
        {
            List<string> matches = new List<string>();
            List<Crew> crews = this.DbContext.Crews.Where(c => c.Status).ToList();

            matches = crews.Where(x => employeNumbers.Contains(x.EmployeeNumber)).Select(x => x.EmployeeNumber).ToList();

            return matches;
        }

        /// <summary>
        /// Validates the name of if exist nick.
        /// </summary>
        /// <param name="nickName">Name of the nick.</param>
        /// <returns>return List string that matches </returns>
        public List<string> ValidateIfExistNickName(List<string> nickName)
        {
            List<string> matches = new List<string>();
            List<Crew> crews = this.DbContext.Crews.Where(c => c.Status).ToList();

            matches = crews.Where(x => nickName.Contains(x.NickName)).Select(x => x.NickName).ToList();

            return matches;
        }

        /// <summary>
        /// Validates if exist nick name sabre.
        /// </summary>
        /// <param name="nickNameSabre">The nick name sabre.</param>
        /// <returns>return List string that matches </returns>
        public List<string> ValidateIfExistNickNameSabre(List<string> nickNameSabre)
        {
            List<string> matches = new List<string>();
            List<Crew> crews = this.DbContext.Crews.Where(c => c.Status).ToList();

            matches = crews.Where(x => nickNameSabre.Contains(x.NickNameSabre)).Select(x => x.NickNameSabre).ToList();

            return matches;
        }

        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="employeNumber">The employee number.</param>
        /// <param name="nickName">Name of the nick.</param>
        /// <param name="nickNameSabre">The nick name sabre.</param>
        /// <returns>return the number of matches</returns>
        public List<string> ValidateFields(string employeNumber, string nickName, string nickNameSabre)
        {
            List<string> errors = new List<string>();
            List<Crew> crews = this.DbContext.Crews.Where(c => c.Status && (c.EmployeeNumber == employeNumber ||
                                                          c.NickName == nickName ||
                                                          c.NickNameSabre == nickNameSabre)).ToList();
            if (crews.Count > 0)
            {
                if (crews.FirstOrDefault().EmployeeNumber != null && crews.FirstOrDefault().EmployeeNumber == employeNumber)
                {
                    errors.Add("Employe Number alredy exist");
                    errors.Add(employeNumber);
                }

                if (crews.FirstOrDefault().NickName != null && crews.FirstOrDefault().NickName == nickName)
                {
                    errors.Add("NickName alredy exist");
                    errors.Add(nickName);
                }

                if (crews.FirstOrDefault().NickNameSabre != null && crews.FirstOrDefault().NickNameSabre == nickNameSabre)
                {
                    errors.Add("NickNameSabre Number alredy exist");
                    errors.Add(nickNameSabre);
                }
            }

            return errors;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="crews">The crews.</param>
        public void AddRange(IList<Crew> crews)
        {
            this.DbContext.Crews.AddRange(crews);
        }

        /// <summary>
        /// Get Crew to Captain
        /// </summary>
        /// <returns>
        /// The active captains and copilots.
        /// </returns>
        public List<Crew> GetActivePilots()
        {
            return this.DbContext.Crews.Where(c => c.Status
                && (c.CrewTypeID.Contains("CAP") || c.CrewTypeID.Contains("COP")))
                .OrderBy(c => c.CrewTypeID)
                .ThenBy(c => c.NickName)
                .ToList();
        }

        /// <summary>
        /// Get Crew to Sobrecargos
        /// </summary>
        /// <returns>
        /// The active stewwardess.
        /// </returns>
        public List<Crew> GetActiveStewardess()
        {
            return this.DbContext.Crews.Where(c => c.Status
                && (c.CrewTypeID.Contains("SOB") || (c.CrewTypeID.Contains("JDC"))))
                .OrderBy(c => c.CrewTypeID)
                .ThenBy(c => c.NickName)
                .ToList();
        }

        /// <summary>
        /// Finds the crew by employee number.
        /// </summary>
        /// <param name="employeeNumber">The employee number.</param>
        /// <returns></returns>
        public Crew FindCrewByEmployeeNumber(string employeeNumber)
        {
            Crew crew = this.DbContext.Crews.Where(c => c.EmployeeNumber == employeeNumber).FirstOrDefault();
            return crew;
        }

        /// <summary>
        /// Gets the crew paged.
        /// </summary>
        /// <param name="paged">The paged.</param>
        /// <returns></returns>
        public List<Crew> GetCrewPaged(Paged paged)
        {
            int skip = 0;
            List<Crew> crew = new List<Crew>();
            IQueryable<Crew> query = Enumerable.Empty<Crew>().AsQueryable();

            try
            {
                skip = (paged.pageNumber - 1) * paged.pageSize;
                query = this.DbContext.Crews.Where(c => c.Status).OrderBy(c => c.CrewID).Skip(skip).Take(paged.pageSize);
                crew = query.ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return crew;
        }
        #endregion
    }
}
