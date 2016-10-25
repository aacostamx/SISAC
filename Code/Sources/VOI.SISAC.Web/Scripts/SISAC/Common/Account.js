accountController = {
    ChangeLanguage: function (language) {
        var languageDiv = document.getElementById('languageDiv');
        languageDiv.value = language;
        document.getElementById('changeLanguage').submit();
    },
    LogOff: function () {
        document.getElementById('logoutForm').submit();
    }
}