$(document).ready(function(){
	$(".btn").click(function() {
		$("#menu, html, .page_cover").addClass("open");
	});
	
	$(".close, .page_cover").click(function() {
		$("#menu, .page_cover, html").removeClass("open");
	});
	//$("#certiBtn").click(function() {
	//	$(".certiCheck").show();
	//});

	$('ul.bmTabs li').click(function(){
		var tab_id = $(this).attr('data-tab');

		$('ul.bmTabs li').removeClass('current');
		$('.tab-content').removeClass('current');

		$(this).addClass('current');
		$("#"+tab_id).addClass('current');
	});

	init();
	init2();

});

// 팝업
function openPopup(obj) {
	scrollY = $(window).scrollTop();

	$('html, body').css({top:-scrollY}).addClass('scroll-fiexd');
	$('.popup_wrap').prepend('<div class="dimm"></di>');
	$(obj).show();
}
function closePopup() {
	$('html, body').removeAttr('style').removeClass('scroll-fiexd');
	$('.dimm').remove();

	$(window).scrollTop(scrollY);
}

$('.popup_wrap .popup_close, .popup_wrap .btn_close').on('click', function(e) {
	e.preventDefault();

	$(this).closest('.popup_wrap').hide();
	closePopup();
});


// 룰렛 영역
function roulette() {
	var gift;
	var roulette = $(".roulette");
	//var rotationPos = new Array(60,120,180,240,300,360);
	var rotationPos = new Array(30,90,150,210,270,330);
	var clicked  = 0;

	function iniGame(num) {
		gift = num;
		TweenLite.killTweensOf(roulette);
		TweenLite.to(roulette, 0, {css:{rotation:rotationPos[gift]}});
		TweenLite.from(roulette, 5, {css:{rotation:-3000}, onComplete:endGame, ease:Sine.easeOut});
		// console.log("gift 숫자 : "+ (gift +1) +"rotationPos:" + rotationPos );
	}

	function endGame() {
	    console.log('end')
	    if (gift == 0 || gift == 2 || gift == 4) {
	        openPopup('.popup_fail');
	    } else {
	        openPopup('.popup_winning');

	    }
	    var result = $("#result").val();
	    $.ajax({
	        type: "POST",
	        contentType: "application/json; charset=utf-8",
	        url: "/api/roulette/play",
	        data: JSON.stringify({ "result": result }),
	        dataType: "json",
	        async: false,
	        success: function (json) {
	            if (json.result !== "SUCCESS") {
	                alert("정상적으로 참여되지 않았습니다.");
	                return false;
	            } else {
	            }
	        },
	        error: function (jqxhr, status, error) {
	            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
	            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
	        }
	    });
	}

	$('.btn_roulette').on('click', function (e) {
	    var result = $("#result").val();
	    if (result === "EXISTS") {
	        clicked = 1;
	    }
	    if (clicked <= 0) {

	        var array = [], ran = 0;
	        if (result === "FAIL") {
	            array = [0, 2, 4];
	        } else {
	            array = [1, 3, 5];
	        }

	        ran = Math.floor(Math.random() * array.length);
	        iniGame(array[ran]);

	        //iniGame(Math.floor(Math.random() * 6));

		} else if (clicked >= 1){
			e.preventDefault();
			alert('룰렛이벤트는 1일 1회참여입니다.');
		}
		clicked++;
	});
}
roulette();

// 상단바 고정
$(window).on('scroll', function() {
	var headerH = $('#header').height();
	var scrollH = $(window).scrollTop();

	if (scrollH >= headerH) {
		$('#header').addClass('fixed');
	} else {
		$('#header').removeClass('fixed');
	}
});

// 탭슬라이드
function init() {
	//$('#tab1').addClass('hover');
	//$('#tab2').click(function() {
	//	$('#tab1').removeClass('hover');
    //});

    var swiper = new Swiper('.swiper-container', {
        autoHeight: true,
		autoplay:false,
        on: {
            init: function () {
                $(".swiper-slide").css("background-color", 'transparent');
                $(".swiper-slide").css("text-align", 'left');
                //$('#tab1').addClass('hover');
            },
            slideChange: function () {
                if (swiper.activeIndex == 0) {
                    $('#tab1').addClass('current');
                    $('#tab2').removeClass('current');
                    $('#tab3').removeClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
 
                } else if (swiper.activeIndex == 1) {
                    $('#tab1').removeClass('current');
                    $('#tab2').addClass('current');
                    $('#tab3').removeClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
 
                } else if (swiper.activeIndex == 2) {
                    $('#tab1').removeClass('current');
                    $('#tab2').removeClass('current');
                    $('#tab3').addClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera

				} 
            }
        }
    });
 
    $('#tab1').click(function () {
        swiper.slideTo(0, 300, false);
    });
    $('#tab2').click(function () {
        swiper.slideTo(1, 300, false);
    });
    $('#tab3').click(function () {
        swiper.slideTo(2, 300, false);
    });
    if (location.hash) {
        setTimeout(function () {
            $(location.hash.split('-')[0]).click();
        }, 1);
    } else {
        $('#tab1').click();
    }
}

function init2() {
	//$('#tab4').addClass('hover');
	//$('#tab5').click(function() {
	//	$('#tab4').removeClass('hover');
	//});
    var swiper2 = new Swiper('.swiper-container2', {
        autoHeight: true,
		autoplay:false,
        on: {
            init2: function () {
                $(".swiper-slide2").css("background-color", 'transparent');
                $(".swiper-slide2").css("text-align", 'left');
				//$('#tab4').addClass('current');
            },
            slideChange: function () {
                var slider2 = this;
                if (swiper2.activeIndex == 0) {
                    $('#tab4').addClass('current');
                    $('#tab5').removeClass('current');
                    $('#tab6').removeClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
 
                } else if (swiper2.activeIndex == 1) {
                    $('#tab4').removeClass('current');
                    $('#tab5').addClass('current');
                    $('#tab6').removeClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
 
                } else if (swiper2.activeIndex == 2) {
                    $('#tab4').removeClass('current');
                    $('#tab5').removeClass('current');
                    $('#tab6').addClass('current');
 
                    document.body.scrollTop = 0; // For Safari
                    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera

				} 
            }
        }
    });
 
    $('#tab4').click(function () {
        swiper2.slideTo(0, 300, false);
    });
    $('#tab5').click(function () {
        swiper2.slideTo(1, 300, false);
    });
    $('#tab6').click(function () {
        swiper2.slideTo(2, 300, false);
    });

    if (location.hash) {
        setTimeout(function () {
            $(location.hash.split('-')[0]).click();
        }, 1);
    } else {
        $('#tab4').click();
    }
 
}

