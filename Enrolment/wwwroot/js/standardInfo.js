$(document).ready(function () {
    getDetail();

    var validator = $("#form-info").validate({
        rules: {
            taxFileNumber: {
                required: true
            },
            surname: {
                required: true
            },
            firstName: {
                required: true
            },
            nameTitle: {
                required: true
            },
            homeAddress: {
                required: true
            },
            suburb: {
                required: true
            },
            state: {
                required: true
            },
            postCode: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            dayOfBirth: {
                required: true
            },
            monthOfBirth: {
                required: true
            },
            yearOfBirth: {
                required: true
            },
            paidBasis: {
                required: true
            },
            residencyStatus: {
                required: true
            },
            claimTaxFree: {
                required: true
            },
            haveLoanProgram: {
                required: true
            },
        },

        messages: {
            taxFileNumber: {
                required: "Tax file number cannot be empty"
            },
            surname: {
                required: "Surname cannot be empty"
            },
            firstName: {
                required: "Max student number cannot be empty"
            },
            nameTitle: {
                required: "Please select a name title"
            },
            homeAddress: {
                required: "Home address cannot be empty"
            },
            suburb: {
                required: "Suburb/town/locality cannot be empty"
            },
            state: {
                required: "State/territory cannot be empty"
            },
            postCode: {
                required: "PostCode cannot be empty"
            },
            email: {
                required: "Primary e-mail address cannot be empty",
                email: "Email is invalid"
            },
            dayOfBirth: {
                required: "Choose day of birth"
            },
            monthOfBirth: {
                required: "Choose month of birth"
            },
            yearOfBirth: {
                required: "Choose year of birth"
            },
            paidBasis: {
                required: "Please select one"
            },
            residencyStatus: {
                required: "Please select one"
            },
            claimTaxFree: {
                required: "Please select one"
            },
            haveLoanProgram: {
                required: "Please select one"
            },
        }
    });
});

function getDetail() {
    $.ajax({
        url: "/Home/StandardInfoDetail/" + $('#email-register').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.status == 0) {
                toastr.error(result.message, "Error");
                window.location = "/Home/RegisterEmail";
                return false;
            }

            console.log(result.data);
            if (result.data.payee != null) {
                var payee = result.data.payee;
                $('#taxFileNumber').val(payee.taxFileNumber);
                $('#surname').val(payee.surname);
                $('#firstName').val(payee.firstName);
                $('#otherName').val(payee.otherName);
                $('#homeAddress').val(payee.homeAddress);
                $('#suburb').val(payee.suburb);
                $('#state').val(payee.state);
                $('#postCode').val(payee.postCode);
                $('#previousFamilyName').val(payee.previousFamilyName);
                $('#email').val(payee.email);

                $('#dayOfBirth').val(payee.dayOfBirth);
                $('#monthOfBirth').val(payee.monthOfBirth);
                $('#yearOfBirth').val(payee.yearOfBirth);

                $("input[name=nameTitle][value=" + payee.nameTitle + "]").prop('checked', true);
                $("input[name=paidBasis][value=" + payee.paidBasis + "]").prop('checked', true);
                $("input[name=residencyStatus][value=" + payee.residencyStatus + "]").prop('checked', true);
                $("input[name=claimTaxFree][value=" + payee.claimTaxFree + "]").prop('checked', true);
                $("input[name=haveLoanProgram][value=" + payee.haveLoanProgram + "]").prop('checked', true);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
        }
    });
    return false;
}

function submit() {
    if ($("#form-info").valid()) {
        var obj = {
            EmailRegister: $('#email').val(),
            Payee: {
                TaxFileNumber: $('#taxFileNumber').val(),
                NameTitle: parseInt($("input[name='nameTitle']:checked").val()),
                Surname: $('#surname').val(),
                FirstName: $('#firstName').val(),
                OtherName: $('#otherName').val(),
                HomeAddress: $('#homeAddress').val(),
                Suburb: $('#suburb').val(),
                State: $('#state').val(),
                PostCode: $('#postCode').val(),
                PreviousFamilyName: $('#previousFamilyName').val(),
                Email: $('#email').val(),
                DayOfBirth: parseInt($('#dayOfBirth').val()),
                MonthOfBirth: parseInt($('#monthOfBirth').val()),
                YearOfBirth: parseInt($('#yearOfBirth').val()),
                PaidBasis: parseInt($("input[name='paidBasis']:checked").val()),
                ResidencyStatus: parseInt($("input[name='residencyStatus']:checked").val()),
                ClaimTaxFree: parseInt($("input[name='claimTaxFree']:checked").val()),
                HaveLoanProgram: parseInt($("input[name='haveLoanProgram']:checked").val())
            }
        };

        //console.log(obj);

        $.ajax({
            url: "/Home/SubmitStandardInfo",
            data: JSON.stringify(obj),
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

                toastr.success(result.message, 'Success');
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText, "Error occurred");
            }
        });
    }
    
}