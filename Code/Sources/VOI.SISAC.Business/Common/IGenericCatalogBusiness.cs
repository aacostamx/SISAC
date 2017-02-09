//------------------------------------------------------------------------
// <copyright file="GenericCatalogBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Common
{
    using Dto.Airports;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// Contract for Generic Catalog Business
    /// </summary>
    public interface IGenericCatalogBusiness
    {
        /// <summary>
        /// Gets the airline catalog.
        /// </summary>
        /// <returns>Generic Catalog with information for airlines.</returns>
        IList<GenericCatalogDto> GetAirlineCatalog();

        /// <summary>
        /// Gets the airports catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetAirportsCatalog();

        /// <summary>
        /// Gets the national airports catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetNationalAirportsCatalog();

        /// <summary>
        /// Gets the service catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetServiceCatalog();

        /// <summary>
        /// Gets the national jet fuel services catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetNationalJetFuelServicesCatalog();

        /// <summary>
        /// Gets the provider catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetProviderCatalog();

        /// <summary>
        /// Gets the cost center catalog by Airline.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetCostCenterCatalog();

        /// <summary>
        /// Gets the cost center catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetCostCenterCatalog(string airlineCode, string selectItem);

        /// <summary>
        /// Gets the accounting accounts catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetAccountingAccountsCatalog();

        /// <summary>
        /// Gets the liability accounts catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetLiabilityAccountsCatalog();

        /// <summary>
        /// Gets the taxes catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetTaxesCatalog();

        /// <summary>
        /// Gets the currency catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetCurrencyCatalog();

        /// <summary>
        /// Gets the airplane weight measure type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetAirplaneWeightMeasureTypeCatalog();

        /// <summary>
        /// Gets the airplane weight type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetAirplaneWeightTypeCatalog();

        /// <summary>
        /// Gets the operation type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetOperationTypeCatalog();

        /// <summary>
        /// Gets the service calculation type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetServiceCalculationTypeCatalog();

        /// <summary>
        /// Gets the service type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetServiceTypeCatalog();

        /// <summary>
        ///  Gets fuel concepts
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetFuelConceptCatalog();

        /// <summary>
        ///  Gets fuel concept types
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetFuelConceptTypeCatalog();

        /// <summary>
        /// Gets charge factor type
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetChargeFactorTypeCatalog();

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetUserCatalog();

        /// <summary>
        /// Gets Crews
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetCrewCatalog();

        /// <summary>
        /// Get Crew Types
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetCrewSobCatalog();

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetMenu();

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetPermission();

        /// <summary>
        /// GetDepartmentCatalog
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetDepartmentCatalog();

        /// <summary>
        /// Gets the profile catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetProfileCatalog();

        /// <summary>
        /// Gets the role catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetRoleCatalog();

        /// <summary>
        /// GetUserCatalog by airport and roleName
        /// </summary>
        /// <param name="airport"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IList<GenericCatalogDto> GetUserCatalog(string airport, string roleName);

        /// <summary>
        /// Get Jet Fuel Process Catalog
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetJetFuelProcessCatalog();

        /// <summary>
        /// Gets the national jet fuel process catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetNationalJetFuelProcessCatalog();

        /// <summary>
        /// Gets the delay catalog.
        /// </summary>
        /// <returns>
        /// Generic catalog for the delays information.
        /// </returns>
        IList<GenericCatalogDto> GetDelayCatalog();

        /// <summary>
        /// Gets the tolerance type catalog.
        /// </summary>
        /// <returns></returns>
        IList<GenericCatalogDto> GetToleranceTypeCatalog();
    }
}
