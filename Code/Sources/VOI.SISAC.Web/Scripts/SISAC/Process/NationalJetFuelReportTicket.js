var NationalJetFuelReportTicketController = {
    GetPeriods: function () {
        $.ajax({
            url: '../NationalJetFuelReportTicket/GetPeriods',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $('#PeriodCode').html("");
                if (data) {
                    $.each(data, function (index, item) {
                        $('#PeriodCode').append($("<option></option>").val(item.PeriodCode).html(item.PeriodCode));
                    });
                    $('#StartDatePeriod').val(data[0].StartDateToString);
                    $('#EndDatePeriod').val(data[0].EndDateToString);
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    GetDatesByPeriod: function () {
        var periodCode = $('#PeriodCode').val();
        $.ajax({
            url: '../NationalJetFuelReportTicket/GetDates',
            data: { periodCode: periodCode },
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    $('#StartDatePeriod').val(data.StartDateToString);
                    $('#EndDatePeriod').val(data.EndDateToString);
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    Init: function () {
        NationalJetFuelReportTicketController.GetPeriods();
    }
}

$(document).ready(NationalJetFuelReportTicketController.Init);