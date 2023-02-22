$(document).ready(function () {
    //KTApp.showPageLoading();
    $("#register-email-form").validate({
        rules: {
            email: {
                required: true,
                email: true
            }
        },

        messages: {
            email: {
                required: "Email can not be empty",
                email: "Invalid email"
            }
        }
    });
});

//$(document).ajaxStart(function () {
//    $.LoadingOverlay("show");
//});
//$(document).ajaxStop(function () {
//    $.LoadingOverlay("hide");
//});

function showSpinner() {
    $('#register-email-submit').addClass('spinner spinner-white spinner-right');
    $('input.form-control').attr('disabled', true);
}

function registerEmail() {
    if ($("#register-email-form").valid()) {
        $('#card-register').LoadingOverlay("show");

        var email = $('#email').val()
        var postData = {
            Email: email
        };

        $.ajax({
            url: "/Home/RegisterEmail",
            data: JSON.stringify(postData),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            processData: false,
            success: function (result) {
                if (result.status == 0) {
                    toastr.error(result.message, "Error");
                    return false;
                }

                if (result.data.isNewEmail) {
                    toastr.success(result.message, "Success");

                    // show verification mail content
                    $('#email-content').html(result.data.htmlContent);
                }
                else {
                    if (result.data.isConfirmed) {
                        // redirect to Detail page
                        window.location = "/Home/StandardInfo/" + email;
                    }
                    else {
                        // show verification mail content
                        $('#email-content').html(result.data.htmlContent);
                    }
                }

                $('#card-register').LoadingOverlay("hide");
                //window.location = url;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $('#card-register').LoadingOverlay("hide");
            }
        });
    }
}