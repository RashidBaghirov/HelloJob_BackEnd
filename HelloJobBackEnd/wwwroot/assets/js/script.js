const menubtn = document.querySelector(".menu_open");
const mobmenu = document.querySelector(".mobile_menu")
menubtn.addEventListener("click", function () {
    mobmenu.classList.add("active");
});

const closebtn = document.querySelector(".menu_close")
closebtn.addEventListener("click", function () {
    mobmenu.classList.remove("active");
});

const likeButtons = document.querySelectorAll('.like');
const unlikedIcons = document.querySelectorAll('.unliked');
const likedIcons = document.querySelectorAll('.liked');

unlikedIcons.forEach((btn, index) => {
    btn.addEventListener('click', () => {

        likedIcons[index].classList.toggle('hidden');
    });
});

likedIcons.forEach((btn, index) => {
    btn.addEventListener('click', () => {
        btn.classList.toggle('hidden');
    });
});


$(".responsive").slick({
    dots: false,
    infinite: true,
    variableWidth: true,
    speed: 300,
    autoplay: false,
    autoplaySpeed: 2000,
    slidesToShow: 4,
    slidesToScroll: 1,
    prevArrow: '<i class="fa-solid fa-chevron-left left_arrow">',
    nextArrow: ' <i class="fa-solid fa-chevron-right right_arrow"> ',
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: false,
            },
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
            },
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
            },
        },
    ],
});


const login_btn = document.querySelectorAll(".login_btn");
const backgray = document.querySelector(".back");
const close_button = document.querySelectorAll(".close_button")

login_btn.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        e.preventDefault();
        backgray.classList.add("animate__fadeInDown");
        backgray.classList.add("active");
        document.body.style.overflowY = "hidden";

        setTimeout(function () {
            backgray.classList.remove("animate__fadeInDown");
        }, 1000);
    })
})


close_button.forEach(btn => {
    btn.addEventListener("click", (e) => {
        backgray.classList.add("animate__fadeOutUp");
        document.body.style.overflowY = "scroll";
        registration_body.classList.add("d-none");
        login_body.classList.remove("d-none");
        setTimeout(function () {
            backgray.classList.remove("active");
            backgray.classList.remove("animate__fadeOutUp");
        }, 1000);
    })

})

const goRegister = document.querySelector(".goRegister");
const goLogin = document.querySelector(".goLogin");
const login_body = document.querySelector(".login_body")
const registration_body = document.querySelector(".registration_body");

goRegister.addEventListener("click", (e) => {
    e.preventDefault();
    registration_body.classList.remove("d-none");
    login_body.classList.add("d-none");
})


goLogin.addEventListener("click", (e) => {
    e.preventDefault();
    registration_body.classList.add("d-none");
    login_body.classList.remove("d-none");
})





$(document).on("click", ".show-register-modal", function (e) {
    e.preventDefault();

    fetch('https://localhost:7120//account/register')
        .then(response => response.text())
        .then(data => {
            $("#register-modal-detail").html(data)

        })
});

///////////////////////////////////////////////////////////////


$(document).on("click", ".show-login-modal", function (e) {
    e.preventDefault();

    fetch('https://localhost:44363/account/login')
        .then(response => response.text())
        .then(data => {
            $("#login-modal-detail").html(data)

        })
});