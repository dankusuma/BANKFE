$(document).ready(() => {

    $('#changePasswodBtn').click(function () {
        var newPass = $('#newPass').val();
        var confirmNewPass = $('#confirmNewPass').val();
        var username = getParameterByName('username');
        var token = getParameterByName('token');
        var postdata = { NewPassword: newPass, ConfirmPassword: confirmNewPass, UserName: username, Token: token }

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