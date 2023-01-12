class classCorrectData {
    DepartmentsData = false;

    /*Create Resources Data*/
    NameAndGoalData = true;
    UserPermissionData = true;

    /*Delete Create Data*/
    NameResourcesData = true;
    PathResourcesData = true;

    AllDataIsCorrect() {
        //console.log('check');
        //console.log('DepartmentsData ' + this.DepartmentsData);
        //console.log('NameAndGoalData ' + this.NameAndGoalData);
        //console.log('UserPermissionData ' + this.UserPermissionData);
        //console.log('NameResourcesData ' + this.NameResourcesData);
        //console.log('PathResourcesData ' + this.PathResourcesData);
        if (this.DepartmentsData &&
            this.NameAndGoalData &&
            this.UserPermissionData &&
            this.NameResourcesData &&
            this.PathResourcesData) {
            document.getElementById("SubmitButton").disabled = false;
        }
        else {
            document.getElementById("SubmitButton").disabled = true;
        }
    }
}

var correctData = new classCorrectData();

window.onload = function () {
    DepartmentsDataValid();
}
window.onchange = function () {
    setTimeout(function () {
        correctData.AllDataIsCorrect();
    }, 150);
}

/*FormalData Page Valid*/
function DepartmentsDataValid() {
    var result = document.getElementById("Dept").value;
    var errorObj = document.getElementById("DeptError");

    if (result == null || result == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.DepartmentsData = false;
    }
    else {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.DepartmentsData = true;
    }
}

/*Funkcje i zmienne do zarządzania która część formularza ma być walidowana*/
var howTypFormResources;

function ChangeType(result) {
    if (result == "Add") {
        howTypFormResources == "Add";

        correctData.NameResourcesData = true;
        correctData.PathResourcesData = true;

        ValidAddResourcesData();
    }
    else {
        howTypFormResources == "Del";

        correctData.NameAndGoalData = true;
        correctData.UserPermissionData = true;

        ValidDelResources();
    }
}

function ValidAddResourcesData() {
    NameAndGoalValid();
    UserPermissionValid();
}

function ValidDelResources() {
    NameResourcesValid();
    PathResourcesValid();
}

/*Crate Resources Data*/
function NameAndGoalValid() {
    var result = document.getElementById('NameAndGoalResources_Add').value;
    var errorObj = document.getElementById('NameAndGoalResources_Error');

    if (result == null || result == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.NameAndGoalData = false;
    }
    else {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.NameAndGoalData = true;
    }
}

function UserPermissionValid() {
    setTimeout(function () {
        var nowUsers = 1;
        var allIsCorrect = true;

        var tempUserPermission_Add = document.getElementById("UserPermission_Add");
        tempUserPermission_Add.value = "";

        var result = document.getElementById('permission' + nowUsers + 'user');

        while (result != null) {
            var errorObj = document.getElementById('permission' + nowUsers + 'Error');
            var permission = document.querySelector('input[name="permission' + nowUsers + '"]:checked');

            const reg = /[A-Z]{1}[a-z]{4,50} [A-Z]{1}[a-z]{1,50}-?[A-Z]?[a-z]{1,50}?/g;
            var regTest;
            regTest = reg.test(result.value);

            if (result.value == null || result.value == "") {
                errorObj.style.display = "inline";
                errorObj.innerHTML = "Pole wymagane";
                allIsCorrect = false;

                nowUsers++;
                result = document.getElementById('permission' + nowUsers + 'user');

                continue;
            }
            else if (regTest == false) {
                errorObj.style.display = "inline";
                errorObj.innerHTML = "Pole musi być wypełnione zgodnie z przykładem: <br /> Jan Kowalski lub Jan Kowalski-Nowak";
                allIsCorrect = false;

                nowUsers++;
                result = document.getElementById('permission' + nowUsers + 'user');

                continue;
            }
            else {
                errorObj.style.display = "none";
                errorObj.innerHTML = "";
            }

            tempUserPermission_Add.value += result.value + "," + permission.value + ";";
            nowUsers++;
            result = document.getElementById('permission' + nowUsers + 'user');
        }

        if (allIsCorrect) {
            correctData.UserPermissionData = true;
        }
        else {
            correctData.UserPermissionData = false;
        }

        console.log(tempUserPermission_Add.value);

    }, 50);
}

/*Delete Respurces Data*/
function NameResourcesValid() {
    var result = document.getElementById('NameResources_Del').value;
    var errorObj = document.getElementById('NameResources_Error');

    if (result == null || result == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.NameResourcesData = false;
    }
    else {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.NameResourcesData = true;
    }
}

function PathResourcesValid() {
    var input = document.getElementById("PathResources_Del").value;
    var errorObj = document.getElementById("PathResources_Error");

    const reg = /\\\\{1}[\s\S]{3,50}\\{1}[\s\S]{3,50}/g
    var checkReg = reg.test(input);

    if (input == null || input == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Podać serwer";
        correctData.PathResourcesData = false;
    }
    else if (checkReg == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Trzeba podać adres zasobu zgodnie z przykłądem \\[NazwaSerwera]\[NazwaZasobu]";
        correctData.PathResourcesData = false;
    }
    else {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.PathResourcesData = true;
    }
}