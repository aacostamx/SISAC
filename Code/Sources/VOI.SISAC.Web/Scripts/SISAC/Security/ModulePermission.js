var messageData;

var ModulePermissionController = {
    getPermissions: function () {
        var moduleCode = $("#ModuleCode").val();
        $.ajax({
            url: '../ModulePermission/GetAllPermissions',
            type: "POST",
            dataType: "JSON",
            data: { moduleCode: moduleCode },
            async: false,
            success: function (data) {
                $('#PermissionSection').empty();
                if (data) {
                    $.each(data, function (index, item) {
                        var divCheckBox = $(document.createElement('div'));
                        divCheckBox.addClass('col-xs-12 col-sm-12 col-md-3 col-lg-3');

                        var inputCheckBox = $(document.createElement('input')).attr({
                            id: item.PermissionCode,
                            name: 'Permission[' + index + '].IsSelected',
                            type: 'checkbox',
                            checked: item.Selected,
                            value: true
                        });
                        inputCheckBox.addClass('checkVolaris top-padding');

                        var labelInput = $(document.createElement('label')).attr({
                            'for': item.PermissionCode,
                            name: 'Permission[' + index + '].IsSelected',
                            value: item.PermissionCode
                        });
                        labelInput.addClass('control_gris');
                        labelInput.append('<span></span>');

                        var inputHiddenCheckBox = $(document.createElement('input')).attr({
                            name: 'Permission[' + index + '].PermissionCode',
                            type: 'hidden',
                            value: item.PermissionCode,
                        });

                        var labelDescription = $(document.createElement('label')).text(item.PermissionName);
                        labelDescription.addClass('subtitle_h6_blue');

                        divCheckBox.append(inputCheckBox);
                        divCheckBox.append(labelInput);
                        divCheckBox.append(inputHiddenCheckBox);
                        divCheckBox.append(labelDescription);

                        $('#PermissionSection').append(divCheckBox);
                    });
                }
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
    postForm: function () {
        var form = document.getElementById('ModulePermissionCreate');
        if (form) {
            form.submit();
        }
    },
    showConfirmMessage: function () {
        swal({
            title: messageData.Title,
            text: messageData.Text,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: messageData.Confirm,
            confirmButtonColor: "#83217a",
            cancelButtonText: messageData.Cancel,
            closeOnConfirm: false
        },
        function () {
            ModulePermissionController.postForm();
        });
    },
    getMessageWarningConfiguration: function () {
        $.ajax({
            url: '../ModulePermission/GetWarningMessageConfiguration',
            type: "GET",
            dataType: "JSON",
            cache: false,
            success: function (data) {
                messageData = data;
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
    initiallize: function () {
        ModulePermissionController.getPermissions();
        ModulePermissionController.getMessageWarningConfiguration();
    }
}

$(document).ready(function () {
    ModulePermissionController.initiallize();
});