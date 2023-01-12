// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function HideObject(idCheckbox, idObject) {
    var result = document.getElementById(idCheckbox).checked;

    if (result != true) {
        document.getElementById(idObject).style.display = "inline";
    }
    else {
        document.getElementById(idObject).style.display = "none";
    }
}

function UnHide(idCheckbox, idObject) {
    var result = document.getElementById(idCheckbox).checked;

    if (result == true) {
        document.getElementById(idObject).style.display = "inline";
    }
    else {
        document.getElementById(idObject).style.display = "none";
    }
}

function ChangeDisplay(idObject) {
    var object = document.getElementById(idObject);

    if (object.style.display == "none" || object.style.display == "") {
        object.style.display = "inline";
    }
    else {
        object.style.display = "none";
    }
}

function ChangeDisplay(idObject, idArrow) {
    var object = document.getElementById(idObject);
    var arrow = document.getElementById(idArrow);

    if (object.style.display == "none" || object.style.display == "") {
        object.style.display = "inline";
        arrow.style.transform = 'rotate(-45deg)';
    }
    else {
        object.style.display = "none";
        arrow.style.transform = 'rotate(135deg)';
    }
}

function CheckboxButton(idCheckbox) {
    var checkbox = document.getElementById(idCheckbox);

    if (checkbox.checked) {
        checkbox.checked = false
    }
    else {
        checkbox.checked = true;
    }
}