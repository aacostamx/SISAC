var editionMode = false;
var updateFirst = false;

var AirportServicesController = {
    toggle: function (name) {
        console.log(name);
    },
    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm"> <button type="button" class="btn btn-default dropdown-toggle menuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> <span class="caret"></span> <span class="sr-only">Toggle Dropdown</span> </button> </div>';
    },
    setStartTime: function (value, row, index) {
        var startTime;
        if (value) {
            value = value.substring(0, 10);
            startTime = row.StartTimeService;

            if (startTime) {
                startTime = startTime.substring(0, 5);
                value = value + " " + startTime;
            }
        }
        return value;
    },
    setEndTime: function (value, row, index) {
        var endTime;
        if (value) {
            value = value.substring(0, 10);
            endTime = row.EndTimeService;

            if (endTime) {
                endTime = endTime.substring(0, 5);
                value = value + " " + endTime;
            }
        }
        return value;
    },
    createService: function () {
        var url = window.location.href;
    },
    GetProviders: function () {
        var airlineCode = $('#AirlineCode').val();
        var serviceCode = $('#ServicesDropDown').val();
        var arrivalStation = $('#ArrivalStation').val();
        var departureStation = $('#DepartureStation').val();
        var operationTypeName = $('#OperationTypeName').val();

        if (airlineCode && serviceCode && arrivalStation && departureStation && operationTypeName) {
            $.ajax({
                url: '../AirportService/GetProvidersByService',
                type: "GET",
                dataType: "JSON",
                async: false,
                data: {
                    airlineCode: airlineCode,
                    arrivalStation: arrivalStation,
                    departureStation: departureStation,
                    serviceCode: serviceCode,
                    operationType: operationTypeName
                },
                success: function (data) {
                    $('#ProviderNumber').html("");
                    $.each(data, function (index, item) {
                        $('#ProviderNumber').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status);
                }
            });
        }
        AirportServicesController.ShowAditionalOptions(serviceCode);
    },
    ShowAditionalOptions: function (serviceCode) {
        if (serviceCode.indexOf('AGPO') > -1) {
            document.getElementById('DrinkingWaterSection').style.display = 'block';
            document.getElementById('GpuSection').style.display = 'none';
            document.getElementById('ParkingSection').style.display = 'none';
            var equipmentNumber = $('#EquipmentNumber').val();
            if (!updateFirst) {
                AirportServicesController.GetDrinkingWater(equipmentNumber);
            }

            AirportServicesController.setDefaultValues(serviceCode);
            return;
        }

        if (serviceCode.indexOf('EPP') > -1) {
            document.getElementById('DrinkingWaterSection').style.display = 'none';
            document.getElementById('GpuSection').style.display = 'none';
            document.getElementById('ParkingSection').style.display = 'block';
            AirportServicesController.setDefaultValues(serviceCode);
            return;
        }

        if (serviceCode.indexOf('GPU') > -1) {
            document.getElementById('DrinkingWaterSection').style.display = 'none';
            document.getElementById('GpuSection').style.display = 'block';
            document.getElementById('ParkingSection').style.display = 'none';

            var arrivalStation = $('#ArrivalStation').val();
            var departureStation = $('#DepartureStation').val();
            var operationTypeName = $('#OperationTypeName').val();
            if (!updateFirst) {
                AirportServicesController.GetGpuConceps(arrivalStation, departureStation, operationTypeName);
                AirportServicesController.GetGpuObsercations();
            }

            AirportServicesController.setDefaultValues(serviceCode);
            return;
        }
        AirportServicesController.setDefaultValues(serviceCode);
        document.getElementById('DrinkingWaterSection').style.display = 'none';
        document.getElementById('GpuSection').style.display = 'none';
        document.getElementById('ParkingSection').style.display = 'none';
    },
    GetDrinkingWater: function (equipmentNumber) {
        if (equipmentNumber) {
            $.ajax({
                url: '../AirportService/GetDrinkingWaters',
                type: "GET",
                dataType: "JSON",
                async: false,
                data: { equipmentNumber: equipmentNumber },
                success: function (data) {
                    $('#DrinkingWaterID').html("");
                    $.each(data, function (index, item) {
                        $('#DrinkingWaterID').append($("<option></option>").val(item.Id).html(item.Description));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status);
                }
            });
        }
    },
    GetGpuConceps: function (arrivarl, departure, operationType) {
        if (arrivarl || departure || operationType) {
            $.ajax({
                url: '../AirportService/GetGpu',
                type: "GET",
                dataType: "JSON",
                async: false,
                data: {
                    arriveStation: arrivarl,
                    departureStation: departure,
                    operationType: operationType
                },
                success: function (data) {
                    $('#GpuCode').html("");
                    $.each(data, function (index, item) {
                        $('#GpuCode').append($("<option></option>").val(item.Id).html(item.Id));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status);
                }
            });
        }
    },
    GetGpuObsercations: function () {
        $.ajax({
            url: '../AirportService/GetGpuObservation',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $('#GpuObservationCode').html("");
                $.each(data, function (index, item) {
                    $('#GpuObservationCode').append($("<option></option>").val(item.Id).html(item.CodeAndDescription));
                });
            },
            error: function (result) {
                alert("ERROR " + result.status);
            }
        });
    },
    postForm: function () {
        var serviceCode = $('#ServicesDropDown').val();
        var providerNumber = $('#ProviderNumber').val();
        var apronPosition = $('#ApronPosition').val();
        var startDateService = $('#StartDateService').val();
        var startTimeService = $('#StartTimeService').val();
        var endDateService = $('#EndDateService').val();
        var endTimeService = $('#EndTimeService').val();

        if (serviceCode
            && providerNumber
            && apronPosition
            && startDateService
            && endDateService
            && endTimeService) {

            if (startDateService <= endDateService) {

                if (startDateService == endDateService &&
                    endTimeService < startTimeService) {
                    if (currentLang.includes("es")) {
                        messageEmptyFields = "Es necesario que la hora final sea mayor a la hora inicial";
                        typeOfAlert = "Advertencia";
                    }
                    else {
                        messageEmptyFields = "It is necessary that End Time is greater than the Start Time";
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

                //AGPO - AGUA POTABLE
                if (serviceCode.indexOf('AGPO') > -1) {
                    var drinkingWaterID = $("#DrinkingWaterID").val();

                    if (!drinkingWaterID) {
                        if (currentLang.includes("es")) {
                            messageEmptyFields = "Es necesario ingresar todos los campos de Agua Potable";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageEmptyFields = "You must enter all fields of Drinking Water";
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
                }
                //EP - ESTACIONAMIENTO EN PLATAFORMA
                if (serviceCode.indexOf('EP') > -1) {
                    var fuelBeforeLanding = $("#FuelBeforeLanding").val();
                    var fuelLoaded = $("#FuelLoaded").val();

                    if (!fuelBeforeLanding || !fuelLoaded) {
                        if (currentLang.includes("es")) {
                            messageEmptyFields = "Es necesario ingresar todos los campos de Combustible";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageEmptyFields = "You must enter all fields of Fuel";
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
                }
                //GPU	PLANTA ELECTRICA (GPU-VOI)
                //GPU 3 HR	PLANTA ELECTRICA GPU MAYOR A 3 HR
                //GPU-PROVE	PLANTA ELECTRICA GPU-PROVEEDOR
                if (serviceCode.indexOf('GPU') > -1) {
                    var gpuCode = $("#GpuCode").val();
                    var gpuObservationCode = $("#GpuObservationCode").val();
                    var gpuStartMeter = $("#GpuStartMeter").val();
                    var gpuEndMeter = $("#GpuEndMeter").val();

                    if (!gpuCode || !gpuObservationCode || !gpuStartMeter || !gpuEndMeter) {
                        if (currentLang.includes("es")) {
                            messageEmptyFields = "Es necesario ingresar todos los campos de GPU";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageEmptyFields = "You must enter all fields of GPU";
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
                }
                var form = document.getElementById('formAirportService');
                if (form) {
                    form.submit();
                }
            }
            else {
                if (currentLang.includes("es")) {
                    messageEmptyFields = "Es necesario que la fecha final sea mayor que la inicial";
                    typeOfAlert = "Advertencia";
                }
                else {
                    messageEmptyFields = "It is necessary that the end date is greater than start date";
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

        }
        else {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Es necesario ingresar todos los campos requeridos";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "You must enter all fields";
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
    },
    postFormDelete: function () {
        var form = document.getElementById('formAirportServiceDelete');
        if (form) {
            form.submit();
        }
    },
    setDefaultValues: function (option) {
        if (option.indexOf('GPU') > -1) {
            $("#DrinkingWaterID").val('');
            $("#DrinkingWaterID").text('');
            $("#FuelBeforeLanding").val('');
            $("#FuelBeforeLanding").text('');
            $("#FuelLoaded").val('');
            $("#FuelLoaded").text('');
            return;
        }

        if (option.indexOf('AGPO') > -1) {
            $("#FuelBeforeLanding").val('');
            $("#FuelBeforeLanding").text('');
            $("#FuelLoaded").val('');
            $("#FuelLoaded").text('');
            $("#GpuCode").val('');
            $("#GpuCode").text('');
            $("#GpuStartMeter").val('');
            $("#GpuStartMeter").text('');
            $("#GpuEndMeter").val('');
            $("#GpuEndMeter").text('');
            $("#GpuObservationCode").val('');
            $("#GpuObservationCode").text('');
            return;
        }

        if (!option.indexOf('EP') > -1) {
            $("#DrinkingWaterID").val('');
            $("#DrinkingWaterID").text('');
            $("#GpuCode").val('');
            $("#GpuCode").text('');
            $("#GpuStartMeter").val('');
            $("#GpuStartMeter").text('');
            $("#GpuEndMeter").val('');
            $("#GpuEndMeter").text('');
            $("#GpuObservationCode").val('');
            $("#GpuObservationCode").text('');
            return;
        }

        $("#FuelBeforeLanding").val('');
        $("#FuelBeforeLanding").text('');
        $("#FuelLoaded").val('');
        $("#FuelLoaded").text('');
        $("#GpuCode").val('');
        $("#GpuCode").text('');
        $("#GpuStartMeter").val('');
        $("#GpuStartMeter").text('');
        $("#GpuEndMeter").val('');
        $("#GpuEndMeter").text('');
        $("#GpuObservationCode").val('');
        $("#GpuObservationCode").text('');
        $("#DrinkingWaterID").val('');
        $("#DrinkingWaterID").text('');
    },
    messageValidations: function (message_es, alert_es, message_en, alert_en) {
        if (currentLang.includes("es")) {
            messageEmptyFields = message_es;
            typeOfAlert = alert_es;
        }
        else {
            messageEmptyFields = message_en;
            typeOfAlert = alert_en;
        }
        swal({
            title: typeOfAlert,
            text: messageEmptyFields,
            type: alert_en,
            confirmButtonColor: "#83217a",
            animation: "slide-from-top",
            timer: 12000
        })
    },
    validateServicesContract: function () {
        if (currentLang.includes("es")) {
            messageEmptyFields = "No hay contratos registrados para esta Aerolínea y Aeropuerto";
            typeOfAlert = "Advertencia";
        }
        else {
            messageEmptyFields = "No Contracts registered for this Airline and Airport";
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
}

$(document).ready(function () {
    if (!String.prototype.includes) {
        String.prototype.includes = function () {
            'use strict';
            return String.prototype.indexOf.apply(this, arguments) !== -1;
        };
    }
    if (editionMode) {
        var serviceCode = $('#ServicesDropDown').val();
        AirportServicesController.ShowAditionalOptions(serviceCode);
    }
    updateFirst = false;
});

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}

