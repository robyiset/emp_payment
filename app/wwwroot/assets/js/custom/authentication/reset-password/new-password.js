"use strict";
var KTAuthNewPassword = (function () {
    var t;
    var e;
    var r;
    var o;
    var n = function () {
        return o.getScore() > 50;
    };

    return {
        init: function () {
            t = document.querySelector("#kt_new_password_form");
            e = document.querySelector("#kt_new_password_submit");
            o = KTPasswordMeter.getInstance(
                t.querySelector('[data-kt-password-meter="true"]')
            );
            r = FormValidation.formValidation(t, {
                fields: {
                    password: {
                        validators: {
                            notEmpty: { message: "The password is required" },
                            callback: {
                                message: "Please enter a valid password",
                                callback: function (t) {
                                    if (t.value.length > 0) return n();
                                },
                            },
                        },
                    },
                    "confirm-password": {
                        validators: {
                            notEmpty: { message: "The password confirmation is required" },
                            identical: {
                                compare: function () {
                                    return t.querySelector('[name="password"]').value;
                                },
                                message: "The password and its confirm are not the same",
                            },
                        },
                    },
                    toc: {
                        validators: {
                            notEmpty: {
                                message: "You must accept the terms and conditions",
                            },
                        },
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: { password: !1 },
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: "",
                    }),
                },
            });

            t.querySelector('input[name="password"]').addEventListener("input", function () {
                if (this.value.length > 0) r.updateFieldStatus("password", "NotValidated");
            });

            if (!(function (t) {
                try {
                    return new URL(t), !0;
                } catch (t) {
                    return !1;
                }
            })(t.getAttribute("action"))) {
                e.addEventListener("click", function (n) {
                    n.preventDefault();
                    r.revalidateField("password");
                    r.validate().then(function (r) {
                        if ("Valid" == r) {
                            e.setAttribute("data-kt-indicator", "on");
                            e.disabled = !0;
                            setTimeout(function () {
                                e.removeAttribute("data-kt-indicator");
                                e.disabled = !1;
                                Swal.fire({
                                    text: "You have successfully reset your password!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: { confirmButton: "btn btn-primary" },
                                }).then(function (e) {
                                    if (e.isConfirmed) {
                                        t.querySelector('[name="password"]').value = "";
                                        t.querySelector('[name="confirm-password"]').value = "";
                                        o.reset();
                                        var redirectUrl = t.getAttribute("data-kt-redirect-url");
                                        if (redirectUrl) location.href = redirectUrl;
                                    }
                                });
                            }, 1500);
                        } else {
                            Swal.fire({
                                text: "Sorry, looks like there are some errors detected, please try again.",
                                icon: "error",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: { confirmButton: "btn btn-primary" },
                            });
                        }
                    });
                });
            } else {
                e.addEventListener("click", function (o) {
                    o.preventDefault();
                    r.revalidateField("password");
                    r.validate().then(function (r) {
                        if ("Valid" == r) {
                            e.setAttribute("data-kt-indicator", "on");
                            e.disabled = !0;
                            axios
                                .post(e.closest("form").getAttribute("action"), new FormData(t))
                                .then(function (e) {
                                    if (e) {
                                        t.reset();
                                        const redirectUrl = t.getAttribute("data-kt-redirect-url");
                                        if (redirectUrl) location.href = redirectUrl;
                                    } else {
                                        Swal.fire({
                                            text: "Sorry, the email is incorrect, please try again.",
                                            icon: "error",
                                            buttonsStyling: !1,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: { confirmButton: "btn btn-primary" },
                                        });
                                    }
                                })
                                .catch(function (t) {
                                    Swal.fire({
                                        text: "Sorry, looks like there are some errors detected, please try again.",
                                        icon: "error",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: { confirmButton: "btn btn-primary" },
                                    });
                                })
                                .then(() => {
                                    e.removeAttribute("data-kt-indicator");
                                    e.disabled = !1;
                                });
                        } else {
                            Swal.fire({
                                text: "Sorry, looks like there are some errors detected, please try again.",
                                icon: "error",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: { confirmButton: "btn btn-primary" },
                            });
                        }
                    });
                });
            }
        },
    };
})();

KTUtil.onDOMContentLoaded(function () {
    KTAuthNewPassword.init();
});
