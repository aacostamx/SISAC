
var RoleModulePermissionController = {
    iniciar: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
        RoleModulePermissionController.getRoleModules();
    },

    getRoleModules: function () {
        var role = $("#RoleCode").val();
        var url = "../RoleModulePermission/GetModulesPermissions?roleCode=";
        url = url.concat(role);

        $.ajaxSetup({ cache: false, async: false });

        $.get(url, function (data) {
            var roleModules = jQuery.parseJSON(data);
            if (roleModules) {
                for (i = 0; i < roleModules.DataValue.length; i++) {
                    var input = $('[data-value="' + roleModules.DataValue[i] + '"]').find("input");
                    input.prop('checked', true);
                    if (!roleModules.Edit) {
                        if (i === 0) {
                            var titles = $(".title input")
                            titles.prop('disabled', true);
                        }
                        input.prop('disabled', true);
                    }
                }
            }
        });
    },
    postRoleModules: function () {
        var selected, roleModules;
        var moduleCode, permissionCode, item;
        var jsonModules = [];
        var roleModulesPermissions = [];

        selected = $("#selectModules option:selected");

        if (selected) {
            selected.each(function (index, value) {
                moduleCode = $(this).data("module");
                permissionCode = $(this).data("permission");

                item = {};
                item["ModuleCode"] = moduleCode;
                item["PermissionCode"] = permissionCode;

                jsonModules.push(item);
            });
        }

        roleModulesPermissions = {
            "RoleCode": $("#RoleCode").val(),
            "ModulePermissions": jsonModules
        }

        $.ajax({
            url: '../RoleModulePermission/EditRoleModules',
            type: "POST",
            data: JSON.stringify(roleModulesPermissions),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data === "Success") {
                    RoleModulePermissionController.AlertRedirect("The configuration has been created",
                        "La configuración ha sido creada", "success", "Success", "Exitoso", "../Role/");
                }
                else {
                    RoleModulePermissionController.showAlert("The configuration has not been created",
                        "La configuración no ha sido creada", "error", "Error", "Error");
                }
            },
            error: function (result) {
                if (request.status == 403) {
                    $('#UnauthorizedModal').modal('show');
                }
                else {
                    RoleModulePermissionController.showAlert("The configuration has not been created",
                        "La configuración no ha sido creada", "error", "Error", "Error");
                    console.log('ERROR ' + result.status + ' ' + result.statusText);
                }
            }
        });

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
    AlertRedirect: function (messageEn, messageEs, alertType, titleEn, tittleEs, path) {
        //"warning", "error", "success" and "info".
        alertType = alertType || "warning";
        if (currentLang.includes("es")) {
            swal({
                title: tittleEs,
                text: messageEs,
                type: alertType,
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    location.href = path;
                }
            });
        }
        else {
            swal({
                title: titleEn,
                text: messageEn,
                type: alertType,
                confirmButtonColor: "#83217a",
                animation: "slide-from-top",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    location.href = path;
                }
            });
        }
    }
}
$(document).ready(RoleModulePermissionController.iniciar);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}
