let title = $('#title');

title.on('click', function () {

    if ($(this).text=="Hello") {
        $(this).text("JOB FAST")
    } else {
        $(this).text("Hello");
    }
});
