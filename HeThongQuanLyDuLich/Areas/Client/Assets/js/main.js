function myFunc(i) {
    let element = document.getElementById(i.toString());
    if (element.classList.contains("ShowDetailHanhTrinh")) {
        element.classList.remove("ShowDetailHanhTrinh")
    } else {
        element.classList.add("ShowDetailHanhTrinh");
    }
}


var wrapToScoll = document.getElementById("wrapToScroll");
var a = $('.wrap_datTourComponent').offset().top;
$(window).scroll(function () {
    if ($(window).scrollTop() >= a - 20) {
        wrapToScoll.classList.add("scrolled")
    } else {
        wrapToScoll.classList.remove("scrolled")

    }
});
