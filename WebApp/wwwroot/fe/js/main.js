// Slick Destinations
$('.destinations-slide').slick({
    centerMode: true,
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3500,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    infinite: true,
    responsive: [{
            breakpoint: 769,
            settings: {
                arrows: true,
                centerMode: false,
                slidesToShow: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                centerMode: false,
                slidesToShow: 1
            }
        }
    ]
});


// Fixed Menu
const fixedElement = document.getElementById('fixedNav');

window.addEventListener('scroll', () => {
    if (window.scrollY > 90) {
        fixedElement.style.position = 'fixed';
    } else {
        fixedElement.style.position = 'relative';
    }
});

//Slick Places
$('.places-slide').slick({
    infinite: true,
    slidesToShow: 1,
    slidesToScroll: 1,
    dots: true,
    autoplay: false,
    autoplaySpeed: 3500,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    responsive: [{
            breakpoint: 769,
            settings: {
                dots: true,
            }
        },
        {
            breakpoint: 501,
            settings: {
                dots: false,
            }
        },
    ]
});

//Slick Events
// $('.events-month-slide').slick({
//     slidesToShow: 3,
//     slidesToScroll: 1,
//     centerMode: true,
//     asNavFor: '.events-slide',
//     initialSlide: 11,
//     prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
//     nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
//     responsive: [{
//         breakpoint: 480,
//         settings: {
//             arrows: false,
//         }
//     }, ]
// });

$('.events-slide').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    // initialSlide: 11,
    // asNavFor: '.events-month-slide',
    centerMode: false,
    focusOnSelect: true,
    arrows: true,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    responsive: [{

        breakpoint: 480,
        settings: {
            arrows: false,
            slidesToShow: 1,
        }
    }]
});

//Slick Relics Detail Image 
$('.slide-relics-image').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3500,
    infinite: true,
    arrows: false
});

//Slick ĐỊA ĐIỂM XUNG QUANH
$('.relics-destinations-slide').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3500,
    infinite: true,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    arrows: true,
    responsive: [{
            breakpoint: 1024,
            settings: {
                slidesToShow: 2
            }
        },
        {
            breakpoint: 769,
            settings: {
                slidesToShow: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                slidesToShow: 1
            }
        }
    ]
});

// SLICK CHI TIẾT TIN TỨC - SỰ KIỆN

$('.slick-news').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3500,
    infinite: true,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-chevron-left"></i></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-chevron-right"></i></button>`,
    arrows: true,
    responsive: [{
            breakpoint: 769,
            settings: {
                slidesToShow: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                slidesToShow: 1
            }
        }
    ]
});

$('.slide-artifacts').slick({
    slidesToShow: 4,
    slidesToScroll: 4,
    autoplay: false,
    autoplaySpeed: 3500,
    infinite: true,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-chevron-left"></i></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-chevron-right"></i></button>`,
    arrows: true,
    responsive: [{
            breakpoint: 1025,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
            }
        },
        {
            breakpoint: 769,
            settings: {
                arrows: true,
                slidesToScroll: 2,
                slidesToShow: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        }
    ]
});

$('.artifacts-slide-1').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    asNavFor: '.artifacts-slide-2',
    arrows: false,
    infinite: true,
    verticalSwiping: true,
    vertical: true,
    focusOnSelect: true,
    responsive: [{
        breakpoint: 480,
        settings: {
            arrows: false,
            vertical: false,
            verticalSwiping: false,
            centerMode: true,

        }
    }, ]
});

$('.artifacts-slide-2').slick({
    slidesToShow: 1,
    infinite: true,
    slidesToScroll: 1,
    asNavFor: '.artifacts-slide-1',
    arrows: false,
    fade: true,
});


$('.slide-artifacts-other').slick({
    slidesToShow: 4,
    slidesToScroll: 4,
    autoplay: false,
    autoplaySpeed: 3500,
    infinite: true,
    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-chevron-left"></i></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-chevron-right"></i></button>`,
    arrows: true,
    responsive: [{
            breakpoint: 1025,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
            }
        },
        {
            breakpoint: 769,
            settings: {
                arrows: true,
                slidesToScroll: 2,
                slidesToShow: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        }
    ]
});

// Lọc di tích
$("#selectAll").click(function() {
    $("input[type=checkbox]").prop("checked", $(this).prop("checked"));
});

$("input[type=checkbox]").click(function() {
    if (!$(this).prop("checked")) {
        $("#selectAll").prop("checked", false);
    }
});

$(".rotate").click(function() {
    $(this).toggleClass("right");
    $('.map-filter').slideToggle();
})

//    SLICK DAN TOC
$('.people-slide').slick({
    centerMode: true,
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    // autoplay: true,
    // autoplaySpeed: 3500,

    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    infinite: true,
    responsive: [{
            breakpoint: 769,
            settings: {
                arrows: true,
                centerMode: true,
                slidesToShow: 3
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                centerMode: false,
                slidesToShow: 1
            }
        }
    ]
});

//    SLICK TRANG PHUC DAN TOC
$('.national-costume-slide').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    // autoplay: true,
    // autoplaySpeed: 3500,

    prevArrow: `<button type='button' class='slick-arrow slick-prev pull-left'><i class="fa-solid fa-arrow-left-long"></i></button>`,
    nextArrow: `<button type='button' class='slick-arrow slick-next pull-right'><i class="fa-solid fa-arrow-right-long"></i></button>`,
    infinite: true,
    responsive: [{
            breakpoint: 769,
            settings: {
                arrows: true,
                slidesToShow: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                centerMode: false,
                slidesToShow: 1
            }
        }
    ]
});