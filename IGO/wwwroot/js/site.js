// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // 設定左側導覽
    scrollSpy = new bootstrap.ScrollSpy(document.body, {
        target: '#side-nav',
        offset: 100
    })

    // 修正左側導覽點下去後，畫面不會跑到正確的位置的問題
    $('.nav-link').click(function () {
        divId = $(this).attr('href');
        $('html, body').animate({
            scrollTop: $(divId).offset().top - 60
        }, 100);
    });

    renderSideNav();

    // 瀏覽器resize時，修正左側導覽
    $(window).resize(function () {
        renderSideNav();
    });

    $(window).scroll(function (e) {
        renderSideNav();
    });


});

// 繪製左側導覽
function renderSideNav() {
    var $el = $('.fixedElement'); //600-135=465
    var position = $el.parent().offset().top - 135; // 取得左側導覽在畫面中的初始位置，定位用

    var isPositionFixed = ($el.css('position') == 'fixed');

    // 如果滾到最上方，就把導覽卡在左側
    if ($(this).scrollTop() > position && !isPositionFixed) {
        $el.addClass('affix');
    }
    // 如果沒有，就按照一般顯示
    if ($(this).scrollTop() < position && isPositionFixed) {
        $el.removeClass('affix');
    }
}


