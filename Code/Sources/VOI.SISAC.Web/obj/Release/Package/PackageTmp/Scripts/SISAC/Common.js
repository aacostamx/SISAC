$("form").submit(function () {
    $('#overlay').show();
});

$(document).on('submit', 'form', function (e) {
    $('#overlay').show();
});

$(window).bind('beforeunload', function () {
    $('#overlay').show();
});