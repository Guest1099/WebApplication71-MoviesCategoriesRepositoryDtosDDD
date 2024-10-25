 

function initializeDropdownMenu() {
    var dropdowns = document.querySelectorAll('.dropdown');
    dropdowns.forEach(function (dropdown) {
        var dropbtn = dropdown.querySelector('.dropbtn');
        var content = dropdown.querySelector('.dropdown-content');

        dropbtn.addEventListener('click', function () {
            closeAllDropdowns();
            content.classList.toggle('show');
        });

        window.addEventListener('click', function (event) {
            if (!event.target.matches('.dropbtn')) {
                if (content.classList.contains('show')) {
                    content.classList.remove('show');
                }
            }
        });
    }); 
    function closeAllDropdowns() {
        dropdowns.forEach(function (otherDropdown) {
            var otherContent = otherDropdown.querySelector('.dropdown-content');
            if (otherContent.classList.contains('show')) {
                otherContent.classList.remove('show');
            }
        });
    }
}
document.addEventListener('DOMContentLoaded', function () {
    initializeDropdownMenu();
});

 
 