var dt;

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
                        || operatingWeight > json.EmptyOperatingWeight)
                    {
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
        var inputValue = str.substr(str.indexOf('>')+1, str.length);
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
    postForm: function (action) {
        document.getElementById('Action').value = action ? action : 1;
        ManifestDepartureController.addAttrToDelayTable();
        var doPost = ManifestDepartureController.checkDataOperation();
        form = document.getElementById('form-manifest');
        if (form && doPost == 0) {
            form.submit();
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
            return;
        }
    },
    initialize: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
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

        ManifestDepartureController.setInfoOnCombos();

        $("#real-time").on("dp.change", function (e) {
            ManifestDepartureController.calculatedDelay();
        });
        $('#real-date').on("dp.change", function (e) {
            ManifestDepartureController.calculatedDelay();
        });

        ManifestDepartureController.calculatedDelay();
    }
}

$(document).ready(ManifestDepartureController.initialize());

$(document).on('submit', '#form-manifest', function (e) {
    //prevent the form from doing a submit
    e.preventDefault();
    return false;
});