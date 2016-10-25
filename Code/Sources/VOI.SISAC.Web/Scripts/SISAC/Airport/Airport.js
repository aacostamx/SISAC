function DisplayElement(checkbox, div) {
    if ($('#' + checkbox).is(':checked')) {
        $('#' + div).show();
    }
    else {
        $('#' + div).hide();
    }
}

window.onerror = function (message, url, linenumber) {
    console.log('Message: ' + message);
    console.log('URL: ' + url);
    console.log('Line: ' + linenumber);
}