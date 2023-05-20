const vacans_cv = document.querySelector("#vacans_cv");
const cvLink = document.querySelector('.cv-link');
const liked_vacans = vacans_cv.querySelector('.liked_vacans');
const liked_cv = vacans_cv.querySelector('.liked_cv');
const vacansLink = document.querySelector(".vacancy-link")

cvLink.addEventListener('click', () => {
    liked_vacans.classList.add('active');
    liked_cv.classList.remove('active');
    cvLink.classList.add("blue");
    vacansLink.classList.remove("blue");
});

vacansLink.addEventListener('click', () => {
    liked_vacans.classList.remove('active');
    liked_cv.classList.add('active');
    cvLink.classList.remove("blue");
    vacansLink.classList.add("blue");
});


document.addEventListener('click', function (event) {
    if (event.target.classList.contains('deletedcv')) {
        var heartIcon = event.target;
        var parentDiv = heartIcon.closest('.user_block');
        parentDiv.parentNode.removeChild(parentDiv);
    }
});
document.addEventListener('click', function (event) {
    if (event.target.classList.contains('deletedvacans')) {
        var heartIcon = event.target;
        var parentDiv = heartIcon.closest('.vacancies_href');
        parentDiv.parentNode.removeChild(parentDiv);
    }
});