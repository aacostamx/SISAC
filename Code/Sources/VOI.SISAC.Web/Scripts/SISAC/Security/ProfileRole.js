var messageData;

var ProfileRoleController = {
    getRoles: function () {
        var profileCode = $("#ProfileCode").val();
        $.ajax({
            url: '../ProfileRole/GetAllRoles',
            type: "POST",
            dataType: "JSON",
            data: { profileCode: profileCode },
            async: false,
            success: function (data) {
                $('#RolesSection').empty();
                if (data) {
                    $.each(data, function (index, item) {
                        var divCheckBox = $(document.createElement('div'));
                        divCheckBox.addClass('col-xs-12 col-sm-12 col-md-3 col-lg-3');

                        var inputCheckBox = $(document.createElement('input')).attr({
                            id: item.RoleCode,
                            name: 'Role[' + index + '].IsSelected',
                            type: 'checkbox',
                            checked: item.Selected,
                            value: true
                        });
                        inputCheckBox.addClass('checkVolaris top-padding');

                        var labelInput = $(document.createElement('label')).attr({
                            'for': item.RoleCode,
                            name: 'Role[' + index + '].IsSelected',
                            value: item.RoleCode
                        });
                        labelInput.addClass('control_gris');
                        labelInput.append('<span></span>');

                        var inputHiddenCheckBox = $(document.createElement('input')).attr({
                            name: 'Role[' + index + '].RoleCode',
                            type: 'hidden',
                            value: item.RoleCode,
                        });

                        var labelDescription = $(document.createElement('label')).text(item.RoleName);
                        labelDescription.addClass('subtitle_h6_blue');

                        divCheckBox.append(inputCheckBox);
                        divCheckBox.append(labelInput);
                        divCheckBox.append(inputHiddenCheckBox);
                        divCheckBox.append(labelDescription);

                        $('#RolesSection').append(divCheckBox);
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
            ProfileRoleController.postForm();
        });
    },
    getMessageWarningConfiguration: function () {
        $.ajax({
            url: '../ProfileRole/GetWarningMessageConfiguration',
            type: "GET",
            dataType: "JSON",
            success: function (data) {
                messageData = data;
            },
            error: function (result) {
                console.log('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    },
    initiallize: function () {
        ProfileRoleController.getRoles();
        ProfileRoleController.getMessageWarningConfiguration();
    }
}

$(document).ready(function () {
    ProfileRoleController.initiallize();
});