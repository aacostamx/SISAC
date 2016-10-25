var processMessageData;
var warningMessageData;
var confirmMessageData;

var ConfirmNationalPeriodController = {
    GetMessagesConfiguration: function () {
        $.ajax({
            url: '../ConfirmNationalPeriod/GetMessagesConfiguration',
            type: "GET",
            dataType: "JSON",
            cache: false,
            success: function (data) {
                processMessageData = data[0];
                warningMessageData = data[1];
                confirmMessageData = data[2];
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
    ValidatePeriod: function () {
        var periodCode = $('#PeriodCode').val();
        var proceed;
        if (periodCode) {
            $.ajax({
                url: '../ConfirmNationalPeriod/ValidatePeriod',
                data: { periodCode: periodCode },
                type: "GET",
                dataType: "text",
                async: false,
                cache: false,
                success: function (data) {
                    if (data == 'False') {
                        proceed = 0;
                    }
                    else {
                        proceed = 1;
                    }
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }

        return proceed;
    },
    GetPeriods: function () {
        $.ajax({
            url: '../ConfirmNationalPeriod/GetPeriods',
            type: 'GET',
            dataType: 'JSON',
            async: false,
            cache: false,
            success: function (data) {
                $('#PeriodCode').html("");
                if (data) {
                    $('#PeriodCode').append($("<option></option>").val('').html(''));
                    $.each(data, function (index, item) {
                        $('#PeriodCode').append($("<option></option>").val(item.PeriodCode).html(item.PeriodCode));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    FindErrors: function () {
        var periodCode = $('#PeriodCode').val();
        var errors;
        if (periodCode) {
            $.ajax({
                url: '../ConfirmNationalPeriod/FindErrors',
                data: { periodCode: periodCode },
                type: 'GET',
                dataType: 'text',
                async: false,
                cache: false,
                success: function (data) {
                    if (data == 'True') {
                        errors = 1;
                    }
                    else {
                        errors = 0;
                    }
                },
                error: function (result) {
                    console.log('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }

        return errors;
    },
    GetDatesByPeriod: function () {
        var periodCode = $('#PeriodCode').val();
        if (periodCode) {
            $.ajax({
                url: '../ConfirmNationalPeriod/GetDates',
                data: { periodCode: periodCode },
                type: 'GET',
                dataType: 'JSON',
                async: false,
                cache: false,
                success: function (data) {
                    if (data) {
                        $('#StartDatePeriod').val(data.StartDateToString);
                        $('#EndDatePeriod').val(data.EndDateToString);
                        $('#ConfirmationStatusCode').val(data.ConfirmationStatusCode);
                        $('#ConfirmationDate').val(data.ConfirmationDateToString);
                        $('#ConfirmedByUserName').val(data.ConfirmedByUserName);
                    }
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
        else {
            $('#StartDatePeriod').val('');
            $('#EndDatePeriod').val('');
            $('#ConfirmationStatusCode').val('');
            $('#ConfirmationDate').val('');
            $('#ConfirmedByUserName').val('');
        }
    },
    HideOptions: function () {
        var confirm = $('#ConfirmationStatusCode').val();
        if (!confirm) {
            $('#btnOpen').addClass('hidden');
            $('#btnClose').addClass('hidden');
            return;
        }

        if (confirm == 'CLO') {
            $('#btnOpen').removeClass('hidden');
            $('#btnClose').addClass('hidden');
        }
        else {
            $('#btnOpen').addClass('hidden');
            $('#btnClose').removeClass('hidden');
        }
    },
    ShowOnProcessMessage: function () {
        swal({
            title: processMessageData.Title,
            text: processMessageData.Text,
            type: 'warning',
            confirmButtonText: processMessageData.Confirm,
            confirmButtonColor: '#83217a',
            animation: 'slide-from-top',
            timer: 12000
        });
    },
    ShowWarningMessage: function (form) {
        swal({
            title: warningMessageData.Title,
            text: warningMessageData.Text,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: warningMessageData.Confirm,
            confirmButtonColor: '#8bc53f',
            cancelButtonText: warningMessageData.Cancel,
            cancelButtonColor: '#83217a',
            closeOnConfirm: false,
            animation: 'slide-from-top'
        },
        function () {
            form.submit();
        });
    },
    ShowConfirmMessage: function (form) {
        swal({
            title: confirmMessageData.Title,
            text: confirmMessageData.Text,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: confirmMessageData.Confirm,
            confirmButtonColor: '#8bc53f',
            cancelButtonText: confirmMessageData.Cancel,
            cancelButtonColor: '#83217a',
            closeOnConfirm: false
        },
        function () {
            form.submit();
            //ModulePermissionController.postForm();
        });
    },
    OpenPeriod: function () {
        var form = document.getElementById('ConfirmPeriodForm');
        if (form) {
            var proceed = ConfirmNationalPeriodController.ValidatePeriod();
            if (proceed == 1) {
                form.action = '../Process/ConfirmNationalPeriod/Open';
                ConfirmNationalPeriodController.ShowConfirmMessage(form);
                // form.submit();
            }
            else if (proceed == 0) {
                ConfirmNationalPeriodController.ShowOnProcessMessage();
            }
        }
    },
    ClosePeriod: function () {
        var form = document.getElementById('ConfirmPeriodForm');
        if (form) {
            var proceed = ConfirmNationalPeriodController.ValidatePeriod();
            if (proceed == 1) {
                var errors = ConfirmNationalPeriodController.FindErrors();
                if (errors == 1) {
                    ConfirmNationalPeriodController.ShowWarningMessage(form);
                }
                else {
                    ConfirmNationalPeriodController.ShowConfirmMessage(form);
                    // form.submit();
                }
            }
            else if (proceed == 0) {
                ConfirmNationalPeriodController.ShowOnProcessMessage();
            }
        }
    },
    Init: function () {
        processMessageData;
        warningMessageData;
        ConfirmNationalPeriodController.GetPeriods();
        ConfirmNationalPeriodController.GetMessagesConfiguration();
    }
}

$(document).ready(ConfirmNationalPeriodController.Init);