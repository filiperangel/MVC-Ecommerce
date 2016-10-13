$(document).ready(function () {
    autoresize(document.getElementById("Synopsis"));

    $('#Synopsis').keyup(function () {
        var synopsis = document.getElementById("Synopsis");
        autoresize(synopsis);
    });
});

function autoresize(textarea) {
        textarea.style.height = '0px';     //Reset height, so that it not only grows but also shrinks
        textarea.style.height = (textarea.scrollHeight + 10) + 'px';    //Set new height
}