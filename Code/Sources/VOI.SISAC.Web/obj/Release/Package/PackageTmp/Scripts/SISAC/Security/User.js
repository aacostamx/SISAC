var UserID, urlPath;
var listaErrores;
var User = {
    iniciar: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }

        //Validar si es Edit o Insert
        UserID = $("#UserID").val();
        if (UserID) {
            urlPath = "../../";
        }
        else {
            UserID = 0;
            urlPath = "../";
        }
        //Fin validacion Create o Edit

        $.validator.addMethod("valueNotEquals", function (value, element, param) {
            return param != value;
        });

        //Llamado a los resource de mensajes de validacion
        var url = urlPath + "User/GetWarningMessageConfiguration";

        $.ajax({
            url: url,
            type: "GET",
            cache: false,
            async : false,
            dataType: "JSON",
            success: function (data) {
                listaErrores = data;
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
        //Fin de llamado

        $(function () {

            // Setup form validation on the #register-form element
            $("#formUser").validate({

                // Specify the validation rules
                rules: {
                    AirlineCode: {
                        required: true
                    },
                    EmployeeNumber: {
                        required: true, //valueNotEquals: "OK",
                        maxlength: 10
                    },
                    UserName: {
                        required: true,
                        maxlength: 25
                    },
                    PasswordEncripted: {
                        required: "#UserVolaris:unchecked",
                        minlength: 5,
                        maxlength: 20
                    },
                    PasswordEncriptedAgain: {
                        equalTo: "#PasswordEncripted"
                    },
                    Name: "required",
                    FirstName: "required",
                    Email: {
                        email: true
                    },
                    DepartmentID: {
                        required: true
                    },
                    EntryDate: "required"
                },

                // Specify the validation error messages

                messages: {
                    AirlineCode: {
                        required: listaErrores.requiered
                    },
                    EmployeeNumber: {
                        required: listaErrores.requiered, //valueNotEquals: "no ok",
                        maxlength: listaErrores.max10
                    },
                    UserName: {
                        required: listaErrores.requiered,
                        maxlength: listaErrores.max25
                    },
                    Name: listaErrores.requiered,
                    FirstName: listaErrores.requiered,
                    PasswordEncripted: {
                        required: listaErrores.requiered,
                        minlength: listaErrores.min5,
                        maxlength: listaErrores.max20
                    },
                    PasswordEncriptedAgain: {
                        equalTo: listaErrores.password
                    },
                    Email: listaErrores.email,
                    DepartmentID: {
                        required: listaErrores.requiered
                    },
                    EntryDate: listaErrores.requiered
                },

                submitHandler: function (form) {
                    User.saveUser();
                }
            });            
        });
        
        //Traer Modulos Permisos que hay en BD para usuario x
        User.getRoleModulesFromDataBase();
        //validacion de checkeds en Airports y Profiles-Roles
        User.validateNumCheck(-1);
        User.validateNumCheckPR(-1);        
    },
    saveUser: function () {
        var selectedModules, optionSelected, splitModulePermission;
        var moduleCode, permissionCode, item;
        var jsonModulesPermission = [];
        var jsonAirports = [];
        var jsonProfileRoles = [];
        var count;

        var fields = [$("#Airline").val(), $("#EmployeeNumber").val(), $("#UserName").val(),
                      $("#PasswordEncripted").val(), $("#Name").val(), $("#FirstName").val(),
                      $("#LastName").val(), $("#Email").val(), $("#Department").val(), $("#UserVolaris").is(":checked")]

        
        var mensaje = User.validateRequiredFields(fields);


        //Si fecha es incorrecta
        if (mensaje == "EntryIncorrect") {
            User.showAlert("Incorrect Format of Entry Date",
                "Formato Incorrecto de Fecha de Ingreso", "warning", "Warning", "Advertencia");
            return;
        }

        //Si faltan datos de usuario
        //if (mensaje != "EntryIncorrect" && mensaje != "true") {
        //    User.showAlert("Fill " + mensaje + " of user data",
        //            "Falta " + mensaje + " de usuario por capturar", "warning", "Warning", "Advertencia");
        //    return;
        //}                

        //Obtener aeropuertos
        var tabla = $("#tbAirport tbody");
        count = 0;
        tabla.find('tr').each(function (col, td) {
            var $tds = $(this).find('td');

            item = {};
            item["StationCode"] = $tds.eq(0).text().trim();
            item["Principal"] = $tds.eq(2).find('input').is(":checked");
            if ($tds.eq(2).find('input').is(":checked")) {
                count++;
            }
            jsonAirports.push(item);
        });

        //Valida al menos un aeropuerto default Aeropuerto 
        if (count == 0) {
            User.showAlert("Select a default airport",
                    "Debes seleccionar un aeropuerto como principal", "warning", "Warning", "Advertencia");
            return;
        }

        //Obtener Perfil-Role
        var tabla = $("#tbProfileRole tbody");
        count = 0;
        tabla.find('tr').each(function (col, td) {
            var $tds = $(this).find('td');

            item = {};
            item["ProfileCode"] = $tds.eq(1).text().trim();
            item["RoleCode"] = $tds.eq(2).text().trim();
            item["Principal"] = $tds.eq(3).find('input').is(":checked");
            if ($tds.eq(3).find('input').is(":checked")) {
                count++;
            }
            jsonProfileRoles.push(item);
        });

        //Valida al menos un perfil-rol default  
        if (count == 0) {
            User.showAlert("Select a default Profile-Role",
                    "Debes seleccionar un Profile-Role como principal", "warning", "Warning", "Advertencia");
            return;
        }

        //Obtener Modulos Permisos

        selectedModules = $("#selectModules");
        optionSelected = selectedModules[0];

        var input;
        for (var i = 0 ; i < optionSelected.length ; i++) {
            input = $('[data-value="' + optionSelected[i].value + '"]').find("input");
            if (input[0].checked) {
                item = {};
                splitModulePermission = (optionSelected[i].value).split("/");
                item["ModuleCode"] = splitModulePermission[0];
                item["PermissionCode"] = splitModulePermission[1];
                jsonModulesPermission.push(item);
            }
        }

        //Configura Objeto Complejo UserVO para mandar a post
        userConfiguration = {
            "AirlineCode": fields[0],
            "EmployeeNumber": fields[1],
            "UserName": fields[2],
            "PasswordEncripted": fields[3],
            "Name": fields[4],
            "FirstName": fields[5],
            "LastName": fields[6],
            "Email": fields[7],
            "DepartmentID": fields[8],
            "EntryDate": User.GetDatePickerEntryValue(),
            "ModulePermissions": jsonModulesPermission,
            "UserProfileRoles": jsonProfileRoles,
            "UserAirports": jsonAirports,
            "UserVolaris": fields[9],
            "UserID": UserID,
            "Status": $("#Status").val()
        }



        var urlPar = "";
        if (UserID) {
            urlPar = "User/Edit";
        }
        else {
            urlPar = "User/Create";
        }
        //Realiza llamado a Create Post
        $.ajax({
            url: urlPath + urlPar,
            type: "POST",
            data: JSON.stringify(userConfiguration),//JSON.stringify({ cat: categoryModel, prd: productModel }
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data == "Success") {
                    User.AlertRedirect("The configuration has been created",
                        "La configuración ha sido creada", "success", "Success", "Exitoso", urlPath + "User");
                } else {
                    if (data == "10") {
                        User.showAlert("The user has not been created, duplicate User Name",
                     "El usuario no ha sido creado, nombre de usuario duplicado", "error", "Error", "Error");
                    } else {
                        User.showAlert("The user has not been created or modified, Error: " + data.substring(0,30),
                     "El usuario no ha sido creado o modificado, Error: " + data.substring(0,30), "error", "Error", "Error");
                    }
                }
            },
            error: function (request, status, error) {
                if (request.status == 403) {
                    $('#UnauthorizedModal').modal('show');
                } else {
                    User.showAlert("The user has not been created",
                     "El usuario no ha sido creado", "error", "Error", "Error");
                    console.log('ERROR ' + request.status + ' ' + request.statusText);
                }
            }
        });
    },
    validarMaxLengthAlfanumerico: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    },
    validarMaxLengthAlfanumericoPunto: function (e, Control, maxlength) {
        //commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ.]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    },
    toggle_visibility: function (id) {
        var e = document.getElementById(id);
        e.style.display = (e.style.display == 'none') ? 'block' : 'none';
    },
    AddAttributeStationCode: function (value, row, index) {
        row.StationCode = row.StationCode.trim();
        return value;
    },
    AddAttributeProfileRoleCode: function (value, row, index) {
        row.ProfileRoleCode = row.ProfileCode.trim() + row.RoleCode.trim();
        row.ProfileCode = row.ProfileCode.trim();
        row.RoleCode = row.RoleCode.trim();
        return value;
    },
    AddAirport: function () {
        var airportID = $("#ddlAirport").val();
        var airport = $("#ddlAirport option:selected").text();
        var $table = $('#tbAirport');

        if (airport != "Seleccionar..") {
            var items = $table.bootstrapTable('getData');
            if (items.length > 0) {
                for (i = 0; i < items.length; i++) {
                    if (airportID == String(items[i].StationCode).trim()) {
                        if (currentLang.includes("es")) {
                            messageEmptyFields = "El Aeropuerto ya existe";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageEmptyFields = "The Airport already exist";
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
                     index: 1,
                     row: {
                         StationCode: airportID,
                         StationName: airport,
                         Principal: '',
                         Actions: '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"User.removeAirport(\'' + airportID + '\')\"><i class=\"fa fa-minus\"></i></button>'
                     }
                 });
            }
            else {
                $table.bootstrapTable('insertRow',
                 {
                     index: 1,
                     row: {
                         StationCode: airportID,
                         StationName: airport,
                         Principal: '',
                         Actions: '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"User.removeAirport(\'' + airportID + '\')\"><i class=\"fa fa-minus\"></i></button>'
                     }
                 });
            }
        }
        else {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Debes elegir un Aeropuerto";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "Choice a Airport";
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
    AddProfileRole: function () {
        var profileCode = $("#ddlProfiles").val();
        var roleCode = $("#ddlRoles").val();
        var profile = $("#ddlProfiles option:selected").text();
        var role = $("#ddlRoles option:selected").text();
        var $table = $('#tbProfileRole');
        var profileRoleID;
        var selected = false;

        profileRoleCode = profileCode + roleCode;

        if (profile === "" || roleCode === "")
            selected = true;

        if (selected) {
            swal({
                title: "Warning",
                text: "Rofile and Rol are <strong>required fields</strong>",
                type: "warning",
                confirmButtonColor: "#83217a",
                html: true,
                timer: 12000
            })
            return;
        }

        if (profile != "Seleccionar.." && role != "Seleccionar..") {
            var items = $table.bootstrapTable('getData');
            if (items.length > 0) {
                for (i = 0; i < items.length; i++) {
                    if (profileCode == String(items[i].ProfileCode).trim() && roleCode == String(items[i].RoleCode).trim()) {
                        if (currentLang.includes("es")) {
                            messageEmptyFields = "El Perfil-Rol ya existe";
                            typeOfAlert = "Advertencia";
                        }
                        else {
                            messageEmptyFields = "The Profile-Role already exist";
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
                     index: 1,
                     row: {
                         ProfileCode: profileCode,
                         RoleCode: roleCode,
                         Principal: '',
                         ProfileRoleCode: profileRoleCode,
                         Actions: '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"User.removeProfileRole(\'' + profileRoleCode + '\' )\"><i class=\"fa fa-minus\"></i></button>'
                     }
                 });
            }
            else {
                $table.bootstrapTable('insertRow',
                 {
                     index: 1,
                     row: {
                         ProfileCode: profileCode,
                         RoleCode: roleCode,
                         Principal: '',
                         ProfileRoleCode: profileRoleCode,
                         Actions: '<button class=\"btn btn-default\" type=\"button\" name=\"Remove\" title=\"Remove\" onclick=\"User.removeProfileRole(\'' + profileRoleCode + '\')\"><i class=\"fa fa-minus\"></i></button>'
                     }
                 });
            }
        }
        else {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Debes elegir un Profile-Role";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "Choice a Profile-Role";
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

        User.getRoleModules();
    },
    AddAttributePrincipal: function (value, row, index) {
        if (row.Principal.indexOf('checked') > -1) {
            value = '<input class=\"checkbox\" checked type=\"checkbox\" id=\"Principal' + index + '\" name=\"UserAirports[' + index + '].Principal\"' +
            'onclick=\"User.validateNumCheck(' + index + ')\"></input>'
        } else {
            value = '<input class=\"checkbox\" type=\"checkbox\" id=\"Principal' + index + '\" name=\"UserAirports[' + index + '].Principal\"' +
            'onclick=\"User.validateNumCheck(' + index + ')\"></input>'
        }
        return value;
    },
    AddAttributePrincipalPR: function (value, row, index) {
        if (row.Principal.indexOf('checked') > -1) {
            value = '<input class=\"checkbox\" checked type=\"checkbox\" id=\"PrincipalPR' + index + '\" name=\"UserProfileRoles[' + index + '].Principal\"' +
            'onclick=\"User.validateNumCheckPR(' + index + ')\"></input>'
        } else {
            value = '<input class=\"checkbox\" type=\"checkbox\" id=\"PrincipalPR' + index + '\" name=\"UserProfileRoles[' + index + '].Principal\"' +
            'onclick=\"User.validateNumCheckPR(' + index + ')\"></input>'
        }
        return value;
    },
    removeAirport: function (StationCode) {
        if (StationCode) {
            var $table = $('#tbAirport');
            $table.bootstrapTable('removeByUniqueId', StationCode);
        }
    },
    removeProfileRole: function (ProfileRolCode) {
        if (ProfileRolCode) {
            var $table = $('#tbProfileRole');
            $table.bootstrapTable('removeByUniqueId', ProfileRolCode);
            //volver a consultar con roles actuales despues del que se esta eliminando
            User.getRoleModules();
        }
    },        
    validateRequiredFields: function (fields) {
        //Ingles
        var fieldsOnErrorEn = ["Airline", "Employee Number", "User Name",
                      "Password", "Name", "First Name",
                      "Last Name", "Email", "Deparment"];
        //Español
        var fieldsOnErrorSp = ["Aerolinea", "Numero de Empleado", "Nombre de Usuario",
                      "Password", "Nombre", "Apellido Paterno",
                      "Apellido Materno", "Email", "Deparmento"]


        var entryDate = User.GetDatePickerEntryValue();
        if (entryDate == null || entryDate == 'undefined') {
            return "EntryIncorrect";
        }

        //validando campo por campo
        ////////////////////////////////////////        
        for (var i = 0; i < fields.length; i++) {
            if (fields[i] == "") {
                if (currentLang.includes("es"))
                    return fieldsOnErrorSp[i];
                else
                    return fieldsOnErrorEn[i];
            }
        }
        ////////////////////////////////////////////

        return "true";
    },
    validarEmail: function (e, Control, maxlength) {
        if (Control.value.length > maxlength) {
            Control.value = "";
            User.showAlert("Length maximum must be " + maxlength + " characters",
                "Longitud máxima de " + maxlength + " caracteres", "warning", "Warning", "Advertencia");
            return;
        }

        patron = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (!patron.test(Control.value)) {
            //User.showAlert("Please enter a valid email address",
            //    "Email Invalido", "warning", "Warning", "Advertencia");
        }
        return;
    },
    ifUncheckPasswordRequired: function (e, Control) {
        if (Control.checked) {
            document.getElementById('PasswordEncripted').readOnly = true;
            document.getElementById('PasswordEncriptedAgain').readOnly = true;
            document.getElementById('PasswordEncripted').value = "";
            document.getElementById('PasswordEncriptedAgain').value = "";

        } else {
            document.getElementById('PasswordEncripted').readOnly = false;
            document.getElementById('PasswordEncriptedAgain').readOnly = false;
        }
    },
    GetDatePickerEntryValue: function () {
        var dateTimePicker = $('#EntryDate').data("DateTimePicker").date();
        if (dateTimePicker == null || dateTimePicker == 'undefined') {
            return null;
        }
        return dateTimePicker._d.toDateString();
    },
    getRoleModules: function () {
        var item;
        var jsonProfileRoles = [];

        var tabla = $("#tbProfileRole tbody");
        tabla.find('tr').each(function (col, td) {
            var $tds = $(this).find('td');
            item = {};
            item["ProfileCode"] = $tds.eq(1).text();
            item["RoleCode"] = $tds.eq(2).text();
            jsonProfileRoles.push(item);
        });


        //Eliminar opciones ya seleccionadas
        var selectedModules = $("#selectModules");
        var optionSelected = selectedModules[0];

        var input;
        for (var i = 0 ; i < optionSelected.length ; i++) {
            input = $('[data-value="' + optionSelected[i].value + '"]').find("input");
            input.prop('checked', false);
        }

        var titles = $(".title input")
        titles.prop('checked', false);
        //Fin de eliminar opciones
        

        $.ajax({
            url: urlPath + 'User/GetModulesPermissions',
            type: "POST",
            data: JSON.stringify({ profileRole: jsonProfileRoles }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache:false,
            async: false,
            success: function (data) {
                var roleModules = jQuery.parseJSON(data);                

                    if (roleModules) {
                        for (i = 0; i < roleModules.DataValue.length; i++) {
                            var input = $('[data-value="' + roleModules.DataValue[i] + '"]').find("input");

                            input.prop('checked', true);
                            
                            if (!roleModules.Edit) {
                                if (i == 0) {
                                    var titles = $(".title input")
                                    titles.prop('checked', true);
                                }
                            }
                        }
                    }
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
    getRoleModulesFromDataBase: function () {

        if (UserID) {
            var url = urlPath + "User/GetModulesPermissionsFromDataBase?UserID=";
            url = url.concat(UserID);

            $.ajaxSetup({ cache: false, async: false });

            $.get(url, function (data) {
                var roleModules = jQuery.parseJSON(data);
                if (roleModules) {
                    for (i = 0; i < roleModules.DataValue.length; i++) {
                        var input = $('[data-value="' + roleModules.DataValue[i] + '"]').find("input");

                        input.prop('checked', true);

                        if (!roleModules.Edit) {
                            if (i == 0) {
                                var titles = $(".title input")
                                titles.prop('checked', true);
                            }
                        }
                    }
                }
            });
        }
    },
    validateNumCheck: function (id)
    {
        var tabla = $("#tbAirport tbody")

        var count = 0;
        
        tabla.find('tr').each(function (col, td) {
            var $tds = $(this).find('td');

            if ($tds.eq(2).find('input').is(":checked")) {
                count = count + 1;
                if (count > 1) {
                    if ($("#Principal" + id)[0])//Si existe
                    $("#Principal" + id)[0].checked = false;
                }
            }
        });

        //Implica que hay dos checks que vienen de BD, -1 es cuando carga(ready) y count >= 2 es pues cuando viene de BD con solo 2  debe quitarlos
        if (count > 2 || (id == -1 && count >= 2))
        {
            //Borrar todos los checks
            tabla.find('tr').each(function (col, td) {
                var $tds = $(this).find('td');

                if ($tds.eq(2).find('input').is(":checked")) {
                    $tds.eq(2).find('input')[0].checked = false;
                }
            });
        }

        if (count > 1)
        {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Solo debe existir un aeropuerto como principal";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "Solo debe existir un aeropuerto como principal";
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
    validateNumCheckPR: function (id) {
        var tabla = $("#tbProfileRole tbody")

        var count = 0;

        tabla.find('tr').each(function (col, td) {
            var $tds = $(this).find('td');

            if ($tds.eq(3).find('input').is(":checked")) {
                count = count + 1;
                if (count > 1) {
                    if ($("#PrincipalPR" + id)[0])//Si existe
                    $("#PrincipalPR" + id)[0].checked = false;
                }
            }
        });

        //Implica que hay dos checks que vienen de BD, -1 es cuando carga(ready) y count >= 2 es pues cuando viene de BD con solo 2  debe quitarlos
        if (count > 2 || (id == -1 && count >= 2)) {
            //Borrar todos los checks
            tabla.find('tr').each(function (col, td) {
                var $tds = $(this).find('td');

                if ($tds.eq(3).find('input').is(":checked")) {
                    $tds.eq(3).find('input')[0].checked = false;
                }
            });
        }

        if (count > 1) {
            if (currentLang.includes("es")) {
                messageEmptyFields = "Solo debe existir un Perfil-Role como principal";
                typeOfAlert = "Advertencia";
            }
            else {
                messageEmptyFields = "Solo debe existir un Perfil-Role como principal";
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
    GetRolesByProfile: function ()
    {
        var profileCode = $('#ddlProfiles').val();
        if (profileCode != null)
        {
            $.ajax({
                url: urlPath + 'User/GetRolesByProfile',//para funcione en create y edit
                type: "GET",
                dataType: "JSON",
                async: false,
                data: { profileCode: profileCode },
                success: function (data) {
                    $('#ddlRoles').html("");
                    $.each(data, function (index, item) {
                        $('#ddlRoles').append($("<option></option>").val(item.Id).html(item.Description));
                    });
                },
                error: function (result) {
                    alert("ERROR " + result.status);
                }
            });
        }
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

$(document).ready(User.iniciar);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}