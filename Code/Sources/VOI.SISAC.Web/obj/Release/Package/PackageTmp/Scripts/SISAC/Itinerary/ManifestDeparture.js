var dt;
var $tableBoarding = $('#table-boarding');
var us = [];

var ManifestDepartureController = {
    checkDataOperation: function () {
        var equipmentNumber = document.getElementById('EquipmentNumber').value;
        var result = 0;
        $.ajax({
            url: '../Airplane/GetAirplaneType',
            data: { equipmentNumber: equipmentNumber },
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var jetFuel = document.getElementById('jet-fuel').value;
                    var realTakeoffWeight = document.getElementById('real-takeoff-weight').value;
                    var operatingWeight = document.getElementById('operating-weight').value;
                    var safetyMarginKg = document.getElementById('structural-takeoff-weight').value;
                    var json = JSON.parse(data);
                    if (jetFuel > json.FuelInKg
                        || realTakeoffWeight > json.MaximumTakeoffWeight
                        || operatingWeight > json.EmptyOperatingWeight) {
                        result = jetFuel > json.FuelInKg ? 1 : (realTakeoffWeight > json.MaximumTakeoffWeight ? 2 : (operatingWeight > json.EmptyOperatingWeight ? 3 : 0));
                    }
                    else {
                        result = 0;
                        safetyMarginKg = safetyMarginKg - realTakeoffWeight;
                        document.getElementById('safety-margin').value = safetyMarginKg;
                    }
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
        return result;
    },
    calculatedDelay: function () {
        var sd_val = document.getElementById('scheduled-date').value;
        var st_val = document.getElementById('scheduled-time').value;
        var scheduled = sd_val + ' ' + st_val;

        var rd_val = document.getElementById('real-date').value;
        var rt_val = document.getElementById('real-time').value;
        var real = rd_val + ' ' + rt_val;

        var m_scheduled = moment(scheduled, dt);
        var m_real = moment(real, dt);

        // Converting to minutes
        var result = m_scheduled.diff(m_real, 'minutes');

        // Verifing the tolerance and setting the difference
        if (result >= parseInt(tolerance) * -1) {
            $('#diff-icon').removeClass('fa-times fa fa-times-delay-icon');
            $('#diff-icon').addClass('fa-check-delay-icon fa-check fa');
            $('#delay-section').hide();
            $('#table-delays').bootstrapTable('removeAll');
        }
        else {
            $('#diff-icon').removeClass('fa-check fa-check-delay-icon fa');
            $('#diff-icon').addClass('fa-times-delay-icon fa-times fa');
            $('#delay-section').show();
        }

        document.getElementById('diff-minutes').textContent = result >= 0 ? result : result * -1;
    },
    getAirportsCombo: function () {
        $('#next-scale').html("");
        $('#arrival-station').html("");

        $.ajax({
            url: '../Airport/Airport/GetAirports',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var next_scale = document.getElementById("next-scale");
                    var arrival_station = document.getElementById("arrival-station");

                    $.each(data, function (index, item) {
                        var description = item.StationCode + " - " + item.StationName;
                        if (index === 0) {
                            next_scale.add(ManifestDepartureController.createOptionTag('', ''));
                            arrival_station.add(ManifestDepartureController.createOptionTag('', ''));
                        }

                        next_scale.add(ManifestDepartureController.createOptionTag(item.StationCode, description, '', '', ''));
                        arrival_station.add(ManifestDepartureController.createOptionTag(item.StationCode, description, '', '', ''));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getDelayCombo: function () {
        $('#select-delay').html("");
        $.ajax({
            url: '../Airport/Delay/GetDelays',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var delays = document.getElementById("select-delay");
                    if (delays) {
                        $.each(data, function (index, item) {
                            if (index === 0) {
                                delays.add(ManifestDepartureController.createOptionTag('', ''));
                            }

                            delays.add(ManifestDepartureController.createOptionTag(item.DelayCode, item.DelayCode + ' - ' + item.DelayName, '', '', ''));
                        });
                    }
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getPilotsCombo: function () {
        $('#crew-member-1').html("");
        $('#crew-member-2').html("");
        $('#crew-member-3').html("");
        $('#crew-member-4').html("");
        $.ajax({
            url: '../Airport/Crew/GetPilots',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var crew_member_1 = document.getElementById("crew-member-1");
                    var crew_member_2 = document.getElementById("crew-member-2");
                    var crew_member_3 = document.getElementById("crew-member-3");
                    var crew_member_4 = document.getElementById("crew-member-4");

                    $.each(data, function (index, item) {
                        var description = item.NickNameSabre + " - " + item.FullName;
                        if (index === 0) {
                            crew_member_1.add(ManifestDepartureController.createOptionTag('', ''));
                            crew_member_2.add(ManifestDepartureController.createOptionTag('', ''));
                            crew_member_3.add(ManifestDepartureController.createOptionTag('', ''));
                            crew_member_4.add(ManifestDepartureController.createOptionTag('', ''));
                        }

                        crew_member_1.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'crew-member-1'));
                        crew_member_2.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'crew-member-2'));
                        crew_member_3.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'crew-member-3'));
                        crew_member_4.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'crew-member-4'));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getStewardessCombo: function () {
        $('#stewardess-1').html("");
        $('#stewardess-2').html("");
        $('#stewardess-3').html("");
        $('#stewardess-4').html("");
        $.ajax({
            url: '../Airport/Crew/GetStewardess',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data) {
                    var stewardess_1 = document.getElementById("stewardess-1");
                    var stewardess_2 = document.getElementById("stewardess-2");
                    var stewardess_3 = document.getElementById("stewardess-3");
                    var stewardess_4 = document.getElementById("stewardess-4");

                    $.each(data, function (index, item) {
                        var description = item.NickNameSabre + " - " + item.FullName;
                        if (index === 0) {
                            stewardess_1.add(ManifestDepartureController.createOptionTag('', ''));
                            stewardess_2.add(ManifestDepartureController.createOptionTag('', ''));
                            stewardess_3.add(ManifestDepartureController.createOptionTag('', ''));
                            stewardess_4.add(ManifestDepartureController.createOptionTag('', ''));
                        }

                        stewardess_1.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'stewardess-1'));
                        stewardess_2.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'stewardess-2'));
                        stewardess_3.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'stewardess-3'));
                        stewardess_4.add(ManifestDepartureController.createOptionTag(item.NickName, description, 'license', item.LicenceNumber, 'stewardess-4'));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    setLicense: function (name) {
        if (name) {
            var value = document.getElementById(name).value;
            if (value) {
                var option = document.getElementById(name + '-' + value);
                if (option) {
                    var number = option.getAttribute('data-license');
                    var input = document.getElementById(name + '-id');
                    input.value = number;
                }
            }
            else {
                var input = document.getElementById(name + '-id');
                input.value = '';
            }
        }
    },
    createOptionTag: function (value, text, attrName, attrValue, id) {
        var option = document.createElement("option");
        option.text = text;
        option.value = value;
        if (attrName && attrValue) {
            var dataName = "data-" + attrName;
            option.setAttribute(dataName, attrValue);
        }

        if (id) {
            option.setAttribute('id', id + '-' + value);
        }

        return option;
    },
    addDelays: function () {
        var selected = document.getElementById('select-delay')
        var value = selected.value;
        var text = selected.options[selected.selectedIndex].text;

        if (text) {
            var $table = $('#table-delays');
            var items = $table.bootstrapTable('getData');
            for (i = 0; i < items.length; i++) {
                if (value == String(items[i].DelayCode).trim()) {
                    if (currentLang.includes("es")) {
                        messageEmptyFields = "La Demora ya existe";
                        typeOfAlert = "Advertencia";
                    }
                    else {
                        messageEmptyFields = "Delay already exist";
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

            $table.bootstrapTable('insertRow',
            {
                index: 0,
                row: {
                    DelayCode: value,
                    DelayName: text,
                }
            });
            $('#select-delay').data('combobox').clearTarget();
            $('#select-delay').data('combobox').clearElement();
        }
        else {
            return;
        }
    },
    deleteButtonFormat: function (value, row, index) {
        var code = '';
        var str = row.DelayCode;
        var inputValue = str.substr(str.indexOf('>') + 1, str.length);
        if (str.indexOf('>') > 0) {
            code = inputValue;
        }
        else if (row) {
            code = row.DelayCode;
        }
        return '<button class=\"btn btn-default\" type=\"button\" title=\"remove\" onclick=\"ManifestDepartureController.removeDelay(\'' + code + '\')\"><i class=\"fa fa-minus\"></i></button>'
    },
    removeDelay: function (DelayCode) {
        if (DelayCode) {
            $('#table-delays').bootstrapTable('removeByUniqueId', DelayCode);
        }
    },
    getUsersCombo: function () {
        $('#user-1').html("");
        $('#user-2').html("");
        var stationCode = $('#DepartureStationCode').val();
        $.ajax({
            url: '../ManifestDeparture/GetAorUsers',
            type: "GET",
            dataType: "JSON",
            data: { stationCode: stationCode },
            async: false,
            success: function (data) {
                if (data) {
                    us[0] = data;
                    var user_1 = document.getElementById("user-1");
                    var user_2 = document.getElementById("user-2");
                    $.each(data, function (index, item) {
                        if (index === 0) {
                            user_1.add(ManifestDepartureController.createOptionTag('', ''));
                            user_2.add(ManifestDepartureController.createOptionTag('', ''));
                        }

                        user_1.add(ManifestDepartureController.createOptionTag(item.Id, item.Description, '', '', ''));
                        user_2.add(ManifestDepartureController.createOptionTag(item.Id, item.Description, '', '', ''));
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    setCombos: function () {
        ManifestDepartureController.getPilotsCombo();
        ManifestDepartureController.getStewardessCombo();
        ManifestDepartureController.getDelayCombo();
        ManifestDepartureController.getUsersCombo();
        ManifestDepartureController.getAirportsCombo();
    },
    setInfoOnCombos: function () {
        document.getElementById('crew-member-1').value = cm1_val;
        document.getElementById('crew-member-2').value = cm2_val;
        document.getElementById('crew-member-3').value = cm3_val;
        document.getElementById('crew-member-4').value = cm4_val;
        ManifestDepartureController.setLicense('crew-member-1');
        ManifestDepartureController.setLicense('crew-member-2');
        ManifestDepartureController.setLicense('crew-member-3');
        ManifestDepartureController.setLicense('crew-member-4');

        document.getElementById('stewardess-1').value = sm1_val;
        document.getElementById('stewardess-2').value = sm2_val;
        document.getElementById('stewardess-3').value = sm3_val;
        document.getElementById('stewardess-4').value = sm4_val
        ManifestDepartureController.setLicense('stewardess-1');
        ManifestDepartureController.setLicense('stewardess-2');
        ManifestDepartureController.setLicense('stewardess-3');
        ManifestDepartureController.setLicense('stewardess-4');

        document.getElementById('user-1').value = u1_val;
        document.getElementById('user-2').value = u2_val;

        document.getElementById('arrival-station').value = aa_val;
        document.getElementById('next-scale').value = sa_val;

        $('#crew-member-1').data('combobox').refresh();
        $('#crew-member-2').data('combobox').refresh();
        $('#crew-member-3').data('combobox').refresh();
        $('#crew-member-4').data('combobox').refresh();

        $('#stewardess-1').data('combobox').refresh();
        $('#stewardess-2').data('combobox').refresh();
        $('#stewardess-3').data('combobox').refresh();
        $('#stewardess-4').data('combobox').refresh();

        $('#user-1').data('combobox').refresh();
        $('#user-2').data('combobox').refresh();

        $('#arrival-station').data('combobox').refresh();
        $('#next-scale').data('combobox').refresh();
    },
    addAttrToDelayTable: function () {
        var $tb_delays = $('#table-delays');
        var items = $tb_delays.bootstrapTable('getData');

        if (items.length > 0) {
            $.each(items, function (index, item) {
                var procesado = item.DelayCode.indexOf('input name=') > 0 ? true : false;
                if (!procesado) {
                    var inputCode = '<input name=\'Delays[' + index + '].DelayCode\' type=\'hidden\' value=\'' + item.DelayCode.trim() + '\'>';
                    var inputName = '<input name=\'Delays[' + index + '].DelayName\' type=\'hidden\' value=\'' + item.DelayName.trim() + '\'>';
                    $tb_delays.bootstrapTable('updateRow', {
                        index: index,
                        row: {
                            DelayCode: inputCode + item.DelayCode,
                            DelayName: inputName + item.DelayName
                        }
                    });
                }
            });
        }
    },
    //Boarding Inicio   
    addAttrToBoardingTable: function () {
        var items = $tableBoarding.bootstrapTable('getData');
        $('#divHidden').html('');

        if (items.length > 0) {
            $.each(items, function (index, item) {

                if ($('#Station' + item.Position).html() && $('#Station' + item.Position).html() != 'Empty') {
                    var inputHidden = '<input name=\'ManifestDepartureBoardings[' + index + '].Sequence\' type=\'hidden\' value=\'' + item.Sequence + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].AirlineCode\' type=\'hidden\' value=\'' + item.AirlineCode.trim() + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].FlightNumber\' type=\'hidden\' value=\'' + item.FlightNumber.trim() + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].ItineraryKey\' type=\'hidden\' value=\'' + item.ItineraryKey.trim() + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].Position\' type=\'hidden\' value=\'' + item.Position + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].Station\' type=\'hidden\' value=\'' + $('#Station' + item.Position).html() + '\'>';

                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].PassengerAdult\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(document.getElementById('PassengerAdult' + item.Position).innerHTML, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].PassengerMinor\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(document.getElementById('PassengerMinor' + item.Position).innerHTML, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].PassengerInfant\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(document.getElementById('PassengerInfant' + item.Position).innerHTML, 0) + '\'>';

                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].LuggageQuantity\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(item.LuggageQuantity, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].LuggageKgs\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseFloat(item.LuggageKgs, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].ChargeQuantity\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(item.ChargeQuantity, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].ChargeKgs\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseFloat(item.ChargeKgs, 0) + '\'>';

                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].MailQuantity\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseInt(document.getElementById('MailQuantity' + item.Position).innerHTML, 0) + '\'>';
                    inputHidden = inputHidden + '<input name=\'ManifestDepartureBoardings[' + index + '].MailKgs\' type=\'hidden\' value=\'' + ManifestDepartureController.tryParseFloat(document.getElementById('MailKgs' + item.Position).innerHTML, 0) + '\'>';

                    $('#divHidden').append(inputHidden);
                }
            });
        }
    },
    tryParseInt: function (str, defaultValue) {
        var retValue = defaultValue;
        if (str !== null) {
            if (str.length > 0) {
                if (!isNaN(str)) {
                    retValue = parseInt(str);
                }
            }
        }
        return retValue;
    },
    tryParseFloat: function (str, defaultValue) {
        var retValue = defaultValue;
        if (str !== null) {
            if (str.length > 0) {
                if (!isNaN(str)) {
                    retValue = parseFloat(str);
                }
            }
        }
        return retValue;
    },
    validatesPrevious: function (value) {
        if (value > 1 && $('#Station' + (value - 1)).html() == 'Empty') {
            if (currentLang.includes("es")) {
                messageEmptyFields = "No puedes editar este registro hasta haber editado el anterior aeropuerto";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "You cannot edit this record until you have made the previous airport";
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
    },
    //Boarding information Inicio
    addBoardingInfo: function (BoardingID, Position) {
        if ($('#charge-info-modal')) { // If the modal exists

            // hides the boarding info sections
            var pos = $('div[id^="detail-section-"]').length;
            for (var i = 1; i <= pos; i++) {
                $('#detail-section-' + i).hide();
            }

            // If the section does not have information then sets the structure and gets the information
            if ($('#detail-section-' + Position).html() == '') {
                var html = '<div id="comp-section-' + Position + '" class="row"></div><br />' +
                           '<div id="totals-section-' + Position + '" class="show-group-border row"></div><br />' +
                           '<div id="info-section-' + Position + '" class="show-group-border row"></div><br />' +
                           '<div id="aor-section-' + Position + '" class="show-group-border row"></div>';
                $('#detail-section-' + Position).append(html);

                // draws the total sections
                ManifestDepartureController.setTotalSectionInit(Position);

                // gets and sets the table section
                ManifestDepartureController.setAddBoardingDetailInit(BoardingID, Position);

                // gets and sets the innformation section (checkbox)
                ManifestDepartureController.setAddBoardingInfoInit(BoardingID, Position);

                // gets and sets the aor section
                ManifestDepartureController.setAorSection(Position);
            }

            // shows the selected DIV
            $('#tb-selected').val(Position);
            $('#detail-section-' + Position).show();
        }
    },
    setAorSection: function (position) {
        var html = '<div id="aor-section-' + position + '" class="show-group-border row">' +
                        '<div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">' +
                            '<label class="subtitle_h5 control-label required-field">Responsable en Rampa</label>' +
                            '<input type="text" class="inputForm form-control" id="comp-ramp-' + position + '"' +
                                ' name="ManifestDepartureBoardings[' + (position - 1) + '].ManifestDepartureBoardingDetails[0].RampResponsible"' +
                                ' value="' + us[1].RampResponsible + '"/>' +
                            '<span id="comp-ramp-' + position + '-err" class="text-danger text-danger-error"></span>' +
                        '</div>' +
                        '<div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">' +
                            '<label class="subtitle_h5 control-label required-field">AOR</label>' +
                            '<select class="inputForm form-control combobox remove-ms-clear"' +
                                ' id="aor-select-' + position + '"' +
                                ' name="ManifestDepartureBoardings[' + (position - 1) + '].ManifestDepartureBoardingDetails[0].AorUserID">' +
                            '</select>' +
                            '<span id="aor-select-' + position + '-err" class="text-danger text-danger-error"></span>' +
                        '</div>' +
                        '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                            '<label class="subtitle_h5 control-label">Observaciones</label>' +
                            '<textarea class="inputForm form-control" id="comp-remark-' + position + '" cols="120"' +
                                ' name="ManifestDepartureBoardings[' + (position - 1) + '].ManifestDepartureBoardingDetails[0].Remarks">' +
                                us[1].Remarks +
                            '</textarea>' +
                        '</div>' +
                    '</div>';
        $('#aor-section-' + position).append(html);

        if (us[0]) {
            $.each(us[0], function (index, item) {
                if (index === 0) {
                    $('#aor-select-' + position).append(ManifestDepartureController.createOptionTag('', ''));
                }

                $('#aor-select-' + position).append(ManifestDepartureController.createOptionTag(item.Id, item.Description, '', '', ''));
            });
        }
        $('#aor-select-' + position).combobox();
        $('#aor-select-' + position).val(us[1].AorUserID);
        $('#aor-select-' + position).data('combobox').refresh();
    },
    setTotalSectionInit: function (position) {
        var html = '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">' +
                        '<span>' +
                            '<strong>' +
                                'Total Carga:&nbsp;&nbsp;' +
                                '<span id="comp-' + position + '-total-ch-pc"></span>' +
                                '&nbsp;pzas' +
                                '&nbsp;/&nbsp;' +
                                '<span id="comp-' + position + '-total-ch-wg"></span>' +
                                '&nbsp;Kg' +
                            '</strong>' +
                        '</span>' +
                    '</div>' +
                    '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">' +
                        '<span>' +
                            '<strong>' +
                                'Total Equipaje:&nbsp;&nbsp' +
                                '<span id="comp-' + position + '-total-eq-pc"></span>' +
                                '&nbsp;pzas' +
                                '&nbsp;/&nbsp;' +
                                '<span id="comp-' + position + '-total-eq-wg"></span>' +
                                '&nbsp;Kg' +
                            '</strong>' +
                        '</span>' +
                    '</div>' +
                    '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">' +
                        '<span>' +
                            '<strong>' +
                                'Total:&nbsp;&nbsp;' +
                                '<span id="comp-' + position + '-total"></span>' +
                            '</strong>' +
                        '</span>' +
                    '</div>';

        $('#totals-section-' + position).append(html);
    },
    setAddBoardingInfoInit: function (BoardingID, Position) {
        var $divDetail = $('#detail-section-' + Position.toString()); // Finds the the DIV's that will contain the information for the selected position
        var $divInfo = $('#info-section-' + Position.toString());
        if ($divDetail && !$divInfo.html()) {
            $.ajax({
                url: '../ManifestDeparture/GetBoardingInformationForManifest',
                type: "POST",
                dataType: "JSON",
                data: {
                    boardingID: BoardingID,
                    Sequence: $('#Sequence').val(),
                    AirlineCode: $('#AirlineCode').val(),
                    FlightNumber: $('#FlightNumber').val(),
                    ItineraryKey: $('#ItineraryKey').val()
                },
                async: false,
                success: function (data) {
                    $('#info-section-' + Position.toString()).empty();
                    if (data) {
                        $.each(data, function (index, item) {
                            var checked = item.Checked ? 'checked="' + item.Checked + '"' : '';
                            var id = item.CompartmentTypeInformationID.toString() + item.CompartmentTypeID.toString() + Position.toString();
                            var html = '<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">' +
                                            '<input name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].Checked"' +
                                                'class="checkVolaris top-padding" id="' + id + '" type="checkbox" ' + checked + ' value="true" />' +
                                            '<label class="control_gris" for="' + id + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].Checked" ' +
                                                'value="' + id + '"><span></span></label>' +
                                            '<input name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].CompartmentTypeInformationID" ' +
                                                'type="hidden" value="' + item.CompartmentTypeInformationID + '" />' +
                                            '<input name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].CompartmentTypeID" ' +
                                                'type="hidden" value="' + item.CompartmentTypeID + '">' +
                                            '<input name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].BoardingID" ' +
                                                'type="hidden" value="' + item.BoardingID + '">' +
                                            '<input name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingInformations[' + index + '].BoardingInformationID" ' +
                                                'type="hidden" value="' + item.BoardingInformationID + '">' +
                                            '<label class="subtitle_h6_blue">' + item.CompartmentTypeInformationName.toString() + ' (' + item.CompartmentTypeName.toString() + ')</label>' +
                                        '</div>';

                            $('#info-section-' + Position.toString()).append(html);
                        });
                    }
                },
                error: function (result) {
                    console.log('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
    },
    setAddBoardingDetailInit: function (BoardingID, Position) {
        if ($('#charge-info-modal')) { // If the modal exists
            var $divText = $('#detail-section-' + Position.toString()); // Finds the the DIV that will contain the information for the selected position
            if ($divText && $divText.html() != '') { // TODO: Modificar esta validación
                $.ajax({
                    url: '../ManifestDeparture/GetBoardingDetailForManifest',
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        boardingID: BoardingID,
                        Sequence: $('#Sequence').val(),
                        AirlineCode: $('#AirlineCode').val(),
                        FlightNumber: $('#FlightNumber').val(),
                        ItineraryKey: $('#ItineraryKey').val()
                    },
                    async: false,
                    success: function (data) {
                        $('#comp-section-' + Position.toString()).empty();
                        if (data) {
                            var columns = [[], []];
                            var info = [{}, {}, {}, {}];
                            var upHeader = 1, lowHeader = 0, ind = 100;
                            var table = '<table id="tb-comp-' + Position.toString() + '"></table>'; // creates the table
                            var numComp = data.length;

                            $('#comp-section-' + Position.toString()).append(table);
                            columns[0][0] = { field: 'tb-comp-' + Position + '-lb', rowspan: 2, align: 'left', valign: 'middle', halign: "center" };

                            info[0]['tb-comp-' + Position + '-lb'] = "Piezas";
                            info[1]['tb-comp-' + Position + '-lb'] = "Peso";
                            info[2]['tb-comp-' + Position + '-lb'] = "Total Peso";
                            info[3]['tb-comp-' + Position + '-lb'] = "Total Máximo";

                            $.each(data, function (index, item) {
                                // Sets the upper header
                                columns[0][upHeader++] = { title: item.CompartmentTypeName, colspan: 2, align: 'center', valign: 'middle' };

                                // Sets the lower headers
                                columns[1][lowHeader++] = {
                                    field: 'tb-comp-' + Position + '-ch-' + (index + 1),
                                    title: 'Carga',
                                    align: 'center',
                                    valign: 'middle'
                                };
                                columns[1][lowHeader++] = {
                                    field: 'tb-comp-' + Position + '-eq-' + (index + 1),
                                    title: 'Equipo',
                                    align: 'center',
                                    valign: 'middle'
                                };

                                // Hidden info 
                                var hidden = '<input value="' + item.BoardingDetailID + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].BoardingDetailID" type="hidden" />' +
                                             '<input value="' + item.BoardingID + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].BoardingID" type="hidden" />' +
                                             '<input value="' + item.CompartmentTypeID + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].CompartmentTypeID" type="hidden" />'

                                // Sets the information
                                // First row
                                var element = 'ch-pc';
                                info[0]['tb-comp-' + Position + '-ch-' + (index + 1)] = '<input value="' + item.ChargeQuantity + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].ChargeQuantity"' +
                                                                                            ' type="number" class="form-control total-value remove-ms-clear" onkeypress="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);"' +
                                                                                            ' onchange="ManifestDepartureController.sumTotals(' + Position + ',' + numComp + ',\'' + element + '\')"' +
                                                                                            ' onkeyup="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);" tabindex="' + ind++ + '" id="tb-comp-' + Position + '-ch-pc-' + index + '" />';

                                element = 'eq-pc';
                                info[0]['tb-comp-' + Position + '-eq-' + (index + 1)] = '<input value="' + item.LuggageQuantity + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].LuggageQuantity"' +
                                                                                            ' type="number" class="form-control total-value remove-ms-clear" onkeypress="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);"' +
                                                                                            ' onchange="ManifestDepartureController.sumTotals(' + Position + ',' + numComp + ',\'' + element + '\')"' +
                                                                                            ' onkeyup="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);" tabindex="' + ind++ + '" id="tb-comp-' + Position + '-eq-pc-' + index + '" />' +
                                                                                            hidden;

                                // Second row
                                element = 'ch-wg';
                                info[1]['tb-comp-' + Position + '-ch-' + (index + 1)] = '<input value="' + item.ChargeKgs + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].ChargeKgs"' +
                                                                                            ' type="number" class="form-control total-value remove-ms-clear" onkeypress="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);"' +
                                                                                            ' onchange="ManifestDepartureController.sumWeight(' + Position + ',' + index + '); ManifestDepartureController.sumTotals(' + Position + ',' + numComp + ',\'' + element + '\')";' +
                                                                                            ' onkeyup="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);" tabindex="' + ind++ + '" id="tb-comp-' + Position + '-ch-wg-' + index + '" />';

                                element = 'eq-wg';
                                info[1]['tb-comp-' + Position + '-eq-' + (index + 1)] = '<input value="' + item.LuggageKgs + '" name="ManifestDepartureBoardings[' + (Position - 1) + '].ManifestDepartureBoardingDetails[' + index + '].LuggageKgs"' +
                                                                                            ' type="number" class="form-control total-value remove-ms-clear" onkeypress="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);"' +
                                                                                            ' onchange="ManifestDepartureController.sumWeight(' + Position + ',' + index + '); ManifestDepartureController.sumTotals(' + Position + ',' + numComp + ',\'' + element + '\')";' +
                                                                                            ' onkeyup="return commonFunctions.validarNumeroDecimal(event, this, 18, 5);" tabindex="' + ind++ + '" id="tb-comp-' + Position + '-eq-wg-' + index + '" />';

                                info[2]['tb-comp-' + Position + '-ch-' + (index + 1)] = '<span id="tb-comp-' + Position + '-total-wg-' + index + '">' + (item.ChargeKgs + item.LuggageKgs) + ' Kg' + '</span>';
                                info[3]['tb-comp-' + Position + '-ch-' + (index + 1)] = '<span id="tb-comp-' + Position + '-total-wg-' + index + '">' + item.MaximumWeight + '</span>';

                                if (index == 0) {
                                    us[1] = {
                                        RampResponsible: item.RampResponsible != null ? item.RampResponsible : '',
                                        AorUserID: item.AorUserID,
                                        Remarks: item.Remarks != null ? item.Remarks : ''
                                    };
                                }
                            });

                            $('#tb-comp-' + Position).bootstrapTable({
                                columns: columns,
                                data: info
                            });

                            for (var i = 1; i <= numComp; i++) {
                                $('#tb-comp-' + Position).bootstrapTable('mergeCells', {
                                    index: 2,
                                    field: 'tb-comp-' + Position + '-ch-' + i,
                                    colspan: 2,
                                    rowspan: 1
                                })
                                $('#tb-comp-' + Position).bootstrapTable('mergeCells', {
                                    index: 3,
                                    field: 'tb-comp-' + Position + '-ch-' + i,
                                    colspan: 2,
                                    rowspan: 1
                                })
                            }

                            ManifestDepartureController.sumTotals(Position, numComp, 'ch-pc');
                            ManifestDepartureController.sumTotals(Position, numComp, 'eq-pc');
                            ManifestDepartureController.sumTotals(Position, numComp, 'ch-wg');
                            ManifestDepartureController.sumTotals(Position, numComp, 'eq-wg');
                        }
                    },
                    error: function (result) {
                        console.log('ERROR ' + result.status + ' ' + result.statusText);
                    }
                });
            }
        }
    },
    sumWeight: function (pos, idx) {
        var $chargeWg = $('#tb-comp-' + pos + '-ch-wg-' + idx);
        var $equipmentWg = $('#tb-comp-' + pos + '-eq-wg-' + idx);

        if ($chargeWg && $equipmentWg) {
            var eq, ch;
            if (!isNaN($equipmentWg.val()) && $equipmentWg.val()) {
                eq = $equipmentWg.val();
            }
            else {
                $equipmentWg.val(0)
                eq = $equipmentWg.val();
            }

            if (!isNaN($chargeWg.val()) && $chargeWg.val()) {
                ch = $chargeWg.val();
            }
            else {
                $chargeWg.val(0);
                ch = $chargeWg.val();
            }

            var total = parseFloat(ch) + parseFloat(eq);
            $('#tb-comp-' + pos + '-total-wg-' + idx).html(total + ' <strong>&nbsp;Kg</strong>');
        }
    },
    sumTotals: function (pos, totalCompartments, element) {
        var total = 0, inputId, totalId;
        switch (element) {
            case 'ch-pc':
                inputId = '#tb-comp-' + pos + '-ch-pc-';
                totalId = '#comp-' + pos + '-total-ch-pc';
                break;
            case 'eq-pc':
                inputId = '#tb-comp-' + pos + '-eq-pc-';
                totalId = '#comp-' + pos + '-total-eq-pc';
                break;
            case 'ch-wg':
                inputId = '#tb-comp-' + pos + '-ch-wg-';
                totalId = '#comp-' + pos + '-total-ch-wg';
                break;
            case 'eq-wg':
                inputId = '#tb-comp-' + pos + '-eq-wg-';
                totalId = '#comp-' + pos + '-total-eq-wg';
                break;
        }

        if (inputId && totalId) {
            for (var i = 0; i < totalCompartments; i++) {
                var value = $(inputId + i.toString()).val();
                total += !isNaN(value) && value ? parseFloat(value) : 0;
            }

            if (element == 'ch-pc' || element == 'eq-pc') {
                total = total;
            }

            if (element == 'ch-wg' || element == 'eq-wg') {
                total = total.toFixed(4);
            }

            $(totalId).html(total);
            $('#comp-' + pos + '-total').html(
                parseFloat($('#comp-' + pos + '-total-ch-wg').html()) + parseFloat($('#comp-' + pos + '-total-eq-wg').html()));
        }
    },
    validateAddBoardingInfo: function () {
        var pos = $('#tb-selected').val();
        var ramp = $('#comp-ramp-' + pos).val(), aor = $('#aor-select-' + pos).val();
        if (!ramp || !aor) {
            var message;
            if (currentLang.includes('es')) {
                message = "Campo Requerido";
            }
            else {
                message = "Required Field";
            }
            $('#comp-ramp-' + pos + '-err').html(ramp ? '' : message);
            $('#aor-select-' + pos + '-err').html(aor ? '' : message);
            return;
        }

        $('#table-boarding').bootstrapTable(
            'updateRow',
            {
                index: (pos - 1),
                row: {
                    LuggageQuantity: $('#comp-' + pos + '-total-eq-pc').html(),
                    LuggageKgs: $('#comp-' + pos + '-total-eq-wg').html(),
                    ChargeQuantity: $('#comp-' + pos + '-total-ch-pc').html(),
                    ChargeKgs: $('#comp-' + pos + '-total-ch-wg').html()
                }
            });

        $('#charge-info-modal').modal('hide');
    },
    //Boarding information End
    linkFormatterStation: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='Station" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    },
    linkFormatterPassengerAdult: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='PassengerAdult" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    },
    linkFormatterPassengerMinor: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='PassengerMinor" + row.Position + "'>" + item + "</a>";
    },
    linkFormatterPassengerInfant: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='PassengerInfant" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    },
    //linkFormatterLuggageQuantity: function (value, row, index) {
    //    var item = value == null ? '' : value;
    //    return "<a href='#' id='LuggageQuantity" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    //},
    //linkFormatterLuggageKgs: function (value, row, index) {
    //    var item = value == null ? '' : value;
    //    return "<a href='#' id='LuggageKgs" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    //},
    //linkFormatterChargeQuantity: function (value, row, index) {
    //    var item = value == null ? '' : value;
    //    return "<a href='#' id='ChargeQuantity" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    //},
    //linkFormatterChargeKgs: function (value, row, index) {
    //    var item = value == null ? '' : value;
    //    return "<a href='#' id='ChargeKgs" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    //},
    linkFormatterMailQuantity: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='MailQuantity" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    },
    linkFormatterMailKgs: function (value, row, index) {
        var item = value == null ? '' : value;
        return "<a href='#' id='MailKgs" + row.Position + "' onclick='return ManifestDepartureController.validatesPrevious(" + row.Position + ");'>" + item + "</a>";
    },
    linkFormatterAction: function (value, row, index) {
        var html =
            '<button class=\"btn btn-default\" type=\"button\" title=\"Add Information\" data-toggle=\"modal\" data-target=\"#charge-info-modal\" onclick=\"ManifestDepartureController.addBoardingInfo(' + row.BoardingID + ',' + row.Position + ')\">'
                + '<i class=\"fa fa-plus fa-fw\"></i>'
            + '</button>';
        if (row.BoardingID > 0) {
            html = html +
                '<button class=\"btn btn-default\" type=\"button\" title=\"Report\" onclick=\"ReportInformation(' + row.BoardingID + ')\">'
                    + '<i class=\"fa fa-print fa-fw\"></i>'
                + '</button>';
        } else {
            html = html +
                '<button class=\"btn btn-default\" type=\"button\" title=\"Report\" disabled onclick=\"ReportInformation(' + row.BoardingID + ')\">'
                    + '<i class=\"fa fa-print fa-fw\"></i>'
                + '</button>';
        }
        return html;
    },
    getStationCombo: function () {
        return $.ajax({
            url: '../Airport/Airport/GetAirports',
            type: "GET",
            dataType: "JSON",
            async: true
        });
    },
    stationEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {
                ManifestDepartureController.getStationCombo().done(function (result) {
                    //formato Value Text para combo
                    var resultFormat = [];
                    var itemFormat = {};
                    itemFormat["value"] = "";
                    itemFormat["text"] = "";
                    resultFormat.push(itemFormat);

                    $.each(result, function (i, value) {

                        itemFormat = {};
                        itemFormat["value"] = value.StationCode;
                        itemFormat["text"] = value.StationCode;

                        resultFormat.push(itemFormat);
                    });

                    //Asignar valores a combo
                    $('#Station' + item.Position).editable({
                        type: 'select',
                        value: item.Station,
                        title: 'Select Station',
                        source: resultFormat,
                    });

                }).fail(function () { });

                //Para Mostrar y ocultar editable
                $('#Station' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });
                $('#Station' + item.Position).on('save', function (e, params) {
                    if (params.newValue == '') {
                        for (var i = item.Position + 1; i <= $('a[id^="Station"]').length ; i++) {
                            $('#Station' + i).html('Empty');
                            $('#PassengerAdult' + i).html('Empty');
                            $('#PassengerMinor' + i).html('Empty');
                            $('#PassengerInfant' + i).html('Empty');
                            //$('#LuggageQuantity' + i).html('Empty');
                            //$('#LuggageKgs' + i).html('Empty');
                            //$('#ChargeQuantity' + i).html('Empty');
                            //$('#ChargeKgs' + i).html('Empty');
                            $('#MailQuantity' + i).html('Empty');
                            $('#MailKgs' + i).html('Empty');
                        }
                    }
                });
            });
        }
    },
    adultEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#PassengerAdult' + item.Position).editable({
                    type: 'number',
                    value: item.PassengerAdult,
                    title: 'Adults',
                });

                //Para Mostrar y ocultar editable
                $('#PassengerAdult' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    minorEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#PassengerMinor' + item.Position).editable({
                    type: 'number',
                    value: item.PassengerMinor,
                    title: 'Minors',
                });

                //Para Mostrar y ocultar editable
                $('#PassengerMinor' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    infantEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#PassengerInfant' + item.Position).editable({
                    type: 'number',
                    value: item.PassengerInfant,
                    title: 'Infants',
                });

                //Para Mostrar y ocultar editable
                $('#PassengerInfant' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    luggageQuantityEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#LuggageQuantity' + item.Position).editable({
                    type: 'number',
                    value: item.MailQuantity,
                    title: 'Mail Quantity',
                });

                //Para Mostrar y ocultar editable
                $('#LuggageQuantity' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    luggageKgsEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#LuggageKgs' + item.Position).editable({
                    type: 'number',
                    step: 'any',
                    value: item.MailKgs,
                    title: 'Mail Kgs',
                });

                //Para Mostrar y ocultar editable
                $('#LuggageKgs' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    chargeQuantityEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#ChargeQuantity' + item.Position).editable({
                    type: 'number',
                    value: item.MailQuantity,
                    title: 'Mail Quantity',
                });

                //Para Mostrar y ocultar editable
                $('#ChargeQuantity' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    chargeKgsEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#ChargeKgs' + item.Position).editable({
                    type: 'number',
                    step: 'any',
                    value: item.MailKgs,
                    title: 'Mail Kgs',
                });

                //Para Mostrar y ocultar editable
                $('#ChargeKgs' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });
            });
        }
    },
    mailQuantityEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#MailQuantity' + item.Position).editable({
                    type: 'number',
                    value: item.MailQuantity,
                    title: 'Mail Quantity',
                });

                //Para Mostrar y ocultar editable
                $('#MailQuantity' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    mailKgsEditable: function (data) {
        if (data.length > 0) {
            $.each(data, function (index, item) {

                //Asignar valores a combo
                $('#MailKgs' + item.Position).editable({
                    type: 'number',
                    step: 'any',
                    value: item.MailKgs,
                    title: 'Mail Kgs',
                });

                //Para Mostrar y ocultar editable
                $('#MailKgs' + item.Position).on('shown', function () {
                    var $innerForm = $(this).data('editable').input.$input.closest('form');
                    var $outerForm = $innerForm.parents('form').eq(0);
                    $innerForm.data('validator', $outerForm.data('validator'));
                });

            });
        }
    },
    initChargeModal: function (numSection) {
        if ($('#charge-info-modal')) {
            $('#charge-info-body').empty();
            $('#charge-info-body').append('<input id="tb-selected" type="hidden" />');
            var position = 1;
            while (position <= numSection) {
                $('#charge-info-body').append('<div id="detail-section-' + position + '">');
                position++;
            }
        }
    },
    boardingTableEditable: function () {
        $.fn.editable.defaults.mode = 'popup';

        //Ya que cargo los datos de la tabla 
        $tableBoarding.on('load-success.bs.table', function () {
            var items = $tableBoarding.bootstrapTable('getData');
            //Station 
            ManifestDepartureController.stationEditable(items);
            //Adult
            ManifestDepartureController.adultEditable(items);
            //Minor
            ManifestDepartureController.minorEditable(items);
            //Infant
            ManifestDepartureController.infantEditable(items);
            //MailQuantity
            ManifestDepartureController.mailQuantityEditable(items);
            //MailKgs
            ManifestDepartureController.mailKgsEditable(items);
            // sets the strucure for the charge information
            ManifestDepartureController.initChargeModal(items.length);
        });

        $tableBoarding.on('reset-view.bs.table', function () {
            var items = $tableBoarding.bootstrapTable('getData');
            //Station 
            ManifestDepartureController.stationEditable(items);
            //Adult
            ManifestDepartureController.adultEditable(items);
            //Minor
            ManifestDepartureController.minorEditable(items);
            //Infant
            ManifestDepartureController.infantEditable(items);
            //MailQuantity
            ManifestDepartureController.mailQuantityEditable(items);
            //MailKgs
            ManifestDepartureController.mailKgsEditable(items);
        });
    },
    //Boarding Fin
    validateAddInfo: function () {
        if (!$('#ai-extra').val() || !$('#ai-stewar').val() || !$('#ai-pilots').val()) {
            var message;
            if (currentLang.includes('es')) {
                message = "Campo Requerido";
            }
            else {
                message = "Required Field";
            }
            $('#ai-extra-err').html($('#ai-extra').val() ? '' : message);
            $('#ai-stewar-err').html($('#ai-stewar').val() ? '' : message);
            $('#ai-pilots-err').html($('#ai-pilots').val() ? '' : message);
            return;
        }

        $('#Pilot').val($('#ai-pilots').val());
        $('#Surcharge').val($('#ai-stewar').val());
        $('#ExtraCrew').val($('#ai-extra').val());
        $('#TypeFlight').val($('#type').val());
        $('#SlotAllocatedTime').val($('#hr-assig').val());
        $('#SlotCoordinatedTime').val($('#hr-coord').val());
        $('#OvernightEndTime').val($('#hr-end').val());
        $('#ManeuverStartTime').val($('#hr-beign').val());
        $('#PositionOutputTime').val($('#hr-exit').val());
        $('#FirstDelayDescription').val($('#remark-dly-1').val());
        $('#SecondDelayDescription').val($('#remark-dly-2').val());
        $('#ThirdDelayDescription').val($('#remark-dly-3').val());

        $('#ai-extra-err').html('');
        $('#ai-stewar-err').html('');
        $('#ai-pilots-err').html('');
        $('#add-info-modal').modal('hide');
    },
    postForm: function (action) {
        document.getElementById('Action').value = action ? action : 1;
    },
    setAddInfoInit: function () {
        if ($('#add-info-modal')) {
            $('#ai-pilots').val($('#Pilot').val());
            $('#ai-stewar').val($('#Surcharge').val());
            $('#ai-extra').val($('#ExtraCrew').val());
            $('#type').val($('#TypeFlight').val());
            $('#hr-assig').val($('#SlotAllocatedTime').val());
            $('#hr-coord').val($('#SlotCoordinatedTime').val());
            $('#hr-end').val($('#OvernightEndTime').val());
            $('#hr-beign').val($('#ManeuverStartTime').val());
            $('#hr-exit').val($('#PositionOutputTime').val());
            $('#remark-dly-1').html($('#FirstDelayDescription').val());
            $('#remark-dly-2').html($('#SecondDelayDescription').val());
            $('#remark-dly-3').html($('#ThirdDelayDescription').val());
        }
    },
    initialize: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }

        ManifestDepartureController.boardingTableEditable();
        dt = 'YYYY/MM/DD HH:mm';
        ManifestDepartureController.setCombos();
        $('.combobox').combobox();
        $('#real-date').datetimepicker({
            format: 'YYYY/MM/DD',
            locale: currentLang,
            showTodayButton: true,
            showClose: true,
            showClear: true,
            useCurrent: false
        });
        $('#real-time').datetimepicker({
            format: 'HH:mm'
        });

        $('#hr-assig').datetimepicker({
            format: 'HH:mm'
        });
        $('#hr-coord').datetimepicker({
            format: 'HH:mm'
        });
        $('#hr-end').datetimepicker({
            format: 'HH:mm'
        });
        $('#hr-beign').datetimepicker({
            format: 'HH:mm'
        });
        $('#hr-exit').datetimepicker({
            format: 'HH:mm'
        });

        ManifestDepartureController.setInfoOnCombos();

        $("#real-time").on("dp.change", function (e) {
            ManifestDepartureController.calculatedDelay();
        });
        $('#real-date').on("dp.change", function (e) {
            ManifestDepartureController.calculatedDelay();
        });

        ManifestDepartureController.calculatedDelay();
        ManifestDepartureController.setAddInfoInit();
    }
}

