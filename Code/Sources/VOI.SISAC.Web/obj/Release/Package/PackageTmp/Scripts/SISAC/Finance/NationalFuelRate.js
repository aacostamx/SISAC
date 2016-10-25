var rowSelected;

var NationalFuelRate = {
    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm">' +
                    '<button type="button" class="btn btn-default dropdown-toggle menuButton"' +
                        'data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                            '<span class="caret"></span> <span class="sr-only">Toggle Dropdown</span>' +
                    '</button>' +
                '</div>';
    },
    submitForm: function () {
        var form = $('#NationalFuelRateForm');
        if (form) {
            form.submit();
        }
    },
}

$(document).ready();