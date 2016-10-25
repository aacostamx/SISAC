var mexicanAirport;
var PassengerController = {
    getSum: function () {
        if (mexicanAirport == 'False') {
            return;
        }

        var TotAdultsCabins;
        var TotAdultsCabinA = $('#AdultsCabinA').val();
        var TotAdultsCabinB = $('#AdultsCabinB').val();
        TotAdultsCabins = parseInt(TotAdultsCabinA) + parseInt(TotAdultsCabinB);
        var TotalAdultsCb = document.getElementById('IdTotalAdults');
        TotalAdultsCb.innerHTML = TotAdultsCabins;

        var TotTeensCabins;
        var TotTeensCabinA = $('#TeenageCabinA').val();
        var TotTeensCabinB = $('#TeenageCabinB').val();
        TotTeensCabins = parseInt(TotTeensCabinA) + parseInt(TotTeensCabinB);
        var TotalTeensCb = document.getElementById('IdTotalTeens');
        TotalTeensCb.innerHTML = TotTeensCabins;

        var TotChildCabins;
        var TotChildCabinA = $('#ChildrenCabinA').val();
        var TotChildCabinB = $('#ChildrenCabinB').val();
        TotChildCabins = parseInt(TotChildCabinA) + parseInt(TotChildCabinB);
        var TotalChildCb = document.getElementById('IdTotalChild');
        TotalChildCb.innerHTML = TotChildCabins;

        var TotAdultTeen;
        var TotAdultLcl = $('#LocalAdults').val();
        var TotTeenLcl = $('#LocalTeenage').val();
        TotAdultTeen = parseInt(TotAdultLcl) + parseInt(TotTeenLcl);
        var TotalAT = document.getElementById('IdTotalAT');
        TotalAT.innerHTML = TotAdultTeen;

        var TotalInfants;
        var totInf = $('#LocalChildren').val();
        TotalInfants = parseInt(totInf);
        var TotalChild = document.getElementById('IdTotalInfants');
        TotalChild.innerHTML = TotalInfants;

        var TotalTTOs;
        var TTOsAdult = $('#TransitoryAdults').val();
        var TTOsTeen = $('#TransitoryTeenage').val();
        var TTOsChild = $('#TransitoryChildren').val();
        TotalTTOs = parseInt(TTOsAdult) + parseInt(TTOsTeen) + parseInt(TTOsChild);
        var TotTTTOs = document.getElementById('IdTotalTTOs');
        TotTTTOs.innerHTML = TotalTTOs;

        var TotalCNXs;
        var CNXsAdult = $('#ConnectionAdults').val();
        var CNXsTeen = $('#ConnectionTeenage').val();
        var CNXsChild = $('#ConnectionChildren').val();
        TotalCNXs = parseInt(CNXsAdult) + parseInt(CNXsTeen) + parseInt(CNXsChild);
        var TotTCNXs = document.getElementById('IdTotalCNXs');
        TotTCNXs.innerHTML = TotalCNXs;

        var TotalQuantityBaggage;
        var LoLQuantity = $('#LocalBaggageQuantity').val();
        var TToQuantity = $('#TransitoryBaggageQuantity').val();
        var CNxQuantity = $('#ConnectionBaggageQuantity').val();
        var DiploQuantity = $('#DiplomaticBaggageQuantity').val();
        var ExtaCrewQuantity = $('#ExtraCrewBaggageQuantity').val();
        var OtherQuantity = $('#OtherBaggageQuantity').val();
        TotalQuantityBaggage = parseInt(LoLQuantity) + parseInt(TToQuantity) + parseInt(CNxQuantity) + parseInt(DiploQuantity) + parseInt(ExtaCrewQuantity) + parseInt(OtherQuantity);
        var TotalQuantity = document.getElementById('IdTotalQuantity');
        TotalQuantity.innerHTML = TotalQuantityBaggage;

        var TotalWeightBaggage;
        var LoLWeight = $('#LocalBaggageWeight').val();
        var TToWeight = $('#TransitoryBaggageWeight').val();
        var CNxWeight = $('#ConnectionBaggageWeight').val();
        var DiploWeight = $('#DiplomaticBaggageWeight').val();
        var ExtaCrewWeight = $('#ExtraCrewBaggageWeight').val();
        var OtherWeight = $('#OtherBaggageWeight').val();
        TotalWeightBaggage = parseInt(LoLWeight) + parseInt(TToWeight) + parseInt(CNxWeight) + parseInt(DiploWeight) + parseInt(ExtaCrewWeight) + parseInt(OtherWeight);
        var TotalWeight = document.getElementById('IdTotalWeight');
        TotalWeight.innerHTML = TotalWeightBaggage;

        var Total;
        var isNal = $('#IsNational').val();
        var TotAdultLclTUA = $('#LocalAdults').val();
        var TotTeenLclTUA = $('#LocalTeenage').val();
        var Total = parseInt(TotAdultLclTUA) + parseInt(TotTeenLclTUA);
        var infoTotalTUANal = document.getElementById('IdTUANational');
        var totalTua = document.getElementById('IdTUATotal');
        var TotalInt = $('#InternationalTua').val();
        if (isNal == "True") {
            if (TotalInt != "0") {
                Total = Total - TotalInt;
                infoTotalTUANal.innerHTML = Total;
                totalTua.innerHTML = Total;
            }
            else {
                infoTotalTUANal.innerHTML = Total;
                totalTua.innerHTML = Total;
                $('#InternationalTua').val('0');
            }
        }
        else {
            $('#InternationalTua').val(Total);
            totalTua.innerHTML = Total;
        }


    },
    ejemplo: function () {
    },
    IsClose: function () {
        if (isClose == "True") {
            var msg;
            $.ajax({
                url: "../PassengerInformation/GetInformationMessage",
                type: "POST",
                dataType: "text",
                async: false,
                success: function (result, a, b) {
                    msg = result;
                },
                error: function (r, a, b) {
                    alert();
                }
            });

            document.getElementById("AdultsCabinA").readOnly = true;
            document.getElementById("TeenageCabinA").readOnly = true;
            document.getElementById("ChildrenCabinA").readOnly = true;

            if (mexicanAirport == 'True') {
                document.getElementById("AdultsCabinB").readOnly = true;
                document.getElementById("TeenageCabinB").readOnly = true;
                document.getElementById("ChildrenCabinB").readOnly = true;
                document.getElementById("LocalAdults").readOnly = true;
                document.getElementById("LocalTeenage").readOnly = true;
                document.getElementById("LocalChildren").readOnly = true;
                document.getElementById("TransitoryAdults").readOnly = true;
                document.getElementById("TransitoryTeenage").readOnly = true;
                document.getElementById("TransitoryChildren").readOnly = true;
                document.getElementById("ConnectionAdults").readOnly = true;
                document.getElementById("ConnectionTeenage").readOnly = true;
                document.getElementById("ConnectionChildren").readOnly = true;
                document.getElementById("Diplomatic").readOnly = true;
                document.getElementById("ExtraCrew").readOnly = true;
                document.getElementById("Other").readOnly = true;
                document.getElementById("LocalBaggageQuantity").readOnly = true;
                document.getElementById("LocalBaggageWeight").readOnly = true;
                document.getElementById("TransitoryBaggageQuantity").readOnly = true;
                document.getElementById("TransitoryBaggageWeight").readOnly = true;
                document.getElementById("ConnectionBaggageQuantity").readOnly = true;
                document.getElementById("ConnectionBaggageWeight").readOnly = true;
                document.getElementById("DiplomaticBaggageQuantity").readOnly = true;
                document.getElementById("DiplomaticBaggageWeight").readOnly = true;
                document.getElementById("ExtraCrewBaggageQuantity").readOnly = true;
                document.getElementById("ExtraCrewBaggageWeight").readOnly = true;
                document.getElementById("OtherBaggageQuantity").readOnly = true;
                document.getElementById("OtherBaggageWeight").readOnly = true;
                document.getElementById("InternationalTua").readOnly = true;
                document.getElementById("Observation").readOnly = true;
            }
            swal({
                title: "",
                text: msg,
                type: "warning",
            });
        }
    },
    showSelectFlightModal: function () {
        if (document.getElementById('has-prev-flight').checked) {
            $('#prev-flight').modal('show');
            $('#flight-information').show();
            $('#btn-refresh').show();
        }
        else {
            $('#tb-information-flight').bootstrapTable('removeAll');
            $('#prev-flight').modal('hide');
            $('#flight-information').hide();
            $('#btn-refresh').hide();
        }
    },
    cancelSelectFlight: function () {
        if (document.getElementById('has-prev-flight')) {
            document.getElementById('has-prev-flight').checked = false;
            $('#flight-information').hide();
            $('#btn-refresh').hide();
        }
    },
    selectFlight: function (id) {
        $('#tb-information-flight').bootstrapTable('removeAll');
        var data = $('#tb-search-flight').bootstrapTable('getRowByUniqueId', id);
        $('#tb-information-flight').bootstrapTable('insertRow', {
            index: 0,
            row: {
                Sequence: data.Sequence,
                AirlineCode: data.AirlineCode
                    + '&nbsp;<input type=\"hidden\" name=\"PreviousSequence\" id=\"PreviousSequence\" value=\"' + data.Sequence + '\" />&nbsp;'
                    + '&nbsp;<input type=\"hidden\" name=\"PreviousAirlineCode\" id=\"PreviousAirlineCode\" value=\"' + data.AirlineCode + '\" />&nbsp;',
                FlightNumber: data.FlightNumber
                    + '&nbsp;<input type=\"hidden\" name=\"PreviousItineraryKey\" id=\"PreviousItineraryKey\" value=\"' + data.ItineraryKey + '\" />&nbsp;'
                    + '&nbsp;<input type=\"hidden\" name=\"PreviousFlightNumber\" id=\"PreviousFlightNumber\" value=\"' + data.FlightNumber + '\" />&nbsp;',
                ItineraryKey: data.ItineraryKey,
                EquipmentNumber: data.EquipmentNumber,
                Flight: data.DepartureStation + '&nbsp;<i class=\"fa fa-long-arrow-right fa-adjust\"></i>&nbsp;' + data.ArrivalStation,
                DepartureDate: data.DepartureDate,
                DepartureTime: data.DepartureTime,
                ArrivalDate: data.ArrivalDate,
                ArrivalTime: data.ArriveTime
            }
        });

        $('#prev-flight').modal('hide');

        // Finds ohter link excents flights
        pSequence = data.Sequence;
        pAirlineCode = data.AirlineCode;
        pFlightNumber = data.FlightNumber;
        pItineraryKey = data.ItineraryKey;
        PassengerController.getInternationalTUA();
    },
    getAirportsCombo: function () {
        $('#DepartureStationParam').html("");
        if (!document.getElementById("DepartureStationParam")) {
            return;
        }
        $.ajax({
            url: '../PassengerInformation/GetAirports',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var departure_station = document.getElementById("DepartureStationParam");

                    $.each(data, function (index, item) {
                        var description = item.StationCode + " - " + item.StationName;
                        var option = document.createElement("option");
                        if (index === 0) {
                            var emptyOption = document.createElement("option");
                            emptyOption.text = '';
                            emptyOption.value = '';
                            departure_station.add(emptyOption);
                        }


                        option.text = description;
                        option.value = item.StationCode;
                        departure_station.add(option);
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getInternationalTUA: function () {
        $.ajax({
            url: '../PassengerInformation/GetInternationalTUA?pSequence=' + pSequence + '&pAirlineCode=' + pAirlineCode + '&pFlightNumber=' + pFlightNumber + '&pItineraryKey=' + pItineraryKey,
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (InternationalTUA) {
                var otrosExentos = $('#Other').val();
                var messageFields = "";
                if (InternationalTUA == -1) {
                    //No se ha capturado información de pasajeros del vuelo anterior
                    if (currentLang.includes("es")) {
                        messageFields = "No se ha capturado información de pasajeros del vuelo anterior";
                        typeOfAlert = "Advertencia";
                    }
                    else {
                        messageFields = "It has not been captured Passenger Information of the previous flight";
                        typeOfAlert = "Warning";
                    }

                    swal({
                        title: typeOfAlert,
                        text: messageFields,
                        type: "warning",
                        confirmButtonColor: "#83217a",
                        animation: "slide-from-top",
                        timer: 12000
                    })
                } else
                    if (InternationalTUA == 0) {
                        //El valor de Tua Internacional del vuelo anterior es 0
                        if (currentLang.includes("es")) {
                            messageFields = "El valor de Tua Internacional del vuelo anterior es 0";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageFields = "The value of International TUA is 0 of the previous flight";
                            typeOfAlert = "Warning";
                        }

                        swal({
                            title: typeOfAlert,
                            text: messageFields,
                            type: "warning",
                            confirmButtonColor: "#83217a",
                            animation: "slide-from-top",
                            timer: 12000
                        })
                    }

                if (otrosExentos < InternationalTUA) {
                    $('#Other').val(InternationalTUA);
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    buttonFormatter: function (value, row, index) {
        return '<button class=\"btn btn-default\" type=\"button\" title=\"select\" onclick=\"PassengerController.selectFlight(\'' + index + '\')\"><i class=\"fa fa-fw fa-star\"></i></button>';
    },
    formatDate: function (value, row, index) {
        value = value.substr(0, 10);
        return value;
    },
    formatTime: function (value, row, index) {
        return value.substr(0, 5);
    },
    upperCase: function (Control) {
        var controlName = document.getElementById(Control.id);
        controlName.value = controlName.value.toUpperCase();
        return true;
    },
    deshabiliarEspacio: function (event) {
        if (event.keyCode == 32) {
            event.returnValue = false;
            return false;
        }
    },        
    validarMaxLengthAlfanumerico: function (e, Control) {
        PassengerController.upperCase(Control);
        PassengerController.deshabiliarEspacio(e);
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[a-zA-Z0-9 ]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    },
    getPrevFlight: function () {
        prevSeq = document.getElementById('PreviousSequence').value;
        prevAC = document.getElementById('PreviousAirlineCode').value;
        prevIK = document.getElementById('PreviousItineraryKey').value;
        prevFN = document.getElementById('PreviousFlightNumber').value;

        if (prevSeq && prevFN && prevAC && prevIK) {
            $.ajax({
                url: "../PassengerInformation/GetPreviousFlightInformation",
                type: "GET",
                data: { previousSequence: prevSeq, previousAirlineCode: prevAC, previousItineraryKey: prevIK, previousFlightNumber: prevFN },
                dataType: "JSON",
                async: false,
                success: function (data) {
                    $('#tb-information-flight').bootstrapTable('removeAll');
                    $('#tb-information-flight').bootstrapTable('insertRow', {
                        index: 0,
                        row: {
                            Sequence: data.Sequence,
                            AirlineCode: data.AirlineCode
                                + '&nbsp;<input type=\"hidden\" name=\"PreviousSequence\" id=\"PreviousSequence\" value=\"' + data.Sequence + '\" />&nbsp;'
                                + '&nbsp;<input type=\"hidden\" name=\"PreviousAirlineCode\" id=\"PreviousAirlineCode\" value=\"' + data.AirlineCode + '\" />&nbsp;',
                            FlightNumber: data.FlightNumber
                                + '&nbsp;<input type=\"hidden\" name=\"PreviousItineraryKey\" id=\"PreviousItineraryKey\" value=\"' + data.ItineraryKey + '\" />&nbsp;'
                                + '&nbsp;<input type=\"hidden\" name=\"PreviousFlightNumber\" id=\"PreviousFlightNumber\" value=\"' + data.FlightNumber + '\" />&nbsp;',
                            ItineraryKey: data.ItineraryKey,
                            EquipmentNumber: data.EquipmentNumber,
                            Flight: data.DepartureStation + '&nbsp;<i class=\"fa fa-long-arrow-right fa-adjust\"></i>&nbsp;' + data.ArrivalStation,
                            DepartureDate: moment(data.DepartureDate).format("YYYY-MM-DD"),
                            DepartureTime: moment(data.DepartureTime).format("HH:mm"),
                            ArrivalDate: moment(data.ArrivalDate).format("YYYY-MM-DD"),
                            ArrivalTime: moment(data.ArrivalTime).format("HH:mm")
                        }
                    });
                },
                error: function (r, a, b) {

                }
            });
        }
    }
}

$(document).ready(function () {
    if (!String.prototype.includes) {
        String.prototype.includes = function () {
            'use strict';
            return String.prototype.indexOf.apply(this, arguments) !== -1;
        };
    }

    PassengerController.getSum();
    PassengerController.IsClose();
    if (applyPrev == 'True') {
        PassengerController.getAirportsCombo();
        $('.combobox').combobox();
        $('#DepartureDateParam').datetimepicker({
            format: 'YYYY/MM/DD',
            locale: currentLang,
            showTodayButton: true,
            showClose: true,
            showClear: true,
            useCurrent: false
        });
        PassengerController.getPrevFlight();
    }
});

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}