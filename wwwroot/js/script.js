'use strict';

/**
* [23/05/2024] - Create - Set time out turn off message.
*/
$(document).ready(function () {
    loadBlog();

    var editor = new FroalaEditor('#myEditor', {
        //toolbarButtons: {
        //    'moreText': {
        //        'buttons': ['bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', 'fontFamily', 'fontSize', 'textColor', 'backgroundColor', 'inlineClass', 'inlineStyle', 'clearFormatting']
        //    },
        //    'moreParagraph': {
        //        'buttons': ['alignLeft', 'alignCenter', 'formatOLSimple', 'alignRight', 'alignJustify', 'formatOL', 'formatUL', 'paragraphFormat', 'paragraphStyle', 'lineHeight', 'outdent', 'indent', 'quote']
        //    },
        //    'moreRich': {
        //        'buttons': ['insertLink', 'insertImage', 'insertVideo', 'insertTable', 'emoticons', 'fontAwesome', 'specialCharacters', 'embedly', 'insertFile', 'insertHR']
        //    },
        //    'moreMisc': {
        //        'buttons': ['undo', 'redo', 'fullscreen', 'print', 'getPDF', 'spellChecker', 'selectAll', 'html', 'help']
        //    }
        //},
        placeholderText: ''
    });

    setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 3000);

    readingTime();
});

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
    image.removeAttribute("src", '../images/');
    $("#BlogName").val("");
    $("#Author").val("");
    $("#CategoryID").val("");
    $("#Link").val("");
}

/**
* [14/05/2024] - Created - load data.
*/
function loadBlog() {
    $.ajax({
        async: true,
        cache: false,
        type: 'GET',
        dataType: 'HTML',
        contentType: false,
        processType: false,
        url: '/BlogWebs/GetBlog',
        success: function (data) {
            $("#loadBlog").html(data)
        },
        error: function () {
            alert('Something went wrong at load data Blog!');
        }
    });
}

/**
 * [19/05/2024] - Create
 * [21/05/2024] - Updated - Add element id to load data when edit.
 *                        - Check image null.
 */
function updateBlog(blogID) {
    /*Tìm thẻ p chứa blogDescription và set id để load data khi edit*/
    var para = document.getElementsByTagName('p')[4];
    para.id = "DataBlogDescription";
    $.ajax({
        async: true,
        cache: false,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'applicaton/json;charset=utf-8',
        data: { blogID: blogID },
        url: "/BlogWebs/UpdateBlog",
        success: function (data) {
            $("#exampleModal").modal("show");
            $('#btnSave').val("Cập nhật");
            $('#BlogID').val(data.blogID);
            $('#BlogName').val(data.blogName);
            $('#Author').val(data.author);
            $('#CategoryID').val(data.categoryID);
            $('#Link').val(data.link);
            if (data.image == null) {
                $("#previewImage").attr('src', "../images/default_imageblog.jpg");
            } else {
                $("#previewImage").attr('src', "../images/" + data.image);
            }

            const element = document.getElementById("DataBlogDescription");
            element.innerHTML = data.blogDescription;
        },
        error: function (errormessage) {
            alert('Something went wrong at updated data Blog!');
        }
    });
}

/**
 * [11/06/2024] - Create - Caculate word into reading time.
 */
function readingTime() {
    const wpm = 200;
    const text = $('#article').text();
    const words = text.trim().split(/\s+/).length;
    const time = Math.ceil(words / wpm);
    document.getElementById("time").innerText = time + " phút đọc";
}