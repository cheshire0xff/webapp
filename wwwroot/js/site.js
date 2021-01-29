// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
window.onload = function () {
    stringShorten(80);
    hilightCurrent();

    
};

function hilightCurrent() {
    const queryString = new URL(window.location);
    const jobId = queryString.searchParams.get('jobId');

    if (jobId === null)
        document.getElementsByTagName("tr").item(1).style.backgroundColor = 'rgb(173, 244, 255)';
    else
        document.getElementById("trPreview+" + jobId).style.backgroundColor = 'rgb(173, 244, 255)';
}

function stringShorten(length) {
    var a = document.getElementsByClassName("shortString");
    for (var i = 0; i < a.length; i++) {

        var tmp = a[i].textContent.trim();
        if (a[i].textContent.length > length)
            tmp = a[i].textContent.substring(0, length - 3) + "...";

        a[i].textContent = tmp;
    }
}

//function firstCapitalLetterFunc() {
//    var a = document.getElementsByClassName("firstCapitalLetter");
//    for (var i = 0; i < a.length; i++) {
//        a[i].textContent =
//            a[i].textContent.trim().substring(0, 1).toUpperCase()
//            + a[i].textContent.trim().substring(1);
//    }
//}

function openJobOfferId(jobId) {  
    document.getElementById("preview+" + jobId).click();
}

//function displayJobOfferId(jobId) {
//    let params = new URLSearchParams(url.search.slice(1));
//    var a = document.getElementsByClassName("shortString");
//    for (var i = 0; i < a.length; i++) {

//        var tmp = a[i].textContent.trim();
//        if (a[i].textContent.length > length)
//            tmp = a[i].textContent.substring(0, length - 3) + "...";

//        a[i].textContent = tmp;
//    }
//}