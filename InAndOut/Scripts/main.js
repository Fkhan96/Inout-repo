// $('.sec-1 a').on("click", function(){           
//   $('html, body').animate({
//   scrollTop: $('#2').offset().top
//    }, 1500);
// });

$(".sideform").addClass("open");
  $(".sideform .left h4, .animation-quote > a").click(function(t) {
      t.preventDefault(), $(".sideform").hasClass("closed") ? $(".sideform").animate({
          right: -398
      }, 500, function() {
          $(this).removeClass("closed").addClass("open")
      }) : $(".sideform").animate({
          right: -1
      }, 500, function() {
          $(this).removeClass("open").addClass("closed")
      })
});




$(".sideform .left").click(function (event) {
    $('.formOverlaySide').toggleClass('active');
    event.stopPropagation();
});

/*------------------------------------------- ------- Header Fix---------------------------------------------------------------*/
$(function(){
var shrinkHeader = 100;
$(window).scroll(function() {
var scroll = getCurrentScroll();
  if ( scroll >= shrinkHeader ) {
	   $('header').addClass('fixed');}
	else {
		$('header').removeClass('fixed');}});
function getCurrentScroll() {
return window.pageYOffset;
}});
/*    ----------------------------------------------- Windows Size -------------------------------------------------------- */
var WindowsSize=function(){
var h=$(window).height(),w=$(window).width();
$("#winSize").html("<p>Width: "+w+"<br>Height: "+h+"</p>");};
$(document).ready(function(){WindowsSize();}); 
$(window).resize(function(){WindowsSize();}); 
/*----------------------------------------------------Back to top---------------------------------------------------------------*/
$(document).ready(function(){$("#back-top").hide();$(function(){$(window).scroll(function(){if($(this).scrollTop()>100){$("#back-top").fadeIn()}else{$("#back-top").fadeOut()}});$("#back-top a").click(function(){$("body,html").animate({scrollTop:0},800);return false})})})




// $(".navigation ul li a").click(function(){
//   $( this ).parent().addClass("active");
//   $( this ).parent().siblings().removeClass("active");
// });


$(document).ready(function () {
    $(document).on("scroll", onScroll);
    
    //smoothscroll
    $('a[href^="#"]').on('click', function (e) {
        e.preventDefault();
        $(document).off("scroll");
        
        $('a').each(function () {
            $(this).removeClass('active');
        })
        $(this).addClass('active');
      
        var target = this.hash,
            menu = target;
        $target = $(target);
        $('html, body').stop().animate({
            'scrollTop': $target.offset().top+2
        }, 1000, 'swing', function () {
            window.location.hash = target;
            $(document).on("scroll", onScroll);
        });
    });
});

function onScroll(event){
    var scrollPos = $(document).scrollTop();
    $('#mainNav a').each(function () {
        var currLink = $(this);
        var refElement = $(currLink.attr("href"));
        if (refElement.position().top <= scrollPos && refElement.position().top + refElement.height() > scrollPos) {
            $('#mainNav ul li a').removeClass("active");
            currLink.addClass("active");
        }
        else{
            currLink.removeClass("active");
        }
    });
}





 $('body').on('click', "#chngType", function(event) {
        event.preventDefault();
        var that = $(this);
        var password = $("#pwd");
        var pType = password.attr('type');
        var type = (pType == 'text' ? 'password' : 'text');
        password.attr('type',type);
        
    });

// -------------------------------------------------- Camera Slider JS ------------------------------------------------------------------------//


$(document).ready(function(){
$('#mainslider1').camera({
    loader: 'none',
    pagination: false,
    playPause: false,
    thumbnails: false,
    autoAdvance: true,
    time: 3000,
    navigation: false,
    hover: false,
    opacityOnGrid: false,
    overlayer: true,
    fx: 'random'
  });
});



$('.center').slick({
  centerMode: true,
  centerPadding: '400px',
  slidesToShow: 1,
  autoplay: true,
  speed: 2000,
  arrows: false,
  dots: true,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 3
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    }
  ]
});