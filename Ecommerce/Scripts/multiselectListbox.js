$('#bntRight').click(function (e) {
    var selectedOptions = $('#selectedAuthors option:selected');
    if (selectedOptions.length == 0) {
        alert('Nothing to move');
        e.preventDefault();
    }

    $('#availableAuthors').append($(selectedOptions).clone());
    $(selectedOptions).remove();
    e.preventDefault();
});

$('#btnLeft').click(function (e) {
    var selectedOptions = $('#availableAuthors option:selected');
    if (selectedOptions.length == 0) {
        alert('Nothing to move');
        e.preventDefault();
    }

    $('#selectedAuthors').append($(selectedOptions).clone());
    $(selectedOptions).remove();
    e.preventDefault();
});




$('#bntRightGenre').click(function (e) {
    var selectedOptions = $('#selectedGenres option:selected');
    if (selectedOptions.length == 0) {
        alert('Nothing to move');
        e.preventDefault();
    }

    $('#availableGenres').append($(selectedOptions).clone());
    $(selectedOptions).remove();
    e.preventDefault();
});

$('#btnLeftGenre').click(function (e) {
    var selectedOptions = $('#availableGenres option:selected');
    if (selectedOptions.length == 0) {
        alert('Nothing to move');
        e.preventDefault();
    }

    $('#selectedGenres').append($(selectedOptions).clone());
    $(selectedOptions).remove();
    e.preventDefault();
});

$('#btnSubmit').click(function (e) {
    $('#selectedGenres option').prop('selected', true);
    $('#selectedAuthors option').prop('selected', true);
});