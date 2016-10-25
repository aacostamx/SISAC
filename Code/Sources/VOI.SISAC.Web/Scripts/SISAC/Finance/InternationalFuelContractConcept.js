
var FuelConceptController = {
    iniciar: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
        FuelConceptController.settingModal();
    },

    getCombo: function () {
        var $table = $('#tbFuelContractConcept');
        var conceptItems = 0;
        var duplicate = false;
        var select = false;
        var interConceptID;
        var fuelConcept, provider, fuelConceptType, chargeFactorType;
        var fuelConceptID, providerID, fuelConceptTypeID, chargeFactorTypeID;

        fuelConceptID = $("#FuelConcept_FuelConceptName").val();
        fuelConcept = $("#FuelConcept_FuelConceptName option:selected").text();

        fuelConceptTypeID = $("#FuelConceptType_FuelConceptTypeName").val();
        fuelConceptType = $("#FuelConceptType_FuelConceptTypeName  option:selected").text();

        providerID = $("#Provider_ProviderName").val();
        provider = $("#Provider_ProviderName  option:selected").text();

        chargeFactorTypeID = $("#ChargeFactorType_ChargeFactorTypeName").val();
        chargeFactorType = $("#ChargeFactorType_ChargeFactorTypeName  option:selected").text();

        //Valida que todo el registro sea único
        //interConceptID = fuelConceptID + fuelConceptTypeID + providerID + chargeFactorTypeID;
        interConceptID = fuelConceptID + providerID;

        conceptItems = $table.bootstrapTable('getData');

        if (conceptItems.length > 0) {
            for (i = 0; i < conceptItems.length; i++) {
                var tableConceptID = conceptItems[i].InterConceptID;
                if (tableConceptID === interConceptID)
                    duplicate = true;
            }
        }

        if (fuelConceptID === "" || fuelConceptTypeID === "" || chargeFactorTypeID === "" || providerID === "")
            select = true;

        if (select) {
            FuelConceptController.showAlert('<strong>All fields are required</strong>', '<strong>Todos los campos son requeridos</strong>', 'warning', 'Warning', 'Advertencia');
            return;
        }

        if (duplicate) {
            swal({
                title: "Duplicado",
                text: "El concepto de combutible está duplicado",
                type: "error",
                confirmButtonColor: "#83217a"
            })
            return;
        }

        if (!duplicate && !select) {

            var index = conceptItems.length;
            var button = '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"FuelConceptController.removeConcept(\'' + interConceptID + '\')\"><i class=\"fa fa-minus\"></i></button>';

            $table.bootstrapTable('insertRow', {
                index: 1,
                row: {
                    InterConceptID: interConceptID,
                    FuelConceptID: fuelConceptID,
                    FuelConceptName: fuelConcept,
                    FuelConceptTypeCode: fuelConceptTypeID,
                    FuelConceptTypeName: fuelConceptType,
                    ChargeFactorTypeID: chargeFactorTypeID,
                    ChargeFactorTypeName: chargeFactorType,
                    ProviderNumber: providerID,
                    ProviderName: provider,
                    Actions: button
                }

            });
            FuelConceptController.showAlert('International Fuel Contract Concept has been add successfully', 'Concepto de combustible añadido exitosamente', 'success', 'Success', 'Exitoso');
        }

    },

    removeConcept: function (conceptId) {
        if (conceptId) {
            var $table = $('#tbFuelContractConcept');
            $table.bootstrapTable('removeByUniqueId', conceptId);
        }
    },

    AddAttributeFuelConceptID: function (value, row, index) {
        var input = '<input type=\'hidden\' name=InternationalFuelContractConcepts[' + index + '].FuelConceptID value=\'' + value + '\'>'
        return input + value;
    },

    AddAttributeFuelConceptTypeCode: function (value, row, index) {
        var input = '<input type=\'hidden\' name=InternationalFuelContractConcepts[' + index + '].FuelConceptTypeCode value=\'' + value + '\'>'
        return input + value;
    },

    AddAttributeChargeFactorTypeID: function (value, row, index) {
        var input = '<input type=\'hidden\' name=InternationalFuelContractConcepts[' + index + '].ChargeFactorTypeID value=\'' + value + '\'>'
        return input + value;
    },

    AddAttributeProviderNumber: function (value, row, index) {
        var input = '<input type=\'hidden\' name=InternationalFuelContractConcepts[' + index + '].ProviderNumber value=\'' + value + '\'>'
        return input + value;
    },

    postForm: function () {
        var $table = $('#tbFuelContractConcept');
        var conceptItems = 0;
        var providerPrimary = 0;
        var jetFuel = false;
        var intoPlane = false;

        conceptItems = $table.bootstrapTable('getData');
        providerPrimary = $("#ProviderNumberPrimary").val();

        if (conceptItems.length > 0) {

            if (conceptItems) {
                jQuery.each(conceptItems, function (index, value) {
                    //JET FUEL el concepto debe ser el mismo del contrato y debe existir en el concepto
                    if (value.FuelConceptTypeCode === 'JTFL' && value.ProviderNumber === providerPrimary) {
                        jetFuel = true;
                    }
                    ////INTO PLANE debe de exitir por lo menos un proovedor en el contrato
                    if (value.FuelConceptTypeCode === 'INTPL') {
                        intoPlane = true;
                    }
                });
            }

            if (jetFuel && intoPlane) {
                $table.bootstrapTable('showColumn', 'FuelConceptID');
                $table.bootstrapTable('hideColumn', 'FuelConceptName');

                $table.bootstrapTable('showColumn', 'FuelConceptTypeCode');
                $table.bootstrapTable('hideColumn', 'FuelConceptTypeName');

                $table.bootstrapTable('showColumn', 'ChargeFactorTypeID');
                $table.bootstrapTable('hideColumn', 'ChargeFactorTypeName');

                $table.bootstrapTable('showColumn', 'ProviderNumber');
                $table.bootstrapTable('hideColumn', 'ProviderName');

                document.getElementById("formContract").submit();
            }
            else {
                if (!jetFuel) {
                    FuelConceptController.showAlert('You must have at least one fuel concept name <strong>JET FUEL</strong> with the primary providers  of contract', 'Es necesario tener con al menos un nombre de concepto <strong>JET FUEL con el proovedor primario del contrato</strong>', 'warning', 'Warning', 'Advertencia');
                    return;
                }
                if (!intoPlane) {
                    FuelConceptController.showAlert('You must have at least one concept name <strong>INTO-PLANE</strong>', 'Es necesario tener con al menos un nombre de concepto <strong>INTO-PLANE</strong>', 'warning', 'Warning', 'Advertencia');
                    return;
                }
            }

        }
        else {
            FuelConceptController.showAlert('You must have at least one <strong> Concept Fuel </ strong>', 'Es necesario tener con al menos un <strong>Concepto de Combustible</strong>', 'warning', 'Warning', 'Advertencia');
            return;
        }

    },

    settingComboboxes: function () {
        $('#FuelConcept_FuelConceptName').val('');
        $('#FuelConceptType_FuelConceptTypeName').val('');
        $('#ChargeFactorType_ChargeFactorTypeName').val('');
        $('#Provider_ProviderName').val('');
    },

    settingModal: function () {
        $('#modalTable').on('hidden.bs.modal', function () {
            FuelConceptController.settingComboboxes();
        })
    },

    toggle: function (id) {
        $("#" + id).toggle();
    },

    showAlert: function (messageEn, messageEs, alertType, titleEn, tittleEs) {
        //"warning", "error", "success" and "info".
        alertType = alertType || "warning";
        if (currentLang.includes("es")) {
            swal({
                title: tittleEs,
                text: messageEs,
                type: alertType,
                confirmButtonColor: "#83217a",
                html: true,
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
                html: true,
                animation: "slide-from-top",
                timer: 12000
            })
        }
    },

    postGeneric: function () {
        var form = document.getElementById('formContract');
        if (form) {
            form.submit();
        }
    }
}

$(document).ready(FuelConceptController.iniciar);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}