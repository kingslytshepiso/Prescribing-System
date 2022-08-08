// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Code for buttons that execute severe/important funtions in the app
const form_critical = document.getElementsByClassName("form-critical")[0];
if (form_critical) {
    form_critical.addEventListener("submit", function (e) {
        let result = confirm("Are you sure you want to proceed?");
        if (result)
            return true;
        else
            e.preventDefault()

    });
}
