
var commonFunctions = {

    relocation: function (loc) {
        location.replace(loc);
    },

    Uppercase: function (Control) {
        var controlName = document.getElementById(Control.id);
        controlName.value = controlName.value.toUpperCase();
        return true;
    },

    validarMaxLengthAlfanumerico: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ ]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthAlfanumericoPunto: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ.]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthAlfanumericoDiagonalGuion: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ\-\/]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthAlfanumericoDiagonal: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9\-\/]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthAlfanumericoGuion: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9-]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthAlfabetico: function (e, Control, maxlength, espacio) {
        commonFunctions.Uppercase(Control);
        var patron = /[a-zA-ZÃ±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ ]/;
        if (espacio > 0) {
            patron = /[a-zA-ZÃ±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼Ãœ]/;
        }

        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    reemplazarCaracteresInvalidos: function (e, control, tipo) {
        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        switch (tipo) {
            case 'alfanumerico':
                patron = /[^a-zA-Z0-9Ã±Ã‘ ]/g;
                break;
            case 'alfabetico':
                patron = /[^a-zA-ZÃ±Ã‘ ]/g;
                break;
            case 'numerico':
                patron = /[^0-9]/g;
                break;
        }
        var valor = control.value.replace(patron, '');
        control.value = valor;
    },

    validarNumeros: function (e) {
        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[0-9]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarMaxLengthNumerico: function (e, Control, maxlength, espacios) {
        espacios = espacios || 0;
        var patron = /[0-9]/;
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    DeshabiliarEspacio: function (event) {
        if (event.keyCode == 32) {
            event.returnValue = false;
            return false;
        }
    },

    validarNumeroDecimal: function (e, Control, precision, scale) {
        var dot = Control.value.indexOf(".");
        if (dot > -1) {
            if (Control.value.length > precision + 1) {
                Control.value = Control.value.substring(0, precision + 1);
            }
        }
        else {
            if (Control.value.length > precision - scale) {
                Control.value = Control.value.substring(0, precision - scale);
            }
        }

        var dot = Control.value.indexOf(".");
        if (dot > -1) {
            Control.value = Control.value.substring(0, dot + scale + 1);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        if (Control.value.length == 0 && tecla == 46) return false;
        if (dot > -1 && tecla == 46) return false;

        var patron = /[0-9\.]/;
        var te = String.fromCharCode(tecla);
        return algo = patron.test(te);
    },

    validarMaxLengthAlfanumericoCaracteresEspeciales: function (e, Control, maxlength, espacios) {
        espacios = espacios || 0;
        commonFunctions.Uppercase(Control);
        var patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼ÃœñÑÁÉÍÓÚáéíóú.,;:_\-\/\\%() ]/;
        if (espacios === 1) {
            // commonFunctions.DeshabiliarEspacio(e);
            patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼ÃœñÑÁÉÍÓÚáéíóú.,;:_\-\/\\%()]/;
        }

        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var te = String.fromCharCode(tecla);
        var algo = patron.test(te);
        return algo;
    },

    validarMatricula: function (e, Control, maxlength) {
        commonFunctions.Uppercase(Control);
        var dash = Control.value.indexOf("-");
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        if (Control.value.length == 0 && tecla == 45) return false;
        if (Control.value.length == maxlength - 1 && tecla == 45) return false;

        var patron = /[a-zA-Z0-9\-]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    actionsButtons: function (value, row, index) {
        return '<div class="btn-group btn-group-sm"> <button type="button" class="btn btn-default dropdown-toggle menuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> <span class="caret"></span> <span class="sr-only">Toggle Dropdown</span> </button> </div>';
    },

    validarAlfanumericoLlaves: function (e, Control, maxlength, espacios) {
        espacios = espacios || 0;
        commonFunctions.Uppercase(Control);
        if (Control.value.length > maxlength) {
            Control.value = Control.value.substring(0, maxlength);
        }

        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validateAlphanumeric: function (e) {
        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[a-zA-Z0-9]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validateNumeric: function (e) {
        //commonFunctions.Uppercase(Control);
        var tecla = e.which || e.keyCode;
        if (tecla == 8 || tecla == 9) return true;

        var patron = /[0-9]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarComentarios: function (e, Control) {
        //commonFunctions.Uppercase(Control);
        var tecla = e.which || e.keyCode;

        if (tecla == 8 || tecla == 9) return true;
        var patron = /[a-zA-Z0-9Ã±Ã‘Ã¡Ã©Ã­Ã³ÃºÃÃ‰ÃÃ“ÃšÃ¼ÃœñÑÁÉÍÓÚáéíóú.,;:_\-\/\\%+!¡¿?'"#$&*=() ]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },
    initDoc: function () {

    }
}

$(document).ready(commonFunctions.initDoc);

//window.onerror = function (message, url, linenumber) {
//    console.log('Message: ' + message);
//    console.log('URL: ' + url);
//    console.log('Line: ' + linenumber);
//}