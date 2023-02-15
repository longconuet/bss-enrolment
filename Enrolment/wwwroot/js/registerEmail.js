$(document).ready(function () {
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

function registerEmail() {
    if ($("#register-email-form").valid()) {
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
            statusCode: {
                400: function (responseObject, textStatus, jqXHR) {
                    toastr.error(responseObject, "Bad request");
                }
            },
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

                //window.location = url;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}