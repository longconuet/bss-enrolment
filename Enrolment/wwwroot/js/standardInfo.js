var inputChangeFlag = false;
var showSideBar = false;

function checkInputChange() {
    if (inputChangeFlag) {
        if (confirm("Discard changed") == true) {
            alert("Discard");
        } else {
            alert("continue");
        }
    }
}

$(document).ready(function () {
    
    //$('input').change(function () {
    //    if (confirm("Discard changed") == true) {
    //        alert("Discard");
    //    } else {
    //        alert("continue");
    //    }

    //});

    getPayeeStep();
});

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

        businessNumber: {
            required: true
        },
        appliedForNumber: {
            required: true
        },
        branchNumber: {
            required: true
        },
        legalName: {
            required: true
        },
        businessAddress: {
            required: true
        },
        suburbPayer: {
            required: true
        },
        statePayer: {
            required: true
        },
        postCodePayer: {
            required: true
        },
        emailPayer: {
            required: true,
            email: true
        },
        contactPerson: {
            required: true
        },
        businessPhone: {
            required: true
        },

        superannuationFund: {
            required: true
        },
        employeeName: {
            required: true
        },
        identificationNumber: {
            required: true
        },
        employeeTaxFileNumber: {
            required: true
        },
        employeeFundName: {
            required: true
        },
        employeeFundAddress: {
            required: true
        },
        employeeSuburb: {
            required: true
        },
        employeeState: {
            required: true
        },
        employeePostCode: {
            required: true
        },
        accountName: {
            required: true
        },
        daytimePhoneNumber: {
            required: true
        },

        employerBusinessName: {
            required: true
        },
        employerBusinessNumber: {
            required: true
        },
        employerFundName: {
            required: true
        },
        employerFundWebsite: {
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

var payeeValidator = $("#payeeForm").validate({
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
        }
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
    },

    errorPlacement: function (error, element) {
        if (element.attr("type") == "radio") {
            error.insertAfter("#error-" + element.attr('name'));
        } else {
            error.insertAfter(element);
        }
    },

});

var employeevalidator = $("#employeeForm").validate({
    rules: {
        superannuationFund: {
            required: true
        },
        employeeName: {
            required: true
        },
        identificationNumber: {
            required: true
        },
        employeeTaxFileNumber: {
            required: true
        },
        employeeFundName: {
            required: true
        },
        employeeFundAddress: {
            required: true
        },
        employeeSuburb: {
            required: true
        },
        employeeState: {
            required: true
        },
        employeePostCode: {
            required: true
        },
        accountName: {
            required: true
        },
        daytimePhoneNumber: {
            required: true
        }
    },
    errorPlacement: function (error, element) {
        if (element.attr("type") == "radio") {
            error.insertAfter("#error-" + element.attr('name'));
        } else {
            error.insertAfter(element);
        }
    },
});

var uploadValidator = $("#identityProofForm").validate({
    rules: {
        imageFile: {
            required: true,
            extension: "png|jpg|jpeg"
        }
    }
});

function uploadFile() {
    var files = $('#fileUpload')[0].files; //get files
    var formData = new FormData(); //create form

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]); //loop through all files and append
    }

    $.ajax(
        {
            url: "/Home/AjaxUpload",
            data: formData, // send it as formData
            processData: false,
            contentType: false,
            type: "POST", //type is post as we will need to post files
            success: function (result) {
                if (result.status == 0) {
                    toastr.error(result.message, "Error");
                    return false;
                }
                else {
                    $('#fileUploadPath').val(result.data);
                }
            }
        }
    );
}

