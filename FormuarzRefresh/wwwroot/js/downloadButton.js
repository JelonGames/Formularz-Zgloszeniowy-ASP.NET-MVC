function download(btn) {
    const text = btn.querySelector(".download-text");
    const cloud = btn.querySelector(".cloud-icon");
    const check = btn.querySelector(".check-icon");
    cloud.classList.add("pulseAnimation");

    text.style.transform = "translateX(-200%)";
    setTimeout(() => {
        btn.style.transform = "scaleX(0.4)";
        btn.style.borderRadius = "30px";
        cloud.style.transform = "translateX(-100%) scaleX(3.2) scaleY(1.25)";
    }, 200);
    setTimeout(() => {
        cloud.style.transform = "translateX(-100%) translateY(-200%)";
        btn.style.background = "#268778";
    }, 1800);
    setTimeout(() => {
        cloud.style.display = "none";
        check.style.display = "block";
    }, 2100);
    setTimeout(() => {
        check.style.transform = "scaleX(2.5) translateX(-40%) translateY(0)";
    }, 2150);

    save();
}

function save() {
    var element = document.getElementById("form");
    var opt = {
        margin: [10, 5, 10, 5],
        filename: 'formularz.pdf',
        //pagebreak: { mode: ['avoid-all', 'legacy'], before: ['.form-line', '.hide', 'input', 'button'], after: ['.form-line', '.hide', 'input', 'button'] },
        pagebreak: { mode: 'avoid-all' },
        jsPDF: { orientation: 'portrait', unit: 'mm', format: 'a4', comperssPDF: false }
    };

    html2pdf().set(opt).from(element).save();
}