var NationalPolicySendController = {
    SetAirportComboConfiguration: function () {
        $('#AirportCodesList').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            filterPlaceholder: '',
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#AirportCodesList').multiselect('updateButtonText');
        $('#AirportCodesList').multiselect('disable');
    },
    SetAirlineComboConfiguration: function () {
        $('#AirlineCode').multiselect({
            buttonClass: 'btn-multi-select col-xs-12',
            maxHeight: 200,
        });
        $('#AirlineCode').multiselect('disable');
    },
    SetServiceComboConfiguration: function () {
        $('#ServiceCodesList').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#ServiceCodesList').multiselect('updateButtonText');
        $('#ServiceCodesList').multiselect('disable');
    },
    SetProviderComboConfiguration: function () {
        $('#ProviderCodesList').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            filterPlaceholder: '',
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#ProviderCodesList').multiselect('updateButtonText');
        $('#ProviderCodesList').multiselect('disable');
    },
    postSend: function () {
        NationalPolicySendController.submitForm();
    },
    postForm: function (operation) {
        var startDate = $("#StartDateReal").data("DateTimePicker").date().toDate();
        var endDate = $("#EndDateReal").data("DateTimePicker").date().toDate();
        var lastDay = new Date(endDate.getFullYear(), endDate.getMonth() + 1, 0);
        var EndDateRealMoment = moment(endDate);
        var lastDayMoment = moment(lastDay);
        var daydiff = lastDayMoment.diff(EndDateRealMoment, 'days');

        var startDateComplementary = $("#StartDateComplementary").data("DateTimePicker").date();
        var endDateComplementary = $("#EndDateComplementary").data("DateTimePicker").date();
        if (startDateComplementary && endDateComplementary) {
            var daydiffComplementary = endDateComplementary.diff(startDateComplementary, 'days');
            daydiffComplementary = daydiffComplementary + 1;
            var totalDiff = daydiff - daydiffComplementary;

            if (daydiff <= daydiffComplementary) {
                NationalPolicySendController.submitForm();
            }
            else {
                NationalPolicySendController.showAlert('The complementary range is incorrect. Add more days: ' + totalDiff, 'El período de fechas complementarias es incorrecto. Agregar más días: ' + totalDiff, 'warning', 'Warning', 'Advertencia');
            }
        }
        else {
            NationalPolicySendController.submitForm();
        }

    },
    changeAccionForm: function (action) {
        $("#SendNTLPolicyForm").attr("action", action);
    },
    submitForm: function () {
        var form = $('#SendNTLPolicyForm');
        if (form) {
            form.submit();
        }
    },
    showAlert: function (messageEn, messageEs, alertType, titleEn, tittleEs) {
        //"warning", "error", "success" and "info".
        alertType = alertType || "warning";
        if (currentLang.includes("es")) {
            swal({
                title: tittleEs,
                text: messageEs,
                type: alertType,
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
        }
        else {
            swal({
                title: titleEn,
                text: messageEn,
                type: alertType,
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
        }
    },
    Init: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
        NationalPolicySendController.SetAirportComboConfiguration();
        NationalPolicySendController.SetAirlineComboConfiguration();
        NationalPolicySendController.SetServiceComboConfiguration();
        NationalPolicySendController.SetProviderComboConfiguration();
    }
};

$(document).ready(NationalPolicySendController.Init);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}