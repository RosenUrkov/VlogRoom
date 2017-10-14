$(() => {
    $('#loader').hide();
});

$(() => {
    size_li = $("#myList li").length;
    x = 1;
    $('#myList li:lt(' + x + ')').show();

    $('#loadMore').on("click", () => {
        x = (x + 1 <= size_li) ? x + 1 : size_li;
        $('#myList li:lt(' + x + ')').show();
    });

    $('#showLess').on("click", () => {
        x = (x - 1 < 0) ? 1 : x - 1;
        $('#myList li').not(':lt(' + x + ')').hide();
    });
});

$(() => {
    $("#slider3").responsiveSlides({
        auto: true,
        pager: false,
        nav: true,
        speed: 500,
        namespace: "callbacks",
        before: () => {
            $('.events').append("<li>before event fired.</li>");
        },
        after: () => {
            $('.events').append("<li>after event fired.</li>");
        }
    });

    $(".container").on("change", "#upload-input", () => {
        $("#upload-form").submit();
    });

    $(".container").on("click", "#subscribe-button", (ev) => {
        ev.preventDefault();
        $("#subscribe-form").submit();
    });

    $(".container").on("click", ".edit-username", (ev) => {
        const value = $(".username-value").text();
        $(".username-value").replaceWith($("<input>").addClass("username-input").addClass("form-control").val(value));

        $(".edit-username")
            .removeClass("glyphicon-pencil")
            .removeClass("edit-username")
            .addClass("glyphicon-check")
            .addClass("save-username");
    });

    $(".container").on("click", ".save-username", () => {
        $('#loader').show();

        const newName = $(".username-input").val();
        $(".username-input").replaceWith($("<span>").addClass("username-value").text(newName));
        $(".author").text(newName);

        $(".save-username")
            .removeClass("glyphicon-check")
            .removeClass("save-username")
            .addClass("glyphicon-pencil")
            .addClass("edit-username");

        $.ajax({
            url: "/Users/RenameRoom",
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify({ newName }),
            success: () => $('#loader').hide()
        });
    });

    $(".container").on("click", ".remove-button", (ev) => {
        $('#loader').show();

        ev.preventDefault();
        const videoId = $(".user-video").remove().attr("id");

        $.ajax({
            url: "/Videos/DeleteVideo",
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify({ videoId }),
            success: () => $('#loader').hide()
        });
    });
});