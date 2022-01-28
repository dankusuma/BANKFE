$(document).ready(() => {
    var passwordValid = false;
    var confirmPswValid = false;

    $('#newPass').on("input", () => {
        const value = $('#newPass').val();
        passwordValid = value.length >= 8 && value.length < 50;
        const isDisable = !passwordValid || !confirmPswValid;

        console.log(passwordValid, confirmPswValid, isDisable);
        $('#changePasswodBtn').prop('disabled', isDisable);
        if (!isDisable) $('#warning-text').hide();
        else $('#warning-text').show();
    });

    $('#confirmNewPass').on("input", () => {
        const value = $('#confirmNewPass').val();
        confirmPswValid = value.length >= 8 && value.length < 50;
        const isDisable = !passwordValid || !confirmPswValid;

        console.log(passwordValid, confirmPswValid, isDisable);
        $('#changePasswodBtn').prop('disabled', isDisable);
        if (!isDisable) $('#warning-text').hide();
        else $('#warning-text').show();
    });

    $('#changePasswodBtn').click(function () {
        var newPass = $('#newPass').val();
        var confirmNewPass = $('#confirmNewPass').val();
        var username = getParameterByName('username');
        var token = getParameterByName('token');
        var mode = getParameterByName('mode');
        var reff = getParameterByName('reff');
        var postdata = { NewPassword: newPass, ConfirmPassword: confirmNewPass, UserName: username, Token: token, Mode: mode, Reff: reff }

        if (newPass != confirmNewPass) {
            alert("Password Confirmation Not Match");
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/ChangePassword/DoChangePassword',
                data: JSON.stringify(postdata),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                success: function (data) {
                    window.location.href = '/login';
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
    });

    const getParameterByName = (name, url = window.location.href) => {
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }
});