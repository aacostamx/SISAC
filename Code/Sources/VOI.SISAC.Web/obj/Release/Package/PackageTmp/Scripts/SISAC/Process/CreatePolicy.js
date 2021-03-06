﻿var CreatePolicyController = {
    SetAirportComboConfiguration: function () {
        $('#AirportCodes').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            filterPlaceholder: '',
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
            //checkboxName:
        });
        $('#AirportCodes').multiselect('selectAll', false);
        $('#AirportCodes').multiselect('updateButtonText');
    },
    SetAirlineComboConfiguration: function () {
        $('#AirlineCode').multiselect({
            buttonClass: 'btn-multi-select col-xs-12',
            maxHeight: 200,
        });
    },
    SetServiceComboConfiguration: function () {
        $('#ServiceCodes').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#ServiceCodes').multiselect('selectAll', false);
        $('#ServiceCodes').multiselect('updateButtonText');
    },
    SetProviderComboConfiguration: function () {
        $('#ProviderCodes').multiselect({
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
        $('#ProviderCodes').multiselect('selectAll', false);
        $('#ProviderCodes').multiselect('updateButtonText');
    },
    postSend: function () {
        var form = document.getElementById('CalculationResultForm');
        if (form) {
            // TODO: VALIDATE THE POLICIY IDENTIFIER
            form.action = '../Process/CreatePolicy/Send';
            form.submit();
        }
    },
    postForm: function (operation) {
        //Validate Complementary Dates
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
                    //CreatePolicyController.changeAction('Create');
                    CreatePolicyController.submitForm();
                }
                else {
                    CreatePolicyController.showAlert('The complementary range is incorrect. Add more days: ' + totalDiff, 'El período de fechas complementarias es incorrecto. Agregar más días: ' + totalDiff, 'warning', 'Warning', 'Advertencia');
                }
            }
            else {
                //CreatePolicyController.changeAction('Save');
                CreatePolicyController.submitForm();
            }
        }

    },
    changeAccionForm: function (action) {
        $("#CalculationResultForm").attr("action", action);
    },
    submitForm: function () {
        var form = $('#CalculationResultForm');
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
        CreatePolicyController.SetAirportComboConfiguration();
        CreatePolicyController.SetAirlineComboConfiguration();
        CreatePolicyController.SetServiceComboConfiguration();
        CreatePolicyController.SetProviderComboConfiguration();
    }
};

$(document).ready(CreatePolicyController.Init);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}