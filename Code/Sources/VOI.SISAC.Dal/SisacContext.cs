//------------------------------------------------------------------------
// <copyright file="SisacContext.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using Configuration;
    using Configuration.Airport;
    using Configuration.Catalog;
    using Configuration.Finance;
    using Configuration.Itineraries;
    using Configuration.Process;
    using Configuration.Security;
    using Entities.Airport;
    using Entities.Catalog;
    using Entities.Finance;
    using Entities.Itineraries;
    using Entities.Process;
    using Entities.Security;
    using Entities.Views;

    /// <summary>
    /// Context class
    /// </summary>
    public class SisacContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SisacContext"/> class.
        /// </summary>
        public SisacContext()
            : base("name=SisacDBConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        #region Finances
        /// <summary>
        /// Gets or sets the Currency entity.
        /// </summary>
        /// <value>
        /// Currency entity.
        /// </value>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets the accounting accounts.
        /// </summary>
        /// <value>
        /// The accounting accounts.
        /// </value>
        public DbSet<AccountingAccount> AccountingAccounts { get; set; }

        /// <summary>
        /// Gets or sets the cost centers.
        /// </summary>
        /// <value>
        /// The cost centers.
        /// </value>
        public DbSet<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Gets or sets the liability accounts.
        /// </summary>
        /// <value>
        /// The liability accounts.
        /// </value>
        public DbSet<LiabilityAccount> LiabilityAccounts { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public DbSet<Provider> Providers { get; set; }

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        public DbSet<Tax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the InternationalFuelContract.
        /// </summary>
        /// <value>
        /// The InternationalFuelContracts.
        /// </value>
        public DbSet<InternationalFuelContract> InternationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets the InternationalFuelContractConcept.
        /// </summary>
        /// <value>
        /// The InternationalFuelContractConcepts.
        /// </value>
        public DbSet<InternationalFuelContractConcept> InternationalFuelContractConcepts { get; set; }

        /// <summary>
        /// Gets or sets the InternationalFuelRate.
        /// </summary>
        /// <value>
        /// The InternationalFuelRates.
        /// </value>
        public DbSet<InternationalFuelRate> InternationalFuelRates { get; set; }

        /// <summary>
        /// Gets or sets the airport service contract.
        /// </summary>
        /// <value>
        /// The airport service contract.
        /// </value>
        public DbSet<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the exchange rates.
        /// </summary>
        /// <value>
        /// The exchange rates.
        /// </value>
        public DbSet<ExchangeRates> ExchangeRates { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public DbSet<NationalFuelContract> NationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concepts.
        /// </summary>
        /// <value>
        /// The national fuel contract concepts.
        /// </value>
        public DbSet<NationalFuelContractConcept> NationalFuelContractConcepts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel rates.
        /// </summary>
        /// <value>
        /// The national fuel rates.
        /// </value>
        public DbSet<NationalFuelRate> NationalFuelRates { get; set; }
        #endregion

        #region Airports

        /// <summary>
        /// Gets or sets the jet fuel tickets.
        /// </summary>
        /// <value>
        /// The jet fuel tickets.
        /// </value>
        public DbSet<JetFuelTicket> JetFuelTickets { get; set; }

        /// <summary>
        /// Gets or sets the airport services.
        /// </summary>
        /// <value>
        /// The airport services.
        /// </value>
        public DbSet<AirportService> AirportServices { get; set; }

        /// <summary>
        /// Gets or sets the FuelConceptType.
        /// </summary>
        /// <value>
        /// The airports.
        /// </value>
        public DbSet<FuelConceptType> FuelConceptType { get; set; }

        /// <summary>
        /// Gets or sets the airports.
        /// </summary>
        /// <value>
        /// The airports.
        /// </value>
        public DbSet<Airport> Airports { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public DbSet<Airline> Airlines { get; set; }

        /// <summary>
        /// Gets or sets the airplanes.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public DbSet<Airplane> Airplanes { get; set; }

        /// <summary>
        /// Gets or sets the airplane types.
        /// </summary>
        /// <value>
        /// The airplane types.
        /// </value>
        public DbSet<AirplaneType> AirplaneTypes { get; set; }

        /// <summary>
        /// Gets or sets the airport groups.
        /// </summary>
        /// <value>
        /// The airport groups.
        /// </value>
        public DbSet<AirportGroup> AirportGroups { get; set; }

        /// <summary>
        /// Gets or sets the compartment types.
        /// </summary>
        /// <value>
        /// The compartment types.
        /// </value>
        public DbSet<CompartmentType> CompartmentTypes { get; set; }

        /// <summary>
        /// Gets or sets the compartment type configuration.
        /// </summary>
        /// <value>
        /// The compartment type configuration.
        /// </value>
        public DbSet<CompartmentTypeConfig> CompartmentTypeConfigs { get; set; }

        /// <summary>
        /// Gets or sets the compartment type information.
        /// </summary>
        /// <value>
        /// The compartment type information.
        /// </value>
        public DbSet<CompartmentTypeInformation> CompartmentTypeInformations { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The gpus.
        /// </value>
        public DbSet<Gpu> Gpus { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The gpus.
        /// </value>
        public DbSet<Crew> Crews { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The Gpu Observations.
        /// </value>
        public DbSet<GpuObservation> GpuObservations { get; set; }

        /// <summary>
        /// Gets or sets the drinking waters.
        /// </summary>
        /// <value>
        /// The drinking waters.
        /// </value>
        public DbSet<DrinkingWater> DrinkingWaters { get; set; }


        /// <summary>
        /// Gets or sets the airport schedules.
        /// </summary>
        /// <value>The airport schedules.</value>
        public DbSet<AirportSchedule> AirportSchedules { get; set; }

        /// <summary>
        /// Gets or sets the type of the crew.
        /// </summary>
        /// <value>
        /// The type of the crew.
        /// </value>
        public DbSet<CrewType> CrewTypes { get; set; }

        /// <summary>
        /// Gets or sets the type of the FuelConcept.
        /// </summary>
        /// <value>
        /// The type of the FuelConcept.
        /// </value>
        public DbSet<FuelConcept> FuelConcepts { get; set; }

        /// <summary>
        /// Gets or sets the type of the Delay
        /// </summary>
        public DbSet<Delay> Delay { get; set; }

        /// <summary>
        /// Gets or sets the type of the Manifest Time Config
        /// </summary>
        public DbSet<ManifestTimeConfig> ManifestTimeConfig { get; set; }

        /// <summary>
        /// Gets or sets the passenger information.
        /// </summary>
        /// <value>
        /// The passenger information.
        /// </value>
        public DbSet<PassengerInformation> PassengerInformation { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel ticket.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket.
        /// </value>
        public DbSet<NationalJetFuelTicket> NationalJetFuelTicket { get; set; }

        /// <summary>
        /// Gets or sets the additional passenger information.
        /// </summary>
        /// <value>
        /// The additional passenger information.
        /// </value>
        public DbSet<AdditionalPassengerInformation> AdditionalPassengerInformation { get; set; }
        #endregion

        #region Catalogs


        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public DbSet<ReconciliationTolerance> ReconciliationTolerances { get; set; }

        /// <summary>
        /// Gets or sets the tolerance types.
        /// </summary>
        /// <value>
        /// The tolerance types.
        /// </value>
        public DbSet<ToleranceType> ToleranceTypes { get; set; }

        /// <summary>
        /// Gets or sets the Charge Factory Type
        /// </summary>
        public virtual DbSet<ChargeFactorType> ChargeFactorType { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Gets or sets the FunctionalAreas.
        /// </summary>
        /// <value>
        /// The FunctionalArea.
        /// </value>
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }

        /// <summary>
        /// Gets or sets the service types.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public DbSet<ServiceType> ServiceTypes { get; set; }

        /// <summary>
        /// Gets or sets the service calculation types.
        /// </summary>
        /// <value>
        /// The service calculation types.
        /// </value>
        public DbSet<ServiceCalculationType> ServiceCalculationTypes { get; set; }

        /// <summary>
        /// Gets or sets the operation types.
        /// </summary>
        /// <value>
        /// The operation types.
        /// </value>
        public DbSet<OperationType> OperationTypes { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight types.
        /// </summary>
        /// <value>
        /// The airplane weight types.
        /// </value>
        public DbSet<AirplaneWeightType> AirplaneWeightTypes { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight measure types.
        /// </summary>
        /// <value>
        /// The airplane weight measure types.
        /// </value>
        public DbSet<AirplaneWeightMeasureType> AirplaneWeightMeasureTypes { get; set; }

        /// <summary>
        /// Gets or sets the status on board.
        /// </summary>
        /// <value>
        /// The status on board.
        /// </value>
        public DbSet<StatusOnBoard> StatusOnBoard { get; set; }

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>
        /// The type of the schedule.
        /// </value>
        public DbSet<ScheduleType> ScheduleType { get; set; }

        /// <summary>
        /// Gets or sets the type of the movement.
        /// </summary>
        /// <value>
        /// The type of the movement.
        /// </value>
        public DbSet<MovementType> MovementType { get; set; }

        /// <summary>
        /// Gets or sets the procedure calculations.
        /// </summary>
        /// <value>
        /// The procedure calculations.
        /// </value>
        public DbSet<ProcedureCalculation> ProcedureCalculations { get; set; }
        #endregion

        #region Security
        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public DbSet<Department> Departments { get; set; }

        //// public DbSet<UserType> UserTypes { get; set; }

        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public DbSet<Module> Modules { get; set; }

        /// <summary>
        /// Gets or sets the module permissions.
        /// </summary>
        /// <value>
        /// The module permissions.
        /// </value>
        public DbSet<ModulePermission> ModulePermissions { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the profile roles.
        /// </summary>
        /// <value>
        /// The profile roles.
        /// </value>
        public DbSet<ProfileRole> ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the user profile roles.
        /// </summary>
        /// <value>
        /// The user profile roles.
        /// </value>
        public DbSet<UserProfileRole> UserProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the user airports.
        /// </summary>
        /// <value>
        /// The user airports.
        /// </value>
        public DbSet<UserAirport> UserAirports { get; set; }

        /// <summary>
        /// Gets or sets the menus.
        /// </summary>
        /// <value>
        /// The menus.
        /// </value>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// Gets or sets the page reports.
        /// </summary>
        /// <value>
        /// The page reports.
        /// </value>
        public DbSet<PageReport> PageReports { get; set; }
        #endregion

        #region Itineraries
        /// <summary>
        /// Gets or sets the itineraries.
        /// </summary>
        public DbSet<Itinerary> Itineraries { get; set; }

        /// <summary>
        /// Gets or sets the ItineraryLogs.
        /// </summary>
        public DbSet<ItineraryLog> ItineraryLogs { get; set; }

        /// <summary>
        /// Gets or sets the general document departure.
        /// </summary>
        public DbSet<GendecDeparture> GendecDepartures { get; set; }

        /// <summary>
        /// Gets or sets the general document arrival
        /// </summary>
        public DbSet<GendecArrival> GendecArrivals { get; set; }

        /// <summary>
        /// Gets or sets the manifests departure.
        /// </summary>
        /// <value>
        /// The manifests departure.
        /// </value>
        public DbSet<ManifestDeparture> ManifestsDeparture { get; set; }

        /// <summary>
        /// Gets or sets the additional departure information.
        /// </summary>
        /// <value>
        /// The additional departure information.
        /// </value>
        public DbSet<AdditionalDepartureInformation> AdditionalDepartureInformation { get; set; }

        /// <summary>
        /// Gets or sets the additional arrival information.
        /// </summary>
        /// <value>
        /// The additional arrival information.
        /// </value>
        public DbSet<AdditionalArrivalInformation> AdditionalArrivalInformation { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boardings.
        /// </summary>
        /// <value>
        /// The manifest departure boardings.
        /// </value>
        public DbSet<ManifestDepartureBoarding> ManifestDepartureBoardings { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding details.
        /// </summary>
        /// <value>
        /// The manifest departure boarding details.
        /// </value>
        public DbSet<ManifestDepartureBoardingDetail> ManifestDepartureBoardingDetails { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding informations.
        /// </summary>
        /// <value>
        /// The manifest departure boarding informations.
        /// </value>
        public DbSet<ManifestDepartureBoardingInformation> ManifestDepartureBoardingInformations { get; set; }

        /// <summary>
        /// Gets or sets the manifests arrival.
        /// </summary>
        /// <value>
        /// The manifests arrival.
        /// </value>
        public DbSet<ManifestArrival> ManifestsArrival { get; set; }

        /// <summary>
        /// Gets or sets the timeline.
        /// </summary>
        /// <value>
        /// The timeline.
        /// </value>
        public DbSet<Timeline> Timeline { get; set; }

        /// <summary>
        /// Gets or sets the timeline movement.
        /// </summary>
        /// <value>
        /// The timeline movement.
        /// </value>
        public DbSet<TimelineMovement> TimelineMovement { get; set; }

        /// <summary>
        /// Gets or sets the vw timeline order.
        /// </summary>
        /// <value>
        /// The vw timeline order.
        /// </value>
        public virtual DbSet<VW_TimelineOrder> VW_TimelineOrder { get; set; }
        #endregion

        #region Process
        /// <summary>
        /// Gets or sets the CalculationStatus
        /// </summary>
        public DbSet<CalculationStatus> CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmationStatus
        /// </summary>
        public DbSet<ConfirmationStatus> ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the StatusProcess
        /// </summary>
        public DbSet<StatusProcess> StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the JetFuelProcess
        /// </summary>
        public DbSet<JetFuelProcess> JetFuelProcess { get; set; }

        /// <summary>
        /// Gets or sets the JetFuelProvision
        /// </summary>
        public DbSet<JetFuelProvision> JetFuelProvisions { get; set; }

        /// <summary>
        /// Gets or sets the JetFuelLogError
        /// </summary>
        public DbSet<JetFuelLogError> JetFuelLogErrors { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel control policies.
        /// </summary>
        /// <value>
        /// The jet fuel control policies.
        /// </value>
        public DbSet<JetFuelPolicyControl> JetFuelControlPolicies { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel policies.
        /// </summary>
        /// <value>
        /// The jet fuel policies.
        /// </value>
        public DbSet<JetFuelPolicy> JetFuelPolicies { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel cost.
        /// </summary>
        /// <value>
        /// The national jet fuel cost.
        /// </value>
        public DbSet<NationalJetFuelCost> NationalJetFuelCost { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel log error.
        /// </summary>
        /// <value>
        /// The national jet fuel log error.
        /// </value>
        public DbSet<NationalJetFuelLogError> NationalJetFuelLogError { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel process.
        /// </summary>
        /// <value>
        /// The national jet fuel process.
        /// </value>
        public DbSet<NationalJetFuelProcess> NationalJetFuelProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel policy.
        /// </summary>
        /// <value>
        /// The national jet fuel policy.
        /// </value>
        public DbSet<NationalJetFuelPolicy> NationalJetFuelPolicy { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel policy control.
        /// </summary>
        /// <value>
        /// The national jet fuel policy control.
        /// </value>
        public DbSet<NationalJetFuelPolicyControl> NationalJetFuelPolicyControl { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice control.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice control.
        /// </value>
        public DbSet<NationalJetFuelInvoiceControl> NationalJetFuelInvoiceControl { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice detail.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice detail.
        /// </value>
        public DbSet<NationalJetFuelInvoiceDetail> NationalJetFuelInvoiceDetail { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice detail.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice detail.
        /// </value>
        public DbSet<NationalJetFuelInvoicePolicy> NationalJetFuelInvoicePolicy { get; set; }

        /// <summary>
        /// Gets or sets the document status.
        /// </summary>
        /// <value>
        /// The document status.
        /// </value>
        public virtual DbSet<DocumentStatus> DocumentStatus { get; set; }
        #endregion

        #region Stored Procedure
        /// <summary>
        /// Calculate International Fuel Stored
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <param name="typeProcess">The type process.</param>
        public virtual void CalculateInternationalFuel(string periodCode, int? typeProcess)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                var typeProcessParameter = typeProcess.HasValue ?
                    new SqlParameter("TypeProcess", typeProcess) :
                    new SqlParameter("TypeProcess", 0);

                this.Database.SqlQuery<object>("Process.CalculateInternationalFuel @PeriodCode, @TypeProcess", periodCodeParameter, typeProcessParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculate International Fuel Pending
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateInternationalFuelPending(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<object>("Process.CalculateInternationalFuelPending", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculate International FuelRevert
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateInternationalFuelRevert(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<object>("Process.CalculateInternationalFuelRevert", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculate International Fuel Total
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateInternationalFuelTotal(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<object>("Process.CalculateInternationalFuelTotal", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculates the national fuel.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <param name="typeProcess">The type process.</param>
        public virtual void CalculateNationalFuel(string periodCode, int? typeProcess)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                var typeProcessParameter = typeProcess.HasValue ?
                    new SqlParameter("TypeProcess", typeProcess) :
                    new SqlParameter("TypeProcess", 0);

                var result = this.Database.SqlQuery<object>("Process.CalculateNationalFuel @PeriodCode, @TypeProcess", periodCodeParameter, typeProcessParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculates the national fuel pending.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateNationalFuelPending(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<object>("Process.CalculateNationalFuelPending", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculates the national fuel revert.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateNationalFuelRevert(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<object>("Process.CalculateNationalFuelRevert", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculates the national fuel total.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public virtual void CalculateNationalFuelTotal(string periodCode)
        {
            try
            {
                var periodCodeParameter = periodCode != null ?
                    new SqlParameter("PeriodCode", periodCode) :
                    new SqlParameter("PeriodCode", string.Empty);

                this.Database.SqlQuery<int>("Process.CalculateNationalFuelTotal", periodCodeParameter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Save Policy Provisions
        /// </summary>
        /// <param name="startDateReal">The start date real.</param>
        /// <param name="endDateReal">The end date real.</param>
        /// <param name="startDateComplementary">The start date complementary.</param>
        /// <param name="endDateComplementary">The end date complementary.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="itemText">The item text.</param>
        /// <param name="dateValue">The date value.</param>
        /// <param name="datePosting">The date posting.</param>
        /// <param name="dateBase">The date base.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="providerCodes">The provider codes.</param>
        /// <param name="serviceCodes">The service codes.</param>
        /// <param name="stationCodes">The station codes.</param>
        /// <param name="userSend">The user send.</param>
        /// <returns>Policy identifier for the record.</returns>
        public virtual long SavePolicyProvisions(
            DateTime startDateReal,
            DateTime endDateReal,
            DateTime startDateComplementary,
            DateTime endDateComplementary,
            string headerText,
            string itemText,
            DateTime dateValue,
            DateTime datePosting,
            DateTime dateBase,
            string airlineCode,
            string providerCodes,
            string serviceCodes,
            string stationCodes,
            string userSend)
        {
            SqlParameter startDateRealParameter = startDateReal != default(DateTime) ?
                new SqlParameter("@StartDateReal", startDateReal) :
                new SqlParameter("@StartDateReal", new DateTime(1900, 1, 1));

            SqlParameter endDateRealParameter = endDateReal != default(DateTime) ?
                new SqlParameter("@EndDateReal", endDateReal) :
                new SqlParameter("@EndDateReal", new DateTime(1900, 1, 1));

            SqlParameter startDateComplementaryParameter = startDateComplementary != default(DateTime) ?
                new SqlParameter("@StartDateComp", startDateComplementary) :
                new SqlParameter("@StartDateComp", new DateTime(1900, 1, 1));

            SqlParameter endDateComplementaryParameter = endDateComplementary != default(DateTime) ?
                new SqlParameter("@EndDateComp", endDateComplementary) :
                new SqlParameter("@EndDateComp", new DateTime(1900, 1, 1));

            SqlParameter headerTextParameter = headerText != null ?
                new SqlParameter("@HeaderText", headerText) :
                new SqlParameter("@HeaderText", string.Empty);

            SqlParameter itemTextParameter = itemText != null ?
                new SqlParameter("@ItemText", itemText) :
                new SqlParameter("@ItemText", string.Empty);

            SqlParameter dateValueParameter = dateValue != default(DateTime) ?
                new SqlParameter("@DateValue", dateValue) :
                new SqlParameter("@DateValue", new DateTime(1900, 1, 1));

            SqlParameter datePostingParameter = datePosting != default(DateTime) ?
                new SqlParameter("@DatePosting", datePosting) :
                new SqlParameter("@DatePosting", new DateTime(1900, 1, 1));

            SqlParameter dateBaseParameter = dateBase != default(DateTime) ?
                new SqlParameter("@DateBase", dateBase) :
                new SqlParameter("@DateBase", new DateTime(1900, 1, 1));

            SqlParameter airlineCodeParameter = airlineCode != null ?
                new SqlParameter("@AirlineCode", airlineCode) :
                new SqlParameter("@AirlineCode", string.Empty);

            SqlParameter providerCodesParameter = providerCodes != null ?
                new SqlParameter("@ProviderCodes", providerCodes) :
                new SqlParameter("@ProviderCodes", string.Empty);

            SqlParameter serviceCodesParameter = serviceCodes != null ?
                new SqlParameter("@ServiceCodes", serviceCodes) :
                new SqlParameter("@ServiceCodes", string.Empty);

            SqlParameter stationCodesParameter = stationCodes != null ?
                new SqlParameter("@StationCodes", stationCodes) :
                new SqlParameter("@StationCodes", string.Empty);

            SqlParameter userSendParameter = userSend != null ?
                new SqlParameter("@UserSend", userSend) :
                new SqlParameter("@UserSend", string.Empty);

            return this.Database.SqlQuery<long>(
                "Process.SavePolizaProvisionesInt "
                + "@StartDateReal,"
                + "@EndDateReal,"
                + "@StartDateComp,"
                + "@EndDateComp,"
                + "@HeaderText,"
                + "@ItemText,"
                + "@DateValue,"
                + "@DatePosting,"
                + "@DateBase,"
                + "@AirlineCode,"
                + "@ProviderCodes,"
                + "@ServiceCodes,"
                + "@StationCodes,"
                + "@UserSend",
                startDateRealParameter,
                endDateRealParameter,
                startDateComplementaryParameter,
                endDateComplementaryParameter,
                headerTextParameter,
                itemTextParameter,
                dateValueParameter,
                datePostingParameter,
                dateBaseParameter,
                airlineCodeParameter,
                providerCodesParameter,
                serviceCodesParameter,
                stationCodesParameter,
                userSendParameter).SingleOrDefault();
        }

        /// <summary>
        /// Save Policy Provisions
        /// </summary>
        /// <param name="startDateReal">The start date real.</param>
        /// <param name="endDateReal">The end date real.</param>
        /// <param name="startDateComplementary">The start date complementary.</param>
        /// <param name="endDateComplementary">The end date complementary.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="itemText">The item text.</param>
        /// <param name="dateValue">The date value.</param>
        /// <param name="datePosting">The date posting.</param>
        /// <param name="dateBase">The date base.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="providerCodes">The provider codes.</param>
        /// <param name="serviceCodes">The service codes.</param>
        /// <param name="stationCodes">The station codes.</param>
        /// <param name="userSend">The user send.</param>
        /// <returns>Policy identifier for the record.</returns>
        public virtual IList<CurrencyTypeChage> VerifyPolicyProvissions(
            DateTime startDateReal,
            DateTime endDateReal,
            DateTime startDateComplementary,
            DateTime endDateComplementary,
            string headerText,
            string itemText,
            DateTime dateValue,
            DateTime datePosting,
            DateTime dateBase,
            string airlineCode,
            string providerCodes,
            string serviceCodes,
            string stationCodes,
            string userSend)
        {
            SqlParameter startDateRealParameter = startDateReal != default(DateTime) ?
                new SqlParameter("@StartDateReal", startDateReal) :
                new SqlParameter("@StartDateReal", new DateTime(1900, 1, 1));

            SqlParameter endDateRealParameter = endDateReal != default(DateTime) ?
                new SqlParameter("@EndDateReal", endDateReal) :
                new SqlParameter("@EndDateReal", new DateTime(1900, 1, 1));

            SqlParameter startDateComplementaryParameter = startDateComplementary != default(DateTime) ?
                new SqlParameter("@StartDateComp", startDateComplementary) :
                new SqlParameter("@StartDateComp", new DateTime(1900, 1, 1));

            SqlParameter endDateComplementaryParameter = endDateComplementary != default(DateTime) ?
                new SqlParameter("@EndDateComp", endDateComplementary) :
                new SqlParameter("@EndDateComp", new DateTime(1900, 1, 1));

            SqlParameter headerTextParameter = headerText != null ?
                new SqlParameter("@HeaderText", headerText) :
                new SqlParameter("@HeaderText", string.Empty);

            SqlParameter itemTextParameter = itemText != null ?
                new SqlParameter("@ItemText", itemText) :
                new SqlParameter("@ItemText", string.Empty);

            SqlParameter dateValueParameter = dateValue != default(DateTime) ?
                new SqlParameter("@DateValue", dateValue) :
                new SqlParameter("@DateValue", new DateTime(1900, 1, 1));

            SqlParameter datePostingParameter = datePosting != default(DateTime) ?
                new SqlParameter("@DatePosting", datePosting) :
                new SqlParameter("@DatePosting", new DateTime(1900, 1, 1));

            SqlParameter dateBaseParameter = dateBase != default(DateTime) ?
                new SqlParameter("@DateBase", dateBase) :
                new SqlParameter("@DateBase", new DateTime(1900, 1, 1));

            SqlParameter airlineCodeParameter = airlineCode != null ?
                new SqlParameter("@AirlineCode", airlineCode) :
                new SqlParameter("@AirlineCode", string.Empty);

            SqlParameter providerCodesParameter = providerCodes != null ?
                new SqlParameter("@ProviderCodes", providerCodes) :
                new SqlParameter("@ProviderCodes", string.Empty);

            SqlParameter serviceCodesParameter = serviceCodes != null ?
                new SqlParameter("@ServiceCodes", serviceCodes) :
                new SqlParameter("@ServiceCodes", string.Empty);

            SqlParameter stationCodesParameter = stationCodes != null ?
                new SqlParameter("@StationCodes", stationCodes) :
                new SqlParameter("@StationCodes", string.Empty);

            SqlParameter userSendParameter = userSend != null ?
                new SqlParameter("@UserSend", userSend) :
                new SqlParameter("@UserSend", string.Empty);

            return this.Database.SqlQuery<CurrencyTypeChage>(
                "Process.VerifyPolizaProvisionesInt "
                + "@StartDateReal,"
                + "@EndDateReal,"
                + "@StartDateComp,"
                + "@EndDateComp,"
                + "@HeaderText,"
                + "@ItemText,"
                + "@DateValue,"
                + "@DatePosting,"
                + "@DateBase,"
                + "@AirlineCode,"
                + "@ProviderCodes,"
                + "@ServiceCodes,"
                + "@StationCodes,"
                + "@UserSend",
                startDateRealParameter,
                endDateRealParameter,
                startDateComplementaryParameter,
                endDateComplementaryParameter,
                headerTextParameter,
                itemTextParameter,
                dateValueParameter,
                datePostingParameter,
                dateBaseParameter,
                airlineCodeParameter,
                providerCodesParameter,
                serviceCodesParameter,
                stationCodesParameter,
                userSendParameter).ToList();
        }

        /// <summary>
        /// Cancels the jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        public bool CancelJetFuelPolicy(long? policyID)
        {
            var sucess = false;
            try
            {
                var policyIDParameter = policyID.HasValue ?
                    new SqlParameter("@PolicyID", policyID) :
                    new SqlParameter("@PolicyID", 0L);
                var result = this.Database.SqlQuery<int>("Process.CancelJetFuelPolicy @PolicyID", policyIDParameter).SingleOrDefault();
                sucess = result == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Saves the national policy cost.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        public long SaveNationalPolicyCost(NationalJetFuelPolicyControl policyCtrl)
        {
            var policyID = 0L;
            var querySQL = string.Empty;
            var sqlParams = new object[0];

            try
            {
                var startDateRealParameter = policyCtrl.StartDateReal != null ?
                    new SqlParameter("@StartDateReal", policyCtrl.StartDateReal) :
                    new SqlParameter("@StartDateReal", new DateTime(1900, 1, 1));

                var endDateRealParameter = policyCtrl.EndDateReal != null ?
                    new SqlParameter("@EndDateReal", policyCtrl.EndDateReal) :
                    new SqlParameter("@EndDateReal", new DateTime(1900, 1, 1));

                var startDateCompParameter = policyCtrl.StartDateComplementary != null ?
                    new SqlParameter("@StartDateComp", policyCtrl.StartDateComplementary) :
                    new SqlParameter("@StartDateComp", new DateTime(1900, 1, 1));

                var endDateCompParameter = policyCtrl.EndDateComplementary != null ?
                    new SqlParameter("@EndDateComp", policyCtrl.EndDateComplementary) :
                    new SqlParameter("@EndDateComp", new DateTime(1900, 1, 1));

                var headerTextParameter = policyCtrl.HeaderText != null ?
                    new SqlParameter("@HeaderText", policyCtrl.HeaderText) :
                    new SqlParameter("@HeaderText", string.Empty);

                var itemTextParameter = policyCtrl.ItemText != null ?
                    new SqlParameter("@ItemText", policyCtrl.ItemText) :
                    new SqlParameter("@ItemText", string.Empty);

                var dateValueParameter = policyCtrl.DateValue != null ?
                    new SqlParameter("@DateValue", policyCtrl.DateValue) :
                    new SqlParameter("@DateValue", new DateTime(1900, 1, 1));

                var datePostingParameter = policyCtrl.DatePosting != null ?
                    new SqlParameter("@DatePosting", policyCtrl.DatePosting) :
                    new SqlParameter("@DatePosting", new DateTime(1900, 1, 1));

                var dateBaseParameter = policyCtrl.DateBaseline != null ?
                    new SqlParameter("@DateBase", policyCtrl.DateBaseline) :
                    new SqlParameter("@DateBase", new DateTime(1900, 1, 1));

                var airlineCodeParameter = policyCtrl.AirlineCode != null ?
                    new SqlParameter("@AirlineCode", policyCtrl.AirlineCode) :
                    new SqlParameter("@AirlineCode", string.Empty);

                var providerCodesParameter = policyCtrl.ProviderCodes != null ?
                    new SqlParameter("@ProviderCodes", policyCtrl.ProviderCodes) :
                    new SqlParameter("@ProviderCodes", string.Empty);

                var serviceCodesParameter = policyCtrl.ServiceCodes != null ?
                    new SqlParameter("@ServiceCodes", policyCtrl.ServiceCodes) :
                    new SqlParameter("@ServiceCodes", string.Empty);

                var stationCodesParameter = policyCtrl.AirportCodes != null ?
                    new SqlParameter("@StationCodes", policyCtrl.AirportCodes) :
                    new SqlParameter("@StationCodes", string.Empty);

                var userSendParameter = policyCtrl.SendByUserName != null ?
                    new SqlParameter("@UserSend", policyCtrl.SendByUserName) :
                    new SqlParameter("@UserSend", string.Empty);

                querySQL = "Process.SaveNationalPolicyCost @StartDateReal, @EndDateReal, @StartDateComp, " +
                            "@EndDateComp, @HeaderText, @ItemText, @DateValue, @DatePosting, @DateBase, " +
                            "@AirlineCode, @ProviderCodes, @ServiceCodes, @StationCodes, @UserSend";

                sqlParams = new[]
                {
                    startDateRealParameter, endDateRealParameter, startDateCompParameter,
                    endDateCompParameter, headerTextParameter, itemTextParameter,
                    dateValueParameter, datePostingParameter, dateBaseParameter,
                    airlineCodeParameter, providerCodesParameter, serviceCodesParameter,
                    stationCodesParameter, userSendParameter
                };

                policyID = this.Database.SqlQuery<long>(querySQL, sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return policyID;
        }

        /// <summary>
        /// Verifies the national policy currency.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        public IList<CurrencyTypeChage> VerifyNationalPolicyCurrency(NationalJetFuelPolicyControl policyCtrl)
        {
            var querySQL = string.Empty;
            var sqlParams = new object[0];
            var currencies = new List<CurrencyTypeChage>();

            try
            {
                var startDateRealParameter = policyCtrl.StartDateReal != null ?
                    new SqlParameter("@StartDateReal", policyCtrl.StartDateReal) :
                    new SqlParameter("@StartDateReal", new DateTime(1900, 1, 1));

                var endDateRealParameter = policyCtrl.EndDateReal != null ?
                    new SqlParameter("@EndDateReal", policyCtrl.EndDateReal) :
                    new SqlParameter("@EndDateReal", new DateTime(1900, 1, 1));

                var startDateCompParameter = policyCtrl.StartDateComplementary != null ?
                    new SqlParameter("@StartDateComp", policyCtrl.StartDateComplementary) :
                    new SqlParameter("@StartDateComp", new DateTime(1900, 1, 1));

                var endDateCompParameter = policyCtrl.EndDateComplementary != null ?
                    new SqlParameter("@EndDateComp", policyCtrl.EndDateComplementary) :
                    new SqlParameter("@EndDateComp", new DateTime(1900, 1, 1));

                var headerTextParameter = policyCtrl.HeaderText != null ?
                    new SqlParameter("@HeaderText", policyCtrl.HeaderText) :
                    new SqlParameter("@HeaderText", string.Empty);

                var itemTextParameter = policyCtrl.ItemText != null ?
                    new SqlParameter("@ItemText", policyCtrl.ItemText) :
                    new SqlParameter("@ItemText", string.Empty);

                var dateValueParameter = policyCtrl.DateValue != null ?
                    new SqlParameter("@DateValue", policyCtrl.DateValue) :
                    new SqlParameter("@DateValue", new DateTime(1900, 1, 1));

                var datePostingParameter = policyCtrl.DatePosting != null ?
                    new SqlParameter("@DatePosting", policyCtrl.DatePosting) :
                    new SqlParameter("@DatePosting", new DateTime(1900, 1, 1));

                var dateBaseParameter = policyCtrl.DateBaseline != null ?
                    new SqlParameter("@DateBase", policyCtrl.DateBaseline) :
                    new SqlParameter("@DateBase", new DateTime(1900, 1, 1));

                var airlineCodeParameter = policyCtrl.AirlineCode != null ?
                    new SqlParameter("@AirlineCode", policyCtrl.AirlineCode) :
                    new SqlParameter("@AirlineCode", string.Empty);

                var providerCodesParameter = policyCtrl.ProviderCodes != null ?
                    new SqlParameter("@ProviderCodes", policyCtrl.ProviderCodes) :
                    new SqlParameter("@ProviderCodes", string.Empty);

                var serviceCodesParameter = policyCtrl.ServiceCodes != null ?
                    new SqlParameter("@ServiceCodes", policyCtrl.ServiceCodes) :
                    new SqlParameter("@ServiceCodes", string.Empty);

                var stationCodesParameter = policyCtrl.AirportCodes != null ?
                    new SqlParameter("@StationCodes", policyCtrl.AirportCodes) :
                    new SqlParameter("@StationCodes", string.Empty);

                var userSendParameter = policyCtrl.SendByUserName != null ?
                    new SqlParameter("@UserSend", policyCtrl.SendByUserName) :
                    new SqlParameter("@UserSend", string.Empty);

                querySQL = "Process.VerifyNationalPolicyCurrency @StartDateReal, @EndDateReal, @StartDateComp, " +
                            "@EndDateComp, @HeaderText, @ItemText, @DateValue, @DatePosting, @DateBase, " +
                            "@AirlineCode, @ProviderCodes, @ServiceCodes, @StationCodes, @UserSend";

                sqlParams = new object[]
                {
                    startDateRealParameter, endDateRealParameter, startDateCompParameter,
                    endDateCompParameter, headerTextParameter, itemTextParameter,
                    dateValueParameter, datePostingParameter, dateBaseParameter,
                    airlineCodeParameter, providerCodesParameter, serviceCodesParameter,
                    stationCodesParameter, userSendParameter
                };

                currencies = this.Database.SqlQuery<CurrencyTypeChage>(querySQL, sqlParams).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return currencies;
        }

        /// <summary>
        /// Cancels the national jet fuel policy.
        /// </summary>
        /// <param name="NationalPolicyID">The national policy identifier.</param>
        /// <returns></returns>
        public bool CancelNationalJetFuelPolicy(long? NationalPolicyID)
        {
            var sucess = false;
            try
            {
                var policyIDParameter = NationalPolicyID.HasValue ?
                    new SqlParameter("@NationalPolicyID", NationalPolicyID) :
                    new SqlParameter("@NationalPolicyID", 0L);
                var result = this.Database.SqlQuery<int>("Process.CancelNationalJetFuelPolicy @NationalPolicyID", policyIDParameter).SingleOrDefault();
                sucess = result == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }


        /// <summary>
        /// Validates the and save remittance identifier.
        /// </summary>
        /// <param name="remittanceInfo">The remittance information.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns></returns>
        public virtual IList<RemittanceIDValidate> ValidateAndSaveRemittanceID(DataTable remittanceInfo, string airlineCode)
        {
            var sqlParams = new object[0];

            SqlParameter remittanceInfoParameter = new SqlParameter();
            remittanceInfoParameter.ParameterName = "@RemittanceInfo";
            remittanceInfoParameter.Value = remittanceInfo;
            remittanceInfoParameter.SqlDbType = SqlDbType.Structured;
            remittanceInfoParameter.TypeName = "[Process].[NationalJetFuelInvoiceDetailType]";

            SqlParameter airlineCodeParameter = airlineCode != null ?
                new SqlParameter("@AirlineCode", airlineCode) :
                new SqlParameter("@AirlineCode", string.Empty);

            sqlParams = new object[]
                {
                    remittanceInfoParameter,
                    airlineCodeParameter
                };

            return this.Database.SqlQuery<RemittanceIDValidate>(
                "Process.ValidateAndSaveRemittances "
                + "@RemittanceInfo,"
                + "@AirlineCode",
                sqlParams).ToList();
        }

        /// <summary>
        /// Uploads the manual reconcile.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        public virtual IList<RemittanceIDValidate> UploadManualReconcile(DataTable reconcileInfo)
        {
            var sqlParams = new object[0];

            SqlParameter remittanceInfoParameter = new SqlParameter();
            remittanceInfoParameter.ParameterName = "@ReconcileInfo";
            remittanceInfoParameter.Value = reconcileInfo;
            remittanceInfoParameter.SqlDbType = SqlDbType.Structured;
            remittanceInfoParameter.TypeName = "[Process].[NationalJetFuelManualReconcileType]";

            sqlParams = new object[]
                {
                    remittanceInfoParameter
                };

            return this.Database.SqlQuery<RemittanceIDValidate>(
                "[Process].[UploadManualReconcile] "
                + "@ReconcileInfo",
                sqlParams).ToList();
        }

        /// <summary>
        /// Uploads the nonconformity.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        public virtual IList<RemittanceIDValidate> UploadNonconformity(DataTable reconcileInfo)
        {
            var sqlParams = new object[0];

            SqlParameter remittanceInfoParameter = new SqlParameter();
            remittanceInfoParameter.ParameterName = "@ReconcileInfo";
            remittanceInfoParameter.Value = reconcileInfo;
            remittanceInfoParameter.SqlDbType = SqlDbType.Structured;
            remittanceInfoParameter.TypeName = "[Process].[NationalJetFuelNonconformityType]";

            sqlParams = new object[]
                {
                    remittanceInfoParameter
                };

            return this.Database.SqlQuery<RemittanceIDValidate>(
                "[Process].[UploadNonconformity] "
                + "@ReconcileInfo",
                sqlParams).ToList();
        }

        /// <summary>
        /// Deletes the national invoice.
        /// </summary>
        /// <param name="remittanceID">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        public virtual int DeleteNationalInvoice(string remittanceID, string monthYear, string period)
        {
            var result = 0;
            object[] sqlParams = new object[0];

            try
            {
                var remittanceIDParameter = remittanceID != null ?
                new SqlParameter("@RemittanceID", remittanceID) :
                new SqlParameter("@RemittanceID", string.Empty);

                SqlParameter monthYearParameter = monthYear != null ?
                    new SqlParameter("@MonthYear", monthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                SqlParameter periodParameter = period != null ?
                    new SqlParameter("@Period", period) :
                    new SqlParameter("@Period", string.Empty);

                sqlParams = new object[]
                {
                    remittanceIDParameter,
                    monthYearParameter,
                    periodParameter
                };

                result = this.Database.SqlQuery<int>("[Process].[DeleteNationalInvoice] @RemittanceID, @MonthYear, @Period", sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Verifies the national jet fuel invoice policy.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <returns>
        /// -1: An error during the call of the store procedure.
        /// 0: No errors found.
        /// 1: Error. The policy has been created.
        /// 2: Error caused because there are errors in the remittense.
        /// </returns>
        public virtual int VerifyNationalJetFuelInvoicePolicy(string remittanceId, string monthYear, string period)
        {
            int result = -1;
            object[] sqlParams = new object[0];

            try
            {
                SqlParameter remittanceIdParameter = remittanceId != null ?
                    new SqlParameter("@RemittanceID", remittanceId) :
                    new SqlParameter("@RemittanceID", string.Empty);

                SqlParameter monthYearParameter = monthYear != null ?
                    new SqlParameter("@MonthYear", monthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                SqlParameter periodParameter = period != null ?
                    new SqlParameter("@Period", period) :
                    new SqlParameter("@Period", string.Empty);

                sqlParams = new object[]
                {
                    remittanceIdParameter,
                    monthYearParameter,
                    periodParameter
                };

                result = this.Database.SqlQuery<int>("[Process].[VerifyNationalJetFuelInvoicePolicy] @RemittanceID, @MonthYear, @Period", sqlParams).SingleOrDefault();
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, exception);
            }

            return result;
        }

        /// <summary>
        /// Saves the national jet fuel invoice policy.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="dateValue">The date value.</param>
        /// <param name="datePosting">The date posting.</param>
        /// <param name="dateBase">The date base.</param>
        /// <param name="period">The period.</param>
        /// <returns>
        /// Greater than zero. The process was completed successfuly.
        /// Less or equal than zero. An error in the process.
        /// </returns>
        public virtual int SaveNationalJetFuelInvoicePolicy(
            string remittanceId,
            string monthYear,
            DateTime dateValue,
            DateTime datePosting,
            DateTime dateBase,
            string period)
        {
            string querySQL = string.Empty;
            object[] sqlParams = new object[0];
            string result = string.Empty;
            try
            {
                SqlParameter remittanceIdParameter = remittanceId != null ?
                    new SqlParameter("@RemittanceID", remittanceId) :
                    new SqlParameter("@RemittanceID", string.Empty);

                SqlParameter monthYearParameter = monthYear != null ?
                    new SqlParameter("@MonthYear", monthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                SqlParameter dateValueParameter = dateValue != null ?
                    new SqlParameter("@DateValue", dateValue) :
                    new SqlParameter("@DateValue", new DateTime(1900, 1, 1));

                SqlParameter datePostingParameter = datePosting != null ?
                    new SqlParameter("@DatePosting", datePosting) :
                    new SqlParameter("@DatePosting", new DateTime(1900, 1, 1));

                SqlParameter dateBaseParameter = dateBase != null ?
                    new SqlParameter("@DateBase", dateBase) :
                    new SqlParameter("@DateBase", new DateTime(1900, 1, 1));

                SqlParameter periodParameter = period != null ?
                    new SqlParameter("@Period", period) :
                    new SqlParameter("@Period", string.Empty);

                querySQL = "[Process].[SaveNationalJetFuelInvoicePolicy] " +
                            "@RemittanceID, @MonthYear, @DateValue, " +
                            "@DatePosting, @DateBase, @Period";

                sqlParams = new object[]
                {
                    remittanceIdParameter,
                    monthYearParameter,
                    dateValueParameter,
                    datePostingParameter,
                    dateBaseParameter,
                    periodParameter
                };

                result = this.Database.SqlQuery<string>(querySQL, sqlParams).SingleOrDefault();
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, exception);
            }

            return result != "-1" ? 1 : 0;
        }

        /// <summary>
        /// Verifies the national jet fuel reconciliation.
        /// </summary>
        /// <param name="rec">The record.</param>
        /// <returns></returns>
        public virtual int VerifyNationalJetFuelReconciliation(NationalJetFuelInvoiceControl rec)
        {
            var verify = 0;
            var sqlParams = new object[0];

            try
            {
                var remittanceIDParameter = rec.RemittanceID != null ?
                    new SqlParameter("@RemittanceID", rec.RemittanceID) :
                    new SqlParameter("@RemittanceID", string.Empty);

                var monthYearParameter = rec.MonthYear != null ?
                    new SqlParameter("@MonthYear", rec.MonthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                var periodParameter = rec.Period != null ?
                    new SqlParameter("@Period", rec.Period) :
                    new SqlParameter("@Period", string.Empty);

                sqlParams = new object[] { remittanceIDParameter, monthYearParameter, periodParameter };
                verify = this.Database.SqlQuery<int>("[Process].[VerifyNationalJetFuelReconciliation] @RemittanceID, @MonthYear, @Period", sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return verify;
        }


        /// <summary>
        /// Jets the fuel reconciliation.
        /// </summary>
        /// <param name="rec">The record.</param>
        /// <returns></returns>
        public virtual int JetFuelReconciliation(NationalJetFuelInvoiceControl rec)
        {
            var reconciled = 0;
            var sqlParams = new object[0];

            try
            {
                var remittanceIDParameter = rec.RemittanceID != null ?
                    new SqlParameter("@RemittanceID", rec.RemittanceID) :
                    new SqlParameter("@RemittanceID", string.Empty);

                var monthYearParameter = rec.MonthYear != null ?
                    new SqlParameter("@MonthYear", rec.MonthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                var periodParameter = rec.Period != null ?
                    new SqlParameter("@Period", rec.Period) :
                    new SqlParameter("@Period", string.Empty);

                var typeProcessParameter = (int)rec.TypeProcess > 0 ?
                    new SqlParameter("@TypeProcess", (int)rec.TypeProcess) :
                    new SqlParameter("@TypeProcess", 0);

                sqlParams = new object[] { remittanceIDParameter, monthYearParameter, periodParameter, typeProcessParameter };
                reconciled = this.Database.SqlQuery<int>("[Process].[JetFuelReconciliation] @RemittanceID, @MonthYear, @Period, @TypeProcess", sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return reconciled;
        }

        /// <summary>
        /// Jets the fuel revert manual reconcile.
        /// </summary>
        /// <param name="rec">The record.</param>
        /// <returns></returns>
        public virtual int JetFuelRevertManualReconcile(NationalJetFuelInvoiceControl rec)
        {
            var reconciled = 0;
            var sqlParams = new object[0];

            try
            {
                var remittanceIDParameter = rec.RemittanceID != null ?
                    new SqlParameter("@RemittanceID", rec.RemittanceID) :
                    new SqlParameter("@RemittanceID", string.Empty);

                var monthYearParameter = rec.MonthYear != null ?
                    new SqlParameter("@MonthYear", rec.MonthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                var periodParameter = rec.Period != null ?
                    new SqlParameter("@Period", rec.Period) :
                    new SqlParameter("@Period", string.Empty);

                sqlParams = new object[] { remittanceIDParameter, monthYearParameter, periodParameter };
                reconciled = this.Database.SqlQuery<int>("[Process].[JetFuelReconciliationManualRevert] @RemittanceID, @MonthYear, @Period", sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return reconciled;
        }

        public virtual int JetFuelRevertNonconformity(NationalJetFuelInvoiceControl rec)
        {
            var reconciled = 0;
            var sqlParams = new object[0];

            try
            {
                var remittanceIDParameter = rec.RemittanceID != null ?
                    new SqlParameter("@RemittanceID", rec.RemittanceID) :
                    new SqlParameter("@RemittanceID", string.Empty);

                var monthYearParameter = rec.MonthYear != null ?
                    new SqlParameter("@MonthYear", rec.MonthYear) :
                    new SqlParameter("@MonthYear", string.Empty);

                var periodParameter = rec.Period != null ?
                    new SqlParameter("@Period", rec.Period) :
                    new SqlParameter("@Period", string.Empty);

                sqlParams = new object[] { remittanceIDParameter, monthYearParameter, periodParameter };
                reconciled = this.Database.SqlQuery<int>("[Process].[JetFuelNonconformityRevert] @RemittanceID, @MonthYear, @Period", sqlParams).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return reconciled;
        }

        /// <summary>
        /// Automatics the timeline.
        /// </summary>
        /// <param name="startDateParam">The start date parameter.</param>
        /// <param name="endDateParam">The end date parameter.</param>
        /// <returns></returns>
        public virtual bool AutomaticTimeline(DateTime? startDateParam, DateTime? endDateParam)
        {
            var sucess = false;
            var sqlParams = new object[0];
            var result = new object();

            try
            {

                var startDateParamParameter = startDateParam != null ?
                    new SqlParameter("@StartDateParam", startDateParam) :
                    new SqlParameter("@StartDateParam", string.Empty);

                var endDateParamParameter = endDateParam != null ?
                    new SqlParameter("@EndDateParam", endDateParam) :
                    new SqlParameter("@EndDateParam", string.Empty);

                sqlParams = new object[] { startDateParamParameter, endDateParamParameter };
                result = this.Database.SqlQuery<object>("[Itinerary].[AutomaticTimeline] @StartDateParam, @EndDateParam", sqlParams).SingleOrDefault();
                sucess = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return sucess;
        }
        #endregion

        /// <summary>
        /// Save changes.
        /// </summary>
        public virtual void Commit()
        {
            this.SaveChanges();
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the Data base ModelBuilder and Database ContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CurrencyConfiguration());
            modelBuilder.Configurations.Add(new AirportConfiguration());
            modelBuilder.Configurations.Add(new AirportGroupConfiguration());
            modelBuilder.Configurations.Add(new ProviderConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new AccountingAccountConfiguration());
            modelBuilder.Configurations.Add(new LiabilityAccountConfiguration());
            modelBuilder.Configurations.Add(new CostCenterConfiguration());
            modelBuilder.Configurations.Add(new AirlineConfiguration());
            modelBuilder.Configurations.Add(new ServiceConfiguration());
            modelBuilder.Configurations.Add(new TaxConfiguration());
            modelBuilder.Configurations.Add(new AirplaneTypeConfiguration());
            modelBuilder.Configurations.Add(new AirplaneConfiguration());
            modelBuilder.Configurations.Add(new CompartmentTypeConfiguration());
            modelBuilder.Configurations.Add(new CompartmentTypeInformationConfiguration());
            modelBuilder.Configurations.Add(new CompartmentTypeConfigConfiguration());
            modelBuilder.Configurations.Add(new GpuConfiguration());
            modelBuilder.Configurations.Add(new GpuObservationConfiguration());
            modelBuilder.Configurations.Add(new CrewConfiguration());
            modelBuilder.Configurations.Add(new DrinkingWaterConfiguration());
            modelBuilder.Configurations.Add(new AirportScheduleConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelTicketConfiguration());

            modelBuilder.Configurations.Add(new CrewTypeConfiguration());
            modelBuilder.Configurations.Add(new AirportServiceConfiguration());
            modelBuilder.Configurations.Add(new JetFuelTicketConfiguration());

            modelBuilder.Configurations.Add(new FuelConceptConfiguration());
            modelBuilder.Configurations.Add(new FunctionalAreaConfiguration());
            modelBuilder.Configurations.Add(new InternationalFuelContractConfiguration());
            modelBuilder.Configurations.Add(new InternationalFuelContractConceptConfiguration());
            modelBuilder.Configurations.Add(new InternationalFuelRateConfiguration());
            modelBuilder.Configurations.Add(new ChargeFactorTypeConfiguration());
            modelBuilder.Configurations.Add(new FuelConceptTypeConfiguration());
            modelBuilder.Configurations.Add(new ExchangeRatesConfiguration());
            modelBuilder.Configurations.Add(new ScheduleTypeConfiguration());

            modelBuilder.Configurations.Add(new AirportServiceContractConfiguration());
            modelBuilder.Configurations.Add(new ServiceCalculationTypeConfiguration());
            modelBuilder.Configurations.Add(new ServiceTypeConfiguration());
            modelBuilder.Configurations.Add(new OperationTypeConfiguration());
            modelBuilder.Configurations.Add(new AirplaneWeightMeasureTypeConfiguration());
            modelBuilder.Configurations.Add(new AirplaneWeightTypeConfiguration());

            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new ModuleConfiguration());
            modelBuilder.Configurations.Add(new ModulePermissionConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new ProfileRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserProfileRoleConfiguration());
            modelBuilder.Configurations.Add(new UserAirportConfiguration());
            modelBuilder.Configurations.Add(new PageReportConfiguration());

            modelBuilder.Configurations.Add(new ItineraryConfiguration());
            modelBuilder.Configurations.Add(new ItineraryLogConfiguration());
            modelBuilder.Configurations.Add(new StatusOnBoardConfiguration());
            modelBuilder.Configurations.Add(new GendecDepartureConfiguration());
            modelBuilder.Configurations.Add(new PassengerInformationConfiguration());
            modelBuilder.Configurations.Add(new GendecArrivalConfiguration());
            modelBuilder.Configurations.Add(new MenuConfiguration());

            modelBuilder.Configurations.Add(new CalculationStatusConfiguration());
            modelBuilder.Configurations.Add(new ConfirmationStatusConfiguration());
            modelBuilder.Configurations.Add(new StatusProcessConfiguration());
            modelBuilder.Configurations.Add(new JetFuelProcessConfiguration());

            modelBuilder.Configurations.Add(new JetFuelProvisionConfiguration());
            modelBuilder.Configurations.Add(new JetFuelLogErrorConfiguration());
            modelBuilder.Configurations.Add(new JetFuelPolicyControlConfiguration());
            modelBuilder.Configurations.Add(new JetFuelPolicyConfiguration());

            modelBuilder.Configurations.Add(new NationalFuelContractConfiguration());
            modelBuilder.Configurations.Add(new NationalFuelContractConceptConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelCostConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelLogErrorConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelProcessConfiguration());

            modelBuilder.Configurations.Add(new NationalFuelRateConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelPolicyConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelPolicyControlConfiguration());

            modelBuilder.Configurations.Add(new NationalJetFuelInvoiceControlConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelInvoiceDetailConfiguration());
            modelBuilder.Configurations.Add(new NationalJetFuelInvoicePolicyConfiguration());
            modelBuilder.Configurations.Add(new DocumentStatusConfiguration());

            modelBuilder.Configurations.Add(new ReconciliationToleranceConfiguration());
            modelBuilder.Configurations.Add(new ToleranceTypeConfiguration());
            
            modelBuilder.Configurations.Add(new ManifestDepartureConfiguration());
            modelBuilder.Configurations.Add(new ManifestArrivalConfiguration());
            modelBuilder.Configurations.Add(new AdditionalDepartureInformationConfiguration());
            modelBuilder.Configurations.Add(new AdditionalArrivalInformationConfiguration());
            modelBuilder.Configurations.Add(new ManifestDepartureBoardingConfiguration());
            modelBuilder.Configurations.Add(new ManifestDepartureBoardingInformationConfiguration());
            modelBuilder.Configurations.Add(new ManifestDepartureBoardingDetailConfiguration());

            modelBuilder.Configurations.Add(new TimelineConfiguration());
            modelBuilder.Configurations.Add(new TimelineMovementConfiguration());
            modelBuilder.Configurations.Add(new MovementTypeConfiguration());

            modelBuilder.Configurations.Add(new AdditionalPassengerInformationConfiguration());
        }
    }
}
