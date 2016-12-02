var messageData;

var GendecArrivalController = {
    toggle_visibility: function (id) {
        var e = document.getElementById(id);
        e.style.display = (e.style.display == 'none') ? 'block' : 'none';
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
    IsClose: function () {
        if (isClose == "True") {

            document.getElementById("TotalPax").readOnly = true;
            document.getElementById("BlockTime").readOnly = true;
            document.getElementById("GateNumber").readOnly = true;
            document.getElementById("TotalCrew").readOnly = true;
            document.getElementById("ManifestNumber").readOnly = true;
            document.getElementById("ActualTimeOfArrival").readOnly = true;
            document.getElementById("Member").setAttribute("disabled", "disabled");
            document.getElementById("Observation").readOnly = true;

            swal({
                title: messageData.Title,
                text:  messageData.NotEditGendec,
                type: "warning",
                confirmButtonColor: "#83217a",
                html: true,
                timer: 12000
            })
            return;
        }
    },
    // Obtiene el número de los tripulantes para mostrarlo en el campos de TotalCrew
    GetNumberCrews: function () {
        $("#TotalCrew").val(totalCrew);
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
                errorClass: "text-danger text-danger-error",
                errorElement: 'span',
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
                    ActualTimeOfArrival: {
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
                    ActualTimeOfArrival: {
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
                            form.submit();
                        }
                        else {
                            swal({
                                title: messageData.Title,
                                text: messageData.NotExistSteward,
                                type: "warning",
                                confirmButtonColor: "#83217a",
                                html: true,
                                timer: 12000
                            });
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
                        });
                        return;
                    }
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
                errorClass: "text-danger text-danger-error",
                errorElement: 'span',
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
                    ActualTimeOfArrival: {
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
                    ActualTimeOfArrival: {
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
                        form.submit();
                    }
                    else {
                        swal({
                            title: messageData.Title,
                            text: messageData.NotCloseGendec,
                            type: "warning",
                            confirmButtonColor: "#83217a",
                            html: true,
                            timer: 12000
                        });
                        return;
                    }
                }
            });
        });
        // Termina Validate
    },
    getMessageWarningConfiguration: function () {
        $.ajax({
            url: '../GendecArrival/GetWarningMessageConfiguration',
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

    GendecArrivalController.Initiate;
    GendecArrivalController.getMessageWarningConfiguration();
    GendecArrivalController.IsClose();
    GendecArrivalController.GetNumberCrews();

    $('#Save').click(function () {
        $('#Action').val(1);
        GendecArrivalController.ValidateFieldsSave();
    });

    $('#Close').click(function () {
        $('#Action').val(2);
        GendecArrivalController.ValidateFieldsClosed();
    });

    $('#Open').click(function () {
        $('#Action').val(3);
    });
});

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}