function GetNumberAirline() {
    var AirlineCode = $('#ddlCostCenter').val();
    $.ajax({
        url: '../CostCenter/GetNumberAirline',
        type: "GET",
        dataType: "JSON",
        data: { AirlineCode: AirlineCode },
        success: function (data) {
            var res = data.d;
            if (!res) {
                var res = document.getElementById('lblNombre');
                res.innerHTML = data;
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}