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
    $(".container").on("change", "#upload-input", () => {
        $("#upload-form").submit();
    });

    $(".container").on("click", "#subscribe-button", (ev) => {
        ev.preventDefault();
        $("#subscribe-form").submit();
    });
});