$(document).ready(ManifestDepartureController.initialize());

$(document).on('submit', '#form-manifest', function (e) {
    //prevent the form from doing a submit
    //e.preventDefault();
    //return false;
    //document.getElementById('Action').value = action ? action : 1;

    var doPost = ManifestDepartureController.checkDataOperation();
    form = document.getElementById('form-manifest');
    if (form && doPost == 0) {
        ManifestDepartureController.addAttrToDelayTable();
        ManifestDepartureController.addAttrToBoardingTable();
        return;
        //form.submit();
    }
    else {
        // Mensaje

        if (currentLang.includes("es")) {
            if (doPost == 1) {
                messageEmptyFields = "Combustible (Kg) debe ser menor que al configurado para el Modelo de Avión";
            }
            if (doPost == 2) {
                messageEmptyFields = "Peso Real Despegue (Kg) debe ser menor que al configurado para el Modelo de Avión";
            }
            if (doPost == 3) {
                messageEmptyFields = "Peso de Operación (Kg) debe ser menor que al configurado para el Modelo de Avión";
            }

            typeOfAlert = "Advertencia";
        }
        else {
            if (doPost == 1) {
                messageEmptyFields = "Jet Fuel (Kg) must be less than the set for Model Airplane";
            }
            if (doPost == 2) {
                messageEmptyFields = "Real Takeoff Weight (Kg) must be less than the set for Model Airplane";
            }
            if (doPost == 3) {
                messageEmptyFields = "Operating Weight (Kg) must be less than the set for Model Airplane";
            }
            typeOfAlert = "Warning";
        }
        if (doPost == 1 || doPost == 2 || doPost == 3)
            swal({
                title: typeOfAlert,
                text: messageEmptyFields,
                type: "warning",
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
        return false;
    }
});