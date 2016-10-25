var NationalPolicyCreateController = {
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
        $('#AirportCodesList').multiselect('selectAll', false);
        $('#AirportCodesList').multiselect('updateButtonText');
    },
    SetAirlineComboConfiguration: function () {
        $('#AirportCode').multiselect({
            buttonClass: 'btn-multi-select col-xs-12',
            maxHeight: 200,
        });
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
        $('#ServiceCodesList').multiselect('selectAll', false);
        $('#ServiceCodesList').multiselect('updateButtonText');
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
        $('#ProviderCodesList').multiselect('selectAll', false);
        $('#ProviderCodesList').multiselect('updateButtonText');
    },
    postSend: function () {
        var form = document.getElementById('CreateNTLPolicyForm');
        if (form) {
            form.action = '../Process/NationalPolicy/SendNationalPolicy';
            form.submit();
        }
    },
    postForm: function (operation) {
        var startDate = $("#StartDateReal").data("DateTimePicker").date();
        var endDate = $("#EndDateReal").data("DateTimePicker").date();

        if (startDate && endDate) {
            startDate = startDate.toDate();
            endDate = endDate.toDate();
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
                    NationalPolicyCreateController.submitForm();
                }
                else {
                    NationalPolicyCreateController.showAlert('The complementary range is incorrect. Add more days: ' + totalDiff, 'El período de fechas complementarias es incorrecto. Agregar más días: ' + totalDiff, 'warning', 'Warning', 'Advertencia');
                }
            }
            else {
                NationalPolicyCreateController.submitForm();
            }
        }

    },
    changeAccionForm: function (action) {
        $("#CreateNTLPolicyForm").attr("action", action);
    },
    submitForm: function () {
        var form = $('#CreateNTLPolicyForm');
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
        NationalPolicyCreateController.SetAirportComboConfiguration();
        NationalPolicyCreateController.SetAirlineComboConfiguration();
        NationalPolicyCreateController.SetServiceComboConfiguration();
        NationalPolicyCreateController.SetProviderComboConfiguration();
    }
};

$(document).ready(NationalPolicyCreateController.Init);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}