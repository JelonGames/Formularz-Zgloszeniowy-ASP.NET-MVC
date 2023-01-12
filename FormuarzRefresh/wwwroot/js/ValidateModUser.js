class classCorrectData {
    DepartmentsData = false;
    //JbsData = true;
    NetResourcesData = true;
    PublicFolderData = true;

    UserNameData = false;
    BusinessPhoneData = true;
    VoipProjectData = true;
    ExpandActiveAccountData = true;
    ChangeLastNameData = true;
    GrowupDiskZData = true;
    MorePageData = true;

    RedirectEmailData = true;
    AccessOtherEmailData = true;
    AccessOtherDiskZData = true;

    AllDataIsCorrect() {
        if (this.DepartmentsData &&
            //this.JbsData &&
            this.NetResourcesData &&
            this.PublicFolderData &&
            this.UserNameData &&
            this.BusinessPhoneData &&
            this.VoipProjectData &&
            this.ExpandActiveAccountData &&
            this.ChangeLastNameData &&
            this.GrowupDiskZData &&
            this.MorePageData &&
            this.RedirectEmailData &&
            this.AccessOtherEmailData &&
            this.AccessOtherDiskZData) {
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
//function JbsDataValid(checkboxID, selectID, errorBoxID) {
//    var checkbox = document.getElementById(checkboxID);
//    var select = document.getElementById(selectID).value;
//    var errorObj = document.getElementById(errorBoxID);

//    if (checkbox.checked == true && select == "Brak") {
//        errorObj.style.display = "inline";
//        errorObj.innerHTML = "Musisz wybrać poziom uprawnień";
//        correctData.JbsData = false;
//    }
//    else if (checkbox.checked == false && select != "Brak") {
//        errorObj.style.display = "inline";
//        errorObj.innerHTML = "Musisz zaznaczyć że użytkownik ma mieć dostęp do tej bazy";
//        correctData.JbsData = false;
//    }
//    else if (checkbox.checked == true && select != "Brak") {
//        errorObj.style.display = "none";
//        errorObj.innerHTML = "";
//        correctData.JbsData = true;
//    }
//    else if (checkbox.checked == false && select == "Brak") {
//        errorObj.style.display = "none";
//        errorObj.innerHTML = "";
//        correctData.JbsData = true;
//    }
//}

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

/*ModUser Page Valid*/
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

function RedirectEmailValid() {
    var checkbox = document.getElementById("RedirectEmail").checked;
    var toEmail = document.getElementById("ToEmail").value;
    var fromEmail = document.getElementById("FromEmail").value;
    var when = document.getElementById("WhenRedirect").valueAsDate;

    var errorObj = document.getElementById("RedirectEmailError");

    var date = new Date();

    if (checkbox == true &&
        ((toEmail == null || toEmail == "") || (fromEmail == null || fromEmail == "") || (when == null || when == ""))) {

        errorObj.style.display = "inline";
        errorObj.innerHTML = "Wymagane jest uzupełnienie wszystkich pól";
        correctData.RedirectEmailData = false;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.RedirectEmailData = true;
    }
    else if (checkbox == true &&
        (toEmail != null && toEmail != "") &&
        (fromEmail != null && fromEmail != "") &&
        (when != null && when != "")) {

        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.RedirectEmailData = true;
    }
}

function AccessOtherEmailValid() {
    var checkbox = document.getElementById("AccessOtherEmail").checked;
    var result = document.getElementById("WhoseEmail").value;

    var errorObj = document.getElementById("AccessOtherEmailError");

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Wymagane jest uzupełnienie pola czyją pocztę udostępnić";
        correctData.AccessOtherEmailData = false;
    }
    else if (checkbox == true && (result != null || result != "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.AccessOtherEmailData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.AccessOtherEmailData = true;
    }
}

function AccessOtherDiskZValid() {
    var checkbox = document.getElementById("AccessOtherDiskZ").checked;
    var result = document.getElementById("WhoseDiskZ").value;
    var when = document.getElementById("WhenAccessDiskZ").valueAsDate;

    var errorObj = document.getElementById("AccessOtherDiskZError");

    var date = new Date();

    if (checkbox == true &&
        ((result == null || result == "") || (when == null || when == ""))) {

        errorObj.style.display = "inline";
        errorObj.innerHTML = "Wymagane jest uzupełnienie wszystkich pól";
        correctData.AccessOtherDiskZData = false;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.AccessOtherDiskZData = true;
    }
    else if (checkbox == true &&
        (result != null && result != "") &&
        (when != null && when != "")) {

        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.AccessOtherDiskZData = true;
    }
}


// Funkcja do walidacji danych takich jak
// Telefon służbowy                     - BusinessPhone     - BP
// Podpięcie do pętli                   - VoipProject       - VP
// Przedłużenie ważnosci konta          - ExpandActiveDate  - EAD
// Zmiana nazwiska w systemie KPL       - ChangeLastName    - CLN
// Powiększenie dysku Z                 - GrowupDiskZ       - GDZ
// Dodatkowe strony internetowe         - MorePage          - MP
function ValidateHideObject(idCheckbox, idObject, idErrorObject, typeValidateData) {

    switch (typeValidateData) {
        case "BP":
            BusinesPhoneValid(idCheckbox, idObject, idErrorObject);
            break;
        case "VP":
            VoipProjectValid(idCheckbox, idObject, idErrorObject);
            break;
        case "EAD":
            ExpandActiveAccountValid(idCheckbox, idObject, idErrorObject);
            break;
        case "CLN":
            ChangeLastNameValid(idCheckbox, idObject, idErrorObject);
            break;
        case "GDZ":
            GrowupDiskZValid(idCheckbox, idObject, idErrorObject);
            break;
        case "MP":
            MorePageValid(idCheckbox, idObject, idErrorObject);
            break;
    }
}

function BusinesPhoneValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).value;
    var errorObj = document.getElementById(idErrorObject);

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.BusinessPhoneData = false;
    }
    else if (checkbox == true && (result != null || result == "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.BusinessPhoneData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.BusinessPhoneData = true;
    }
}

function VoipProjectValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).value;
    var errorObj = document.getElementById(idErrorObject);

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.VoipProjectData = false;
    }
    else if (checkbox == true && (result != null || result == "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.VoipProjectData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.VoipProjectData = true;
    }
}

function ExpandActiveAccountValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).valueAsDate;
    var errorObj = document.getElementById(idErrorObject);

    var date = new Date();

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.ExpandActiveAccountData = false;
    }
    else if (checkbox == true && (result != null || result != "") && result >= date) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.ExpandActiveAccountData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.ExpandActiveAccountData = true;
    }
    else{
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Data nie może być wcześniejsza niż jutrzejsza.";
        correctData.ExpandActiveAccountData = false;
    }
}

function ChangeLastNameValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).value;
    var errorObj = document.getElementById(idErrorObject);

    const reg = /[A-Z]{1}[a-z]{1,50}-?[A-Z]?[a-z]{1,50}/g;
    var iscorrect = reg.test(result);

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.ChangeLastNameData = false;
    }
    else if (checkbox == true && (result != null || result == "") && iscorrect == false) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione zgodnie z przykładem: <br /> Kowalski lub Kowalski-Nowak";
        correctData.ChangeLastNameData = false;
    }
    else if (checkbox == true && (result != null || result == "") && iscorrect == true) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.ChangeLastNameData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.ChangeLastNameData = true;
    }
}

function GrowupDiskZValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).value;
    var errorObj = document.getElementById(idErrorObject);

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.GrowupDiskZData = false;
    }
    else if (checkbox == true && (result != null || result == "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.GrowupDiskZData = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.GrowupDiskZData = true;
    }
}

function MorePageValid(idCheckbox, idObject, idErrorObject) {
    var checkbox = document.getElementById(idCheckbox).checked;
    var result = document.getElementById(idObject).value;
    var errorObj = document.getElementById(idErrorObject);

    if (checkbox == true && (result == null || result == "")) {
        errorObj.style.display = "inline";
        errorObj.innerHTML = "Pole musi być wypełnione.";
        correctData.MorePageValid = false;
    }
    else if (checkbox == true && (result != null || result == "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.MorePageValid = true;
    }
    else if (checkbox == false) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.MorePageValid = true;
    }
}