var contractParams = [
    'EffectiveDate',
    'AirlineCode',
    'StationCode',
    'ServiceCode',
    'ProviderNumber',
    'CostCenterNumber'
];

var rowSelected;

var AirportServiceContract = {
    GetAirlineCode: function () {
        var path = window.location.pathname;
        var AirlineCode = $('#ddlCCFlag').val();
        if (AirlineCode != "") {
            $.ajax({
                url: '../AirportServiceContract/GetAirlineCode',
                type: "GET",
                dataType: "JSON",
                data: { AirlineCode: AirlineCode },
                success: function (data) {
                    $("[id$='CostCenterNumber']").html("");
                    $.each(data, function (index, item) {
                        $("[id$='CostCenterNumber']").append($("<option></option>").val(item.Id).html(item.Description));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
    }
    ,
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
        var contract = AirportServiceContract.GetContractValues();
        var endDate = AirportServiceContract.GetDatePickerValue();
        if (endDate == null || endDate == 'undefined') {
            AirportServiceContract.GetValidationMessage(endDate, contract);
            return;
        }

        var ok = AirportServiceContract.ValidateDate(endDate, contract);
        if (ok != 1) {
            return;
        }

        var success = AirportServiceContract.Inactivate(endDate, contract);
        if (success != undefined && success != 'undefined') {
            location.reload();
        }
    }
    ,
    Inactivate: function (endDate, contract) {
        var something;
        $.ajax({
            url: '../AirportServiceContract/InactivateContract',
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
        var tableData = $('#tbAirportServiceContract').bootstrapTable('getSelections');

        // sets the data on an object -contractParams- and converts it to Json string
        var data = JSON.stringify(rowSelected, contractParams);
        //var data = JSON.stringify(tableData[0], contractParams);

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
        dataToJason.ProviderNumber = dataToJason.ProviderNumber.trim();
        dataToJason.CostCenterNumber = dataToJason.CostCenterNumber.trim();
        return JSON.stringify(dataToJason);
    }
    ,
    GetValidationMessage: function () {
        $.ajax({
            url: '../AirportServiceContract/GetValidationMessage',
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
            url: '../AirportServiceContract/ValidateDate',
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
    }
    ,
    toggleRateParams: function () {
        if ($('#MultiRateFlag') && $('#rate-params')) {
            var $rp = $('#rate-params');
            var $mrf = $('#MultiRateFlag');
            $rp[0].style.display = (!$mrf[0].checked) ? 'block' : 'none';
            if ($rp[0].style.display == 'none') {
                $("#Rate").val('');
                $("#CurrencyCode").val('');
            }
        }
    }
}