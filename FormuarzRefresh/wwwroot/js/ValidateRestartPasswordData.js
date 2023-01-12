class classCorrectData {
    DepartmentsData = false;

    UserNameData = false;

    AllDataIsCorrect() {
        if (this.DepartmentsData &&
            this.UserNameData) {
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

/*RestartPassword Page*/
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
        errorObj.innerHTML = "Pole musi być wypełnione zgodnie z przykładem: <br /> Jak Kowalski lub Jan Kowalski-Nowak";
        correctData.UserNameData = false;
    }
    else if (correct == true && (result != null || result != "")) {
        errorObj.style.display = "none";
        errorObj.innerHTML = "";
        correctData.UserNameData = true;
    }
}