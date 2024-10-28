document.querySelectorAll('.btn.btn-primary').forEach(button => {
    button.addEventListener('click', () => {
        console.log('Button clicked!');
    });
});
$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    if (scroll >= 60) {
        $('#navbar').addClass('scrolled');
    } else {
        $('#navbar').removeClass('scrolled');
    }
});
