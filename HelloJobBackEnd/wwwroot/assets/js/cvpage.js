//Range method
const range = document.querySelector('#ranger');
const intervals = document.querySelectorAll('.interval span');
const checkboxes = document.querySelectorAll('input[name="experienceIds"]');

const min = 0;
const max = 5;
const step = 1;

let currentValue = 0;
let currentPercent = 0;

range.addEventListener('mousedown', () => {
    document.addEventListener('mousemove', rangeMove);
});

document.addEventListener('mouseup', () => {
    document.removeEventListener('mousemove', rangeMove);
});

function rangeMove(event) {
    const rangeWidth = range.offsetWidth;
    const rangeLeft = range.getBoundingClientRect().left;
    let newValue = (event.clientX - rangeLeft) / rangeWidth * (max - min);

    if (newValue < min) {
        newValue = min;
    } else if (newValue > max) {
        newValue = max;
    }

    currentValue = Math.round(newValue / step) * step;
    currentPercent = ((currentValue - min) / (max - min)) * 100;

    document.querySelector('.range').style.width = `${currentPercent}%`;
    document.querySelectorAll('.handle').forEach((handle) => {
        handle.style.left = `${currentPercent}%`;
    });

    updateIntervals();
    updateCheckboxes();
}

function updateIntervals() {
    intervals.forEach((interval) => {
        const intervalValue = parseInt(interval.innerText);
        if (currentValue < intervalValue) {
            interval.classList.add('inactive');
        } else {
            interval.classList.remove('inactive');
        }
    });
}

function updateCheckboxes() {
    checkboxes.forEach((checkbox) => {
        const checkboxValue = parseInt(checkbox.value);
        if (checkboxValue <= currentValue) {
            checkbox.checked = true;
            checkbox.parentNode.style.color = '#2196F3';
        } else {
            checkbox.checked = false;
            checkbox.parentNode.style.color = 'initial';
        }
    });
}

updateIntervals();
//Filter's method '
document.getElementById("filterForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var formData = new FormData(this);

    fetch('/Cvpage/FilterData', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text())
        .then(result => {
            document.getElementById("userBlocks").innerHTML = result;
        })
        .catch(error => {
            console.error('Bir hata oluştu:', error);
        });
});

//Cvpage search method
const searchForm = document.querySelector('.search');
const searchInput = searchForm.querySelector('.search-input');
const searchResults = document.querySelector('.search-results');

searchForm.addEventListener('submit', (e) => {
    const searchQuery = e.target.value.trim();
    e.preventDefault();
    window.location.href = "/CvPage/Search?search=${searchQuery}";
});

let timeoutId;

searchInput.addEventListener('input', (e) => {
    const searchQuery = e.target.value.trim();
    if (searchQuery.length < 2) {
        searchResults.innerHTML = '';
        return;
    }
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => {
        fetch(`/Cvpage/Search?search=${searchQuery}`)
            .then(response => response.text())
            .then(data => {
                searchResults.innerHTML = data;
            })
            .catch(error => console.log(error));
    }, 500);
});



//sort method
const sortSelect = document.getElementById("sort2");

const savedSort = localStorage.getItem("selectedSort");
if (savedSort) {
    sortSelect.value = savedSort;
}

sortSelect.addEventListener("change", function () {
    const selectedSort = this.value;
    localStorage.setItem("selectedSort", selectedSort);
    window.location.href = `/CvPage/Index?sort=${selectedSort}`;
});