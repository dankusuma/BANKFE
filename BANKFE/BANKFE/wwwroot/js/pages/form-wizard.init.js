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
    const constraintsFoto = { audio: false, video: true };

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

    async function blobToBase64() {
        let blob = new Blob(recordedBlobs, { type: 'video/mp4' });
        return new Promise((resolve, _) => {
            const reader = new FileReader();
            reader.onloadend = () => resolve(reader.result);
            reader.readAsDataURL(blob);
        });
    }

    const submitButton = document.querySelector('#submit-form');
    submitButton.onclick = async function () {
        let videoBase64 = await blobToBase64();
        let selfieBase64 = $('#canvas-foto')[0].toDataURL("image/jpeg");
        let videoString = videoBase64.split(',')[1];
        let fotoString = selfieBase64.split(',')[1];
      
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
            Id: 0,
            Nik: nik,
            Nama: name,
            TempatLahir: tmpLahir,
            TanggalLahir: tglLahir,
            NamaIbuKandung: ibuKandung,
            Alamat: alamat,
            Kelurahan: kelurahan,
            Kecamatan: kecamatan,
            Kabupaten: kabupaten,
            Provinsi: provinsi,
            Kelamin: kelamin,
            StatusPernikahan: marital,
            Pekerjaan: pekerjaan,
            Email: email,
            Username: username,
            Password: password,
            Telepon: handphone
        };

        var postUpload = {
            photoName: nik + "_" + name,
            stringPhoto: fotoString,
            videoName: nik + "_" + name,
            stringVideo: videoString
        };

        $.ajax({
            type: 'POST',
            url: '/Registration/SubmitRegistration',
            data: JSON.stringify(postData),
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            success: function (data) {
                console.log("Success");
            },
            error: function (data) {
                alert(data.responseText);
            }
        });

        $.ajax({
            type: 'POST',
            url: '/Registration/SubmitUpload',
            data: JSON.stringify(postUpload),
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            success: function (data) {
                console.log("Success");
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
});