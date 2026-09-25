$(document).ready(function () {

    "use strict";
    // wow 
    new WOW({
        // mobile: false,
    }).init();

    // 3 items carousel carousel-items-3
    $(".carousel-items-4").owlCarousel({
        margin: 20,
        loop: true,
        navText: ['<span class="emdad-iconarrow-left"></span>', '<span class="emdad-iconarrow-right"></span>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });


    // 3 items carousel carousel-items-6
    $(".carousel-items-6").owlCarousel({
        margin: 20,
        loop: true,
        navText: ['<span class="emdad-iconarrow-left"></span>', '<span class="emdad-iconarrow-right"></span>'],
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 3
            },
            1000: {
                items: 6
            }
        }
    });

    $('[data-countdown]').each(function () {
        var $this = $(this), finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime('<div class="row"><div class="col-6 col-lg-3 mb-3 mb-lg-0 comming-soon-counter">%D <br> <span>Days</span></div>  <div class="col-6 col-lg-3 comming-soon-counter"> %H <br> <span>Hours</span> </div> <div class="col-6 col-lg-3 mb-3 mb-lg-0 comming-soon-counter">  %M <br> <span>Minutes</span></div> <div class="col-6 col-lg-3 mb-3 mb-lg-0 comming-soon-counter"> %S <br> <span>Seconds</span></div></div>'));
        });
    });



    // sync carousel

    var sync1 = $("#sync1");
    var sync2 = $("#sync2");
    var slidesPerPage = 3; //globaly define number of elements per page
    var syncedSecondary = true;

    sync1.owlCarousel({
        items: 1,
        slideSpeed: 2000,
        nav: true,
        autoplay: true,
        center: true,
        dots: true,
        loop: true,
        responsiveRefreshRate: 200,
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
    }).on('changed.owl.carousel', syncPosition);

    sync2
        .on('initialized.owl.carousel', function () {
            sync2.find(".owl-item").eq(0).addClass("current");
        })
        .owlCarousel({
            items: slidesPerPage,
            dots: false,
            nav: false,
            mouseDrag: true,
            smartSpeed: 200,
            slideSpeed: 500,
            slideBy: slidesPerPage, //alternatively you can slide by 1, this way the active slide will stick to the first item in the second carousel
            responsiveRefreshRate: 100
        }).on('changed.owl.carousel', syncPosition2);

    function syncPosition(el) {
        //if you set loop to false, you have to restore this next line
        //var current = el.item.index;

        //if you disable loop you have to comment this block
        var count = el.item.count - 1;
        var current = Math.round(el.item.index - (el.item.count / 2) - .5);

        if (current < 0) {
            current = count;
        }
        if (current > count) {
            current = 0;
        }

        //end block

        sync2
            .find(".owl-item")
            .removeClass("current")
            .eq(current)
            .addClass("current");
        var onscreen = sync2.find('.owl-item.active').length - 1;
        var start = sync2.find('.owl-item.active').first().index();
        var end = sync2.find('.owl-item.active').last().index();

        if (current > end) {
            sync2.data('owl.carousel').to(current, 100, true);
        }
        if (current < start) {
            sync2.data('owl.carousel').to(current - onscreen, 100, true);
        }
    }

    function syncPosition2(el) {
        if (syncedSecondary) {
            var number = el.item.index;
            sync1.data('owl.carousel').to(number, 100, true);
        }
    }

    sync2.on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).index();
        sync1.data('owl.carousel').to(number, 300, true);
    });




    $('#emdad-testimonial-carousel').owlCarousel({
        loop: true,
        dots: true,
        margin: 20,
        autoplay: true,
        items: 1,
        navText: ['<span class="emdad-iconarrow-left"></span>', '<span class="emdad-iconarrow-right"></span>']

    });

    $('#emdad-partner-carousel').owlCarousel({
        loop: true,
        dots: false,
        autoplay: true,
        margin: 20,
        nav: true,
        navText: ['<i class="fa fa-chevron-left emdad-main-text-color emdad-main-border-color emdad-partner-nav-icon" aria-hidden="true"></i>', '<i class="fa fa-chevron-right emdad-main-text-color emdad-main-border-color emdad-partner-nav-icon" aria-hidden="true"></i>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 3
            },
            1000: {
                items: 4
            },
            1440: {
                items: 6
            }
        }
    });




    // multi event gallery timeLine

    $('.timeline').timelify({

        // animation types
        animLeft: "bounceInLeft",
        animRight: "bounceInRight",
        animCenter: "bounceInUp",

        // animation speed
        animSpeed: 1500,

        // trigger position in pixels
        offset: 150

    });




});


$(function ($) {
    "use strict";
    animatecounters();
});
function animatecounters() {
    $('.timer').each(count);
    function count(options) {
        var $this = $(this);
        options = $.extend({}, options || {}, $this.data('countToOptions') || {});
        $this.countTo(options);
    }
}
