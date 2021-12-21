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

    //Take Picture
    const constraintsFoto= { audio: false, video: true };
    const frameFoto = document.querySelector('#frame-foto');
    const canvasFoto = window.canvas = document.querySelector('#canvas-foto');
    const startPic = document.querySelector('#start-foto');
    const takePic = document.querySelector('#take-foto');
    canvasFoto.width = 480;
    canvasFoto.height = 360;

    function handleSuccess(stream) {
        window.stream = stream;
        frameFoto.srcObject = stream;
    }

    function handleError(error) {
        console.log('navigator.MediaDevices.getUserMedia error: ', error.message, error.name);
    }

    function stopCamera() {
        stream = frameFoto.srcObject;
        tracks = stream.getTracks();
        tracks.forEach(function (track) {
            track.stop();
        });
        frameFoto.srcObject = null;
    }

    startPic.onclick = function () {
        navigator.mediaDevices.getUserMedia(constraintsFoto).then(handleSuccess).catch(handleError);
        $("#start-foto").css({ display: "none" });
        $('#take-foto').css({ display: "block" });
    }

    takePic.onclick = function () {
        canvasFoto.width = frameFoto.videoWidth;
        canvasFoto.height = frameFoto.videoHeight;
        canvasFoto.getContext('2d').drawImage(frameFoto, 0, 0, canvasFoto.width, canvasFoto.height);
        $("#frame-foto").css({ display: "none" });
        $("#canvas-foto").css({ display: "block" });
        stopCamera();
    };

    //Record Video
    let mediaRecorder;
    let recordedBlobs;
    const mimeVideo = 'video/webm;codecs=vp9,opus'.split(';', 1)[0];
    const frameVideo = document.querySelector('#frame-video');
    const frameRecorded = document.querySelector('#frame-recorded');
    const startVideo = document.querySelector('#start-video');
    const startRecord = document.querySelector('#start-record');
    const stopRecord = document.querySelector('#stop-record');
    const playRecord = document.querySelector('#play-record');

    function handleDataAvailable(event) {
        if (event.data && event.data.size > 0) {
            recordedBlobs.push(event.data);
        }
    }

    startRecord.onclick = function startRecording() {
        recordedBlobs = [];
        const mimeType = 'video/webm;codecs=vp9,opus';
        const options = { mimeType };

        try {
            $("#start-record").css({ display: "none" });
            $("#stop-record").css({ display: "block" });
            mediaRecorder = new MediaRecorder(window.stream, options);
        } catch (e) {
            console.error('Exception while creating MediaRecorder:', e);
            errorMsgElement.innerHTML = `Exception while creating MediaRecorder: ${JSON.stringify(e)}`;
            return;
        }

        mediaRecorder.ondataavailable = handleDataAvailable;
        mediaRecorder.start();
    }

    stopRecord.onclick = function stopRecording() {
        mediaRecorder.stop();
        $("#frame-video").css({ display: "none" });
        $("#frame-recorded").css({ display: "block" });
        $("#stop-record").css({ display: "none" });
        $("#play-record").css({ display: "block" });

        stream = frameVideo.srcObject;
        tracks = stream.getTracks();
        tracks.forEach(function (track) {
            track.stop();
        });
        frameVideo.srcObject = null;
    }

    playRecord.onclick = function playRecord() {
        const superBuffer = new Blob(recordedBlobs, { type: mimeVideo });
        frameRecorded.src = null;
        frameRecorded.srcObject = null;
        frameRecorded.src = window.URL.createObjectURL(superBuffer);
        frameRecorded.controls = true;
        frameRecorded.play();
    }

    function handleSuccessVideo(stream) {
        $("#start-video").css({ display: "none" });
        $("#start-record").css({ display: "block" });
        window.stream = stream;
        frameVideo.srcObject = stream;
    }

    async function initVideo(constraints) {
        try {
            const stream = await navigator.mediaDevices.getUserMedia(constraints);
            handleSuccessVideo(stream);
        } catch (e) {
            console.error('navigator.getUserMedia error:', e);
            errorMsgElement.innerHTML = `navigator.getUserMedia error:${e.toString()}`;
        }
    }

    startVideo.onclick = async function () {
        const constraints = {
            audio: {
                echoCancellation: { exact: false }
            },
            video: {
                width: 1280, height: 720
            }
        };
        await initVideo(constraints);
    }

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
        var selfieId = $('#canvas-foto').val();
        var videoPernyataan = new Blob(recordedBlobs, { type: mimeVideo });

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