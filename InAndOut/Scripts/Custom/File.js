$(document).ready(function () {
    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
			label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [label]);
    });

    $('.btn-file :file').on('fileselect', function (event, label) {

        var input = $(this).parents('.input-group').find(':text'),
            log = label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }

    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#img-upload').attr('src', e.target.result);
                //sentDataUrl(e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#imgInp").change(function () {
        readURL(this);
    });

    $("#imgEmpDetails").change(function () {
        readDetailsURL(this);
    });

    function readDetailsURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#user-pic').attr('src', e.target.result);
                //sentDataUrl(e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    function sentDataUrl(url) {
        if (url != "/Image/dummy.jpg") {
            var base64url = url.split(',')[1];
            var myurl = '/File/LoadImage';
            var data = { dataUrl: base64url };
            XHRPOSTRequest(myurl, data, function (result) {

            });
        }
    }
});