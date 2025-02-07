﻿$(document).ready(function () {
    $("#form1").validate({
        rules: {
            nik: {
                required: true,
                number: true,
                minlength: 16,
                maxlength: 16
            },
            name: {
                required: true,
                maxlength: 100,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            lahir: {
                required: true,
                maxlength: 30,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            tanggal: {
                required: true,
                date: true
            },
            ibu: {
                required: true,
                maxlength: 100,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            alamat: {
                required: true,
                maxlength: 200,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            kel: {
                required: true,
                maxlength: 50,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            kec: {
                required: true,
                maxlength: 50,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            kab: {
                required: true,
                maxlength: 50,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            prov: {
                required: true,
                maxlength: 50,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            gender: {
                required: true
            },
            marital: {
                required: true
            },
            job: {
                required: true
            }
        },
        messages: {
            nik: {
                number: "Tolong input NIK dengan benar",
                minlength: "NIK harus 16 karakter",
                maxlength: "NIK harus 16 karakter",
            },
            name: {
                maxlength: "Maksimal 30 karakter",
                normalizer: "Tolong input nama"
            },
            lahir: {
                maxlength: "Maksimal 30 karakter",
                normalizer: "Tolong input tempat lahir yang benar"
            },
            tanggal: "Tolong input Tanggal Lahir",
            ibu: {
                maxlength: "Maksimal 100 karakter",
                normalizer: "Tolong input nama ibu yang benar"
            },
            alamat: {
                maxlength: "Maksimal 200 karakter",
                normalizer: "Tolong input alamat yang benar"
            },
            kel: {
                maxlength: "Maksimal 50 karakter",
                normalizer: "Tolong input kelurahan yang benar"
            },
            kec: {
                maxlength: "Maksimal 50 karakter",
                normalizer: "Tolong input kecamatan yang benar"
            },
            kab: {
                maxlength: "Maksimal 50 karakter",
                normalizer: "Tolong input Kabupaten/Kota yang benar"
            },
            prov: {
                maxlength: "Maksimal 50 karakter",
                normalizer: "Tolong input provinsi yang benar"
            },
            gender: "Tolong pilih Jenis Kelamin",
            marital: "Tolong pilih Status Pernikahan",
            job: "Tolong pilih Pekerjaan"
        },
        highlight: function (element) {
            $(element).removeClass('is-valid').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid').addClass('is-valid');
        }
    });

    $("#form2").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            username: {
                required: true,
                minlength: 8,
                maxlength: 20,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            password: {
                required: true,
                minlength: 8,
                maxlength: 50,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            retype: {
                required: true,
                equalTo: '[name="password"]',
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            handphone: {
                required: true,
                number: true
            }
        },
        messages: {
            email: {
                email: "Format Email salah"
            },
            username: {
                minlength: "Username minimal 8 karakter",
                maxlength: "Username maximal 20 karakter",
                normalizer: "Tolong input nama yang benar"
            },
            password: {
                minlength: "Password minimal 8 karakter",
                maxlength: "Password maximal 50 karakter",
                normalizer: "Tolong input password yang benar"
            },
            retype: {
                equalTo: "Password tidak sama"
            },
            handphone: {
                number: "Tolong input No Handphone dengan benar"
            }
        },
        highlight: function (element) {
            $(element).removeClass('is-valid').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid').addClass('is-valid');
        }
    })

    $("#basic-pills-wizard").bootstrapWizard({
        tabClass: "nav nav-pills nav-justified",
        onTabShow: function (tab, navigation, index) {
            var $total = navigation.find('li').length;
            var $current = index + 1;
            if ($current >= $total) {
                $('#basic-pills-wizard').find('.pager .next').hide();
                $('#basic-pills-wizard').find('.pager .finish').show();
            } else {
                $('#basic-pills-wizard').find('.pager .next').show();
                $('#basic-pills-wizard').find('.pager .finish').hide();
            }
        },
        onNext: function (tab, navigation, index) {
            if (index == 1) {
                var form1Valid = $("#form1").valid();
                if (!form1Valid) {
                    return false;
                }
            } else if (index == 2) {
                var form2Valid = $("#form2").valid();
                if (!form2Valid) {
                    return false;
                }
            } else if (index == 3) {
                $('#canvas-foto')[0].toBlob(function (blob) {
                    console.log(blob.size / 1024);
                    if (blob.size > 2097152) {
                        alert("Selfie + KTP > 2Mb!!!");
                        return false;
                    }
                });
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
        let stream = frameFoto.srcObject;
        let tracks = stream.getTracks();
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

    playRecord.onclick = () => {
        const superBuffer = new Blob(recordedBlobs, { type: mimeVideo });
        frameRecorded.src = null;
        frameRecorded.srcObject = null;
        frameRecorded.src = window.URL.createObjectURL(superBuffer);
        frameRecorded.controls = true;
        frameRecorded.play();
        console.log(frameRecorded.src);
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
        let blob = new Blob(recordedBlobs, { type: 'video/mp4' });
        if (blob.size > 15728640) {
            alert("Selfie + KTP > 2Mb!!!");
        } else {
            let videoBase64 = await blobToBase64();
            let selfieBase64 = $('#canvas-foto')[0].toDataURL("image/jpeg");
            let videoString = videoBase64.split(',')[1];
            let fotoString = selfieBase64.split(',')[1];

            var nik = $('#nik').val();
            var name = $('#name').val();
            var tmpLahir = $('#lahir').val();
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
                PHONE: handphone
            };

            var postUpload = {
                photoName: nik + "_" + name + Date.now(),
                stringPhoto: fotoString,
                videoName: nik + "_" + name + Date.now(),
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
    }
});