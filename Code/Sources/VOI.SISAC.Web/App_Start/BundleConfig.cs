//-----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Volaris">
// Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------
namespace VOI.SISAC.Web
{
    using System.Web.Optimization;

    /// <summary>
    /// Clase que configura paquetes de archivos javascritp y hojas de estilo
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Metodo estatico que registra los paquetes de archivos javascritp y las hojas de estilos
        /// </summary>
        /// <param name="bundles">Contiene y gestiona los paquetes javascritps y hojas de estilos</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*-- CSS Main Style --*/
            bundles.Add(new StyleBundle("~/Content/Main/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/http://aacosta.com.mx/css"));

            /*-- CSS Bootstrap Style --*/
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"));

            /*-- CSS Boostrap Table Style --*/
            bundles.Add(new StyleBundle("~/Content/bootstraptablecss").Include(
                "~/Content/bootstrap-table.css"));

            /*--CSS FontAwesome Style --*/
            bundles.Add(new StyleBundle("~/Content/fontawesomecss").Include(
                ("~/Content/font-awesome.css")));

            /*--CSS jQuery UI Style --*/
            bundles.Add(new StyleBundle("~/Content/cssjqUi").Include
                ("~/Content/themes/jquery-ui/jquery-ui.css"));

            /*-- CSS Bootstraptimepicker Style --*/
            bundles.Add(new StyleBundle("~/Content/bootstrapdtimecss").Include(
                ("~/Content/datetimepicker/bootstrap-datetimepicker.min.css")));

            /*--CSS Bootstrap Combobox--*/
            bundles.Add(new StyleBundle("~/bundles/CSSbootstrapCombobox").Include(
                "~/Content/bootstrap-combobox.css"));

            /*--CSS Login--*/
            bundles.Add(new StyleBundle("~/bundles/LoginCSS").Include(
                "~/Content/login.css"));

            /*--CSS Sweet Alert --*/
            bundles.Add(new StyleBundle("~/bundles/SweetAlertCSS").Include(
                "~/Content/sweetalert.css"));

            /*--CSS Bootstrap Select Dropdown --*/
            bundles.Add(new StyleBundle("~/bundles/BootstrapSelectCSS").Include(
                "~/Content/bootstrap-select.css"));

            /*--CSS TreeSelectCSS --*/
            bundles.Add(new StyleBundle("~/bundles/TreeSelectCSS").Include(
                "~/Content/jquery.tree-multiselect.css"));

            /*--CSS BootstrapMultiselectCSS --*/
            bundles.Add(new StyleBundle("~/bundles/BootstrapMultiselectCSS").Include(
                "~/Content/bootstrap-multiselect.css"));

            /*--CSS TimelineCSS --*/
            bundles.Add(new StyleBundle("~/bundles/TimelineCSS").Include(
                "~/Content/timeline.css",
                "~/Content/genericons.css",
                "~/Content/wpex.css"));

            /*--CSS ButtonHoverCSS --*/
            bundles.Add(new StyleBundle("~/bundles/ButtonHoverCSS").Include(
                "~/Content/button_hover.css"));

            /*--CSS CircleMenuCSS --*/
            bundles.Add(new StyleBundle("~/bundles/CircleMenuCSS").Include(
                "~/Content/circle-menu.css"));




            /*--JS Jquery Script --*/
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            /*--JS jQuery Validate Script --*/
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            /*--JS Jquery UI Script --*/
            bundles.Add(new ScriptBundle("~/bundles/jqueryUi").Include
                (("~/Scripts/jquery-ui-{version}.js")));

