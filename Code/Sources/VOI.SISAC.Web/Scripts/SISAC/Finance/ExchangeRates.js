
var ExchangeRatesController = {
    iniciar: function () {
        if (!String.prototype.includes) {
            String.prototype.includes = function () {
                'use strict';
                return String.prototype.indexOf.apply(this, arguments) !== -1;
            };
        }
    },
    monthFormat: function (value, row, index) {
        if (currentLang.includes('es')) {
            ExchangeRatesController.spanishMonths(value);
        }
        else {
            ExchangeRatesController.englishMonths(value);
        }
        return value;
    },
    englishMonths: function (value) {
        switch (value) {
            case 1:
                value = "January";
                break;
            case 2:
                value = "February";
                break;
            case 3:
                value = "March";
                break;
            case 4:
                value = "April";
                break;
            case 5:
                value = "May";
                break;
            case 6:
                value = "June";
                break;
            case 7:
                value = "July";
                break;
            case 8:
                value = "August";
                break;
            case 9:
                value = "September";
                break;
            case 10:
                value = "October";
                break;
            case 11:
                value = "November";
                break;
            case 12:
                value = "December";
                break;
        }
    },
    spanishMonths: function (value) {
        switch (value) {
            case 1:
                value = "Enero";
                break;
            case 2:
                value = "Febrero";
                break;
            case 3:
                value = "Marzo";
                break;
            case 4:
                value = "Abril";
                break;
            case 5:
                value = "May0";
                break;
            case 6:
                value = "Junio";
                break;
            case 7:
                value = "Julio";
                break;
            case 8:
                value = "Agosto";
                break;
            case 9:
                value = "Septiembre";
                break;
            case 10:
                value = "Octubre";
                break;
            case 11:
                value = "Noviembre";
                break;
            case 12:
                value = "Diciembre";
                break;
        }

    }
}

$(document).ready(ExchangeRatesController.iniciar);

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}