function getDetail() {
    $.ajax({
        url: "/Home/StandardInfoDetail/" + $('#email-register').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.status == 0) {
                toastr.error(result.message, "Error");
                //window.location = "/Home/RegisterEmail";
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

            if (result.data.payee != null) {
                var payer = result.data.payer;
                $('#businessNumber').val(payer.businessNumber);
                $("input[name=appliedForNumber][value=" + payer.appliedForNumber + "]").prop('checked', true);
                $('#branchNumber').val(payer.branchNumber);
                $('#legalName').val(payer.legalName);
                $('#businessAddress').val(payer.businessAddress);
                $('#suburbPayer').val(payer.suburb);
                $('#statePayer').val(payer.state);
                $('#postCodePayer').val(payer.postCode);
                $('#emailPayer').val(payer.email);
                $('#contactPerson').val(payer.contactPerson);
                $('#businessPhone').val(payer.businessPhone);
                if (payer.makePayment == 1) {
                    $('#makePayment').prop('checked', true);
                }
            }

            if (result.data.employee != null) {
                var employee = result.data.employee;
                $("input[name=superannuationFund][value=" + employee.superannuationFund + "]").prop('checked', true);
                $('#employeeName').val(employee.name);
                $('#identificationNumber').val(employee.identificationNumber);
                $('#employeeTaxFileNumber').val(employee.taxFileNumber);
                $('#employeeFundName').val(employee.fundName);
                $('#employeeFundAddress').val(employee.fundAddress);
                $('#employeeSuburb').val(employee.suburb);
                $('#employeeState').val(employee.state);
                $('#employeePostCode').val(employee.postCode);
                $('#memberNo').val(employee.memberNo);
                $('#accountName').val(employee.accountName);
                $('#employeeBusinessNumber').val(employee.businessNumber);
                $('#employeeSuperannuationProductIdentificationNumber').val(employee.superannuationProductIdentificationNumber);
                $('#daytimePhoneNumber').val(employee.daytimePhoneNumber);
                if (employee.haveAttached == 1) {
                    $('#haveAttached').prop('checked', true);
                }
            }

            if (result.data.employer != null) {
                var employer = result.data.employer;
                $('#employerBusinessName').val(employer.businessName);
                $('#employerBusinessNumber').val(employer.businessNumber);
                $('#employerFundName').val(employer.fundName);
                $('#employerSuperannuationProductIdentificationNumber').val(employer.superannuationProductIdentificationNumber);
                $('#employerFundPhone').val(employer.fundPhone);
                $('#employerFundWebsite').val(employer.fundWebsite);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
        }
    });
    return false;
}

function hideSideBar() {
    if (showSideBar) {
        $('#openSidebarMenu').click();
        showSideBar = false;
    }
    clickSidebar();
}

function clickSidebar() {
    showSideBar = !showSideBar;
}

function getPayeeStep(sideBarClick = false) {
    //if ($('#kt_header_menu_mobile_toggle').hasClass('active')) {
    //    $('#kt_header_menu_mobile_toggle').click();
    //}

    if (sideBarClick) {
        hideSideBar();
    }
    
    console.log(inputChangeFlag);
    
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

    setFormChange();

    // load data
    getPayeeDetail();

    inputChangeFlag = false;
}

function setFormChange() {
    $('#content-side input, #content-side select').change(function (e) {
        console.log(e);
        inputChangeFlag = true;
    });
}

function getPayeeDetail() {
    $.LoadingOverlay("show");
    activeStepper('payee');
    scollToTop();

    $.ajax({
        url: "/Home/PayeeDetail/" + $('#email-register').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.status == 0) {
                toastr.error(result.message, "Error");
                //window.location = "/Home/RegisterEmail";
                return false;
            }

            if (result.data != null) {
                var payee = result.data;
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
                $('#fileUploadPath').val(payee.filePath);

                $('#dayOfBirth').val(payee.dayOfBirth);
                $('#monthOfBirth').val(payee.monthOfBirth);
                $('#yearOfBirth').val(payee.yearOfBirth);

                $("input[name=nameTitle][value=" + payee.nameTitle + "]").prop('checked', true);
                $("input[name=paidBasis][value=" + payee.paidBasis + "]").prop('checked', true);
                $("input[name=residencyStatus][value=" + payee.residencyStatus + "]").prop('checked', true);
                $("input[name=claimTaxFree][value=" + payee.claimTaxFree + "]").prop('checked', true);
                $("input[name=haveLoanProgram][value=" + payee.haveLoanProgram + "]").prop('checked', true);
            }

            $.LoadingOverlay("hide");
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
            $.LoadingOverlay("hide");
        }
    });
    
    return false;
}

