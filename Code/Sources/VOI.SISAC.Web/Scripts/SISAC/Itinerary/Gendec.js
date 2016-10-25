var messageData;

var GendecController = {
    toggle_visibility: function (id) {
        var e = document.getElementById(id);
        e.style.display = (e.style.display == 'none') ? 'block' : 'none';
    },
    // Obtiene la lista de la Tripulación
    GetCrewsGendec: function () {
        var crewID = $("#ddlCrew").val();
        var crew = $("#ddlCrew option:selected").text();

        if (crew != "Seleccionar..") {
            GendecController.AddCrews();
        }
        else {
            swal({
                title: messageData.Title,
                text: messageData.ChoiceCrew,
                type: "warning",
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
            return;
        }
    },
    // Agrega y valida que no existan repetidos los tripulantes (Copilotos y Capitanes)
    AddCrews: function () {
        var crewID = $("#ddlCrew").val();
        var crew = $("#ddlCrew option:selected").text();
        var $table = $('#tbCrew');
        var items = $table.bootstrapTable('getData');
        for (i = 0; i < items.length; i++) {
            if (crewID == String(items[i].CrewID).trim()) {
                swal({
                    title: messageData.Title,
                    text: messageData.AlreadyExistCrew,
                    type: "warning",
                    confirmButtonColor: "#83217a",
                    animation: "slide-from-top",
                    timer: 12000
                })
                return;
            }
        }
        GendecController.GetCrews(crewID, $table);
    },
    // Agrega y valida que no existan repetidos los tripulantes (Sobrecargos y Jefes de Cabina)
    AddSobrecargos: function () {
        var $table = $('#tbCrewSob');
        var crewID = $("#ddlCrewSob").val();
        var crew = $("#ddlCrewSob option:selected").text();
        var items = $table.bootstrapTable('getData');

        for (i = 0; i < items.length; i++) {
            if (crewID == String(items[i].CrewID).trim()) {
                swal({
                    title: messageData.Title,
                    text: messageData.AlreadyExistSteward,
                    type: "warning",
                    confirmButtonColor: "#83217a",
                    animation: "slide-from-top",
                    timer: 12000
                })
                return;
            }
        }
        GendecController.GetCrews(crewID, $table);
    },
    // Inserta en las tablas los tripulantes seleccionados
    GetCrews: function (crewID, $table) {
        $.ajax({
            url: '../GendecDeparture/GetCrewsGendec',
            type: "GET",
            dataType: "JSON",
            data: { crewID: crewID },
            async: false,
            success: function (data) {
                $table.bootstrapTable('insertRow',
                    {
                        index: 1,
                        row: {
                            CrewID: data.CrewID,
                            CrewTypeId: data.CrewTypeID,
                            NickName: data.NickName,
                            LastName: data.LastName,
                            PassportNumber: data.PassportNumber,
                            DateOfBird: GendecController.ConvertDate(data.DateOfBird),
                            Gender: data.Gender,
                            Citizenship: data.Citizenship,
                            Actions: '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"GendecController.removeCrew(\'' + crewID + '\')\"><i class=\"fa fa-minus\"></i></button>'
                        }
                    });
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
        var numberCrews = GendecController.GetNumberCrews();
        $("#TotalCrew").val(numberCrews);
    },
    // Convierte la Fecha al formato de dia/mes/año
    ConvertDate: function (datevalue) {
        var value = new Date
            (
                 parseInt(datevalue.replace(/(^.*\()|([+-].*$)/g, ''))
            );
        var dat = value.getDate() +
                    "/" +
                    value.getMonth() +
                    1 +
                    "/" +
                    value.getFullYear();
        return dat;
    },
    // Obtiene el número de tripulantes existentes en las tablas para mostralo en el campo de TotalCrew
    GetNumberCrews: function () {
        var $table = $('#tbCrewSob');
        var items = $table.bootstrapTable('getData');
        var $tableCrew = $('#tbCrew');
        var itemsCrew = $tableCrew.bootstrapTable('getData');

        var numberCrew = parseInt(items.length) + parseInt(itemsCrew.length);

        return numberCrew;
    },
    // Inserta los sobrecargos
    GetCrewsSobGendec: function () {
        var crewID = $("#ddlCrewSob").val();
        var crew = $("#ddlCrewSob option:selected").text();

        if (crew != "Seleccionar..") {
            GendecController.AddSobrecargos();
        }
        else {
            swal({
                title:  messageData.Title,
                text: messageData.ChoiceSteward,
                type: "warning",
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                timer: 12000
            })
            return;
        }
    },
    // Elimina de la tabla un tripulante, antes de ser guardado
    removeCrew: function (CrewID) {
        if (CrewID) {
            var $table = $('#tbCrew');
            $table.bootstrapTable('removeByUniqueId', CrewID);
            var $tableSob = $('#tbCrewSob');
            $tableSob.bootstrapTable('removeByUniqueId', CrewID);
        }
        var numberCrews = GendecController.GetNumberCrews();
        $("#TotalCrew").val(numberCrews);
    },
    // Guarda información de Gendec
    saveGendecDeparture: function () {
        var $table = $('#tbCrew');
        var $tableSob = $('#tbCrewSob');
        var items = 0;
        var itemsSob = 0;
        var member;
        var gateNumber;

        items = $table.bootstrapTable('getData');
        itemsSob = $tableSob.bootstrapTable('getData');

        if (items.length > 0) {
            if (itemsSob.length > 0) {
                $table.bootstrapTable('showColumn', 'CrewID');
                $tableSob.bootstrapTable('showColumn', 'CrewID');

                $table.bootstrapTable('append', itemsSob);
                $.ajax({
                    type: "POST",
                    url: "../GendecDeparture/Edit",
                    data: $("#formGendec").serialize(),
                    async: false,
                    success: function (data) {
                    },
                    error: function (request, status, error) {
                        if (request.status == 403) {
                            $('#UnauthorizedModal').modal('show');
                            return;
                        }
                    }
                })
                location.reload();
            }
            else {
                swal({
                    title: messageData.Title,
                    text: messageData.NotExistSteward,
                    type: "warning",
                    confirmButtonColor: "#83217a",
                    html: true,
                    timer: 12000
                })
                return;
            }
        }
        else {
            swal({
                title: messageData.Title,
                text: messageData.NotExistCrews,
                type: "warning",
                confirmButtonColor: "#83217a",
                html: true,
                timer: 12000
            })
            return;
        }
    },
    // Cierra el Gendec
    closeGendecDeparture: function () {
        var $table = $('#tbCrew');
        var $tableSob = $('#tbCrewSob');
        var items = 0;
        var itemsSob = 0;
        var member;

        items = $table.bootstrapTable('getData');
        itemsSob = $tableSob.bootstrapTable('getData');

        if (items.length > 0 && itemsSob.length > 0) {
            $table.bootstrapTable('showColumn', 'CrewID');
            $tableSob.bootstrapTable('showColumn', 'CrewID');

            $table.bootstrapTable('append', itemsSob);
            $.ajax({
                type: "POST",
                url: "../GendecDeparture/CloseGendec",
                data: $("#formGendec").serialize(),
                async: false,
                success: function (data) {
                },
                error: function (request, status, error) {
                    if (request.status == 403) {
                        $('#UnauthorizedModal').modal('show');
                        return;
                    }
                }
            })
            location.reload();
        }
        else {
            swal({
                title: messageData.Title,
                text: messageData.NotCloseGendec,
                type: "warning",
                confirmButtonColor: "#83217a",
                html: true,
                timer: 12000
            })
            return;
        }

    },
    AddAttributeCrewID: function (value, row, index) {
        var input = '<input type=\'hidden\' name=Crews[' + index + '].CrewID value=\'' + value + '\'>'
        return input + value;
    },
    AddAttributeCrewIDSob: function (value, row, index) {
        var input = '<input type=\'hidden\' name=Crews[' + index + '].CrewID     value=\'' + value + '\'>'
        return input + value;
    },
    // Valida si el Gendec esta cerrado para no permitir la edición
    isCloseDeparture: function () {

        if (isClose == "True") {
            document.getElementById("TotalPax").readOnly = true;
            document.getElementById("BlockTime").readOnly = true;
            document.getElementById("GateNumber").readOnly = true;
            document.getElementById("TotalCrew").readOnly = true;
            document.getElementById("ManifestNumber").readOnly = true;
            document.getElementById("ActualTimeOfDeparture").readOnly = true;
            document.getElementById("ddlCrew").setAttribute("disabled", "disabled");
            document.getElementById("ddlCrewSob").setAttribute("disabled", "disabled");
            document.getElementById("AddCrew").setAttribute("disabled", "disabled");
            document.getElementById("AddSob").setAttribute("disabled", "disabled");
            document.getElementById("Member").setAttribute("disabled", "disabled");
            document.getElementById("Remarks").setAttribute("disabled", "disabled");

            swal({
                title: messageData.Title,
                text: messageData.NotEditGendec,
                type: "warning",
                confirmButtonColor: "#83217a",
                html: true,
                timer: 12000
            })
            return;
        }
    },
    // Valida campos para guardar Gendec
    ValidateFieldsSave: function () {
        // Inicia Validate
        $.validator.addMethod("valueNotEquals", function (value, element, param) {
            return param != value;
        });
      
        $(function () {

            // Setup form validation on the #register-form element
            $("#formGendec").validate({

                // Specify the validation rules
                rules: {
                    TotalPax: {
                        required: true,
                        range: [1, parseInt(totalPassengers)],
                        number: true,
                    },
                    GateNumber: {
                        required: true,
                        maxlength: 8
                    },
                    ActualTimeOfDeparture: {
                        required: true,
                    },
                    TotalCrew: {
                        required: true,
                        range: [3, parseInt(totalCrews)],
                    },
                    BlockTime: {
                        required: true,
                    },
                    Member: {
                        required: true,
                    }
                },

                // Specify the validation error messages

                messages: {
                    TotalPax: {
                        required: messageData.RequiredField,
                        range: messageData.rangePax + totalPassengers
                    },
                    GateNumber: {
                        required: messageData.RequiredField,
                        maxlength: messageData.MaxLength8Val
                    },
                    ActualTimeOfDeparture: {
                        required: messageData.RequiredField
                    },
                    TotalCrew: {
                        required: messageData.RequiredField,
                        range: messageData.rangeCrew + totalCrews
                    },
                    BlockTime: {
                        required: messageData.RequiredField
                    },
                    Member: {
                        required: messageData.requiredCombo
                    }
                },

                submitHandler: function (form) {
                    GendecController.saveGendecDeparture();
                }
            });
        });
        // Termina Validate
    },
    // Valida campos para cerrar Gendec
    ValidateFieldsClosed: function () {
        // Inicia Validate
        $.validator.addMethod("valueNotEquals", function (value, element, param) {
            return param != value;
        });
        
        $(function () {

            // Setup form validation on the #register-form element
            $("#formGendec").validate({

                // Specify the validation rules
                rules: {
                    TotalPax: {
                        required: true,
                        range: [1, parseInt(totalPassengers)],
                        number: true,
                    },
                    GateNumber: {
                        required: true,
                        maxlength: 8
                    },
                    ActualTimeOfDeparture: {
                        required: true,
                    },
                    TotalCrew: {
                        required: true,
                        range: [3, parseInt(totalCrews)],
                    },
                    BlockTime: {
                        required: true,
                    },
                    Member: {
                        required: true,
                    },
                    ManifestNumber: {
                        required: true,
                    }
                },

                // Specify the validation error messages

                messages: {
                    TotalPax: {
                        required: messageData.RequiredField,
                        range: messageData.rangePax + totalPassengers
                    },
                    GateNumber: {
                        required: messageData.RequiredField,
                        maxlength: messageData.MaxLength8Val
                    },
                    ActualTimeOfDeparture: {
                        required: messageData.RequiredField
                    },
                    TotalCrew: {
                        required: messageData.RequiredField,
                        range: messageData.rangeCrew + totalCrews
                    },
                    BlockTime: {
                        required: messageData.RequiredField
                    },
                    Member: {
                        required: messageData.requiredCombo
                    },
                    ManifestNumber: {
                        required: messageData.RequiredField,
                        maxlength: messageData.MaxLength8Val
                    }
                },

                submitHandler: function (form) {
                    GendecController.closeGendecDeparture();
                }
            });
        });
        // Termina Validate
    },
    getMessageWarningConfiguration: function () {
        $.ajax({
            url: '../GendecDeparture/GetWarningMessageConfiguration',
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                messageData = data;
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
}

$(document).ready(function () {
    if (!String.prototype.includes) {
        String.prototype.includes = function () {
            'use strict';
            return String.prototype.indexOf.apply(this, arguments) !== -1;
        };
    }

    GendecController.Initiate;
    GendecController.getMessageWarningConfiguration();
    GendecController.isCloseDeparture();

    $('#Save').click(function () {
        GendecController.ValidateFieldsSave();
    });

    $('#Close').click(function () {
        GendecController.ValidateFieldsClosed();
    });
    $("#TotalCrew").val(totalCrew);
});

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}