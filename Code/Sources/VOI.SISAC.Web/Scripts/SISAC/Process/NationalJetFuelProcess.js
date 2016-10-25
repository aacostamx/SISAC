var NationalJetFuelProcessController = {
    GetFuelProcess: function () {
        NationalJetFuelProcessController.blankTable();
        var periodCode = $('#PeriodCode').val();

        if (periodCode) {
            $.ajax({
                url: '../NationalJetFuelProcess/GetFuelProcess',
                data: { periodCode: periodCode },
                type: "GET",
                dataType: "JSON",
                async: false,
                success: function (data) {
                    if (data) {
                        var confirmationStatus;
                        var closed;

                        $('#StartDatePeriod').text(NationalJetFuelProcessController.getDateOnly(data.StartDatePeriod));
                        $('#EndDatePeriod').text(NationalJetFuelProcessController.getDateOnly(data.EndDatePeriod));
                        $('#StatusProcessCode').text(data.StatusProcess.StatusProcessName);
                        NationalJetFuelProcessController.setProcessProgress(data);
                        $('#StartDateProcess').text(NationalJetFuelProcessController.formatDate(data.StartDateProcess));
                        $('#EndDateProcess').text(NationalJetFuelProcessController.formatDate(data.EndDateProcess));
                        $('#ProcessedByUserName').text(data.ProcessedByUserName);
                        $('#CalculationStatusCode').text(data.CalculationStatus.CalculationStatusName);
                        $('#ConfirmationStatusCode').text(data.ConfirmationStatus.ConfirmationStatusName);
                        $('#ConfirmationDate').text(NationalJetFuelProcessController.formatDate(data.ConfirmationDate));
                        $('#ConfirmedByUserName').text(data.ConfirmedByUserName);

                        //Si el estatus de confirmacin es CLO = CLOSED no se podra revertir ni reprocesar
                        confirmationStatus = data.ConfirmationStatus.ConfirmationStatusCode;
                        //Si closed es true ya se mando a SAP y por no se podra revertir ni reprocesar
                        closed = data.ClosedProvision;

                        NationalJetFuelProcessController.setInitialActions();

                        if (confirmationStatus != 'CLO' && closed) {
                            $("#btnRevertNationalJetFuelProcess").prop('disabled', true);
                            $("#btnStartNationalJetFuelProcess").prop('disabled', false);
                            $("#processRadio").show();
                            $("#lbAll").hide();
                            NationalJetFuelProcessController.selectProcessType("processPending")
                            return;
                        }

                        if (confirmationStatus === 'CLO') {
                            $("#btnRevertNationalJetFuelProcess").prop('disabled', true);
                            $("#btnStartNationalJetFuelProcess").prop('disabled', true);
                            $("#processRadio").hide();
                        }
                        else {
                            $("#btnRevertNationalJetFuelProcess").prop('disabled', false);
                            $("#btnStartNationalJetFuelProcess").prop('disabled', false);
                            $("#processRadio").show();
                        }

                    }
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
    },
    setProcessProgress: function (obj) {
        var $progressBar = $('#progressBar');
        var value = obj.ProcessProgress;

        $('#hiddenBar').show();

        if (value) {
            var percentage = String();
            var iValue = parseInt(value);

            switch (true) {
                case (iValue < 0):
                    percentage = '0';
                    break;
                case (iValue > 100):
                    percentage = '100'
                    break;
                default:
                    percentage = String(value);
            }

            percentage = percentage.concat('%');
            $progressBar.text(percentage);
            $progressBar.css('width', percentage);
        }
        else {
            $progressBar.text('');
            $progressBar.css('width', '0');
        }

    },
    resetProgressBar: function () {
        var $progressBar = $('#progressBar');

        $progressBar.text('');
        $progressBar.css('width', '0');

        $('#hiddenBar').hide();
    },
    blankTable: function () {
        $('#StartDatePeriod').text('');
        $('#EndDatePeriod').text('');
        $('#StatusProcessCode').text('');
        NationalJetFuelProcessController.resetProgressBar();
        $('#StartDateProcess').text('');
        $('#EndDateProcess').text('');
        $('#ProcessedByUserName').text('');
        $('#CalculationStatusCode').text('');
        $('#ConfirmationStatusCode').text('');
        $('#ConfirmationStatusName').text('');
        $('#ConfirmationDate').text('');
        $('#ConfirmedByUserName').text('');
    },
    getDateOnly: function (value) {
        var date;
        if (value) {
            date = value.substr(0, 10);
        }
        return date;
    },
    getTimeOnly: function (value) {
        var time;
        if (value) {
            time = value.substr(11, 5);
        }
        return time;
    },
    formatDate: function (value) {
        var date, time, datetime;
        if (value) {
            date = value.substr(0, 10);
            time = value.substr(11, 5);
            datetime = date.concat(' ', time);
        }
        return datetime;
    },
    selectProcessType: function (type) {
        NationalJetFuelProcessController.resetProcessType();

        if (type === 'processAll') {
            $('#processAllDOM').prop("checked", true);
            $('#processPendingDOM').prop("checked", false);

            $('#processAll').val(true);
            $('#processPending').val(false);
        }
        else if (type === 'processPending') {
            $('#processAllDOM').prop("checked", false);
            $('#processPendingDOM').prop("checked", true);

            $('#processAll').val(false);
            $('#processPending').val(true);
        }

    },
    initialProcessType: function () {
        $('#processAllDOM').prop("checked", true);
        $('#processPendingDOM').prop("checked", false);

        $('#processAll').val(true);
        $('#processPending').val(false);
    },
    resetProcessType: function () {
        $('#processAllDOM').prop("checked", false);
        $('#processPendingDOM').prop("checked", false);

        $('#processAll').val('');
        $('#processPending').val('');
    },
    revertNationalJetFuelProcess: function () {
        var period = $("#PeriodCode").val();
        var url = "../NationalJetFuelProcess/RevertProcess";
        var post;

        if (period) {
            NationalJetFuelProcessController.blankTable();
            NationalJetFuelProcessController.initialProcessType();
            NationalJetFuelProcessController.resetComboboxPeriod();

            // Send the data using post
            post = $.post(url, { periodCode: period });

            // Put the results in a div
            post.done(function (data) {
                if (data === 'True') {
                    NationalJetFuelProcessController.showAlert('The fuel process has been reversed', 'El proceso de combustible ha sido revertido', 'success', 'Success', 'Éxito');
                }

            });
        }
        else {
            NationalJetFuelProcessController.selectPeriod();
        }

    },
    selectPeriod: function () {
        NationalJetFuelProcessController.showAlert('You need to select a period', 'Es necesario seleccionar un período', 'info', 'Warning', 'Advertencia');
    },
    startNationalJetFuelProcess: function () {
        var period = $("#PeriodCode").val();
        var all = $("#processAll").val();
        var pending = $("#processPending").val();
        var url = "../NationalJetFuelProcess/StartProcess";
        var post;

        if (period) {
            NationalJetFuelProcessController.blankTable();
            NationalJetFuelProcessController.initialProcessType();
            NationalJetFuelProcessController.resetComboboxPeriod();

            // Send the data using post
            post = $.post(url, { periodCode: period, processAll: all, processPending: pending });

            // Put the results in a div
            post.done(function (data) {
                if (data === 'True') {
                    NationalJetFuelProcessController.showAlert('The fuel process is complete', 'El proceso de combustible ha finalizado', 'success', 'Success', 'Éxito');
                }
            });
        }
        else {
            NationalJetFuelProcessController.selectPeriod();
        }
    },
    resetComboboxPeriod: function () {
        $("#PeriodCode").val('');
    },
    setInitialActions: function () {
        $("#btnRevertNationalJetFuelProcess").prop('disabled', false);
        $("#btnStartNationalJetFuelProcess").prop('disabled', false);
        $("#processRadio").show();
        $("#lbAll").show();
        $("#lbPen").show();
        NationalJetFuelProcessController.selectProcessType("processAll")
    },
    setupAjax: function () {
        $.ajaxSetup({ cache: false, async: false });
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
    startDoc: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
        NationalJetFuelProcessController.setupAjax();
        NationalJetFuelProcessController.initialProcessType();
    }
}

$(document).ready(NationalJetFuelProcessController.startDoc);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}