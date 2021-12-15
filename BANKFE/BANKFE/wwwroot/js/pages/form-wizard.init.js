

$(document).ready(function () {
    $("#basic-pills-wizard").bootstrapWizard({
        tabClass: "nav nav-pills nav-justified",
        onTabShow: function (tab, navigation, index) {
            var $total = navigation.find('li').length;
            var $current = index + 1;
            var $percent = ($current / $total) * 100;
            $('#basic-pills-wizard').find('.bar').css({ width: $percent + '%' });
            if ($total == $current) {
                $(".finish").css({ display: "block" });
                $('.next').css({ display: "none" });
            } else {
                $(".finish").css({ display: "none" });
                $('.next').css({ display: "block" });
            }
        }
    });

    $("#submit-form").click(function () {
        var nik = $('#nik').val();
        var name = $('#name').val();
        var tmpLahir = $('#lahir').val();
        var tglLahir = $('#tanggal').val();
        var ibuKandung = $('#ibu').val();
        var tglLahir = $('#tanggal').val();
        var alamat = $('#alamat').val();
        var kelurahan = $('#kel').val();
        var kecamatan = $('#kec').val();
        var kabupaten = $('#kab').val();
        var provinsi = $('#prov').val();
        var kelamin = $('#gender').val();
        var marital = $('#marital').val();
        var pekerjaan = $('#job').val();
        var email = $('#email').val();
        var username = $('#username').val();
        var password = $('#password').val();
        var handphone = $('#handphone').val();

        var postData = {
            ID: 0,
            NIK: nik,
            NAME: name,
            BIRTH_PLACE: tmpLahir,
            BIRTH_DATE: tglLahir,
            MOTHER_MAIDEN_NAME: ibuKandung,
            ADDRESS: alamat,
            KELURAHAN: kelurahan,
            KECAMATAN: kecamatan,
            KABUPATEN_KOTA: kabupaten,
            PROVINCE: provinsi,
            GENDER: kelamin,
            MARITAL_STATUS: marital,
            JOB: pekerjaan,
            EMAIL: email,
            USERNAME: username,
            PASSWORD: password,
            PHONE: handphone,
        };

        $.ajax({
            type: 'POST',
            url: '/Registration/SubmitRegistration',
            data: JSON.stringify(postData),
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            success: function (data) {
                alert('success');
            }, error: function (data) {
                alert('failure');
            }
        });
    });
});