function activeStepper(item) {
    // content
    $('.step-content').removeClass('current');
    $('#step-content-' + item).addClass('current');

    // icon
    $('.stepper-icon').removeClass('stepper-icon-active');
    $('#stepper-icon-' + item).addClass('stepper-icon-active');

    // title
    $('.stepper-title').removeClass('stepper-title-active');
    $('#stepper-title-' + item).addClass('stepper-title-active');
}

function getEmployeeStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }

    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

    setFormChange();

    // load data
    getEmployeeDetail();

    inputChangeFlag = false;
}

function getEmployeeDetail() {
    $.LoadingOverlay("show");
    scollToTop();
    activeStepper('employee');

    $.ajax({
        url: "/Home/EmployeeDetail/" + $('#email-register').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.status == 0) {
                toastr.error(result.message, "Error");
                window.location = "/Home/RegisterEmail";
                return false;
            }

            if (result.data != null) {
                var employee = result.data;
                $("input[name=superannuationFund][value=" + employee.superannuationFund + "]").prop('checked', true);
                $('#employeeName').val(employee.name);
                $('#identificationNumber').val(employee.identificationNumber);
                $('#employeeTaxFileNumber').val(employee.taxFileNumber);
                $('#employeeFundName').val(employee.fundName);
                $('#employeeFundAddress').val(employee.fundAddress);
                $('#employeeSuburb').val(employee.suburb);
                $('#employeeState').val(employee.state);
                $('#employeePostCode').val(employee.postCode);
                $('#memberNo').val(employee.memberNo);
                $('#accountName').val(employee.accountName);
                $('#employeeBusinessNumber').val(employee.businessNumber);
                $('#employeeSuperannuationProductIdentificationNumber').val(employee.superannuationProductIdentificationNumber);
                $('#daytimePhoneNumber').val(employee.daytimePhoneNumber);
                if (employee.haveAttached == 1) {
                    $('#haveAttached').prop('checked', true);
                }
            }

            $.LoadingOverlay("hide");
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
            $.LoadingOverlay("hide");
        }
    });
    return false;
}

