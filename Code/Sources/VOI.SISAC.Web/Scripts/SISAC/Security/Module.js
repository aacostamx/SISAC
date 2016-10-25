var ModuleController = {
    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm"> <button type="button" class="btn btn-default dropdown-toggle menuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> <span class="caret"></span> <span class="sr-only">Toggle Dropdown</span> </button> </div>';
    },
    ConfirmDelete: function ()
    {
        if (currentLang.includes("es")) {
            messageEmptyFields = "Todas las relaciones con Modulo serán eliminadas. Modulo-Permisos, Roles-Modulo-Permiso, Módulo";
            typeOfAlert = "¿Estás seguro?";
            confirmButton = "Si";
            cancelButton = "No";
        }
        else {

            messageEmptyFields  = "All relationships with Module will be removed. Module-Permission, Roles-Module-Permission, Module";
            typeOfAlert = "Are you sure?";
            confirmButton = "Yes, delete it!";
            cancelButton = "No, cancel plx!";
        }
        swal({
            title: typeOfAlert,
            text: messageEmptyFields,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: confirmButton,
            confirmButtonColor: "#83217a",
            cancelButtonText: cancelButton,
            closeOnConfirm: false
        },
        function () {
            ModuleController.postForm();
        });
        return;
    },
    postForm: function () {
        var form = document.getElementById('deleteForm');
        if (form) {
            form.submit();
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

    ModuleController.Initiate;
});