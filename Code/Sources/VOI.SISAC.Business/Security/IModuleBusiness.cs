//------------------------------------------------------------------------
// <copyright file="IModuleBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Security
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Security;

    public interface IModuleBusiness
    {
        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <returns></returns>
        IList<ModuleDto> GetModule();

        /// <summary>
        /// Finds the module by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ModuleDto FindModuleById(string id);

        /// <summary>
        /// Adds the module.
        /// </summary>
        /// <param name="moduleDto">The module dto.</param>
        /// <returns></returns>
        bool AddModule(ModuleDto moduleDto);

        /// <summary>
        /// Updates the module.
        /// </summary>
        /// <param name="moduleDto">The module dto.</param>
        /// <returns></returns>
        bool UpdateModule(ModuleDto moduleDto);

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="moduleDto">The module dto.</param>
        /// <returns></returns>
        bool DeleteModule(ModuleDto moduleDto);
    }
}
