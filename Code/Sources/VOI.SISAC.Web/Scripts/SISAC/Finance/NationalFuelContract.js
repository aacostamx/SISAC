var contractParams = [
    'EffectiveDate',
    'AirlineCode',
    'StationCode',
    'ServiceCode',
    'ProviderNumberPrimary',
];

var rowSelected;

var NationalFuelContract = {
    GetAirlineCode: function () {
        var path = window.location.pathname;
        var AirlineCode = $('#ddlCCFlag').val();
        if (AirlineCode != "") {
            $.ajax({
                url: '../NationalFuelContract/GetAirlineCode',
                type: "GET",
                dataType: "JSON",
                data: { AirlineCode: AirlineCode },
                success: function (data) {
                    $("[id$='CCNumber']").html("");
                    $.each(data, function (index, item) {
                        $("[id$='CCNumber']").append($("<option></option>").val(item.Id).html(item.Description));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
    },

    ShowCostCenterInput: function (id) {
        if (document.getElementById) {
            var el = document.getElementById(id);
            el.style.display = (el.style.display == 'none') ? 'block' : 'none';

            if (el.style.display == 'none') {
                $("[id$='CCNumber']").val('');
            }
        }
    },

    Initialize: function () {
        var flag = document.getElementById('AircraftRegistCCFlag');
        if (flag) {
            if (flag.checked) {
                $('#CostCenterInput').hide();
            }
            else {
                $('#CostCenterInput').show();
            }
        }
    },

    muestraoculta: function (id) {
        if (document.getElementById) {
            var el = document.getElementById(id);
            el.style.display = (el.style.display == 'none') ? 'block' : 'none';
            if (el.style.display == 'none') {
                $("[id$='AirplaneWeightCode']").val('');
                $("[id$='AirplaneWeightUnit']").val('');
                $("[id$='AirplaneWeightMultiplier']").val('');
            }
        }
    }
    ,
    toggle_visibility: function (id) {
        var e = document.getElementById(id);
        e.style.display = (e.style.display == 'none') ? 'block' : 'none';
    }
    ,
    inactiveContract: function () {
        var contract = NationalFuelContract.GetContractValues();
        var endDate = NationalFuelContract.GetDatePickerValue();
        if (endDate == null || endDate == 'undefined') {
            NationalFuelContract.GetValidationMessage(endDate, contract);
            return;
        }

        var ok = NationalFuelContract.ValidateDate(endDate, contract);
        if (ok != 1) {
            return;
        }

        var success = NationalFuelContract.Inactivate(endDate, contract);
        if (success != undefined && success != 'undefined') {
            location.reload();
        }
    }
    ,
    Inactivate: function (endDate, contract) {
        var something;
        $.ajax({
            url: '../NationalFuelContract/InactivateContract',
            type: "POST",
            dataType: "JSON",
            async: false,
            data: { endDate: endDate, contract: contract },
            success: function (data) {
                something = 1;
            },
            error: function (request, status, error) {
                if (request.status == 403) {
                    $('#ShowEndDate').modal('hide');
                    $('#UnauthorizedModal').modal('show');
                }
            }
        });

        return something;
    }
    ,
    GetDatePickerValue: function () {
        var dateTimePicker = $('#EndDateContract').data("DateTimePicker").date();
        if (dateTimePicker == null || dateTimePicker == 'undefined')
            return null;
        return dateTimePicker._d.toDateString();
    }
    ,
    GetContractValues: function () {
        // gets the selected data from the table
        var tableData = $('#tbNationalFuelContract').bootstrapTable('getSelections');

        // sets the data on an object -contractParams- and converts it to Json string
        var data = JSON.stringify(rowSelected, contractParams);

        // converts to Json object
        var dataToJason = JSON.parse(data);

        // Sets the current date
        if (currentLang == "en-US") {
            var date = moment(dataToJason.EffectiveDate.trim(), 'MM/DD/YYYY', currentLang);
        }
        else {
            var date = moment(dataToJason.EffectiveDate.trim(), 'DD/MM/YYYY', currentLang);
        }

        // sets format
        dataToJason.EffectiveDate = date;
        dataToJason.AirlineCode = dataToJason.AirlineCode.trim();
        dataToJason.StationCode = dataToJason.StationCode.trim();
        dataToJason.ServiceCode = dataToJason.ServiceCode.trim();
        dataToJason.ProviderNumberPrimary = dataToJason.ProviderNumberPrimary.trim();
        return JSON.stringify(dataToJason);
    }
    ,
    GetValidationMessage: function () {
        $.ajax({
            url: '../NationalFuelContract/GetValidationMessage',
            type: "GET",
            dataType: "JSON",
            data: {},
            success: function (data) {
                var res = data.d;
                if (!res) {
                    var res = document.getElementById('ErrorMessageModal');
                    res.innerHTML = data;
                }
            }
        });
    }
    ,
    ValidateDate: function (endDate, contract) {
        var n = 0;
        $.ajax({
            url: '../NationalFuelContract/ValidateDate',
            type: "GET",
            dataType: "JSON",
            async: false,
            data: { endDate: endDate, contract: contract },
            success: function (data) {
                var res = data.d;
                if (!res && res == "OK") {
                    var res = document.getElementById('ErrorMessageModal');
                    res.innerHTML = data;
                }

                n = 1;
            },
            error: function (request, status, error) {
                var res = document.getElementById('AlertMessageModal');
                res.innerHTML = request.responseText;
            }
        });

        return n;
    }
    ,
    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm">' +
                    '<button type="button" class="btn btn-default dropdown-toggle menuButton"' +
                        'data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                            '<span class="caret"></span> <span class="sr-only">Toggle Dropdown</span>' +
                    '</button>' +
                '</div>';
    }
    ,
    showInactiveWindow: function (row) {
        // gets the selected data from the table
        rowSelected = row;

        // Shows modal for set the end contract date
        $('#ShowEndDate').modal('show');
    },
}

$(document).ready(NationalFuelContract.Initialize());