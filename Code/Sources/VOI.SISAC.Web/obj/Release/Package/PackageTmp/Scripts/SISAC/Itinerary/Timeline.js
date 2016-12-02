var TimelineController = {
    getTimeline: function (equipNum, seq, airline, flight, itiKey, direction) {
        var url = '../Timeline/GetTimelineByEquipmentNumber';

        // PAGINATION, WORK IN PROGRESS
        if (direction == 1) {
            url = '../Timeline/GetTimelineForFlightPrev'
        }
        else if (direction == 2) {
            url = '../Timeline/GetTimelineForFlightNext'
        }
        $.ajax({
            url: url,
            dateType: 'JSON',
            type: 'POST',
            data: { equipmentNumber: equipNum },
            async: false,
            success: function (data, textStatus) {
                if (data && $('.stations-list')) {
                    var obj = JSON.parse(data);
                    if (obj.length > 0) {

                        // PAGINATION, WORK IN PROGRESS ...
                        // ELEMENTS TO THE LEFT
                        if (direction == 1) {
                            $.each(obj.rows, function (index, item) {
                                $('#stations-list').prepend(TimelineController.createTimelineElement(item));
                            });
                            $.each(obj.rows, function (index, item) {
                                $('#timeline-body').prepend(TimelineController.createTimelineBody(item));
                            });
                        }
                            // ELEMENTS TO THE RIGHT, THIS CAN BE REFACTOR WITH THE BEGINING
                        else if (direction == 2) {
                            $.each(obj.rows, function (index, item) {
                                $('#stations-list').append(TimelineController.createTimelineElement(item));
                            });
                            $.each(obj.rows, function (index, item) {
                                $('#timeline-body').append(TimelineController.createTimelineBody(item));
                            });
                        }
                            // FIRST TIME THAT THE ELEMENTS ARE CREATED
                        else {
                            $.each(obj, function (index, item) {
                                $('#stations-list').append(TimelineController.createTimelineElement(item));
                            });
                            $.each(obj, function (index, item) {
                                $('#timeline-body').append(TimelineController.createTimelineBody(item));
                                ////TimelineController.changeMovDetail(0, item.Sequence, item.AirlineCode, item.FlightNumber, item.ItineraryKey);
                            });
                        }
                        $('#timeline').attr('data-startat', seq + '-' + airline + '-' + flight + '-' + itiKey);
                    }
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    getMovements: function (seq, airline, flight, itiKey) {
        $.ajax({
            url: '../Timeline/GetMovementsForFlight',
            dateType: 'JSON',
            type: 'GET',
            data: { sequence: 1, airlineCode: airline, flightNumber: flight, itineraryKey: itiKey },
            async: false,
            success: function (data, textStatus) {
                if (data) {
                    var movesDiv = document.getElementById('moves');
                    var $movesTable = $('#tbMoves');
                    if (movesDiv && $movesTable) {
                        movesDiv.innerHTML = '';
                        var obj = JSON.parse(data);
                        $movesTable.bootstrapTable('removeAll');
                        $.each(obj.rows, function (index, item) {
                            var moveSpan = $(document.createElement('SPAN')).attr({
                                id: 'mov-' + index,
                                name: 'mov-' + index,
                                onclick: 'TimelineController.changeMovDetail(' + index + ')'
                            });

                            if (index == 0) {
                                $(moveSpan).addClass("active");
                            }

                            var textnode = document.createTextNode(item.MovementDate + ' - ' + item.MovementTypeDescription);
                            moveSpan[0].appendChild(textnode);
                            movesDiv.appendChild(moveSpan[0]);
                            $movesTable.bootstrapTable('insertRow', {
                                index: index,
                                row: {
                                    MovementTypeCode: item.MovementTypeCode,
                                    MovementTypeDescription: item.MovementTypeDescription,
                                    Position: item.Position,
                                    OperationDescription: item.OperationDescription,
                                    RemainingFuel: item.RemainingFuel,
                                }
                            });
                        });

                        TimelineController.changeMovDetail(0);
                    }
                }
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    },
    changeMovDetail: function (index, seq, airline, flight, itiKey) {
        var $movesTable = $('#tbMoves-' + seq + '-' + airline + '-' + flight + '-' + itiKey);
        if ($movesTable && index != 'undefined') {
            $movesTable.removeClass('hidden');
            for (var i = 0; i < $movesTable.bootstrapTable('getData').length; i++) {
                $movesTable.bootstrapTable('hideRow', { index: i });
            }

            $movesTable.bootstrapTable('showRow', { index: index });
        }
    },
    changeStationMoves: function (control) {
        if (control) {
            var dataAttr = $(control).data();
            $("#current-airline").html('<strong style="color: white">' + dataAttr.airline + '</strong><span class="lines"></span>');
            $("#current-flight").html('<strong style="color: white">' + dataAttr.flight + '</strong><span class="lines"></span>');
            $("#current-date").html('<strong style="color: white">' + dataAttr.date + '</strong><span class="lines"></span>');
            $("#current-time").html('<strong style="color: white">' + dataAttr.time + '</strong><span class="lines"></span>');

            // PAGINATION, WORK IN PROGRESS ...
            //if (!dataAttr.next) {
            //    // Set parameter here
            //    var selected = $(control).data('sequence') + '-' + $(control).data('airline') + '-' + $(control).data('flight') + '-' + $(control).data('itinerary');
            //    TimelineController.getTimeline($(control).data('sequence'), $(control).data('airline'), $(control).data('flight'), $(control).data('itinerary'), 2);
            //    $(control).data('next', true);
            //    setConfig($('#timeline'), selected);
            //}
            //if (!dataAttr.prev) {
            //    // Set parameter here
            //    var selected = $(control).data('sequence') + '-' + $(control).data('airline') + '-' + $(control).data('flight') + '-' + $(control).data('itinerary');
            //    TimelineController.getTimeline($(control).data('sequence'), $(control).data('airline'), $(control).data('flight'), $(control).data('itinerary'), 1);
            //    $(control).data('prev', true);
            //    setConfig($('#timeline'), selected);
            //}
        }
    },
    createTimelineElement: function (item) {
        var date = moment(item.Itinerary.DepartureDate, 'YYYY/MM/DD HH:mm');
        return $('<li>').attr('id', item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey).append(
            $('<a>').html(item.Itinerary.DepartureStation)
                    .attr({
                        'onclick': 'TimelineController.changeStationMoves(this)',
                        'data-sequence': item.Sequence,
                        'data-airline': item.AirlineCode,
                        'data-flight': item.FlightNumber,
                        'data-itinerary': item.ItineraryKey,
                        'data-date': date.format('YYYY/MM/DD'),
                        'data-time': date.format('HH:mm'),
                        'data-next': item.NextFlightNumber ? true : false,
                        'data-prev': item.PreviousFlightNumber ? true : false,
                        'href': '?Sequence='
                            + item.Sequence
                            + '&AirlineCode='
                            + item.AirlineCode
                            + '&FlightNumber='
                            + item.FlightNumber
                            + '&ItineraryKey='
                            + item.ItineraryKey
                    })
        );
    },
    createTimelineBody: function (item) {
        var movDescription = movDetail = '';
        var providerName = null, providerNumber = null;


        $.each(item.TimelineMovements, function (indexMov, movements) {
            var date = moment(movements.MovementDate, 'YYYY/MM/DD HH:mm');
            movDescription +=
            '<span id="mov-' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '-' + movements.ID + '"' + ' onclick="TimelineController.changeMovDetail(' + indexMov + ',' + item.Sequence + ',\'' + item.AirlineCode + '\',\'' + item.FlightNumber + '\',\'' + item.ItineraryKey + '\')" data-id="' + movements.ID + '">'
                + date.format('HH:mm') + ' - ' + movements.MovementType.MovementDescription +
            '</span>';

            // Commented for further implementations
            ////if (indexMov != 0) {
            ////    movDescription +=
            ////    '<span id="mov-' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '-' + movements.ID + '"' + ' onclick="TimelineController.changeMovDetail(' + indexMov + ',' + item.Sequence + ',\'' + item.AirlineCode + '\',\'' + item.FlightNumber + '\',\'' + item.ItineraryKey + '\')" data-id="' + movements.ID + '">'
            ////        + date.format('HH:mm') + ' - ' + movements.MovementType.MovementDescription +
            ////    '</span>';
            ////}
            ////else {
            ////    movDescription +=
            ////    '<span id="mov-' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '-' + movements.ID + '"' + ' onclick="TimelineController.changeMovDetail(' + indexMov + ',' + item.Sequence + ',\'' + item.AirlineCode + '\',\'' + item.FlightNumber + '\',\'' + item.ItineraryKey + '\')" data-id="' + movements.ID + '" class="active">'
            ////        + date.format('HH:mm') + ' - ' + movements.MovementType.MovementDescription +
            ////    '</span>';
            ////}

            if (movements.Provider) {
                providerNumber = movements.Provider.ProviderNumber;
                providerName = movements.Provider.ProviderName;
            }

            //var editButton = '<button class="btn btn-default" type="button" name="Edit" title="Editar" onclick="TimelineController.EditMovement(' + movements.ID + ',' + item.Sequence + ',\'' + item.AirlineCode + '\',\'' + item.FlightNumber + '\',\'' + item.ItineraryKey + '\')");" style="background: rgb(139, 197, 63);"><i class="fa fa-pencil fa-fw"></i></button>';
            var editButton = '<button class="btn btn-default" type="button" name="Edit" title="Editar" onclick="TimelineController.EditMovement(' + movements.ID + ');" style="background: rgb(139, 197, 63);"><i class="fa fa-pencil fa-fw"></i></button>';
            var deleteButton = '<button class="btn btn-default" type="button" name="Delete" title="Eliminar" onclick="TimelineController.DeleteMovement(' + movements.ID + ');"><i class="fa fa-trash-o fa-fw"></i></button> </td>'

            movDetail +=
                '<tr>' +
                    '<td> ' + editButton + deleteButton +
                    '<td>' + movements.ID + '</td>' +
                    '<td>' + movements.MovementTypeCode + '</td>' +
                    '<td>' + movements.MovementType.MovementDescription + '</td>' +
                    '<td>' + movements.OperationTypeID + '</td>' +
                    '<td>' + movements.OperationType.OperationName + '</td>' +
                    '<td>' + date.format('YYYY/MM/DD HH:MM') + '</td>' +
                    '<td>' + movements.Position + '</td>' +
                    '<td>' + providerNumber + '</td>' +
                    '<td>' + providerName + '</td>' +
                    '<td>' + movements.RemainingFuel + '</td>' +
                    '<td>' + movements.Remarks + '</td>' +
                '</tr>'
        });
        var htmlString =
            '<li id="' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '" style="width: 828px; opacity: 0.2;" class="">' +
                '<div class="wpex-timeline-label">' +
                    '<div class="timeline-media">' +
                                '<div class="viavi_share">' +
                                    '<div class="Vbtn Vvolaris Vslide_x" onclick="TimelineController.showModal()">' +
                                        '<div class="Vicon"><i class="fa fa-plus"></i></div>' +
                                        '<div class="Vslide"> <span> Add </span> </div>' +
                                        '<div class="viavi_bg_button">' +
                                            // Cambiar texto para que sea multi lenguaje
                                            '<div>' + 'Movement' + '</div>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                        '<div class="wpex-filter active" style="padding-left: 75px;">' +
                            '<div id="moves-' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '">' +
                                movDescription +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="timeline-details">' +
                        '<h2>Movement Details</h2>' +
                        '<div class="wptl-excerpt">' +
                            '<div class="container-fluid">' +
                                '<table class="table table-bordered hidden" id="tbMoves-' + item.Sequence + '-' + item.AirlineCode + '-' + item.FlightNumber + '-' + item.ItineraryKey + '" data-toggle="table" data-mobile-responsive="true" data-unique-id="ID">' +
                                    '<thead>' +
                                        '<tr>' +
                                            '<th style="" data-field="Action">Acciones</th>' +
                                            '<th style="" data-field="ID" data-visible="false">ID</th>' +
                                            '<th style="" data-field="MovementTypeCode">Código</th>' +
                                            '<th style="" data-field="MovementDescription">Descripción</th>' +
                                            '<th style="" data-field="OperationType" data-visible="false">Tipo Operación</th>' +
                                            '<th style="" data-field="OperationDescription">Tipo Operación</th>' +
                                            '<th style="" data-field="MovementDate">Fecha</th>' +
                                            '<th style="" data-field="Position">Posición</th>' +
                                            '<th style="" data-field="ProviderNumber" data-visible="false">Provider Number</th>' +
                                            '<th style="" data-field="ProviderName">Provider Name</th>' +
                                            '<th style="" data-field="RemainingFuel">Remaining Fuel</th>' +
                                            '<th style="" data-field="Remarks">Observaciones</th>' +
                                        '</tr>' +
                                    '</thead>' +
                                    '<tbody>' +
                                        movDetail +
                                    '</tbody>' +
                                '</table>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
                '<div class="clearfix"></div>' +
            '</li>';
        return htmlString;
    },
    AddTimelineMovement: function () {
        var $formModal = $("#frmMovement");
        $('#movementModal').modal('toggle');


        var dataForm = $formModal.serializeArray();
        var Sequence = $('.selected').data('sequence');
        var AirlineCode = $('.selected').data('airline');
        var FlightNumber = $('.selected').data('flight');
        var ItineraryKey = $('.selected').data('itinerary');
        var MovementDate = $("#MovementDate").data("DateTimePicker").date()
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
                alert('OKAY');
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
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
                $.post("../Timeline/DeleteTimelineMovement", { ID: id }, function (data) {
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
                console.log(movement.MovementDate);
                $('#IDMovement').val(id);
                $('#OperationTypeID').val(movement.OperationTypeID);
                $('#MovementTypeCode').val(movement.MovementTypeCode);
                //$('#MovementDate').data("DateTimePicker").clear();
                //$('#MovementDate').datetimepicker({ defaultDate: movement.MovementDate, });
                $('#Position').val(movement.Position);
                $('#ProviderNumber').val(movement.ProviderNumber);
                $('#RemainingFuel').val(movement.RemainingFuel);
                $('#Remarks').val(movement.Remarks);

                $('#modalTitle').text('Edit Movement');
                $('#btnAddMove').hide();
                $('#btnEditMove').show();
                $('#movementModal').modal('show');
            }
        });
    },
    EditTimelineMovement: function ()
    {
        var $formModal = $('#frmMovement');
        var dataForm = $formModal.serializeArray();
        var IDMovement = $('#IDMovement').val();
        var Sequence = $('.selected').data('sequence');
        var AirlineCode = $('.selected').data('airline');
        var FlightNumber = $('.selected').data('flight');
        var ItineraryKey = $('.selected').data('itinerary');
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
                alert('OKAY - EDIT');
            },
            error: function (result) {
                console.log("ERROR " + result.status + ' ' + result.statusText);
            }
        });
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
        $('#modalTitle').text('Add Movement');
        $('#btnEditMove').hide();
        $('#btnAddMove').show();

        $('#movementModal').modal('show');
    }
}

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}