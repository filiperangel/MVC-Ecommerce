$(document).ready(function () {
    $("#searchText").keypress(function (event) {
        if (event.which == 13) {
            $("#searchButton").click();
        }
    });
});

function Search() {
    window.location.href = 'http://localhost:49283/Home/?search=' + $("#searchText").val();
}