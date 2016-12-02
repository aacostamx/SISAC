var flag = true;

var ItineraryController = {
    iniciar: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
    },
    formatDate: function (value, row, index) {
        value = value.substr(0, 10);
        return value;
    },
    formatTime: function (value, row, index) {
        return value.substr(0, 5);
    },
    getAirlinesCombo: function () {
        $.ajax({
            url: '../Itinerary/AirlineComboBox',
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
            url: '../Itinerary/AirlineComboBox',
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
    getAirportCombo: function () {
        $.ajax({
            url: '../Itinerary/AirportComboBox',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $('#DepartureStation').html("");
                $('#ArrivalStation').html("");
                $('#LocalizationStation').html("");
                if (data) {
                    $.each(data, function (index, item) {
                        if (index === 0) {
                            $('#DepartureStation').append($("<option></option>").val('').html(''));
                            $('#ArrivalStation').append($("<option></option>").val('').html(''));
                            $('#LocalizationStation').append($("<option></option>").val('').html(''));
                        }
                        $('#DepartureStation').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                        $('#ArrivalStation').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                        $('#LocalizationStation').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm"> <button type="button" class="btn btn-default dropdown-toggle menuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> <span class="caret"></span> <span class="sr-only">Toggle Dropdown</span> </button> </div>';
    },
    clearForm: function () {
        var $form = $("#formSearchItinerary")[0];

        if ($form) {
            $('#AirlineCode').data('combobox').clearTarget();
            $('#AirlineCode').data('combobox').clearElement();
            $('#DepartureStation').data('combobox').clearTarget();
            $('#DepartureStation').data('combobox').clearElement();
            $('#ArrivalStation').data('combobox').clearTarget();
            $('#ArrivalStation').data('combobox').clearElement();
            $('#LocalizationStation').data('combobox').clearTarget();
            $('#LocalizationStation').data('combobox').clearElement();
            $('#dtpDepartureDate').data("DateTimePicker").clear();
            $('#dtpArrivalDate').data("DateTimePicker").clear();
            $('#dtpArrivalDate').data("DateTimePicker").minDate(false);
            $('#dtpDepartureDate').data("DateTimePicker").maxDate(false);
            $form.reset();
        }
    },
    searchItinerary: function (currentLang) {
        var AirlineCode, FlightNumber, EquipmentNumber, DepartureStation, ArrivalStation;
        var LocalizationStation, DepartureDate, ArrivalDate;
        var messageEmptyFields, typeOfAlert;
        var $table = $('#tbItinerary');

        $("#spanAirline").text('');
        $("#spanFlightNumber").text('');
        $("#spanEquipmentNumber").text('');
        $("#spanDepartureStation").text('');
        $("#spanArrivalStation").text('');
        $("#spanLocalizationStationn").text('');
        $("#spanDepartureDateStart").text('');
        $("#spanDepartureDateEnd").text('');

        $("#colAirline").hide();
        $("#colFlightNumber").hide();
        $("#colEquipmentNumber").hide();
        $("#colDepartureStation").hide();
        $("#colArrivalStation").hide();
        $("#colLocalizationStation").hide();
        $("#colDepartureDateStart").hide();
        $("#colDepartureDateEnd").hide();

        AirlineCode = $('#AirlineCode').val();
        FlightNumber = $('#FlightNumber').val();
        EquipmentNumber = $('#EquipmentNumber').val();
        DepartureStation = $('#DepartureStation').val();
        ArrivalStation = $('#ArrivalStation').val();
        LocalizationStation = $('#LocalizationStation').val();
        DepartureDate = $('#DepartureDate').val();
        ArrivalDate = $('#ArrivalDate').val();

        if (!AirlineCode && !FlightNumber && !EquipmentNumber && !DepartureStation
            && !ArrivalStation && !LocalizationStation && !DepartureDate && !ArrivalDate) {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Es necesario ingresar por lo menos un campo";
                typeOfAlert = "Alerta";
            }
            else {
                messageEmptyFields = "You must enter at least one field";
                typeOfAlert = "Warning";
            }
            swal({
                title: typeOfAlert,
                text: messageEmptyFields,
                type: "warning",
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
        }
        else {
            if ((DepartureDate && !ArrivalDate) || (!DepartureDate && ArrivalDate)) {
                if (currentLang.includes("es")) {
                    messageEmptyFields = "Es necesario ingresar la fecha de salida inicial y final";
                    typeOfAlert = "Alerta";
                }
                else {
                    messageEmptyFields = "You must enter the date of initial departure and final";
                    typeOfAlert = "Warning";
                }
                swal({
                    title: typeOfAlert,
                    text: messageEmptyFields,
                    type: "warning",
                    confirmButtonColor: "#83217a",
                    animation: "slide-from-top",
                    timer: 12000
                })
                return;
            }

            $table.bootstrapTable('removeAll');
            $("#btnAddItineary").hide();
            $("#btnUploadItinerary").hide();
            if (currentLang.includes("es")) {
                $("#titleItinery").text('Búsqueda Avanzada de Itinerarios');
                $("#btnSearchItineary").text('Modificar Búsqueda');
                $("#btnSearchItineary").prepend('<i class="fa fa-pencil fa-fw"></i>&nbsp;')
            } else {
                $("#titleItinery").text('Advance Search Flights Schedule');
                $("#btnSearchItineary").text('Modify Search');
                $("#btnSearchItineary").prepend('<i class="fa fa-pencil fa-fw"></i>&nbsp;');
            }

            if (AirlineCode) {
                $("#spanAirline").text(AirlineCode);
                $("#colAirline").show();
            }
            if (FlightNumber) {
                $("#spanFlightNumber").text(FlightNumber);
                $("#colFlightNumber").show();
            }
            if (EquipmentNumber) {
                $("#spanEquipmentNumber").text(EquipmentNumber);
                $("#colEquipmentNumber").show();
            }
            if (DepartureStation) {
                $("#spanDepartureStation").text(DepartureStation);
                $("#colDepartureStation").show();
            }
            if (LocalizationStation) {
                $("#spanLocalizationStation").text(LocalizationStation);
                $("#colLocalizationStation").show();
            }
            if (ArrivalStation) {
                $("#spanArrivalStation").text(ArrivalStation);
                $("#colArrivalStation").show();
            }
            if (DepartureDate && ArrivalDate) {
                $("#spanDepartureDateStart").text(DepartureDate);
                $("#spanDepartureDateEnd").text(ArrivalDate);
                $("#colDepartureDateStart").show();
                $("#colDepartureDateEnd").show();
            }
            $("#divSearchItineary").show();
            $("#btnClearForm").show();
            $table.bootstrapTable('refresh');
            ItineraryController.closeModal();
        }
    },
    closeModal: function () {
        $('#SearchItinerary').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    },
    activeComboboxes: function () {
        if (flag === true) {
            $('.combobox').combobox();
            flag = false;
        }
    },
    addClassCombobox: function () {
        if ($('.combobox').length === 0) {
            $('#DepartureStation').addClass('combobox');
            $('#ArrivalStation').addClass('combobox');
            $('#AirlineCode').addClass('combobox');
            $('#LocalizationStation').addClass('combobox');
        }
    },
    settingModalSearch: function () {
        ItineraryController.addClassCombobox();
        ItineraryController.getAirlinesComboBlank();
        ItineraryController.getAirportCombo();
        ItineraryController.activeComboboxes(flag);
    },
    upperCase: function (Control) {
        var controlName = document.getElementById(Control.id);
        controlName.value = controlName.value.toUpperCase();
        return true;
    },
    validarMaxLengthAlfanumerico: function (e, Control) {
        ItineraryController.upperCase(Control);
        ItineraryController.deshabiliarEspacio(e);
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla === 8) return true;
        patron = /[a-zA-Z0-9 ]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    },
    deshabiliarEspacio: function (event) {
        if (event.keyCode === 32) {
            event.returnValue = false;
            return false;
        }
    },
    showSweetAlert: function (messageEn, messageEs, alertType, titleEn, tittleEs) {
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
    getMtvMessage: function (row) {
        $.ajax({
            type: 'GET',
            url: '../AircraftMovementMessage/GetAircraftMovementMessage',
            data: { sequence: row.Sequence, airlineCode: row.AirlineCode, flightNumber: row.FlightNumber, itineraryKey: row.ItineraryKey }, //JSON.stringify(row),
            //contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data, textStatus, jqXHR) {
                if (data) {
                    var msg = JSON.parse(data);
                    var html = '<p>' + msg.Title + '</p><br />' +
                        '<p>' + msg.DepartureInformation + '</p><br />' +
                        '<p>' + msg.ArrivalInformation + '</p><br />' +
                        '<p>' + msg.JetFuelInformation + '</p><br />';

                    $.each(msg.DelaysInformation, function (index, item) {
                        html += '<p>' + item + '</p><br />';
                    });

                    html += '<p>' + msg.CaptainsInformation + '</p><br />' +
                        '<p>' + msg.StewardessInformation + '</p><br />' +
                        '<p>' + msg.ChargeInformationTitle + '</p><br />';

                    $.each(msg.ChargeInformation, function (index, item) {
                        html += '<p>' + item + '</p><br />';
                    });
                    $('#mvt-modal-body').html(html);
                    $('#mvt-modal').modal('show');
                }
                else {
                    swal({
                        title: "Advertencia.",
                        text: "No se encontró información del mensaje MVT, vuelva a intentar. Si el problema persiste consulte al administrador",
                        type: "warning",
                        confirmButtonColor: "#83217a",
                        animation: "slide-from-top",
                        timer: 12000
                    });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
                alert(errorThrown);
            }
        });
    }
}

$(document).ready(ItineraryController.iniciar);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}