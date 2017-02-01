var TimelineController = {
    AddTimelineMovement: function () {
        var $formModal = $("#frmMovement");
        $('#movementModal').modal('toggle');

        var dataForm = $formModal.serializeArray();
        var Sequence = $('.selected').data('sequence');
        var AirlineCode = $('.selected').data('airline');
        var FlightNumber = $('.selected').data('flight');
        var ItineraryKey = $('.selected').data('itinerarykey');
        var MovementDate = $("#MovementDate").data("DateTimePicker").date();


        if (MovementDate && dataForm[1] && dataForm[2]) {
            dataForm.push({ name: "Sequence", value: Sequence });
            dataForm.push({ name: "AirlineCode", value: AirlineCode });
            dataForm.push({ name: "FlightNumber", value: FlightNumber });
            dataForm.push({ name: "ItineraryKey", value: ItineraryKey });
            dataForm.push({ name: "MovementDate", value: MovementDate.format("YYYY/MM/DD HH:mm") });

            $.ajax({
                url: "../Timeline/AddTimelineMovement",
                dateType: 'JSON',
                type: 'POST',
                data: dataForm,
                async: false,
                success: function (data) {
                    swal({
                        title: "Success",
                        text: "Movement added correctly!",
                        type: "success",
                        confirmButtonColor: "#83217a",
                        confirmButtonText: "Okay",
                        closeOnConfirm: false,
                        confirmButtonColor: "#83217a",
                        animation: "slide-from-top",
                        timer: 12000
                    },
                    function () {
                        location.reload();
                    });
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
        else {
            TimelineController.showSweetAlert("Please enter all required fields", "Ingresar todos los campos requeridos", "warning", "*Required field", "*Campo Requerido");
        }



    },
    DeleteMovement: function (id) {
        swal({
            title: 'Are you sure?',
            text: 'Are you sure you want to delete the item',
            type: 'warning',
            confirmButtonColor: "#83217a",
            animation: "slide-from-top",
            closeOnConfirm: false,
            showCancelButton: true,
            closeOnCancel: false
        },
        function (isConfirm) {
            if (isConfirm) {
                $.post("../Itineraries/Timeline/DeleteTimelineMovement", { ID: id }, function (data) {
                    if (data) {
                        swal({
                            title: "Deleted!",
                            text: "The movement has been deleted.",
                            type: "success",
                            confirmButtonColor: "#83217a",
                            animation: "slide-from-top",
                            closeOnConfirm: false,
                            showCancelButton: true,
                            closeOnCancel: false
                        });
                        var table = $('*[data-id="' + id + '"]').attr("data-table");
                        $('#' + table).bootstrapTable('removeAll');
                        $('*[data-id="' + id + '"]').remove();
                    }
                    else {
                        swal({
                            title: "Cancelled!",
                            text: "The movement has not been deleted.",
                            type: "error",
                            confirmButtonColor: "#83217a",
                            animation: "slide-from-top",
                            closeOnConfirm: false,
                            showCancelButton: true,
                            closeOnCancel: false
                        });
                    }
                });
            } else {
                swal({
                    title: "Cancelled!",
                    text: "The movement has not been deleted.",
                    type: "error",
                    confirmButtonColor: "#83217a",
                    animation: "slide-from-top",
                    closeOnConfirm: false,
                    showCancelButton: true,
                    closeOnCancel: false
                });
            }
        });

    },
    EditMovement: function (id) {
        $.post("../Timeline/GetTimelineMovement", { ID: id }, function (data) {
            if (data) {
                var movement = $.parseJSON(data);
                if (movement) {
                    $('#modalTitleAdd').hide();
                    $('#modalTitleEdit').show();

                    $('#btnEditMove').show();
                    $('#btnAddMove').hide();

                    var date = moment(new Date(movement.MovementDate));
                    $('#IDMovement').val(id);
                    $('#OperationTypeID').val(movement.OperationTypeID);
                    $('#MovementTypeCode').val(movement.MovementTypeCode);
                    $('#MovementDate').data("DateTimePicker").date(moment(date));
                    $('#Position').val(movement.Position);
                    $('#ProviderNumber').val(movement.ProviderNumber);
                    $('#RemainingFuel').val(movement.RemainingFuel);
                    $('#Remarks').val(movement.Remarks);
                }

                $('#modalTitle').text('Edit Movement');
                $('#btnAddMove').hide();
                $('#btnEditMove').show();
                $('#movementModal').modal('show');
            }
        });
    },
    EditTimelineMovement: function () {
        var $formModal = $('#frmMovement');
        var dataForm = $formModal.serializeArray();
        var IDMovement = $('#IDMovement').val();
        var Sequence = $('.selected').data('sequence');
        var AirlineCode = $('.selected').data('airline');
        var FlightNumber = $('.selected').data('flight');
        var ItineraryKey = $('.selected').data('itinerarykey');
        var MovementDate = $("#MovementDate").data("DateTimePicker").date()
        dataForm.push({ name: "Sequence", value: Sequence });
        dataForm.push({ name: "AirlineCode", value: AirlineCode });
        dataForm.push({ name: "FlightNumber", value: FlightNumber });
        dataForm.push({ name: "ItineraryKey", value: ItineraryKey });
        dataForm.push({ name: "MovementDate", value: MovementDate.format("YYYY/MM/DD HH:mm") });
        dataForm.push({ name: "ID", value: IDMovement });

        $.ajax({
            url: "../Timeline/UpdateTimelineMovement",
            dateType: 'JSON',
            type: 'POST',
            data: dataForm,
            async: false,
            success: function (data) {
                if (data) {
                    $('#movementModal').modal('hide');

                    swal({
                        title: "Success",
                        text: "Movement edit correctly!",
                        type: "success",
                        confirmButtonColor: "#83217a",
                        confirmButtonText: "Okay",
                        closeOnConfirm: false,
                        confirmButtonColor: "#83217a",
                        animation: "slide-from-top",
                        timer: 12000
                    },
                    function () {
                        location.reload();
                    });
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    ChangeMoveDetail: function (control) {
        var attrib = $(control).data();

        if (attrib) {
            var $table = $('#' + attrib.table);
            $table.bootstrapTable('removeAll');

            $.ajax({
                type: 'POST',
                url: '../Itineraries/Timeline/GetTimelineMovement',
                data: attrib,
                success: function (data) {
                    var response = JSON.parse(data);
                    var editButton = '';
                    var deleteButton = '';

                    var edit = attrib.editpermission === "True";
                    var del = attrib.deletepermission === "True";

                    if (edit) {
                        editButton = '<button class="btn btn-default" type="button" name="Edit" title="Editar" onclick="TimelineController.EditMovement(' + response.ID + ');" style="background: rgb(139, 197, 63);"><i class="fa fa-pencil fa-fw"></i></button>';
                    }

                    if (del) {
                        deleteButton = '<button class="btn btn-default" type="button" name="Delete" title="Eliminar" onclick="TimelineController.DeleteMovement(' + response.ID + ');"><i class="fa fa-trash-o fa-fw"></i></button> </td>';
                    }

                    $table.bootstrapTable('insertRow', {
                        index: 1,
                        row: {
                            Action: editButton + deleteButton,
                            ID: response.ID,
                            MovementTypeCode: response.MovementType.MovementTypeCode,
                            MovementDescription: response.MovementType.MovementDescription,
                            OperationType: response.OperationTypeID,
                            OperationDescription: response.OperationType.OperationName,
                            MovementDate: moment(response.MovementDate).format("YYYY-MM-DD HH:mm"),
                            Position: response.Position ? response.Position : '',
                            ProviderNumber: response.Provider ? response.Provider.ProviderNumber : '',
                            ProviderName: response.Provider ? response.Provider.ProviderName : '',
                            RemainingFuel: response.RemainingFuel ? response.RemainingFuel : '',
                            Remarks: response.Remarks ? response.Remarks : ''
                        }
                    });
                },
                error: function (result) {
                    console.log("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
    },
    setMenuContext: function () {
        var $tables = $('table[id^=tbMoves-]');
        $.each($tables, function (index, item) {
            $(item).bootstrapTable({
                contextMenu: '#context-menu',
                contextMenuButton: '.menuButton',
                contextMenuAutoClickRow: true,
                onContextMenuItem: function (row, $el) {
                    if ($el.data("item") == 'Edit') {
                        console.log(item);
                        console.log(row);
                        alert("Editar " + $(item).attr('id'));
                    }

                    if ($el.data("item") == 'Delete') {
                        alert("Eliminar " + $(item).attr('id'));
                    }
                }
            });
        });
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
    showModal: function () {
        $('#modalTitleAdd').show();
        $('#modalTitleEdit').hide();

        $('#btnEditMove').hide();
        $('#btnAddMove').show();

        $('#frmMovement')[0].reset();

        $('#movementModal').modal('show');
    }
}

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}