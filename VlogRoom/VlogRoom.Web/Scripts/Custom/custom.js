// You can also use "$(window).load(function() {"
$(function () {
    $("#slider3").responsiveSlides({
        auto: true,
        pager: false,
        nav: true,
        speed: 500,        
        namespace: "callbacks",
        before: function () {
	        $('.events').append("<li>before event fired.</li>");
        },
        after: function () {
	        $('.events').append("<li>after event fired.</li>");
        }
    });
});