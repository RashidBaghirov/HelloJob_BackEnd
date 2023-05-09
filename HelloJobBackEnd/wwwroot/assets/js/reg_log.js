const login_btn  = document.querySelectorAll(".login_btn");
const backgray = document.querySelector(".back");
const close_button=document.querySelectorAll(".close_button")

login_btn.forEach((btn)=>{
  btn.addEventListener("click",(e)=>{
    e.preventDefault();
    backgray.classList.add("animate__fadeInDown");
    backgray.classList.add("active");
    document.body.style.overflowY = "hidden";

    setTimeout(function() {
      backgray.classList.remove("animate__fadeInDown");
    }, 1000);
  })
})


close_button.forEach(btn=>{
  btn.addEventListener("click",(e)=>{
    backgray.classList.add("animate__fadeOutUp");
    document.body.style.overflowY = "scroll";
    registration_body.classList.add("d-none");
    login_body.classList.remove("d-none");
    setTimeout(function() {
      backgray.classList.remove("active");
      backgray.classList.remove("animate__fadeOutUp");
    }, 1000);
  })
 
})

const goRegister=document.querySelector(".goRegister");
const goLogin=document.querySelector(".goLogin");
const login_body=document.querySelector(".login_body")
const registration_body=document.querySelector(".registration_body");

goRegister.addEventListener("click",(e)=>{
  e.preventDefault();
  registration_body.classList.remove("d-none");
  login_body.classList.add("d-none");
})


goLogin.addEventListener("click",(e)=>{
  e.preventDefault();
  registration_body.classList.add("d-none");
  login_body.classList.remove("d-none");
})