// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Filter cities according to the selected province
var provinces = document.getElementById("slctProvince");
var cities = document.getElementById("slctCities");
var suburbs = document.getElementById("slctSuburbs");
if (typeof srlProvs !== 'undefined') {
    const allProvs = JSON.parse(srlProvs.replace(/&quot;/g, '"'));
    const allCities = JSON.parse(srlCities.replace(/&quot;/g, '"'));
    const allSuburbs = JSON.parse(srlSuburbs.replace(/&quot;/g, '"'));
    if (provinces) {
        provinces.addEventListener("change", function () {
            cities.innerHTML = "";
            for (var i = 0; i < allCities.length; i++) {
                var obj = allCities[i];
                let selectedProvince = provinces.options[provinces.selectedIndex].value;
                if (selectedProvince == obj["ProvID"]) {
                    let newOption = new Option(obj["Name"], obj["CityID"]);
                    cities.add(newOption, undefined);
                }
            }
        })
    }
    if (cities) {
        cities.addEventListener("change", function () {
            suburbs.innerHTML = "";
            for (var i = 0; i < allSuburbs.length; i++) {
                var obj = allSuburbs[i];
                let selectedCity = cities.options[cities.selectedIndex].value;
                if (selectedCity == obj["CityID"]) {
                    let newOption = new Option(obj["Name"], obj["SuburbID"]);
                    suburbs.add(newOption, undefined);
                }
            }
        });
    }
}
function updateCities() {
    cities.innerHTML = "";
    for (var i = 0; i < allCities.length; i++) {
        var obj = allCities[i];
        let selectedProvince = provinces.options[provinces.selectedIndex].value;
        if (selectedProvince == obj["ProvID"]) {
            let newOption = new Option(obj["Name"], obj["CityID"]);
            cities.add(newOption, undefined);
        }
    }
}
function updateSuburbs() {
    suburbs.innerHTML = "";
    for (var i = 0; i < allSuburbs.length; i++) {
        var obj = allSuburbs[i];
        let selectedCity = cities.options[cities.selectedIndex].value;
        if (selectedCity == obj["CityID"]) {
            let newOption = new Option(obj["Name"], obj["SuburbID"]);
            suburbs.add(newOption, undefined);
        }
    }
}

//Code for buttons that execute severe/important funtions in the app
const form_critical = document.getElementsByClassName("form-critical");
const forms_critical_extra = document.getElementsByClassName("form-critical-extra");
const btns_critical_extra = document.getElementsByClassName("btn-critical-extra");
const btns_critical = document.getElementsByClassName("btn-critical");
var messageElement = document.getElementById("messageValue");
for (let i = 0; i < form_critical.length; i++) {
    if (form_critical[i]) {
        form_critical[i].addEventListener("submit", function (e) {
            let result = confirm("Are you sure you want to proceed?");
            if (result) {
                return true;
            }
            else {
                e.preventDefault()
            }
        });
    }
}
for (let i = 0; i < forms_critical_extra.length; i++) {
    if (forms_critical_extra[i]) {
        forms_critical_extra[i].addEventListener("submit", function (e) {
            let message = prompt("Enter reason for rejecting");
            if (message === null) {
                e.preventDefault();
            }
            else if (message === "") {
                while (message === "") {
                    message = prompt("Enter reason for rejecting");
                }
            }
            if (!(message === null) || message) {
                let result = confirm("Are you sure you want to proceed?");
                if (result) {
                    messageElement.value = message;
                    return true;
                }
                else {
                    e.preventDefault();
                }
            }
            else if (message === null) {
                e.preventDefault();
            }

        });
    }
}

//styles a div when its container expands

const bg_rejected = document.getElementsByClassName('bg-rejected');
const bg_container = document.getElementsByClassName('table-container');

window.addEventListener('load', (event) => {
    if (bg_container != null) {
        for (let i = 0; i < bg_container.length; i++) {
            if (bg_container[i]) {
                var style = window.getComputedStyle(bg_container[i])
                width = style.getPropertyValue('width');
                height = style.getPropertyValue('height');
                bg_rejected[i].style.width = width;
                bg_rejected[i].style.height = height;
                bg_container[i].addEventListener('change', function () {
                    width = style.getPropertyValue('width');
                    height = style.getPropertyValue('height');
                    bg_rejected[i].style.width = width;
                    bg_rejected[i].style.height = height;
                })
            }
        }
    }
});
//Code for checking if an item is checked or not for it to disable a textbox

const checkDeciders = document.getElementsByClassName('check-decider');
const checkDependents = document.getElementsByClassName('check-dependant');

for (let i = 0; i < checkDeciders.length; i++) {
    if (checkDeciders[i]) {
        checkDeciders[i].addEventListener('change', function () {
            if (checkDeciders[i].checked) {
                checkDependents[i].disabled = false;
                checkDependents[i].required = true;
            }
            else {
                checkDependents[i].disabled = true;
                checkDependents[i].required = false;
            }
        })
    }
}

//Code for loading a picture

$("#profileImage").click(function (e) {
    $("#imageUpload").click();
});
const imageRemover = document.getElementById('imageRemover');
const imageUplder = document.getElementById('imageUpload');
function fasterPreview(uploader) {
    if (uploader.files && uploader.files[0]) {
        $('#profileImage').attr('src',
            window.URL.createObjectURL(uploader.files[0]));
        imageRemover.style.display = "inline";
    }
}

$("#imageUpload").change(function () {
    fasterPreview(this);
});
function removeImage() {
    imageUplder.value = null;
    $('#profileImage').attr('src',
        "/icons/user-icon.png");
    imageRemover.style.display = "none";
}
    
