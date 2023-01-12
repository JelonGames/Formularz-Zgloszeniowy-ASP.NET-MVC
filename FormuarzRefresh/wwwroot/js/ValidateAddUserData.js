class classCorrectData {
    DepartmentsData = false;
    JbsData = true;
    NetResourcesData = true;
    PublicFolderData = true;

    UserNameData = false;
    ActiveDateData = true;
    DeactiveDateData = false;

    AllDataIsCorrect() {
        if (this.DepartmentsData &&
            this.JbsData &&
            this.NetResourcesData &&
            this.PublicFolderData &&
            this.UserNameData &&
            this.ActiveDateData &&
            this.DeactiveDateData) {
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
    UserNameValid();

    SetDateActive();
    ActiveDateValid();
    DeactivDateValid();
}
window.onchange = function () {
    correctData.AllDataIsCorrect();
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

/*DataAccess Page Valid*/
function JbsDataValid(checkboxID, selectID, errorBoxID) {
    var checkbox = document.getElementById(checkboxID);
    var select = document.getElementById(selectID).value;
    var errorObj = document.getElementById(errorBoxID);

    if (checkbox.checked == true && select == "Brak") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Musisz wybrać poziom uprawnień";
        correctData.JbsData = false;
    }
    else if (checkbox.checked == false && select != "Brak") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Musisz zaznaczyć że użytkownik ma mieć dostęp do tej bazy";
        correctData.JbsData = false;
    }
    else if (checkbox.checked == true && select != "Brak") {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.JbsData = true;
    }
    else if (checkbox.checked == false && select == "Brak") {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.JbsData = true;
    }
}

function NetResourcesValid() {
    var checbox = document.getElementById("NetResourcesCheckbox");
    var input = document.getElementById("NerResources").value;
    var errorObj = document.getElementById("NetResourcesError");

    const reg = /\\\\{1}[\s\S]{3,50}\\{1}[\s\S]{3,50}/g
    var checkReg = reg.test(input);

    if (checbox.checked == true && (input == null || input == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Podać serwer";
        correctData.NetResourcesData = false;
    }
    else if (checbox.checked == true && checkReg == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Trzeba podać adres zasobu zgodnie z przykłądem \\[NazwaSerwera]\[NazwaZasobu]";
        correctData.NetResourcesData = false;
    }
    else if (checbox.checked == true && checkReg == true) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.NetResourcesData = true;
    }
    else if (checbox.checked == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.NetResourcesData = true;
    }
}

function PublicFolderValid() {
    var checkbox = document.getElementById("PublicFolderCheckbox");
    var input = document.getElementById("PathPublicFolder").value;
    var checkboxPermision;
    var errorObj = document.getElementById("PublicFolderError");

    if (document.getElementById("ReadPublicFolder").checked == true ||
        document.getElementById("CreateFileOnPublicFolder").checked == true ||
        document.getElementById("CreateDirectoryOnPublicFolder").checked == true) {
        checkboxPermision = true;
    }
    else {
        checkboxPermision = false;
    }

    if (checkbox.checked == true && (input == null || input == "") && checkboxPermision == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Uzupełnij reszte danych";
        correctData.PublicFolderData = false;
    }
    else if (checkbox.checked == true && (input != null || input != "") && checkboxPermision == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Wybierz uprawnienia";
        correctData.PublicFolderData = false;
    }
    else if (checkbox.checked == true && (input == null || input == "") && checkboxPermision == true) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Podaj ścieżkę do folderu publicznego";
        correctData.PublicFolderData = false;
    }
    else if (checkbox.checked == true && (input != null || input != "") && checkboxPermision == true) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.PublicFolderData = true;
    }
    else if (checkbox.checked == true) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.PublicFolderData = true;
    }
}

/*AddUser Page Valid*/
function UserNameValid() {
    var result = document.getElementById("UserName").value;
    var errorObj = document.getElementById("UserNameError");

    const reg = /[A-Z]{1}[a-z]{4,50} [A-Z]{1}[a-z]{1,50}-?[A-Z]?[a-z]{1,50}?/g;
    var correct = reg.test(result);

    if (result == null || result == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.UserNameData = false;
    }
    else if ((result != null || result != "") && correct == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione zgodnie z przykładem: <br /> Jan Kowalski lub Jan Kowalski-Nowak";
        correctData.UserNameData = false;
    }
    else if (correct == true && (result != null || result != "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.UserNameData = true;
    }
}

function ActiveDateValid() {
    var activeDate = document.getElementById("ActiveDate").valueAsDate;
    var errorObj = document.getElementById("ActiveDateError");

    if (activeDate == null || activeDate == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.ActiveDateData = false;
    }
    else {
        errorObj.style.display = "";
        errorObj.innerHTML = "";
        correctData.ActiveDateData = true;
    }
}

function DeactivDateValid() {
    var activeDate = document.getElementById("ActiveDate").valueAsDate;
    var deactiveDate = document.getElementById("DeactiveDate").valueAsDate;
    var errorObj = document.getElementById("DeactiveDateError");

    if (deactiveDate == null || deactiveDate == "") {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole wymagane";
        correctData.DeactiveDateData = false;
    }
    else if (deactiveDate < activeDate) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Data deaktywacji nie może być wcześniejsza jak dzień aktywacji";
        correctData.DeactiveDateData = false;
    }
    else {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.DeactiveDateData = true;
    }
}

/*Funkcje tylko do ładowania strony*/
function SetDateActive() {
    var inputDateObject = document.getElementById("ActiveDate");

    inputDateObject.valueAsDate = new Date();
}