            /*--JS Modernizr Script --*/
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/respond.js",
                "~/Scripts/modernizr-*"));

            /*--JS Bootstrap Script --*/
            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.js"));

            /*--JS Bootstrap Table Script --*/
            bundles.Add(new ScriptBundle("~/bundles/bootstrapTable").Include(
                "~/Scripts/bootstrap-table.js",
                "~/Scripts/bootstrap-table-en-US.js",
                "~/Scripts/bootstrap-table-es-MX.js",
                "~/Scripts/bootstrap-table-es-ES.js",
                "~/Scripts/FileSaver.js"));

            /*--JS Bootstrap Table Script --*/
            bundles.Add(new ScriptBundle("~/bundles/bootstrapEditableTablejs").Include(
                "~/Scripts/bootstrap3-editable/js/bootstrap-editable.min.js"));

            /*--JS Bootstrap Table Plugins Script --*/
            bundles.Add(new ScriptBundle("~/bundles/bootstrapTablePlugins").Include(
                "~/Scripts/bootstrap-table-contextmenu",
                "~/Scripts/bootstrap-table-mobile.js"));

            /*--JS Bootstraptimepicker Script --*/
            bundles.Add(new ScriptBundle("~/bundles/bootstrapdtimejs").Include(
                ("~/Scripts/moment/moment.js"),
                ("~/Scripts/moment/locale/*.js"),
                ("~/Scripts/moment/moment-with-locales.js"),
                ("~/Scripts/datetimepicker/bootstrap-datetimepicker.min.js")));

            /*--JS Commons functions SISAC Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CommonFunctions").Include(
                "~/Scripts/SISAC.js"));

            /*--JS CostCenter Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CostCenter").Include(
                "~/Scripts/SISAC/Catalog/CostCenter.js"));

            /*--JS Airport Script --*/
            bundles.Add(new ScriptBundle("~/bundles/Airport").Include(
                "~/Scripts/SISAC/Airport/Airport.js"));

            /*--JS DateTimeValidate Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JSDateTimeValidate").Include(
                "~/Scripts/SISAC/Common/jquery.validate.datetime.js"));

            /*--JS FlightSchedule --*/
            bundles.Add(new ScriptBundle("~/bundles/FlightSchedule").Include(
                "~/Scripts/bootstrap-table-filter-control.js"));

            /*--JS Bootstrap Input File --*/
            bundles.Add(new ScriptBundle("~/bundles/BootstrapInputFile").Include(
                "~/Scripts/bootstrap-filestyle.js"));

            /*--JS Export Table Bootstrap plugin--*/
            bundles.Add(new ScriptBundle("~/bundles/ExportTable").Include(
                "~/Scripts/bootstrap-table-export.js",
                "~/Scripts/tableExport.js",
                "~/Scripts/jquery.base64.js"));

            /*--JS Bootstrap Combobox--*/
            bundles.Add(new ScriptBundle("~/bundles/JSbootstrapCombobox").Include(
                "~/Scripts/bootstrap-combobox.js"));

            /*--JS Bootstrap Table Plugin Resizable--*/
            bundles.Add(new ScriptBundle("~/bundles/JSbootstrapTableResizable").Include(
                "~/Scripts/bootstrap-table-resizable.js",
                "~/Scripts/colResizable-1.5.source.js"));

            /*--JS InternationalFuelContract Script --*/
            bundles.Add(new ScriptBundle("~/bundles/InternationalFuelContract").Include(
                "~/Scripts/SISAC/Finance/InternationalFuelContract.js"));

            /*--JS InternationalFuelContractConcept Script --*/
            bundles.Add(new ScriptBundle("~/bundles/InternationalFuelContractConcept").Include(
                "~/Scripts/SISAC/Finance/InternationalFuelContractConcept.js"));

            /*--JS AirportServiceContract Script --*/
            bundles.Add(new ScriptBundle("~/bundles/AirportServiceContract").Include(
                "~/Scripts/SISAC/Finance/AirportServiceContract.js"));

            /*--JS SweetAlert Script --*/
            bundles.Add(new ScriptBundle("~/bundles/SweetAlertJS").Include(
                "~/Scripts/sweetalert.min.js"));

            /*--JS Itinerary Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ItineraryJS").Include(
                "~/Scripts/SISAC/Itinerary/Itinerary.js"));

            /*--JS Bootstrap Context Menu Script --*/
            bundles.Add(new ScriptBundle("~/bundles/BootstrapContextMenu").Include(
                "~/Scripts/bootstrap-table-contextmenu.js"));

            /*--JS JetFuelTicket Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JetFuelTicket").Include(
                "~/Scripts/SISAC/Airport/JetFuelTicket.js"));

            /*--JS Passenger Information Script --*/
            bundles.Add(new ScriptBundle("~/bundles/PassengerInformation").Include(
                "~/Scripts/SISAC/Airport/PassengerInformation.js"));

            /*--JS Gendec Script --*/
            bundles.Add(new ScriptBundle("~/bundles/GendecJS").Include(
                "~/Scripts/SISAC/Itinerary/Gendec.js"));

            /*--JS Airport Services Script --*/
            bundles.Add(new ScriptBundle("~/bundles/AirportServicesJS").Include(
                "~/Scripts/SISAC/Airport/AirportService.js"));

            /*--JS bootstrap Select Script --*/
            bundles.Add(new ScriptBundle("~/bundles/BootstrapSelectJS").Include(
                "~/Scripts/SISAC/bootstrap-select.js"));

            /*--JS Gendec Arrival Script --*/
            bundles.Add(new ScriptBundle("~/bundles/GendecArrivalJS").Include(
                "~/Scripts/SISAC/Itinerary/GendecArrival.js"));

            /*--JS Profile Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ProfileJS").Include(
                "~/Scripts/SISAC/Security/Profile.js"));

            /*--JS Module Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ModuleJS").Include(
                "~/Scripts/SISAC/Security/Module.js"));

            /*--JS Permission Script --*/
            bundles.Add(new ScriptBundle("~/bundles/PermissionJS").Include(
                "~/Scripts/SISAC/Security/Permission.js"));

            /*--JS Rol Script --*/
            bundles.Add(new ScriptBundle("~/bundles/RoleJS").Include(
                "~/Scripts/SISAC/Security/Role.js"));

            /*--JS Tree Multiselect Script --*/
            bundles.Add(new ScriptBundle("~/bundles/TreeSelectJS").Include(
                "~/Scripts/jquery.tree-multiselect.js"));

            /*--JS Role Module Permission Script --*/
            bundles.Add(new ScriptBundle("~/bundles/RoleModulePermissionJS").Include(
                "~/Scripts/SISAC/Security/RoleModulePermission.js"));

            /*--JS Role Module Permission Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CommonAction").Include(
                "~/Scripts/SISAC/Common/Account.js"));

            /*--JS Jet Fuel Process Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JetFuelProcessJS").Include(
                "~/Scripts/SISAC/Process/JetFuelProcess.js"));

            /*--JS National Jet Fuel Process Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalJetFuelProcessJS").Include(
                "~/Scripts/SISAC/Process/NationalJetFuelProcess.js"));

            /*--JS Jet Fuel Log Error Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JetFuelLogErrorJS").Include(
                "~/Scripts/SISAC/Process/JetFuelLogError.js"));

            /*--JS Jet Fuel Log Error Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalJetFuelLogErrorJS").Include(
                "~/Scripts/SISAC/Process/NationalJetFuelLogError.js"));

            /*--JS Confirm Period Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ConfirmPeriodJS").Include(
                "~/Scripts/SISAC/Process/ConfirmPeriod.js"));

            /*--JS Bootstrap multiselect Script --*/
            bundles.Add(new ScriptBundle("~/bundles/BootstrapMultiselect").Include(
                "~/Scripts/bootstrap-multiselect.js",
                "~/Scripts/bootstrap-multiselect-collapsible-groups.js"));

            /*--JS Jet Fuel Calculation Result Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JetFuelCalculationResultJS").Include(
                "~/Scripts/SISAC/Process/JetFuelCalculationResult.js"));

            /*--JS National Jet Fuel Calculation Result Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalJetFuelCalculationResultJS").Include(
                "~/Scripts/SISAC/Process/NationalJetFuelCalculationResult.js"));

            /*--JS Exchange Rates Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ExchangeRatesJS").Include(
                "~/Scripts/SISAC/Finance/ExchangeRates.js"));

            /*--JS Exchange Rates Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ReconciliationToleranceJS").Include(
                "~/Scripts/SISAC/Catalog/ReconciliationTolerance.js"));

            /*--JS Create Policy Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CreatePolicyJS").Include(
                "~/Scripts/SISAC/Process/CreatePolicy.js"));

            /*--JS Create Policy Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CreatePolicySendJS").Include(
                "~/Scripts/SISAC/Process/CreatePolicySend.js"));

            /*--JS National Fuel Contract Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalFuelContractJS").Include(
                "~/Scripts/SISAC/Finance/NationalFuelContract.js"));

            /*--JS National Fuel Contract Concept Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalFuelContractConceptJS").Include(
                "~/Scripts/SISAC/Finance/NationalFuelContractConcept.js"));

            /*--JS National Fuel Contract Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalFuelRateJS").Include(
                "~/Scripts/SISAC/Finance/NationalFuelRate.js"));
            BundleTable.EnableOptimizations = false;

            /*--JS Confirm National Period Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ConfirmNationalPeriodJS").Include(
                "~/Scripts/SISAC/Process/ConfirmNationalPeriod.js"));

            /*--JS National Policy Create Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalPolicyCreateJS").Include(
                "~/Scripts/SISAC/Process/NationalPolicyCreate.js"));

            /*--JS National Policy Send Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalPolicySendJS").Include(
                "~/Scripts/SISAC/Process/NationalPolicySend.js"));

            /*--JS National JetFuel InvoiceJS Script --*/
            bundles.Add(new ScriptBundle("~/bundles/NationalJetFuelInvoiceJS").Include(
                "~/Scripts/SISAC/Process/NationalJetFuelInvoice.js"));

            /*--JS Mask Plugin Script --*/
            bundles.Add(new ScriptBundle("~/bundles/MaskPluginJS").Include(
                "~/Scripts/jquery.mask.js"));

            /*--JS Jet Fuel Process Script --*/
            bundles.Add(new ScriptBundle("~/bundles/JetFuelProcessJS").Include(
                "~/Scripts/SISAC/Process/JetFuelProcess.js"));

            /*--JS Manifest Departure Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ManifestDepartureJS").Include(
                "~/Scripts/SISAC/Itinerary/ManifestDeparture.js"));

            /*--JS Manifest Arrival Script --*/
            bundles.Add(new ScriptBundle("~/bundles/ManifestArrivalJS").Include(
                "~/Scripts/SISAC/Itinerary/ManifestArrival.js"));

            /*--JS Timeline Scroll Script --*/
            bundles.Add(new ScriptBundle("~/bundles/TimelineScrollJS").Include(
                "~/Scripts/timeline-scroll.js"));

            /*--JS Circle Menu Script --*/
            bundles.Add(new ScriptBundle("~/bundles/CircleMenuJS").Include(
                "~/Scripts/circleMenu.js"));

            /*--JS Timeline Script --*/
            bundles.Add(new ScriptBundle("~/bundles/TimelineJS").Include(
                "~/Scripts/SISAC/Itinerary/Timeline.js"));
        }
    }
}