function submit() {
    if ($("#form-info").valid()) {        
        var obj = {
            EmailRegister: $('#email').val(),
            FilePath: $('#fileUploadPath').val(),
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
            },
            Payer: {
                BusinessNumber: $('#businessNumber').val(),
                BranchNumber: $('#branchNumber').val(),
                AppliedForNumber: parseInt($("input[name='appliedForNumber']:checked").val()),
                LegalName: $('#legalName').val(),
                BusinessAddress: $('#businessAddress').val(),
                Suburb: $('#suburbPayer').val(),
                State: $('#statePayer').val(),
                PostCode: $('#postCodePayer').val(),
                Email: $('#emailPayer').val(),
                ContactPerson: $('#contactPerson').val(),
                BusinessPhone: $('#businessPhone').val(),
                MakePayment: $('#makePayment').prop('checked') ? 1 : 0,
            },
            Employee: {
                SuperannuationFund: parseInt($("input[name='superannuationFund']:checked").val()),
                Name: $('#employeeName').val(),
                IdentificationNumber: $('#identificationNumber').val(),
                TaxFileNumber: $('#employeeTaxFileNumber').val(),
                FundName: $('#employeeFundName').val(),
                FundAddress: $('#employeeFundAddress').val(),
                Suburb: $('#employeeSuburb').val(),
                State: $('#employeeState').val(),
                PostCode: $('#employeePostCode').val(),
                MemberNo: $('#memberNo').val(),
                AccountName: $('#accountName').val(),
                BusinessNumber: $('#employeeBusinessNumber').val(),
                SuperannuationProductIdentificationNumber: $('#employeeSuperannuationProductIdentificationNumber').val(),
                DaytimePhoneNumber: $('#daytimePhoneNumber').val(),
                HaveAttached: $('#haveAttached').prop('checked') ? 1 : 0,
            },
            Employer: {
                BusinessName: $('#employerBusinessName').val(),
                BusinessNumber: $('#employerBusinessNumber').val(),
                FundName: $('#employerFundName').val(),
                SuperannuationProductIdentificationNumber: $('#employerSuperannuationProductIdentificationNumber').val(),
                FundPhone: $('#employerFundPhone').val(),
                FundWebsite: $('#employerFundWebsite').val(),
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

function submitPayee() {
    if ($("#payeeForm").valid()) {
        $.LoadingOverlay("show");

        var obj = {
            EmailRegister: $('#email-register').val(),
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
            HaveLoanProgram: parseInt($("input[name='haveLoanProgram']:checked").val()),
            FilePath: $('#fileUploadPath').val()
        };

        $.ajax({
            url: "/Home/SubmitPayeeInfo",
            data: JSON.stringify(obj),
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

                toastr.success(result.message, 'Success');
                $.LoadingOverlay("hide");
                getEmployeeDetail();
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText, "Error occurred");
                $.LoadingOverlay("hide");
            }
        });
    }
}

function submitEmployee() {
    if ($("#employeeForm").valid()) {
        $.LoadingOverlay("show");

        var obj = {
            EmailRegister: $('#email-register').val(),
            SuperannuationFund: parseInt($("input[name='superannuationFund']:checked").val()),
            Name: $('#employeeName').val(),
            IdentificationNumber: $('#identificationNumber').val(),
            TaxFileNumber: $('#employeeTaxFileNumber').val(),
            FundName: $('#employeeFundName').val(),
            FundAddress: $('#employeeFundAddress').val(),
            Suburb: $('#employeeSuburb').val(),
            State: $('#employeeState').val(),
            PostCode: $('#employeePostCode').val(),
            MemberNo: $('#memberNo').val(),
            AccountName: $('#accountName').val(),
            BusinessNumber: $('#employeeBusinessNumber').val(),
            SuperannuationProductIdentificationNumber: $('#employeeSuperannuationProductIdentificationNumber').val(),
            DaytimePhoneNumber: $('#daytimePhoneNumber').val(),
            HaveAttached: $('#haveAttached').prop('checked') ? 1 : 0,
        };

        $.ajax({
            url: "/Home/SubmitEmployeeInfo",
            data: JSON.stringify(obj),
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

                toastr.success(result.message, 'Success');
                //window.location = "/Home/Complete";
                $.LoadingOverlay("hide");
                getIdentityProofImage();
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText, "Error occurred");
                $.LoadingOverlay("hide");
            }
        });
    }
}

function getIdentityProofImageStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }

    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

    setFormChange();

    // load data
    getIdentityProofImage();

    inputChangeFlag = false;
}

function getIdentityProofImage() {
    $.LoadingOverlay("show");
    scollToTop();
    activeStepper('identity-proof');

    $.ajax({
        url: "/Home/IdentityProofImage/" + $('#email-register').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.status == 0) {
                toastr.error(result.message, "Error");
                window.location = "/Home/RegisterEmail";
                return false;
            }

            if (result.data != null) {
                $('#imageFile').attr('src', result.data);
                $('#frame').attr('src', result.data);
            }

            $.LoadingOverlay("hide");
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
            $.LoadingOverlay("hide");
        }
    });
    return false;
}

function submitIdentityProofImage() {
    if ($("#identityProofForm").valid()) {
        $.LoadingOverlay("show");

        var files = $('#imageFile')[0].files; //get files
        var formData = new FormData(); //create form

        formData.append("imageFile", files[0]);
        //formData.append("emailRegister", $('#email-register').val());

        $.ajax({
            url: "/Home/SubmitIdentityProofImage/" + $('#email-register').val(),
            data: formData,
            type: "POST",
            contentType: false,
            async: true,
            processData: false,
            success: function (result) {
                if (result.status == 0) {
                    toastr.error(result.message, "Error");
                    return false;
                }

                toastr.success(result.message, 'Success');
                $.LoadingOverlay("hide");
                window.location = "/Home/Complete";
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText, "Error occurred");
                $.LoadingOverlay("hide");
            }
        });
    }
}

function preview() {
    frame.src = URL.createObjectURL(event.target.files[0]);
}

function clearImage() {
    document.getElementById('imageFile').value = null;
    frame.src = "";
}

function scollToTop() {
    $("html, body").animate({ scrollTop: 0 }, 1000);
    return false;
}