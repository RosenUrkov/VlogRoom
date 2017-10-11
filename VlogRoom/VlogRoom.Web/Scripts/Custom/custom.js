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

$(document).ready(function () {
	size_li = $("#myList li").length;
	x=1;
	$('#myList li:lt('+x+')').show();
	$('#loadMore').click(function () {
		x= (x+1 <= size_li) ? x+1 : size_li;
		$('#myList li:lt('+x+')').show();
	});
	$('#showLess').click(function () {
		x=(x-1<0) ? 1 : x-1;
		$('#myList li').not(':lt('+x+')').hide();
	});
});