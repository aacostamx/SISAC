var JetFuelCalculationResultController = {
    SelectOption: function (type) {
        // JetFuelCalculationResultController.ResetOption();

        if (type === 'PeriodOption') {
            $('#PeriodCombo').show('slow');
            $('#PeriodCombo').val('');
            $("#StartDate").attr("readonly", true);
            $("#EndDate").attr("readonly", true);
            $('#StartDate').val('');
            $('#EndDate').val('');
        }
        else if (type === 'OpenOption') {
            $('#PeriodCombo').hide('slow');
            $('#PeriodCode').multiselect('select', '');
            $("#StartDate").attr("readonly", false);
            $("#EndDate").attr("readonly", false);
            $('#StartDate').val('');
            $('#EndDate').val('');
        }
    },
    GetDatesByPeriod: function () {
        var periodCode = $('#PeriodCode').val();
        if (periodCode) {
            $.ajax({
                url: '../JetFuelCalculationResult/GetDates',
                data: { periodCode: periodCode },
                type: 'GET',
                dataType: 'JSON',
                async: false,
                cache: false,
                success: function (data) {
                    if (data) {
                        $('#StartDate').val(data.StartDateToString);
                        $('#EndDate').val(data.EndDateToString);
                    }
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
        else {
            $('#StartDate').val('');
            $('#EndDate').val('');
        }
    },
    SetAirportComboConfiguration: function () {
        $('#StationCode').multiselect({
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
        $('#StationCode').multiselect('selectAll', false);
        $('#StationCode').multiselect('updateButtonText');
    },
    SetAirlineComboConfiguration: function () {
        $('#AirlineCode').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#AirlineCode').multiselect('selectAll', false);
        $('#AirlineCode').multiselect('updateButtonText');
    },
    SetServiceComboConfiguration: function () {
        $('#ServiceCode').multiselect({
            selectAllText: 'Select All',
            allSelectedText: 'All Selected',
            includeSelectAllOption: true,
            buttonClass: 'btn-multi-select col-xs-12',
            numberDisplayed: 1,
            maxHeight: 200,
        });
        $('#ServiceCode').multiselect('selectAll', false);
        $('#ServiceCode').multiselect('updateButtonText');
    },
    SetProviderComboConfiguration: function () {
        $('#ProviderNumber').multiselect({
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
        $('#ProviderNumber').multiselect('selectAll', false);
        $('#ProviderNumber').multiselect('updateButtonText');
    },
    SetPeriodComboConfiguration: function () {
        $('#PeriodCode').multiselect({
            buttonClass: 'btn-multi-select col-xs-12',
            maxHeight: 200,
        });
    },
    PostForm: function (type) {
        var form = document.getElementById('CalculationResultForm');
        if (form) {
            if (type === 'Policy') {
                form.action = '../Process/JetFuelCalculationResult/CreatePolicy';
            }
            else if (type === 'Group') {
                form.action = urlReportGroup;
            }
            else if (type === 'Detailed') {
                form.action = urlReportDetail;
            }
            else if (type === 'Error') {
                form.action = urlReportError;
            }

            form.submit();
        }
    },
    ShowCreatePolicyModal: function () {
        var startDate = $('#StartDate').val();
        var endDate = $('#EndDate').val();
        if (startDate && endDate) {
            $('PeriodString').html(startDate + '-' + endDate);
            $('#CreatePolicyModal').modal('show');
        }
        else {
            swal({
                // title: warningMessageData.Title,
                title: 'Warning',
                // text: warningMessageData.Text,
                text: 'Specified the start and end dates.',
                type: 'warning',
                // confirmButtonText: warningMessageData.Confirm,
                confirmButtonText: 'Accept',
                confirmButtonColor: '#8bc53f',
                closeOnConfirm: true,
                animation: 'slide-from-top'
            });
        }
    }
};

$(document).ready(function () {
    JetFuelCalculationResultController.SetAirportComboConfiguration();
    JetFuelCalculationResultController.SetAirlineComboConfiguration();
    JetFuelCalculationResultController.SetServiceComboConfiguration();
    JetFuelCalculationResultController.SetProviderComboConfiguration();
    JetFuelCalculationResultController.SetPeriodComboConfiguration();
});