var NationalJetFuelInvoiceController = {
    getAirlinesCombo: function () {
        $.ajax({
            url: '../NationalJetFuelInvoice/AirlineComboBox',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $('#AirlineCodeCombobox').html("");
                if (data) {
                    $.each(data, function (index, item) {
                        $('#AirlineCodeCombobox').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getAirlinesComboBlank: function () {
        $.ajax({
            url: '../NationalJetFuelInvoice/AirlineComboBox',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $('#AirlineCodeCombobox').html("");
                if (data) {
                    $.each(data, function (index, item) {
                        if (index === 0) {
                            $('#AirlineCode').append($("<option></option>").val('').html(''));
                            $('#AirlineCode').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                        }
                        else {
                            $('#AirlineCode').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                        }
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    settingModalSearch: function () {
        ItineraryController.getAirlinesComboBlank();
    },
    submitCreatePolicyForm: function () {
        var form = $('#formCreatePolicy');
        if (form) {
            form.submit();
        }
    },

    dateMonthYearFormat: function (value) {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }

        if (value) {
            console.log(value);
            if (currentLang.includes("es")) {
                var month = value.substring(3, 5);
                var year = value.substring(8);
                return month + year;
            }
            if (currentLang.includes("en")) {
                if (value.length == 10) {
                    var month = value.substring(0, 2);
                    var year = value.substring(8, 10);
                    return month + year;
                }

                if (value.length == 8) {
                    var month = value.substring(0, 1);
                    var year = value.substring(6, 8);
                    return '0' + month + year;
                }

                if (value.length == 9) {
                    var month = value.substring(0, 2);
                    var year = value.substring(7, 9);

                    if (month.indexOf('/') >= 0) {
                        month = '0' + month.substring(0, 1);
                    }
                    return month + year;
                }
            }
        }

        return '-';
    }
}