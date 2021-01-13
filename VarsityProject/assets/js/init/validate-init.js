"use strict";
$(document).ready(function () {

    jQuery(".xp-form-validate").validate({
        ignore: [],
        errorClass: "invalid-feedback animated fadeInDown",
        errorElement: "div",
        errorPlacement: function (e, a) {
            jQuery(a).parents(".form-group > div").append(e)
        },
        highlight: function (e) {
            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
        },
        success: function (e) {
            jQuery(e).closest(".form-group").removeClass("is-invalid"), jQuery(e).remove()
        },
        rules: {
            "val-Name": {
                required: !0,
                minlength: 3
            },
            "val-Surname": {
                required: !0,
                minlength: 3
            },
            "val-username": {
                required: !0,
                minlength: 3
            },
            "val-default-date":
            {
                date: true,
                required: !0
            },
            "val-title": {
                required: !0,
                minlength: 3
            },
            "val-email": {
                required: !0,
                email: !0
            },
            "val-employee-number": {
                required: !0,
                minlength: 5
            },
            "val-student-number": {
                required: !0,
                minlength: 5
            },
            "val-password": {
                required: !0,
                minlength: 5
            },
            "val-confirm-password": {
                required: !0,
                equalTo: "#val-password"
            },
            "val-select2": {
                required: !0
            },
            "val-select2-multiple": {
                required: !0,
                minlength: 2
            },
            "val-course-code": {
                required: !0
            },
            "facultyDropdown": {
                required: !0
            },
            "val-departments": {
                required: !0
            },
            "val-digits": {
                required: !0,
                digits: !0
            },
            "val-duration": {
                required: !0,
                digits: !0,
                range: [1, 4]
            },
            "val-cellphone": {
                required: !0
            },
            "val-range": {
                required: !0,
                range: [1, 5]
            },
            "val-courses[]": {
                required: !0
            },
            "val-subject[]": {
                required: !0
            },
            "val-gender": {
                required: !0
            },
            "val-subject-code": {
                required: !0
            }
        },
        messages: {
            "val-subject-code": {
                required: "Please enter the code of the subject"
            },
            "val-Name": {
                required: "Please enter name",
                minlength: "Your name must consist of at least 3 characters"
            },
            "val-Surname": {
                required: "Please enter surname",
                minlength: "Surname must consist of at least 3 characters"
            },
            "val-username": {
                required: "Please enter a username",
                minlength: "Username must consist of at least 3 characters"
            },
            "val-default-date":
            {
                date: "valid date required",
                required: "Please select date"
            },
            "val-title": {
                required: "Please enter a title",
                minlength: "Title must consist of at least 3 characters"
            },
            "val-email": "Please enter a valid email address",
            "val-employee-number": {
                required: "Please provide an employee number",
                minlength: "Employee must be at least 5 characters long"
            },
            "val-password": {
                required: "Please provide a password",
                minlength: "Password must be at least 5 characters long"
            },
            "val-confirm-password": {
                required: "Please provide a password",
                minlength: "Password must be at least 5 characters long",
                equalTo: "Please enter the same password as above"
            },
            "val-select2": "Please select a value!",
            "val-select2-multiple": "Please select at least 2 values!",
            "facultyDropdown": "Please select a faculty!",
            "val-course-code": "Please enter the course code!",
            "val-departments": "Please select a department!",
            "val-subject[]": "Please select subject(s)!",
            "val-courses[]": "Please select course(s)!",
            "val-digits": "Please enter only digits!",
            "val-duration": "Please enter a valid course duration!",
            "val-cellphone": "Please enter a cellphone!",
            "val-range": "Please enter a number between 1 and 5!",
            "student-number": "Please provide a student number!",
            "val-gender": "Please choose a gender type"
        }
    })

});