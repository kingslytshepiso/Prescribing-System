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
for (let i = 0; i < form_critical.length; i++) {
    if (form_critical[i]) {
        form_critical[i].addEventListener("submit", function (e) {
            let result = confirm("Are you sure you want to proceed?");
            if (result)
                return true;
            else
                e.preventDefault()

        });
    }
}
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
    

