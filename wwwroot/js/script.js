'use strict';

/**
 * @since [23/05/2024] - Create - Set time out turn off message.
 * @since [04/07/2024] - Update - Change function declare -> function expression to imporve performance.
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
    }, 3500);

    readingTime();
    /**
     * @since [27/06/2024] - Create - Hold header when scroll down.
     */
    window.onscroll = function () { myFunction() };

    var header = document.querySelector("header");
    var sticky = header.offsetTop;
    var myFunction = function () {
        if (window.pageYOffset > sticky) {
            header.classList.add("sticky");
        } else {
            header.classList.remove("sticky");
        }
    }

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
        });
    }
});

/**
 * @since [09/07/2024] - Create - Add function add attribute image.
 * @param {any} dataReceive
 * flagAction 0: default, 1: add new, 2: update.
 */
function addAttributeCoverImage(dataReceive, flagAction) {
    let attributeImage = document.querySelector("#previewImage");

    attributeImage.setAttribute('height', "250px");
    attributeImage.setAttribute('width', "250px");
    attributeImage.setAttribute('style', "border:1px solid #ced4da; border-radius: .25rem");

    switch (flagAction) {
        case 1:
            attributeImage.setAttribute('src', dataReceive.target.result);
            break;
        case 2:
            attributeImage.setAttribute('src', "../ImagesBlog/" + dataReceive);
            break;
        default:
            attributeImage.setAttribute('src', "../ImagesBlog/default_imageblog.jpg");
            break;
    }
}

/**
 * @since [13/05/2024] - Create - Display image when upload.
 * @since [07/07/2024] - Updated - Add attribute image Blog.
 * @param {any} result
 */
var displayImage = function (result) {
    if (result.files && result.files[0]) {
        let fileReader = new FileReader();
        fileReader.onload = function (e) {
            addAttributeCoverImage(e, 1);
        }
        fileReader.readAsDataURL(result.files[0]);
    }
}

/**
 * @since [07/07/2024] - Updated - Remove attribute img Blog.
 */
var resetValue = function () {
    let attributeImage = document.querySelector("#previewImage");
    let elementBlogDescription = document.getElementsByTagName('p')[4];

    if (attributeImage != ((null) || (""))) {
        attributeImage.removeAttribute('src');
        attributeImage.removeAttribute('height');
        attributeImage.removeAttribute('width');
        attributeImage.removeAttribute('style');
    }

    if (elementBlogDescription != (null)) {
        elementBlogDescription.innerHTML = "";
    }

    $("#BlogName").val("");
    $("#Image").val("");
    $("#Author").val("");
    $("#CategoryID").val("");
    $("#Link").val("");
    $("#CoverImage").val("");
}

/**
 * @since [14/05/2024] - Created - load data.
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
            $("#loadBlog").html(data);
        },
        error: function () {
            alert('Something went wrong at load data Blog!');
        }
    });
}

/**
 * @since [19/05/2024] - Create
 * @since [21/05/2024] - Updated - Add element id to load data when edit.
 *                               - Check image null.
 * @since [07/07/2024] - Updates - Set attribute img.
 * @since [13/07/2024] - Updates - Remove id = "dataBlogDescription".
 * @param {any} blogID
 */
var updateBlog = function (blogID) {
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

            /* Tìm thẻ p chứa blogDescription và set id để load data khi edit. */
            if (data.blogDescription != null) {
                let para = document.getElementsByTagName('p')[4];
                para.innerHTML = data.blogDescription;
            }

            if (data.coverImage != null) {
                addAttributeCoverImage(data.coverImage, 2);
            }
            else {
                addAttributeCoverImage(null, 0);
            }
        },
        error: function (errormessage) {
            alert('Something went wrong at updated data Blog !');
        }
    });
}

/**
 * @since [11/06/2024] - Create - Caculate word into reading time.
 * @since [27/06/2024] - Updated - Check null if there isn't value text.
 */
function readingTime() {
    let wpm = 200;
    let text = $('#article').text();
    let words = text.trim().split(/\s+/).length;
    let time = Math.ceil(words / wpm);
    if (text != "") {
        document.getElementById("time").innerText = time + " phút đọc";
    }
}