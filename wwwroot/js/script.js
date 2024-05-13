'use strict';
/**
 * Navbar variables
 */
const nav = document.querySelector('.mobile-nav');
const navMenuBtn = document.querySelector('.nav-menu-btn');
const navCloseBtn = document.querySelector('.nav-close-btn');

/**
 * navToogle function.
 */
const navToogleFunc = function () {
    nav.classList.toggle('active');
}

navMenuBtn.addEventListener('click', navToogleFunc);
navCloseBtn.addEventListener('click', navToogleFunc);

/** theme toogle variables. */
const themeBtn = document.querySelectorAll('.theme-btn');

for (let i = 0; i < themeBtn.length; i++) {
    themeBtn[i].addEventListener('click', function () {
        document.body.classList.toggle('light-theme');
        document.body.classList.toggle('dark-theme');

        for (let i = 0; i < themeBtn.length; i++) {
            themeBtn[i].classList.toggle('light');
            themeBtn[i].classList.toggle('dark');
        }
    })
}

/*
[13/05/2024] - Create - Display image when upload.
*/
function displayImage(result) {
    if (result.files && result.files[0]) {
        var fileReader = new FileReader;
        fileReader.onload = function (e) {
            $("#previewImage").attr('src', e.target.result);
        }
        fileReader.readAsDataURL(result.files[0]);
    }
}

function resetValue() {
    $('#Image').val("");
    var image = document.getElementById("previewImage");
    image.removeAttribute("src");
    $("#BlogName").val("");
    $("#Author").val("");
    $("#CategoryID").val("");
    $("#ReadingTime").val("");
    $("#BlogDescription").val("");
    $("#Link").val("");
}