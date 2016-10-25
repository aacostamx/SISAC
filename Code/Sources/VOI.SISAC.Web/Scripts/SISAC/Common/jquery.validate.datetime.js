
var DateTimeController = {

    iniciar: function () {          
        moment.locale(currentLang);
        $('.ClassDate').datetimepicker({
            locale: currentLang,
            format: 'L',
            showClose: true,
            showClear: true,
            toolbarPlacement: 'top'
        });

        
    }
    ,
    DateTimeVal: function () {
        jQuery.extend(jQuery.validator.methods, {
            date: function (value, element) {
                var isChrome = window.chrome;

                // make correction for chrome
                if (isChrome) {
                    var d = new Date();
                    if (currentLang.toString() == "en-US") {
                        var day = moment(value, "MM/DD/YYYY", currentLang);
                    }
                    else {
                        var day = moment(value, 'DD/MM/YYYY', currentLang);
                    }
                    
                    var isValid = this.optional(element) ||
                    !/Invalid|NaN/.test(new Date(day));
                    return isValid;
                }
                else {
                    // leave default behavior
                    return this.optional(element) ||
                    !/Invalid|NaN/.test(new Date(value));
                }
            }
        });
    }
    ,
    Initiate: function () {
        DateTimeController.iniciar();
        DateTimeController.DateTimeVal();
    }
}

$(document).ready(DateTimeController.Initiate);

